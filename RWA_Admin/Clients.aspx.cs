using RWA_Admin.App_Code;
using RWA_Admin.App_Code.Enums;
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
        public bool UpdateError { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillData();
            }
        }

        private void FillData()
        {
            lbClients.Height = 350;
            lbClients.DataSource = Repo.GetClients();
            lbClients.DataTextField = "Name";
            lbClients.DataValueField = "Id";
            lbClients.DataBind();
            lblClientStatus.Text = "";
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            int clientID = int.Parse(lbClients.SelectedValue);
            Repo.DeactivateTeam(clientID);
            Response.Redirect(Request.RawUrl);
            //FillData();
        }

        protected void btnUredi_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int success = Repo.UpdateClient(new Client
                {
                    Id = int.Parse(lbClients.SelectedValue),
                    Name = txtIme.Text,
                    Address = txtAddress.Text,
                    OIB=txtOIB.Text
                });
                if (success != 1)
                {
                    ViewState["lblError"] = "Client not updated";
                }
                else
                {
                    FillData();
                    ViewState["lblError"] = null;
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
            Client client = Repo.GetClient(clientID);
            txtOIB.Text = client.OIB.ToString();
            txtIme.Text = client.Name;
            txtAddress.Text = client.Address;
            lblClientStatus.Text = client.ClientStatus.ToString();
        }
    }
}