using RWA_User.App_Code;
using RWA_User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RWA_User.Controllers
{
    public class ReportController : BaseController
    {
        [Authorize]
        public ActionResult ReportSearch()
        {
            TimeSheetReportViewModel tsrvm = new TimeSheetReportViewModel
            {
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now,
                EmployeeID = int.Parse(Session["IDEmployee"].ToString())
            };
            return View(tsrvm);
        }
        [Authorize]
        [HttpPost]
        public ActionResult ReportSearch(TimeSheetReportViewModel tsrvm)
        {
            tsrvm.EmployeeID = int.Parse(Session["IDEmployee"].ToString());
            List<TimeSheet> TS = Repo.GetTimeSheetReports(tsrvm);
            return View("ReportShow", TS);
        }
    }
}