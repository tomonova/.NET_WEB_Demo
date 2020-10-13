using RWA_Admin.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWA_Admin
{
    public partial class ManageProjectContirbutors : BasePage
    {
        int projectID;
        List<Employee> ActiveContirbutors;
        List<int> removedEmployees;
        List<int> selectedEmployees;
        bool hasMatch;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ProjectId"] == null 
                || Session["ProjectName"]==null 
                || !int.TryParse(Session["ProjectId"].ToString(), out projectID))
            {
                Response.Redirect("~/Projects.aspx");
            }
            if (!IsPostBack)
            {
                FillData();
            }
        }

        private void FillData()
        {
            try
            {
                ddlProjects.DataSource = Repo.GetProjects();
                ddlProjects.DataTextField = "Name";
                ddlProjects.DataValueField = "Id";
                ddlProjects.DataBind();
                ddlProjects.SelectedValue = projectID.ToString();

                lbActiveEmployees.DataSource = Repo.GetProjectContributors(projectID);
                lbActiveEmployees.DataTextField = "FullName";
                lbActiveEmployees.DataValueField = "Id";
                lbActiveEmployees.DataBind();

                lbAvilableEmployees.DataSource = Repo.GetAvailableContributors(projectID);
                lbAvilableEmployees.DataTextField = "FullName";
                lbAvilableEmployees.DataValueField = "Id";
                lbAvilableEmployees.DataBind();
            }
            catch (Exception ex)
            {

                lblError.Text = ex.Message;
            }
        }

        protected void btnDodaj_Click(object sender, EventArgs e)
        {
            projectID = int.Parse(ddlProjects.SelectedValue);
            try
            {
                ActiveContirbutors = Repo.GetProjectContributors(projectID);
            }
            catch (Exception ex)
            {

                lblError.Text = ex.Message;
            }
            RemoveFromProject();
            AddToProject();
        }

        private void AddToProject()
        {
            if (lbActiveEmployees.Items.Count > 0)
            {
                selectedEmployees = GetNewContributors();
                try
                {
                    Repo.ManageContributors(selectedEmployees, projectID);
                }
                catch (Exception ex)
                {
                    lblError.Text = $"{ex.Message}";
                    Response.Redirect(Request.RawUrl);
                }
            }
        }

        private List<int> GetNewContributors()
        {
            List<int> chosenEmployees = GetChosenEmployees();
            foreach (Employee employee in ActiveContirbutors)
            {
                foreach (int employeeValue in chosenEmployees.ToList())
                {
                    if (employee.Id==employeeValue)
                    {
                        chosenEmployees.Remove(employeeValue);
                    }
                }
            }
            return chosenEmployees;
        }

        private List<int> GetChosenEmployees()
        {
            List<int> listOfEmployees = new List<int>();
            foreach (ListItem item in lbActiveEmployees.Items)
            {
                listOfEmployees.Add(int.Parse(item.Value));
            }

            return listOfEmployees;
        }

        private void RemoveFromProject()
        {
            List<int> chosenEmployees = GetChosenEmployees();
            removedEmployees = FilterEmployees(chosenEmployees, ActiveContirbutors);
            try
            {
                Repo.RemoveFromProject(removedEmployees, projectID);
            }
            catch (Exception ex)
            {
                lblError.Text = $"{ex.Message}";
                Response.Redirect(Request.RawUrl);
            }
        }

        private List<int> FilterEmployees(List<int> chosenEmployees, List<Employee> activeContirbutors)
        {
            List<int> filterEmployees = new List<int>();

            foreach (Employee employee in activeContirbutors)
            {
                hasMatch = false;

                foreach (int employeeValue in chosenEmployees)
                {
                    if (employee.Id==employeeValue)
                    {
                        hasMatch = true;
                    }
                }
                if (hasMatch == false)
                {
                    filterEmployees.Add(employee.Id);
                }
            }
            return filterEmployees;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (lbAvilableEmployees.SelectedIndex!=-1)
            {
                lbActiveEmployees.Items.Add(lbAvilableEmployees.SelectedItem);
                lbAvilableEmployees.Items.Remove(lbAvilableEmployees.SelectedItem);
                lbAvilableEmployees.SelectedIndex = -1;
                lbActiveEmployees.SelectedIndex = -1;
                sortListBoxes();
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (lbActiveEmployees.SelectedIndex!=-1)
            {
                lbAvilableEmployees.Items.Add(lbActiveEmployees.SelectedItem);
                lbActiveEmployees.Items.Remove(lbActiveEmployees.SelectedItem);
                lbAvilableEmployees.SelectedIndex = -1;
                lbActiveEmployees.SelectedIndex = -1;
                sortListBoxes();
            }
        }

        private void sortListBoxes()
        {
            List<ListItem> listActive = new List<ListItem>(lbActiveEmployees.Items.Cast<ListItem>());
            listActive = listActive.OrderBy(li => li.Text).ToList<ListItem>();
            lbActiveEmployees.Items.Clear();
            lbActiveEmployees.Items.AddRange(listActive.ToArray<ListItem>());

            List<ListItem> listAvailable = new List<ListItem>(lbAvilableEmployees.Items.Cast<ListItem>());
            listAvailable = listAvailable.OrderBy(li => li.Text).ToList<ListItem>();
            lbAvilableEmployees.Items.Clear();
            lbAvilableEmployees.Items.AddRange(listAvailable.ToArray<ListItem>());
        }

        protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            projectID = int.Parse(ddlProjects.SelectedValue);
            ClearData();
            FillData();
        }

        private void ClearData()
        {
            ddlProjects.Items.Clear();
            lbActiveEmployees.Items.Clear();
            lbAvilableEmployees.Items.Clear();
        }
        protected void Page_Error(object sender, EventArgs e)
        {
            Response.Redirect("~/Errors.aspx");
        }
    }
}