<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Users.ascx.cs" Inherits="Tools_Controls_Users" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="modal fade" id="modalNewUser" role="dialog" style="margin-top: 130px">

            <asp:Panel ID="Panel1" runat="server" DefaultButton="BtnSave" class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content" style="width: 800px">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Yeni istifadəçi</h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                            <ContentTemplate>
                                <table class="table">
                                    <tr>
                                        <td class="table-info">İstifadəçinin statusu</td>
                                        <td>

                                            <asp:DropDownList ID="DlistUsersStatus" runat="server" DataTextField="Name" DataValueField="ID" CssClass="form-control" OnSelectedIndexChanged="DlistUsersStatus_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            <br />
                                            <p style="font-size: 8pt; color: #585858; padding-top: 10px;">
                                                <b>İşçi </b>- sistemə giriş hüququ olmur, sadəcə siyahılarda əks olunur.<br></br>
                                                <b>Nəzarətçi </b>- sistemə giriş imkanı olmaqla yanaşı, bütün bölmələrə baxmaq hüququna malik olur<br></br>
                                                <b>İstifadəçi </b>- sistemə giriş imkanı olmaqla yanaşı, bütün bölmələrə baxmaq və əlavə etmək imkanı olur<br></br>
                                                <b>Əməliyyatçı </b>- yuxarıdakı bütün hüququlara malik olmaqla yanaşı, istifadəçi əlavə edə bilir
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Regional mərkəz</td>
                                        <td>
                                            <asp:DropDownList ID="DlistRegionalCenter" runat="server" DataTextField="Name" DataValueField="ID" CssClass="form-control"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <asp:Panel ID="PnlUsernamePassword" runat="server" Visible="false">
                                        <tr>
                                            <td>İstifadəçi adı</td>
                                            <td>
                                                <asp:TextBox ID="TxtUsersName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Şifrə</td>
                                            <td>
                                                <asp:TextBox ID="TxtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                                <asp:Label ID="LblWarning" runat="server" Font-Size="11px" Text="(Şifrəni yeniləmək istəmədiyiniz halda boş saxlayın. Əks halda köhnə şifrə sıfırlanacaqdır.)"></asp:Label>
                                            </td>
                                        </tr>
                                        <asp:Panel ID="PnlPasswordRepeat" runat="server">
                                            <tr>
                                                <td>Şifrə təkrar</td>
                                                <td>
                                                    <asp:TextBox ID="TxtPasswordRepeat" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                    </asp:Panel>

                                    <tr>
                                        <td>Soyadı, adı və atasının adı</td>
                                        <td>
                                            <asp:TextBox ID="TxtFullname" runat="server" CssClass="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Əlaqə nömrəsi</td>
                                        <td>
                                            <asp:TextBox ID="TxtContacts" runat="server" CssClass="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Vəzifə</td>
                                        <td>
                                            <asp:TextBox ID="TxtPositions" runat="server" CssClass="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Qeyd</td>
                                        <td>
                                            <asp:TextBox ID="TxtDescriptions" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="BtnSave" runat="server" Text="Yadda saxla" Height="35px" CssClass="btn btn-success" OnClick="BtnSave_Click" />
                    </div>
                </div>
            </asp:Panel>
        </div>

        <div class="panel panel-default">
            <div class="panel-heading" style="background-color: #f5f5f5; color: black">
                İstifadəçilər və əməkdaşlar
            </div>
            <div class="panel-body">
                <asp:Panel ID="PnlSearch" runat="server" DefaultButton="BtnSearch" CssClass="printStyle">
                    Soyadı, adı və atasının adı<br />
                    <asp:TextBox ID="TxtFullnameFilter" runat="server" CssClass="form-control" Height="35px" Width="350PX"></asp:TextBox>
                    <asp:Button ID="BtnSearch" runat="server" Text="Axtar" CssClass="btn btn-success" Height="35px" Width="80px" OnClick="BtnSearch_Click" />

                    <hr />
                    <asp:LinkButton ID="LnkNew" runat="server" OnClick="LnkNew_Click">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/add.png" />
                        <span>Yeni istifadəçi</span>
                    </asp:LinkButton><a href="#print" onclick="window.print();">
                        <img src="/images/print.png" style="margin-left: 10pt;" />
                        Çap et</a>


                    <br />
                    <br />
                </asp:Panel>
                <asp:GridView ID="GrdUsers" AutoGenerateColumns="False" runat="server" BorderWidth="0px" CellPadding="4" Width="100%" CssClass="form-control" Font-Bold="False">
                    <Columns>
                        <asp:BoundField HeaderText="Soyadı, adı və atasının adı" DataField="Fullname">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Regional mərkəz" DataField="RegionalCenterName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="İstifadəçi adı" DataField="Username">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Əlaqə nömrəsi" DataField="Contacts">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Vəzifə" DataField="Positions">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="İstifadəçinin statusu" DataField="UserStatusName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Regional mərkəz" DataField="RegionalCenterName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LnkDeleted" runat="server" CommandName="Deaktiv" CommandArgument='<%#Eval("ID")%>' OnClientClick="return confirm('İstifadəçini silmək istədiyinizdən əminsinizmi?')" OnClick="LnkDeleted_Click" Visible='<%#Eval("ID")._ToString()!=DALC._GetAdministratorsLogin.ID.ToString() %>'> <span class="glyphicon glyphicon-trash"></span> Sil</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="80px" />
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LnkEdit" runat="server" CommandName="Redakte" CommandArgument='<%#Eval("ID")%>' OnClick="LnkEdit_Click" Visible='<%#Eval("ID")._ToString()!=DALC._GetAdministratorsLogin.ID.ToString() %>'> <span class="glyphicon glyphicon-pencil"></span> Redaktə et</asp:LinkButton>
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
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

