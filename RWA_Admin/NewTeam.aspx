<%@ Page Title="" Language="C#" MasterPageFile="~/RWA.Master" AutoEventWireup="true" CodeBehind="NewTeam.aspx.cs" Inherits="RWA_Admin.NewTeam" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
      <div class="d-flex flex-column flex-lg-row flex-wrap">
        <div class="p-5 d-flex flex-fill ">
            <table class="table table-borderless">
                <tr>
                    <td>
                        <asp:Label Text="Name:" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtIme" runat="server" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqrValidName" runat="server" Display="Dynamic" ErrorMessage="Name is mandatory" ControlToValidate="txtIme">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Team Lead:" runat="server" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTeamLead" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rqrdTeamLead" runat="server" ErrorMessage="Team Lead Manadatory"  Display="Dynamic" ControlToValidate="ddlTeamLead" CssClass="error">*</asp:RequiredFieldValidator>
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
        <div class="container">
            <asp:Button ID="btnDodaj" CssClass="btn btn-primary btn-lg btn-block p-2" runat="server" Text="CREATE" OnClick="btnDodaj_Click" />
        </div>
    </div>
</asp:Content>
