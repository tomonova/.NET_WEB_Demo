<%@ Page Title="" Language="C#" MasterPageFile="~/RWA.Master" AutoEventWireup="true" CodeBehind="Clients.aspx.cs" Inherits="RWA_Admin.Clients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Clients</title>
    <style>
        #linkClients {
            font-weight: bold;
            color: purple;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
      <div class="d-flex flex-column flex-lg-row flex-wrap">
        <div class="d-flex flex-fill p-5">
            <asp:ListBox ID="lbClients" runat="server" Width="100%" CssClass="listBox" OnSelectedIndexChanged="lbClients_SelectedIndexChanged"   AutoPostBack="true"></asp:ListBox>
        </div>
        <div class="p-5 d-flex flex-fill ">
            <table class="table table-borderless">
                <tr>
                    <td>
                        <asp:Label Text="OIB:" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtOIB" runat="server" ReadOnly="true" BorderWidth="0px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqrValidOIB" runat="server" Display="Dynamic" ErrorMessage="OIB is mandatory, chose an employee" ControlToValidate="txtOIB">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Name:" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtIme" runat="server"  />
                        <asp:RequiredFieldValidator ID="rqrdValidName" runat="server" ErrorMessage="First Name Mandatory"  Display="Dynamic" ControlToValidate="txtIme" CssClass="error">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Address:" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtAddress" runat="server" />
                        <asp:RequiredFieldValidator ID="rqrdValidAddress" runat="server" ErrorMessage="Last name mandatory" Display="Dynamic" ControlToValidate="txtAddress" CssClass="error">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Status:" runat="server" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlClientStatus" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" Font-Bold="True" />
                        <asp:Label ID="lblError" runat="server" Text="test" Visible="false" ForeColor="Red" Font-Bold="true" Font-Size="Large" CssClass="error"></asp:Label>
                    </td>
                </tr>
            </table>

        </div>

    </div>
    <div class="p-2 d-flex flex-row mb-3">
        <div class="mr-auto ml-5">
            <asp:Button ID="btnDodaj" CssClass="btn btn-primary  p-2" runat="server" Text="NEW CLIENT" OnClick="btnDodaj_Click" CausesValidation="False" />
        </div>
        <div>
            <asp:Button ID="btnUredi" CssClass="btn btn-warning mr-5 p-2" runat="server" Text="EDIT CLIENT" OnClick="btnUredi_Click" />
        </div>
        <div class="mr-lg-5">
            <asp:Button ID="btnDeactivate" CssClass="btn btn-danger mr-5 p-2" runat="server" Text="DEACTIVATE CLIENT" 
                OnClick="btnDeactivate_Click"
                AutoPostBack="true"
                OnClientClick="return confirm ('Are you sure you want to deaactivate the team')"/>
        </div>
    </div>
</asp:Content>
