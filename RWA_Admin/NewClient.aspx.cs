using RWA_Admin.App_Code;
using RWA_Admin.App_Code.Enums;
using System;
using System.Web.UI.WebControls;

namespace RWA_Admin
{
    public partial class NewClient : BasePage
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
            int i = 0;
            foreach (ClientStatus item in (ClientStatus[])Enum.GetValues(typeof(ClientStatus)))
            {
                ddlClientStatus.Items.Add(new ListItem(item.ToString(), i.ToString()));
                i++;
            }
        }

        protected void btnDodaj_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int success = Repo.InsertClient(new Client
                {
                    Name = txtIme.Text,
                    OIB = txtOIB.Text,
                    Address = txtAddress.Text,
                    ClientStatus = (ClientStatus)Enum.Parse(typeof(ClientStatus), ddlClientStatus.Text)
                });
                if (success != 1)
                {
                    ViewState["lblError"] = "Client not created";
                }
                else
                {
                    Response.Redirect("~/Clients.aspx");
                }
            }
            else
            {
                ViewState["lblError"] = "Not all client data was correct";
            }
        }
    }
}