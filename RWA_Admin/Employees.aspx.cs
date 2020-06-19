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
        public bool UpdateError { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DDLFill();
            }
            else
            {
                if (ViewState["lblError"] != null)
                {
                    lblError.Text = ViewState["lblError"].ToString();
                }
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
            ddlTeamsAssigned.DataSource = Repo.GetEmployees();
            ddlTeamsAssigned.DataTextField = "Name";
            ddlTeamsAssigned.DataValueField = "Id";
            ddlTeamsAssigned.DataBind();
        }


        private void ShowEmployeeData(int employeeID)
        {
            Employee employee = Repo.GetEmployeeAdmin(employeeID);
            txtID.Text = employee.Id.ToString();
            txtIme.Text = employee.Name;
            txtPrezime.Text = employee.Surname;
            datepicker.Text = employee.EmploymentDate.ToString("dd/MM/yyyy");
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
            int employeeID = int.Parse(txtID.Text);
            Repo.DeleteEmployee(employeeID);
            DDLFill();
        }

        protected void btnUredi_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int success= Repo.UpdateEmployee(new Employee
                {
                    Id = int.Parse(txtID.Text),
                    Name = txtIme.Text,
                    Surname = txtPrezime.Text,
                    EmploymentDate = DateTime.Parse(datepicker.Text),
                    EmployeeType = (EmployeeType)Enum.Parse(typeof(EmployeeType), ddlEmpType.Text),
                    EmployeePosition = (EmployeePosition)Enum.Parse(typeof(EmployeePosition), ddlEmpPosition.Text),
                    AssignedTeam = int.Parse(ddlTeamsAssigned.SelectedValue)
                });
                if (success!=1)
                {
                    ViewState["lblError"] = "Employee not updated";
                }
                else
                {
                    DDLFill();
                    ViewState["lblError"] = null;
                }
            }
            else
            {
                ViewState["lblError"] = "Not all employee data was correct";
            }
        }

        protected void btnDodaj_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/NewEmployee.aspx");
        }
    }
}