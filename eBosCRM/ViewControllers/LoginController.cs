using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Bogus;
using eBosCRM.Controllers;
using eBosCRM.Models;

namespace eBosCRM.ViewControllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        //POST : Authentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ApplicationUser user, string ReturnUrl)
        {
            ApplicationUser matchedUserByName;

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            GetMatchedUserByName(user, out matchedUserByName);

            if (matchedUserByName == null)
            {
                //Invalid Credentials
                ViewBag.errorMessage = "Invalid Credentials! Username or Password incorrect.";
                return View();
            }
            else
            {
                if (user.Password.Trim().Equals(matchedUserByName.Password))
                {
                    //Login Successfull
                    FormsAuthentication.SetAuthCookie(matchedUserByName.ApplicationUserID.ToString(), false);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //Invalid Credentials
                    ViewBag.errorMessage = "Invalid Credentials! Username or Password incorrect.";

                    return View();
                }
            }
        }

        private static void GetMatchedUserByName(ApplicationUser user, out ApplicationUser matchedUserByName)
        {
            var clientService = new ApplicationUsersController();

            var allRegisteredUsers = clientService.GetApplicationUsers().ToList();

            matchedUserByName = allRegisteredUsers.FirstOrDefault(u =>
                u.Username.Trim().ToLower().Equals(user.Username.ToLower()));
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}