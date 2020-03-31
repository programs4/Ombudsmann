<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AuditsOrganizationsMeetingPersons.ascx.cs" Inherits="Tools_Controls_AuditsOrganizationsMeetingPersons" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="modal fade" id="modalNewMeetingPerson" role="dialog" style="margin-top: 130px">

            <asp:Panel ID="Panel2" runat="server" class="modal-dialog" DefaultButton="BtnSaveMeetingPerson">
                <!-- Modal content-->
                <div class="modal-content" style="width: 800px">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Yeni görüşülən şəxs</h4>
                    </div>
                    <div class="modal-body">
                        <table class="table">
                            <%-- <tr>
                                <td class="table-info">Şikayət nömrəsi</td>
                                <td>
                                    <asp:TextBox ID="TxtMeetinPersonComplaintNumber" runat="server" CssClass="form-control" Height="35px"></asp:TextBox>
                                </td>
                            </tr>--%>
                            <tr>
                                <td>Soyadı, adı və atasının adı</td>
                                <td>
                                    <asp:TextBox ID="TxtMeetingPersonFullname" runat="server" CssClass="form-control" Height="35px"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>Cinsi</td>
                                <td>
                                    <asp:DropDownList ID="DListGenderType" runat="server" CssClass="form-control" DataValueField="ID" DataTextField="Name"></asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td>Sosial statusu</td>
                                <td>
                                    <asp:DropDownList ID="DListSocialStatus" runat="server" CssClass="form-control" DataValueField="ID" DataTextField="Name"></asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td>Saxlandığı yer</td>
                                <td>
                                    <asp:TextBox ID="TxtMeetingPersonPlace" runat="server" CssClass="form-control" Height="35px"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>Təqsirləndirildiyi maddə</td>
                                <td>
                                    <asp:TextBox ID="TxtMeetingPersonAccusedArticle" runat="server" CssClass="form-control" Height="35px"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>Saxlanma tarixi (başlanğıc)</td>
                                <td>
                                    <asp:TextBox ID="TxtMeetingPersonStartPunishmentPeriod" AutoCompleteType="Disabled" autocomplete="off" runat="server" CssClass="form-control form_datetime" Height="35px"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>Saxlanma tarixi (son)</td>
                                <td>
                                    <asp:TextBox ID="TxtMeetingPersonEndPunishmentPeriod" runat="server" AutoCompleteType="Disabled" autocomplete="off" CssClass="form-control form_datetime" Height="35px"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>Problem</td>
                                <td>
                                    <asp:TextBox ID="TxtMeetingPersonProblem" runat="server" CssClass="form-control" Height="35px"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>Nəticə</td>
                                <td>
                                    <asp:DropDownList ID="DListComplaintResultType" runat="server" CssClass="form-control" DataValueField="ID" DataTextField="Name"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Qeyd</td>
                                <td>
                                    <asp:TextBox ID="TxtMeetingPersonsResult" runat="server" CssClass="form-control" Height="35px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Görüşün tarixi</td>
                                <td>
                                    <asp:TextBox ID="TxtMeetingPersonMeetingDt" AutoCompleteType="Disabled" autocomplete="off" runat="server" CssClass="form-control form_datetime" Height="35px" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>

                        </table>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="BtnSaveMeetingPerson" runat="server" Text="Yadda saxla" CssClass="btn btn-primary" Height="35px" OnClientClick="this.style.display='none';document.getElementById('load').style.display=''" OnClick="BtnSaveMeetingPerson_Click" />
                        <img id="load" src="/images/loading.gif" style="display: none" />
                    </div>
                </div>
            </asp:Panel>
        </div>
        <asp:Panel ID="PnlAuditDetails" runat="server">
            <div class="panel panel-default">
                <div class="panel-heading" style="background-color: #f5f5f5; color: black">
                    Keçirilən baxışın detalları
                </div>
                <div class="panel-body">
                    <a href="#" onclick="window.print()">
                        <span class="glyphicon glyphicon-print"></span>Çap et
                    </a>
                    <br />
                    <br />
                    <table class="table table-bordered table-hover" id="printable">
                        <tr>
                            <td style="width: 350px"><strong>Regional mərkəz</strong> </td>
                            <td>
                                <asp:Label ID="LblRegionalCenter" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td><strong>Qurum</strong></td>
                            <td>
                                <asp:Label ID="LblOrganization" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td><strong>Başçəkmənin forması</strong> </td>
                            <td>
                                <asp:Label ID="LblVisitType" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td><strong>Aşkar edilmiş problemlər</strong> </td>
                            <td>
                                <asp:Label ID="LblProblems" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td><strong>Tövsiyə</strong></td>
                            <td>
                                <asp:Label ID="LblSuggestion" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td><strong>Baxış tarixi </strong></td>
                            <td>
                                <asp:Label ID="LblVisitDate" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td><strong>Qeyd</strong> </td>
                            <td>
                                <asp:Label ID="LblDescription" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Panel>
        <br />
        <br />
        <br />
        <div class="panel panel-default">
            <div class="panel-heading" style="background-color: #f5f5f5; color: black">
                Baxış zamanı görüşülən şəxslər
            </div>
            <asp:Panel ID="Panel1" runat="server" class="panel-body" DefaultButton="BtnSearch">
                <br />
                <div class="filter-panel">
                    <div class="filter-item">
                        №<br />
                        <asp:TextBox ID="TxtRowIndexFilter" runat="server" AutoCompleteType="Disabled" autocomplete="off" Height="35px" CssClass="form-control"></asp:TextBox>
                    </div>
                    <%--<div class="filter-item">
                        Şikayətin nömrəsi<br />
                        <asp:TextBox ID="TxtComplaintNumbersFilter" runat="server" AutoCompleteType="Disabled" autocomplete="off" Height="35px" CssClass="form-control"></asp:TextBox>
                    </div>--%>
                    <div class="filter-item">
                        Üst Qurum:<br />
                        <asp:DropDownList ID="DListTopOrganizationFilter" DataValueField="ID" DataTextField="Name" runat="server" Height="35px" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DListTopOrganizationFilter_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="filter-item">
                        Qurum:<br />
                        <asp:DropDownList ID="DListSubOrganizationFilter" DataValueField="ID" DataTextField="Name" runat="server" Height="35px" CssClass="form-control">
                        </asp:DropDownList>
                    </div>

                    <div class="filter-item">
                        Soyadı,adı və ata adı<br />
                        <asp:TextBox ID="TxtFullnameFilter" runat="server" Height="35px" AutoCompleteType="Disabled" autocomplete="off" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="filter-item">
                        Cinsi<br />
                        <asp:DropDownList ID="DListGenderTypeFilter" runat="server" CssClass="form-control" DataValueField="ID" DataTextField="Name"></asp:DropDownList>
                    </div>

                    <div class="filter-item">
                        Sosial statusu<br />
                        <asp:DropDownList ID="DListSocialStatusFilter" runat="server" CssClass="form-control" DataValueField="ID" DataTextField="Name"></asp:DropDownList>
                    </div>

                    <div class="filter-item">
                        Saxlandığı yer<br />
                        <asp:TextBox ID="TxtPlaceFilter" runat="server" Height="35px" AutoCompleteType="Disabled" autocomplete="off" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="filter-item">
                        Təqsirləndirildiyi maddə<br />
                        <asp:TextBox ID="TxtAccusedArticlesFilter" runat="server" AutoCompleteType="Disabled" autocomplete="off" Height="35px" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="filter-item">
                        Saxlanma tarixi (başlanğıc)<br />
                        <asp:TextBox ID="TxtStartPunishmentPeriodFilter" runat="server" AutoCompleteType="Disabled" autocomplete="off" Height="35px" CssClass="form-control form_datetime"></asp:TextBox>
                    </div>
                    <div class="filter-item">
                        Saxlanma tarixi (son)<br />
                        <asp:TextBox ID="TxtEndPunishmentPeriodFilter" runat="server" AutoCompleteType="Disabled" autocomplete="off" Height="35px" CssClass="form-control form_datetime"></asp:TextBox>
                    </div>
                    <div class="filter-item">
                        Problem<br />
                        <asp:TextBox ID="TxtProblemsFilter" runat="server" AutoCompleteType="Disabled" autocomplete="off" Height="35px" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="filter-item">
                        Nəticə<br />
                        <asp:DropDownList ID="DListComplaintResultTypeFilter" runat="server" CssClass="form-control" DataValueField="ID" DataTextField="Name"></asp:DropDownList>
                    </div>
                    <div class="filter-item">
                        Qeyd<br />
                        <asp:TextBox ID="TxtResultsFilter" runat="server" AutoCompleteType="Disabled" autocomplete="off" Height="35px" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="filter-item">
                        Görüşün tarixi (başlanğıc)<br />
                        <asp:TextBox ID="TxtMeetingStartDtFilter" runat="server" AutoCompleteType="Disabled" autocomplete="off" Height="35px" CssClass="form-control form_datetime"></asp:TextBox>
                    </div>
                    <div class="filter-item">
                        Görüşün tarixi (son)<br />
                        <asp:TextBox ID="TxtMeetingEndDtFilter" runat="server" AutoCompleteType="Disabled" autocomplete="off" Height="35px" CssClass="form-control form_datetime"></asp:TextBox>
                    </div>

                    <div class="filter-item">
                        Sıralama tarixə görə:<br />
                        <asp:DropDownList ID="DListDatePriority" runat="server" Height="35px" CssClass="form-control">
                            <asp:ListItem Text="Azalan" Value="10"></asp:ListItem>
                            <asp:ListItem Text="Artan" Value="20"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="filter-item">
                        Məlumatları:<br />
                        <asp:DropDownList ID="DListPageSize" runat="server" Height="35px" CssClass="form-control">
                            <asp:ListItem Text="Səhifələ" Value="10"></asp:ListItem>
                            <asp:ListItem Text="Bütün məlumatlar" Value="20"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div style="clear: both;"></div>
                    <asp:Button ID="BtnSearch" runat="server" Text="Axtar" CssClass="btn btn-success" Width="120px" Height="35px" OnClientClick="this.style.display='none';document.getElementById('search_load').style.display=''" OnClick="BtnSearch_Click" />
                    <asp:Button ID="BtnClear" runat="server" Text="X" CssClass="btn btn-default" Height="35px" Width="50px" OnClientClick="this.style.display='none';document.getElementById('search_load').style.display=''" OnClick="BtnClear_Click" />
                    <img id="search_load" src="/images/loading.gif" style="display: none" />
                    <hr />
                    <div class="row">
                        <div class="col-md-6">

                            <asp:LinkButton ID="LnkNew" runat="server" OnClick="LnkNew_Click">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/add.png" />
                                <span>Yeni görüşülən şəxs</span>
                            </asp:LinkButton>

                            <asp:LinkButton ID="LnkExportExcel" runat="server" OnClick="LnkExportExcel_Click">
                            <img src="/images/excel.png" style="margin-left:10pt;" /> İxrac et
                            </asp:LinkButton>
                            <a href="#print" onclick="window.print();">
                                <img src="/images/print.png" style="margin-left: 10pt;" />
                                Çap et</a>

                        </div>
                        <div class="col-md-6 text-right" style="padding-top: 7px">
                            <asp:Label ID="LblCountInfo" Font-Bold="true" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <br />
                </div>
                <br />
                <asp:GridView ID="GrdAuditsOrganizationsMeetingPersons" AutoGenerateColumns="False" runat="server" BorderWidth="0px" CellPadding="4" Width="100%" CssClass="form-control" Font-Bold="False">
                    <Columns>

                        <asp:BoundField HeaderText="№" DataField="RowIndex">
                            <ItemStyle Font-Bold="true" Width="80px" />
                        </asp:BoundField>

                        <%--  <asp:BoundField HeaderText="Şikayət nömrəsi" DataField="ComplaintNumbers">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>--%>

                        <asp:BoundField HeaderText="Qurum" DataField="OrganizationsName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Soyadı, adı və ata adı" DataField="Fullname">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Cinsi" DataField="GenderName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="SosialStaus" DataField="SocialStatusName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Saxlandığı yer" DataField="Place">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Təqsirləndirildiyi maddə" DataField="AccusedArticles">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Saxlanma tarixi (başlanğıc)" DataField="StartPunishmentPeriod" DataFormatString="{0:dd.MM.yyyy}" />
                        <asp:BoundField HeaderText="Saxlanma tarixi (son)" DataField="EndPunishmentPeriod" DataFormatString="{0:dd.MM.yyyy}" />

                        <asp:BoundField HeaderText="Problem" DataField="Problems">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Nəticə" DataField="ComplaintResultTypeName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Görüşün tarixi" DataField="Meeting_Dt" DataFormatString="{0:dd.MM.yyyy}" />

                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LnkDelete" OnClientClick="return confirm('Əminsiniz?');" runat="server" CausesValidation="False" Text='<span class="glyphicon glyphicon-trash"></span> Sil' CommandArgument='<%# Eval("ID") %>' OnClick="LnkDelete_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <span class="glyphicon glyphicon-pencil"></span>
                                <asp:LinkButton ID="LnkEdit" runat="server" CommandArgument='<%#Eval("ID")%>' OnClick="LnkEdit_Click">Redaktə et</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="110px" />
                        </asp:TemplateField>

                    </Columns>
                    <AlternatingRowStyle BackColor="#FBFBFB" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <EmptyDataTemplate>
                        <div class="textBox" style="margin-top: 10px; margin-bottom: 10px; border-width: 0px">
                            Məlumat tapılmadı.
                        </div>
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="#1C5E55" />
                    <HeaderStyle BackColor="#F2F2F2" Height="65px" />
                    <SelectedRowStyle BackColor="#eae1b8" />
                    <PagerSettings PageButtonCount="20" />
                    <PagerStyle BackColor="White" CssClass="Gridpager" ForeColor="White" HorizontalAlign="Right" />
                    <RowStyle CssClass="hoverLink cursorRePointer" Height="40px" HorizontalAlign="Center" Font-Size="10pt" />
                </asp:GridView>
                <asp:Panel ID="PnlPager" CssClass="pager_top" runat="server">
                    <ul class="pagination bootpag"></ul>
                </asp:Panel>

                <asp:HiddenField ID="HdnTotalCount" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="HdnPageNumber" ClientIDMode="Static" Value="1" runat="server" />
            </asp:Panel>
        </div>

    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="LnkExportExcel" />
    </Triggers>
</asp:UpdatePanel>
<script>
    function GetPagination(t, p) {
        $('.pager_top').bootpag({
            total: t,
            page: p,
            maxVisible: 15,
            leaps: true,
            firstLastUse: true,
            first: '<span aria-hidden="true">&larr;</span>',
            last: '<span aria-hidden="true">&rarr;</span>',
            wrapClass: 'pagination',
            activeClass: 'active',
            disabledClass: 'disabled',
            nextClass: 'next',
            prevClass: 'prev',
            lastClass: 'last',
            firstClass: 'first',

        }).on("page", function (event, num) {
            window.location.href = '/tools/AuditsOrganizationsMeetingPersons/?p=' + num;
        }).find('.pagination');
    }

    function pageLoad() {
        GetPagination($('#HdnTotalCount').val(), $('#HdnPageNumber').val());
        datetime();
    }
</script>
