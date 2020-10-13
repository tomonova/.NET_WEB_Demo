using RWA_Admin.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWA_Admin.App_Code
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OIB { get; set; }
        public string Address { get; set; }
        public ClientStatus ClientStatus { get; set; }
        public override string ToString() => $"{Name}";
    }
}
