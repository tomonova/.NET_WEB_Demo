using RWA_Admin.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWA_Admin
{
    public partial class NewTeam : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTeamLeads(); 
            }
        }

        private void LoadTeamLeads()
        {
            try
            {
                ddlTeamLead.DataSource = Repo.GetTeamLeads();
                ddlTeamLead.DataTextField = "FullName";
                ddlTeamLead.DataValueField = "Id";
                ddlTeamLead.DataBind();
            }
            catch (Exception ex)
            {

                lblError.Text = ex.Message;
            }
        }

        protected void btnDodaj_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    Repo.InsertTeam(new Team
                    {
                        Name = txtIme.Text,
                        TeamLead = int.Parse(ddlTeamLead.SelectedValue)
                    });
                    Response.Redirect("~/Teams.aspx");
                }
                catch (Exception ex)
                {

                    lblError.Text = ex.Message;
                }

            }
        }
        protected void Page_Error(object sender, EventArgs e)
        {
            Response.Redirect("~/Errors.aspx");
        }
    }
}