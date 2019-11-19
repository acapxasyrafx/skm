<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="status_tawaran.ascx.vb" Inherits="QuartersManagement.status_tawaran" %>
<%@ Register Assembly="TextboxioControl" Namespace="TextboxioControl" TagPrefix="textboxio" %>

<head>
   <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title>jQuery UI Datepicker - Default functionality</title>
  <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
  <link rel="stylesheet" href="/resources/demos/style.css">
  <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
   <script>
       $(function () {
           $(".datepicker").datepicker({ dateformat:'dd MM yy' }).val();
       });
  </script>
</head>

<style type="text/css">
    .ddl {
        border-radius: 25px;
    }

    .CalendarCssClass {
        background-color: #990000;
        font-family: Century;
        text-transform: lowercase;
        width: 750px;
        border: 1px solid Olive;
        z-index:2;
    }
    .tbltexbox {
        z-index:-1;
    }
    .formdetail {
        z-index:1;
    }
    </style>

<script type="text/javascript">

    function date_Click() {
        if (divcalendar.style.display == "none")
            divcalendar.style.display = "";
        else
            divcalendar.style.display = "none";
    }
</script>



<div class="formdetail">
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
</div>

<div>
<table>
    <tr>
        <td>
            <asp:Label runat="server" ID="lblNama">Nama</asp:Label>
        </td>
        <td>:</td>
        <td>
            <asp:Label runat ="server" id="lbloutname"></asp:Label>

        </td>
    </tr>
     <tr>
        <td>
            <asp:Label runat="server" ID="lblDateMasuk">Tarikh Masuk</asp:Label>
        </td>
        <td>:</td>
        <td>
            <br>            
                <asp:TextBox CssClass="datepicker" ID="datepicker" runat="server" />
                &#160; <i class="fa fa-calendar w3-medium w3-text-black"></i>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="lblKuarters">Kuarters Dipilih</asp:Label>
        </td>
        <td>:</td>
        <td>
            <asp:Label runat ="server" id="lblOutKuarters"></asp:Label>

        </td>
    </tr>
    <tr>
        <td>
            <asp:label runat ="server" ID="lbl_unit">Unit</asp:label>
        </td>
        <td>:</td>
        <td>
            <asp:DropDownList runat ="server" id="ddl_unit" AutoPostBack="true" ></asp:DropDownList>

        </td>
        </tr>
   </table>
    </div>

<div class="tbltexbox">
<table>
    <tr>
        <td>
            
        </td>
    </tr>
    <tr>
        <td colspan="3" >   
        <textboxio:Textboxio
            runat="server"
            ID="editorSurattawaran"
            ScriptSrc="textboxio/textboxio.js"
            Content="<p></p>" />
        
            </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnSimpan" runat="server" Text ="Hantar Surat Tawaran" />
        </td>
    </tr>
</table>

    </div>

<asp:Label runat="server">Disclaimer : Fungsi surat tawaran ini masih lagi dalam pembinaan tetapi fungsi ini akan membuat surat tawaran berserta unit yang dipilih oleh admin.</asp:Label>