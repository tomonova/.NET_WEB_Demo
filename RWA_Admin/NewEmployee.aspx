<%@ Page Title="" Language="C#" MasterPageFile="~/RWA.Master" AutoEventWireup="true" CodeBehind="NewEmployee.aspx.cs" Inherits="RWA_Admin.NewEmployee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>New Employee</title>
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
                        <asp:RequiredFieldValidator ID="rqrdValidName" runat="server" ErrorMessage="First Name Mandatory"  Display="Dynamic" ControlToValidate="txtIme" CssClass="error">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Surname:" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtPrezime" runat="server" />
                        <asp:RequiredFieldValidator ID="rqrdValidSurname" runat="server" ErrorMessage="Last name mandatory" Display="Dynamic" ControlToValidate="txtPrezime" CssClass="error">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Employment date:" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="datepicker" runat="server" />
                        <script type="text/javascript">
                            $(document).ready(function () {
                                $('#datepicker').datepicker({
                                    format: 'dd.mm.yyyy'
                                });
                            });
                        </script>
                        <asp:RequiredFieldValidator ID="rqrdValidDate" runat="server" ErrorMessage="Date is mandatory" Display="Dynamic" ControlToValidate="datepicker" CssClass="error">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regValidDate" runat="server" ErrorMessage="Date in wrong format, please use dd/mm/yyyy" Display="Dynamic" ControlToValidate="datepicker"  
                            ValidationExpression="^([0-2][0-9]|(3)[0-1])(\.)(((0)[0-9])|((1)[0-2]))(\.)\d{4}$">*</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Employee type:" runat="server" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEmpType" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Employee position:" runat="server" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEmpPosition" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Team:" runat="server" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTeamsAssigned" runat="server" />
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
</asp:Content>
