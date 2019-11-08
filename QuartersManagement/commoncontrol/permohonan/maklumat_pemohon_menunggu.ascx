﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="maklumat_pemohon_menunggu.ascx.vb" Inherits="QuartersManagement.maklumat_pemohon_menunggu" %>
<style type="text/css">
    .auto-style1 {
        height: 26px;
    }
    .auto-style2 {
        width: 1%;
    }
    .auto-style3 {
        width: 176px;
        align-items :center ;
    }
    .auto_style4{
        position :center
    }
    .auto-style6 {
        height: 26px;
        width: 22%;
    }
    .auto-style12 {
        height: 26px;
        width: 1%;
    }
    .auto-style17 {
        width: 8%;
    }
    .auto-style18 {
        width: 21%;
    }
    .auto-style20 {
        width: 26%;
    }
    .auto-style23 {
        width: 13px;
    }
    .auto-style25 {
        width: 32%;
    }
    .auto-style27 {
        width: 22%;
    }
    .auto-style29 {
        width: 95px;
    }
    .auto-style30 {
        width: 22%;
        height: 29px;
    }
    .auto-style31 {
        width: 1%;
        height: 29px;
    }
    .auto-style33 {
        width: 95px;
        height: 29px;
    }
    .auto-style34 {
        width: 13px;
        height: 29px;
    }
    .auto-style35 {
        height: 29px;
    }
    .auto-style36 {
        width: 16%;
        height: 29px;
    }
    .auto-style37 {
        width: 16%;
    }
    .auto-style38 {
        width: 189px;
    }
    .auto-style39 {
        width: 18px;
    }
    .auto-style40 {
        width: 12%;
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

<table class="fbform" style="width:100%">
    <tr class="fbform_mheader">
        <td colspan="7">Butiran Peribadi</td>
    </tr>
    <tr>
        <td class="auto-style6">Nama</td>
        <td class="auto-style12">:</td>
        <td colspan="5" class="auto-style1">
            <asp:Label runat="server" ID="lblNama"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style27">Jantina</td>
        <td class="auto-style2">:</td>
        <td colspan="5">
            <asp:Label ID="lblJantina" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style27">Taikh Lahir</td>
        <td class="auto-style2">:</td>
        <td class="auto-style37" colspan="2">
            <asp:Label ID="lblLahirTahun" text="" runat="server"></asp:Label>
        </td>
        <td class="auto-style29">Status Kewarganegaraan</td>
        <td class="auto-style23">:</td>
        <td class="auto-style17">
            <asp:label ID="lblKewarganegaraan" runat="server" > </asp:label>
        </td>
    </tr>

    <tr>
        <td class="auto-style27">Jawatan</td>
        <td class="auto-style2">:</td>
        <td class="auto-style37" colspan ="2">
            <asp:Label runat="server" ID="lblJawatan" Text=""/>
        </td>
        <td class="auto-style29">No. Tentera</td>
        <td class="auto-style23">:</td>
        <td class="auto-style17">
            <asp:Label runat="server" ID="lblNoTentera"/>
        </td>
    </tr>

    <tr>
        <td class="auto-style27">Tarikh Berkhidmat</td>
        <td class="auto-style2">
            :
        </td>
        <td colspan="2" class="auto-style37">
            <asp:Label Text=""  runat="server" ID="lblTarikhMulaBerkhidmat"/>
        </td>
        <td class="auto-style29">Hingga</td>
        <td class="auto-style23" >:</td>
            
                    
        
            <td>
              <asp:Label Text="" runat="server" ID="lblTarikhAkhirBerkhidmat"/>
                    </td>
           
    </tr>

     <tr class="fbform_mheader">
        <td colspan="7">Butiran Keluarga</td>
    </tr>
    <tr>
        <td class="auto-style27">Bil. Anak</td>
        <td class="auto-style2">:</td>
        <td class="auto-style20" colspan="5"><asp:Label runat="server" ID="lblBilAnak"></asp:Label></td>
    </tr>
    <tr>
        <td class="auto-style30">Tinggal Di Luar(Menyewa Rumah Sewa)</td>
        <td class="auto-style31">:</td>
        <td colspan="2" class="auto-style36">

            <asp:label ID="lblSewaMulaHari" runat="server"></asp:label> 
        </td>
        <td class="auto-style33">Hingga</td>
        <td class="auto-style34">:</td>
        <td colspan="2" class="auto-style35">
            <table class="fbform" style="width:100%">
                <tr>
                    
                    <td>
                        <asp:Label ID="lblSewaAkhirHari" runat="server"></asp:Label> 
                        
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="auto-style27">Tinggal Di Wisma(Keluarg Di kampung)</td>
        <td class="auto-style2">:</td>
        <td colspan="2" class="auto-style37">
            <asp:Label ID="lblWismaMulaHari" runat="server"></asp:Label> 
        </td>
        <td class="auto-style29">Hingga</td>
        <td class="auto-style23">:</td>
        <td colspan="2">
            <table class="fbform" style="width:100%">
                <tr>
                    <td>
                        <asp:Label ID="lblWismaAkhir" runat="server"></asp:Label> 
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="auto-style27">Bertugas Di Seberang (Keluarga Berada Di Rumah Sewa Di Malaysia Barat)</td>        
        <td class="auto-style2">:</td>
        <td colspan="2" class="auto-style37">
            <asp:Label ID="lblSeberangMulaHari" runat="server"></asp:Label>
        </td>
        <td class="auto-style29">Hingga</td>
        <td class="auto-style23">:</td>
        <td colspan="2" class="auto-style18">
            <table class="fbform" style="width:100%">
                <tr>
                    <td>
                        <asp:Label ID="lblSeberangAkhirTahun" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>

     <tr class="fbform_mheader">
        <td colspan="7">Butiran Permohonan</td>
    </tr>
    <tr>
        <td class="auto-style27">Rumah Dikehendaki Di </td>
        <td class="auto-style2">:</td>
        <td class="auto-style20" colspan="5">
            <asp:Label runat="server" ID="lblSenaraiRumah"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style27">Dari (Pasukan)</td>
        <td class="auto-style2">:</td>
        <td class="auto-style37" colspan="2">
            <asp:Label runat="server" ID="lblDariPasukan"></asp:Label>
        </td>
        <td class="auto-style29">Ke (Pasukan)</td>
        <td class="auto-style23">:</td>
        <td class="auto-style17">
            <asp:Label runat="server" ID="lblKePasukan"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style27">Dari (Tarikh Mula): </td>
        <td class="auto-style2">:</td>
        <td colspan="5" class="auto-style25">
            <asp:Label ID="lblTarikhMulaHari" runat="server"></asp:Label>
        </td>
    </tr>
    
</table>
<table class="fbform" style="width:100%">
    <tr class="fbform_mheader">
        <td class="auto-style40">
            Total Markah Terkumpul
        </td>
        <td class="auto-style2">:</td>
        <td class="auto-style3">
            <asp:Label runat="server" ID="lblpoinDisplay"></asp:Label>
        </td>
        
    </tr>

</table>

<table class="fbform" style="width:100%">
    <tr >
        <td colspan="3">Rummah Dipohon</td>
    </tr>
    <tr>
        <td class="auto-style38">
        <asp:Label runat="server" >Rumah Yang Dipohon</asp:Label>
        </td>
        <td class="auto-style39">:</td>
        <td><asp:Label runat ="server" ID="lblrumahpemohonDipohon"></asp:Label></td>
    </tr>
    <tr class="fbform_mheader">
        <td colspan="3">Cadangan Unit</td>
    </tr>
    <tr>
        <td class="auto-style38">
            Unit 1
        </td>
        <td class="auto-style39">:</td>
        <td>
            <asp:DropDownList ID="ddlcadanganUnit1" runat="server"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="auto-style38">
            Unit 2
        </td>
        <td class="auto-style39">:</td>
        <td>
            <asp:DropDownList ID="ddlcadanganUnit2" runat="server"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="auto-style38">
            Unit 3
        </td>
        <td class="auto-style39">:</td>
        <td>
            <asp:DropDownList ID="ddlcadanganUnit3" runat="server"></asp:DropDownList>
        </td>
    </tr>
    <tr><td colspan="3"> <asp:Button runat ="server" ID ="btn_submitUnit" Text ="Hantar Unit Cadangan" /> </td></tr>

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