using RWA_Admin.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWA_Admin
{
    public partial class NewProject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillClients();
            }
        }

        private void FillClients()
        {
            ddlClients.DataSource = Repo.GetClients();
            ddlClients.DataTextField = "Name";
            ddlClients.DataValueField = "Id";
            ddlClients.DataBind();
        }

        protected void btnDodaj_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (chbInterno.Checked)
                {
                    int success = Repo.InsertInternalProject(new Project
                    {
                        Name = txtIme.Text,

                    }, Session["username"].ToString());
                    if (success != 1)
                    {
                        ViewState["lblError"] = "Project not created";
                    }
                    else
                    {
                        Response.Redirect("~/Projects.aspx");
                    }
                }
                else
                {
                    int success = Repo.InsertProject(new Project
                    {
                        Name = txtIme.Text,
                        Client = Repo.GetClient(int.Parse(ddlClients.SelectedValue))

                    }, Session["username"].ToString());
                    if (success != 1)
                    {
                        ViewState["lblError"] = "Project not created";
                    }
                    else
                    {
                        Response.Redirect("~/Projects.aspx");
                    }
                }
            }
            else
            {
                ViewState["lblError"] = "Not all employee data was correct";
            }
        }
        protected void Page_Error(object sender, EventArgs e)
        {
            Response.Redirect("~/Errors.aspx");
        }
    }
}
