using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LtrFooter.Text = string.Format("© <b>{0}</b> BÜTÜN HÜQUQLARI QORUNUR ", DateTime.Now.Year.ToString());

        Session.Clear();
        Session.RemoveAll();

        if (!IsPostBack)
            TxtUsername.Focus();
    }

    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        string Username = TxtUsername.Text;
        string Password = TxtPassword.Text;

        if (string.IsNullOrEmpty(Username))
        {
            Config.MsgBoxAjax("İstifadəçi adı daxil edin.");
            TxtUsername.Focus();
            return;
        }

        if (string.IsNullOrEmpty(Password))
        {
            Config.MsgBoxAjax("Şifrə daxil edni.");
            TxtPassword.Focus();
            return;
        }

        DataTable Dt = DALC.GetDataTable("*", "Users", "Username,Password,IsActive", new object[] { Username, Password.Sha1(), true });

        if (Dt == null)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        if (Dt.Rows.Count != 1)
        {
            Config.MsgBoxAjax("Giriş baş tutmadı!");
            return;
        }

        var Dictionary = new Dictionary<string, string>();

        //Success
        DALC.AdministratorClass AdminLoginInformation = new DALC.AdministratorClass();
        AdminLoginInformation.ID = Dt._RowsObject("ID")._ToInt16();
        AdminLoginInformation.Fullname = Dt._Rows("Fullname");
        AdminLoginInformation.UsersStatusID = Dt._Rows("UsersStatusID")._ToInt16();
        Session["AdminLogin"] = AdminLoginInformation;


        string ReturnResult = Config._GetQueryString("return");

        if (ReturnResult == "close-page")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "JsWindowClose", "this.close();", true);
            return;
        }

        if (ReturnResult.Length > 0)
        {
            Config.Rd(ReturnResult);
        }
        else
        {
            Config.Rd("/tools/main");
        }
    }
}