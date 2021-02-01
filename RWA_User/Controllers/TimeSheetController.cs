using RWA_User.App_Code;
using RWA_User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RWA_User.Controllers
{
    public class TimeSheetController : BaseController
    {
        // GET: TimeSheet
        public ActionResult TimeSheetSearch()
        {
            TimeSheet ts = new TimeSheet { TimeSheetDate = DateTime.Now , EmployeeID= int.Parse(Session["IDEmployee"].ToString()) };
            return View(ts);
        }
        public ActionResult Edit(TimeSheet ts)
        {
            return View();
        }
        [HttpPost]
        public ActionResult TimeSheetSearch(TimeSheet ts)
        {
            //ts = (TimeSheet)ViewBag.TimeSheet;
            if (!Repo.CheckTimeSheet(ts))
            {
                TempData["CheckTimeSheet"] = $"In selected date you weren't part of any project!";
                return View();
            }
            else if(Repo.CheckTimeSheet(ts)&&Repo.GetTimeSheetID(ts)!=0)
            {
                ts.ID = Repo.GetTimeSheetID(ts);
                ts = Repo.GetTimeSheet(ts.ID);
                return View("Edit",ts);
            }
            else
            {
                ts = Repo.CreateTimeSheet(ts);
                return View("Edit",ts);
            }
        }
    }
}