using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Tools_Controls_AuditsOrganizationsMeetingPersons : System.Web.UI.UserControl
{
    string _CountCacheName = "AuditsOrganizationsMeetingPersons";
    string _FilterSessionName = "AuditsOrganizationsMeetingPersons";

    void BindDList()
    {
        DListTopOrganizationFilter.DataSource = DALC.GetOrganizationTop();
        DListTopOrganizationFilter.DataBind();
        DListTopOrganizationFilter.Items.Insert(0, new ListItem("--", "-1"));
        DListTopOrganizationFilter_SelectedIndexChanged(null, null);

        DListGenderType.DataSource = DALC.GetGenderType();
        DListGenderType.DataBind();
        DListGenderType.Items.Insert(0, new ListItem("--", "-1"));

        DListSocialStatus.DataSource = DALC.GetSocialStaus();
        DListSocialStatus.DataBind();
        DListSocialStatus.Items.Insert(0, new ListItem("--", "-1"));

        DListGenderTypeFilter.DataSource = DALC.GetGenderType();
        DListGenderTypeFilter.DataBind();
        DListGenderTypeFilter.Items.Insert(0, new ListItem("--", "-1"));

        DListSocialStatusFilter.DataSource = DALC.GetSocialStaus();
        DListSocialStatusFilter.DataBind();
        DListSocialStatusFilter.Items.Insert(0, new ListItem("--", "-1"));

        DListComplaintResultType.DataSource = DALC.GetComplaintResultType();
        DListComplaintResultType.DataBind();
        DListComplaintResultType.Items.Insert(0, new ListItem("--", "-1"));

        DListComplaintResultTypeFilter.DataSource = DALC.GetComplaintResultType();
        DListComplaintResultTypeFilter.DataBind();
        DListComplaintResultTypeFilter.Items.Insert(0, new ListItem("--", "-1"));

    }

    void BindGrid()
    {
        if (Session[_FilterSessionName] != null)
        {
            var DictionarySession = (Dictionary<string, object>)Session[_FilterSessionName];
            if (DictionarySession.Count != 0)
            {
                TxtRowIndexFilter.Text = DictionarySession["RowIndex"]._ToString();
                //TxtComplaintNumbersFilter.Text = DictionarySession["ComplaintNumbers"]._ToString();
                DListTopOrganizationFilter.SelectedValue = DictionarySession["ParentOrganizationsID"]._ToString();
                DListTopOrganizationFilter_SelectedIndexChanged(null, null);
                DListSubOrganizationFilter.SelectedValue = DictionarySession["OrganizationsID"]._ToString();
                TxtFullnameFilter.Text = DictionarySession["Fullname"]._ToString();
                DListGenderTypeFilter.SelectedValue = DictionarySession["GenderTypeID"]._ToString();
                DListSocialStatusFilter.SelectedValue = DictionarySession["SocialStatusID"]._ToString();
                TxtPlaceFilter.Text = DictionarySession["Place"]._ToString();
                TxtAccusedArticlesFilter.Text = DictionarySession["AccusedArticles"]._ToString();
                TxtStartPunishmentPeriodFilter.Text = DictionarySession["StartPunishmentPeriod"]._ToString();
                TxtEndPunishmentPeriodFilter.Text = DictionarySession["EndPunishmentPeriod"]._ToString();
                TxtProblemsFilter.Text = DictionarySession["Problems"]._ToString();
                DListComplaintResultTypeFilter.SelectedValue = DictionarySession["ComplaintResultTypeID"]._ToString();
                TxtResultsFilter.Text = DictionarySession["Results"]._ToString();
                TxtMeetingStartDtFilter.Text = DictionarySession["StartMeeting_Dt"]._ToString();
                TxtMeetingEndDtFilter.Text = DictionarySession["EndMeeting_Dt"]._ToString();
                DListDatePriority.SelectedValue = DictionarySession["DatePriority"]._ToString();
                DListPageSize.SelectedValue = DictionarySession["PageSize"]._ToString();
            }
        }

        #region BetweenDateFromat
        string DatePunishmentPeriod = "";

        string DatePunishmentPeriod1 = "19000101";
        string DatePunishmentPeriod2 = DateTime.Now.ToString("yyyyMMdd");


        object DtStartPunishmentPeriod = Config.DateTimeFormat(TxtStartPunishmentPeriodFilter.Text.Trim());
        object DtEndPunishmentPeriod = Config.DateTimeFormat(TxtEndPunishmentPeriodFilter.Text.Trim());

        if (DtStartPunishmentPeriod == null && DtEndPunishmentPeriod == null)
        {
            DatePunishmentPeriod = "";
        }
        else
        {
            if (DtStartPunishmentPeriod != null)
            {
                DatePunishmentPeriod1 = ((DateTime)DtStartPunishmentPeriod).ToString("yyyyMMdd");
            }

            if (DtEndPunishmentPeriod != null)
            {
                DatePunishmentPeriod2 = ((DateTime)DtEndPunishmentPeriod).ToString("yyyyMMdd");
            }

            DatePunishmentPeriod = DatePunishmentPeriod1 + "&" + DatePunishmentPeriod2;
        }


        string Date = "";

        string Dt1 = "19000101";
        string Dt2 = DateTime.Now.ToString("yyyyMMdd");

        object DateFilter1 = Config.DateTimeFormat(TxtMeetingStartDtFilter.Text.Trim());
        object DateFilter2 = Config.DateTimeFormat(TxtMeetingEndDtFilter.Text.Trim());

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

        var Dictionary = new Dictionary<string, object>()
        {
            {"AuditsOrganizationsID", Config._GetQueryString("i")},
           //{"ComplaintNumbers",TxtComplaintNumbersFilter.Text},
            {"ParentOrganizationsID",DListTopOrganizationFilter.SelectedValue},
            {"OrganizationsID",int.Parse(DListSubOrganizationFilter.SelectedValue)},
            {"(LIKE)Fullname",TxtFullnameFilter.Text},
            {"GenderTypeID",int.Parse(DListGenderTypeFilter.SelectedValue)},
            {"SocialStatusID",int.Parse(DListSocialStatusFilter.SelectedValue)},
            {"Place", TxtPlaceFilter.Text},
            {"AccusedArticles", TxtAccusedArticlesFilter.Text},
            {"StartPunishmentPeriod(BETWEEN)",  DatePunishmentPeriod},
            {"EndPunishmentPeriod(BETWEEN)", DatePunishmentPeriod},
            {"(LIKE)Problems", TxtProblemsFilter.Text},
            {"ComplaintResultTypeID", DListComplaintResultTypeFilter.SelectedValue},
            {"(LIKE)Results", TxtResultsFilter.Text},
            {"Meeting_Dt(BETWEEN)", Date}
        };

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

        DALC.DataTableResult ApplicationsList = DALC.GetMeetingPersons(Dictionary, PageNum, RowNumber, new object[] { RowIndex }, OrderByType);

        if (ApplicationsList.Count == -1)
        {
            return;
        }

        if (ApplicationsList.Dt.Rows.Count < 1 && PageNum > 1)
        {
            if (!string.IsNullOrEmpty(Config._GetQueryString("i")))
            {
                Config.RedirectURL(string.Format("/tools/auditsorganizationsmeetingpersons/?p={0}&i={1}", PageNum - 1, Config._GetQueryString("i")));
            }
            else
            {
                Config.RedirectURL(string.Format("/tools/auditsorganizationsmeetingpersons/?p={0}", PageNum - 1));
            }
        }

        int Total_Count = ApplicationsList.Count % RowNumber > 0 ? (ApplicationsList.Count / RowNumber) + 1 : ApplicationsList.Count / RowNumber;
        HdnTotalCount.Value = Total_Count.ToString();

        PnlPager.Visible = ApplicationsList.Count > RowNumber;

        GrdAuditsOrganizationsMeetingPersons.DataSource = ApplicationsList.Dt;
        GrdAuditsOrganizationsMeetingPersons.DataBind();

        LblCountInfo.Text = string.Format("Axtarış üzrə nəticə: {0}", ApplicationsList.Count);
    }

    void ShowPopup()
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "JqueryModal", "$('#modalNewMeetingPerson').modal('show');", true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //eger emeliyatci ve ya istifadeci deyilse 
        if (DALC._GetAdministratorsLogin.UsersStatusID != 30 && DALC._GetAdministratorsLogin.UsersStatusID != 25)
        {
            LnkNew.Visible = false;
        }
        if (string.IsNullOrEmpty(Config._GetQueryString("p")))
        {
            Session[_FilterSessionName] = null;
        }
        if (string.IsNullOrEmpty(Config._GetQueryString("i")))
        {
            LnkNew.Visible = false;
            PnlAuditDetails.Visible = false;
        }
        if (!string.IsNullOrEmpty(Config._GetQueryString("i")))
        {
            DataTable DtDetails = DALC.GetAuditsOrganizationsById(Config._GetQueryString("i"));
            if (DtDetails != null && DtDetails.Rows.Count > 0)
            {
                LblRegionalCenter.Text = DtDetails._Rows("RegionalCenter");
                LblOrganization.Text = DtDetails._Rows("Organization");
                LblVisitType.Text = DtDetails._Rows("VisitType");
                LblProblems.Text = DtDetails._Rows("Problems");
                LblSuggestion.Text = DtDetails._Rows("Suggestions");
                LblDescription.Text = DtDetails._Rows("Descriptions");
                ViewState["MeetingDt"] = TxtMeetingPersonMeetingDt.Text = LblVisitDate.Text = DateTime.Parse(DtDetails._Rows("Visit_Dt")).ToString("dd.MM.yyyy");
            }
        }
        if (!IsPostBack)
        {
            BindDList();
            BindGrid();

        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        var Dictionary = new Dictionary<string, object>()
        {
            {"RowIndex",TxtRowIndexFilter.Text},
            {"AuditsOrganizationsID", Config._GetQueryString("i")},
            //{"ComplaintNumbers",TxtComplaintNumbersFilter.Text},
            {"ParentOrganizationsID",DListTopOrganizationFilter.SelectedValue},
            {"OrganizationsID",DListSubOrganizationFilter.SelectedValue},
            {"Fullname",TxtFullnameFilter.Text},
            {"GenderTypeID",int.Parse(DListGenderTypeFilter.SelectedValue)},
            {"SocialStatusID",int.Parse(DListSocialStatusFilter.SelectedValue)},
            {"Place", TxtPlaceFilter.Text},
            {"AccusedArticles", TxtAccusedArticlesFilter.Text},
            {"StartPunishmentPeriod",  TxtStartPunishmentPeriodFilter.Text},
            {"EndPunishmentPeriod", TxtEndPunishmentPeriodFilter.Text},
            {"Problems", TxtProblemsFilter.Text},
            {"ComplaintResultTypeID", DListComplaintResultTypeFilter.SelectedValue},
            {"Results", TxtResultsFilter.Text},
            {"StartMeeting_Dt", TxtMeetingStartDtFilter.Text},
            {"EndMeeting_Dt", TxtMeetingEndDtFilter.Text},
            {"DatePriority", DListDatePriority.SelectedValue},
            {"PageSize", DListPageSize.SelectedValue}
        };

        Session[_FilterSessionName] = Dictionary;
        BindGrid();
        Cache.Remove(_CountCacheName);
        if (!string.IsNullOrEmpty(Config._GetQueryString("i")))
        {
            Config.RedirectURL("/tools/auditsorganizationsmeetingpersons/?p=1&i=" + Config._GetQueryString("i"));
        }
        else
        {
            Config.RedirectURL("/tools/auditsorganizationsmeetingpersons/?p=1");
        }
    }

    protected void BtnSaveMeetingPerson_Click(object sender, EventArgs e)
    {
        //Eger istifadeci ve ya emeliyyatci deyilse
        if (DALC._GetAdministratorsLogin.UsersStatusID != 30 && DALC._GetAdministratorsLogin.UsersStatusID != 25)
        {
            return;
        }

        if (BtnSaveMeetingPerson.CommandName == "add" && string.IsNullOrEmpty(Config._GetQueryString("i")))
        {
            return;
        }

        //if (string.IsNullOrEmpty(TxtMeetinPersonComplaintNumber.Text.Trim()))
        //{
        //    Config.MsgBoxAjax("Şikayət nömrəsini daxil edin.");
        //    ShowPopup();
        //    return;
        //}

        if (string.IsNullOrEmpty(TxtMeetingPersonFullname.Text.Trim()))
        {
            Config.MsgBoxAjax("Görüşülən şəxsin soyadını, adını və ata adını daxil edin.");
            ShowPopup();
            return;
        }

        if (DListGenderType.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Görüşülən şəxsin cinsini daxil edin.");
            ShowPopup();
            return;
        }

        if (string.IsNullOrEmpty(TxtMeetingPersonPlace.Text.Trim()))
        {
            Config.MsgBoxAjax("Görüşülən şəxsin saxlandığı yeri daxil edin.");
            ShowPopup();
            return;
        }

        if (string.IsNullOrEmpty(TxtMeetingPersonAccusedArticle.Text.Trim()))
        {
            Config.MsgBoxAjax("Görüşülən şəxsin təqsirləndirildiyi maddəni daxil edin.");
            ShowPopup();
            return;
        }

        object DtStartPunishmentPeriod = DBNull.Value;
        if (!string.IsNullOrEmpty(TxtMeetingPersonStartPunishmentPeriod.Text.Trim()))
        {
            DtStartPunishmentPeriod = TxtMeetingPersonStartPunishmentPeriod.Text.DateTimeFormat();
            if (DtStartPunishmentPeriod == null)
            {
                Config.MsgBoxAjax("Saxlanma tarixini (başlanğıc) düzgün daxil edin.");
                ShowPopup();
                return;
            }
        }

        object DtEndPunishmentPeriod = DBNull.Value;
        if (!string.IsNullOrEmpty(TxtMeetingPersonEndPunishmentPeriod.Text.Trim()))
        {
            DtEndPunishmentPeriod = TxtMeetingPersonEndPunishmentPeriod.Text.DateTimeFormat();
            if (DtStartPunishmentPeriod == null)
            {
                Config.MsgBoxAjax("Saxlanma tarixini (son) düzgün daxil edin.");
                ShowPopup();
                return;
            }
        }

        if (string.IsNullOrEmpty(TxtMeetingPersonProblem.Text.Trim()))
        {
            Config.MsgBoxAjax("Problemi daxil edin.");
            ShowPopup();
            return;
        }

        //if (string.IsNullOrEmpty(TxtMeetingPersonsResult.Text.Trim()))
        //{
        //    Config.MsgBoxAjax("Nəticəni daxil edin.");
        //    ShowPopup();
        //    return;
        //}

        object DtMeetingDt = null;
        if (BtnSaveMeetingPerson.CommandName == "add")
        {
            DtMeetingDt = ViewState["MeetingDt"]._ToString().DateTimeFormat();
            if (DtMeetingDt == null)
            {
                Config.MsgBoxAjax("Görüşün tarixini düzgün daxil edin.");
                ShowPopup();
                return;
            }
        }


        Dictionary<string, object> parameters = new Dictionary<string, object>();


        //parameters.Add("ComplaintNumbers", TxtMeetinPersonComplaintNumber.Text);
        parameters.Add("Fullname", TxtMeetingPersonFullname.Text);
        parameters.Add("GenderTypeID", int.Parse(DListGenderType.SelectedValue));

        if (DListSocialStatus.SelectedValue == "-1")
            parameters.Add("SocialStatusID", DBNull.Value);
        else
            parameters.Add("SocialStatusID", int.Parse(DListSocialStatus.SelectedValue));

        parameters.Add("Place", TxtMeetingPersonPlace.Text);
        parameters.Add("AccusedArticles", TxtMeetingPersonAccusedArticle.Text);
        parameters.Add("StartPunishmentPeriod", DtStartPunishmentPeriod);
        parameters.Add("EndPunishmentPeriod", DtEndPunishmentPeriod);
        parameters.Add("Problems", TxtMeetingPersonProblem.Text);

        if (DListComplaintResultType.SelectedValue == "-1")
            parameters.Add("ComplaintResultTypeID", DBNull.Value);
        else
            parameters.Add("ComplaintResultTypeID", int.Parse(DListComplaintResultType.SelectedValue));

        parameters.Add("Results", TxtMeetingPersonsResult.Text);

        int result;
        if (BtnSaveMeetingPerson.CommandName == "add")
        {
            parameters.Add("AuditsOrganizationsID", Config._GetQueryString("i"));
            parameters.Add("IsDeleted", false);
            parameters.Add("Meeting_Dt", (DateTime)DtMeetingDt);
            parameters.Add("Add_Dt", DateTime.Now);
            parameters.Add("Add_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger());
            result = DALC.InsertDatabase("AuditsOrganizationsMeetingPersons", parameters);
        }
        else
        {
            parameters.Add("WhereID", int.Parse(BtnSaveMeetingPerson.CommandArgument));
            result = DALC.UpdateDatabase("AuditsOrganizationsMeetingPersons", parameters);
        }

        if (result > 0)
        {
            Config.MsgBoxAjax("Məlumatlar qeydə alındı.", true);
            BindGrid();
        }
        else
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages, false);
        }
    }

    protected void LnkNew_Click(object sender, EventArgs e)
    {
        //TxtMeetinPersonComplaintNumber.Text = "";
        TxtMeetingPersonFullname.Text = "";
        DListGenderType.SelectedIndex = DListSocialStatus.SelectedIndex = DListComplaintResultType.SelectedIndex = 0;
        TxtMeetingPersonPlace.Text = "";
        TxtMeetingPersonAccusedArticle.Text = "";
        TxtMeetingPersonStartPunishmentPeriod.Text = "";
        TxtMeetingPersonEndPunishmentPeriod.Text = "";
        TxtMeetingPersonProblem.Text = "";
        TxtMeetingPersonsResult.Text = "";
        BtnSaveMeetingPerson.CommandName = "add";
        ShowPopup();
    }

    protected void LnkExportExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment; filename = GorusulenSexsler.xls");
        Response.ContentType = "application/vnd.xls";
        StringWriter stringWrite = new StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        GrdAuditsOrganizationsMeetingPersons.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }

    protected void DListTopOrganizationFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        DListSubOrganizationFilter.DataSource = DALC.GetOrganizationSub(DListTopOrganizationFilter.SelectedValue);
        DListSubOrganizationFilter.DataBind();
        DListSubOrganizationFilter.Items.Insert(0, new ListItem("--", "-1"));
    }

    protected void LnkEdit_Click(object sender, EventArgs e)
    {
        DataTable DtPersons = DALC.GetAuditsOrganizationsMeetingPersonsByID(int.Parse((sender as LinkButton).CommandArgument));
        if (DtPersons == null || DtPersons.Rows.Count < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        BtnSaveMeetingPerson.CommandArgument = (sender as LinkButton).CommandArgument;
        BtnSaveMeetingPerson.CommandName = "edit";

        //TxtMeetinPersonComplaintNumber.Text = DtPersons._Rows("ComplaintNumbers");
        TxtMeetingPersonFullname.Text = DtPersons._Rows("Fullname");
        DListGenderType.SelectedValue = string.IsNullOrEmpty(DtPersons._Rows("GenderTypeID")) ? "-1" : DtPersons._Rows("GenderTypeID");
        DListSocialStatus.SelectedValue = string.IsNullOrEmpty(DtPersons._Rows("SocialStatusID")) ? "-1" : DtPersons._Rows("SocialStatusID");
        TxtMeetingPersonPlace.Text = DtPersons._Rows("Place");
        TxtMeetingPersonAccusedArticle.Text = DtPersons._Rows("AccusedArticles");

        TxtMeetingPersonStartPunishmentPeriod.Text = "";
        TxtMeetingPersonEndPunishmentPeriod.Text = "";

        if (!string.IsNullOrEmpty(DtPersons._Rows("StartPunishmentPeriod")))
        {
            TxtMeetingPersonStartPunishmentPeriod.Text = ((DateTime)DtPersons._RowsObject("StartPunishmentPeriod")).ToString("dd.MM.yyyy");
        }

        if (!string.IsNullOrEmpty(DtPersons._Rows("EndPunishmentPeriod")))
        {
            TxtMeetingPersonEndPunishmentPeriod.Text = ((DateTime)DtPersons._RowsObject("EndPunishmentPeriod")).ToString("dd.MM.yyyy");
        }


        TxtMeetingPersonProblem.Text = DtPersons._Rows("Problems");
        DListComplaintResultType.SelectedValue = string.IsNullOrEmpty(DtPersons._Rows("ComplaintResultTypeID")) ? "-1" : DtPersons._Rows("ComplaintResultTypeID");
        TxtMeetingPersonsResult.Text = DtPersons._Rows("Results");
        TxtMeetingPersonMeetingDt.Text = ((DateTime)DtPersons._RowsObject("Meeting_Dt")).ToString("dd.MM.yyyy");

        ShowPopup();
    }

    protected void LnkDelete_Click(object sender, EventArgs e)
    {
        if (DALC.UpdateDatabase("AuditsOrganizationsMeetingPersons", new string[] { "IsDeleted", "WhereID" }, new object[] { true, ((LinkButton)sender).CommandArgument }) < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages, false);
            return;
        }
        BindGrid();
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        var Dictionary = new Dictionary<string, object>()
        {
            {"RowIndex",""},
            {"AuditsOrganizationsID", Config._GetQueryString("i")},
            {"ComplaintNumbers",""},
            {"ParentOrganizationsID",-1},
            {"OrganizationsID",-1},
            {"Fullname",""},
            {"GenderTypeID",-1},
            {"SocialStatusID",-1},
            {"Place", ""},
            {"AccusedArticles", ""},
            {"StartPunishmentPeriod",  ""},
            {"EndPunishmentPeriod", ""},
            {"Problems", ""},
            {"ComplaintResultTypeID", -1},
            {"Results", ""},
            {"StartMeeting_Dt", ""},
            {"EndMeeting_Dt", ""},
            {"DatePriority", "10"},
            {"PageSize", "10"}
        };

        Session[_FilterSessionName] = Dictionary;
        Config.RedirectURL("/tools/auditsorganizationsmeetingpersons/?p=1");
    }
}