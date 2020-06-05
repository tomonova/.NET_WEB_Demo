<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RWA_Admin.Login" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ERV Login</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/css/login.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper fadeInDown">
            <div id="formContent">
                <!-- Icon -->
                <div class="fadeIn first">
                    <img src="Content/Images/erv.png" id="icon" alt="User Icon" />
                </div>
                <!-- Login Form -->
                <div>
                    <input type="text" id="login" class="fadeIn second" name="login" placeholder="login" runat="server" />
                    <input type="password" id="password" class="fadeIn third" name="login" placeholder="password" runat="server" />
                    <asp:Button ID="btnLog" runat="server" Text="Log In" CssClass="fadeIn fourth" OnClick="btnLog_Click" meta:resourcekey="btnLogResource1" />
                </div>
                <div id="formFooter">
                    <asp:Label ID="lblInfo" runat="server" Text="Label" Visible="False" meta:resourcekey="lblInfoResource1"></asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
