<%@ Page Title="" Language="C#" MasterPageFile="~/RWA.Master" AutoEventWireup="true" CodeBehind="Employees.aspx.cs" Inherits="RWA_Admin.Employees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Employee</title>
    <style>
        #linkEmployees {
            font-weight: bold;
            color: purple;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="d-flex flex-column flex-lg-row flex-wrap">
        <div class="d-flex flex-fill p-5">
            <asp:ListBox ID="lbEmployees" runat="server" Width="100%" CssClass="listBox" OnSelectedIndexChanged="EmployeeIndexChange" AutoPostBack="true"></asp:ListBox>
        </div>
        <div class="p-5 d-flex flex-fill ">
            <table class="table table-borderless">
                <tr>
                    <td>
                        <asp:Label Text="ID:" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtID" runat="server" ReadOnly="true" BorderWidth="0px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqrValidID" runat="server" Display="Dynamic" ErrorMessage="ID is mandatory, chose an employee" ControlToValidate="txtID">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Name:" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtIme" runat="server" />
                        <asp:RequiredFieldValidator ID="rqrdValidName" runat="server" ErrorMessage="First Name Mandatory" Display="Dynamic" ControlToValidate="txtIme" CssClass="error">*</asp:RequiredFieldValidator>
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
                        <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqrdValidEmail" runat="server" ErrorMessage="Email mandatory" Display="Dynamic" ControlToValidate="txtEmail" CssClass="error">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rqrdValidEmailFormat" runat="server" ErrorMessage="Email not correct" Display="Dynamic" ControlToValidate="txtEmail" CssClass="error" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
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
                        <asp:Label Text="Team:" runat="server" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTeamsAssigned" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblInfo" runat="server" Text="" Visible="True" Font-Bold="true" Font-Size="Large" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" Font-Bold="True" />
                        <asp:Label ID="lblError" runat="server" Text="" Visible="True" ForeColor="Red" Font-Bold="true" Font-Size="Large" CssClass="error"></asp:Label>
                    </td>
                </tr>
            </table>

        </div>

    </div>
    <div class="p-2 d-flex flex-row mb-3">
        <div class="mr-auto ml-5">
            <asp:Button ID="btnDodaj" CssClass="btn btn-primary  p-2" runat="server" Text="NEW EMPLOYEE" OnClick="btnDodaj_Click" CausesValidation="False" />
        </div>
        <div>
            <asp:Button ID="btnUredi" CssClass="btn btn-warning mr-5 p-2" runat="server" Text="EDIT EMPLOYEE" OnClick="btnUredi_Click" />
        </div>
        <div class="mr-lg-5">
            <asp:Button ID="btnObrisi" CssClass="btn btn-danger mr-5 p-2" runat="server" Text="DEACTIVATE EMPLOYEE"
                OnClick="btnObrisi_Click"
                AutoPostBack="true"
                OnClientClick="return confirm ('Are you sure you want to deactivate the user')" />
        </div>
        <div class="ValidationSummary">
        </div>
    </div>
</asp:Content>
