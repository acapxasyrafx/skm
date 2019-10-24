<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="status_permohonan.ascx.vb" Inherits="QuartersManagement.status_permohonan1" %>

<table class="fbform" style="width:100%;">
    <tr class="fbform_mheader">
        <td colspan="6">Status Permohonan</td>
    </tr>
    <tr>
        <td colspan="5">Maklumaat Permohonan</td>
        <td colspan="2">Status Permohonan</td>
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
        <td>Jawatan</td>
        <td>:</td>
        <td colspan="3">
            <asp:Label runat="server" ID="lblJawatanPemohon"/>
        </td>
    </tr>
</table>
