<%@ Page Title="" Language="C#" MasterPageFile="~/RWA.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="RWA_Admin.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="d-flex flex-column justify-content-around align-items-center">
        <div>
            <asp:Label ID="lblWelcome" runat="server" CssClass="lblWelcome" Text="Welcome to application for managing your working hours"></asp:Label>
        </div>
        <div class="flex-fill">
            <img src="Content/Images/erv.jpg" id="welcomePicture" class="img-fluid" />
        </div>

    </div>
</asp:Content>
