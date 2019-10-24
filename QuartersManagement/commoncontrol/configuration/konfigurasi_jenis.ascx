<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="konfigurasi_jenis.ascx.vb" Inherits="QuartersManagement.konfigurasi_jenis1" %>

<%-- Menu Label Indicator --%>
<asp:Panel ID="PnlIndicator" runat="server">
    <asp:Label ID="lbl1" runat="server" Visible="false">Konfigurasi Utama > Penetapan Jenis Kuarters > Penetapan Baru</asp:Label>
    <asp:Label ID="lblRefresh" runat="server" Visible="false">Konfigurasi Utama > Penetapan Jenis Kuarters</asp:Label>
</asp:Panel>

<table class="fbform" style="width: 100%">
    <tr class="fbform_header">
        <td><span id="MsgTop" runat="server">
            <asp:Label ID="strlbl_top" runat="server"></asp:Label></span></td>
        <td>
            <span class="buttonMenu">
                <a href="Konfigurasi.Jenis.Baru.aspx?P=<%=lbl1.Text %>" id="SaveFunction">
                    <img title="Add" style="vertical-align: middle;" src="icons/add.png" width="25" height="25" alt="::" /></a>
                | <a href="Konfigurasi.Jenis.aspx?P=<%=lblRefresh.Text %>" id="Refresh">
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
            <asp:DropDownList ID="ddlJenis" runat="server" AutoPostBack="true"></asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td></td>
        <td></td>
        <td>
            <asp:Button ID="btnCari" runat="server" Text="CARI" Width="70px" />
        </td>
    </tr>

</table>

<br />

<table class="fbform" style="width: 100%">

    <tr class="fbform_mheader">
        <td colspan="7">SENARAI JENIS KUARTERS</td>
    </tr>

    <%--LIST--%>
    <tr>
    </tr>
    <%--LIST--%>
</table>
