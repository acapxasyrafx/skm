<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="konfigurasi_jenis_kemaskini.ascx.vb" Inherits="QuartersManagement.konfigurasi_jenis_kemaskini" %>

<%-- Menu Label Indicator --%>
<asp:Panel ID="PnlIndicator" runat="server">
    <asp:Label ID="lbl1" runat="server" Visible="false">Konfigurasi Utama > Penetapan Jenis Kuarters > Kemaskini</asp:Label>
</asp:Panel>

<table class="fbform" style="width: 100%">
    <tr class="fbform_header">
        <td><span id="MsgTop" runat="server">
            <asp:Label ID="strlbl_top" runat="server"></asp:Label></span></td>
        <td>
            <span class="buttonMenu">
                <a href="#" runat="server" id="SaveFunction">
                    <img title="Save" style="vertical-align: middle;" src="icons/save.png" width="25" height="25" alt="::" /></a>
                | <a href="Konfigurasi.Jenis.Kemaskini.aspx?P=<%=lbl1.Text %>" id="Refresh">
                    <img title="Refresh" style="vertical-align: middle;" src="icons/refresh.png" width="22" height="22" alt="::" /></a>
                | <a href="#" id="Help">
                    <img title="Help" style="vertical-align: middle;" src="icons/help.png" width="22" height="22" alt="::" /></a>

            </span>
        </td>
    </tr>
</table>

<table class="fbform" style="width: 100%">

    <tr class="fbform_mheader">
        <td colspan="3">MAKLUMAT JENIS KUARTERS</td>
    </tr>

    <tr>
        <td style="width: 150px">JENIS</td>
        <td style="width: 20px">:</td>
        <td>
            <asp:TextBox ID="txtJenis" runat="server" Width="200px"></asp:TextBox>
        </td>
    </tr>

    <tr>
        <td style="width: 150px">BILANGAN UNIT</td>
        <td style="width: 20px">:</td>
        <td>
            <asp:TextBox ID="txtBilanganUnit" runat="server" Width="50px"></asp:TextBox>
        </td>
    </tr>

    <tr>
        <td style="width: 150px">KELUASAN (KAKI PERSEGI)</td>
        <td style="width: 20px">:</td>
        <td>
            <asp:TextBox ID="txtKeluasan" runat="server" Width="50px"></asp:TextBox>
        </td>
    </tr>

    <tr>
        <td></td>
        <td></td>
        <td>
            <asp:Button ID="btnHantar" runat="server" Text="HANTAR" Width="100px" />
            <asp:Button ID="btnPadam" runat="server" Text="PADAM" Width="100px" />
        </td>
    </tr>

</table>

