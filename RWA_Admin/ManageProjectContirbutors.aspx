<%@ Page Title="" Language="C#" MasterPageFile="~/RWA.Master" AutoEventWireup="true" CodeBehind="ManageProjectContirbutors.aspx.cs" Inherits="RWA_Admin.ManageProjectContirbutors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Manage Project Contributors</title>
    <style>
        #linkProjects {
            font-weight: bold;
            color: purple;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="container mt-5">
        <div class="d-flex flex-column">
            <div class="d-flex justify-content-left mb-3">
                <asp:Label ID="lblProjectName" runat="server" Text="Project: " Font-Size="Large" Font-Bold="true"></asp:Label>
                <asp:DropDownList ID="ddlProjects" runat="server" CssClass="ml-3" OnSelectedIndexChanged="ddlProjects_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="d-flex flex-wrap bd-highlight ">
                <div class="p-2 mr-auto flex-fill flex-grow-1 flex-direction=column bd-highlight">
                    <asp:Label ID="lblACtive" runat="server" Text="Project contibutors" Font-Bold="true" CssClass="mb-2"></asp:Label>
                    <asp:ListBox ID="lbActiveEmployees" runat="server" Height="350px" Width="100%"></asp:ListBox>
                </div>
                <div class="p-2 flex-shrink bd-highlight align-self-center">
                    <asp:Button ID="btnAdd" runat="server" Text="  <<  " CssClass="btn btn-primary btn-lg" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnRemove" runat="server" Text="  >>  " CssClass="btn btn-primary btn-lg" OnClick="btnRemove_Click" />
                </div>
                <div class="p-2 ml-auto flex-fill flex-grow-1 flex-direction=column bd-highlight">
                    <asp:Label ID="lblAvailable" runat="server" Text="Available contibutors" Font-Bold="true" CssClass="mb-2"></asp:Label>
                    <asp:ListBox ID="lbAvilableEmployees" runat="server" Height="350px" Width="100%"></asp:ListBox>
                </div>
                <asp:Button ID="btnDodaj" OnClick="btnDodaj_Click" runat="server" Text="CONFIRM" CssClass="btn btn-dark btn-block mt-3" />
            </div>
        </div>
        <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red" Font-Bold="true" />
    </div>
</asp:Content>
