using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using CCAvenueIntegrationDL;
using SriGreenShoppingCart.Models;

namespace SriGreenShoppingCart.Controllers
{
    [HandleError]
    public class AccountController : Controller
    {
        public IFormsAuthenticationService FormsService { get; set; }

        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

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
                        return Redirect(returnUrl);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsService.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus = MembershipService.CreateUser(model.UserName, model.Password, model.Email);
                if (createStatus == MembershipCreateStatus.Success)
                {
                    var scope = ObjectScopeProvider1.GetNewObjectScope();
                    int count = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                 where !c.Username.Equals(DBNull.Value) && c.Username.ToLower().Equals(model.UserName.ToLower())
                                 select c).Count();
                    if (count == 0)
                    {
                        scope.Transaction.Begin();
                        var user = new User();
                        user.BillingAddress = model.BillingAddress;
                        user.BillingCity = model.BillingCity;
                        user.BillingCountry = model.BillingCountry;
                        user.BillingFaxno = model.BillingFaxno;
                        user.BillingPin = model.BillingPin;
                        user.BillingState = model.BillingState;

                        if (Request.Form["yes"] == "true" || Request.Form["yes"] == "on")
                        {
                            user.DeliveryAddress = model.BillingState;
                            user.DeliveryCity = model.BillingCity;
                            user.DeliveryCountry = model.BillingCountry;
                            user.DeliveryPin = model.BillingPin;
                            user.DeliveryState = model.BillingState;
                        }
                        else
                        {
                            user.DeliveryAddress = model.DeliveryState;
                            user.DeliveryCity = model.DeliveryCity;
                            user.DeliveryCountry = model.DeliveryCountry;
                            user.DeliveryPin = model.DeliveryPin;
                            user.DeliveryState = model.DeliveryState;
                        }

                        user.Email = model.Email;
                        user.IsheAdmin = false;
                        user.Mobileno = model.Mobile;
                        user.Username = model.UserName;

                        scope.Add(user);
                        scope.Transaction.Commit();
                        FormsService.SignIn(model.UserName, false /* createPersistentCookie */);

                        var sendMailViaGmail = new SendMailViaGmail();
                        sendMailViaGmail.PGmailAccount = Utilities.Gmailid;
                        sendMailViaGmail.PGmailPassword = Utilities.Gmailpassword;

                        // mail sending here to the admin
                        try
                        {
                            var stringBuilder = new StringBuilder();
                            stringBuilder.Append(@"<table border='0' cellpadding='0' cellspacing='0' width='98%'><tbody><tr><td style='padding: 10px 15px 40px; font-family: Helvetica,Arial,sans-serif; font-size: 16px; line-height: 1.3em; text-align: left;' valign='top'><h1 style='font-family: Helvetica,Arial,sans-serif; color: rgb(34, 34, 34); font-size: 28px; line-height: normal; letter-spacing: -1px;'>A new user registered!</h1><p>Hi <b>Srigreensentei</b>,</p><p>Here is the new user registered details.</p>");

                            stringBuilder.Append("<p><b>Username            :</b> " + user.Username + " <br></p>");
                            stringBuilder.Append("<p><b>Email               :</b> " + user.Email + " <br></p>");
                            stringBuilder.Append("<p><b>Mobile              :</b> " + user.Mobileno + " <br></p>");

                            stringBuilder.Append("<p><b>Billing Address     :</b> " + user.BillingAddress + " <br></p>");
                            stringBuilder.Append("<p><b>        City        :</b> " + user.BillingCity + " <br></p>");
                            stringBuilder.Append("<p><b>        State       :</b> " + user.BillingState + " <br></p>");
                            stringBuilder.Append("<p><b>        Country     :</b> " + user.BillingCountry + " <br></p>");
                            stringBuilder.Append("<p><b>        Pin Code    :</b> " + user.BillingPin + " <br></p>");
                            stringBuilder.Append("<p><b>        Fax No      :</b> " + user.BillingFaxno + " <br></p>");

                            stringBuilder.Append("<p><b>Delivery Address    :</b> " + user.BillingAddress + " <br></p>");
                            stringBuilder.Append("<p><b>         City       :</b> " + user.BillingCity + " <br></p>");
                            stringBuilder.Append("<p><b>         State      :</b> " + user.BillingState + " <br></p>");
                            stringBuilder.Append("<p><b>         Country    :</b> " + user.BillingCountry + " <br></p>");
                            stringBuilder.Append("<p><b>         Code       :</b> " + user.BillingPin + " <br></p>");
                            stringBuilder.Append("<p><b>         Fax No     :</b> " + user.BillingFaxno + " <br></p>");

                            stringBuilder.Append(@"<hr) style='margin-top: 30px; border-right: medium none; border-width: 1px medium medium; border-style: solid none none; border-color: rgb(204, 204, 204) -moz-use-text-color -moz-use-text-color;'><p style='font-size: 13px; line-height: 1.3em;'></p></td></tr></tbody></table>");
                            sendMailViaGmail.SendMail(Utilities.Srigreenmails,
                                                      "A new user registered with us.", stringBuilder.ToString(),
                                                      new List<string>());
                        }
                        catch (Exception)
                        {
                        }

                        // mail sending here to the user
                        try
                        {
                            var stringBuilder = new StringBuilder();
                            stringBuilder.Append(@"<table border='0' cellpadding='0' cellspacing='0' width='98%'><tbody><tr><td style='padding: 10px 15px 40px; font-family: Helvetica,Arial,sans-serif; font-size: 16px; line-height: 1.3em; text-align: left;' valign='top'><h1 style='font-family: Helvetica,Arial,sans-serif; color: rgb(34, 34, 34); font-size: 28px; line-height: normal; letter-spacing: -1px;'>You have registered with us!</h1><p>Hi <b>" + user.Username + "</b>,</p><p>Your account has been created with us. Here is your account details.</p>");

                            stringBuilder.Append("<p><b>Username            :</b> " + user.Username + " <br></p>");
                            stringBuilder.Append("<p><b>Email               :</b> " + user.Email + " <br></p>");
                            stringBuilder.Append("<p><b>Mobile              :</b> " + user.Mobileno + " <br></p>");

                            stringBuilder.Append("<p><b>Billing Address     :</b> " + user.BillingAddress + " <br></p>");
                            stringBuilder.Append("<p><b>        City        :</b> " + user.BillingCity + " <br></p>");
                            stringBuilder.Append("<p><b>        State       :</b> " + user.BillingState + " <br></p>");
                            stringBuilder.Append("<p><b>        Country     :</b> " + user.BillingCountry + " <br></p>");
                            stringBuilder.Append("<p><b>        Pin Code    :</b> " + user.BillingPin + " <br></p>");
                            stringBuilder.Append("<p><b>        Fax No      :</b> " + user.BillingFaxno + " <br></p>");

                            stringBuilder.Append("<p><b>Delivery Address    :</b> " + user.BillingAddress + " <br></p>");
                            stringBuilder.Append("<p><b>         City       :</b> " + user.BillingCity + " <br></p>");
                            stringBuilder.Append("<p><b>         State      :</b> " + user.BillingState + " <br></p>");
                            stringBuilder.Append("<p><b>         Country    :</b> " + user.BillingCountry + " <br></p>");
                            stringBuilder.Append("<p><b>         Code       :</b> " + user.BillingPin + " <br></p>");
                            stringBuilder.Append("<p><b>         Fax No     :</b> " + user.BillingFaxno + " <br></p>");

                            stringBuilder.Append(@"<hr) style='margin-top: 30px; border-right: medium none; border-width: 1px medium medium; border-style: solid none none; border-color: rgb(204, 204, 204) -moz-use-text-color -moz-use-text-color;'><p style='font-size: 13px; line-height: 1.3em;'></p></td></tr></tbody></table>");
                            sendMailViaGmail.SendMail(new List<string>() { user.Email },
                                                      "Your account has been created at Srigreensentei", stringBuilder.ToString(),
                                                      new List<string>());
                        }
                        catch (Exception)
                        {
                        }

                        return RedirectToAction("Index", "Home");
                    }

