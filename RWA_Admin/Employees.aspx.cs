using RWA_Admin.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWA_Admin
{
    public partial class Employees : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DDLFill();
            }
        }

        private void DDLFill()
        {
            lbEmployees.Height = 350;
            lbEmployees.DataSource = Repo.GetEmployees();
            lbEmployees.DataTextField = "FullName";
            lbEmployees.DataValueField = "Id";
            lbEmployees.DataBind();
            int i = 1;
            foreach (EmployeePosition item in (EmployeePosition[])Enum.GetValues(typeof(EmployeePosition)))
            {
                ddlEmpPosition.Items.Add(new ListItem(item.ToString(), i.ToString()));
                i++;
            }
            i = 1;
            foreach (EmployeeType item in (EmployeeType[])Enum.GetValues(typeof(EmployeeType)))
            {
                ddlEmpType.Items.Add(new ListItem(item.ToString(), i.ToString()));
                i++;
            }
            ddlTeamsAssigned.DataSource = Repo.GetTeams();
            ddlTeamsAssigned.DataTextField = "Name";
            ddlTeamsAssigned.DataValueField = "Id";
            ddlTeamsAssigned.DataBind();
        }


        private void ShowEmployeeData(int employeeID)
        {
            Employee employee = Repo.GetEmployeeAdmin(employeeID);
            lblID.Text = employee.Id.ToString();
            txtIme.Text = employee.Name;
            txtPrezime.Text = employee.Surname;
            datepicker.Text = employee.EmploymentDate.ToLongDateString();
            ddlEmpType.SelectedValue = ((int)employee.EmployeeType).ToString();
            ddlEmpPosition.SelectedValue = ((int)employee.EmployeePosition).ToString();
            ddlTeamsAssigned.SelectedValue = employee.AssignedTeam.ToString(); ;
        }
        protected void EmployeeIndexChange(object sender,EventArgs e)
        {
            ShowEmployeeData(int.Parse(lbEmployees.SelectedValue));
        }

        protected void btnObrisi_Click(object sender, EventArgs e)
        {
            int employeeID = int.Parse(lblID.Text);
            Repo.DeleteEmployee(employeeID);
            DDLFill();
        }

        protected void btnUredi_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Repo.UpdateEmployee(new Employee
                {
                    Id = int.Parse(lblID.Text),
                    Name = txtIme.Text,
                    Surname = txtPrezime.Text,
                    EmploymentDate = DateTime.Parse(datepicker.Text),
                    EmployeeType = (EmployeeType)Enum.Parse(typeof(EmployeeType), ddlEmpType.Text),
                    EmployeePosition = (EmployeePosition)Enum.Parse(typeof(EmployeePosition), ddlEmpPosition.Text),
                    AssignedTeam = int.Parse(ddlTeamsAssigned.Text)
                }); 
            }
        }
    }
}