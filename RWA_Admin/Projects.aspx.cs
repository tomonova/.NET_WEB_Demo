using RWA_Admin.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWA_Admin
{
    public partial class Projects :  BasePage
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
                lbProjects.DataSource = Repo.GetProjects();
                lbProjects.DataTextField = "Name";
                lbProjects.DataValueField = "Id";
                lbProjects.DataBind();
            }
            catch (Exception ex)
            {

                lblError.Text = ex.Message;
            }
        }

        protected void lbProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowProjectData(int.Parse(lbProjects.SelectedValue));
            ShowContributors(int.Parse(lbProjects.SelectedValue));
        }

        private void ShowContributors(int projectID)
        {
            try
            {
                lbEmployess.DataSource = Repo.GetProjectEmployees(projectID);
                lbEmployess.DataTextField = "FullName";
                lbEmployess.DataValueField = "Id";
                lbEmployess.DataBind();
            }
            catch (Exception ex)
            {

                lblError.Text = ex.Message;
            }
        }

        private void ShowProjectData(int projectID)
        {
            try
            {
                Project project = Repo.GetProject(projectID);
                txtName.Text = project.Name;
                txtClient.Text = project.Client.Name;
                txtProjectLead.Text = project.ProjectLead.FullName;
                txtDate.Text = project.CreationDate.ToLongDateString();
                txtProjectStatus.Text = project.ProjectStatus.ToString();
            }
            catch (Exception ex)
            {

                lblError.Text = ex.Message;
            }
        }

        protected void btnDodaj_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/NewProject.aspx");
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            try
            {
                ChangeProjectStatus("D");
            }
            catch (Exception ex)
            {
                lblError.Text = $"{ex.Message}";
                Response.Redirect(Request.RawUrl);
            }
        }

        private void ChangeProjectStatus(string status)
        {
            try
            {
                int projectID = int.Parse(lbProjects.SelectedValue);
                Repo.ProjectStatusChange(projectID, status);
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {

                lblError.Text = ex.Message;
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                ChangeProjectStatus("C");
            }
            catch (Exception ex)
            {
                lblError.Text = $"{ex.Message}";
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {
            if (lbProjects.SelectedValue == null)
                return;

            Session["ProjectId"] = lbProjects.SelectedValue;
            Session["ProjectName"] = lbProjects.SelectedItem;
            Response.Redirect("~/ManageProjectContirbutors.aspx");
        }
        protected void Page_Error(object sender, EventArgs e)
        {
            Response.Redirect("~/Errors.aspx");
        }
    }
}