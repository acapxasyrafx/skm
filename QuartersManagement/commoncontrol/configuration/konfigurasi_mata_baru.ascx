<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="konfigurasi_mata_baru.ascx.vb" Inherits="QuartersManagement.konfigurasi_mata_baru" %>

<%-- Menu Label Indicator --%>
<asp:Panel ID="PnlIndicator" runat="server">
    <asp:Label ID="lbl1" runat="server" Visible="false">Konfigurasi Utama > Penetapan Mata > Penetapan Baru</asp:Label>
</asp:Panel>

<table class="fbform" style="width: 100%">
    <tr class="fbform_header">
        <td><span id="MsgTop" runat="server">
            <asp:Label ID="strlbl_top" runat="server"></asp:Label></span></td>
        <td>
            <span class="buttonMenu">
                <a href="#" runat="server" id="SaveFunction">
                    <img title="Save" style="vertical-align: middle;" src="icons/save.png" width="25" height="25" alt="::" /></a>
                | <a href="Konfigurasi.Mata.Baru.aspx?P=<%=lbl1.Text %>" id="Refresh">
                    <img title="Refresh" style="vertical-align: middle;" src="icons/refresh.png" width="22" height="22" alt="::" /></a>
                | <a href="#" id="Help">
                    <img title="Help" style="vertical-align: middle;" src="icons/help.png" width="22" height="22" alt="::" /></a>

            </span>
        </td>
    </tr>
</table>

<table class="fbform" style="width: 100%">

    <tr class="fbform_mheader">
        <td colspan="3">MAKLUMAT MATA DALAM PERKHIDMATAN</td>
    </tr>

    <tr>
        <td style="width: 150px">PANGKAT</td>
        <td style="width: 20px">:</td>
        <td>
            <asp:DropDownList ID="ddlPangkat" runat="server" AutoPostBack="true" Width="300px">
                <asp:ListItem Value="-PILIH-">-PILIH-</asp:ListItem>
                <asp:ListItem Value="PEGAWAI KANAN">PEGAWAI KANAN</asp:ListItem>
                <asp:ListItem Value="PEGAWAI MUDA">PEGAWAI MUDA</asp:ListItem>
                <asp:ListItem Value="LAIN-LAIN PANGKAT RENDAH">LAIN-LAIN PANGKAT RENDAH</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td style="width: 150px">NAMA PANGKAT</td>
        <td style="width: 20px">:</td>
        <td>
            <asp:DropDownList ID="ddlNamaPangkat" runat="server" AutoPostBack="true" Width="200px"></asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td style="width: 200px">MATA</td>
        <td style="width: 20px">:</td>
        <td>
            <asp:TextBox ID="txtMata" runat="server" Width="200px"></asp:TextBox>
        </td>
    </tr>   

    <tr>
        <td></td>
        <td></td>
        <td>
            <asp:Button ID="btnHantar" runat="server" Text="HANTAR" Width="100px" />
        </td>
    </tr>

</table>

