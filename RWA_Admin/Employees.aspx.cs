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
        private const int EMPTY_VALUE = 0;
        private const string CHOOSE_TEAM = "--TEAM--";
        private const string CHOOSE_TYPE = "--TYPE--";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    DDLFill();
                }
                catch (Exception ex)
                {

                    lblError.Text = ex.Message;
                }
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
            ddlEmpType.AppendDataBoundItems = true;
            ddlEmpType.Items.Add(new ListItem { Text = CHOOSE_TYPE, Value = EMPTY_VALUE.ToString() });
            foreach (EmployeeType item in (EmployeeType[])Enum.GetValues(typeof(EmployeeType)))
            {
                ddlEmpType.Items.Add(new ListItem(item.ToString(), i.ToString()));
                i++;
            }
            ddlTeamsAssigned.AppendDataBoundItems = true;
            ddlTeamsAssigned.Items.Add(new ListItem { Text = CHOOSE_TEAM, Value = EMPTY_VALUE.ToString() });
            ddlTeamsAssigned.DataSource = Repo.GetTeamsAdmin();
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
                txtEmail.Text = employee.Email;
                datepicker.Text = employee.EmploymentDate.ToString("dd/MM/yyyy");
                ddlEmpType.SelectedValue = ((int)employee.EmployeeType).ToString();
                ddlTeamsAssigned.SelectedValue = employee.AssignedTeam.ToString(); 
        }
        protected void EmployeeIndexChange(object sender,EventArgs e)
        {
            try
            {
                ShowEmployeeData(int.Parse(lbEmployees.SelectedValue));
            }
            catch (Exception ex)
            {

                lblError.Text = ex.Message;
            }
        }

        protected void btnObrisi_Click(object sender, EventArgs e)
        {
            int employeeID = int.Parse(txtID.Text);
            try
            {
                Repo.DeleteEmployee(employeeID);
            }
            catch (Exception ex)
            {

                lblError.Text = ex.Message;
            }
            DDLFill();
        }

        protected void btnUredi_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    lblInfo.Text = string.Empty;
                    int mailStatus = Repo.CheckMail(txtEmail.Text.Trim(), int.Parse(lbEmployees.SelectedValue));                      
                    if (mailStatus==0 || mailStatus==1)
                    {
                        if (ddlTeamsAssigned.Text != EMPTY_VALUE.ToString() && ddlEmpType.Text != EMPTY_VALUE.ToString())
                        {
                            Repo.UpdateEmployee(new Employee
                            {
                                Id = int.Parse(txtID.Text),
                                Name = txtIme.Text.Trim(),
                                Surname = txtPrezime.Text.Trim(),
                                Email = txtEmail.Text.Trim(),
                                EmploymentDate = DateTime.Parse(datepicker.Text),
                                EmployeeType = (EmployeeType)Enum.Parse(typeof(EmployeeType), ddlEmpType.Text),
                                AssignedTeam = int.Parse(ddlTeamsAssigned.SelectedValue)
                            }) ;
                            lblError.Text = String.Empty;
                            lblInfo.Text = $"Zaposlenik {txtIme.Text.Trim()} {txtPrezime.Text.Trim()} promijenjen";
                        }
                    }
                    else if (mailStatus==2)
                    {
                        lblError.Text = $"{txtEmail.Text.Trim()} već postoji, unesite novi Email";
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
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
        protected void Page_Error(object sender, EventArgs e)
        {
            Response.Redirect("~/Errors.aspx");
        }
    }
}