﻿using RWA_Admin.App_Code;
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
            ddlTeamLead.DataSource = Repo.GetTeamLeads();
            ddlTeamLead.DataTextField = "FullName";
            ddlTeamLead.DataValueField = "Id";
            ddlTeamLead.DataBind();
        }

        protected void btnDodaj_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int success = Repo.InsertTeam(new Team
                {
                    Name = txtIme.Text,
                    TeamLead = int.Parse(ddlTeamLead.SelectedValue)
                });
                if (success != 2)
                {
                    ViewState["lblError"] = "Team not created";
                }
                else
                {
                    Response.Redirect("~/Teams.aspx");
                }
            }
            else
            {
                ViewState["lblError"] = "Not all team data was correct";
            }
        }
    }
}