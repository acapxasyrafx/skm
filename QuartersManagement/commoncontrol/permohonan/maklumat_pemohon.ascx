<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="maklumat_pemohon.ascx.vb" Inherits="QuartersManagement.maklumat_pemohon" %>
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
        <td colspan="6">Butiran Peribadi</td>
    </tr>
    <tr>
        <td class="auto-style1">Nama</td>
        <td class="auto-style1">:</td>
        <td colspan="4" class="auto-style1">
            <asp:Label runat="server" ID="lblNama"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Jantina</td>
        <td>:</td>
        <td colspan="4">
            <asp:Label ID="lblJantina" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Taikh Lahir</td>
        <td>
            <asp:Label ID="lblLahirTahun" text="" runat="server"></asp:Label>
        </td>
        <td>Status Kewarganegaraan</td>
        <td>:</td>
        <td>
            <asp:label ID="lblKewarganegaraan" runat="server" > </asp:label>
        </td>
    </tr>

    <tr>
        <td>Jawatan</td>
        <td>:</td>
        <td>
            <asp:Label runat="server" ID="lblJawatan" Text=""/>
        </td>
        <td>No. Tentera</td>
        <td>:</td>
        <td>
            <asp:Label runat="server" ID="lblNoTentera"/>
        </td>
    </tr>

    <tr>
        <td>Tarikh Berkhidmat</td>
        <td colspan="2">
            <asp:Label Text=""  runat="server" ID="lblTarikhMulaBerkhidmat"/>
        </td>
        <td>Hingga</td>
        <td colspan="2">
            <table class="fbform" style="width:100%">
                <tr>
                    <td>
                        <asp:Label Text="" runat="server" ID="lblTarikhAkhirBerkhidmat"/>
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
        <td>:<asp:Label runat="server" ID="lblBilAnak"></asp:Label></td>
    </tr>
    <tr>
        <td>Tinggal Di Luar(Menyewa Rumah Sewa)</td>
        <td colspan="2">

            <asp:label ID="lblSewaMulaHari" runat="server"></asp:label> 
        </td>
        <td>Hingga</td>
        <td colspan="2">
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
        <td>Tinggal Di Wisma(Keluarg Di kampung)</td>
        <td colspan="2">
            <asp:Label ID="lblWismaMulaHari" runat="server"></asp:Label> 
        </td>
        <td>Hingga</td>
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
        <td>Bertugas Di Seberang (Keluarga Berada Di Rumah Sewa Di Malaysia Barat)</td>
        <td colspan="2">
            <asp:Label ID="lblSeberangMulaHari" runat="server"></asp:Label>
        </td>
        <td>Hingga</td>
        <td colspan="2">
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
        <td colspan="6">Butiran Permohonan</td>
    </tr>
    <tr>
        <td>Rumah Dikehendaki Di: </td>
        <td>
            <asp:Label runat="server" ID="lblSenaraiRumah"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Dari (Pasukan): </td>
        <td>
            <asp:Label runat="server" ID="lblDariPasukan"></asp:Label>
        </td>
        <td>Ke (Pasukan): </td>
        <td>
            <asp:Label runat="server" ID="lblKePasukan"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Dari (Tarikh Mula): </td>
        <td>:</td>
        <td colspan="2">
            <asp:Label ID="lblTarikhMulaHari" runat="server"></asp:Label>
        </td>
    </tr>
    
</table>
<table class="fbform" style="width:100%">
    <tr class="fbform_mheader">
        <td width="10%">
            Total Poin Terkumpul
        </td>
        <td class="auto-style2">:</td>
        <td class="auto-style3">
            <asp:Label runat="server" ID="lblpoinDisplay"></asp:Label>
        </td>
        <td class="auto-style4">
            <asp:ImageButton runat="server" ID="btnImg_lulus" CommandName ="Approved" CommandArgument ='<%#Eval("permohonan_id")%>' OnClientClick="javascript:return confirm('Adakah anda pasti mahu meluluskan permohonan ini? ')" ImageUrl="~/icons/form_process.png" ToolTip="Approved"/>
            <asp:ImageButton runat="server" ID="btnImg_ditolak" />
        </td>
    </tr>

</table>