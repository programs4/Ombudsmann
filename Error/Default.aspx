<%@ Page Language="C#" %>

<!DOCTYPE html>
<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Clear();

    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ombudsman App </title>
    <link href="/Css/bootstrap.min.css" rel="stylesheet" />
    <link href="/Css/bootstrap-theme.css" rel="stylesheet" />
    <link href="/Css/styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="aspnetForm" runat="server">
        <div style="margin: 20px">
            <div class="alert alert-danger" role="alert">
                <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                <span class="sr-only">Error:</span>
                <%=Config._DefaultErrorMessages%> | 
                <a href="/">İstifadəçi girişi</a>
            </div>
        </div>
    </form>
</body>
</html>
