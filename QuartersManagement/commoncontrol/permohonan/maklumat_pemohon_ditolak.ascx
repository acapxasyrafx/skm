<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="maklumat_pemohon_ditolak.ascx.vb" Inherits="QuartersManagement.maklumat_pemohon_ditolak" %>
<style>
    .label{
        display: inline-block;
        margin:0;
        padding:5px;
        font-size: 100%;
        font-weight:normal;
    }
    .wrapper{
        display:flex;
        justify-content: space-evenly;
    }
    .left{
        display:block;
        width: 50%;
    }
    .right{
        display:block;
        width: 50%;
    }
    .tblLabel{
        width: 150px;
    }
    .tblColon{
        width: 5px;
    }
    .wrapper{
        display: flex;
        height: 100%;
        justify-content:space-evenly;
    }
    .right{
        width: 50%;
        display: block;
    }
    .left{
        width: 50%;
        display: block;
    }
    .auto-style2 {
        height: 100%;
        width: 14px;
    }
    .auto-style3 {
        width: 14px;
    }
    .auto-style4 {
        width: 4px;
    }
</style>
<table class="fbform" style="width: 100%">
    <tr class="fbform_header">
        <td><span id="MsgTop" runat="server">
            <asp:Label ID="strlbl_top" runat="server"></asp:Label></span></td>
        <td>
            <span class="buttonMenu">
                <a href="#" id="Refresh" runat="server"><img title="Refresh" style="vertical-align: middle;" src="icons/refresh.png" width="22" height="22" alt="::" /></a>
                | <a href="#" id="Help"><img title="Help" style="vertical-align: middle;" src="icons/help.png" width="22" height="22" alt="::" /></a>
            </span>
        </td>
    </tr>
</table>

