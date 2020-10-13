using RWA_Admin.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWA_Admin
{
    public partial class Clients : BasePage
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
                lbClients.Height = 350;
                lbClients.DataSource = Repo.GetClients();
                lbClients.DataTextField = "Name";
                lbClients.DataValueField = "Id";
                lbClients.DataBind();
                lblClientStatus.Text = "";
            }
            catch (Exception ex)
            {

                lblError.Text = ex.Message;
            }
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            try
            {
                int clientID = int.Parse(lbClients.SelectedValue);
                Repo.DeactivateClient(clientID);
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        protected void btnUredi_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    Repo.UpdateClient(new Client
                    {
                        Id = int.Parse(lbClients.SelectedValue),
                        Name = txtIme.Text.Trim(),
                        Address = txtAddress.Text.Trim(),
                        OIB = txtOIB.Text.Trim()
                    });
                    lblInfo.Text = $"Klijent  {txtIme.Text.Trim()} promijenjen";
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                }
            }
        }

        protected void btnDodaj_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/NewClient.aspx");
        }

        protected void lbClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowClientData(int.Parse(lbClients.SelectedValue));
        }
        private void ShowClientData(int clientID)
        {
            try
            {
                Client client = Repo.GetClient(clientID);
                txtOIB.Text = client.OIB.ToString();
                txtIme.Text = client.Name;
                txtAddress.Text = client.Address;
                lblClientStatus.Text = client.ClientStatus.ToString();
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