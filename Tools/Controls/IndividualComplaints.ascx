<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IndividualComplaints.ascx.cs" Inherits="Tools_Controls_IndividualComplaints" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="modal fade" id="modalNewIndividualComplaint" role="dialog" style="margin-top: 130px">
            <div class="modal-dialog">
                <!-- Modal content-->

                <asp:Panel ID="Panel1" runat="server" class="modal-content" Style="width: 800px" DefaultButton="BtnSave">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Yeni fərdi şikayət</h4>
                    </div>
                    <div class="modal-body">
                        <table class="table">
                            <tr>
                                <td class="table-info">FŞ №</td>
                                <td>
                                    <asp:TextBox ID="TxtNumbers" runat="server" Height="35px" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>Şikayətin mahiyyəti</td>
                                <td>
                                    <asp:DropDownList ID="DListComplaintType" runat="server" CssClass="form-control" DataValueField="ID" DataTextField="Name"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="table-info">İçracı</td>
                                <td>
                                    <asp:DropDownList ID="DlistExecutiveUsers" runat="server" CssClass="form-control" DataTextField="Fullname" DataValueField="ID"></asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td>Aparata daxil olma tarixi</td>
                                <td>
                                    <asp:TextBox ID="TxtEnterOrganizationsDt" runat="server" Height="35px" CssClass="form-control form_datetime"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>Sektora daxil olma tarixi</td>
                                <td>
                                    <asp:TextBox ID="TxtEnterSectorDt" runat="server" Height="35px" CssClass="form-control form_datetime"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>Ərizəçi</td>
                                <td>
                                    <asp:TextBox ID="TxtApplicantsFullname" runat="server" Height="35px" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>Ərizəçinin ünvanı</td>
                                <td>
                                    <asp:TextBox ID="TxtApplicantsAddress" runat="server" Height="35px" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>Kimin barəsində müraciət edilir</td>
                                <td>
                                    <asp:TextBox ID="TxtVictimsFullname" runat="server" Height="35px" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>Cinsi</td>
                                <td>
                                    <asp:DropDownList ID="DListApplicatsGenderType" runat="server" CssClass="form-control" DataValueField="ID" DataTextField="Name"></asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td>Sosial statusu</td>
                                <td>
                                    <asp:DropDownList ID="DListApplicantsSocialStatus" runat="server" CssClass="form-control" DataValueField="ID" DataTextField="Name"></asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td>Şikayət olunan subyekt (orqan)</td>
                                <td>
                                    <asp:TextBox ID="TxtComplaintInstitution" runat="server" Height="35px" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>Pis rəftar, işkəncə</td>
                                <td>
                                    <asp:CheckBox ID="ChkIsBadTreatment" runat="server" CssClass="Chekbx" />
                                </td>
                            </tr>

                            <tr>
                                <td>Sorğu göndərilən orqan</td>
                                <td>
                                    <asp:TextBox ID="TxtQueriedInstitution" runat="server" Height="35px" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Sorğuya cavab</td>
                                <td>
                                    <asp:TextBox ID="TxtQueriedRespond" runat="server" Height="35px" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Vətəndaşa cavab</td>
                                <td>
                                    <asp:TextBox ID="TxtCitizenRespond" runat="server" Height="35px" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Arayış (*)</td>
                                <td>
                                    <asp:CheckBox ID="ChkIsReference" runat="server" CssClass="Chekbx" />
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
                                    <asp:TextBox ID="TxtResults" runat="server" Height="35px" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="BtnSave" runat="server" Text="Yadda saxla" CssClass="btn btn-success" Height="35px" OnClientClick="this.style.display='none';document.getElementById('load').style.display=''" OnClick="BtnSave_Click" />
                        <img id="load" src="/images/loading.gif" style="display: none" />
                    </div>
                </asp:Panel>
            </div>
        </div>


        <div class="modal fade" id="modalDetails" role="dialog" style="margin-top: 130px">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content" style="width: 800px">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Ətraflı</h4>
                    </div>
                    <div class="modal-body">
                        <a href="#" onclick="printModal();">
                            <span class="glyphicon glyphicon-print"></span>Çap et
                        </a>
                        <br />
                        <br />
                        <table class="table table-bordered table-hover" id="printable">
                            <tr>
                                <td class="table-info"><strong>Fərdi şikayət nömrəsi</strong></td>
                                <td>
                                    <asp:Label ID="LblNumbers" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td><strong>Şikayətin mahiyyəti </strong></td>
                                <td>
                                    <asp:Label ID="LblComplaintType" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td><strong>İcraçı</strong></td>
                                <td>
                                    <asp:Label ID="LblExecutive" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td><strong>Ərizəçi</strong> </td>
                                <td>
                                    <asp:Label ID="LblApplicantsFullname" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td><strong>Ərizəçinin Ünvanı</strong> </td>
                                <td>
                                    <asp:Label ID="LblApplicantsAddress" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td><strong>Kimin barəsində müraciət edilir</strong></td>
                                <td>
                                    <asp:Label ID="LblVictimsFullname" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td><strong>Cinsi</strong> </td>
                                <td>
                                    <asp:Label ID="LblApplicantsGenderType" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td><strong>Sosial statusu</strong> </td>
                                <td>
                                    <asp:Label ID="LblApplicantsSocialStatus" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td><strong>Şikayət olunan subyekt (orqan)</strong> </td>
                                <td>
                                    <asp:Label ID="LblComplaintInstitution" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td><strong>Pis rəftar, işkəncə</strong> </td>
                                <td>
                                    <asp:Label ID="LblIsBadTreatment" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td><strong>Sorğu göndərilən orqan</strong> </td>
                                <td>
                                    <asp:Label ID="LblQueriedInstitution" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><strong>Sorğuya cavab</strong> </td>
                                <td>
                                    <asp:Label ID="LblQueriedRespond" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><strong>Vətəndaşa cavab</strong></td>
                                <td>
                                    <asp:Label ID="LblCitizenRespond" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><strong>Arayış (*)</strong> </td>
                                <td>
                                    <asp:Label ID="LblIsReference" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><strong>Nəticə</strong> </td>
                                <td>
                                    <asp:Label ID="LblResults" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><strong>Aparata daxil olma tarixi</strong> </td>
                                <td>
                                    <asp:Label ID="LblEnterOrganizationsDt" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td><strong>Sektora daxil olma tarixi</strong> </td>
                                <td>
                                    <asp:Label ID="LblEnterSectorDt" runat="server"></asp:Label>
                                </td>
                            </tr>

                        </table>
                    </div>
                </div>
            </div>
        </div>

        <div class="panel panel-default">
            <div class="panel-heading" style="background-color: #f5f5f5; color: black">
                Fərdi şikayətlər
            </div>

            <asp:Panel ID="Panel2" runat="server" class="panel-body" DefaultButton="BtnSearch">
                <br />
                <div class="filter-panel">
                    <div class="filter-item">
                        №<br />
                        <asp:TextBox ID="TxtRowIndexFilter" runat="server" AutoCompleteType="Disabled" autocomplete="off" Height="35px" CssClass="form-control"></asp:TextBox>
                        <br />
                        <br />
                    </div>
                    <div class="filter-item">
                        İcraçı<br />
                        <asp:DropDownList ID="DlistExecutiveUsersFilter" runat="server" CssClass="form-control" DataTextField="Fullname" DataValueField="ID"></asp:DropDownList>
                    </div>
                    <div class="filter-item">
                        Fərdi şikayət nömrəsi<br />
                        <asp:TextBox ID="TxtNumbersFilter" runat="server" Height="35px" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="filter-item">
                        Şikayətin mahiyyəti<br />
                        <asp:DropDownList ID="DListComplaintTypeFilter" runat="server" CssClass="form-control" DataValueField="ID" DataTextField="Name"></asp:DropDownList>
                    </div>

                    <div class="filter-item">
                        Ərizəçi<br />
                        <asp:TextBox ID="TxtApplicantsFullnameFilter" runat="server" Height="35px" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="filter-item">
                        Kim barədə<br />
                        <asp:TextBox ID="TxtVictimsFullnameFilter" runat="server" Height="35px" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="filter-item">
                        Cinsi<br />
                        <asp:DropDownList ID="DListApplicantsGenderTypeFilter" runat="server" CssClass="form-control" DataValueField="ID" DataTextField="Name"></asp:DropDownList>
                    </div>

                    <div class="filter-item">
                        Sosial statusu<br />
                        <asp:DropDownList ID="DListApplicantsSocialStatusFilter" runat="server" CssClass="form-control" DataValueField="ID" DataTextField="Name"></asp:DropDownList>
                    </div>

                    <div class="filter-item">
                        Şikayət olunan subyekt<br />
                        <asp:TextBox ID="TxtComplaintInstitutionFilter" runat="server" Height="35px" CssClass="form-control"></asp:TextBox>
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
                        Aparata daxil olma tarix aralığı (başlanğıc)<br />
                        <asp:TextBox ID="TxtEnterOrganizationsStartDtFilter" runat="server" placeholder="başlama" AutoCompleteType="Disabled" autocomplete="off" Height="35px" CssClass="form-control form_datetime"></asp:TextBox>
                    </div>

                    <div class="filter-item">
                        Aparata daxil olma tarixi aralığı (son)<br />
                        <asp:TextBox ID="TxtEnterOrganizationsEndDtFilter" runat="server" placeholder="bitmə" AutoCompleteType="Disabled" autocomplete="off" Height="35px" CssClass="form-control form_datetime"></asp:TextBox>
                    </div>

                    <div class="filter-item">
                        Sektora daxil olma tarixi aralığı (başlanğıc)<br />
                        <asp:TextBox ID="TxtEnterSectorStartDtFilter" runat="server" AutoCompleteType="Disabled" autocomplete="off" Height="35px" CssClass="form-control form_datetime"></asp:TextBox>
                    </div>

                    <div class="filter-item">
                        Sektora daxil olma tarixi aralığı (son)<br />
                        <asp:TextBox ID="TxtEnterSectorEndDtFilter" runat="server" AutoCompleteType="Disabled" autocomplete="off" Height="35px" CssClass="form-control form_datetime"></asp:TextBox>

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
                    <asp:Button ID="BtnSearch" runat="server" Text="Axtar" Height="35px" Width="120px" CssClass="btn btn-success" OnClientClick="this.style.display='none';document.getElementById('search_load').style.display=''" OnClick="BtnSearch_Click" />
                    <asp:Button ID="BtnClear" runat="server" Text="X" CssClass="btn btn-default" Height="35px" Width="50px" OnClientClick="this.style.display='none';document.getElementById('search_load').style.display=''" OnClick="BtnClear_Click" />
                    <img id="search_load" src="/images/loading.gif" style="display: none" />
                    <hr />
                    <div class="row">
                        <div class="col-md-6">
                            <asp:LinkButton ID="LnkNew" runat="server" OnClick="LnkNew_Click">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/add.png" />
                                <span>Yeni fərdi şikayət</span>
                            </asp:LinkButton>

                            <asp:LinkButton ID="LnkExportExcel" runat="server" OnClick="LnkExportExcel_Click">
                                <img src="/images/excel.png" style="margin-left:10pt;" /> İxrac et
                            </asp:LinkButton><a href="#print" onclick="window.print();">
                                <img src="/images/print.png" style="margin-left: 10pt;" />
                                Çap et</a>
                        </div>
                        <div class="col-md-6 text-right" style="padding-top: 7px">
                            <asp:Label ID="LblCountInfo" Font-Bold="true" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <br />
                </div>
                <asp:GridView ID="GrdIndividualComplaints" AutoGenerateColumns="False" runat="server" BorderWidth="0px" CellPadding="4" Width="100%" CssClass="form-control" Font-Bold="False">
                    <Columns>

                        <asp:BoundField HeaderText="№" DataField="RowIndex">
                            <ItemStyle Font-Bold="true" Width="80px" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="FŞ №" DataField="Numbers">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Şikayətin mahiyyəti" DataField="ComplaintTypeName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="İcraçı" DataField="ExecutiveUsersName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Ərizəçi" DataField="ApplicantsFullname">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="250px" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Kimin barəsində müraciət edilir" DataField="VictimsFullname">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="250px" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Cinsi" DataField="ApplicantsGenderTypeName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="60px" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Sosial satusu" DataField="ApplicantsSocialStatusName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Şikayə olunan subyekt (orqan)" DataField="ComplaintInstitution">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Nəticə" DataField="ComplaintResultTypeName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Aparata daxil olma tarixi" DataField="EnterOrganizations_Dt" DataFormatString="{0:dd.MM.yyyy}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="180px" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Sektora daxil olma tarixi" DataField="EnterSector_Dt" DataFormatString="{0:dd.MM.yyyy}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="180px" />
                        </asp:BoundField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <span class="glyphicon glyphicon-list-alt"></span>
                                <asp:LinkButton ID="LnkDetails" runat="server" CommandName="Details" CommandArgument='<%#Eval("ID")%>' OnClick="LnkDetails_Click">Ətraflı</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <span class="glyphicon glyphicon-pencil"></span>
                                <asp:LinkButton ID="LnkEdit" runat="server" CommandName="Redakte" CommandArgument='<%#Eval("ID")%>' OnClick="LnkEdit_Click">Redaktə et</asp:LinkButton>
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
            window.location.href = '/tools/individualcomplaints/?p=' + num;
        }).find('.pagination');
    }

    function pageLoad() {
        GetPagination($('#HdnTotalCount').val(), $('#HdnPageNumber').val());
        datetime();
    }

</script>
