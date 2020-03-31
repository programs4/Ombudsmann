<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Datalist.ascx.cs" Inherits="Tools_Controls_Datalist" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="modal fade" id="modalNewOrganization" role="dialog" style="margin-top: 130px">
            <div class="modal-dialog">
                <!-- Modal content-->
                <asp:Panel ID="Panel1" runat="server" class="modal-content" Style="width: 800px" DefaultButton="BtnSave">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Yeni qurum</h4>
                    </div>
                    <div class="modal-body">
                        <table class="table">
                            <tr>
                                <td>Tabeçiliyində olduğu qurum</td>
                                <td>
                                    <asp:DropDownList ID="DListTopOrganization" runat="server" Height="35px" DataTextField="Name" DataValueField="ID" CssClass="form-control"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="table-info">Qurumun adı</td>
                                <td>
                                    <asp:TextBox ID="TxtOrganizationName" runat="server" Height="35px" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="table-info">Qurumun adı</td>
                                <td>
                                    <asp:CheckBox ID="ChkAktiv" runat="server" Text="Silinmiş" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="BtnSave" runat="server" Text="Yadda saxla" Height="35px" CssClass="btn btn-success" OnClick="BtnSave_Click" />
                    </div>
                </asp:Panel>
            </div>
        </div>

        <div class="panel panel-default">
            <div class="panel-heading" style="background-color: #f5f5f5; color: black">
                Sorağçalar
            </div>
            <div class="panel-body">
                <asp:Panel ID="PnlSearch" runat="server" DefaultButton="BtnSearch">
                    <div class="row">
                        <div class="col-md-2">
                            Qurum:<br />
                            <asp:DropDownList ID="DListTopOrganizationFilter" DataValueField="ID" DataTextField="Name" runat="server" Height="35px" CssClass="form-control">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-2">
                            Qurumun adı<br />
                            <asp:TextBox ID="TxtNameFilter" runat="server" CssClass="form-control" Height="35px"></asp:TextBox><br />
                        </div>

                        <div class="col-md-2">
                            <br />
                            <asp:Button ID="BtnSearch" runat="server" Text="Axtar" CssClass="btn btn-success" Height="35px" Width="120px" OnClick="BtnSearch_Click" />
                        </div>

                    </div>

                </asp:Panel>
                <hr />
                <asp:LinkButton ID="LnkNew" runat="server" OnClick="LnkNew_Click">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/add.png" />
                    <span>Yeni qurum</span>
                </asp:LinkButton>
                <br />
                <br />
                <asp:GridView ID="GrdOrganizations" AutoGenerateColumns="False" runat="server" BorderWidth="0px" CellPadding="4" Width="100%" CssClass="form-control" Font-Bold="False">
                    <Columns>

                        <asp:BoundField HeaderText="Adı" DataField="Name">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:CheckBoxField DataField="IsDeleted" ReadOnly="True" Text="Silinmiş">
                            <ItemStyle Width="100px" />
                        </asp:CheckBoxField>

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
            </div>
        </div>
    </ContentTemplate>
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
            window.location.href = '/tools/datalist/?p=' + num;
        }).find('.pagination');
    }
</script>
