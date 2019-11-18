<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pendaftaran_penjawat.ascx.vb" Inherits="QuartersManagement.pendaftaran_penjawat" %>

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
<table class="fbform" style="width: 100%">
    <tr class="fbform_mheader">
        <td colspan="6">BAHAGIAN I : MAKLUMAT DEMOGRAFI</td>
    </tr>
    <tr>
        <td>No.Tentera</td>
        <td>:</td>
        <td><asp:TextBox ID="txtNoPekerja" runat="server" Width="200px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Pangkat</td>
        <td>:</td>
        <td colspan="4"><asp:DropDownList ID="ddlJawatan" runat="server"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Nama </td>
        <td>:</td>
        <td colspan="4"><asp:TextBox ID="txtNamaPertama" runat="server" Width="250px"></asp:TextBox>
    </tr>
    <tr>
        <td>Pangkalan</td>
        <td>:</td>
        <td colspan="4"><asp:DropDownList ID="ddlCawangan" runat="server"></asp:DropDownList></td>
    </tr>
    <%--<tr>
        <td>Mykad</td>
        <td>:</td>
        <td colspan="4"><asp:TextBox ID="txtNoKP" runat="server" Width="250px"></asp:TextBox></td>
    </tr>--%>
    <tr>
        <td>Jantina</td>
        <td>:</td>
        <td colspan="4">
            <asp:DropDownList ID="ddlJantina" runat="server">
                <asp:ListItem Value="">- PILIH -</asp:ListItem>
                <asp:ListItem Value="Lelaki">LELAKI</asp:ListItem>
                <asp:ListItem Value="Perempuan">PEREMPUAN</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td>Tarikh Lahir</td>
        <td>:</td>
        <td><asp:TextBox runat="server" ID="txt_MyDate" MaxLength="10"/>
        <%--<td>Warganegara</td>
        <td>:</td>
        <td>
            <asp:DropDownList ID="ddlKewarganegaraan" runat="server" AutoPostBack="true">
                <asp:ListItem Value="">- PILIH -</asp:ListItem>
                <asp:ListItem Value="WARGANEGARA">WARGANEGARA</asp:ListItem>
                <asp:ListItem Value="BUKAN WARGANEGARA">BUKAN WARGANEGARA</asp:ListItem>
                <asp:ListItem Value="PENDUDUK TETAP">PENDUDUK TETAP</asp:ListItem>
            </asp:DropDownList></td>
    </tr>--%>
    <tr>
        <td>Emel</td>
        <td>:</td>
        <td><asp:TextBox ID="txtEmail" runat="server" Width="200px"></asp:TextBox></td>
        <td>No.Telefon</td>
        <td>:</td>
        <td><asp:TextBox ID="txtTelefon" runat="server" Width="200px"></asp:TextBox></td>
    </tr>
    <tr class="fbform_mheader">
        <td colspan="6">BAHAGIAN II : ALAMAT SURAT MENYURAT</td>
    </tr>
    <tr>
        <td rowspan="3" style="vertical-align: top;">Alamat</td>
        <td rowspan="3" style="vertical-align: top;">:</td>
        <td colspan="4"><asp:TextBox ID="txtAlamat1Daftar" runat="server" Width="350px"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="4"><asp:TextBox ID="txtAlamat2Daftar" runat="server" Width="350px"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="4"><asp:TextBox ID="txtAlamat3Daftar" runat="server" Width="350px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Bandar</td>
        <td>:</td>
        <td colspan="4"><asp:TextBox ID="txtBandarDaftar" runat="server" Width="200px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Negeri</td>
        <td>:</td>
        <td><asp:DropDownList ID="ddlNegeriDaftar" runat="server"></asp:DropDownList></td>
        <td>Poskod</td>
        <td>:</td>
        <td><asp:TextBox ID="txtPoskodDaftar" runat="server" Width="200px"></asp:TextBox></td>
    </tr>
</table>