                    ModelState.AddModelError("UserName", "Username already exists.");
                }
                else
                {
                    ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View(model);
        }

        public ActionResult MyProfile()
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
                List<User> users = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                    where !c.Username.Equals(DBNull.Value) && c.Username.ToLower().Equals(User.Identity.Name.ToLower())
                                    select c).ToList();
                var registerModel = new RegisterModel();
                if (users.Count > 0)
                {
                    registerModel.BillingAddress = users[0].BillingAddress;
                    registerModel.BillingCity = users[0].BillingCity;
                    registerModel.BillingCountry = users[0].BillingCountry;
                    registerModel.BillingFaxno = users[0].BillingFaxno;
                    registerModel.BillingPin = users[0].BillingPin;
                    registerModel.BillingState = users[0].BillingState;
                    registerModel.DeliveryAddress = users[0].DeliveryAddress;
                    registerModel.DeliveryCity = users[0].DeliveryCity;
                    registerModel.DeliveryCountry = users[0].DeliveryCountry;
                    registerModel.DeliveryPin = users[0].DeliveryPin;
                    registerModel.DeliveryState = users[0].DeliveryState;
                    registerModel.Email = users[0].Email;
                    registerModel.Mobile = users[0].Mobileno;
                    registerModel.Password = "123456789";
                    registerModel.ConfirmPassword = "123456789";
                    registerModel.UserName = users[0].Username;
                }
                return View(registerModel);
            }
            return RedirectToAction("LogOn");
        }

        [HttpPost]
        public ActionResult MyProfile(RegisterModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var scope = ObjectScopeProvider1.GetNewObjectScope();
                    var users = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                 where
                                     !c.Username.Equals(DBNull.Value) &&
                                     c.Username.ToLower().Equals(model.UserName.ToLower())
                                 select c).ToList();
                    if (users.Count > 0)
                    {
                        scope.Transaction.Begin();
                        var user = users[0];
                        user.BillingAddress = model.BillingAddress;
                        user.BillingCity = model.BillingCity;
                        user.BillingCountry = model.BillingCountry;
                        user.BillingFaxno = model.BillingFaxno;
                        user.BillingPin = model.BillingPin;
                        user.BillingState = model.BillingState;
                        if (Request.Form["yes"] == "true" || Request.Form["yes"] == "on")
                        {
                            user.DeliveryAddress = model.BillingState;
                            user.DeliveryCity = model.BillingCity;
                            user.DeliveryCountry = model.BillingCountry;
                            user.DeliveryPin = model.BillingPin;
                            user.DeliveryState = model.BillingState;
                        }
                        else
                        {
                            user.DeliveryAddress = model.DeliveryState;
                            user.DeliveryCity = model.DeliveryCity;
                            user.DeliveryCountry = model.DeliveryCountry;
                            user.DeliveryPin = model.DeliveryPin;
                            user.DeliveryState = model.DeliveryState;
                        }
                        user.Email = model.Email;
                        user.Mobileno = model.Mobile;
                        scope.Add(user);
                        scope.Transaction.Commit();
                        return RedirectToAction("Index", "Home");
                    }
                }
                // If we got this far, something failed, redisplay form
                ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
                return View(model);
            }
            return RedirectToAction("LogOn");
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View(model);
        }

        [Authorize]
        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }
    }
}