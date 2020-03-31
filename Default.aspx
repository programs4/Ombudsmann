<%@ Page Language="C#" %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetAdministratorsLogin != null)
        {
            Config.Rd("/tools/main");
            return;
        }

        Session.Clear();
        Config.Rd("/login/?k=pJXazBZXqhfrQAnJNmjfFfphT2QZKsDuKsMsFgPdVyMrYTeGGk&return=" + Config._GetQueryString("return"));
    }
</script>
