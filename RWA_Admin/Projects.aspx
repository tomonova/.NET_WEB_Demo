<%@ Page Title="" Language="C#" MasterPageFile="~/RWA.Master" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="RWA_Admin.Projects" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Projects</title>
    <style>
        #linkProjects {
            font-weight: bold;
            color: purple;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="d-flex flex-column flex-lg-row flex-wrap">
        <div class="d-flex flex-fill p-5">
            <asp:ListBox ID="lbProjects" runat="server" Width="100%" CssClass="listBox" OnSelectedIndexChanged="lbProjects_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
        </div>
        <div class="p-5 d-flex flex-fill ">
            <table class="table table-borderless">
                <tr>
                    <td>
                        <asp:Label Text="Name:" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" ReadOnly="true" BorderWidth="0px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Client:" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtClient" runat="server" ReadOnly="true" BorderWidth="0px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Project lead:" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtProjectLead" runat="server" ReadOnly="true" BorderWidth="0px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Project opened:" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtDate" runat="server" ReadOnly="true" BorderWidth="0px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Project status:" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtProjectStatus" runat="server" ReadOnly="true" BorderWidth="0px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Project contibutors:" runat="server" />
                    </td>
                    <td rowspan="2">
                        <div>
                            <asp:ListBox ID="lbEmployess" runat="server" CssClass="listBox" Width="100%" Rows="8"></asp:ListBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnAddEmployee" CssClass="btn btn-primary p-2" runat="server" Text="Manage contributors" OnClick="btnAddEmployee_Click" CausesValidation="False" />
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
            <asp:Button ID="btnDodaj" CssClass="btn btn-primary p-2" runat="server" Text="NEW PROJECT" OnClick="btnDodaj_Click" CausesValidation="False" />
        </div>
        <div>
            <asp:Button ID="btnDeactivate" CssClass="btn btn-warning  mr-5 p-2" runat="server" Text="DEACTIVATE PROJECT" OnClick="btnDeactivate_Click1"
                AutoPostBack="true"
                OnClientClick="return confirm ('Are you sure you want to deactivate the project')" />
        </div>
        <div class="mr-lg-5">
            <asp:Button ID="btnClose" CssClass="btn btn-danger mr-5 p-2" runat="server" Text="CLOSE PROJECT"
                OnClick="btnClose_Click"
                AutoPostBack="true"
                OnClientClick="return confirm ('Are you sure you want to close the project')" />
        </div>
        <div class="ValidationSummary">
        </div>
    </div>
</asp:Content>
