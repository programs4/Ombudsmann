using System;
using System.Web.UI;

public partial class Adminn_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PnlLeft.Style.Remove("display");
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "LeftPanelFixSize", "setLeftBlackPanel();", true);

        if (!IsPostBack)
        {
            LtrAdminFullName.Text = DALC._GetAdministratorsLogin.Fullname;
        }

        if (DALC._GetAdministratorsLogin.UsersStatusID != 30)
        {
            PnlUserWorker.Visible = false;
        }

        string URL = Request.Url.ToString().ToLower();

        if (URL.IndexOf("main") > -1)
            LtrTitle.Text = "Tənzimləmələr";
        if (URL.IndexOf("auditsorganizations") > -1)
            LtrTitle.Text = "Baxış keçirilən müəssisələr";
        if (URL.IndexOf("auditsorganizationsmeetingpersons") > -1)
            LtrTitle.Text = "Baxış zamanı görüşülən şəxslər";
        if (URL.IndexOf("individualcomplaints") > -1)
            LtrTitle.Text = "Fərdi şikayətlər";
        if (URL.IndexOf("callcenter") > -1)
            LtrTitle.Text = "Çağrı mərkəzinə gələn şikayətlər";
        if (URL.IndexOf("datalist") > -1)
            LtrTitle.Text = "Sorağçalar";
        if (URL.IndexOf("usersandworker") > -1)
            LtrTitle.Text = "İstifadəçilər və əməkdaşlar";
    }
}
