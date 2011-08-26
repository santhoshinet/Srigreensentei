using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CCAvenueIntegrationDL;

namespace SriGreenShoppingCart.Controllers
{
    public class ProductsController : Controller
    {
        public ActionResult Products(string categoryname, int pageindex)
        {
            ViewData["Products"] = GetProductList(categoryname, pageindex);
            ViewData["Category"] = categoryname;

            var scope = ObjectScopeProvider1.GetNewObjectScope();
            int count = (from c in scope.GetOqlQuery<Category>().ExecuteEnumerable()
                         where
                             c.Name.ToLower().Equals(categoryname.ToLower()) && c.Deletedstatus == DeleteStatus.Working &&
                             c.Categorytype == Categorytype.Special
                         select c).Count();
            if (count == 0)
            {
                // it is not special category
                if (pageindex == 1)
                    return View();
            }
            else
            {
                // it is a special category
                if (pageindex == 1)
                    return View(categoryname);
            }
            // this is for ajax
            return View("ProductsList");
        }

        public ActionResult All()
        {
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            ViewData["categories"] = (from c in scope.GetOqlQuery<Category>().ExecuteEnumerable()
                                      where c.Deletedstatus == DeleteStatus.Working
                                      select c).ToList();
            return View();
        }

        public FileContentResult Photo(string id)
        {
            try
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                List<File> files = (from c in scope.GetOqlQuery<File>().ExecuteEnumerable()
                                    where c.ID.ToString().Equals(id)
                                    select c).ToList();
                if (files.Count > 0)
                {
                    return File(files[0].Filedata, files[0].MimeType, files[0].Filename);
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        private static List<Product> GetProductList(string categoryname, int pageindex)
        {
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            List<Product> products = (from c in scope.GetOqlQuery<Category>().ExecuteEnumerable()
                                      from d in c.Products
                                      where c.Name != null && c.Name.ToLower().Equals(categoryname.ToLower().Trim()) && c.Deletedstatus == DeleteStatus.Working
                                      select d).Skip(((pageindex - 1) * 20)).Take(20).ToList();
            return products;
        }
    }
}