<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="status_permohonan.ascx.vb" Inherits="QuartersManagement.status_permohonan1" %>

<table class="fbform" style="width:100%;">
    <tr class="fbform_mheader">
        <td colspan="3">Maklumat Pemohon</td>
        <td colspan="3">Status Permohonan</td>
    </tr>
    <tr>
        <td>Nama</td>
        <td>:</td>
        <td colspan="3">
            <asp:Label runat="server" ID="lblNamaPemohon"/>
        </td>
        <td rowspan="6">
            <div class="fbform">
                <h3 runat="server" ID="lblStatusPermohonan">Dalam Process...</h3>
            </div>
        </td>
    </tr>
    <tr>
        <td>Jantina</td>
        <td>:</td>
        <td>
            <asp:Label runat="server" ID="lblJantina"/>
        </td>
    </tr>
    <tr>
        <td>Tarikh Lahir</td>
        <td>:</td>
        <td>
            <asp:Label runat="server" ID="lblTarikhLahir"/>
        </td>
    </tr>
    <tr>
        <td>Status Kewarganegaraan</td>
        <td>:</td>
        <td>
            <asp:Label runat="server" ID="lblStatusKewarganegaraan"/>
        </td>
    </tr>
    <tr>
        <td>Jawatan</td>
        <td>:</td>
        <td colspan="3">
            <asp:Label runat="server" ID="lblJawatan"/>
        </td>
    </tr>
    <tr>
        <td>Tarikh Berkhidmat</td>
        <td>:</td>
        <td>
            <asp:Label runat="server" ID="lblTarikhBerkhidmatMula"/> Hingga <asp:Label runat="server" ID="lblTarikhBerkhidmatAkhir"/>
        </td>
    </tr>
</table>
