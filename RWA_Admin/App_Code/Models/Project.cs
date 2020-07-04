using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWA_Admin.App_Code
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Client Client { get; set; }
        public DateTime CreationDate { get; set; }
        public Employee ProjectLead { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
    }
}
    