using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CCAvenueIntegrationDL;
using SriGreenShoppingCart.Models;
using Telerik.OpenAccess;
using File = CCAvenueIntegrationDL.File;

namespace SriGreenShoppingCart.Controllers
{
    public class ControlpanelController : Controller
    {
        [HttpGet]
        public ActionResult Categories()
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (Checkauthorization(scope, User.Identity.Name))
                {
                    ViewData["categories"] = (from c in scope.GetOqlQuery<Category>().ExecuteEnumerable()
                                              where c.Deletedstatus == DeleteStatus.Working
                                              select c).ToList();
                    return View(new CategoriesModel());
                }

                ViewData["Status"] = "You are not authorized to do this operation";
                return View("Status");
            }
            return RedirectToAction("LogOn");
        }

        [HttpPost]
        public ActionResult Categories(CategoriesModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (Checkauthorization(scope, User.Identity.Name))
                {
                    if (ModelState.IsValid)
                    {
                        int count = (from c in scope.GetOqlQuery<Category>().ExecuteEnumerable()
                                     where c.Name.Equals(model.CategoryName) && c.Deletedstatus == DeleteStatus.Working
                                     select c).Count();
                        if (count == 0)
                        {
                            scope.Transaction.Begin();
                            var category = new Category();
                            category.Name = model.CategoryName;
                            category.Description = model.CategoryDescription;
                            category.Id = Guid.NewGuid();
                            category.Deletedstatus = DeleteStatus.Working;
                            category.Categorytype = Categorytype.General;
                            scope.Add(category);
                            scope.Transaction.Commit();
                            return RedirectToAction("Categories");
                        }
                        ModelState.AddModelError("CategoryName", "The category is already exists.");
                        ViewData["categories"] = (from c in scope.GetOqlQuery<Category>().ExecuteEnumerable()
                                                  where c.Deletedstatus == DeleteStatus.Working
                                                  select c).ToList();
                        return View(model);
                    }
                    ViewData["categories"] = (from c in scope.GetOqlQuery<Category>().ExecuteEnumerable()
                                              where c.Deletedstatus == DeleteStatus.Working
                                              select c).ToList();
                    return View(model);
                }
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("Status");
            }
            return RedirectToAction("LogOn");
        }

        [HttpGet]
        public ActionResult Products()
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (Checkauthorization(scope, User.Identity.Name))
                {
                    var productModel = new ProductModel();
                    productModel.CategoryName = (from c in scope.GetOqlQuery<Category>().ExecuteEnumerable()
                                                 where c.Deletedstatus == DeleteStatus.Working
                                                 select c).Select(prod =>
                      new SelectListItem
                      {
                          Selected = false,
                          Text = prod.Name,
                          Value = prod.Id.ToString()
                      });
                    return View(productModel);
                }
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("Status");
            }
            return RedirectToAction("LogOn");
        }

        [HttpPost]
        public ActionResult Products(ProductModel model, HttpPostedFileBase image)
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (Checkauthorization(scope, User.Identity.Name))
                {
                    model.CategoryName = (from c in scope.GetOqlQuery<Category>().ExecuteEnumerable()
                                          where c.Deletedstatus == DeleteStatus.Working
                                          select c).Select(prod =>
                      new SelectListItem
                      {
                          Selected = false,
                          Text = prod.Name,
                          Value = prod.Id.ToString()
                      });

                    if (!string.IsNullOrEmpty(model.ProductName) && !string.IsNullOrEmpty(model.ProductDescription) && !string.IsNullOrEmpty(model.ProductPrice) && image != null && image.ContentLength != 0)
                    {
                        Guid categoryId;
                        try
                        {
                            categoryId = new Guid(Request.Form["CategoryName"]);
                        }
                        catch (Exception)
                        {
                            categoryId = Guid.Empty;
                        }
                        if (categoryId != Guid.Empty)
                        {
                            var categories = (from c in scope.GetOqlQuery<Category>().ExecuteEnumerable()
                                              where c.Id.Equals(categoryId)
                                              select c).ToList();
                            if (categories.Count > 0)
                            {
                                bool exitFlag = false;
                                foreach (var category in categories)
                                {
                                    if (!exitFlag)
                                    {
                                        foreach (var selectListItem in model.CategoryName)
                                        {
                                            if (selectListItem.Value == category.Id.ToString())
                                            {
                                                selectListItem.Selected = true;
                                                exitFlag = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                double price;
                                try
                                {
                                    price = Convert.ToDouble(model.ProductPrice);
                                }
                                catch (Exception)
                                {
                                    price = 0.0;
                                }
                                if (price != 0.0)
                                {
                                    scope.Transaction.Begin();
                                    var product = new Product
                                                      {
                                                          Category = categories[0].Name,
                                                          Name = model.ProductName,
                                                          Id = Guid.NewGuid(),
                                                          Price = price,
                                                          Description = model.ProductDescription
                                                      };
                                    // saving image here
                                    try
                                    {
                                        var productFile = new File { Filename = image.FileName };
                                        Stream fileStream = image.InputStream;
                                        int fileLength = image.ContentLength;
                                        productFile.Filedata = new byte[fileLength];
                                        fileStream.Read(productFile.Filedata, 0, fileLength);
                                        productFile.MimeType = image.ContentType;
                                        productFile.ID = Guid.NewGuid();
                                        product.Productfile = productFile;
                                    }
                                    catch
                                    { }
                                    categories[0].Products.Add(product);
                                    scope.Add(categories[0]);
                                    scope.Transaction.Commit();
                                    return RedirectToAction("Products");
                                }
                                ModelState.Remove("CategoryName");
                                if (ModelState.IsValidField("ProductPrice"))
                                    ModelState.AddModelError("ProductPrice", "Input proper price value.");
                                return View(model);
                            }
                        }
                        ModelState.Remove("CategoryName");
                        return View(model);
                    }
                    ModelState.Remove("CategoryName");
                    return View(model);
                }
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("Status");
            }
            return RedirectToAction("LogOn");
        }

        public ActionResult ProductList(string categoryid)
        {
            if (User.Identity.IsAuthenticated)
            {
                Guid categoryId;
                try
                {
                    categoryId = new Guid(categoryid);
                }
                catch (Exception)
                {
                    categoryId = Guid.Empty;
                }
                if (categoryId != Guid.Empty)
                {
                    var scope = ObjectScopeProvider1.GetNewObjectScope();
                    if (Checkauthorization(scope, User.Identity.Name))
                    {
                        List<Category> categories = (from c in scope.GetOqlQuery<Category>().ExecuteEnumerable()
                                                     where c.Id.Equals(categoryId)
                                                     select c).ToList();
                        if (categories.Count > 0)
                        {
                            ViewData["ProductList"] = categories[0].Products.ToList();
                            return View();
                        }
                        return RedirectToAction("Home");
                    }
                    ViewData["Status"] = "You are not authorized to do this operation";
                    return View("Status");
                }
                return null;
            }
            return RedirectToAction("LogOn");
        }

        [HttpGet]
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ValidateUser(model.UserName, model.Password))
                {
                    FormsService.SignIn(model.UserName, model.RememberMe);
                    //FormsAuthentication.SetAuthCookie(model.UserName, true);
                    //HttpCookie myCookie = FormsAuthentication.GetAuthCookie(model.UserName, true);
                    //myCookie.Domain = "srigreensentei.com";
                    //myCookie.Path = "/";
                    //myCookie.Expires = DateTime.Now.AddDays(1);
                    //Response.AppendCookie(myCookie);

                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Home");
                }
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Home()
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (Checkauthorization(scope, User.Identity.Name))
                {
                    return View();
                }
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("Status");
            }
            return RedirectToAction("LogOn");
        }

        [HttpGet]
        public ActionResult Editproduct(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                Guid productId;
                try
                {
                    productId = new Guid(id);
                }
                catch (Exception)
                {
                    productId = Guid.Empty;
                }
                if (productId != Guid.Empty)
                {
                    var scope = ObjectScopeProvider1.GetNewObjectScope();
                    if (Checkauthorization(scope, User.Identity.Name))
                    {
                        List<Product> products = (from c in scope.GetOqlQuery<Product>().ExecuteEnumerable()
                                                  where c.Id.Equals(productId)
                                                  select c).ToList();
                        if (products.Count > 0)
                        {
                            var productModel = new ProductModel();
                            productModel.ProductDescription = products[0].Description;
                            productModel.ProductName = products[0].Name;
                            productModel.ProductPrice = products[0].Price.ToString();
                            productModel.Id = products[0].Id.ToString();
                            productModel.CategoryName = (from c in scope.GetOqlQuery<Category>().ExecuteEnumerable()
                                                         select c).Select(prod =>
                      new SelectListItem
                      {
                          Selected = false,
                          Text = prod.Name,
                          Value = prod.Id.ToString()
                      });
                            foreach (var selectListItem in productModel.CategoryName)
                            {
                                if (selectListItem.Text.ToLower() == products[0].Category.ToLower())
                                {
                                    selectListItem.Selected = true;
                                    break;
                                }
                            }
                            return View(productModel);
                        }
                    }
                    ViewData["Status"] = "You are not authorized to do this operation";
                    return View("Status");
                }
                return RedirectToAction("Home");
            }
            return RedirectToAction("LogOn");
        }

        [HttpPost]
        public ActionResult Editproduct(ProductModel model, HttpPostedFileBase image)
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (Checkauthorization(scope, User.Identity.Name))
                {
                    if (!string.IsNullOrEmpty(model.ProductName) && !string.IsNullOrEmpty(model.ProductDescription) && !string.IsNullOrEmpty(model.ProductPrice))
                    {
                        double price;
                        try
                        {
                            price = Convert.ToDouble(model.ProductPrice);
                        }
                        catch (Exception)
                        {
                            price = 0.0;
                        }
                        if (price != 0.0)
                        {
                            List<Product> products = (from c in scope.GetOqlQuery<Product>().ExecuteEnumerable()
                                                      where c.Id.ToString().Equals(model.Id)
                                                      select c).ToList();
                            if (products.Count > 0)
                            {
                                scope.Transaction.Begin();
                                products[0].Name = model.ProductName;
                                products[0].Description = model.ProductDescription;
                                products[0].Price = price;
                                if (image != null && image.ContentLength != 0)
                                {
                                    // updating image here
                                    try
                                    {
                                        products[0].Productfile.Filename = image.FileName;
                                        Stream fileStream = image.InputStream;
                                        int fileLength = image.ContentLength;
                                        products[0].Productfile.Filedata = new byte[fileLength];
                                        fileStream.Read(products[0].Productfile.Filedata, 0, fileLength);
                                        products[0].Productfile.MimeType = image.ContentType;
                                    }
                                    catch
                                    {
                                    }
                                }
                                scope.Add(products[0]);
                                scope.Transaction.Commit();
                                ViewData["Status"] = "Product updated successfully. <br /> now you can close this window.";
                                return View("success_small");
                            }
                            ModelState.Remove("CategoryName");
                            return View(model);
                        }
                        ModelState.Remove("CategoryName");
                        if (ModelState.IsValidField("ProductPrice"))
                            ModelState.AddModelError("ProductPrice", "Input proper price value.");
                        return View(model);
                    }
                    ModelState.Remove("CategoryName");
                    return View(model);
                }
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("success");
            }
            return RedirectToAction("LogOn");
        }

        [HttpGet]
        public ActionResult CustomerList(string id)
        {
            int pageindex;// here id is used as pageindex
            try
            {
                if (string.IsNullOrEmpty(id))
                    pageindex = 1;
                else
                    pageindex = Convert.ToInt16(id);
            }
            catch (Exception)
            {
                pageindex = 0;
            }
            if (pageindex != 0)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                var userList = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                where c.IsheAdmin.Equals(false)
                                select c).Skip(((pageindex - 1) * 20)).Take(20).ToList();
                ViewData["CustomerList"] = userList;
                if (pageindex == 1)
                    return View();
                // for ajax
                return View("Customerlistajax");
            }

            return RedirectToAction("Home");
        }

        [HttpGet]
        public ActionResult CustomerProfile(string id)
        {
            string custName = id; // here id is used as customer name
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            var userList = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                            where c.IsheAdmin.Equals(false) && c.Username.ToLower().Equals(custName.ToLower())
                            select c).ToList();
            ViewData["CustomerFullDetail"] = userList;
            return View();
        }

        [HttpGet]
        public ActionResult TransactionList(string id)
        {
            int pageindex;// here id is used as pageindex
            try
            {
                if (string.IsNullOrEmpty(id))
                    pageindex = 1;
                else
                    pageindex = Convert.ToInt16(id);
            }
            catch (Exception)
            {
                pageindex = 0;
            }
            if (pageindex != 0)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                var userList = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                select c).Skip(((pageindex - 1) * 20)).Take(20).ToList();
                ViewData["CustomerList"] = userList;
                if (pageindex == 1)
                    return View();
                // for ajax
                return View("TransactionListajax");
            }

            return RedirectToAction("Home");
        }

        [HttpGet]
        public ActionResult LatestNews()
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (Checkauthorization(scope, User.Identity.Name))
                {
                    List<LatestNews> latestNewses = (from c in scope.GetOqlQuery<LatestNews>().ExecuteEnumerable()
                                                     select c).ToList();
                    if (latestNewses.Count > 0)
                        return View(new LatestnewsModel() { Latestnews = latestNewses[0].News });
                    return View(new LatestnewsModel() { Latestnews = "Input your updates here." });
                }
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("Status");
            }
            return RedirectToAction("LogOn");
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult LatestNews(LatestnewsModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (Checkauthorization(scope, User.Identity.Name))
                {
                    if (ModelState.IsValid)
                    {
                        List<LatestNews> latestNewses = (from c in scope.GetOqlQuery<LatestNews>().ExecuteEnumerable()
                                                         select c).ToList();
                        if (latestNewses.Count > 0)
                        {
                            scope.Transaction.Begin();
                            latestNewses[0].News = model.Latestnews;
                            scope.Add(latestNewses[0]);
                            scope.Transaction.Commit();
                        }
                        else
                        {
                            scope.Transaction.Begin();
                            var latestnews = new LatestNews { News = model.Latestnews };
                            scope.Add(latestnews);
                            scope.Transaction.Commit();
                        }
                        return RedirectToAction("Home");
                    }
                    return View(model);
                }
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("Status");
            }
            return RedirectToAction("LogOn");
        }

        [HttpGet]
        public ActionResult LatestNewsEvents()
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (Checkauthorization(scope, User.Identity.Name))
                {
                    List<LatestNews> latestNewses = (from c in scope.GetOqlQuery<LatestNews>().ExecuteEnumerable()
                                                     select c).ToList();
                    if (latestNewses.Count > 0)
                        return View(new LatestnewsEventsModel() { Latestnews = latestNewses[0].NewsandEvents });
                    return View(new LatestnewsEventsModel() { Latestnews = "Input your updates here." });
                }
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("Status");
            }
            return RedirectToAction("LogOn");
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult LatestNewsEvents(LatestnewsEventsModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (Checkauthorization(scope, User.Identity.Name))
                {
                    if (ModelState.IsValid)
                    {
                        List<LatestNews> latestNewses = (from c in scope.GetOqlQuery<LatestNews>().ExecuteEnumerable()
                                                         select c).ToList();
                        if (latestNewses.Count > 0)
                        {
                            scope.Transaction.Begin();
                            latestNewses[0].NewsandEvents = model.Latestnews;
                            scope.Add(latestNewses[0]);
                            scope.Transaction.Commit();
                        }
                        else
                        {
                            scope.Transaction.Begin();
                            var latestnews = new LatestNews { NewsandEvents = model.Latestnews };
                            scope.Add(latestnews);
                            scope.Transaction.Commit();
                        }
                        return RedirectToAction("Home");
                    }
                    return View(model);
                }
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("Status");
            }
            return RedirectToAction("LogOn");
        }

        [HttpGet]
        public ActionResult DeletedCategories()
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (Checkauthorization(scope, User.Identity.Name))
                {
                    ViewData["categories"] = (from c in scope.GetOqlQuery<Category>().ExecuteEnumerable()
                                              where c.Deletedstatus == DeleteStatus.Recycled
                                              select c).ToList();
                    return View(new CategoriesModel());
                }

                ViewData["Status"] = "You are not authorized to do this operation";
                return View("Status");
            }
            return RedirectToAction("LogOn");
        }

        public string PermanentDeletecategory(string categoryid)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var scope = ObjectScopeProvider1.GetNewObjectScope();
                    if (Checkauthorization(scope, User.Identity.Name))
                    {
                        var categoryUid = new Guid(categoryid);
                        List<Category> categories = (from c in scope.GetOqlQuery<Category>().ExecuteEnumerable()
                                                     where c.Id.Equals(categoryUid)
                                                     select c).ToList();
                        if (categories.Count > 0)
                        {
                            scope.Transaction.Begin();
                            scope.Remove(categories[0]);
                            scope.Transaction.Commit();
                            return "removed";
                        }
                        return "notfound";
                    }
                    return "You are not authorized to do this operation";
                }
                return "loggedout";
            }
            catch (Exception)
            {
                return "failed";
            }
        }

        public string Restorecategory(string categoryid)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var scope = ObjectScopeProvider1.GetNewObjectScope();
                    if (Checkauthorization(scope, User.Identity.Name))
                    {
                        var categoryUid = new Guid(categoryid);
                        List<Category> categories = (from c in scope.GetOqlQuery<Category>().ExecuteEnumerable()
                                                     where c.Id.Equals(categoryUid)
                                                     select c).ToList();
                        if (categories.Count > 0)
                        {
                            scope.Transaction.Begin();
                            categories[0].Deletedstatus = DeleteStatus.Working;
                            scope.Add(categories[0]);
                            scope.Transaction.Commit();
                            return "removed";
                        }
                        return "notfound";
                    }
                    return "You are not authorized to do this operation";
                }
                return "loggedout";
            }
            catch (Exception)
            {
                return "failed";
            }
        }

        public string UpdateTransaction(string transactionID, string actions)
        {
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            var transactions = (from c in scope.GetOqlQuery<PreTransactionDetails>().ExecuteEnumerable()
                                where c.TransactionId != null && c.TransactionId.Equals(transactionID)
                                select c).ToList();
            if (transactions.Count > 0)
            {
                switch (actions)
                {
                    case "Pending":
                        {
                            scope.Transaction.Begin();
                            transactions[0].TransactionStatus = TransactionStatus.Pending;
                            scope.Add(transactions[0]);
                            scope.Transaction.Commit();
                            return "modified";
                        }
                    case "Success":
                        {
                            scope.Transaction.Begin();
                            transactions[0].TransactionStatus = TransactionStatus.Completed;
                            scope.Add(transactions[0]);
                            scope.Transaction.Commit();
                            return "modified";
                        }
                    case "Clear cart":
                        {
                            scope.Transaction.Begin();
                            transactions[0].MyCarts = new List<MyCart>();
                            scope.Add(transactions[0]);
                            scope.Transaction.Commit();
                            return "cleared";
                        }
                }
            }
            return "";
        }

        public string Viewproductinfo(string productid)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var scope = ObjectScopeProvider1.GetNewObjectScope();
                    if (Checkauthorization(scope, User.Identity.Name))
                    {
                        var productUid = new Guid(productid);
                        List<Product> products = (from c in scope.GetOqlQuery<Product>().ExecuteEnumerable()
                                                  where c.Id.Equals(productUid)
                                                  select c).ToList();
                        if (products.Count > 0)
                        {
                            string output = @"<td></td><td><img alt='' class='product_image' src='/Products/Photo/" +
                                            products[0].Productfile.ID + "' title='" + products[0].Name + "' /></td><td>" +
                                            products[0].Name + "</td><td>" + products[0].Description + "</td><td>" +
                                            products[0].Price +
                                            "</td><td style='width: 150px'><span class='delete_button'><img src='/Images/ico-delete.gif' />delete</span></td><td style='width: 100px'><span class='edit_button' href='/Controlpanel/editproduct/" +
                                            products[0].Id + "'><img src='/Images/edit.gif' />edit</span></td>";
                            return output;
                        }
                    }
                    return "You are not authorized to do this operation";
                }
            }
            catch (Exception)
            {
            }
            return "";
        }

        public string Deleteproduct(string productid)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var scope = ObjectScopeProvider1.GetNewObjectScope();
                    if (Checkauthorization(scope, User.Identity.Name))
                    {
                        var productUid = new Guid(productid);
                        List<Category> categories = (from c in scope.GetOqlQuery<Category>().ExecuteEnumerable()
                                                     from d in c.Products
                                                     where d.Id.Equals(productUid)
                                                     select c).ToList();
                        if (categories.Count > 0)
                        {
                            List<Product> products = (from c in scope.GetOqlQuery<Product>().ExecuteEnumerable()
                                                      where c.Id.Equals(productUid)
                                                      select c).ToList();
                            if (products.Count > 0)
                            {
                                foreach (var category in categories)
                                {
                                    if (category.Products.Contains(products[0]))
                                    {
                                        scope.Transaction.Begin();
                                        category.Products.Remove(products[0]);
                                        scope.Add(category);
                                        scope.Transaction.Commit();
                                    }
                                }
                            }
                            return "removed";
                        }
                        return "notfound";
                    }
                    return "You are not authorized to do this operation";
                }
                return "loggedout";
            }
            catch (Exception)
            {
                return "failed";
            }
        }

        public string Deletecategory(string categoryid)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var scope = ObjectScopeProvider1.GetNewObjectScope();
                    if (Checkauthorization(scope, User.Identity.Name))
                    {
                        var categoryUid = new Guid(categoryid);
                        List<Category> categories = (from c in scope.GetOqlQuery<Category>().ExecuteEnumerable()
                                                     where c.Id.Equals(categoryUid) && c.Categorytype == Categorytype.General
                                                     select c).ToList();
                        if (categories.Count > 0)
                        {
                            scope.Transaction.Begin();
                            //scope.Remove(categories[0]);
                            categories[0].Deletedstatus = DeleteStatus.Recycled;
                            scope.Add(categories[0]);
                            scope.Transaction.Commit();
                            return "removed";
                        }
                        return "notfound";
                    }
                    return "You are not authorized to do this operation";
                }
                return "loggedout";
            }
            catch (Exception)
            {
                return "failed";
            }
        }

        public string Editcategory(string categoryid, string categoryname, string categorydesc)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var scope = ObjectScopeProvider1.GetNewObjectScope();
                    if (Checkauthorization(scope, User.Identity.Name))
                    {
                        var categoryUid = new Guid(categoryid);
                        List<Category> categories = (from c in scope.GetOqlQuery<Category>().ExecuteEnumerable()
                                                     where c.Id.Equals(categoryUid)
                                                     select c).ToList();
                        if (categories.Count > 0)
                        {
                            scope.Transaction.Begin();
                            categories[0].Name = categoryname;
                            categories[0].Description = categorydesc;
                            scope.Add(categories[0]);
                            scope.Transaction.Commit();
                            return "updated";
                        }
                        return "notfound";
                    }
                    return "You are not authorized to do this operation";
                }
                return "loggedout";
            }
            catch (Exception)
            {
                return "failed";
            }
        }

        private static bool Checkauthorization(IObjectScope scope, string username)
        {
            List<User> users = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                where c.Username.ToLower().Equals(username.ToLower())
                                select c).ToList();
            if (users.Count > 0 && users[0].IsheAdmin)
                return true;
            return false;
        }

        public IFormsAuthenticationService FormsService { get; set; }

        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }
    }
}