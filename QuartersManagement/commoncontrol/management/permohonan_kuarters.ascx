<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="permohonan_kuarters.ascx.vb" Inherits="QuartersManagement.permohonan_kuarters" %>
<table class="fbform" style="width: 100%">
    <tr class="fbform_header">
        <td><span id="MsgTop" runat="server">
            <asp:Label ID="strlbl_top" runat="server"></asp:Label></span></td>
        <td>
            <span class="buttonMenu"><a href="#" runat="server" id="SaveFunction">
                <img title="Save" style="vertical-align: middle;" src="icons/save.png" width="25" height="25" alt="::" /></a>
                | <a href="#" id="Refresh" runat="server"><img title="Refresh" style="vertical-align: middle;" src="icons/refresh.png" width="22" height="22" alt="::" /></a>
                | <a href="#" id="Help"><img title="Help" style="vertical-align: middle;" src="icons/help.png" width="22" height="22" alt="::" /></a>
            </span>
        </td>
    </tr>
</table>

<table class="fbform" style="width:100%">
    <tr class="fbform_mheader">
        <td colspan="6">Butiran Peribadi</td>
    </tr>
    <tr>
        <td>Nama</td>
        <td>:</td>
        <td colspan="4">
            <asp:TextBox runat="server" ID="txtNama"/>
        </td>
    </tr>
    <tr>
        <td>Jantina</td>
        <td>:</td>
        <td colspan="4">
            <asp:DropDownList ID="ddlJantina" runat="server">
                <asp:ListItem Value="">- PILIH -</asp:ListItem>
                <asp:ListItem Value="Lelaki">LELAKI</asp:ListItem>
                <asp:ListItem Value="Perempuan">PEREMPUAN</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Taikh Lahir</td>
        <td>
            <asp:DropDownList ID="ddlLahirHari" runat="server"></asp:DropDownList> / 
            <asp:DropDownList ID="ddlLahirBulan" runat="server"></asp:DropDownList> / 
            <asp:DropDownList ID="ddlLahirTahun" runat="server"></asp:DropDownList>
        </td>
        <td>Status Kewarganegaraan</td>
        <td>:</td>
        <td>
            <asp:DropDownList ID="ddlKewarganegaraan" runat="server" AutoPostBack="true">
                <asp:ListItem Value="">- PILIH -</asp:ListItem>
                <asp:ListItem Value="WARGANEGARA">WARGANEGARA</asp:ListItem>
                <asp:ListItem Value="BUKAN WARGANEGARA">BUKAN WARGANEGARA</asp:ListItem>
                <asp:ListItem Value="PENDUDUK TETAP">PENDUDUK TETAP</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td>Jawatan</td>
        <td>:</td>
        <td>
            <asp:DropDownList runat="server" ID="ddlJawatan"></asp:DropDownList>
        </td>
        <td>No. Tentera</td>
        <td>:</td>
        <td>
            <asp:TextBox runat="server" ID="txtNoTentera"/>
        </td>
    </tr>

    <tr>
        <td>Tarikh Berkhidmat</td>
        <td colspan="2">
            <asp:DropDownList ID="ddlBerkhidmatMulaHari" runat="server"></asp:DropDownList> / 
            <asp:DropDownList ID="ddlBerkhidmatMulaBulan" runat="server"></asp:DropDownList> / 
            <asp:DropDownList ID="ddlBerkhidmatMulaTahun" runat="server"></asp:DropDownList>
        </td>
        <td>Hingga</td>
        <td colspan="2">
            <table class="fbform" style="width:100%">
                <tr>
                    <td>
                        <asp:CheckBox Text="Sekarang" runat="server" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBerkhidmatAkhirHari" runat="server"></asp:DropDownList> / 
                        <asp:DropDownList ID="ddlBerkhidmatAkhirBulan" runat="server"></asp:DropDownList> / 
                        <asp:DropDownList ID="ddlBerkhidmatAkhirTahun" runat="server"></asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>

     <tr class="fbform_mheader">
        <td colspan="6">Butiran Keluarga</td>
    </tr>
    <tr>
        <td>Bil. Anak</td>
        <td>:</td>
        <td>
            <asp:TextBox runat="server" ID="txtBilAnak"/>
        </td>
    </tr>
    <tr>
        <td>Tinggal Di Luar(Menyewa Rumah Sewa)</td>
        <td colspan="2">
            <asp:DropDownList ID="ddlSewaMulaHari" runat="server"></asp:DropDownList> / 
            <asp:DropDownList ID="ddlSewaMulaBulan" runat="server"></asp:DropDownList> / 
            <asp:DropDownList ID="ddlSewaMulaTahun" runat="server"></asp:DropDownList>
        </td>
        <td>Hingga</td>
        <td colspan="2">
            <table class="fbform" style="width:100%">
                <tr>
                    <td>
                        <asp:CheckBox Text="Sekarang" runat="server" ID="cbSewaSekarang"/>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSewaAkhirHari" runat="server"></asp:DropDownList> / 
                        <asp:DropDownList ID="ddlSewaAkhirBulan" runat="server"></asp:DropDownList> / 
                        <asp:DropDownList ID="ddlSewaAkhirTahun" runat="server"></asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>Tinggal Di Wisma(Keluarg Di kampung)</td>
        <td colspan="2">
            <asp:DropDownList ID="ddlWismaMulaHari" runat="server"></asp:DropDownList> / 
            <asp:DropDownList ID="ddlWismaMulaBulan" runat="server"></asp:DropDownList> / 
            <asp:DropDownList ID="ddlWismaMulaTahun" runat="server"></asp:DropDownList>
        </td>
        <td>Hingga</td>
        <td colspan="2">
            <table class="fbform" style="width:100%">
                <tr>
                    <td>
                        <asp:CheckBox Text="Sekarang" runat="server" id="cbWismaSekarang"/>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlWismaAkhir" runat="server"></asp:DropDownList> / 
                        <asp:DropDownList ID="ddlWismaMAKhirBulan" runat="server"></asp:DropDownList> / 
                        <asp:DropDownList ID="ddlWismaMAKhirTahun" runat="server"></asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>Bertugas Di Seberang (Keluarga Berada Di Rumah Sewa Di Malaysia Barat)</td>
        <td colspan="2">
            <asp:DropDownList ID="ddlSeberangMulaHari" runat="server"></asp:DropDownList> / 
            <asp:DropDownList ID="ddlSeberangMulaBulan" runat="server"></asp:DropDownList> / 
            <asp:DropDownList ID="ddlSeberangMulaTahun" runat="server"></asp:DropDownList>
        </td>
        <td>Hingga</td>
        <td colspan="2">
            <table class="fbform" style="width:100%">
                <tr>
                    <td>
                        <asp:CheckBox Text="Sekarang" runat="server" ID="cbSeberangSekarang"/>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSeberangAkhirHari" runat="server"></asp:DropDownList> / 
                        <asp:DropDownList ID="ddlSeberangAkhirBulan" runat="server"></asp:DropDownList> / 
                        <asp:DropDownList ID="ddlSeberangAkhirTahun" runat="server"></asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>

     <tr class="fbform_mheader">
        <td colspan="6">Butiran Permohonan</td>
    </tr>
    <tr>
        <td>Rumah Dikehendaki Di: </td>
        <td>
            <asp:TextBox runat="server" id="txtRumahDikehendaki"/>
        </td>
    </tr>
    <tr>
        <td>Dari (Pasukan): </td>
        <td>
            <asp:TextBox runat="server" ID="txtDariPasukan"/>
        </td>
        <td>Ke (Pasukan): </td>
        <td>
            <asp:TextBox runat="server" ID="txtKePasukan"/>
        </td>
    </tr>
    <tr>
        <td>Dari (Tarikh Mula): </td>
        <td>:</td>
        <td colspan="2">
            <asp:DropDownList ID="ddlTarikhMulaHari" runat="server"></asp:DropDownList> / 
            <asp:DropDownList ID="ddlTarikhBulan" runat="server"></asp:DropDownList> / 
            <asp:DropDownList ID="ddlTarikhTahun" runat="server"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:CheckBox runat="server" ID="cbPerakuanPemohon"/>
        </td>
    </tr>
</table>