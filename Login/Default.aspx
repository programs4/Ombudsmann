<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Login_Default" %>

<%@ Register Src="~/MessagesAlert.ascx" TagPrefix="uc1" TagName="MessagesAlert" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ombudsman App </title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <!-- Stylesheets -->
    <link href="Css/LoginStyle.css" rel="stylesheet" />
    <link rel="stylesheet" href="/adminn/login/css/loginstyle.css" />
    <script src="/js/jquery.min.js"></script>
    <script src="/js/alertpopup.js"></script>

    <script>
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
            m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-87284511-1', 'auto');
        ga('send', 'pageview');

    </script>

</head>
<body>
    <uc1:MessagesAlert runat="server" ID="MessagesAlert" />
    <div class="container">
        <section id="content">
            <form id="AspnetForm" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <h1>İstifadəçi girişi</h1>
                        <div>
                            <asp:TextBox ID="TxtUsername" CssClass="username" runat="server" placeholder="İstifadəçi adı" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>

                        <div>
                            <asp:TextBox ID="TxtPassword" CssClass="password" runat="server" placeholder="Şifrə" TextMode="Password" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>

                        <div>
                            <asp:Button ID="BtnLogin" runat="server" Text="Giriş" OnClick="BtnLogin_Click"></asp:Button>
                        </div>

                        <div class="button" style="font-size: 8pt; margin-top: 85px; width: 400px; margin-left: -20px;">
                            <asp:Literal ID="LtrFooter" runat="server"></asp:Literal>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </form>

        </section>
    </div>
</body>
</html>
