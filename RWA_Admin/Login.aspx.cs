using RWA_Admin.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWA_Admin
{
    public partial class Login : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            login.Focus();
        }

        protected void btnLog_Click(object sender, EventArgs e)
        {
            string userName = login.Value.Trim();
            string userPass = password.Value.Trim();

            bool credCheck = Repo.CheckCredentialsAdmin(userName, userPass);
            if (credCheck)
            {
                Session["username"] = userName;
                Response.Redirect("Home.aspx");
            }
            else
            {
                login.Focus();
                lblInfo.Visible = true;
            }
        }
    }
}