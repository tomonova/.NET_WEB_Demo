<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Errors.aspx.cs" Inherits="RWA_Admin.Errors" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error page</title>
    <link href="Content/css/Errors.css" rel="stylesheet" />
    <script src="Scripts/Errors.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="box">
            <div class="box__ghost">
                <div class="symbol"></div>
                <div class="symbol"></div>
                <div class="symbol"></div>
                <div class="symbol"></div>
                <div class="symbol"></div>
                <div class="symbol"></div>

                <div class="box__ghost-container">
                    <div class="box__ghost-eyes">
                        <div class="box__eye-left"></div>
                        <div class="box__eye-right"></div>
                    </div>
                    <div class="box__ghost-bottom">
                        <div></div>
                        <div></div>
                        <div></div>
                        <div></div>
                        <div></div>
                    </div>
                </div>
                <div class="box__ghost-shadow"></div>
            </div>

            <div class="box__description">
                <div class="box__description-container">
                    <div class="box__description-title">Whoops!</div>
                    <div class="box__description-text">It seems like we couldn't find the page you were looking for</div>
                </div>

                <%--<a href="Home.aspx" target="_blank" class="box__button">Go back</a>--%>
                <asp:Button ID="Button1" runat="server" class="box__button" OnClick="linkClick" Text="Go back" />

            </div>

        </div>
    </form>
</body>
</html>