<div class="wrapper">
    <div class="left">
        <table class="fbform" style="width: 100%;">
            <tr class="fbform_mheader">
                <td colspan="3">Butiran Peribadi</td>
            </tr>
            <tr>
                <td class="tblLabel">No. Tentera</td>
                <td class="tblColon">:</td>
                <td>
                    <h5 class="label" runat="server" id="lblNoTentera"></h5>
                </td>
            </tr>
            <tr>
                <td class="tblLabel">Jawatan</td>
                <td class="tblColon">:</td>
                <td>
                    <h5 class="label" runat="server" id="lblJawatan"></h5>
                </td>
            </tr>
            <tr>
                <td class="tblLabel">Nama</td>
                <td class="tblColon">:</td>
                <td>
                    <h5 class="label" runat="server" id="lblNama"></h5>
                    <asp:HiddenField runat="server" ID="pID" Value="" />
                </td>
            </tr>
            <tr>
                <td class="tblLabel">Jantina</td>
                <td class="tblColon">:</td>
                <td>
                    <h5 class="label" runat="server" id="lblJantina"></h5>
                </td>
            </tr>
            <tr>
                <td class="tblLabel">Tarikh Lahir</td>
                <td class="tblColon">:</td>
                <td>
                    <h5 class="label" runat="server" id="lblTarikhLahir"></h5>
                </td>
            </tr>
            <tr>
                <td class="tblLabel">Status Perkahwinan</td>
                <td class="tblColon">:</td>
                <td>
                    <h5 class="label" runat="server" id="lblStatusPerkahwinan"></h5>
                </td>
            </tr>
            <tr>
                <td class="tblLabel">Tarikh Mula Berkhidmat</td>
                <td class="tblColon">:</td>
                <td>
                    <h5 class="label" runat="server" id="lblTarikhMulaBerkhidmat">01/01/2010</h5>
                </td>
            </tr>
            <tr>
                <td class="tblLabel">Tarikh Tamat Perkhidmatan</td>
                <td class="tblColon">:</td>
                <td>
                    <h5 class="label" runat="server" id="lblTarikhAkhirBerkhidmat"></h5>
                </td>
            </tr>
        </table>
        <table class="fbform" style="width: 100%;" runat="server" id="divMaklumatAnak">
            <tr class="fbform_mheader">
                <td colspan="3">Maklumat Anak</td>
            </tr>
            <tr>
                <td>
                    <asp:GridView
                        ID="datRespondent"
                        runat="server"
                        DataKeyNames="historyAnak_id"
                        AutoGenerateColumns="False"
                        AllowPaging="false"
                        CellPadding="4"
                        ForeColor="#333333"
                        GridLines="None"
                        Width="100%"
                        PageSize="100"
                        CssClass="gridview_footer">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%" />
                                <ItemStyle VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Nama">
                                <ItemTemplate>
                                    <asp:Label ID="lblNamaAnak" runat="server" Text='<%# Bind("historyAnak_nama")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="IC">
                                <ItemTemplate>
                                    <asp:Label ID="lblICAnak" runat="server" Text='<%# Bind("historyAnak_ic")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="UMUR">
                                <ItemTemplate>
                                    <asp:Label ID="lblUmurAnak" runat="server" Text='<%# icToAge(Eval("historyAnak_ic"))%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Underline="true" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                            HorizontalAlign="Center" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <table class="fbform" style="width: 100%;">
            <tr class="fbform_mheader">
                <td colspan="3">Butiran Keluarga</td>
            </tr>
            <tr>
                <td class="tblLabel">Jenis Tempat Tinggal Akhir</td>
                <td class="tblColon">:</td>
                <td><h5 class="label" runat="server" id="lblJenisPenempatan"></h5></td>
            </tr>
            <tr>
                <td class="tblLabel">Mula Menetap Dari</td>
                <td class="tblColon">:</td>
                <td><h5 class="label" runat="server" id="lbltarikhPenempatan"></h5></td>
            </tr>
        </table>
    </div>
    <div class="right">
        <table class="fbform" style="width: 100%;">
            <tr class="fbform_mheader">
                <td colspan="3">Butiran Permohonan</td>
            </tr>
            <tr>
                <td class="auto-style3">Pangkalan</td>
                <td class="auto-style4">:</td>
                <td><h5 class="label" runat="server" id="lblPangkalanDimohon"></h5></td>
            </tr>
            <tr>
                <td class="auto-style3">Kuarters/Rumah</td>
                <td class="auto-style4">:</td>
                <td><h5 class="label" runat="server" id="lblKuartersDimohon"></h5></td>
            </tr>
            <tr>
                <td colspan="3">
                    <table runat="server" id="tblBertukar" visible="false">
                        <tr>
                            <td class="tblLabel">Dari Pasukan</td>
                            <td class="tblColon">:</td>
                            <td>
                                <h5 class="label" runat="server" id="lbl_pasukanLama"></h5>
                            </td>
                        </tr>
                        <tr>
                            <td class="tblLabel">Ke Pasukan</td>
                            <td class="tblColon">:</td>
                            <td>
                                <h5 class="label" runat="server" id="lbl_pasukanBaru"></h5>

                            </td>
                        </tr>
                        <tr>
                            <td class="tblLabel">Tarikh Bertukar</td>
                            <td class="tblColon">:</td>
                            <td>
                                <h5 class="label" runat="server" id="lbl_tarikhBertukar"></h5>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3"></td>
            </tr>
            <tr class="fbform_mheader">
                <td colspan="3">Sebab Permohonan Ditolak</td>
            </tr>
            <tr>
                <td colspan="3" class="auto-style2">
                    <h4><asp:Label runat="server" ID="lblsebabTolak"></asp:Label></h4>
                </td>
            </tr>
        </table>
    </div>
</div>

<table>
    <tr>
        <td colspan="3"></td>
    </tr>
</table>
<table class ="fbform">
    <tr>
        <td><span id ="MsgBottom" runat ="server" ><asp:Label ID ="strlbl_bottom" runat ="server" ></asp:Label></span></td>
    </tr>

</table>