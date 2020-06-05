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
        protected void Page_Load(object sender, EventArgs e)
        {
            lbEmployees.Height = 350;
            lbEmployees.DataSource = Repo.GetEmployees();
            lbEmployees.DataTextField = "FullName";
            lbEmployees.DataValueField = "Id";
            lbEmployees.DataBind();
        }
    }
}