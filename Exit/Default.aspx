<%@ Page Language="C#" %>

<!DOCTYPE html>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Clear();
        Config.Rd("/?k=pJXazBZXqhfrQAnJNmjfFfphT2QZKsDuKsMsFgPdVyMrYTeGGk&return=" + Config._GetQueryString("return"));
    }
</script>

