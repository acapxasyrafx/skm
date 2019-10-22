<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="konfigurasi_kuarters.ascx.vb" Inherits="QuartersManagement.konfigurasi_kuarters" %>

<%-- Menu Label Indicator --%>
<asp:Panel ID="PnlIndicator" runat="server">    
    <asp:Label ID="lbl1" runat="server" Visible="false">Konfigurasi Utama > Penetapan Kuarters > Penetapan Baru</asp:Label>
</asp:Panel>

<table class="fbform" style="width: 100%">
    <tr class="fbform_header">
        <td><span id="MsgTop" runat="server">
            <asp:Label ID="strlbl_top" runat="server"></asp:Label></span></td>
        <td>
            <span class="buttonMenu">
                <a href="Konfigurasi.Kuarters.Baru.aspx?P=<%=lbl1.Text %>" id="SaveFunction">
                <img title="Add" style="vertical-align: middle;" src="icons/add.png" width="25" height="25" alt="::" /></a>
                | <a href="#" id="Refresh" runat="server">
                    <img title="Refresh" style="vertical-align: middle;" src="icons/refresh.png" width="22" height="22" alt="::" /></a>
                | <a href="#" id="Help">
                    <img title="Help" style="vertical-align: middle;" src="icons/help.png" width="22" height="22" alt="::" /></a>

            </span>
        </td>
    </tr>
</table>

<table class="fbform" style="width: 100%">

    <tr class="fbform_mheader">
        <td colspan="3">MAKLUMAT KUARTERS</td>
    </tr>

        <tr>
        <td style="width:150px">NEGERI</td>
        <td style="width:20px">:</td>
        <td>
            <asp:DropDownList ID="ddlNegeri" runat="server" AutoPostBack="true"></asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td style="width:150px">PANGKALAN</td>
        <td style="width:20px">:</td>
        <td>
            <asp:DropDownList ID="ddlPangkalan" runat="server" AutoPostBack="true"></asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td style="width:150px">JENIS</td>
        <td style="width:20px">:</td>
        <td>
            <asp:DropDownList ID="ddlJenis" runat="server" AutoPostBack="true"></asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td style="width:150px">KUARTERS</td>
        <td style="width:20px">:</td>
        <td>
            <asp:DropDownList ID="ddlKuarters" runat="server" AutoPostBack="true"></asp:DropDownList>
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
        <td colspan="7">SENARAI KUARTERS</td>
    </tr>

    <%--LIST--%>
    <tr>
     
    </tr>
    <%--LIST--%>
</table>

