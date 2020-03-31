using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_Controls_Datalist : System.Web.UI.UserControl
{

    string _CountCacheName = "Organizations";
    string _FilterSessionName = "OrganizationsFilter";

    void BindGrid()
    {
        GrdOrganizations.DataSource = null;
        GrdOrganizations.DataBind();

        if (Session[_FilterSessionName] != null)
        {
            var DictionarySession = (Dictionary<string, object>)Session[_FilterSessionName];
            if (DictionarySession.Count != 0)
            {
                DListTopOrganizationFilter.SelectedValue = DictionarySession["ParentID"]._ToString();
                TxtNameFilter.Text = DictionarySession["Name(LIKE)"]._ToString();
            }
        }

        var Dictionary = new Dictionary<string, object>()
        {
            {"ParentID",DListTopOrganizationFilter.SelectedValue},
            {"Name(LIKE)",TxtNameFilter.Text},
        };


        int PageNum;
        int RowNumber = 20;
        if (!int.TryParse(Config._GetQueryString("p"), out PageNum))
        {
            PageNum = 1;
        }

        // Axirinci sehifeni secmedikce Cache temizlemirik herdefe count almasin deye
        if (PageNum >= (Cache[_CountCacheName]._ToInt32() / RowNumber))
        {
            Cache.Remove(_CountCacheName);
        }

        HdnPageNumber.Value = PageNum.ToString();

        DALC.DataTableResult PersonsList = DALC.GetOrganizationsList(Dictionary, PageNum, RowNumber);

        if (PersonsList.Count == -1)
        {
            return;
        }

        if (PersonsList.Dt.Rows.Count < 1 && PageNum > 1)
        {
            Config.RedirectURL(string.Format("/tools/datalist/?p={0}", PageNum - 1));
        }

        int Total_Count = PersonsList.Count % RowNumber > 0 ? (PersonsList.Count / RowNumber) + 1 : PersonsList.Count / RowNumber;
        HdnTotalCount.Value = Total_Count.ToString();

        PnlPager.Visible = PersonsList.Count > RowNumber;
        GrdOrganizations.DataSource = PersonsList.Dt;
        GrdOrganizations.DataBind();
    }
    void ShowPopup()
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "JqueryModal", "$('#modalNewOrganization').modal('show');", true);
    }
    void ClearForm()
    {
        TxtOrganizationName.Text = "";
        ChkAktiv.Checked = false;
        DListTopOrganization.SelectedIndex = -1;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //eger istifadeci veya emeliyyatci deyilse
        if (DALC._GetAdministratorsLogin.UsersStatusID != 30 && DALC._GetAdministratorsLogin.UsersStatusID != 25)
        {
            LnkNew.Visible = false;
            GrdOrganizations.Columns[GrdOrganizations.Columns.Count - 1].Visible = false;
        }

        if (!IsPostBack)
        {
            DListTopOrganization.DataSource = DListTopOrganizationFilter.DataSource = DALC.GetOrganizationTop();
            DListTopOrganizationFilter.DataBind();
            DListTopOrganization.DataBind();
            DListTopOrganization.Items.Insert(0, new ListItem("--", "-1"));
            DListTopOrganizationFilter.Items.Insert(0, new ListItem("--", "-1"));
            BindGrid();
        }

    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        //eger istifadeci ve admin deyilse
        if (DALC._GetAdministratorsLogin.UsersStatusID != 30 && DALC._GetAdministratorsLogin.UsersStatusID != 25)
        {
            return;
        }

        if (DListTopOrganization.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Tabeliyində olan qurumu seçin.");
            ShowPopup();
            return;
        }

        if (string.IsNullOrEmpty(TxtOrganizationName.Text.Trim()))
        {
            Config.MsgBoxAjax("Qurumun adını daxil edin.");
            ShowPopup();
            return;
        }

        int result = -1;

        if (ViewState["operation"]._ToString() == "new")
        {
            result = DALC.InsertOrganization(TxtOrganizationName.Text, DListTopOrganization.SelectedValue, ChkAktiv.Checked);
        }
        else if (ViewState["operation"]._ToString() == "edit")
        {
            result = DALC.UpdateOrganization(ViewState["OrganizationId"]._ToString(), TxtOrganizationName.Text, DListTopOrganization.SelectedValue, ChkAktiv.Checked);
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
        ClearForm();
        ViewState["operation"] = "new";
        ShowPopup();
        BtnSave.Text = "Yadda saxla";
    }

    protected void LnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton Lnk = (sender as LinkButton);
        DataTable DtDetails = DALC.GetOrganizationById(Lnk.CommandArgument._ToString());
        if (Lnk.CommandName == "Redakte")
        {
            ClearForm();
            ViewState["operation"] = "edit";
            ShowPopup();
            BtnSave.Text = "Redaktə et";
            ViewState["OrganizationId"] = Lnk.CommandArgument;

            if (DtDetails != null && DtDetails.Rows.Count > 0)
            {
                TxtOrganizationName.Text = DtDetails._Rows("Name");
                ChkAktiv.Checked = (bool)DtDetails._RowsObject("IsDeleted");
                if (!string.IsNullOrEmpty(DtDetails._Rows("ID")))
                {
                    DListTopOrganization.SelectedValue = DtDetails._Rows("ParentID");
                }
            }
        }
    }

    protected void LnkDeleted_Click(object sender, EventArgs e)
    {
        LinkButton Lnk = sender as LinkButton;

        //admin olub olmadigin yoxlayaq
        if (DALC._GetAdministratorsLogin.UsersStatusID != 30 && DALC._GetAdministratorsLogin.UsersStatusID != 25)
        {
            return;
        }
        int result = DALC.DeactivateOrganization(Lnk.CommandArgument._ToString());
        if (result > 0)
        {
            Config.MsgBoxAjax("Qurum deaktiv edildi.", true);
            BindGrid();
        }
        else
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        var Dictionary = new Dictionary<string, object>()
        {
            {"ParentID",DListTopOrganizationFilter.SelectedValue},
            {"Name(LIKE)",TxtNameFilter.Text},
        };

        Session[_FilterSessionName] = Dictionary;
        Cache.Remove(_CountCacheName);

        Config.RedirectURL("/tools/datalist/?p=1");
    }
}