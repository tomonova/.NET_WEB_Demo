<%@ Page Title="" Language="C#" MasterPageFile="~/RWA.Master" AutoEventWireup="true" CodeBehind="Employees.aspx.cs" Inherits="RWA_Admin.Employees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Employee</title>
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
                        <asp:Label ID="lblID" runat="server" Text="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Name:" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtIme" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Surname:" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtPrezime" runat="server" />
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
                                $('#datepicker').datepicker();
                            });
                        </script>
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
                        <asp:DropDownList ID="ddlTeamsAssigned" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblError" runat="server" Text="test" Visible="false" ForeColor="Red" Font-Bold="true" Font-Size="Large"></asp:Label>
                    </td>
                </tr>
            </table>

        </div>

    </div>
    <div class="p-2 d-flex flex-row mb-3">
        <div class="mr-auto ml-5">
            <asp:Button ID="btnDodaj" CssClass="btn btn-primary  p-2" runat="server" Text="NEW EMPLOYEE" />
        </div>
        <div>
            <asp:Button ID="btnUredi" CssClass="btn btn-warning mr-5 p-2" runat="server" Text="EDIT EMPLOYEE" OnClick="btnUredi_Click" />
        </div>
        <div class="mr-lg-5">
            <asp:Button ID="btnObrisi" CssClass="btn btn-danger mr-5 p-2" runat="server" Text="DELETE EMPLOYEE" 
                OnClick="btnObrisi_Click"
                AutoPostBack="true"
                OnClientClick="return confirm ('Are you sure you want to delete the user')"/>
        </div>
        <div class="">

        </div>
    </div>
</asp:Content>
