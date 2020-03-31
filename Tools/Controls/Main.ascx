<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Adminn_Tools_Controls_Main" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <ul id="myTab" class="nav nav-pills" role="tablist" style="background-color: #F9F9F9; padding: 5px; border: 1px solid #E4E4E4;">

            <li role="presentation" class="active"><a href="#note" id="note-tab" role="tab" data-toggle="tab" aria-controls="note" aria-expanded="false">
                <span class="glyphicon glyphicon-list-alt"></span>&nbsp;&nbsp;Fərdi qeydlər</a></li>

            <li role="presentation" class=""><a href="#password" role="tab" id="password-tab" data-toggle="tab" aria-controls="password" aria-expanded="true">
                <span class="glyphicon glyphicon-pencil"></span>&nbsp;&nbsp;Şifrəni dəyiş</a></li>
        </ul>

        <div id="myTabContent" class="tab-content" style="padding: 10px 0px 10px 3px; margin-top: 5px">
            <div role="tabpanel" class="tab-pane fade active in" id="note" aria-labelledby="note-tab">
                <asp:Panel ID="PnlRec" runat="server" BackColor="#EFEFEF" CssClass="textBox" DefaultButton="BtnSaveNote">
                    <div class="panel panel-default">
                        <div class="panel-heading" style="background-color: #f5f5f5; color: black">Fərdi qeydlər</div>
                        <div class="panel-body">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <br />
                                    <asp:TextBox ID="TxtDescription" runat="server" BackColor="#FFFFE1" CssClass="form-control" Height="150px" TextMode="MultiLine" Width="70%"></asp:TextBox>
                                    <br />
                                    <br />
                                    <asp:Button ID="BtnSaveNote" runat="server" CssClass="btn btn-default" Height="35px" OnClientClick="this.style.display='none';document.getElementById('load1').style.display='';" OnClick="BtnSaveNote_Click" Text="YADDA SAXLA" />
                                    <div id="load1" style="display: none">
                                        <img src="/images/loading.gif" alt="gif-load" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <br />
                        </div>
                    </div>
                </asp:Panel>
            </div>

            <div role="tabpanel" class="tab-pane fade" id="password" aria-labelledby="password-tab">
                <asp:Panel ID="PnlPassword" runat="server" BackColor="#EFEFEF" CssClass="textBox" DefaultButton="BtnConfirm">
                    <div style="padding: 10px">
                        <div class="panel panel-default">
                            <div class="panel-heading" style="background-color: #f5f5f5; color: black">Şifrəni dəyiş</div>
                            <div class="panel-body no-rtl">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        Köhnə şifrə:<br />
                                        <asp:TextBox ID="TxtOldPassword" runat="server" CssClass="form-control" Height="35px" Width="350px" TextMode="Password"></asp:TextBox>
                                        <br />
                                        <br />
                                        Yeni şifrə:<br />
                                        <asp:TextBox ID="TxtNewPassword" runat="server" CssClass="form-control" Height="35px" TextMode="Password" Width="350px"></asp:TextBox>
                                        <br />
                                        <asp:Label ID="Label1" runat="server" Font-Size="8pt" Text="- minimum 4, maksimum 20 simvol"></asp:Label>
                                        <br />
                                        <br />
                                        Təkrar yeni şifrə:<br />
                                        <asp:TextBox ID="TxtBackPassword" runat="server" CssClass="form-control" Height="35px" TextMode="Password" Width="350px"></asp:TextBox>
                                        <br />
                                        <br />
                                        <asp:Button ID="BtnConfirm" runat="server" CssClass="btn btn-default" Height="35px" OnClientClick="this.style.display='none';document.getElementById('load2').style.display='';" OnClick="BtnConfirm_Click" Text="TƏSDİQ ET" />
                                        <div id="load2" style="display: none">
                                            <img src="/images/loading.gif" alt="gif-load" />
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>
                </asp:Panel>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>

