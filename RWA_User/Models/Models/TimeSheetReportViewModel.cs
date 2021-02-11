using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RWA_User.Models
{
    public class TimeSheetReportViewModel
    {
        [Required]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public int EmployeeID { get; set; }
        public List<TimeSheet> TSList { get; set; }
    }
}