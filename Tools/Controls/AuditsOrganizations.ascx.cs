using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Tools_Controls_AuditsOrganizations : System.Web.UI.UserControl
{
    string _CountCacheName = "AuditsOrganizations";
    string _FilterSessionName = "AuditsOrganizations";

    void BindGrid()
    {
        if (Session[_FilterSessionName] != null)
        {
            var DictionarySession = (Dictionary<string, object>)Session[_FilterSessionName];
            if (DictionarySession.Count != 0)
            {
                TxtRowIndexFilter.Text = DictionarySession["RowIndex"]._ToString();
                DlistRegionalCenterFilter.SelectedValue = DictionarySession["RegionalCentersID"]._ToString();
                DListParentOrganizationFilter.SelectedValue = DictionarySession["ParentOrganizationsID"]._ToString();
                DListTopOrganizationFilter_SelectedIndexChanged(null, null);
                DListSubOrganizationFilter.SelectedValue = DictionarySession["OrganizationsID"]._ToString();
                DlistVisitTypeFilter.SelectedValue = DictionarySession["VisitTypeID"]._ToString();
                TxtVisitDtStartFilter.Text = DictionarySession["StartVisit_Dt"]._ToString();
                TxtVisitDtEndFilter.Text = DictionarySession["EndVisit_Dt"]._ToString();
                TxtDescriptionFilter.Text = DictionarySession["Descriptions"]._ToString();
                DListDatePriority.SelectedValue = DictionarySession["DatePriority"]._ToString();
                DListPageSize.SelectedValue = DictionarySession["PageSize"]._ToString();
            }
        }

        #region BetweenDateFromat

        string Date = "";

        string Dt1 = "19000101";
        string Dt2 = DateTime.Now.ToString("yyyyMMdd");

        object DateFilter1 = Config.DateTimeFormat(TxtVisitDtStartFilter.Text.Trim());
        object DateFilter2 = Config.DateTimeFormat(TxtVisitDtEndFilter.Text.Trim());

        if (DateFilter1 == null && DateFilter2 == null)
        {
            Date = "";
        }
        else
        {
            if (DateFilter1 != null)
            {
                Dt1 = ((DateTime)DateFilter1).ToString("yyyyMMdd");
            }

            if (DateFilter2 != null)
            {
                Dt2 = ((DateTime)DateFilter2).ToString("yyyyMMdd");
            }

            Date = Dt1 + "&" + Dt2;
        }
        #endregion


        var Dictionary = new Dictionary<string, object>()
        {
            {"RegionalCentersID", DlistRegionalCenterFilter.SelectedValue},
            {"ParentOrganizationsID", DListParentOrganizationFilter.SelectedValue},
            {"OrganizationsID", DListSubOrganizationFilter.SelectedValue},
            {"VisitTypeID",  DlistVisitTypeFilter.SelectedValue},
            {"Visit_Dt(BETWEEN)",Date},
            {"Descriptions(LIKE)",TxtDescriptionFilter.Text}
        };

        string OrderByType = "desc";

        if (DListDatePriority.SelectedValue == "20")
        {
            OrderByType = "asc";
        }

        //Sira sayina gore filteri elave olaraq gonderirik
        int RowIndex = -1;
        if (!int.TryParse(TxtRowIndexFilter.Text, out RowIndex))
        {
            RowIndex = -1;
        }

        int PageNum;
        int RowNumber = 30;

        if (DListPageSize.SelectedValue == "20")
        {
            RowNumber = 10000;
        }

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

        DALC.DataTableResult ApplicationsList = DALC.GetAuditsOrganizations(Dictionary, PageNum, RowNumber, new object[] { RowIndex }, OrderByType);

        if (ApplicationsList.Count == -1)
        {
            return;
        }

        if (ApplicationsList.Dt.Rows.Count < 1 && PageNum > 1)
        {
            Config.RedirectURL(string.Format("/tools/AuditsOrganizations/?p={0}", PageNum - 1));
        }

        int Total_Count = ApplicationsList.Count % RowNumber > 0 ? (ApplicationsList.Count / RowNumber) + 1 : ApplicationsList.Count / RowNumber;
        HdnTotalCount.Value = Total_Count.ToString();

        PnlPager.Visible = ApplicationsList.Count > RowNumber;

        GrdAuditsOrganizations.DataSource = ApplicationsList.Dt;
        GrdAuditsOrganizations.DataBind();

        LblCountInfo.Text = string.Format("Axtarış üzrə nəticə: {0}", ApplicationsList.Count);
    }

    void ClearForm()
    {
        for (int i = 0; i < DlistUsers.Items.Count; i++)
        {
            DlistUsers.Items[i].Selected = false;
        }
        DlistRegionalCenter.SelectedIndex = -1;
        DListSubOrganization.SelectedIndex = -1;
        DlistVisitType.SelectedIndex = -1;
        TxtProblems.Text = "";
        TxtSuggestion.Text = "";
        TxtVisitDate.Text = "";
        TxtDescription.Text = "";
    }

    void ShowPopup()
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "JqueryModal", "$('#modalNewAudit').modal('show');", true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //admin olub olmadigin yoxlayaq
        if (DALC._GetAdministratorsLogin.UsersStatusID != 30 && DALC._GetAdministratorsLogin.UsersStatusID != 25)
        {
            LnkNew.Visible = false;
            GrdAuditsOrganizations.Columns[GrdAuditsOrganizations.Columns.Count - 1].Visible = false;
            GrdAuditsOrganizations.Columns[GrdAuditsOrganizations.Columns.Count - 3].Visible = false;
        }

        if (string.IsNullOrEmpty(Config._GetQueryString("p")))
        {
            Session[_FilterSessionName] = null;
        }
        if (!this.IsPostBack)
        {
            DlistRegionalCenter.DataSource = DALC.GetRegionalCenters();
            DlistRegionalCenter.DataBind();
            DlistRegionalCenter.Items.Insert(0, new ListItem("--", "-1"));
            DListSubOrganization.DataSource = DALC.GetOrganizationSub("-1");
            DListSubOrganization.DataBind();
            DListSubOrganization.Items.Insert(0, new ListItem("--", "-1"));
            DlistVisitType.DataSource = DALC.GetVisitTypes();
            DlistVisitType.DataBind();
            DlistVisitType.Items.Insert(0, new ListItem("--", "-1"));

            DlistRegionalCenterFilter.DataSource = DALC.GetRegionalCenters();
            DlistRegionalCenterFilter.DataBind();
            DlistRegionalCenterFilter.Items.Insert(0, new ListItem("--", "-1"));


            DListParentOrganizationFilter.DataSource = DALC.GetOrganizationTop();
            DListParentOrganizationFilter.DataBind();
            DListParentOrganizationFilter.Items.Insert(0, new ListItem("--", "-1"));

            DlistTopOrganization.DataSource = DALC.GetOrganizationTop();
            DlistTopOrganization.DataBind();
            DlistTopOrganization.Items.Insert(0, new ListItem("--", "-1"));


            DListTopOrganizationFilter_SelectedIndexChanged(null, null);
            DlistTopOrganization_SelectedIndexChanged(null, null);

            DlistVisitTypeFilter.DataSource = DALC.GetVisitTypes();
            DlistVisitTypeFilter.DataBind();
            DlistVisitTypeFilter.Items.Insert(0, new ListItem("--", "-1"));

            DlistUsers.DataSource = DALC.GetUsers();
            DlistUsers.DataBind();
            BindGrid();
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        //admin olub olmadigin yoxlayaq
        if (DALC._GetAdministratorsLogin.UsersStatusID != 30 && DALC._GetAdministratorsLogin.UsersStatusID != 25)
        {
            return;
        }

        if (DlistRegionalCenter.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Regional mərkəzi daxil edin.");
            ShowPopup();
            return;
        }
        if (DListSubOrganization.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Qurumu daxil edin.");
            ShowPopup();
            return;
        }
        if (DlistVisitType.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Başçəkmənin formasını daxil edin.");
            ShowPopup();
            return;
        }
        if (string.IsNullOrEmpty(TxtProblems.Text.Trim()))
        {
            Config.MsgBoxAjax("Aşkar edilmiş problemləri daxil edin.");
            ShowPopup();
            return;
        }
        if (string.IsNullOrEmpty(TxtSuggestion.Text.Trim()))
        {
            Config.MsgBoxAjax("Tövsiyəni daxil edin.");
            ShowPopup();
            return;
        }

        object DtVisit = TxtVisitDate.Text.DateTimeFormat();
        if (DtVisit == null)
        {
            Config.MsgBoxAjax("Baxışın həyata keçirildiyi tarixi düzgün seçin.");
            ShowPopup();
            return;
        }


        int result = -1;
        if (ViewState["operation"]._ToString() == "new")
        {
            result = DALC.InsertAuditsOrganizations(
                DALC._GetAdministratorsLogin.ID,
                DlistRegionalCenter.SelectedValue,
                DListSubOrganization.SelectedValue,
                DlistVisitType.SelectedValue,
                TxtProblems.Text,
                TxtSuggestion.Text,
                TxtDescription.Text, (DateTime)DtVisit);
        }

        else if (ViewState["operation"]._ToString() == "edit")
        {
            result = DALC.UpdateAuditOrganizations(ViewState["AuditsOrganizationsID"]._ToString(), DlistRegionalCenter.SelectedValue, DListSubOrganization.SelectedValue,
                                                   DlistVisitType.SelectedValue, TxtProblems.Text, TxtSuggestion.Text, TxtDescription.Text, (DateTime)DtVisit);
        }

        if (result > 0)
        {
            int AuditsOrganizationsID = ViewState["operation"]._ToString() == "new" ? result : ViewState["AuditsOrganizationsID"]._ToInt32();

            //Duzgun update oluna bilmesi ucun her ehtimala qarsi bu AuditsOrganizations-a aid butun userleri evvelce silib sonra tezden insert edek
            if (ViewState["operation"]._ToString() == "edit")
            {
                DALC.DeleteAuditsOrganizationsUsers(AuditsOrganizationsID);
            }

            if (DlistUsers.GetSelectedIndices().Count() > 0)
            {
                for (int i = 0; i <= (DlistUsers.Items.Count - 1); i++)
                {
                    if (DlistUsers.Items[i].Selected)
                    {
                        string SelectedUserId = DlistUsers.Items[i].Value;

                        DALC.InsertAuditsOrganizationsUsers(AuditsOrganizationsID, SelectedUserId._ToInt32());
                    }
                }
            }

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

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        var Dictionary = new Dictionary<string, object>()
        {
            {"RowIndex",TxtRowIndexFilter.Text},
            {"RegionalCentersID", DlistRegionalCenterFilter.SelectedValue},
            {"ParentOrganizationsID", DListParentOrganizationFilter.SelectedValue},
            {"OrganizationsID", DListSubOrganizationFilter.SelectedValue},
            {"VisitTypeID",  DlistVisitTypeFilter.SelectedValue},
            {"StartVisit_Dt", TxtVisitDtStartFilter.Text},
            {"EndVisit_Dt", TxtVisitDtEndFilter.Text},
            {"Descriptions", TxtDescriptionFilter.Text},
            {"DatePriority", DListDatePriority.SelectedValue},
            {"PageSize", DListPageSize.SelectedValue}
        };
        Session[_FilterSessionName] = Dictionary;
        BindGrid();
        Cache.Remove(_CountCacheName);
        Config.RedirectURL("/tools/AuditsOrganizations/?p=1");
    }

    protected void LnkEdit_Click(object sender, EventArgs e)
    {
        string DataID = (sender as LinkButton).CommandArgument;
        ClearForm();
        ViewState["operation"] = "edit";
        ShowPopup();
        BtnSave.Text = "Redaktə et";
        ViewState["AuditsOrganizationsID"] = DataID;
        DataTable DtDetails = DALC.GetAuditsOrganizationsById(DataID);
        if (DtDetails != null && DtDetails.Rows.Count > 0)
        {
            DlistRegionalCenter.SelectedValue = DtDetails._Rows("RegionalCentersID");
            DlistTopOrganization.SelectedValue = DtDetails._Rows("TopOrganizationID");
            DlistTopOrganization_SelectedIndexChanged(null, null);
            DListSubOrganization.SelectedValue = DtDetails._Rows("OrganizationsID");
            DlistVisitType.SelectedValue = DtDetails._Rows("VisitTypeID");

            TxtProblems.Text = DtDetails._Rows("Problems");
            TxtSuggestion.Text = DtDetails._Rows("Suggestions");
            TxtVisitDate.Text = DateTime.Parse(DtDetails._Rows("Visit_Dt")).ToString("dd.MM.yyyy");
            TxtDescription.Text = DtDetails._Rows("Descriptions");

            //elave olunmus userleri secek
            DataTable DtUsers = DALC.GetUsersForAuditOrganizations(int.Parse(DataID));
            if (DtUsers != null && DtUsers.Rows.Count > 0)
            {
                for (int i = 0; i < DtUsers.Rows.Count; i++)
                {
                    DlistUsers.Items[DlistUsers.Items.IndexOf(DlistUsers.Items.FindByValue(DtUsers.Rows[i]["UsersID"]._ToString()))].Selected = true;
                }
            }
        }
    }

    protected void LnkDetails_Click(object sender, EventArgs e)
    {
        string DataID = (sender as LinkButton).CommandArgument;

        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "JqueryModal", "$('#modalDetails').modal('show');", true);
        DataTable DtDetails = DALC.GetAuditsOrganizationsById(DataID);
        if (DtDetails != null && DtDetails.Rows.Count > 0)
        {
            LblRegionalCenter.Text = DtDetails._Rows("RegionalCenter");
            LblOrganization.Text = DtDetails._Rows("Organization");
            LblVisitType.Text = DtDetails._Rows("VisitType");
            LblProblems.Text = DtDetails._Rows("Problems");
            LblSuggestion.Text = DtDetails._Rows("Suggestions");
            LblDescription.Text = DtDetails._Rows("Descriptions");
            LblVisitDate.Text = DateTime.Parse(DtDetails._Rows("Visit_Dt")).ToString("dd.MM.yyyy");

            LblUsers.Text = "";
            //
            for (int i = 0; i < DlistUsers.Items.Count; i++)
            {
                DlistUsers.Items[i].Selected = false;
            }
            //elave olunmus userleri secek
            DataTable DtUsers = DALC.GetUsersForAuditOrganizations(int.Parse(DataID));
            if (DtUsers != null && DtUsers.Rows.Count > 0)
            {
                for (int i = 0; i < DtUsers.Rows.Count; i++)
                {
                    LblUsers.Text += DlistUsers.Items[DlistUsers.Items.IndexOf(DlistUsers.Items.FindByValue(DtUsers.Rows[i]["UsersID"]._ToString()))].Text + ", ";

                }
                LblUsers.Text = LblUsers.Text.TrimEnd(' ').TrimEnd(',');
            }
        }
    }

    protected void LnkExportExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment; filename = BaxisKecirilenQurumlar.xls");
        Response.ContentType = "application/vnd.xls";
        StringWriter stringWrite = new StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        GrdAuditsOrganizations.Columns[GrdAuditsOrganizations.Columns.Count - 1].Visible = false;
        GrdAuditsOrganizations.Columns[GrdAuditsOrganizations.Columns.Count - 2].Visible = false;
        GrdAuditsOrganizations.Columns[GrdAuditsOrganizations.Columns.Count - 3].Visible = false;
        GrdAuditsOrganizations.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
        GrdAuditsOrganizations.Columns[GrdAuditsOrganizations.Columns.Count - 1].Visible = false;
        GrdAuditsOrganizations.Columns[GrdAuditsOrganizations.Columns.Count - 2].Visible = false;
        GrdAuditsOrganizations.Columns[GrdAuditsOrganizations.Columns.Count - 3].Visible = false;
    }

    protected void DListTopOrganizationFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = DALC.GetOrganizationSub(DListParentOrganizationFilter.SelectedValue);

        DListSubOrganizationFilter.DataSource = DALC.GetOrganizationSub(DListParentOrganizationFilter.SelectedValue);
        DListSubOrganizationFilter.DataBind();
        DListSubOrganizationFilter.Items.Insert(0, new ListItem("--", "-1"));

    }

    protected void DlistTopOrganization_SelectedIndexChanged(object sender, EventArgs e)
    {
        DListSubOrganization.DataSource = DALC.GetOrganizationSub(DlistTopOrganization.SelectedValue);
        DListSubOrganization.DataBind();
        DListSubOrganization.Items.Insert(0, new ListItem("--", "-1"));
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        var Dictionary = new Dictionary<string, object>()
        {
            {"RowIndex",""},
            {"RegionalCentersID", "-1"},
            {"ParentOrganizationsID", "-1"},
            {"OrganizationsID", "-1"},
            {"VisitTypeID",  "-1"},
            {"StartVisit_Dt", ""},
            {"EndVisit_Dt", ""},
            {"Descriptions", ""},
            {"DatePriority", "10"},
            {"PageSize", "10"}
        };

        Session[_FilterSessionName] = Dictionary;
        Config.RedirectURL("/tools/AuditsOrganizations/?p=1");
    }
}