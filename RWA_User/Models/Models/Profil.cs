using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RWA_User.Models
{
    public class Profil
    {
        public Employee Employee { get; set; }
        public User User { get; set; }
        public Team Team{ get; set; }

        public String ReturnEmploymentDateForView
        {
            get
            {
                return this.Employee.EmploymentDate.ToString("D");
            }
        }
        public string Password { get; set; }
        [Compare("Password",ErrorMessage ="Passwordi moraju biti isti!!!")]
        public string ConfirmPassword { get; set; }
    }
}