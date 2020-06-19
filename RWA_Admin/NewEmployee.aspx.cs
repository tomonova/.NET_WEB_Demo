using RWA_Admin.App_Code;
using System;
using System.Web.UI.WebControls;

namespace RWA_Admin
{
    public partial class NewEmployee : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DDLFill();
        }

        private void DDLFill()
        {
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

        protected void btnDodaj_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int success = Repo.InsertEmployee(new Employee
                {
                    Name = txtIme.Text,
                    Surname = txtPrezime.Text,
                    EmploymentDate = DateTime.Parse(datepicker.Text),
                    EmployeeType = (EmployeeType)Enum.Parse(typeof(EmployeeType), ddlEmpType.Text),
                    EmployeePosition = (EmployeePosition)Enum.Parse(typeof(EmployeePosition), ddlEmpPosition.Text),
                    AssignedTeam = int.Parse(ddlTeamsAssigned.SelectedValue)
                });
                if (success != 1)
                {
                    ViewState["lblError"] = "Employee not updated";
                }
                else
                {
                    Response.Redirect("~/Employees.aspx");
                }
            }
            else
            {
                ViewState["lblError"] = "Not all employee data was correct";
            }
        }
    }
}