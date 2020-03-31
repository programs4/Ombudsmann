using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Adminn_Tools_Default : System.Web.UI.Page
{
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string ControlPage = Config._Route("page");

        if (DALC._GetAdministratorsLogin == null)
        {
            Session["AdminLogin"] = null;
            Config.Rd("/?return=" + Request.Url.ToString());
            return;
        }
        try
        {
            PanelControl.Controls.Add(Page.LoadControl("/tools/controls/" + ControlPage + ".ascx"));
        }
        catch
        {
            Config.RedirectError();
            return;
        }
    }

    protected void LnkChangePage_Click(object sender, EventArgs e)
    {
        Config.Rd(string.Format("/tools/{1}", (sender as LinkButton).CommandArgument));
        return;
    }

    protected void LinkButtonExit_Click(object sender, EventArgs e)
    {
        Config.Rd("/exit");
    }
}