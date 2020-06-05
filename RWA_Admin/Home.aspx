<%@ Page Title="" Language="C#" MasterPageFile="~/RWA.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="RWA_Admin.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
<div class="d-flex flex-column">
    <div class="p-5">
        <asp:Label ID="lblWelcome" runat="server" CssClass="lblWelcome" Text="Welcome to application for managing your working hours"></asp:Label>
    </div>
    <div class="p-5">
        <img src="Content/Images/erv.jpg" class="welcomePicture"/>
    </div>

</div>
</asp:Content>
