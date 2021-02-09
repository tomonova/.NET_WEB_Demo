using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RWA_User.Models
{
    public class TimeSheet
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        [DisplayName("Time Sheet Date")]
        [DataType(DataType.DateTime),DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime TimeSheetDate { get; set; }
        [Required]
        [Range(0,8)]
        public int WorkHoursSum { get; set; }
        [Required]
        [Range(0,8)]
        public int OverTimeHoursSum { get; set; }
        [Required]
        public TimesheetStatus TimesheetStatus { get; set; }
        public List<TimeSheetRow> TSRows { get; set; }
        public string Comment { get; set; }
    }
}