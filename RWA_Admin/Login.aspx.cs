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



            bool credCheck = Repo.CheckCredentialsAdmin(userName, GetSHA1Hash(userPass));
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
        string GetSHA1Hash(string input)
        {
            System.Security.Cryptography.SHA1CryptoServiceProvider x = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            string password = s.ToString();
            return password;
        }
        protected void Page_Error(object sender, EventArgs e)
        {
            Response.Redirect("~/Errors.aspx");
        }
    }
}