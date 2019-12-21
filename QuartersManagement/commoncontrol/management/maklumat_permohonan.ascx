<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="maklumat_permohonan.ascx.vb" Inherits="QuartersManagement.maklumat_permohonan" %>

<style>
    ul.progress {
        margin: 0;
        padding: 0;
        list-style-type: none;
    }

    ul.progress li {
        display: inline-block;
        text-align: center;
        line-height: 3.5em;
    }

    ul.progress[data-progresstracker-step="2"] li {
        width: 33%;
    }

    ul.progress[data-progresstracker-step="3"] li {
        width: 33%;
    }

    ul.progress[data-progresstracker-step="4"] li {
        width: 33%;
    }

    ul.progress li.progress-done {
        color: black;
        border-bottom: 4px solid #e9f83c;
    }

    ul.progress li.progress-todo {
        color: silver;
        border-bottom: 4px solid silver;
    }

    ul.progress li:after {
        content: '\00a0\00a0'
    }

    ul.progress li:before {
        position: relative;
        bottom: -2.5em;
        float: left;
        left: 50%;
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

    .div-center {
        display: flex;
        align-items: center;
        justify-content: center;
        /*margin-bottom: 15px;*/
        width:99%;
    }

    .div-center ul {
        margin: 0;
        width: 100%;
    }

    .div-right {
        position: relative;
        left: 300px;
        top: 10px;
    }

    .progress-label{
        margin:0;
        padding:0;
        text-decoration:solid;
    }

    .label {
        display: inline-block;
        margin: 0;
        padding: 5px;
        font-size: 100%;
    }

    .hoverWrapper #hoverShow {
        display: none;
        position: absolute;
        background-color: #ccc;
        height: 250px;
    }

    .hoverWrapper:hover #hoverShow {
        display: block;
    }

    .table{
        cursor: pointer;
    }

    .tab-buttons {
        overflow: hidden;
        border: 1px solid #ccc;
        background-color: #f1f1f1;
    }

    .tab-buttons button {
        background-color: inherit;
        float:left;
        border:none;
        outline:none;
        cursor: pointer;
        padding: 14px 16px;
        transition: 0.3s;
    }

    .tab-buttons button:hover {
        background-color: #ddd;
    }

    .tab-buttons button.active {
        background-color: #ccc;
    }

    .tab-content {
        display:none;
        padding: 6px 12px;
        border: 1px solid #ccc;
        border-top:none;
        height: 20em;
        overflow-y: scroll;
    }

    .tab-content {
        animation: fadeEffect 1s;
    }

    @keyframes fadeEffect {
        from {opacity: 0;}
        to {opacity: 1;}
    }
