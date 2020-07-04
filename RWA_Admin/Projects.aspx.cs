using RWA_Admin.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWA_Admin
{
    public partial class Projects : System.Web.UI.Page
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
            lbProjects.DataSource = Repo.GetProjects();
            lbProjects.DataTextField = "Name";
            lbProjects.DataValueField = "Id";
            lbProjects.DataBind();
        }

        protected void lbProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowProjectData(int.Parse(lbProjects.SelectedValue));
            ShowContributors(int.Parse(lbProjects.SelectedValue));
        }

        private void ShowContributors(int projectID)
        {
            lbEmployess.DataSource = Repo.GetProjectEmployees(projectID);
            lbEmployess.DataTextField = "FullName";
            lbEmployess.DataValueField = "Id";
            lbEmployess.DataBind();
        }

        private void ShowProjectData(int projectID)
        {
            Project project = Repo.GetProject(projectID);
            txtName.Text = project.Name;
            txtClient.Text = project.Client.Name;
            txtProjectLead.Text = project.ProjectLead.FullName;
            txtDate.Text = project.CreationDate.ToLongDateString();
            txtProjectStatus.Text = project.ProjectStatus.ToString();
        }

        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {

        }

        protected void btnDodaj_Click(object sender, EventArgs e)
        {

        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {

        }

        protected void btnDeactivate_Click1(object sender, EventArgs e)
        {

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }
    }
}