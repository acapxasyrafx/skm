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

<form>
    <div>
        <textboxio:Textboxio
            runat="server"
            ID="editorSurattawaran"
            ScriptSrc="textboxio/textboxio.js"
            Content="<p>Hello from textbox.io!</p>" />
    </div>
</form>