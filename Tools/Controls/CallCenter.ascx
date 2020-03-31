<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CallCenter.ascx.cs" Inherits="Tools_Controls_CallCenter" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="modal fade" id="modalNewCallCenter" role="dialog" style="margin-top: 130px">

            <asp:Panel ID="Panel2" runat="server" class="modal-dialog" DefaultButton="BtnSave">
                <!-- Modal content-->
                <div class="modal-content" style="width: 800px">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Yeni fərdi şikayət</h4>
                    </div>
                    <div class="modal-body">
                        <table class="table">
                            <%--      <tr>
                                <td class="table-info">Müraciətin kateqoriyası</td>
                                <td>
                                    <asp:DropDownList ID="DListCallCenterType" runat="server" DataTextField="Name" DataValueField="ID" Height="35px" CssClass="form-control"></asp:DropDownList>
                                </td>
                            </tr>--%>

                            <tr>
                                <td class="table-info">Şikayətin növü</td>
                                <td>
                                    <asp:DropDownList ID="DListComplaintType" runat="server" DataTextField="Name" DataValueField="ID" Height="35px" CssClass="form-control"></asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td>Müraciət edənin soyadı, adı və atasının adı</td>
                                <td>
                                    <asp:TextBox ID="TxtApplicantFullname" runat="server" Height="35px" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td class="table-info">Müraciət edənin cinsi</td>
                                <td>
                                    <asp:DropDownList ID="DListApplicantGenderType" runat="server" DataTextField="Name" DataValueField="ID" Height="35px" CssClass="form-control"></asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td class="table-info">Müraciət edənin sosial statusu</td>
                                <td>
                                    <asp:DropDownList ID="DListApplicantSocialStatus" runat="server" DataTextField="Name" DataValueField="ID" Height="35px" CssClass="form-control"></asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td>Mobil nömrə</td>
                                <td>
                                    <asp:TextBox ID="TxtPhoneNumber" runat="server" Height="35px" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>Zəng tarixi</td>
                                <td>
                                    <asp:TextBox ID="TxtCallDtDate" runat="server" CssClass="form-control form_datetime" autocomplete="off" placeholder="tarix" Height="35px" Width="48%"></asp:TextBox>&nbsp/&nbsp<asp:TextBox ID="TxtCallDtTime" runat="server" Height="35px" CssClass="form-control" autocomplete="off" placeholder="saat" Width="48%"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>Kimin barəsində</td>
                                <td>
                                    <asp:TextBox ID="TxtVictimsFullname" runat="server" autocomplete="off" Height="35px" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>Şikayət olunan qurum</td>
                                <td>
                                    <asp:TextBox ID="TxtComplaintInstitution" runat="server" Height="35px" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Saxlanma tarixi (başlanğıc)</td>
                                <td>
                                    <asp:TextBox ID="TxtStartPunishmentPeriod" runat="server" AutoCompleteType="Disabled" autocomplete="off" Height="35px" CssClass="form-control form_datetime"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Saxlanma tarixi (son)</td>
                                <td>
                                    <asp:TextBox ID="TxtEndPunishmentPeriod" runat="server" AutoCompleteType="Disabled" autocomplete="off" Height="35px" CssClass="form-control form_datetime"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>Nəticə</td>
                                <td>
                                    <asp:DropDownList ID="DListComplaintResultType" runat="server" CssClass="form-control" DataValueField="ID" DataTextField="Name"></asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td>Nəticənin təsviri</td>
                                <td>
                                    <asp:TextBox ID="TxtResults" runat="server" Height="35px" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>Qeyd</td>
                                <td>
                                    <asp:TextBox ID="TxtDescriptions" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>

                        </table>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="BtnSave" runat="server" Text="Yadda saxla" CssClass="btn btn-success" OnClientClick="this.style.display='none';document.getElementById('load').style.display=''" OnClick="BtnSave_Click" />
                        <img id="load" src="/images/loading.gif" style="display: none" />
                    </div>
                </div>
            </asp:Panel>
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
                                <td style="width: 35%"><strong>Müraciətin kateqoriyası</strong></td>
                                <td>
                                    <asp:Label ID="LblCallCenterTypeName" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td><strong>Şikayətin növü</strong></td>
                                <td>
                                    <asp:Label ID="LblComplaintType" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td><strong>Müraciət edənin soyadı, adı və atasının adı</strong> </td>
                                <td>
                                    <asp:Label ID="LblApplicantFullname" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td><strong>Müraciət edənin cinsi</strong></td>
                                <td>
                                    <asp:Label ID="LblGenderType" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td><strong>Müraciət edənin sosial statusu</strong></td>
                                <td>
                                    <asp:Label ID="LblSocialStatus" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td><strong>Mobil nömrə</strong></td>
                                <td>
                                    <asp:Label ID="LblPhoneNumber" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td><strong>Zəng tarixi</strong> </td>
                                <td>
                                    <asp:Label ID="LblCallDt" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td><strong>Kimin barəsində</strong> </td>
                                <td>
                                    <asp:Label ID="LblVictimsFullname" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td><strong>Şikayət olunan qurum</strong> </td>
                                <td>
                                    <asp:Label ID="LblComplaintInstitution" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><strong>Saxlanma tarixi (başlanğıc)</strong> </td>
                                <td>
                                    <asp:Label ID="LblStartPunishmentPeriod" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><strong>Saxlanma tarixi (son)</strong></td>
                                <td>
                                    <asp:Label ID="LblEndPunishmentPeriod" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td><strong>Nəticə</strong> </td>
                                <td>
                                    <asp:Label ID="LblComplaintResultType" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td><strong>Nəticənin təsviri</strong> </td>
                                <td>
                                    <asp:Label ID="LblResults" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td><strong>Qeyd</strong> </td>
                                <td>
                                    <asp:Label ID="LblDescriptions" runat="server"></asp:Label>
                                </td>
                            </tr>

                        </table>
                    </div>
                </div>
            </div>
        </div>

        <div class="panel panel-default">
            <div class="panel-heading" style="background-color: #f5f5f5; color: black">
                Çağrı mərkəzinə gələn şikayətlər
            </div>

            <asp:Panel ID="Panel1" runat="server" class="panel-body" DefaultButton="BtnSearch">
                <br />
                <div class="filter-panel">
                    <div class="filter-item">
                        №
                        <br />
                        <asp:TextBox ID="TxtRowIndexFilter" runat="server" Height="35px" CssClass="form-control"></asp:TextBox>
                    </div>
                    <%-- <div class="filter-item">
                        Müraciətin kateqoriyası<br />
                        <asp:DropDownList ID="DlistCallCenterTypeFilter" DataValueField="ID" DataTextField="Name" runat="server" Height="35px" CssClass="form-control">
                        </asp:DropDownList>
                    </div>--%>

                    <div class="filter-item">
                        Şikayətin növü<br />
                        <asp:DropDownList ID="DListComplaintTypeFilter" DataValueField="ID" DataTextField="Name" runat="server" Height="35px" CssClass="form-control">
                        </asp:DropDownList>
                    </div>

                    <div class="filter-item">
                        Müraciət edənin soyadı,adı və ata adı<br />
                        <asp:TextBox ID="TxtApplicantFullnameFilter" runat="server" Height="35px" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="filter-item">
                        Cinsi<br />
                        <asp:DropDownList ID="DListApplicantGenderTypeFilter" DataValueField="ID" DataTextField="Name" runat="server" Height="35px" CssClass="form-control">
                        </asp:DropDownList>
                    </div>

                    <div class="filter-item">
                        Sosial statusu<br />
                        <asp:DropDownList ID="DListApplicantSocialStatusFilter" DataValueField="ID" DataTextField="Name" runat="server" Height="35px" CssClass="form-control">
                        </asp:DropDownList>
                    </div>

                    <div class="filter-item">
                        Mobil nömrə<br />
                        <asp:TextBox ID="TxtPhoneNumberFilter" runat="server" Height="35px" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="filter-item">
                        Kim barədə<br />
                        <asp:TextBox ID="TxtVictimsFullnameFilter" runat="server" Height="35px" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="filter-item">
                        Nəticə<br />
                        <asp:DropDownList ID="DListComplaintResultTypeFilter" runat="server" CssClass="form-control" DataValueField="ID" DataTextField="Name"></asp:DropDownList>
                    </div>

                    <div class="filter-item">
                        Nəticənin təsviri<br />
                        <asp:TextBox ID="TxtResultsFilter" runat="server" AutoCompleteType="Disabled" autocomplete="off" Height="35px" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="filter-item">
                        Zəng tarixi(başlanğıc)<br />
                        <asp:TextBox ID="TxtCallDtStartFilter" runat="server" AutoCompleteType="Disabled" autocomplete="off" Height="35px" CssClass="form-control form_datetime"></asp:TextBox>
                    </div>
                    <div class="filter-item">
                        Zəng tarixi(son)<br />
                        <asp:TextBox ID="TxtCallDtEndFilter" runat="server" AutoCompleteType="Disabled" autocomplete="off" Height="35px" CssClass="form-control form_datetime"></asp:TextBox>
                    </div>
                    <div class="filter-item">
                        Saxlanma tarixi (başlanğıc)<br />
                        <asp:TextBox ID="TxtStartPunishmentPeriodFilter" AutoCompleteType="Disabled" autocomplete="off" runat="server" Height="35px" CssClass="form-control form_datetime"></asp:TextBox>
                    </div>

                    <div class="filter-item">
                        Saxlanma tarixi (son)<br />
                        <asp:TextBox ID="TxtEndPunishmentPeriodFilter" AutoCompleteType="Disabled" autocomplete="off" runat="server" Height="35px" CssClass="form-control form_datetime"></asp:TextBox>
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
                    <asp:Button ID="BtnSearch" runat="server" Text="Axtar" CssClass="btn btn-success" Height="35px" Width="120px" OnClientClick="this.style.display='none';document.getElementById('search_load').style.display=''" OnClick="BtnSearch_Click" />
                    <asp:Button ID="BtnClear" runat="server" Text="X" CssClass="btn btn-default" Height="35px" Width="50px" OnClientClick="this.style.display='none';document.getElementById('search_load').style.display=''" OnClick="BtnClear_Click" />
                    <img id="search_load" src="/images/loading.gif" style="display: none" />
                    <hr />
                    <div class="row">
                        <div class="col-md-6">
                            <asp:LinkButton ID="LnkNew" runat="server" OnClick="LnkNew_Click">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/add.png" />
                                <span>Yeni şikayət</span>
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
                <asp:GridView ID="GrdCallCenter" AutoGenerateColumns="False" runat="server" BorderWidth="0px" CellPadding="4" Width="100%" CssClass="form-control" Font-Bold="False">
                    <Columns>
                        <asp:BoundField HeaderText="№" DataField="RowIndex">
                            <ItemStyle Font-Bold="true" Width="80px" />
                        </asp:BoundField>

                        <%--  <asp:BoundField HeaderText="Müraciətin kateqoriyası" DataField="CallCenterTypeName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="250px" />
                        </asp:BoundField>--%>

                        <asp:BoundField HeaderText="Şikayətin növü" DataField="ComplaintTypeName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="250px" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Müraciət edənin S.A.A" DataField="ApplicantFullname">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="250px" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Cinsi" DataField="ApplicantGenderTypeName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="250px" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Sosial Status" DataField="ApplicantSocialStatusName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="250px" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Mobil nömrə" DataField="PhoneNumber">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="250px" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Kim barədə" DataField="VictimsFullname">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Nəticə" DataField="ComplaintResultTypeName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Zəng tarixi" DataField="Call_Dt" DataFormatString="{0:dd.MM.yyyy}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Saxlanma tarixi (başlanğıc)" DataField="StartPunishmentPeriod" DataFormatString="{0:dd.MM.yyyy}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Saxlanma tarixi (son)" DataField="EndPunishmentPeriod" DataFormatString="{0:dd.MM.yyyy}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:BoundField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <span class="glyphicon glyphicon-pencil"></span>
                                <asp:LinkButton ID="LnkDetails" runat="server" CommandName="Details" CommandArgument='<%#Eval("ID")%>' OnClick="LnkDetails_Click">Ətraflı</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="80px" />
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <span class="glyphicon glyphicon-list-alt"></span>
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
            window.location.href = '/tools/CallCenter/?p=' + num;
        }).find('.pagination');
    }

    function pageLoad() {
        GetPagination($('#HdnTotalCount').val(), $('#HdnPageNumber').val());
        datetime();
    }
</script>
