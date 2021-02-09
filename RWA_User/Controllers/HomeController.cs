using RWA_User.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RWA_User.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.UserName = Repo.GetEmployeeName(int.Parse(Session["IDEmployee"].ToString()));
            return View();
        }
    }
}