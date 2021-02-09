using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RWA_User.Models
{
    public class TimeSheetRowViewModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public int ProjectID { get; set; }
        [Required]
        public string ProjectName { get; set; }
        [Required]
        [Range(0, 8)]
        public int WorkHours { get; set; }
        [Required]
        [Range(0, 4)]
        public int OverTimeHours { get; set; }
    }
}