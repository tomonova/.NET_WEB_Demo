<%@ Page Title="" Language="C#" MasterPageFile="~/RWA.Master" AutoEventWireup="true" CodeBehind="NewClient.aspx.cs" Inherits="RWA_Admin.NewClient" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
     <div class="d-flex flex-column flex-lg-row flex-wrap">
        <div class="p-5 d-flex flex-fill ">
            <table class="table table-borderless">
                <tr>
                    <td>
                        <asp:Label Text="OIB:" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtOIB" runat="server" ></asp:TextBox>
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
                        <asp:Label ID="lblError" runat="server" Text="" Visible="true" ForeColor="Red" Font-Bold="true" Font-Size="Large" CssClass="error"></asp:Label>
                    </td>
                </tr>
            </table>

        </div>

    </div>
    <div class="p-2 d-flex flex-row mb-3">
        <div class="container">
            <asp:Button ID="btnDodaj" CssClass="btn btn-primary btn-lg btn-block p-2" runat="server" Text="CREATE" OnClick="btnDodaj_Click" />
        </div>
    </div>
</asp:Content>
