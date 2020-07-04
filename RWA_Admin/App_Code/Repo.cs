﻿using Microsoft.ApplicationBlocks.Data;
using RWA_Admin.App_Code.Enums;
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
        internal static bool CheckCredentialsAdmin(string userName, string userPass)
        {
            SqlParameter[] Param = new SqlParameter[3];
            Param[0] = new SqlParameter("@userName", SqlDbType.NVarChar);
            Param[0].Value = userName;
            Param[1] = new SqlParameter("@userPass", SqlDbType.NVarChar);
            Param[1].Value = userPass;
            Param[2] = new SqlParameter("@checkOutput", SqlDbType.Int);
            Param[2].Direction = ParameterDirection.Output;
            int credentialsCheck = SqlHelper.ExecuteNonQuery(cs, CommandType.StoredProcedure, "CheckCredentialsAdmin", Param);
            string response = Param[2].Value.ToString();

            return response == "1" ? true : false;
        }

        //-------------------EMPLOYEES----------------
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
        internal static int InsertEmployee(Employee employee)
        {
            SqlParameter[] Param = new SqlParameter[6];
            Param[0] = new SqlParameter("@Name", SqlDbType.NVarChar);
            Param[0].Value = employee.Name;
            Param[1] = new SqlParameter("@Surname", SqlDbType.NVarChar);
            Param[1].Value = employee.Surname;
            Param[2] = new SqlParameter("@EmploymentDate", SqlDbType.DateTime);
            Param[2].Value = employee.EmploymentDate.ToString("yyyy-MM-dd");
            Param[3] = new SqlParameter("@EmployeeType", SqlDbType.Int);
            Param[3].Value = employee.EmployeeType;
            Param[4] = new SqlParameter("@EmployeePosition", SqlDbType.Int);
            Param[4].Value = employee.EmployeePosition;
            Param[5] = new SqlParameter("@TeamID", SqlDbType.Int);
            Param[5].Value = employee.AssignedTeam;
            return SqlHelper.ExecuteNonQuery(cs, CommandType.StoredProcedure, "InsertEmployee", Param);
        }
        internal static void DeleteEmployee(int employeeID)
        {
            SqlParameter param = new SqlParameter("@IDEmployee", SqlDbType.Int);
            param.Value = employeeID;
            SqlHelper.ExecuteNonQuery(cs, CommandType.StoredProcedure, "DeleteEmployee", param);
        }
        internal static int UpdateEmployee(Employee employee)
        {
            SqlParameter[] Param = new SqlParameter[7];
            Param[0] = new SqlParameter("@employeeID", SqlDbType.Int);
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
           return SqlHelper.ExecuteNonQuery(cs, CommandType.StoredProcedure, "UpdateEmployee", Param);
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
        //----------------------TEAMS-------------------------
        public static List<Team> GetTeams()
        {
            List<Team> TeamList = new List<Team>();
            DataTable tblTeams = SqlHelper.ExecuteDataset(cs, "GetTeams").Tables[0];
            foreach (DataRow row in tblTeams.Rows)
            {
                Team team = new Team
                {
                    Id = (int)row["IDTeam"],
                    Name = row["Name"].ToString(),
                    TeamStatus = (TeamStatus)(int)row["TeamStatus"],
                    FoundingDate = (DateTime)row["FoundingDate"]
                };

                if (team.TeamStatus == TeamStatus.Active && team.Name!="NONE")
                {
                    TeamList.Add(team);
                }
            }
            return TeamList;
        }
        public static List<Team> GetTeamsAdmin()
        {
            List<Team> TeamList = new List<Team>();
            DataTable tblTeams = SqlHelper.ExecuteDataset(cs, "GetTeams").Tables[0];
            foreach (DataRow row in tblTeams.Rows)
            {
                Team team = new Team
                {
                    Id = (int)row["IDTeam"],
                    Name = row["Name"].ToString(),
                    TeamStatus = (TeamStatus)(int)row["TeamStatus"],
                    FoundingDate = (DateTime)row["FoundingDate"]
                };
                if (team.TeamStatus==TeamStatus.Active)
                {
                    TeamList.Add(team);
                }
            }
            return TeamList;
        }
        internal static List<Employee> GetTeamLeads()
        {
            List<Employee> employeeList = new List<Employee>();
            DataTable tblEmployees = SqlHelper.ExecuteDataset(cs, "GetTeamLeads").Tables[0];
            foreach (DataRow row in tblEmployees.Rows)
            {
                Employee employee = new Employee
                {
                    Id = (int)row["IDEmployee"],
                    Name = row["Name"].ToString(),
                    Surname = row["Surname"].ToString(),
                    FullName = $"{row["Name"]} {row["Surname"]}"
                };
                employeeList.Add(employee);
            }
            return employeeList.OrderBy(e => e.Surname).ToList();
        }
        internal static Team GetTeam(int teamID)
        {
            DataTable tbl = SqlHelper.ExecuteDataset(cs, "GetTeam", teamID).Tables[0];
            if (tbl.Rows.Count == 0) return null;

            DataRow row = tbl.Rows[0];
            return new Team
            {
                Id = (int)row["IDTeam"],
                Name = row["Name"].ToString(),
                TeamStatus = (TeamStatus)(int)row["TeamStatus"],
                FoundingDate = (DateTime)row["FoundingDate"]
            };
        }
        internal static string GetTeamLead(int teamID)
        {
            SqlParameter[] Param = new SqlParameter[2];
            Param[0] = new SqlParameter("@IDTeam", SqlDbType.Int);
            Param[0].Value = teamID;
            Param[1] = new SqlParameter("@IDEmpleyee", SqlDbType.Int);
            Param[1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(cs, CommandType.StoredProcedure, "GetTeamLead", Param);
            return Param[1].Value.ToString();
        }
        internal static int UpdateTeam(Team team)
        {
            SqlParameter[] Param = new SqlParameter[3];
            Param[0] = new SqlParameter("@IDTeam", SqlDbType.Int);
            Param[0].Value = team.Id;
            Param[1] = new SqlParameter("@Name", SqlDbType.NVarChar);
            Param[1].Value = team.Name;
            Param[2] = new SqlParameter("@TeamLead", SqlDbType.Int);
            Param[2].Value = team.TeamLead;
            return SqlHelper.ExecuteNonQuery(cs, CommandType.StoredProcedure, "UpdateTeam", Param);
        }
        internal static void DeactivateTeam(int iDTeam)
        {
            SqlParameter param = new SqlParameter("@IDTeam", SqlDbType.Int);
            param.Value = iDTeam;
            SqlHelper.ExecuteNonQuery(cs, CommandType.StoredProcedure, "DeactivateTeam", param);
        }
        internal static int InsertTeam(Team team)
        
        {
            SqlParameter[] Param = new SqlParameter[2];
            Param[0] = new SqlParameter("@Name", SqlDbType.NVarChar);
            Param[0].Value = team.Name;
            Param[1] = new SqlParameter("@TeamLead", SqlDbType.Int);
            Param[1].Value = team.TeamLead;
            return SqlHelper.ExecuteNonQuery(cs, CommandType.StoredProcedure, "InsertTeam", Param);
        }

        //------------------CLIENTS----------------------
        internal static List<Client> GetClients()
        {
            List<Client> clientList = new List<Client>();
            DataTable tblClients = SqlHelper.ExecuteDataset(cs, "GetClients").Tables[0];
            foreach (DataRow row in tblClients.Rows)
            {
                Client client = new Client
                {
                    Id = (int)row["IDClient"],
                    Name = row["Name"].ToString(),
                    OIB = row["OIB"].ToString(),
                    Address = row["Address"].ToString(),
                    ClientStatus = (ClientStatus)((int)row["ClientStatus"])
                };
                clientList.Add(client);
            }
            return clientList.OrderBy(e => e.Name).ToList();
        }
        public static Client GetClient(int clientID)
        {
            DataTable tbl = SqlHelper.ExecuteDataset(cs, "GetClient", clientID).Tables[0];
            if (tbl.Rows.Count == 0) return null;

            DataRow row = tbl.Rows[0];

            return new Client
            {
                Id = (int)row["IDClient"],
                Name = row["Name"].ToString(),
                OIB = row["OIB"].ToString(),
                Address = row["Address"].ToString(),
                ClientStatus = (ClientStatus)(int)row["ClientStatus"]
            };
        }
        internal static int UpdateClient(Client client)
        {
            SqlParameter[] Param = new SqlParameter[5];
            Param[0] = new SqlParameter("@IDClient", SqlDbType.Int);
            Param[0].Value = client.Id;
            Param[1] = new SqlParameter("@Name", SqlDbType.NVarChar);
            Param[1].Value = client.Name;
            Param[2] = new SqlParameter("@OIB", SqlDbType.NVarChar);
            Param[2].Value = client.OIB;
            Param[3] = new SqlParameter("@Address", SqlDbType.NVarChar);
            Param[3].Value = client.Address;
            Param[4] = new SqlParameter("@ClientStatus", SqlDbType.Bit);
            Param[4].Value = client.ClientStatus;
            return SqlHelper.ExecuteNonQuery(cs, CommandType.StoredProcedure, "UpdateClient", Param);
        }
        internal static void DeactivateClient(int clientID)
        {
            SqlParameter param = new SqlParameter("@IDClient", SqlDbType.Int);
            param.Value = clientID;
            SqlHelper.ExecuteNonQuery(cs, CommandType.StoredProcedure, "DeactivateClient", param);
        }
        internal static int InsertClient(Client client)
        {
            SqlParameter[] Param = new SqlParameter[5];
            Param[0] = new SqlParameter("@Name", SqlDbType.NVarChar);
            Param[0].Value = client.Name;
            Param[1] = new SqlParameter("@OIB", SqlDbType.NVarChar);
            Param[1].Value = client.OIB;
            Param[2] = new SqlParameter("@Address", SqlDbType.NVarChar);
            Param[2].Value = client.Address;
            Param[3] = new SqlParameter("@ClientStatus", SqlDbType.Bit);
            Param[3].Value = client.ClientStatus;
            return SqlHelper.ExecuteNonQuery(cs, CommandType.StoredProcedure, "InsertClient", Param);
        }

        //-----------------PROJECTS------------------------------

        internal static List<Project> GetProjects()
        {
            List<Project> projectList = new List<Project>();
            DataTable tblClients = SqlHelper.ExecuteDataset(cs, "GetProjects").Tables[0];
            foreach (DataRow row in tblClients.Rows)
            {
                Project project = new Project
                {
                    Id = (int)row["IDProject"],
                    Name = row["Name"].ToString()
                };
                projectList.Add(project);
            }
            return projectList.OrderBy(e => e.Name).ToList();
        }
        internal static Project GetProject(int projectID)
        {
            DataTable tbl = SqlHelper.ExecuteDataset(cs, "GetProjectDetails", projectID).Tables[0];
            if (tbl.Rows.Count == 0) return null;

            DataRow row = tbl.Rows[0];

            return new Project
            {
                Name = row["ProjectName"].ToString(),
                Client = new Client { Name = row["ClientName"].ToString() },
                ProjectLead = new Employee { FullName = row["ProjectLead"].ToString() },
                CreationDate = (DateTime)row["CreationDate"],
                ProjectStatus = (ProjectStatus)((int)row["ProjectStatus"])
            };
        }
        internal static List<Employee> GetProjectEmployees(int projectID)
        {
            List<Employee> employeeList = new List<Employee>();
            DataTable tblClients = SqlHelper.ExecuteDataset(cs, "GetProjectEmployees",projectID).Tables[0];
            foreach (DataRow row in tblClients.Rows)
            {
                Employee employee = new Employee
                {
                    Id = (int)row["IDEmployee"],
                    FullName = row["FullName"].ToString()
                };
                employeeList.Add(employee);
            }
            return employeeList.OrderBy(e => e.FullName).ToList();
        }
    }

}