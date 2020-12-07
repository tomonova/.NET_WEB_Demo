using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWA_User.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TeamStatus TeamStatus { get; set; }
        public DateTime FoundingDate { get; set; }
        public int TeamLead { get; set; }
    }
}
