using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RWA_Admin.App_Code
{
    public class Repo
    {
        private static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public static List<Employee> GetEmployees()
        {
            List<Employee> employeeList = new List<Employee>();
            DataTable tblEmployees = SqlHelper.ExecuteDataset(cs, "GetEmployees").Tables[0];
            foreach (DataRow row in tblEmployees.Rows)
            {
                 Employee employee = new Employee
                {
                    Id = (int)row["IDEmployee"],
                    Name = row["Name"].ToString(),
                    Surname = row["Surname"].ToString(),
                    FullName = $"{row["Name"]} {row["Surname"]}",
                    EmployeePosition = (EmployeePosition)(int)row["EmployeePosition"],
                    EmploymentDate = (DateTime)row["EmploymentDate"],
                    EmployeeType = (EmployeeType)((int)row["EmployeeType"]),
                    EmployeeStatus = (EmployeeStatus)((int)row["EmployeeStatus"])
                };
                employeeList.Add(employee);
            }
            return employeeList.OrderBy(e => e.Surname).ToList();
        }

        public static List<Team> GetTeams()
        {
            List<Team> TeamList = new List<Team>();
            DataTable tblTeams = SqlHelper.ExecuteDataset(cs, "GetTEams").Tables[0];
            foreach (DataRow row in tblTeams.Rows)
            {
                Team team = new Team
                {
                    Id = (int)row["IDTeam"],
                    Name = row["Name"].ToString(),
                    TeamStatus = (TeamStatus)(int)row["TeamStatus"],
                    FoundingDate = (DateTime)row["FoundingDate"]
                };
                TeamList.Add(team);
            }
            return TeamList;
        }

        internal static void DeleteEmployee(int employeeID)
        {
            SqlParameter param = new SqlParameter("@IDEmployee", SqlDbType.Int);
            param.Value = employeeID;
            SqlHelper.ExecuteNonQuery(cs, CommandType.StoredProcedure, "DeleteEmployee", param);
        }
        internal static bool UpdateEmployee(Employee employee)
        {
            SqlParameter[] Param = new SqlParameter[7];
            Param[0] = new SqlParameter("@IDEmployee", SqlDbType.Int);
            Param[0].Value = employee.Id;
            Param[1] = new SqlParameter("@Name", SqlDbType.NVarChar);
            Param[1].Value = employee.Name;
            Param[2] = new SqlParameter("@Surname", SqlDbType.NVarChar);
            Param[2].Value = employee.Surname;
            Param[3] = new SqlParameter("@EmploymentDate", SqlDbType.DateTime);
            Param[3].Value = employee.EmploymentDate.ToString("yyyy-MM-dd");
            Param[4] = new SqlParameter("@EmployeeType", SqlDbType.Int);
            Param[4].Value = employee.EmployeeType;
            Param[5] = new SqlParameter("@EmployeePosition", SqlDbType.Int);
            Param[5].Value = employee.EmployeePosition;
            Param[6] = new SqlParameter("@TeamID", SqlDbType.Int);
            Param[6].Value = employee.AssignedTeam;
            SqlHelper.ExecuteNonQuery(cs, CommandType.StoredProcedure, "UpdateEmployee", Param);
            string response = Param[2].Value.ToString();
            return response == "1" ? true : false;
        }

        internal static bool CheckCredentialsAdmin(string userName, string userPass)
        {
            SqlParameter[] Param = new SqlParameter[3];
            Param[0] = new SqlParameter("@userName", SqlDbType.NVarChar);
            Param[0].Value = userName;
            Param[1] = new SqlParameter("@userPass", SqlDbType.NVarChar);
            Param[1].Value = userPass;
            Param[2] = new SqlParameter("@checkOutput", SqlDbType.Int);
            Param[2].Direction = ParameterDirection.Output;
            int credentialsCheck = SqlHelper.ExecuteNonQuery(cs,CommandType.StoredProcedure, "CheckCredentialsAdmin", Param);
            string response = Param[2].Value.ToString();
            
            return  response == "1" ? true : false;
        }
        public static Employee GetEmployee(int employeeID)
        {
            DataTable tbl = SqlHelper.ExecuteDataset(cs, "GetEmployee", employeeID).Tables[0];
            if (tbl.Rows.Count == 0) return null;

            DataRow row = tbl.Rows[0];

            return new Employee
            {
                Id = (int)row["IDEmployee"],
                Name = row["Name"].ToString(),
                Surname = row["Surname"].ToString(),
                FullName = $"{row["Name"]} {row["Surname"]}",
                EmployeePosition = (EmployeePosition)(int)row["EmployeePosition"],
                EmploymentDate = (DateTime)row["EmploymentDate"],
                EmployeeType = (EmployeeType)((int)row["EmployeeType"]),
                EmployeeStatus = (EmployeeStatus)((int)row["EmployeeStatus"])
            };
        }
        public static Employee GetEmployeeAdmin(int employeeID)
        {
            DataTable tbl = SqlHelper.ExecuteDataset(cs, "GetEmployeeAdmin", employeeID).Tables[0];
            if (tbl.Rows.Count == 0) return null;

            DataRow row = tbl.Rows[0];

            return new Employee
            {
                Id = (int)row["IDEmployee"],
                Name = row["Name"].ToString(),
                Surname = row["Surname"].ToString(),
                FullName = $"{row["Name"]} {row["Surname"]}",
                EmployeePosition = (EmployeePosition)(int)row["EmployeePosition"],
                EmploymentDate = (DateTime)row["EmploymentDate"],
                EmployeeType = (EmployeeType)((int)row["EmployeeType"]),
                EmployeeStatus = (EmployeeStatus)((int)row["EmployeeStatus"]),
                AssignedTeam = (int)row["TeamId"]
            };
        }
    }
}