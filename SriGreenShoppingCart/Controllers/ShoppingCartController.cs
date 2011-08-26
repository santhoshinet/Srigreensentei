using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI.MobileControls;
using CCAvenueIntegrationDL;

namespace SriGreenShoppingCart.Controllers
{
    public class ShoppingCartController : Controller
    {
        //
        // GET: /ShoppingCart/
        public string AddCart(string productId)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var scope = ObjectScopeProvider1.GetNewObjectScope();
                    var productUId = new Guid(productId);
                    List<User> users = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                        where !c.Username.Equals(DBNull.Value) && c.Username.ToLower().Equals(User.Identity.Name.ToLower())
                                        select c).ToList();
                    if (users.Count > 0)
                    {
                        List<Product> products = (from c in scope.GetOqlQuery<Product>().ExecuteEnumerable()
                                                  where c.Id.Equals(productUId)
                                                  select c).ToList();
                        if (products.Count > 0)
                        {
                            List<PreTransactionDetails> preTransactionDetailses = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                                                                   from d in c.PreTransactionDetailses
                                                                                   where !c.Username.Equals(DBNull.Value) && c.Username.ToLower().Equals(User.Identity.Name.ToLower()) && d.TransactionStatus == TransactionStatus.Pending
                                                                                   select d).ToList();

                            // verifying whether the same product is added or not
                            List<MyCart> myCarts = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                                    from d in c.PreTransactionDetailses
                                                    from e in d.MyCarts
                                                    where !c.Username.Equals(DBNull.Value) && c.Username.ToLower().Equals(User.Identity.Name.ToLower()) && d.TransactionStatus == TransactionStatus.Pending && e.Product != null && e.Product.Id.Equals(productUId)
                                                    select e).ToList();
                            if (myCarts.Count == 0)
                            {
                                if (preTransactionDetailses.Count == 0)
                                {
                                    scope.Transaction.Begin();
                                    var transactiondetails = new PreTransactionDetails();
                                    transactiondetails.TransactionStatus = TransactionStatus.Pending;
                                    var random = new Random();
                                    string randomNumner;
                                    int count;
                                    do
                                    {
                                        randomNumner= random.Next(10000000, 90000000).ToString();
                                        string numner = randomNumner;
                                        count =
                                            (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                             from d in c.PreTransactionDetailses
                                             where c.PreTransactionDetailses != null && d.TransactionId != null && d.TransactionId.Equals(numner)
                                             select d).Count();
                                    } while (count != 0);
                                    transactiondetails.TransactionId = randomNumner;
                                    var mycart = new MyCart { Quantity = 1, Product = products[0] };
                                    transactiondetails.MyCarts.Add(mycart);
                                    users[0].PreTransactionDetailses.Add(transactiondetails);
                                    scope.Add(users[0]);
                                    scope.Transaction.Commit();
                                }
                                else
                                {
                                    scope.Transaction.Begin();
                                    var mycart = new MyCart();
                                    mycart.Quantity = 1;
                                    mycart.Product = products[0];
                                    preTransactionDetailses[0].MyCarts.Add(mycart);
                                    scope.Add(preTransactionDetailses[0]);
                                    scope.Transaction.Commit();
                                }
                            }
                            return "added";
                        }
                    }
                    return "notfound";
                }
                return "loggedout";
            }
            catch (Exception)
            {
                return "failed";
            }
        }

        public string DeleteCart(string productId)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var scope = ObjectScopeProvider1.GetNewObjectScope();
                    var productUId = new Guid(productId);
                    List<User> users = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                        where !c.Username.Equals(DBNull.Value) && c.Username.ToLower().Equals(User.Identity.Name.ToLower())
                                        select c).ToList();
                    if (users.Count > 0)
                    {
                        List<Product> products = (from c in scope.GetOqlQuery<Product>().ExecuteEnumerable()
                                                  where c.Id.Equals(productUId)
                                                  select c).ToList();
                        if (products.Count > 0)
                        {
                                List<MyCart> myCarts = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                                                                   from d in c.PreTransactionDetailses
                                                                                   from e in d.MyCarts
                                                                                   where !c.Username.Equals(DBNull.Value) && c.Username.ToLower().Equals(User.Identity.Name.ToLower()) && d.TransactionStatus == TransactionStatus.Pending && e.Product != null && e.Product.Id.Equals(productUId)
                                                                                   select e).ToList();

                                if (myCarts.Count > 0)
                                {
                                    List<PreTransactionDetails> preTransactionDetailses = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                                            from d in c.PreTransactionDetailses
                                                            from e in d.MyCarts
                                                            where !c.Username.Equals(DBNull.Value) && c.Username.ToLower().Equals(User.Identity.Name.ToLower()) && d.TransactionStatus == TransactionStatus.Pending && e.Product != null && e.Product.Id.Equals(productUId)
                                                            select d).ToList();

                                    if (preTransactionDetailses[0].MyCarts.Contains(myCarts[0]))
                                    {
                                        scope.Transaction.Begin();
                                        preTransactionDetailses[0].MyCarts.Remove(myCarts[0]);
                                        scope.Add(preTransactionDetailses[0]);
                                        scope.Transaction.Commit();   
                                    }
                                }
                            return "removed";
                        }
                    }
                    return "notfound";
                }
                return "loggedout";
            }
            catch (Exception)
            {
                return "failed";
            }
        }

        public ActionResult ShowCart()
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                ViewData["Cart"] = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                    from d in c.PreTransactionDetailses
                                    from e in d.MyCarts
                                    where !c.Username.Equals(DBNull.Value) && c.Username.ToLower().Equals(User.Identity.Name.ToLower()) && d.TransactionStatus == TransactionStatus.Pending && e.Product != null 
                                    select e).ToList(); ;
                return View();
            }
            return Redirect("/Account/LogOn");
        }

        public ActionResult ViewCart(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (id == null)
                {
                    ViewData["Cart"] = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                        from d in c.PreTransactionDetailses
                                        from e in d.MyCarts
                                        where
                                            !c.Username.Equals(DBNull.Value) &&
                                            c.Username.ToLower().Equals(User.Identity.Name.ToLower()) &&
                                            d.TransactionStatus == TransactionStatus.Pending && e.Product != null
                                        select e).ToList();
                    
                }
                else
                {
                    ViewData["Cart"] = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                        from d in c.PreTransactionDetailses
                                        from e in d.MyCarts
                                        where
                                            !c.Username.Equals(DBNull.Value) &&
                                              d.TransactionId != null  && d.TransactionId.Equals(id) && e.Product != null
                                        select e).ToList();
                }
                return View();
            }
            return Redirect("/Account/LogOn");
        }

        public ActionResult PaymentOptions()
        {
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            var mycarts = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                           from d in c.PreTransactionDetailses
                           from e in d.MyCarts
                           where !c.Username.Equals(DBNull.Value) &&
                               c.Username.ToLower().Equals(User.Identity.Name.ToLower()) &&
                               d.TransactionStatus == TransactionStatus.Pending && e.Product != null
                           select e).ToList();
            ;
            foreach (var mycart in mycarts)
            {
                int quantity;
                try
                {
                    quantity  = Convert.ToInt16(Request.Form[mycart.Product.Name]);
                }
                catch (Exception)
                {
                    quantity = 1;
                }
                scope.Transaction.Begin();
                mycart.Quantity = quantity;
                scope.Add(mycart);
                scope.Transaction.Commit();
            }
            return View();
        }

        public ActionResult Offlinepayment()
        {
            if (User.Identity.IsAuthenticated)
            {
                CalculateCartAmount();
                return View();
            }
            return Redirect("/Account/LogOn");
        }

        public ActionResult Checkout()
        {
            if (User.Identity.IsAuthenticated)
            {
                string transactionID = string.Empty;
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                var mycarts = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                               from d in c.PreTransactionDetailses
                               from e in d.MyCarts
                               where !c.Username.Equals(DBNull.Value) &&
                                   c.Username.ToLower().Equals(User.Identity.Name.ToLower()) &&
                                   d.TransactionStatus == TransactionStatus.Pending && e.Product != null
                               select e).ToList();
                var users = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                             where c.Username.ToLower().Equals(User.Identity.Name.ToLower())
                             select c).ToList();

                if (users.Count > 0)
                {
                    var pretransactions = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                           from d in c.PreTransactionDetailses
                                           where !c.Username.Equals(DBNull.Value) &&
                                                 c.Username.ToLower().Equals(User.Identity.Name.ToLower()) &&
                                                 d.TransactionStatus == TransactionStatus.Pending
                                           select d).ToList();
                    if (pretransactions.Count > 0)
                        transactionID = pretransactions[0].TransactionId;
                    if (string.IsNullOrEmpty(transactionID))
                    {


                        string productInfo = string.Empty;
                        double totalamount = 0.0;
                        foreach (var mycart in mycarts)
                        {
                            totalamount += mycart.Product.Price*mycart.Quantity;
                            productInfo += "<p><b>Product name        :</b> " + mycart.Product.Name + " <br></p>";
                            productInfo += "<p><b>        Category    :</b> " + mycart.Product.Category + " <br></p>";
                            productInfo += "<p><b>        Price       :</b> " + mycart.Product.Price + " <br></p>";
                            productInfo += "<p><b>Quantity            :</b> " + mycart.Quantity + " <br></p>";
                            productInfo += "<p><br><br></p>";
                        }

                        var sendMailViaGmail = new SendMailViaGmail();
                        sendMailViaGmail.PGmailAccount = Utilities.Gmailid;
                        sendMailViaGmail.PGmailPassword = Utilities.Gmailpassword;
                        // mail sending here to the admin to say that somebody tried to pay online
                        try
                        {
                            var user = users[0];

                            var stringBuilder = new StringBuilder();
                            stringBuilder.Append(
                                @"<table border='0' cellpadding='0' cellspacing='0' width='98%'><tbody><tr><td style='padding: 10px 15px 40px; font-family: Helvetica,Arial,sans-serif; font-size: 16px; line-height: 1.3em; text-align: left;' valign='top'><h1 style='font-family: Helvetica,Arial,sans-serif; color: rgb(34, 34, 34); font-size: 28px; line-height: normal; letter-spacing: -1px;'>Online transaction initiated.</h1><p>Hi <b>Srigreensentei</b>,</p><p>Here is the transaction details.</p>");
                            stringBuilder.Append("<p><b>Transaction ID      :</b> " + transactionID + " <br></p>");
                            stringBuilder.Append("<p><b>Username            :</b> " + user.Username + " <br></p>");
                            stringBuilder.Append("<p><b>Email               :</b> " + user.Email + " <br></p>");
                            stringBuilder.Append("<p><b>Mobile              :</b> " + user.Mobileno + " <br></p>");
                            stringBuilder.Append(productInfo);
                            stringBuilder.Append("<p><b>Total amount        :</b> " + totalamount + " <br></p>");
                            stringBuilder.Append(
                                @"<hr) style='margin-top: 30px; border-right: medium none; border-width: 1px medium medium; border-style: solid none none; border-color: rgb(204, 204, 204) -moz-use-text-color -moz-use-text-color;'><p style='font-size: 13px; line-height: 1.3em;'></p></td></tr></tbody></table>");
                            sendMailViaGmail.SendMail(Utilities.Srigreenmails,
                                                      "A new user registered with us.", stringBuilder.ToString(),
                                                      new List<string>());

                        }
                        catch (Exception)
                        {
                        }

                        var toccavenue = new ToCcAvenue();
                        toccavenue.ConfigureEmail(Utilities.Gmailid, Utilities.Gmailpassword, Utilities.Srigreenmails);
                        ViewData["PostBackData"] = toccavenue.ProcessCcAvenuePostBack(2, transactionID
                                                                                      ,
                                                                                      3, 2, 4,
                                                                                      new Random().NextDouble().ToString
                                                                                          (),
                                                                                      new Random().NextDouble().ToString
                                                                                          (),
                                                                                      "Srigreen product selling online",
                                                                                      totalamount, totalamount,
                                                                                      User.Identity.Name,
                                                                                      users[0].DeliveryAddress,
                                                                                      users[0].DeliveryCity,
                                                                                      users[0].DeliveryState,
                                                                                      users[0].DeliveryPin,
                                                                                      users[0].DeliveryCountry,
                                                                                      users[0].Mobileno, users[0].Email,
                                                                                      "M_domain2h_14761",
                                                                                      "v5n8ti6siksqh5r4hi",
                                                                                      "http://srigreensentei.com/ShoppingCart/Payment");
                        return View();
                    }
                }
                return RedirectToAction("PaymentOptions");
            }
            return Redirect("/Account/LogOn");
        }

        public ActionResult Payment()
        {
            var fromccavenue = new FromCcAvenue();
            fromccavenue.ConfigureEmail(Utilities.Gmailid, Utilities.Gmailpassword, new List<string>() { "santhoshonet@gmail.com" });
            var result = fromccavenue.ProcessCcAvenueData(Request.Form, "v5n8ti6siksqh5r4hi");
            ViewData["result"] = result;
            if(result == FromCcAvenue.TransactionStatus.Success)
            {
                string transactionID = Request.Form["Order_Id"];
                  var scope = ObjectScopeProvider1.GetNewObjectScope();
                var pretransactions = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                           from d in c.PreTransactionDetailses
                                           where !c.Username.Equals(DBNull.Value) &&
                                                 c.Username.ToLower().Equals(User.Identity.Name.ToLower()) &&
                                                 d.TransactionStatus == TransactionStatus.Pending && d.TransactionId.Equals(transactionID)
                                           select d).ToList();
                    if (pretransactions.Count > 0)
                    {
                        scope.Transaction.Begin();
                        pretransactions[0].TransactionStatus = TransactionStatus.Completed;
                        scope.Transaction.Commit();
                    }
            }
            return View();
        }

        private void CalculateCartAmount()
        {
            double totalamount = 0.0;
            string transactionID = "";
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                var pretransactions = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                       from d in c.PreTransactionDetailses
                                       where !c.Username.Equals(DBNull.Value) &&
                                   c.Username.ToLower().Equals(User.Identity.Name.ToLower()) &&
                                   d.TransactionStatus == TransactionStatus.Pending
                                       select d).ToList();
                if (pretransactions.Count > 0)
                {
                    transactionID = pretransactions[0].TransactionId;
                    var mycarts = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                   from d in c.PreTransactionDetailses
                                   from e in d.MyCarts
                                   where !c.Username.Equals(DBNull.Value) &&
                                         c.Username.ToLower().Equals(User.Identity.Name.ToLower()) &&
                                         d.TransactionStatus == TransactionStatus.Pending && e.Product != null
                                   select e).ToList();
                    totalamount = mycarts.Sum(mycart => mycart.Product.Price * mycart.Quantity);
                }
            }
            ViewData["TotalAmount"] = totalamount;
            ViewData["TransactionID"] = transactionID;
        }
    }
}