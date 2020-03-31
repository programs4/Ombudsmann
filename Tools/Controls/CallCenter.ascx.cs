using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_Controls_CallCenter : System.Web.UI.UserControl
{
    string _CountCacheName = "CallCenter";
    string _FilterSessionName = "CallCenter";

    void BindDList()
    {
        //DListCallCenterType.DataSource = DALC.GetCallCenterType();
        //DListCallCenterType.DataBind();
        //DListCallCenterType.Items.Insert(0, new ListItem("--", "-1"));

        //DlistCallCenterTypeFilter.DataSource = DALC.GetCallCenterType();
        //DlistCallCenterTypeFilter.DataBind();
        //DlistCallCenterTypeFilter.Items.Insert(0, new ListItem("--", "-1"));

        DListComplaintType.DataSource = DALC.GetComplaintType();
        DListComplaintType.DataBind();
        DListComplaintType.Items.Insert(0, new ListItem("--", "-1"));

        DListApplicantGenderType.DataSource = DALC.GetGenderType();
        DListApplicantGenderType.DataBind();
        DListApplicantGenderType.Items.Insert(0, new ListItem("--", "-1"));

        DListApplicantSocialStatus.DataSource = DALC.GetSocialStaus();
        DListApplicantSocialStatus.DataBind();
        DListApplicantSocialStatus.Items.Insert(0, new ListItem("--", "-1"));

        DListComplaintResultType.DataSource = DALC.GetComplaintResultType();
        DListComplaintResultType.DataBind();
        DListComplaintResultType.Items.Insert(0, new ListItem("--", "-1"));

        DListComplaintTypeFilter.DataSource = DALC.GetComplaintType();
        DListComplaintTypeFilter.DataBind();
        DListComplaintTypeFilter.Items.Insert(0, new ListItem("--", "-1"));

        DListApplicantGenderTypeFilter.DataSource = DALC.GetGenderType();
        DListApplicantGenderTypeFilter.DataBind();
        DListApplicantGenderTypeFilter.Items.Insert(0, new ListItem("--", "-1"));

        DListApplicantSocialStatusFilter.DataSource = DALC.GetSocialStaus();
        DListApplicantSocialStatusFilter.DataBind();
        DListApplicantSocialStatusFilter.Items.Insert(0, new ListItem("--", "-1"));

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
                //DlistCallCenterTypeFilter.SelectedValue = DictionarySession["CallCenterTypeID"]._ToString();
                DListComplaintTypeFilter.SelectedValue = DictionarySession["ComplaintTypeID"]._ToString();
                TxtApplicantFullnameFilter.Text = DictionarySession["ApplicantFullname"]._ToString();
                DListApplicantGenderTypeFilter.SelectedValue = DictionarySession["GenderTypeID"]._ToString();
                DListApplicantSocialStatusFilter.SelectedValue = DictionarySession["SocialStatusID"]._ToString();
                TxtPhoneNumberFilter.Text = DictionarySession["PhoneNumber"]._ToString();
                TxtVictimsFullnameFilter.Text = DictionarySession["VictimsFullname"]._ToString();
                DListComplaintResultTypeFilter.SelectedValue = DictionarySession["ComplaintResultTypeID"]._ToString();
                TxtResultsFilter.Text = DictionarySession["Results"]._ToString();
                TxtCallDtStartFilter.Text = DictionarySession["StartCall_Dt"]._ToString();
                TxtCallDtEndFilter.Text = DictionarySession["EndCall_Dt"]._ToString();
                TxtStartPunishmentPeriodFilter.Text = DictionarySession["StartPunishmentPeriod"]._ToString();
                TxtEndPunishmentPeriodFilter.Text = DictionarySession["EndPunishmentPeriod"]._ToString();
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

        object DateFilter1 = Config.DateTimeFormat(TxtCallDtStartFilter.Text.Trim());
        object DateFilter2 = Config.DateTimeFormat(TxtCallDtEndFilter.Text.Trim());

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
           // {"CallCenterTypeID",int.Parse(DlistCallCenterTypeFilter.SelectedValue)},
            {"ComplaintTypeID",int.Parse(DListComplaintTypeFilter.SelectedValue)},
            {"(LIKE)ApplicantFullname",TxtApplicantFullnameFilter.Text},
            {"GenderTypeID",int.Parse(DListApplicantGenderTypeFilter.SelectedValue)},
            {"SocialStatusID",int.Parse(DListApplicantSocialStatusFilter.SelectedValue)},
            {"PhoneNumber",TxtPhoneNumberFilter.Text},
            {"VictimsFullname", TxtVictimsFullnameFilter.Text},
            {"ComplaintResultTypeID", DListComplaintResultTypeFilter.SelectedValue},
            {"(LIKE)Results", TxtResultsFilter.Text},
            {"Call_Dt(BETWEEN)",  Date},
            {"StartPunishmentPeriod(BETWEEN)", DatePunishmentPeriod},
            {"EndPunishmentPeriod(BETWEEN)", DatePunishmentPeriod}
        };

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

        string OrderByType = "desc";

        if (DListDatePriority.SelectedValue == "20")
        {
            OrderByType = "asc";
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

        DALC.DataTableResult ApplicationsList = DALC.GetCallCenter(Dictionary, PageNum, RowNumber, new object[] { RowIndex }, OrderByType);

        if (ApplicationsList.Count == -1)
        {
            return;
        }

        if (ApplicationsList.Dt.Rows.Count < 1 && PageNum > 1)
        {
            Config.RedirectURL(string.Format("/tools/CallCenter/?p={0}", PageNum - 1));
        }

        int Total_Count = ApplicationsList.Count % RowNumber > 0 ? (ApplicationsList.Count / RowNumber) + 1 : ApplicationsList.Count / RowNumber;
        HdnTotalCount.Value = Total_Count.ToString();

        PnlPager.Visible = ApplicationsList.Count > RowNumber;

        GrdCallCenter.DataSource = ApplicationsList.Dt;
        GrdCallCenter.DataBind();

        LblCountInfo.Text = string.Format("Axtarış üzrə nəticə: {0}", ApplicationsList.Count);
    }

    void ShowPopup()
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "JqueryModal", "$('#modalNewCallCenter').modal('show');", true);
    }

    void ClearForm()
    {
        //DListCallCenterType.SelectedIndex = 
        DListComplaintType.SelectedIndex =
        DListApplicantGenderType.SelectedIndex =
        DListApplicantSocialStatus.SelectedIndex =
        DListComplaintResultType.SelectedIndex = 0;
        TxtApplicantFullname.Text = "";
        TxtPhoneNumber.Text = "";
        TxtCallDtDate.Text = "";
        TxtCallDtTime.Text = "";
        TxtVictimsFullname.Text = "";
        TxtComplaintInstitution.Text = "";
        TxtStartPunishmentPeriod.Text = "";
        TxtEndPunishmentPeriod.Text = "";
        TxtResults.Text = "";
        TxtDescriptions.Text = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //eger istifadeci veya emeliyyatci deyilse
        if (DALC._GetAdministratorsLogin.UsersStatusID != 30 && DALC._GetAdministratorsLogin.UsersStatusID != 25)
        {
            LnkNew.Visible = false;
            GrdCallCenter.Columns[GrdCallCenter.Columns.Count - 1].Visible = false;
        }

        if (string.IsNullOrEmpty(Config._GetQueryString("p")))
        {
            Session[_FilterSessionName] = null;
        }
        if (!this.IsPostBack)
        {
            BindDList();
            BindGrid();
        }

    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        //eger istifadeci veya emeliyyatci deyilse
        if (DALC._GetAdministratorsLogin.UsersStatusID != 30 && DALC._GetAdministratorsLogin.UsersStatusID != 25)
        {
            return;
        }

        //if (DListCallCenterType.SelectedValue == "-1")
        //{
        //    Config.MsgBoxAjax("Müraciətin kateqoriyasını daxil edin.");
        //    ShowPopup();
        //    return;
        //}

        if (DListComplaintType.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Şikayətin növünü daxil edin.");
            ShowPopup();
            return;
        }

        if (string.IsNullOrEmpty(TxtApplicantFullname.Text.Trim()))
        {
            Config.MsgBoxAjax("Müraciət edənin soyadı, adı və atasının adını daxil edin.");
            ShowPopup();
            return;
        }

        if (DListApplicantGenderType.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Müraciət edənin cinsini daxil edin.");
            ShowPopup();
            return;
        }

        if (string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim()))
        {
            Config.MsgBoxAjax("Mobil nömrəni daxil edin.");
            ShowPopup();
            return;
        }

        object DtCallDtDate = TxtCallDtDate.Text.DateTimeFormat();

        if (DtCallDtDate == null)
        {
            Config.MsgBoxAjax("Zəng tarixini düzgün daxil edin.");
            ShowPopup();
            return;
        }

        TxtCallDtTime.Text = Config.DateTimeClear(TxtCallDtTime.Text, ":");

        DateTime DtCallDtTime;
        bool IsValidCallTime = DateTime.TryParseExact(TxtCallDtTime.Text, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DtCallDtTime);
        if (!IsValidCallTime)
        {
            Config.MsgBoxAjax("Zəng saatını düzgün daxil edin.");
            ShowPopup();
            return;
        }

        if (string.IsNullOrEmpty(TxtVictimsFullname.Text.Trim()))
        {
            Config.MsgBoxAjax("Müraciət kimin barəsində olduğunu daxil edin.");
            ShowPopup();
            return;
        }


        if (string.IsNullOrEmpty(TxtComplaintInstitution.Text.Trim()))
        {
            Config.MsgBoxAjax("Şikayət olunan qurumu daxil edin.");
            ShowPopup();
            return;
        }


        object DtStartPunishmentPeriod = TxtStartPunishmentPeriod.Text.DateTimeFormat();
        if (DtStartPunishmentPeriod == null)
        {
            Config.MsgBoxAjax("Saxlanma tarixini (başlanğıc) düzgün daxil edin.");
            ShowPopup();
            return;
        }

        object DtEndPunishmentPeriod = null;
        //dt Saxlanma tarixi (bitis)
        if (!string.IsNullOrEmpty(TxtEndPunishmentPeriod.Text.Trim()))
        {
            DtEndPunishmentPeriod = TxtEndPunishmentPeriod.Text.DateTimeFormat();
            if (DtStartPunishmentPeriod == null)
            {
                Config.MsgBoxAjax("Saxlanma tarixini (son) düzgün daxil edin.");
                ShowPopup();
                return;
            }
        }


        //if (string.IsNullOrEmpty(TxtResults.Text.Trim()))
        //{
        //    Config.MsgBoxAjax("Nəticəni daxil edin.");
        //    ShowPopup();
        //    return;
        //}

        int result = -1;

        //CallDt-deki tarix ve saati birlesdire
        DateTime DtCallDt = DateTime.ParseExact(((DateTime)DtCallDtDate).ToString("dd.MM.yyyy") + " " + DtCallDtTime.ToString("HH:mm"), "dd.MM.yyyy HH:mm",
                                                CultureInfo.InvariantCulture);

        Dictionary<string, object> parameters = new Dictionary<string, object>();
        //parameters.Add("CallCenterTypeID", int.Parse(DListCallCenterType.SelectedValue));
        parameters.Add("ComplaintTypeID", int.Parse(DListComplaintType.SelectedValue));
        parameters.Add("ApplicantFullname", TxtApplicantFullname.Text);
        parameters.Add("GenderTypeID", int.Parse(DListApplicantGenderType.SelectedValue));

        if (DListApplicantSocialStatus.SelectedValue == "-1")
            parameters.Add("SocialStatusID", DBNull.Value);
        else
            parameters.Add("SocialStatusID", int.Parse(DListApplicantSocialStatus.SelectedValue));

        parameters.Add("PhoneNumber", TxtPhoneNumber.Text);
        parameters.Add("VictimsFullname", TxtVictimsFullname.Text);
        parameters.Add("ComplaintInstitution", TxtComplaintInstitution.Text);
        parameters.Add("StartPunishmentPeriod", (DateTime)DtStartPunishmentPeriod);
        if (DtEndPunishmentPeriod == null)
        {
            parameters.Add("EndPunishmentPeriod", DBNull.Value);
        }
        else
        {
            parameters.Add("EndPunishmentPeriod", (DateTime)DtEndPunishmentPeriod);
        }
        if (DListComplaintResultType.SelectedValue == "-1")
            parameters.Add("ComplaintResultTypeID", DBNull.Value);
        else
            parameters.Add("ComplaintResultTypeID", int.Parse(DListComplaintResultType.SelectedValue));
        parameters.Add("Results", TxtResults.Text);
        parameters.Add("Descriptions", TxtDescriptions.Text);
        parameters.Add("Call_Dt", DtCallDt);

        if (ViewState["operation"]._ToString() == "new")
        {


            parameters.Add("UsersID", DALC._GetAdministratorsLogin.ID);

            parameters.Add("Add_Dt", DateTime.Now);
            parameters.Add("Add_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger());
            result = DALC.InsertDatabase("CallCenter", parameters);
        }
        else if (ViewState["operation"]._ToString() == "edit")
        {
            parameters.Add("WhereId", ViewState["CallCenterId"]._ToString());
            result = DALC.UpdateDatabase("CallCenter", parameters);
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
        ViewState["operation"] = "new";
        ClearForm();
        ShowPopup();
        BtnSave.Text = "Yadda saxla";
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        var Dictionary = new Dictionary<string, object>()
        {
            {"RowIndex",TxtRowIndexFilter.Text},
            //{"CallCenterTypeID",int.Parse(DlistCallCenterTypeFilter.SelectedValue)},
            {"ComplaintTypeID",int.Parse(DListComplaintTypeFilter.SelectedValue)},
            {"ApplicantFullname",TxtApplicantFullnameFilter.Text},
            {"GenderTypeID",int.Parse(DListApplicantGenderTypeFilter.SelectedValue)},
            {"SocialStatusID",int.Parse(DListApplicantSocialStatusFilter.SelectedValue)},
            {"PhoneNumber",TxtPhoneNumberFilter.Text},
            {"VictimsFullname", TxtVictimsFullnameFilter.Text},
            {"ComplaintResultTypeID", DListComplaintResultTypeFilter.SelectedValue},
            {"Results", TxtResultsFilter.Text},
            {"StartCall_Dt",  TxtCallDtStartFilter.Text},
            {"EndCall_Dt",  TxtCallDtEndFilter.Text},
            {"StartPunishmentPeriod", TxtStartPunishmentPeriodFilter.Text},
            {"EndPunishmentPeriod", TxtEndPunishmentPeriodFilter.Text},
            {"DatePriority", DListDatePriority.SelectedValue},
            {"PageSize", DListPageSize.SelectedValue}
        };
        Session[_FilterSessionName] = Dictionary;
        BindGrid();
        Cache.Remove(_CountCacheName);
        Config.RedirectURL("/tools/callcenter/?p=1");
    }

    protected void LnkDetails_Click(object sender, EventArgs e)
    {
        string DataID = (sender as LinkButton).CommandArgument;
        DataTable DtDetails = DALC.GetCallCenterById(DataID);

        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "JqueryModal", "$('#modalDetails').modal('show');", true);
        if (DtDetails != null && DtDetails.Rows.Count > 0)
        {
            LblCallCenterTypeName.Text = DtDetails._Rows("CallCenterTypeName");
            LblComplaintType.Text = DtDetails._Rows("ComplaintTypeName");
            LblApplicantFullname.Text = DtDetails._Rows("ApplicantFullname");
            LblPhoneNumber.Text = DtDetails._Rows("PhoneNumber");
            LblGenderType.Text = DtDetails._Rows("ApplicantGenderTypeName");
            LblSocialStatus.Text = DtDetails._Rows("ApplicantSocialStatusName");
            LblCallDt.Text = DateTime.Parse(DtDetails._Rows("Call_Dt")).ToString("dd.MM.yyyy HH:mm");
            LblVictimsFullname.Text = DtDetails._Rows("VictimsFullname");
            LblComplaintInstitution.Text = DtDetails._Rows("ComplaintInstitution");
            LblStartPunishmentPeriod.Text = DateTime.Parse(DtDetails._Rows("StartPunishmentPeriod")).ToString("dd.MM.yyyy");
            LblEndPunishmentPeriod.Text = DateTime.Parse(DtDetails._Rows("EndPunishmentPeriod")).ToString("dd.MM.yyyy");
            LblComplaintResultType.Text = DtDetails._Rows("ComplaintResultTypeName");
            LblResults.Text = DtDetails._Rows("Results");
            LblDescriptions.Text = DtDetails._Rows("Descriptions");
        }
    }

    protected void LnkEdit_Click(object sender, EventArgs e)
    {
        string DataID = (sender as LinkButton).CommandArgument;
        DataTable DtDetails = DALC.GetCallCenterById(DataID);

        ViewState["operation"] = "edit";
        ClearForm();
        ShowPopup();
        BtnSave.Text = "Redaktə et";
        ViewState["CallCenterId"] = DataID;

        if (DtDetails != null && DtDetails.Rows.Count > 0)
        {
            //DListCallCenterType.SelectedValue = DtDetails._Rows("CallCenterTypeID");
            DListComplaintType.SelectedValue = string.IsNullOrEmpty(DtDetails._Rows("ComplaintTypeID")) ? "-1" : DtDetails._Rows("ComplaintTypeID");
            TxtApplicantFullname.Text = DtDetails._Rows("ApplicantFullname");
            DListApplicantGenderType.SelectedValue = string.IsNullOrEmpty(DtDetails._Rows("GenderTypeID")) ? "-1" : DtDetails._Rows("GenderTypeID");
            DListApplicantSocialStatus.SelectedValue = string.IsNullOrEmpty(DtDetails._Rows("SocialStatusID")) ? "-1" : DtDetails._Rows("SocialStatusID");
            TxtPhoneNumber.Text = DtDetails._Rows("PhoneNumber");
            TxtCallDtDate.Text = DateTime.Parse(DtDetails._Rows("Call_Dt")).ToString("dd.MM.yyyy");
            TxtCallDtTime.Text = DateTime.Parse(DtDetails._Rows("Call_Dt")).ToString("HH:mm");
            TxtVictimsFullname.Text = DtDetails._Rows("VictimsFullname");
            TxtComplaintInstitution.Text = DtDetails._Rows("ComplaintInstitution");
            TxtStartPunishmentPeriod.Text = DateTime.Parse(DtDetails._Rows("StartPunishmentPeriod")).ToString("dd.MM.yyyy");
            TxtEndPunishmentPeriod.Text = DateTime.Parse(DtDetails._Rows("EndPunishmentPeriod")).ToString("dd.MM.yyyy");
            DListComplaintResultType.SelectedValue = string.IsNullOrEmpty(DtDetails._Rows("ComplaintResultTypeID")) ? "-1" : DtDetails._Rows("ComplaintResultTypeID");
            TxtResults.Text = DtDetails._Rows("Results");
            TxtDescriptions.Text = DtDetails._Rows("Descriptions");
        }
    }

    protected void LnkExportExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment; filename = GelenZengler.xls");
        Response.ContentType = "application/vnd.xls";
        StringWriter stringWrite = new StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        GrdCallCenter.Columns[GrdCallCenter.Columns.Count - 1].Visible = false;
        GrdCallCenter.Columns[GrdCallCenter.Columns.Count - 2].Visible = false;
        GrdCallCenter.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
        GrdCallCenter.Columns[GrdCallCenter.Columns.Count - 1].Visible = false;
        GrdCallCenter.Columns[GrdCallCenter.Columns.Count - 2].Visible = false;
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        var Dictionary = new Dictionary<string, object>()
        {
            {"RowIndex",""},
            {"CallCenterTypeID",-1},
            {"ComplaintTypeID",-1},
            {"ApplicantFullname",""},
            {"GenderTypeID",-1},
            {"SocialStatusID",-1},
            {"PhoneNumber",""},
            {"VictimsFullname", ""},
            {"ComplaintResultTypeID", -1},
            {"Results", ""},
            {"StartCall_Dt",  ""},
            {"EndCall_Dt",  ""},
            {"StartPunishmentPeriod", ""},
            {"EndPunishmentPeriod", ""},
            {"DatePriority", "10"},
            {"PageSize", "10"}
        };

        Session[_FilterSessionName] = Dictionary;
        Config.RedirectURL("/tools/callcenter/?p=1");
    }

}