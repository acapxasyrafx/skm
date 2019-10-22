<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="konfigurasi_kuarters_baru.ascx.vb" Inherits="QuartersManagement.konfigurasi_kuarters_baru1" %>

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
                <a href="#" runat="server" id="SaveFunction">
                    <img title="Save" style="vertical-align: middle;" src="icons/save.png" width="25" height="25" alt="::" /></a>
                | <a href="Konfigurasi.Kuarters.Baru.aspx?P=<%=lbl1.Text %>" id="Refresh">
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
        <td style="width: 150px">JENIS</td>
        <td style="width: 20px">:</td>
        <td>
            <asp:DropDownList ID="ddlJenis" runat="server" AutoPostBack="true" Width="200px"></asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td style="width: 150px">NAMA KUARTERS</td>
        <td style="width: 20px">:</td>
        <td>
            <asp:TextBox ID="txtNama" runat="server" Width="400px"></asp:TextBox>
        </td>
    </tr>

    <tr>
        <td style="width: 150px">ALAMAT</td>
        <td style="width: 20px">:</td>
        <td>
            <asp:TextBox ID="txtAlamat1" runat="server" Width="400px"></asp:TextBox>
        </td>
    </tr>

    <tr>
        <td style="width: 150px"></td>
        <td style="width: 20px"></td>
        <td>
            <asp:TextBox ID="txtAlamat2" runat="server" Width="400px"></asp:TextBox>
        </td>
    </tr>

    <tr>
        <td style="width: 150px"></td>
        <td style="width: 20px"></td>
        <td>
            <asp:TextBox ID="txtAlamat3" runat="server" Width="400px"></asp:TextBox>
        </td>
    </tr>

    <tr>
        <td style="width: 150px">POSKOD</td>
        <td style="width: 20px">:</td>
        <td>
            <asp:TextBox ID="txtPoskod" runat="server" Width="50px"></asp:TextBox>
        </td>
    </tr>

    <tr>
        <td style="width: 150px">NEGERI</td>
        <td style="width: 20px">:</td>
        <td>
            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" Width="200px"></asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td style="width: 150px">BANDAR</td>
        <td style="width: 20px">:</td>
        <td>
            <asp:DropDownList ID="ddlBandar" runat="server" AutoPostBack="true" Width="200px"></asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td style="width: 150px">PANGKALAN</td>
        <td style="width: 20px">:</td>
        <td>
            <asp:DropDownList ID="ddlPangkalan" runat="server" AutoPostBack="true" Width="200px"></asp:DropDownList>
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
