using RWA_Admin.App_Code;
using System;
using System.Web.UI.WebControls;

namespace RWA_Admin
{
    public partial class NewEmployee : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
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

        private void DDLFill()
        {
            int i = 1;
            foreach (EmployeeType item in (EmployeeType[])Enum.GetValues(typeof(EmployeeType)))
            {
                ddlEmpType.Items.Add(new ListItem(item.ToString(), i.ToString()));
                i++;
            }
            ddlTeamsAssigned.DataSource = Repo.GetTeamsAdmin();
            ddlTeamsAssigned.DataTextField = "Name";
            ddlTeamsAssigned.DataValueField = "Id";
            ddlTeamsAssigned.DataBind();
        }

        protected void btnDodaj_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    if (Repo.CheckMail(txtEmail.Text.Trim()))
                    {
                        lblError.Text = $"{txtEmail.Text.Trim()} već postoji, unesite novi EMail";
                    }

                    else
                    {
                        Repo.InsertEmployee(new Employee
                        {
                            Name = txtIme.Text.Trim(),
                            Surname = txtPrezime.Text.Trim(),
                            Email = txtEmail.Text.Trim(),
                            EmploymentDate = DateTime.Parse(datepicker.Text),
                            EmployeeType = (EmployeeType)Enum.Parse(typeof(EmployeeType), ddlEmpType.Text),
                            AssignedTeam = int.Parse(ddlTeamsAssigned.SelectedValue)
                        });
                        lblError.Text = String.Empty;
                        lblInfo.Text = $"Zaposlenik {txtIme.Text.Trim()} {txtPrezime.Text.Trim()} unesen";
                        ClearPage();
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

        private void ClearPage()
        {
            txtIme.Text = string.Empty;
            txtPrezime.Text = string.Empty;
            txtEmail.Text = string.Empty;
            datepicker.Text = string.Empty;
        }
        protected void Page_Error(object sender, EventArgs e)
        {
            Response.Redirect("~/Errors.aspx");
        }
    }
}