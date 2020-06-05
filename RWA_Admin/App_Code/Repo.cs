﻿using Microsoft.ApplicationBlocks.Data;
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
        //public static IEnumerable<Employee> GetEmployees()
        //{
        //    DataTable tblEmployees = SqlHelper.ExecuteDataset(cs, "GetEmployees").Tables[0];
        //    foreach (DataRow row in tblEmployees.Rows)
        //    {
        //        yield return new Employee
        //        {
        //            Id = (int)row["IDEmployee"],
        //            Name = row["Name"].ToString(),
        //            Surname = row["Surname"].ToString(),
        //            FullName = $"{row["Name"]} {row["Surname"]}",
        //            EmployeePosition = (EmployeePosition)(int)row["EmployeePosition"],
        //            EmploymentDate = (DateTime)row["EmploymentDate"],
        //            EmployeeType = (EmployeeType)((int)row["EmployeeType"]),
        //            EmployeeStatus = (EmployeeStatus)((int)row["EmployeeStatus"])
        //        };
        //    }
        //}

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

        internal static void DeleteEmployee(int employeeID)
        {
            SqlParameter param = new SqlParameter("@IDEmployee", SqlDbType.Int);
            param.Value = employeeID;
            SqlHelper.ExecuteNonQuery(cs, CommandType.StoredProcedure, "DeleteEmployee", param);
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
                AssignedTeam = row["TeamName"].ToString()
            };
        }
    }
}