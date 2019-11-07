<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="status_permohonan.ascx.vb" Inherits="QuartersManagement.status_permohonan1" %>
<style>
    ul.progress {
        margin: 0;
        padding:0;
        list-style-type:none;
    }

    ul.progress li {
        display:inline-block;
        text-align:center;
        line-height: 3.5em;
    }

    ul.progress[data-pregoresstracker-step="2"] li { width: 49%;}
    ul.progress[data-pregoresstracker-step="3"] li { width: 33%;}
    ul.progress[data-pregoresstracker-step="3"] li { width: 24%;}

    ul.progress li.progress-done {
        color:black;
        border-bottom: 4px solid #e9f83c;
    }

    ul.progress li.progress-todo {
        color:silver;
        border-bottom: 4px solid silver;
    }

    ul.progress li:after{
        content:'\00a0\00a0'
    }
    ul.progress li:before{
        position:relative;
        bottom:-2.5em;
        float:left;
        left:50%;
        line-height: 1em;
    }

    ul.progress li.progress-done:before {
        content: "\2713";
        color: grey;
        background-color: #e9f83c;
        height: 2.2em;
        width: 2.2em;
        line-height: 2.2em;
        border: none;
        border-radius: 2.2em;
    }

    ul.progress li.progress-todo:before {
        content: "\039F";
        color: silver;
        background-color: white;
        font-size: 2.2em;
        bottom: -1.2em;
    }

    .div-center{
        display:flex;
        align-items:center;
        justify-content: center;
    }
    .div-center ul {
        margin:0;
    }
    .label{
        display:inline-block;
        margin:0;
        padding:5px;
        font-size:100%;
    }
</style>
<div class="status-permohonan" style="height:85vh;">
    <div class="fbform">
        <div class="div-center">
            <ul class="progress" data-progresstracker-step="3">
                <li runat="server" id="permohonanBaharu">Permohonan Baharu</li>
                <li runat="server" id="permohonanLayak">Kelayakan Permohonan</li>
                <li runat="server" id="permohonanMenunggu">Kelayakan Menunggu</li>
                <li runat="server" id="permohonanUnitDicadang">Permohonan Unit Dicadang</li>
                <li runat="server" id="suratTawaran">Surat Tawaran</li>
            </ul>
        </div>
        <div class="">
            <table class="fbform" style="width:100%;">
                <tr class="fbform_mheader">
                    <td colspan="3">Maklumat Keluarga</td>
                </tr>
                <tr>
                    <td style="width:100px;">Bilangan Anak</td>
                    <td>:</td>
                    <td>
                        <asp:Label CssClass="label" runat="server" ID="lblBilAnak" Text="4"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Jenis Tempat Tinggal</td>
                    <td>:</td>
                    <td>
                        <asp:Label Text="text" runat="server" ID="lblJenisTempatTinggal"/>
                    </td>
                </tr>
                <tr>
                    <td>Tarikh Mula Menetap</td>
                    <td>:</td>
                    <td>
                        <asp:Label Text="text" runat="server" ID="lblTarikhMulaMenetap"/>
                    </td>
                </tr>
            </table>
            <table class="fbform" style="width:100%;">
                <tr class="fbform_mheader">
                    <td colspan="3">Maklumat Kuarters Dimohon</td>
                </tr>
                <tr>
                    <td style="width:100px;">Nama Kuarters</td>
                    <td>:</td>
                    <td>
                        <asp:Label Text="text" runat="server" ID="lblKuarterDipohon" />
                    </td>
                </tr>
                <tr>
                    <td style="width:100px;">Tarikh Permohonan</td>
                    <td>:</td>
                    <td>
                        <asp:Label Text="text" runat="server" ID="lblTarikhPermohonan" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>