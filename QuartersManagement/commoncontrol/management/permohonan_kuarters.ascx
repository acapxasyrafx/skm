<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="permohonan_kuarters.ascx.vb" Inherits="QuartersManagement.permohonan_kuarters" %>

<style>
    .label{
        display: inline-block;
        margin:0;
        padding:5px;
        font-size: 100%;
    }
    .left {
        display:flex;
        flex: initial;
    }
</style>
<table class="fbform" style="width: 100%">
    <tr class="fbform_header">
        <td><span id="MsgTop" runat="server">
            <asp:Label ID="strlbl_top" runat="server"></asp:Label></span></td>
        <td>
            <span class="buttonMenu">
                <a href="#" runat="server" id="SaveFunction">
                    <img title="Save" style="vertical-align: middle;" src="icons/save.png" width="25" height="25" alt="::" />
                </a>
                | <a href="#" id="Refresh" runat="server"><img title="Refresh" style="vertical-align: middle;" src="icons/refresh.png" width="22" height="22" alt="::" /></a>
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
        <td style="width:100px;">Nama</td>
        <td>:</td>
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
        <td>Kewarganegaraan</td>
        <td>:</td>
        <td>
            <h5 class="label" runat="server" id="lblKewarganegaraan"></h5>
        </td>
    </tr>
    <tr>
        <td>Jawatan</td>
        <td>:</td>
        <td>
            <h5 class="label" runat="server" id="lblJawatan"></h5>
        </td>
    </tr>
    <tr>
        <td>No. Tentera</td>
        <td>:</td>
        <td>
            <h5 class="label" runat="server" id="lblNoTentera"></h5>
        </td>
    </tr>
    <tr>
        <td>Tarikh Berkhidmat</td>
        <td>:</td>
        <td>
            <h5 class="label" runat="server" id="lblTarikhMulaBerkhidmat">01/01/2010</h5>
        </td>
    </tr>
    <tr>
        <td>Tarikh Akhir Berkhidmat</td>
        <td>:</td>
        <td>
            <h5 class="label" runat="server" id="lblTarikhAkhirBerkhidmat">01/01/2019</h5>
        </td>
    </tr>
</table>

<table class="fbform">
    <tr class="fbform_mheader">
        <td colspan="3">Butiran Keluarga</td>
    </tr>
    <tr>
        <td style="width:100px;">Bilangan Anak</td>
        <td>:</td>
        <td>
            <asp:TextBox CssClass="label" runat="server" ID="txtBilAnak" Text="1"/>
            <asp:CheckBox Text="Tiada Anak" runat="server" ID="cbTiadaAnak" AutoPostBack="true"/>
        </td>
    </tr>
    <!-- Table anak Nama, IC, Umur-->
    <tr runat="server" visible="false" id="tableBilAnak">
        <td>
            <table>

            </table>
        </td>
    </tr>
    <tr>
        <td>Jenis Tempat Tinggal Akhir</td>
        <td>:</td>
        <td>
            <asp:DropDownList runat="server" CssClass="label" ID="ddlJenisPenempatan">
                <asp:ListItem Value="Rumah Sewa">Rumah Sewa</asp:ListItem>
                <asp:ListItem Value="Wisma">Wisma (Keluarga Di Kampung)</asp:ListItem>
                <asp:ListItem Value="Seberang">Bertugas Di Seberang (Keluarga Berada Di Rumah Sewa Di Malaysia Barat)</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Mula Menetap Dari</td>
        <td>:</td>
        <td>
            <asp:DropDownList CssClass="label" ID="ddlTarikhTinggalHariMula" runat="server"></asp:DropDownList> / 
            <asp:DropDownList CssClass="label"  ID="ddlTarikhTinggalBulanMula" runat="server"></asp:DropDownList> / 
            <asp:DropDownList CssClass="label"  ID="ddlTarikhTinggalTahunMula" runat="server"></asp:DropDownList>
        </td>
    </tr>
</table>

<table class="fbform">
    <tr class="fbform_mheader">
        <td colspan="4">Butiran Permohonan</td>
    </tr>
    <tr>
        <td style="width:100px;">Rumah Dimohon</td>
        <td>:</td>
        <td colspan="2">
            <asp:DropDownList runat="server" ID="ddlSenaraiRumah"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            Untuk yang bertukar pasukan
            <table>
                <tr>
                    <td>Dari Pasukan</td>
                    <td>:</td>
                    <td>
                        <asp:DropDownList CssClass="label" runat="server" ID="ddlPasukanLama"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Ke Pasukan</td>
                    <td>:</td>
                    <td><asp:DropDownList CssClass="label" runat="server" ID="ddlPasukanBaru"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Tarikh Bertukar</td>
                    <td>:</td>
                    <td>
                        <asp:DropDownList CssClass="label"  ID="ddlTarikhTukarHari" runat="server"></asp:DropDownList> / 
                        <asp:DropDownList CssClass="label"  ID="ddlTarikhTukarBulan" runat="server"></asp:DropDownList> / 
                        <asp:DropDownList CssClass="label"  ID="ddlTarikhTukarTahun" runat="server"></asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:CheckBox CssClass="label" runat="server" ID="cbPerakuanPemohon"/>
            <p class="label">Saya dengan ini memohon sebuah Rumah Keluarga mengikut peraturan dan<u><a href="#" target="_blank">Undang-Undang PAT Jil III(3)</a></u> dan mengaku iaitu butiran-butiran yang dinyatakan seperti berikut adalah benar.</p>
            <asp:Label runat="server" ID="lblCheckBoxAlert" Visible=false/>
        </td>
    </tr>
</table>

<table class ="fbform">
    <tr>
        <td><span id ="MsgBottom" runat ="server" ><asp:Label ID ="strlbl_bottom" runat ="server" ></asp:Label></span></td>
    </tr>
</table>