</style>
<div class="status-permohonan" style="height: 85vh;">
    <div class="div-center fbform" style="padding-bottom: 15px;">
        <ul class="progress" data-progresstracker-step="3">
            <li runat="server" id="permohonanBaharu">Permohonan Baharu |
                <asp:Label runat="server" ID="lblTarikhBaharu" CssClass="progress-label"></asp:Label>
            </li>
            <li runat="server" id="permohonanMenunggu">Kelayakan Menunggu |
                <asp:Label runat="server" ID="lblTarikhMenuggu" Text="Dalam Proses"></asp:Label>
            </li>
            <li runat="server" id="permohonanKeputusan">Keputusan Permohonan |
                <asp:Label runat="server" ID="lblTarikhKeputusan" Text="Dalam Proses"></asp:Label>
            </li>
        </ul>
    </div>
    <div class="" style="width: 100%;">
        <div class="maklumat-pemohon">
            <table class="fbform" style="width: 100%;">
                <tr class="fbform_mheader">
                    <td colspan="3">Maklumat Pemohon</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div class="tab-buttons">
                            <button class="tab-button" onclick="openContent(event, 'pemohon')" id="default">Maklumat Pemohon</button>
                            <button class="tab-button" onclick="openContent(event, 'keluarga')">Maklumat Keluarga</button>
                            <button class="tab-button" onclick="openContent(event, 'anak')">Maklumat Anak</button>
                        </div>
                        <div id="pemohon" class="tab-content">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 150px;">Pangkat</td>
                                    <td>:</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="width: 150px;">No. Tentera</td>
                                    <td>:</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="width: 150px;">Nama</td>
                                    <td>:</td>
                                    <td>Amir Hamzah</td>
                                </tr>
                                <tr>
                                    <td style="width: 150px;">Jantina</td>
                                    <td>:</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="width: 150px;">Tarikh Lahir</td>
                                    <td>:</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="width: 150px;">Status Perkahwinan</td>
                                    <td>:</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="width: 150px;">Tarikh Mula Berkhidmat</td>
                                    <td>:</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="width: 150px;">Tarikh Tamat Perkhidmatan</td>
                                    <td>:</td>
                                    <td></td>
                                </tr>
                            </table>
                        </div>
                        <div id="keluarga" class="tab-content">
                            <table class="fbform" style="width: 100%;">
                                <tr class="fbform_mheader">
                                    <td colspan="3">Maklumat Keluarga</td>
                                </tr>
                                <tr>
                                    <td style="width: 150px;">Jenis Tempat Tinggal</td>
                                    <td style="width: 5px;">:</td>
                                    <td>
                                        <asp:Label Text="text" runat="server" ID="lblJenisTempatTinggal" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Tarikh Mula Menetap</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label Text="text" runat="server" ID="lblTarikhMulaMenetap" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="anak" class="tab-content">
                            <table class="fbform">
                                <tr class="fbform_mheader">
                                    <td>Maklumat Anak</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView
                                            ID="tblMaklumatAnak"
                                            runat="server"
                                            DataKeyNames="historyAnak_id"
                                            AutoGenerateColumns="False"
                                            AllowPaging="false"
                                            CellPadding="4"
                                            ForeColor="#333333"
                                            GridLines="None"
                                            Width="100%"
                                            PageSize="100"
                                            CssClass="gridview_footer">
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                                                    <ItemStyle VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Nama">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHistoryNamaAnak" runat="server" Text='<%# Bind("historyAnak_nama")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="40%" />
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="KP">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHistoryAnak" runat="server" Text='<%# Bind("historyAnak_ic")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="40%" />
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Underline="true" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <EditRowStyle BackColor="#999999" />
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="maklumat-permohonan">
            <table class="fbform" style="width: 100%;">
                <tr class="fbform_mheader">
                    <td colspan="3">Maklumat Kuarters Dimohon</td>
                </tr>
                <tr>
                    <td style="width: 150px;">Nama Kuarters</td>
                    <td style="width: 5px;">:</td>
                    <td>
                        <asp:Label runat="server" ID="lblKuarterDipohon" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px;">Tarikh Permohonan</td>
                    <td style="width: 5px;">:</td>
                    <td>
                        <asp:Label runat="server" ID="lblTarikhPermohonan" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="detail-status">
            <asp:MultiView ActiveViewIndex="0" runat="server" ID="mvStatusPermohonan">
                <asp:View runat="server" ID="viewCadanganKuarters">
                    <table class="fbform" style="width: 100%">
                        <tr class="fbform_header">
                            <td><span id="MsgTop" runat="server">
                                <asp:Label ID="strlbl_top" runat="server"></asp:Label></span></td>
                            <td>
                                <span class="buttonMenu">
                                    <a href="#" runat="server" id="SaveFunction">
                                        <img title="Save" style="vertical-align: middle;" src="icons/save.png" width="25" height="25" alt="::" />
                                    </a>
                                    | <a href="#" id="Refresh" runat="server">
                                        <img title="Refresh" style="vertical-align: middle;" src="icons/refresh.png" width="22" height="22" alt="::" /></a>
                                    | <a href="#" id="Help">
                                        <img title="Help" style="vertical-align: middle;" src="icons/help.png" width="22" height="22" alt="::" /></a>
                                </span>
                            </td>
                        </tr>
                    </table>
                    <table class="fbform">
                        <tr class="fbform_header">
                            <td colspan="3">Pemilihan Kuarters</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <div style="display: inline-flex;">
                                    <p>
                                        Kuarters yang dipohon pada waktu ini tiada sebarang
                                            <p style="color: red; margin: 0; padding: 0;">KEKOSONGAN</p>
                                        .
                                    </p>
                                    <br />
                                </div>
                                <p>Sila <b>PILIH</b> dari senarai kuarters yang dicadangkan dibawah jika Tuan/Puan masih berniat untuk meneruskan permohonan Tuan/Puan: </p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <div>
                                    <asp:GridView
                                        ID="tblCadanganKuarters"
                                        runat="server"
                                        DataKeyNames="kuarters_dicadang"
                                        AutoGenerateColumns="False"
                                        AllowPaging="false"
                                        CellPadding="4"
                                        ForeColor="#333333"
                                        GridLines="None"
                                        Width="100%"
                                        PageSize="100"
                                        CssClass="table"
                                        Autopostback="true">
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="2%" />
                                                <ItemStyle VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nama Kuarters">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNamaAnak" runat="server" Text='<%# Bind("kuarters_nama")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lokasi Kuarters">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNamaAnak" runat="server" Text='<%# Bind("kuarters_alamat")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="40%" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nama Pangkalan">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNamaAnak" runat="server" Text='<%# Bind("pangkalan_nama")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Underline="true" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <EditRowStyle BackColor="#999999" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View runat="server" ID="viewPenerimaanUnit">
                    <table style="width: 100%;" class="fbform">
                        <tr class="fbform_header">
                            <td colspan="3">Keputusan Permohonan</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <p>Permohonan Anda <b>Berjaya</b>.</p>
                            </td>
                        </tr>
                        <tr class="fbform_header">
                            <td>Maklumat Kuarters Yang Diterima
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">Nama Kuarters</td>
                            <td>:</td>
                            <td>
                                <asp:Label Text="text" runat="server" ID="lblLulusKuarters" /></td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">No. Unit</td>
                            <td>:</td>
                            <td>
                                <asp:Label Text="text" runat="server" ID="lblLulusUnit" /></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View runat="server" ID="viewPermohonanGagal">
                    <table style="width: 100%;" class="fbform">
                        <tr>
                            <td colspan="3">Keputusan Permohonan</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <p>Harap maaf, permohonan anda <b>TIDAK BERJAYA</b>.</p>
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
    <div class="" runat="server" visible="false">
    </div>
    <table class="fbform">
        <tr>
            <td><span id="MsgBottom" runat="server">
                <asp:Label ID="strlbl_bottom" runat="server"></asp:Label></span></td>
        </tr>
    </table>
</div>
<script>
    function openContent(e, divID) {
        e.preventDefault();
        var i, tabContents, tabButtons;
        tabContents = document.getElementsByClassName("tab-content");
        for (i = 0; i < tabContents.length; i++) {
            tabContents[i].style.display = "none";
        }

        tabButtons = document.getElementsByClassName("tab-button");
        for (i = 0; i < tabButtons.length; i++) {
            tabButtons[i].className = tabButtons[i].className.replace(" active", "");
        }

        document.getElementById(divID).style.display = "block";
        e.currentTarget.className += " active";
    }
    document.getElementById("default").click();
</script>
