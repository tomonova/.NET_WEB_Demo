using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWA_User.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName { get; set; }
        public DateTime EmploymentDate { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public EmployeePosition EmployeePosition { get; set; }
        public EmployeeStatus EmployeeStatus { get; set; }
        public int AssignedTeam { get; set; }
    }
}

