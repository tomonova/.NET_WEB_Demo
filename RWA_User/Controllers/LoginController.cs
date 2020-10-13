using RWA_User.App_Code;
using RWA_User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;

namespace RWA_User.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Login/Login");
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (Repo.CheckCredentials(user.UserName,user.Password) && ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                return Redirect("/Home/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Incorrect credentials");
                return View(user); 
            }
        }
    }
}