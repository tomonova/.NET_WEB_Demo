using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWA_User.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="UserName mandatory")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password mandatory")]
        //[StringLength (20,MinimumLength =5,ErrorMessage ="Password must be longer than 5 char")]
        public string Password { get; set; }
        public Employee Employee { get; set; }
    }
}
