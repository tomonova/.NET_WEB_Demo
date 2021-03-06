﻿using RWA_Admin.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWA_Admin
{
    public partial class Teams : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillData();
            }
        }

        private void FillData()
        {
            try
            {
                lbTeams.Height = 350;
                lbTeams.DataSource = Repo.GetTeams();
                lbTeams.DataTextField = "Name";
                lbTeams.DataValueField = "Id";
                lbTeams.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }

        }

        protected void lbTeams_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbTeams.SelectedItem.Text == "NONE")
                return;
            ShowTeamData(int.Parse(lbTeams.SelectedValue));
        }

        private void ShowTeamData(int teamID)
        {
            try
            {
                Team team = Repo.GetTeam(teamID);
                txtTeamName.Text = team.Name;
                txtDate.Text = team.FoundingDate.ToLongDateString();
                ddlTeamLead.DataSource = Repo.GetTeamLeads();
                ddlTeamLead.DataTextField = "FullName";
                ddlTeamLead.DataValueField = "Id";
                ddlTeamLead.DataBind();
                ddlTeamLead.SelectedValue = Repo.GetTeamLead(teamID);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        protected void btnDodaj_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/NewTeam.aspx");
        }

        protected void btnUredi_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    Repo.UpdateTeam(new Team
                    {
                        Id = int.Parse(lbTeams.SelectedValue),
                        Name = txtTeamName.Text,
                        TeamLead = int.Parse(ddlTeamLead.SelectedValue),
                    });
                    FillData();
                }
                catch (Exception ex)
                {

                    lblError.Text=ex.Message;
                }
            }
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            try
            {
                int IDTeam = int.Parse(lbTeams.SelectedValue);
                Repo.DeactivateTeam(IDTeam);
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        protected void Page_Error(object sender, EventArgs e)
        {
            Response.Redirect("~/Errors.aspx");
        }
    }
}