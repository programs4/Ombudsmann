using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_Controls_IndividualComplaints : System.Web.UI.UserControl
{
    string _CountCacheName = "IndividualComplaints";
    string _FilterSessionName = "IndividualComplaints";

    void BindDList()
    {
        DlistExecutiveUsers.DataSource = DALC.GetUsers();
        DlistExecutiveUsers.DataBind();
        DlistExecutiveUsers.Items.Insert(0, new ListItem("--", "-1"));

        DlistExecutiveUsersFilter.DataSource = DALC.GetUsers();
        DlistExecutiveUsersFilter.DataBind();
        DlistExecutiveUsersFilter.Items.Insert(0, new ListItem("--", "-1"));

        DListComplaintType.DataSource = DALC.GetComplaintType();
        DListComplaintType.DataBind();
        DListComplaintType.Items.Insert(0, new ListItem("--", "-1"));

        DListApplicatsGenderType.DataSource = DALC.GetGenderType();
        DListApplicatsGenderType.DataBind();
        DListApplicatsGenderType.Items.Insert(0, new ListItem("--", "-1"));

        DListApplicantsSocialStatus.DataSource = DALC.GetSocialStaus();
        DListApplicantsSocialStatus.DataBind();
        DListApplicantsSocialStatus.Items.Insert(0, new ListItem("--", "-1"));

        DListComplaintResultType.DataSource = DALC.GetComplaintResultType();
        DListComplaintResultType.DataBind();
        DListComplaintResultType.Items.Insert(0, new ListItem("--", "-1"));

        DListComplaintTypeFilter.DataSource = DALC.GetComplaintType();
        DListComplaintTypeFilter.DataBind();
        DListComplaintTypeFilter.Items.Insert(0, new ListItem("--", "-1"));

        DListApplicantsGenderTypeFilter.DataSource = DALC.GetGenderType();
        DListApplicantsGenderTypeFilter.DataBind();
        DListApplicantsGenderTypeFilter.Items.Insert(0, new ListItem("--", "-1"));

        DListApplicantsSocialStatusFilter.DataSource = DALC.GetSocialStaus();
        DListApplicantsSocialStatusFilter.DataBind();
        DListApplicantsSocialStatusFilter.Items.Insert(0, new ListItem("--", "-1"));

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
                TxtNumbersFilter.Text = DictionarySession["Numbers"]._ToString();
                DListComplaintTypeFilter.SelectedValue = DictionarySession["ComplaintTypeID"]._ToString();
                DlistExecutiveUsersFilter.SelectedValue = DictionarySession["ExecutiveUsersID"]._ToString();
                TxtApplicantsFullnameFilter.Text = DictionarySession["ApplicantsFullname"]._ToString();
                DListApplicantsGenderTypeFilter.SelectedValue = DictionarySession["GenderTypeID"]._ToString();
                DListApplicantsSocialStatusFilter.SelectedValue = DictionarySession["SocialStatusID"]._ToString();
                TxtVictimsFullnameFilter.Text = DictionarySession["VictimsFullname"]._ToString();
                TxtComplaintInstitutionFilter.Text = DictionarySession["ComplaintInstitution"]._ToString();
                DListComplaintResultTypeFilter.SelectedValue = DictionarySession["ComplaintResultTypeID"]._ToString();
                TxtResultsFilter.Text = DictionarySession["Results"]._ToString();
                TxtEnterOrganizationsStartDtFilter.Text = DictionarySession["StartEnterOrganizations_Dt"]._ToString();
                TxtEnterOrganizationsEndDtFilter.Text = DictionarySession["EndEnterOrganizations_Dt"]._ToString();
                TxtEnterSectorStartDtFilter.Text = DictionarySession["StartEnterSector_Dt"]._ToString();
                TxtEnterSectorEndDtFilter.Text = DictionarySession["EndEnterSector_Dt"]._ToString();
                DListDatePriority.SelectedValue = DictionarySession["DatePriority"]._ToString();
                DListPageSize.SelectedValue = DictionarySession["PageSize"]._ToString();
            }
        }

        #region BetweenDateFromat

        string OrganizationsDate = "";

        string Dt1 = "19000101";
        string Dt2 = DateTime.Now.ToString("yyyyMMdd");

        object DateFilter1 = Config.DateTimeFormat(TxtEnterOrganizationsStartDtFilter.Text.Trim());
        object DateFilter2 = Config.DateTimeFormat(TxtEnterOrganizationsEndDtFilter.Text.Trim());

        if (DateFilter1 == null && DateFilter2 == null)
        {
            OrganizationsDate = "";
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

            OrganizationsDate = Dt1 + "&" + Dt2;
        }


        string SectorDate = "";

        Dt1 = "19000101";
        Dt2 = DateTime.Now.ToString("yyyyMMdd");

        DateFilter1 = Config.DateTimeFormat(TxtEnterSectorStartDtFilter.Text.Trim());
        DateFilter2 = Config.DateTimeFormat(TxtEnterSectorEndDtFilter.Text.Trim());

        if (DateFilter1 == null && DateFilter2 == null)
        {
            SectorDate = "";
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

            SectorDate = Dt1 + "&" + Dt2;

        }
        #endregion

        var Dictionary = new Dictionary<string, object>()
        {
            {"Numbers",TxtNumbersFilter.Text},
            {"ComplaintTypeID",int.Parse(DListComplaintTypeFilter.SelectedValue)},
            {"ExecutiveUsersID",int.Parse(DlistExecutiveUsersFilter.SelectedValue) },
            {"GenderTypeID",int.Parse(DListApplicantsGenderTypeFilter.SelectedValue) },
            {"SocialStatusID",int.Parse(DListApplicantsSocialStatusFilter.SelectedValue) },
            {"(LIKE)ApplicantsFullname",TxtApplicantsFullnameFilter.Text},
            {"(LIKE)VictimsFullname",TxtVictimsFullnameFilter.Text},
            {"ComplaintInstitution", TxtComplaintInstitutionFilter.Text},
            {"ComplaintResultTypeID", DListComplaintResultTypeFilter.SelectedValue},
            {"(LIKE)Results", TxtResultsFilter.Text},
            {"EnterOrganizations_Dt(BETWEEN)", OrganizationsDate},
            {"EnterSector_Dt(BETWEEN)", SectorDate}
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

        DALC.DataTableResult ApplicationsList = DALC.GetIndividualComplaints(Dictionary, PageNum, RowNumber, new object[] { RowIndex }, OrderByType);

        if (ApplicationsList.Count == -1)
        {
            return;
        }

        if (ApplicationsList.Dt.Rows.Count < 1 && PageNum > 1)
        {
            Config.RedirectURL(string.Format("/tools/IndividualComplaints/?p={0}", PageNum - 1));
        }

        int Total_Count = ApplicationsList.Count % RowNumber > 0 ? (ApplicationsList.Count / RowNumber) + 1 : ApplicationsList.Count / RowNumber;
        HdnTotalCount.Value = Total_Count.ToString();

        PnlPager.Visible = ApplicationsList.Count > RowNumber;

        GrdIndividualComplaints.DataSource = ApplicationsList.Dt;
        GrdIndividualComplaints.DataBind();

        LblCountInfo.Text = string.Format("Axtarış üzrə nəticə: {0}", ApplicationsList.Count);

    }

    void ShowAlert(string msg)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "PopupScript", "alert('" + msg + "');", true);
    }

    void ShowPopup()
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "JqueryModal", "$('#modalNewIndividualComplaint').modal('show');", true);
    }

    void ClearForm()
    {
        TxtNumbers.Text = "";
        DlistExecutiveUsers.SelectedIndex =
            DListComplaintType.SelectedIndex =
            DListApplicatsGenderType.SelectedIndex =
            DListApplicantsSocialStatus.SelectedIndex = 0;

        TxtApplicantsFullname.Text = "";
        TxtVictimsFullname.Text = "";
        TxtApplicantsAddress.Text = "";
        TxtComplaintInstitution.Text = "";
        ChkIsBadTreatment.Checked = false;
        TxtQueriedInstitution.Text = "";
        TxtQueriedRespond.Text = "";
        TxtCitizenRespond.Text = "";
        ChkIsReference.Checked = false;
        TxtResults.Text = "";
        TxtEnterOrganizationsDt.Text = "";
        TxtEnterSectorDt.Text = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //eger istifadeci veya admin deyilse
        if (DALC._GetAdministratorsLogin.UsersStatusID != 30 && DALC._GetAdministratorsLogin.UsersStatusID != 25)
        {
            LnkNew.Visible = false;
            GrdIndividualComplaints.Columns[GrdIndividualComplaints.Columns.Count - 1].Visible = false;
        }
        if (string.IsNullOrEmpty(Config._GetQueryString("p")))
        {
            Session[_FilterSessionName] = null;
        }
        if (!IsPostBack)
        {
            BindDList();
            BindGrid();
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        //eger istifadeci veya admin deyilse
        if (DALC._GetAdministratorsLogin.UsersStatusID != 30 && DALC._GetAdministratorsLogin.UsersStatusID != 25)
        {
            return;
        }

        if (string.IsNullOrEmpty(TxtNumbers.Text.Trim()))
        {
            Config.MsgBoxAjax("Fərdi şikayət nömrəsini daxil edin.");
            ShowPopup();
            return;
        }

        if (!Config.IsNumeric(TxtNumbers.Text.Trim()))
        {
            Config.MsgBoxAjax("Fərdi şikayət nömrəsini rəqəm tipdə daxil edin.");
            ShowPopup();
            return;
        }

        if (DListComplaintType.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Şikayətin mahiyyətini seçin.");
            ShowPopup();
            return;
        }

        if (DlistExecutiveUsers.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("İcraçını seçin.");
            ShowPopup();
            return;
        }

        object DtEnterOrganizations = TxtEnterOrganizationsDt.Text.DateTimeFormat();
        if (DtEnterOrganizations == null)
        {
            Config.MsgBoxAjax("Aparata daxil olma tarixini düzgün daxil edin.");
            ShowPopup();
            return;
        }

        object DtEnterSector = TxtEnterSectorDt.Text.DateTimeFormat();
        if (DtEnterSector == null)
        {
            Config.MsgBoxAjax("Sektora daxil olma tarixini düzgün daxil edin.");
            ShowPopup();
            return;
        }

        if (string.IsNullOrEmpty(TxtApplicantsFullname.Text.Trim()))
        {
            Config.MsgBoxAjax("Ərizəçini daxil edin.");
            ShowPopup();
            return;
        }

        if (string.IsNullOrEmpty(TxtApplicantsAddress.Text.Trim()))
        {
            Config.MsgBoxAjax("Ünvanını daxil edin.");
            ShowPopup();
            return;
        }

        if (string.IsNullOrEmpty(TxtVictimsFullname.Text.Trim()))
        {
            Config.MsgBoxAjax("Kimin barəsində müraciət edildiyini daxil edin.");
            ShowPopup();
            return;
        }

        if (DListApplicatsGenderType.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Cinsini daxil edin.");
            ShowPopup();
            return;
        }

        if (string.IsNullOrEmpty(TxtComplaintInstitution.Text.Trim()))
        {
            Config.MsgBoxAjax("Şikayət olunan subyekti daxil edin.");
            ShowPopup();
            return;
        }

        //if (string.IsNullOrEmpty(TxtQueriedInstitution.Text.Trim()))
        //{
        //    Config.MsgBoxAjax("Sorğu göndərilən orqanı daxil edin.");
        //    ShowPopup();
        //    return;
        //}

        //if (string.IsNullOrEmpty(TxtQueriedRespond.Text.Trim()))
        //{
        //    Config.MsgBoxAjax("Sorğuya verilən cavabı daxil edin.");
        //    ShowPopup();
        //    return;
        //}
        //if (string.IsNullOrEmpty(TxtCitizenRespond.Text.Trim()))
        //{
        //    Config.MsgBoxAjax("Vətəndaşa verilən cavabı daxil edin.");
        //    ShowPopup();
        //    return;
        //}

        //if (string.IsNullOrEmpty(TxtResults.Text.Trim()))
        //{
        //    Config.MsgBoxAjax("Nəticəni daxil edin.");
        //    ShowPopup();
        //    return;
        //}

        try
        {
            int result = -1;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("ExecutiveUsersID", int.Parse(DlistExecutiveUsers.SelectedValue));
            parameters.Add("ComplaintTypeID", int.Parse(DListComplaintType.SelectedValue));
            parameters.Add("Numbers", TxtNumbers.Text._ToInt32());
            parameters.Add("ApplicantsFullname", TxtApplicantsFullname.Text);
            parameters.Add("GenderTypeID", int.Parse(DListApplicatsGenderType.SelectedValue));
            if (DListApplicantsSocialStatus.SelectedValue == "-1")
                parameters.Add("SocialStatusID", DBNull.Value);
            else
                parameters.Add("SocialStatusID", int.Parse(DListApplicantsSocialStatus.SelectedValue));
            parameters.Add("VictimsFullname", TxtVictimsFullname.Text);
            parameters.Add("ApplicantsAddress", TxtApplicantsAddress.Text);
            parameters.Add("ComplaintInstitution", TxtComplaintInstitution.Text);
            parameters.Add("IsBadTreatment", ChkIsBadTreatment.Checked);
            parameters.Add("QueriedInstitution", TxtQueriedInstitution.Text);
            parameters.Add("QueriedRespond", TxtQueriedRespond.Text);
            parameters.Add("CitizenRespond", TxtCitizenRespond.Text);
            parameters.Add("IsReference", ChkIsReference.Checked);
            if (DListComplaintResultType.SelectedValue == "-1")
                parameters.Add("ComplaintResultTypeID", DBNull.Value);
            else
                parameters.Add("ComplaintResultTypeID", int.Parse(DListComplaintResultType.SelectedValue));
            parameters.Add("Results", TxtResults.Text);
            parameters.Add("EnterOrganizations_Dt", (DateTime)DtEnterOrganizations);
            parameters.Add("EnterSector_Dt", (DateTime)DtEnterSector);

            if (ViewState["operation"]._ToString() == "new")
            {
                parameters.Add("CreatedUsersID", DALC._GetAdministratorsLogin.ID);
                parameters.Add("Add_Dt", DateTime.Now);
                parameters.Add("Add_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger());
                result = DALC.InsertDatabase("IndividualComplaints", parameters);

            }
            else if (ViewState["operation"]._ToString() == "edit")
            {
                parameters.Add("WhereId", ViewState["IndividualComplaintsId"]._ToString());
                result = DALC.UpdateDatabase("IndividualComplaints", parameters);
            }

            if (result > 0)
            {
                Config.MsgBoxAjax(ViewState["operation"]._ToString() == "new" ? "Məlumatlar qeydə alındı." : "Məlumatlar redaktə edildi.", true);
                BindGrid();
            }
            else
            {
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                ShowPopup();
            }
        }
        catch (Exception er)
        {
            DALC.ErrorLogsInsert("IndividualComplaints catch error: " + er.Message);
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            ShowPopup();
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
            {"Numbers",TxtNumbersFilter.Text},
            {"ExecutiveUsersID",int.Parse(DlistExecutiveUsersFilter.SelectedValue) },
            {"ComplaintTypeID",int.Parse(DListComplaintTypeFilter.SelectedValue)},
            {"ApplicantsFullname",TxtApplicantsFullnameFilter.Text},
            {"GenderTypeID",int.Parse(DListApplicantsGenderTypeFilter.SelectedValue)},
            {"SocialStatusID",int.Parse(DListApplicantsSocialStatusFilter.SelectedValue)},
            {"VictimsFullname",TxtVictimsFullnameFilter.Text},
            {"ComplaintInstitution", TxtComplaintInstitutionFilter.Text},
            {"ComplaintResultTypeID", DListComplaintResultTypeFilter.SelectedValue},
            {"Results", TxtResultsFilter.Text},
            {"StartEnterOrganizations_Dt", TxtEnterOrganizationsStartDtFilter.Text},
            {"EndEnterOrganizations_Dt", TxtEnterOrganizationsEndDtFilter.Text},
            {"StartEnterSector_Dt", TxtEnterSectorStartDtFilter.Text},
            {"EndEnterSector_Dt", TxtEnterSectorEndDtFilter.Text},
            {"DatePriority", DListDatePriority.SelectedValue},
            {"PageSize", DListPageSize.SelectedValue}
        };
        Session[_FilterSessionName] = Dictionary;
        BindGrid();
        Cache.Remove(_CountCacheName);
        Config.RedirectURL("/tools/individualcomplaints/?p=1");
    }

    protected void LnkEdit_Click(object sender, EventArgs e)
    {
        string DataID = (sender as LinkButton).CommandArgument;
        DataTable DtDetails = DALC.GetIndividualComplaintsById(DataID);

        ClearForm();
        ViewState["operation"] = "edit";
        ShowPopup();
        BtnSave.Text = "Redaktə et";
        ViewState["IndividualComplaintsId"] = DataID;

        if (DtDetails != null && DtDetails.Rows.Count > 0)
        {
            TxtNumbers.Text = DtDetails._Rows("Numbers");
            DlistExecutiveUsers.SelectedValue = DtDetails._Rows("ExecutiveUsersID");
            DListComplaintType.SelectedValue = string.IsNullOrEmpty(DtDetails._Rows("ComplaintTypeID")) ? "-1" : DtDetails._Rows("ComplaintTypeID");
            TxtApplicantsFullname.Text = DtDetails._Rows("ApplicantsFullname");
            DListApplicatsGenderType.SelectedValue = string.IsNullOrEmpty(DtDetails._Rows("GenderTypeID")) ? "-1" : DtDetails._Rows("GenderTypeID");
            DListApplicantsSocialStatus.SelectedValue = string.IsNullOrEmpty(DtDetails._Rows("SocialStatusID")) ? "-1" : DtDetails._Rows("SocialStatusID");
            TxtVictimsFullname.Text = DtDetails._Rows("VictimsFullname");
            TxtApplicantsAddress.Text = DtDetails._Rows("ApplicantsAddress");
            TxtComplaintInstitution.Text = DtDetails._Rows("ComplaintInstitution");
            ChkIsBadTreatment.Checked = Convert.ToBoolean(DtDetails._Rows("IsBadTreatment"));
            TxtQueriedInstitution.Text = DtDetails._Rows("QueriedInstitution");
            TxtQueriedRespond.Text = DtDetails._Rows("QueriedRespond");
            TxtCitizenRespond.Text = DtDetails._Rows("CitizenRespond");
            ChkIsReference.Checked = Convert.ToBoolean(DtDetails._Rows("IsReference"));
            DListComplaintResultType.SelectedValue = string.IsNullOrEmpty(DtDetails._Rows("ComplaintResultTypeID")) ? "-1" : DtDetails._Rows("ComplaintResultTypeID");
            TxtResults.Text = DtDetails._Rows("Results");
            TxtEnterOrganizationsDt.Text = DateTime.Parse(DtDetails._Rows("EnterOrganizations_Dt")).ToString("dd.MM.yyyy");
            TxtEnterSectorDt.Text = DateTime.Parse(DtDetails._Rows("EnterSector_Dt")).ToString("dd.MM.yyyy");
        }

    }

    protected void LnkDetails_Click(object sender, EventArgs e)
    {
        string DataID = (sender as LinkButton).CommandArgument;
        DataTable DtDetails = DALC.GetIndividualComplaintsById(DataID);

        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "JqueryModal", "$('#modalDetails').modal('show');", true);
        if (DtDetails != null && DtDetails.Rows.Count > 0)
        {

            LblNumbers.Text = DtDetails._Rows("Numbers");
            LblComplaintType.Text = DtDetails._Rows("ComplaintTypeName");
            LblExecutive.Text = DtDetails._Rows("ExecutiveUsersName");
            LblApplicantsFullname.Text = DtDetails._Rows("ApplicantsFullname");
            LblApplicantsGenderType.Text = DtDetails._Rows("ApplicantsGenderTypeName");
            LblApplicantsSocialStatus.Text = DtDetails._Rows("ApplicantsSocialStatusName");
            LblVictimsFullname.Text = DtDetails._Rows("VictimsFullname");
            LblApplicantsAddress.Text = DtDetails._Rows("ApplicantsAddress");
            LblComplaintInstitution.Text = DtDetails._Rows("ComplaintInstitution");
            LblIsBadTreatment.Text = Convert.ToBoolean(DtDetails._Rows("IsBadTreatment")) ? "Bəli" : "Xeyir";
            LblQueriedInstitution.Text = DtDetails._Rows("QueriedInstitution");
            LblQueriedRespond.Text = DtDetails._Rows("QueriedRespond");
            LblCitizenRespond.Text = DtDetails._Rows("CitizenRespond");
            LblIsReference.Text = Convert.ToBoolean(DtDetails._Rows("IsReference")) ? "Bəli" : "Xeyir";
            LblResults.Text = DtDetails._Rows("Results");
            LblEnterOrganizationsDt.Text = DateTime.Parse(DtDetails._Rows("EnterOrganizations_Dt")).ToString("dd.MM.yyyy");
            LblEnterSectorDt.Text = DateTime.Parse(DtDetails._Rows("EnterSector_Dt")).ToString("dd.MM.yyyy");
        }

    }

    protected void LnkExportExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment; filename = FerdiSikayetler.xls");
        Response.ContentType = "application/vnd.xls";
        StringWriter stringWrite = new StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        GrdIndividualComplaints.Columns[GrdIndividualComplaints.Columns.Count - 1].Visible = false;
        GrdIndividualComplaints.Columns[GrdIndividualComplaints.Columns.Count - 2].Visible = false;
        GrdIndividualComplaints.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
        GrdIndividualComplaints.Columns[GrdIndividualComplaints.Columns.Count - 1].Visible = false;
        GrdIndividualComplaints.Columns[GrdIndividualComplaints.Columns.Count - 2].Visible = false;
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        var Dictionary = new Dictionary<string, object>()
        {
            {"RowIndex",""},
            {"Numbers",""},
            {"ExecutiveUsersID",-1},
            {"ComplaintTypeID",-1},
            {"ApplicantsFullname",""},
            {"GenderTypeID",-1},
            {"SocialStatusID",-1},
            {"VictimsFullname",""},
            {"ComplaintInstitution", ""},
            {"ComplaintResultTypeID", -1},
            {"Results", ""},
            {"StartEnterOrganizations_Dt", ""},
            {"EndEnterOrganizations_Dt", ""},
            {"StartEnterSector_Dt", ""},
            {"EndEnterSector_Dt", ""},
            {"DatePriority", "10"},
            {"PageSize", "10"}
        };

        Session[_FilterSessionName] = Dictionary;
        Config.RedirectURL("/tools/individualcomplaints/?p=1");
    }
}