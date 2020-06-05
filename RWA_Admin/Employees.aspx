<%@ Page Title="" Language="C#" MasterPageFile="~/RWA.Master" AutoEventWireup="true" CodeBehind="Employees.aspx.cs" Inherits="RWA_Admin.Employees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="d-flex">
    <div class="w-25 p-5">
        <asp:ListBox ID="lbEmployees" runat="server" Width="100%" CssClass="listBox"></asp:ListBox>
    </div>
    <div class="w-75 p-5">
        <div>
            <asp:Label Text="text" runat="server" />
        </div>
    </div>

</div>
</asp:Content>
