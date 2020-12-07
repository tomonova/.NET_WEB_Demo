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
        private string _password;
        public int Id { get; set; }
        [Required (ErrorMessage ="UserName mandatory")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password mandatory")]
        //[StringLength (20,MinimumLength =5,ErrorMessage ="Password must be longer than 5 char")]
        public string Password 
        {
            get
            {
                return _password;
            }
            set
            {
                System.Security.Cryptography.SHA1CryptoServiceProvider hash = new System.Security.Cryptography.SHA1CryptoServiceProvider();
                byte[] bs = System.Text.Encoding.UTF8.GetBytes(value);
                bs = hash.ComputeHash(bs);
                System.Text.StringBuilder s = new System.Text.StringBuilder();
                foreach (byte b in bs)
                {
                    s.Append(b.ToString("x2").ToLower());
                }
                _password = s.ToString();
            }
        }
        public Employee Employee { get; set; }
    }
}
