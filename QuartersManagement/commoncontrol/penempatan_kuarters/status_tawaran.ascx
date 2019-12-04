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
    #editorSurartTawaran{
        z-index: -1;
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
    .wrapper{
        display: flex;
        justify-content: normal;
        flex-direction: row;
    }
    .left{
        display: block;
        width: 40%;
    }
    .right{
        display: block;
        width: 60%;
    }
    .datepicker{
        position: absolute;
        z-index: 2;
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

<div class="wrapper">
    <div class="left">
        <table class="fbform">
            <tr class="fbform_mheader">
                <td colspan="3">Maklumat Permohonan</td>
            </tr>
            <tr>
                <td style="width:150px;">No. Permohonan</td>
                <td>:</td>
                <td><asp:Label Text="text" runat="server" ID="lblNoPermohonan"/></td>
            </tr>
            <tr>
                <td style="width: 150px;">Nama</td>
                <td>:</td>
                <td>
                    <asp:Label runat="server" ID="lblNama"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 150px;">No Tentera</td>
                <td>:</td>
                <td>
                    <asp:Label Text="text" runat="server" ID="lblNoTentera"/>
                </td>
            </tr>
            <tr>
                <td style="width: 150px;">Pangkat</td>
                <td>:</td>
                <td>
                    <asp:Label Text="text" runat="server" ID="lblPangkat"/>
                </td>
            </tr>
            <tr>
                <td style="width: 150px;">Pangkalan Kuarters</td>
                <td>:</td>
                <td><asp:Label Text="text" runat="server" ID="lblPangkalan"/></td>
            </tr>
            <tr>
                <td style="width:150px;">Kuarters Diterima</td>
                <td></td>
                <td>
                    <asp:Label Text="text" runat="server" ID="lblKuarters"/>
                </td>
            </tr>
            <tr>
                <td style="width:150px;">Unit Diterima</td>
                <td></td>
                <td>
                    <asp:Label Text="text" runat="server" ID="lblUnit"/>
                </td>
            </tr>
        </table>
    </div>
    <div class="right">
        <table class="fbform">
            <tr class="fbform_mheader">
                <td colspan="3">Surat Tawaran</td>
            </tr>
            <tr>
                <td style="width: 150px;">
                    Tarikh Kemasukan Kuarters
                </td>
                <td>:</td>
                <td>
                    <asp:TextBox CssClass="datepicker" ID="datepicker" runat="server" />
                    &#160; <i class="fa fa-calendar w3-medium w3-text-black"></i>
                </td>
            </tr>
            <tr>
                <td style="width: 150px;">Jenis Surat Tawaran</td>
                <td>:</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlJenisSuratTawaran" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>
        </table>
        <hr />
        <table>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td colspan="3">
                    <textboxio:Textboxio
                        runat="server"
                        ID="editorSurattawaran"
                        ScriptSrc="textboxio/textboxio.js"
                        Content="<p></p>"
                    />

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSimpan" runat="server" Text="Hantar Surat Tawaran" />
                </td>
            </tr>
        </table>
    </div>
</div>

<div class="tbltexbox">
</div>

<asp:Label runat="server">Disclaimer : Fungsi surat tawaran ini masih lagi dalam pembinaan tetapi fungsi ini akan membuat surat tawaran berserta unit yang dipilih oleh admin.</asp:Label>