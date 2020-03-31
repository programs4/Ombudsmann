<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AuditsOrganizations.ascx.cs" Inherits="Tools_Controls_AuditsOrganizations" %>



<asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
    <ContentTemplate>
        <div class="modal fade" id="modalNewAudit" role="dialog" style="margin-top: 130px">
            <asp:Panel ID="PnlPopup" runat="server" class="modal-dialog" DefaultButton="BtnSave">
                <!-- Modal content-->
                <div class="modal-content" style="width: 800px">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Yeni baxış</h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table class="table">
                                    <tr>
                                        <td class="table-info">Regional mərkəz</td>
                                        <td>
                                            <asp:DropDownList ID="DlistRegionalCenter" runat="server" CssClass="form-control" DataValueField="ID" DataTextField="Name"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Üst qurum</td>
                                        <td>
                                            <asp:DropDownList ID="DlistTopOrganization" runat="server" CssClass="form-control" DataValueField="ID" DataTextField="Name" AutoPostBack="True" OnSelectedIndexChanged="DlistTopOrganization_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Qurum</td>
                                        <td>
                                            <asp:DropDownList ID="DListSubOrganization" runat="server" CssClass="form-control" DataValueField="ID" DataTextField="Name"></asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Başçəkmənin forması</td>
                                        <td>
                                            <asp:DropDownList ID="DlistVisitType" runat="server" CssClass="form-control" DataValueField="ID" DataTextField="Name"></asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Aşkar edilmiş problemlər</td>
                                        <td>
                                            <asp:TextBox ID="TxtProblems" runat="server" CssClass="form-control" TextMode="MultiLine" Height="70px"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Tövsiyə</td>
                                        <td>
                                            <asp:TextBox ID="TxtSuggestion" runat="server" CssClass="form-control" TextMode="MultiLine" Height="70px"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Baxış həyata keçirən MPQ üzvləri</td>
                                        <td>
                                            <asp:ListBox ID="DlistUsers" runat="server" SelectionMode="Multiple" data-placeholder="Baxış həyata keçirən MPQ üzvlərin seçin" CssClass="chose" DataValueField="ID" DataTextField="Fullname"></asp:ListBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Baxış tarixi</td>
                                        <td>
                                            <asp:TextBox ID="TxtVisitDate" runat="server" AutoCompleteType="Disabled" autocomplete="off" CssClass="form-control form_datetime"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Qeyd</td>
                                        <td>
                                            <asp:TextBox ID="TxtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Height="70px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                                <td class="table-info"><strong>Regional mərkəz</strong> </td>
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
                                <td><strong>Baxış həyata keçirən MPQ üzvləri</strong> </td>
                                <td>
                                    <asp:Label ID="LblUsers" runat="server" Text=""></asp:Label>

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
            </div>
        </div>

        <div class="panel panel-default">
            <div class="panel-heading" style="background-color: #f5f5f5; color: black">
                Baxış keçirilən müəssisələr
            </div>
            <asp:Panel ID="Panel1" runat="server" class="panel-body" DefaultButton="BtnSearch">
                <br />
                <div class="filter-panel">
                    <div class="filter-item">
                        №:<br />
                        <asp:TextBox ID="TxtRowIndexFilter" AutoCompleteType="Disabled" autocomplete="off" runat="server" Height="35px" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="filter-item">
                        Regional mərkəz:<br />
                        <asp:DropDownList ID="DlistRegionalCenterFilter" DataValueField="ID" DataTextField="Name" runat="server" Height="35px" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="filter-item">
                        Üst Qurum:<br />
                        <asp:DropDownList ID="DListParentOrganizationFilter" DataValueField="ID" DataTextField="Name" runat="server" Height="35px" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DListTopOrganizationFilter_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="filter-item">
                        Qurum:<br />
                        <asp:DropDownList ID="DListSubOrganizationFilter" DataValueField="ID" DataTextField="Name" runat="server" Height="35px" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="filter-item">
                        Başçəkmənin forması:<br />
                        <asp:DropDownList ID="DlistVisitTypeFilter" DataValueField="ID" DataTextField="Name" runat="server" Height="35px" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="filter-item">
                        Başçəkmə tarixi (başlanğıc)<br />
                        <asp:TextBox ID="TxtVisitDtStartFilter" AutoCompleteType="Disabled" autocomplete="off" runat="server" Height="35px" CssClass="form-control form_datetime"></asp:TextBox>
                    </div>

                    <div class="filter-item">
                        Başçəkmə tarixi (son)<br />
                        <asp:TextBox ID="TxtVisitDtEndFilter" AutoCompleteType="Disabled" autocomplete="off" runat="server" Height="35px" CssClass="form-control form_datetime"></asp:TextBox>
                    </div>

                      <div class="filter-item">
                        Qeyd<br />
                        <asp:TextBox ID="TxtDescriptionFilter" runat="server" AutoCompleteType="Disabled" autocomplete="off" Height="35px" CssClass="form-control"></asp:TextBox>
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
                                <span>Yeni baxış</span>
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
                <asp:GridView ID="GrdAuditsOrganizations" AutoGenerateColumns="False" runat="server" BorderWidth="0px" CellPadding="4" Width="100%" CssClass="form-control" Font-Bold="False">
                    <Columns>
                        <asp:BoundField HeaderText="№" DataField="RowIndex">
                            <ItemStyle Font-Bold="true" Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Regional mərkəz" DataField="RegionalCenter">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="250px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Qurum" DataField="Organization">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Başçəkmənin forması" DataField="VisitType">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="250px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Başçəkmə tarixi" DataField="Visit_Dt" DataFormatString="{0:dd.MM.yyyy}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Görüşülən şəxslərin sayı" DataField="MeetingPersonCount">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:BoundField>


                        <asp:TemplateField>
                            <ItemTemplate>
                                <span class="glyphicon glyphicon-list-alt"></span>
                                <asp:LinkButton ID="LnkDetails" runat="server" CommandName="Details" CommandArgument='<%#Eval("ID")%>' OnClick="LnkDetails_Click">Ətraflı</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="80px" />
                        </asp:TemplateField>


                        <asp:TemplateField>
                            <ItemTemplate>
                                <span class="glyphicon glyphicon-user"></span>
                                <a href='/tools/auditsorganizationsmeetingpersons?i=<%#Eval("ID")%>'>Görüşülən şəxslər
                                </a>
                            </ItemTemplate>
                            <ItemStyle Width="150px" />
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
            window.location.href = '/tools/AuditsOrganizations/?p=' + num;
        }).find('.pagination');
    }

    function pageLoad() {
        $(".chose").chosen({
            disable_search_threshold: 10,
            no_results_text: "",
            width: "500px"
        });


        $(".chose-details").chosen({
            disable_search_threshold: 10,
            no_results_text: "",
            width: "500px"
        });

        GetPagination($('#HdnTotalCount').val(), $('#HdnPageNumber').val());
        datetime();
    }
</script>
