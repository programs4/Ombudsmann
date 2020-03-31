using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_Controls_Users : System.Web.UI.UserControl
{
    private void ShowPopup()
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "JqueryModal", "$('#modalNewUser').modal('show');", true);
    }
    void ClearForm()
    {
        DlistRegionalCenter.SelectedIndex = -1;
        TxtUsersName.Text = "";
        TxtPassword.Text = "";
        TxtPasswordRepeat.Text = "";
        TxtFullname.Text = "";
        TxtContacts.Text = "";
        TxtPositions.Text = "";
        TxtDescriptions.Text = "";
        DlistUsersStatus.SelectedIndex = -1;
    }
    void BindGrid()
    {
        GrdUsers.DataSource = DALC.GetOmbudsmanUsers(TxtFullnameFilter.Text);
        GrdUsers.DataBind();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //eger emeliyaytci deyilse bu sehifeni menuda gore bilmez. demeli url-e yazib gelib bu sehifeye. error sehifesine redirect edek
        if (DALC._GetAdministratorsLogin.UsersStatusID != 30)
        {
            Config.RedirectError();
            return;
        }
        if (!this.IsPostBack)
        {
            DlistRegionalCenter.DataSource = DALC.GetRegionalCenters();
            DlistRegionalCenter.DataBind();
            DlistRegionalCenter.Items.Insert(0, new ListItem("--", "-1"));
            DlistUsersStatus.DataSource = DALC.GetUsersStatus();
            DlistUsersStatus.DataBind();
            DlistUsersStatus.Items.Insert(0, new ListItem("--", "-1"));
        }
        BindGrid();
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        //emelliyyatci olub olmadigin yoxlayaq her ehtimala qarsi
        if (DALC._GetAdministratorsLogin.UsersStatusID != 30)
        {
            return;
        }
        if (DlistUsersStatus.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("İstifadəçinin statusunu seçin.");
            ShowPopup();
            return;
        }

        if (DlistRegionalCenter.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Regional mərkəzi seçin.");
            ShowPopup();
            return;
        }

        //eger adi istifadeci deyilse istifadeci adini ve sifreleri yoxlayaq
        if (DlistUsersStatus.SelectedValue != "10")
        {
            if (string.IsNullOrEmpty(TxtUsersName.Text.Trim()))
            {
                Config.MsgBoxAjax("İstifadəçi adını daxil edin.");
                ShowPopup();
                return;
            }

            if (TxtUsersName.Text.Trim().Length < 4 || TxtUsersName.Text.Trim().Length > 10)
            {
                Config.MsgBoxAjax("İstifadəçi adı minimum 4, maksimum 10 simvol olmalıdır.");
                ShowPopup();
                return;
            }

            string LoginAgain = DALC.GetSingleValues("Count(ID)", "Users", "Username", TxtUsersName.Text.Trim(), "", "-1");

            if (LoginAgain == "-1")
            {
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                ShowPopup();
                return;
            }

            //Eger yeni user yoxlayirsa onda sifreleri yoxlayaq update zamani bosda saxlaya biler
            if (ViewState["operation"]._ToString() == "new")
            {
                if (LoginAgain == "1")
                {
                    Config.MsgBoxAjax("İstifadəçi adı başqası tərəfindən istifadə edilir.");
                    ShowPopup();
                    return;
                }

                if (string.IsNullOrEmpty(TxtPassword.Text.Trim()))
                {
                    Config.MsgBoxAjax("Şifrəni daxil edin.");
                    ShowPopup();
                    return;
                }

                if (TxtPassword.Text.Trim() != TxtPasswordRepeat.Text.Trim())
                {
                    Config.MsgBoxAjax("Daxil edilən şifrələr düzgün deyil.");
                    ShowPopup();
                    return;
                }
            }
        }

        if (string.IsNullOrEmpty(TxtFullname.Text.Trim()))
        {
            Config.MsgBoxAjax("İstifadəçinin adını, soyadını və ata adını daxil edin.");
            ShowPopup();
            return;
        }

        if (string.IsNullOrEmpty(TxtContacts.Text.Trim()))
        {
            Config.MsgBoxAjax("İstifadəçinin əlaqə nömrəsini daxil edin.");
            ShowPopup();
            return;
        }

        if (string.IsNullOrEmpty(TxtPositions.Text.Trim()))
        {
            Config.MsgBoxAjax("İstifadəçinin vəzifəsini daxil edin.");
            ShowPopup();
            return;
        }
        int result = -1;

        if (ViewState["operation"]._ToString() == "new")
        {
            result = DALC.InsertUsers(DlistRegionalCenter.SelectedValue, TxtUsersName.Text, TxtPassword.Text, TxtFullname.Text,
                                     TxtContacts.Text, TxtPositions.Text, TxtDescriptions.Text, DlistUsersStatus.SelectedValue);
        }
        else if (ViewState["operation"]._ToString() == "edit")
        {
            result = DALC.UpdateUsers(ViewState["UsersId"]._ToString(), DlistRegionalCenter.SelectedValue, TxtPassword.Text, TxtFullname.Text,
                                      TxtContacts.Text, TxtPositions.Text, TxtDescriptions.Text, DlistUsersStatus.SelectedValue);
        }

        if (result > 0)
        {
            Config.MsgBoxAjax(ViewState["operation"]._ToString() == "new" ? "Məlumatlar qeydə alındı." : "Məlumatlar redaktə edildi.", true);
            BindGrid();
        }
        else
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
        }
    }

    protected void LnkNew_Click(object sender, EventArgs e)
    {
        TxtUsersName.Enabled = true;

        ClearForm();
        PnlPasswordRepeat.Visible = true;
        LblWarning.Visible = false;
        ViewState["operation"] = "new";
        ShowPopup();
        BtnSave.Text = "Yadda saxla";
        DlistUsersStatus_SelectedIndexChanged(null, null);
    }

    protected void DlistUsersStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DlistUsersStatus.SelectedValue == "10" || DlistUsersStatus.SelectedValue == "-1")
        {
            PnlUsernamePassword.Visible = false;
            if (ViewState["operation"]._ToString() == "edit")
            {
                LblWarning.Visible = false;
            }
        }
        else
        {
            PnlUsernamePassword.Visible = true;
            if (ViewState["operation"]._ToString() == "edit")
            {
                LblWarning.Visible = true;
            }
        }
        ShowPopup();
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void LnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton Lnk = sender as LinkButton;
        DataTable DtDetails = DALC.GetOmbudsmanUsersById(Lnk.CommandArgument._ToString());

        ClearForm();
        ViewState["operation"] = "edit";
        ShowPopup();
        BtnSave.Text = "Redaktə et";
        ViewState["UsersId"] = Lnk.CommandArgument;

        if (DtDetails != null && DtDetails.Rows.Count > 0)
        {
            DlistRegionalCenter.SelectedValue = DtDetails._Rows("RegionalCentersID");
            TxtUsersName.Text = DtDetails._Rows("Username");
            TxtUsersName.Enabled = false;

            TxtFullname.Text = DtDetails._Rows("Fullname");
            TxtContacts.Text = DtDetails._Rows("Contacts");
            TxtPositions.Text = DtDetails._Rows("Positions");
            DlistUsersStatus.SelectedValue = DtDetails._Rows("UsersStatusID");
            DlistUsersStatus_SelectedIndexChanged(null, null);

            PnlPasswordRepeat.Visible = false;
            TxtDescriptions.Text = DtDetails._Rows("Descriptions");
        }
    }

    protected void LnkDeleted_Click(object sender, EventArgs e)
    {
        LinkButton Lnk = sender as LinkButton;

        //admin olub olmadigin yoxlayaq
        if (DALC._GetAdministratorsLogin.UsersStatusID != 30)
        {
            return;
        }
        int result = DALC.DeactivateUsers(Lnk.CommandArgument._ToString());
        if (result > 0)
        {
            Config.MsgBoxAjax("İstifadəçi deaktiv edildi.", true);
            BindGrid();
        }
        else
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
        }
    }
}