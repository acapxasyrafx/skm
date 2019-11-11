<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="status_tawaran.ascx.vb" Inherits="QuartersManagement.status_tawaran" %>


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
        <td class="auto-style1">
            Aturan Mengikut
        </td>
        <td class="auto-style2">:</td>
        <td>
            <asp:DropDownList runat ="server" ID="ddlSort" AutoPostBack ="true" ></asp:DropDownList>
        </td>
    </tr>
</table>

<table class="fbform" style="width: 100%">
    <tr class="fbform_mheader"></tr></table>