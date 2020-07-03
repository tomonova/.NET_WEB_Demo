<%@ Page Title="" Language="C#" MasterPageFile="~/RWA.Master" AutoEventWireup="true" CodeBehind="Teams.aspx.cs" Inherits="RWA_Admin.Teams" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Teams</title>
    <style>
        #linkTeams {
            font-weight: bold;
            color: purple;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
       <div class="d-flex flex-column flex-lg-row flex-wrap">
        <div class="d-flex flex-fill p-5">
            <asp:ListBox ID="lbTeams" runat="server" Width="100%" CssClass="listBox" OnSelectedIndexChanged="lbTeams_SelectedIndexChanged"   AutoPostBack="true"></asp:ListBox>
        </div>
        <div class="p-5 d-flex flex-fill ">
            <table class="table table-borderless">
                <tr>
                    <td>
                        <asp:Label Text="Name:" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtTeamName" runat="server" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqrValidName" runat="server" Display="Dynamic" ErrorMessage="Team Name is mandatory" ControlToValidate="txtTeamName">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Team Lead: " runat="server" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTeamLead" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Team creation date:" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtDate" ReadOnly="true" BorderWidth="0px" runat="server" />
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
            <asp:Button ID="btnDodaj" CssClass="btn btn-primary  p-2" runat="server" Text="NEW TEAM" OnClick="btnDodaj_Click" CausesValidation="False" />
        </div>
        <div>
            <asp:Button ID="btnUredi" CssClass="btn btn-warning mr-5 p-2" runat="server" Text="EDIT TEAM" OnClick="btnUredi_Click" />
        </div>
        <div class="mr-lg-5">
            <asp:Button ID="btnDeactivate" CssClass="btn btn-danger mr-5 p-2" runat="server" Text="DEACTIVATE TEAM" 
                OnClick="btnDeactivate_Click"
                AutoPostBack="true"
                OnClientClick="return confirm ('Are you sure you want to deaactivate the team')"/>
        </div>
    </div>
</asp:Content>
