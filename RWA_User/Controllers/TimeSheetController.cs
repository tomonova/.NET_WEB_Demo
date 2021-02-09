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
        [HttpPost]
        public ActionResult TimeSheetSearch(TimeSheet ts)
        {
            TempData.Remove("TimeSheetDate");
            TempData["TimeSheetDate"] = ts.TimeSheetDate;

            if (!Repo.CheckTimeSheet(ts))
            {
                TempData["CheckTimeSheet"] = $"In selected date you weren't part of any project!";
                return View();
            }
            else if(Repo.CheckTimeSheet(ts)&&Repo.GetTimeSheetID(ts)!=0)
            {
                ts.ID = Repo.GetTimeSheetID(ts);
                ts = Repo.GetTimeSheet(ts.ID);
                Session["TSDate"] = ts.TimeSheetDate.ToString("yyyy-MM-dd");
                ViewBag.TimeSheet = ts;
                var tsrvmLista = new List<TimeSheetRowViewModel>();
                CreateVM(ts, tsrvmLista);
                return View("Edit", tsrvmLista);
            }
            else
            {
                ts = Repo.CreateTimeSheet(ts);
                Session["TSDate"] = ts.TimeSheetDate.ToString("yyyy-MM-dd");
                ViewBag.TimeSheet = ts;
                var tsrvmLista = new List<TimeSheetRowViewModel>();
                CreateVM(ts, tsrvmLista);
                return View("Edit", tsrvmLista);
            }
        }

        private static void CreateVM(TimeSheet ts, List<TimeSheetRowViewModel> tsrvmLista)
        {
            foreach (var item in ts.TSRows)
            {
                TimeSheetRowViewModel tsrvm = new TimeSheetRowViewModel
                {
                    ID = item.ID,
                    ProjectID = item.ProjectID,
                    ProjectName = item.ProjectName,
                    OverTimeHours = item.OverTimeHours,
                    WorkHours = item.WorkHours
                };
                tsrvmLista.Add(tsrvm);
            }
        }
        [HttpPost]
        public ActionResult Edit(List<TimeSheetRowViewModel> model)
        {
            return View();
        }
    }
}