using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Adminn_Tools_Controls_Main : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Config._Route("type") == "password")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "tab-click", @"$('#password-tab').click();", true);
            }
        }

        TxtDescription.Text = DALC.GetUsersDescription(DALC._GetAdministratorsLogin.ID);
    }

    protected void BtnSaveNote_Click(object sender, EventArgs e)
    {
        int result = DALC.UpdateUserDescription(DALC._GetAdministratorsLogin.ID, TxtDescription.Text);
        if (result < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages, false);
            return;
        }

        Config.MsgBoxAjax(Config._DefaultSuccessMessages, true);
    }

    protected void BtnConfirm_Click(object sender, EventArgs e)
    {

        if (TxtOldPassword.Text.Length < 1)
        {
            Config.MsgBoxAjax("Köhnə şifrəni daxil edin.");
            return;
        }

        if (TxtNewPassword.Text.Length < 4)
        {
            Config.MsgBoxAjax("Yeni şifrə ən az 4 simvoldan ibarət olmalıdır.");
            return;
        }

        if (TxtNewPassword.Text != TxtBackPassword.Text)
        {
            Config.MsgBoxAjax("Yeni şifərləriniz uyğun gəlmir.");
            return;
        }

        if (Config.Sha1(TxtOldPassword.Text) != DALC.GetSingleValues("Password", "Users", "ID", DALC._GetAdministratorsLogin.ID, "", ""))
        {
            Config.MsgBoxAjax("Köhnə şifrəniz yalnışdır.");
            return;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("Password", Config.Sha1(TxtNewPassword.Text));
        Dictionary.Add("WhereID", DALC._GetAdministratorsLogin.ID);

        int ChekUpdate = DALC.UpdateDatabase("Users", Dictionary);

        if (ChekUpdate < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
        }
        else
        {
            Config.MsgBoxAjax(Config._DefaultSuccessMessages, true);
        }
    }

}
