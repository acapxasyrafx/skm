<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="maklumat_pemohon_menunggu.ascx.vb" Inherits="QuartersManagement.maklumat_pemohon_menunggu" %>

<style>
    .label{
        display: inline-block;
        margin:0;
        padding:5px;
        font-size: 100%;
        font-weight:normal;
    }
    .left {
        display:flex;
        flex: initial;
    }
    .auto-style1 {
        height: 30px;
    }
    .auto-style2 {
        width: 18px;
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


<table class="fbform" style="width:100%;">
    <tr class="fbform_mheader">
        <td colspan="3">Butiran Peribadi</td>
    </tr>
    <tr>
        <td style="width: 150px;">No. Tentera</td>
        <td style="width: 5px;">:</td>
        <td>
            <h5 class="label" runat="server" id="lblNoTentera"></h5>
        </td>
    </tr>
    <tr>
        <td style="width: 150px;">Jawatan</td>
        <td style="width: 5px;">:</td>
        <td>
            <h5 class="label" runat="server" id="lblJawatan"></h5>
        </td>
    </tr>
    <tr>
        <td style="width:100px;">Nama</td>
        <td style="width:5px;">:</td>
        <td>
            <h5 class="label" runat="server" id="lblNama"></h5>
            <asp:HiddenField runat="server" ID="pengguna_id" Value="" />
        </td>
    </tr>
    <tr>
        <td>Jantina</td>
        <td>:</td>
        <td>
            <h5 class="label" runat="server" id="lblJantina"></h5>
        </td>
    </tr>
    <tr>
        <td>Tarikh Lahir</td>
        <td>:</td>
        <td>
            <h5 class="label" runat="server" id="lblTarikhLahir"></h5>
        </td>
    </tr>
    <tr>
        <td>Tarikh Mula Berkhidmat</td>
        <td>:</td>
        <td>
            <h5 class="label" runat="server" id="lblTarikhMulaBerkhidmat"/>
        </td>
    </tr>
    <tr>
        <td>Tarikh Tamat Perkhidmatan</td>
        <td>:</td>
        <td>
            <h5 class="label" runat="server" id="lblTarikhAkhirBerkhidmat"/>
        </td>
    </tr>
</table>

<table class="fbform" style="width: 100%;">
    <tr class="fbform_mheader">
        <td>Maklumat Anak</td>
    </tr>
    <tr>
        <td style="width: 100%;">
            <asp:GridView
                ID="datRespondent"
                runat="server"
                DataKeyNames="anak_id"
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
                            <asp:Label ID="lblNamaAnak" runat="server" Text='<%# Bind("anak_nama")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="IC">
                        <ItemTemplate>
                            <asp:Label ID="lblICAnak" runat="server" Text='<%# Bind("anak_ic")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="UMUR">
                        <ItemTemplate>
                            <asp:Label ID="lblUmurAnak" runat="server" Text='<%# Bind("anak_umur")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
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

<table class="fbform">
    <tr class="fbform_mheader">
        <td colspan="3">Maklumat Keluarga</td>
    </tr>
    <tr>
        <td style="width: 150px;">Jenis Tempat Tinggal Akhir</td>
        <td>:</td>
        <td>
            <h5 class="label" runat="server" id="lblJenisPenempatan"></h5>
        </td>
    </tr>
    <tr>
        <td>Mula Menetap Dari</td>
        <td>:</td>
        <td>
            <h5 class="label" runat="server" id="lbltarikhPenempatan"></h5>
        </td>
    </tr>
</table>

<table class="fbform" style="width:100%;">
    <tr class="fbform_mheader">
        <td colspan="4">Butiran Permohonan</td>
    </tr>
    <tr>
        <td style="width:100px;">Pangkalan</td>
        <td class="auto-style2">:</td>
        <td colspan="2">
            <h5 class="label" runat="server" id="lbl_senaraiPangkalan"></h5>
        </td>
    </tr>
    <tr>
        <td style="width:100px;">Kuarters/Rumah</td>
        <td class="auto-style2">:</td>
        <td colspan="2">
            <h5 class="label" runat="server" id="lbl_senaraiKuarters"></h5>
            <asp:label runat ="server" >Kekosongan unit : </asp:label>
        </td>
    </tr>
    <tr>
        <td colspan="4">
           
            <table runat="server" id="tblBertukar" visible="false">
                <tr>
                    <td>Dari Pasukan</td>
                    <td>:</td>
                    <td>
                        <h5 class="label" runat="server" id="lbl_pasukanLama"></h5>
                    </td>
                </tr>
                <tr>
                    <td>Ke Pasukan</td>
                    <td>:</td>
                    <td>
                        <h5 class="label" runat="server" id="lbl_pasukanBaru"></h5>

                    </td>
                </tr>
                <tr>
                    <td>Tarikh Bertukar</td>
                    <td>:</td>
                    <td>
                        <h5 class="label" runat="server" id="lbl_tarikhBertukar"></h5>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button runat ="server" ID="TerimaPermohonanKuarters" text="Terima Permohonan Kuarters"/>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="3"></td>
    </tr>
    <tr class="fbform_mheader">
        <td colspan="3">Cadangan Kuarters</td>
    </tr>
    <tr>
        <td class="auto-style38">
            Kuarters 1
        </td>
        <td class="auto-style2">:</td>
        <td>
            <asp:DropDownList ID="ddlcadanganUnit1" runat="server"></asp:DropDownList>
            <asp:label runat ="server" >Kekosongan unit : </asp:label>
        </td>
    </tr>
    <tr>
        <td class="auto-style38">
            Kuarters 2
        </td>
        <td class="auto-style2">:</td>
        <td>
            <asp:DropDownList ID="ddlcadanganUnit2" runat="server"></asp:DropDownList>
            <asp:label runat ="server" >Kekosongan unit : </asp:label>
        </td>
    </tr>
    <tr>
        <td class="auto-style38">
            Kuarters 3
        </td>
        <td class="auto-style2">:</td>
        <td>
            <asp:DropDownList ID="ddlcadanganUnit3" runat="server"></asp:DropDownList>
            <asp:label runat ="server" >Kekosongan unit : </asp:label>
        </td>
    </tr>
    <tr><td colspan="3" class="auto-style1"> 
        <asp:ImageButton runat="server" ID="btnImg_ditolak" Height="39px" CommandName ="Cadangan" CommandArgument ='<%#Eval("permohonan_id")%>' OnClientClick="javascript:return confirm('Adakah anda pasti mahu menghantar cadangan kuarters ini?')" ImageUrl="~/icons/send.png" ToolTip="Diterima" />
        <asp:Label runat="server" >Hantar Cadangan Kuarters</asp:Label>
        </td></tr>

</table>



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