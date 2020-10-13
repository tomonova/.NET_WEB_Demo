using RWA_Admin.App_Code;
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
                try
                {
                    Repo.InsertClient(new Client
                    {
                        Name = txtIme.Text.Trim(),
                        OIB = txtOIB.Text.Trim(),
                        Address = txtAddress.Text.Trim(),
                        ClientStatus = (ClientStatus)Enum.Parse(typeof(ClientStatus), ddlClientStatus.Text)
                    });
                    Response.Redirect("~/Clients.aspx");
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                }
            }
            else
            {
                ViewState["lblError"] = "Not all client data was correct";
            }
        }
        protected void Page_Error(object sender, EventArgs e)
        {
            Response.Redirect("~/Errors.aspx");
        }
    }
}