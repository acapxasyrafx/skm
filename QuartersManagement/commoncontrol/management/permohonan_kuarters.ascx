<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="permohonan_kuarters.ascx.vb" Inherits="QuartersManagement.permohonan_kuarters" %>
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

<table class="fbform" style="width:100%">
    <tr class="fbform_mheader">
        <td colspan="6">Butiran Peribadi</td>
    </tr>
    <tr>
        <td>Nama</td>
        <td>:</td>
        <td colspan="5">
            <asp:Label runat="server" ID="lblNama"/>
            <asp:HiddenField runat="server" ID="pengguna_id" Value="" />
        </td>
    </tr>
    <tr>
        <td>Jantina</td>
        <td>:</td>
        <td colspan="5">
            <asp:Label runat="server" ID="lblJantina"/>
        </td>
    </tr>
    <tr>
        <td>Tarikh Lahir</td>
        <td>:</td>
        <td colspan="5">
            <asp:Label runat="server" ID="lblTarikhLahir"/>
        </td>
    </tr>
    <tr>
        <td>Status Kewarganegaraan</td>
        <td>:</td>
        <td colspan="5">
            <asp:Label runat="server" ID="lblKewarganegaraan"/>
        </td>
    </tr>
    <tr>
        <td>Jawatan</td>
        <td>:</td>
        <td colspan="5">
            <asp:Label runat="server" ID="lblJawatan" Text=""/>
        </td>
    </tr>
    <tr>
        <td>No. Tentera</td>
        <td>:</td>
        <td colspan="5">
            <asp:Label runat="server" ID="lblNoTentera"/>
        </td>
    </tr>
    <tr>
        <td>Tarikh Berkhidmat</td>
        <td>:</td>
        <td>
            <asp:Label Text="01/01/1990" runat="server" ID="lblTarikhMulaBerkhidmat"/>
        </td>
        <td>Hingga</td>
        <td>:</td>
        <td>
            <asp:Label runat="server" ID="lblTarikhAkhirBerkhidmat"/>
        </td>
    </tr>

     <tr class="fbform_mheader">
        <td colspan="6">Butiran Keluarga</td>
    </tr>
    <tr>
        <td>Bil. Anak</td>
        <td>:<asp:TextBox runat="server" ID="txtBilAnak" Text="0"/></td>
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
        <td>Tinggal Di Wisma(Keluarga Di kampung)</td>
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
                        <asp:DropDownList ID="ddlWismaAkhirHari" runat="server"></asp:DropDownList> / 
                        <asp:DropDownList ID="ddlWismaAKhirBulan" runat="server"></asp:DropDownList> / 
                        <asp:DropDownList ID="ddlWismaAKhirTahun" runat="server"></asp:DropDownList>
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
            <asp:DropDownList runat="server" ID="ddlSenaraiRumah"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Dari (Pasukan): </td>
        <td>
            <asp:DropDownList runat="server" ID="ddlDariPasukan"></asp:DropDownList>
        </td>
        <td>Ke (Pasukan): </td>
        <td>
            <asp:DropDownList runat="server" ID="ddlKePasukan"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Dari (Tarikh Mula): </td>
        <td>:</td>
        <td colspan="2">
            <asp:DropDownList ID="ddlTarikhMulaHari" runat="server"></asp:DropDownList> / 
            <asp:DropDownList ID="ddlTarikhMulaBulan" runat="server"></asp:DropDownList> / 
            <asp:DropDownList ID="ddlTarikhMulaTahun" runat="server"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:CheckBox runat="server" ID="cbPerakuanPemohon"/>
            <asp:Label runat="server" ID="lblCheckBoxAlert" Visible=false/>
        </td>
    </tr>
</table>
<table class ="fbform">
    <tr>
        <td><span id ="MsgBottom" runat ="server" ><asp:Label ID ="strlbl_bottom" runat ="server" ></asp:Label></span></td>
    </tr>
</table>