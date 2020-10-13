<%@ Page Title="" Language="C#" MasterPageFile="~/RWA.Master" AutoEventWireup="true" CodeBehind="NewProject.aspx.cs" Inherits="RWA_Admin.NewProject" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <title>New Project</title>
    <style>
        #linkProjects {
            font-weight: bold;
            color: purple;
        }
    </style>
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
                        <asp:TextBox ID="txtIme" runat="server"  />
                        <asp:RequiredFieldValidator ID="rqrdValidName" runat="server" ErrorMessage="Project Name Mandatory"  Display="Dynamic" ControlToValidate="txtIme" CssClass="error">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Client:" runat="server" />
                    </td>
                    <td> 
                        <asp:DropDownList ID="ddlClients" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rqrdClient" runat="server" ErrorMessage="Client Mandatory" Display="Dynamic" ControlToValidate="ddlClients" CssClass="error">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblInterno" runat="server" Text="Interno:"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chbInterno" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" Font-Bold="True" />
                        <asp:Label ID="lblError" runat="server" Text="" Visible="false" ForeColor="Red" Font-Bold="true" Font-Size="Large" CssClass="error"></asp:Label>
                    </td>
                </tr>
            </table>

        </div>

    </div>
    <div class="d-flex flex-row mb-3">
        <div class="container">
            <asp:Button ID="btnDodaj" CssClass="btn btn-primary btn-lg btn-block p-2" runat="server" Text="CREATE" OnClick="btnDodaj_Click" />
        </div>
    </div>
    <script>
        $(function () {
            $('#chbInterno').change(function () {
                if ($(this).is(':checked')) {
                    // disable the dropdown:
                    $('#ddlClients').attr('disabled', 'disabled');
                } else {
                    $('#ddlClients').removeAttr('disabled');
                }
            });
        });
    </script>
</asp:Content>
