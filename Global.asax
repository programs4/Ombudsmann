<%@ Application Language="C#" %>
<script RunAt="server">

    void Application_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError().GetBaseException();
        DALC.ErrorLogsInsert("OMBUDSMAN - Global.asax sehv verdi: " + ex.Message + "  |  Source: " + ex.Source, true);

        //Master Page-də səhv çıxan da 
        if (Request.Url.ToString().ToLower().IndexOf("/error") > -1)
        {
            Response.Write("Error 404");
            Response.End();
        }

        Config.RedirectError();
        Response.End();
    }

    void Application_Start(object sender, EventArgs e)
    {
        System.Web.Routing.RouteCollection Collection = new System.Web.Routing.RouteCollection();

        //Admin panel     
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("AdminPanelLogin1", "login", "~/adminn/login/default.aspx"));
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("AdminPanel1", "tools", "~/tools/default.aspx"));
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("AdminPanel2", "tools/{page}", "~/tools/default.aspx"));
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("AdminPanel3", "tools/{page}/{type}", "~/tools/default.aspx"));
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("AdminPanel4", "tools/{page}/{type}/{pagenumber}", "~/tools/default.aspx"));
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("AdminPanel5", "tools/{page}/{type}/{dataid}/{pagenumber}", "~/tools/default.aspx"));
    }

</script>
