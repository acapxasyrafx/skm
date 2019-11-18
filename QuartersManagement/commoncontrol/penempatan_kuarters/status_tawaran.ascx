<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="status_tawaran.ascx.vb" Inherits="QuartersManagement.status_tawaran" %>
<%@ Register Assembly="TextboxioControl" Namespace="TextboxioControl" TagPrefix="textboxio" %>

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

<table>
    <tr>
        <td>
            <asp:label runat ="server" ID="lbl_unit">
            </asp:label>
        </td>
        <td>:</td>
        <td>
            <asp:DropDownList runat ="server" id="ddl_unit"></asp:DropDownList>

        </td>
        </tr>
    <tr>
        <td>
        <textboxio:Textboxio
            runat="server"
            ID="editorSurattawaran"
            ScriptSrc="textboxio/textboxio.js"
            Content="<p></p>" />
            </td>
    </tr>
</table>

<asp:Label runat ="server">Disclaimer : Fungsi surat tawaran ini masih lagi dalam pembinaan tetapi fungsi ini akan membuat surat tawaran berserta unit yang dipilih oleh admin.</asp:Label>