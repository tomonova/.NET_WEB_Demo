using RWA_User.App_Code;
using RWA_User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RWA_User.Controllers
{
    public class TimeSheetUpdateController : BaseController
    {
        // GET: TimeSeetUpdate
        public ActionResult UpdateTimeSheet(List<TimeSheetRowViewModel> tsRows)
        {
            DateTime timeSheetDate = (DateTime)TempData.Peek("TimeSheetDate");
            TempData.Remove("HoursCheck");
            int workHours = 0;
            int overTimeHours = 0;
            foreach (var item in tsRows)
            {
                workHours += item.WorkHours;
                overTimeHours += item.OverTimeHours;
            }
            if (!CheckHours(workHours, overTimeHours, timeSheetDate))
            {
                return View("~/Views/TimeSheet/Edit.cshtml", tsRows);
            };
            Repo.UpdateTimeSheetRows(tsRows);
            TempData["HoursCheck"] = $@"Timesheet for date {timeSheetDate:dd-MM-yyyy} succesfully changed";
            ViewBag.UserName = Repo.GetEmployeeName(int.Parse(Session["IDEmployee"].ToString()));
            return View("~/Views/Home/Index.cshtml");
        }

        private bool CheckHours(int workHours, int overTimeHours,DateTime timeSheetDate)
        {
            
            DayOfWeek day = timeSheetDate.DayOfWeek;
            bool weekday = true;
            EmployeeType employeeType = (EmployeeType)int.Parse(Session["EmployeeType"].ToString());
            if (day == DayOfWeek.Saturday || day == DayOfWeek.Sunday)
            {
                weekday = false;
            }
            if (weekday == false && workHours != 0)
            {
                TempData["HoursCheck"] = $"On weekend only overtime hours can be used";
                return false;
            }
            switch (employeeType)
            {
                case EmployeeType.Permanent:
                    {
                        if (workHours != 8 || overTimeHours > 4)
                        {
                            TempData["HoursCheck"] = $"Permanent employee needs to have exactley 8 working hours and not more then 4 overtime hours";
                            return false;
                        }
                        else return true;
                    }
                case EmployeeType.Temporary:
                    {
                        if ((workHours + overTimeHours) > 12)
                        {
                            TempData["HoursCheck"] = $"Temporary employee can have maximum 12 hours a day";
                            return false;
                        }
                        else return true;
                    }
                case EmployeeType.Student:
                    {
                        if ((workHours + overTimeHours) > 12)
                        {
                            TempData["HoursCheck"] = $"Student employee can have maximum 12 hours a day";
                            return false;
                        }
                        else return true;
                    }
                default:
                    return true;
            }
        }
    }
}