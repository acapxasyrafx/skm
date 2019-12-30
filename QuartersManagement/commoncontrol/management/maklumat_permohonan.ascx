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
    
    /*Tabs styling*/
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
    
    /*Collapsible styling*/
    .collapsible {
        background-color: #eee;
        color: #444;
        cursor:pointer;
        padding: 18px;
        width: 97.5%;
        border: none;
        text-align:left;
        font-size: 15px;
    }
    
    .active, collapsible:hover {
        background-color: #ccc;
    }
    
    .content {
        padding: 20px 18px;
        display: none;
        overflow: hidden;
        /*background-color: #f1f1f1;*/
        border: 1px solid #f1f1f1;
    }
   /*Tabs styling*/

    /*Modal styling*/ 
    /* The Modal (background) */
    .modal {
      display: none; /* Hidden by default */
      position: fixed; /* Stay in place */
      z-index: 1; /* Sit on top */
      padding-top: 100px; /* Location of the box */
      left: 0;
      top: 0;
      width: 100%; /* Full width */
      height: 100%; /* Full height */
      overflow: auto; /* Enable scroll if needed */
      background-color: rgb(0,0,0); /* Fallback color */
      background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
    }

    /* Modal Content */
    .modal-content {
      position: relative;
      background-color: #fefefe;
      margin: auto;
      padding: 0;
      border: 1px solid #888;
      width: 80%;
      box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2),0 6px 20px 0 rgba(0,0,0,0.19);
      -webkit-animation-name: animatetop;
      -webkit-animation-duration: 0.4s;
      animation-name: animatetop;
      animation-duration: 0.4s
    }

    /* Add Animation */
    @-webkit-keyframes animatetop {
      from {top:-300px; opacity:0} 
      to {top:0; opacity:1}
    }

    @keyframes animatetop {
      from {top:-300px; opacity:0}
      to {top:0; opacity:1}
    }

    /* The Close Button */
    .close {
      color: white;
      float: right;
      font-size: 28px;
      font-weight: bold;
    }

    .close:hover,
    .close:focus {
      color: #000;
      text-decoration: none;
      cursor: pointer;
    }

    .modal-header {
      padding: 2px 16px;
      background-color: #fe6845;
      color: white;
    }

    .modal-body {padding: 2px 16px;}

    .modal-footer {
      padding: 2px 16px;
      background-color: #fe6845;
      color: white;
    }
    /*--Modal Styling--*/

    .detail-status {
        display: flex;
        justify-content:space-evenly;
        flex-direction: row;
        width: 100%;
    }

    .maklumat-permohonan {
        width: 30%;
        display: block;
        overflow-y: auto;
        padding: 0 2px;
    }

    .maklumat-status {
        width: 70%;
        display: block;
        padding: 0 2px;
    }
    .btn-group {
        display: flex;
        justify-content: center;
    }
    .btn-group input {
        margin: 5px 2px;
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
    <div>
        <div class="collapsible fbform">
            Maklumat Permohonan (Klik untuk papar maklumat)
        </div>
        <div class="content">
            <div class="maklumat-pemohon">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="3" class="fbform">
                            <div class="tab-buttons">
                                <button class="tab-button" onclick="openContent(event, 'pemohon')" id="default">Maklumat Pemohon</button>
                                <button class="tab-button" onclick="openContent(event, 'keluarga')">Maklumat Keluarga</button>
                                <button class="tab-button" onclick="openContent(event, 'anak')">Maklumat Anak</button>
                            </div>
                            <div id="pemohon" class="tab-content">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 150px;">Pangkat</td>
                                        <td style="width: 5px;">:</td>
                                        <td><asp:Label Text="" runat="server" ID="lblPangkat" /></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;">No. Tentera</td>
                                        <td>:</td>
                                        <td><asp:Label Text="" runat="server" ID="lblNoTentera"/></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;">Nama</td>
                                        <td>:</td>
                                        <td>
                                            <asp:Label Text="" runat="server" ID="lblNama"/>
                                            <asp:HiddenField runat="server" ID="pID" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;">Jantina</td>
                                        <td>:</td>
                                        <td><asp:Label Text="" runat="server" ID="lblJantina"/></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;">Tarikh Lahir</td>
                                        <td>:</td>
                                        <td><asp:Label Text="" runat="server" ID="lblTarikhLahir"/></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;">Status Perkahwinan</td>
                                        <td>:</td>
                                        <td><asp:Label Text="" runat="server" ID="lblStatusKahwin"/></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;">Tarikh Mula Berkhidmat</td>
                                        <td>:</td>
                                        <td><asp:Label Text="" runat="server" ID="lblMulaBerkhidmat"/></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;">Tarikh Tamat Perkhidmatan</td>
                                        <td>:</td>
                                        <td><asp:Label Text="" runat="server" ID="lblTamatBerkhidmat"/></td>
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
                                        <td><asp:Label Text="text" runat="server" ID="lblJenisTempatTinggal" /></td>
                                    </tr>
                                    <tr>
                                        <td>Tarikh Mula Menetap</td>
                                        <td>:</td>
                                        <td><asp:Label Text="text" runat="server" ID="lblTarikhMulaMenetap" /></td>
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
        </div>
    </div>
    <div class="" style="width: 100%;">
        <div class="detail-status">
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
                   <tr runat="server" id="trUnitDitawarkan" visible="false">
                       <td style="width: 150px;">Unit Ditawarkan</td>
                       <td style="width: 5px;">:</td>
                       <td>
                           <asp:Label Text="text" runat="server" ID="lblUnit" />
                            <asp:HiddenField runat="server" ID="hfUnitID" />
                       </td>
                   </tr>
                   <tr runat="server" id="trTarikhMasuk" visible="false">
                       <td style="width: 150px;">Tarikh Masuk</td>
                       <td style="width: 5px;">:</td>
                       <td><asp:Label Text="text" runat="server" ID="lblTarikhMasuk" /></td>
                   </tr>
               </table>
           </div>
            <div class="maklumat-status">
                <table class="fbform" style="width: 100%" runat="server" id="btnField" visible="false">
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
                <asp:MultiView ActiveViewIndex="5" runat="server" ID="mvMaklumatStatus">
                    <%-- Permohonan baru --%>
                    <asp:View runat="server">
                        <div class="maklumat-baru fbform">
                            <table style="width:100%; height: 100%;">
                                <tr class="fbform_mheader">
                                    <td colspan="3">Status Permohonan</td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="align-content:center; text-align:center;">
                                        <p>Permohonan akan diproses secepat mungkin.</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="align-content:center; text-align:center;">
                                        <a 
                                            class="modal-btn" 
                                            id="openModal1" 
                                            href="#"
                                            style="color:red; cursor:pointer;"
                                        >Batal Permohonan?</a>
                                    </td>
                                </tr>
                            </table>
                            <div id="modal1" class="modal">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <span class="close">&times;</span>
                                        <h2>Batal Permohonan?</h2>
                                    </div>
                                    <div class="modal-body">
                                        <h4>Sebab membatalken permohonan:</h4>
                                        <div>
                                            <asp:TextBox 
                                                runat="server" 
                                                ID="tbSebabBatal"
                                                Text="Membatalkan permohonan."
                                                TextMode="MultiLine"
                                                width="400px"
                                                height="150px"
                                            />
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button Text="Simpan" CssClass="footer-btn" runat="server" ID="btnBatalPermohonan" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <%-- Permohonan menunggu(lulus) --%>
                    <asp:View runat="server">
                        <div class="maklumat-diterima fbform">
                            <table style="width:100%; height: 100%;">
                                <tr class="fbform_mheader">
                                    <td colspan="3">Status Permohonan</td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="align-content:center; text-align:center;">
                                        <p>Permohonan telah <b>DILULUSKAN</b>.</p>
                                        <p> Sila tunggu hingga surat tawaran disiapkan untuk no unit dan tarikh kemasukan.</p>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:View>
                    <%-- Tawaran unit --%>
                    <asp:View runat="server">
                        <div class="surat-tawaran fbform">
                            <table style="width:100%; height: 100%;">
                                <tr class="fbform_mheader">
                                    <td colspan="3">Status Permohonan</td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="align-content:center; text-align:center;">
                                       <p runat="server" id="pText"></p>
                                        <div class="fbform">
                                            <p runat="server" id="divSuratTawaran"></p>
                                        </div>
                                        <div class="btn-group" runat="server" id="btnGroupTerimaTawaran">
                                            <asp:Button Text="Terima" runat="server" ID="btnTerimaTawaran"/>
                                            <input type="button" id="openModal2" value="Tolak" />
                                        </div>
                                        <div id="modal2" class="modal">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <span class="close">&times;</span>
                                                    <h2>Tolak Permohonan?</h2>
                                                </div>
                                                <div class="modal-body">
                                                    <h4>Sebab menolak unit ditawarkan:</h4>
                                                    <div>
                                                        <asp:TextBox 
                                                            runat="server" 
                                                            ID="tbSebabTolak"
                                                            Text="Lokasi unit tak bersesuaian"
                                                            TextMode="MultiLine"
                                                            width="400px"
                                                            height="150px"
                                                        />
                                                    </div>
                                                </div>
                                                <div class="modal-footer">
                                                    <asp:Button Text="Simpan" CssClass="footer-btn" runat="server" ID="btnTolakTawaran" />
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:View>
                    <%-- Cadangan kuarters lain --%>
                    <asp:View runat="server">
                        <div class="cadangan-kuarters fbform">
                            <p>Kuarters yang dipohon tiada kekosongan, sila pilih dari senarai kuarters berikut jika masih ingin meneruskan permohonan.</p>
                            <asp:GridView
                                ID="gvSenaraiKuarters"
                                runat="server"
                                DataKeyNames="kuarters_dicadang"
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

                                    <asp:TemplateField HeaderText="Nama Kuarters">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKuarters" runat="server" Text='<%# Bind("kuarters_nama")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="40%" />
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Lokasi kuarters">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLokasiKuarters" runat="server" Text='<%# Bind("kuarters_alamat")%>'></asp:Label>
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
                                <EmptyDataTemplate>
                                    <span style="color: red;">Tiada Kuarters Dipilih
                                    </span>
                                </EmptyDataTemplate>
                                <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Bottom" BackColor="White" ForeColor="#284775" />
                            </asp:GridView>
                            <div class="btn-group" runat="server" id="bgCadangnaKuarters">
                                <asp:Button Text="Simpan" runat="server" ID="btnTerimaCadangan"/>
                                <div>
                                    <input type="button" ID="openModal3" value="Tolak"/>
                                    <div id="modal3" class="modal">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <span class="close">&times;</span>
                                        <h2>Batal Permohonan?</h2>
                                    </div>
                                    <div class="modal-body">
                                        <h4>Sebab membatalken permohonan:</h4>
                                        <div>
                                            <asp:TextBox 
                                                runat="server" 
                                                ID="tbTolakCadanganKuarters"
                                                Text="Membatalkan permohonan."
                                                TextMode="MultiLine"
                                                width="400px"
                                                height="150px"
                                            />
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button Text="Simpan" CssClass="footer-btn" runat="server" ID="btnTolakCadangan" />
                                    </div>
                                </div>
                            </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <%-- Keputusan batal/tolak --%>
                    <asp:View runat="server">
                        <div class="maklumat-ditolak">
                            <table class="fbform">
                                <tr class="fbform_mheader">
                                    <td colspan="3">Keputusan Permohonan</td>
                                </tr>
                                <tr>
                                    <td>Status Permohonan</td>
                                    <td>:</td>
                                    <td><asp:Label Text="text" runat="server" ID="lblKeputusanTolak"/></td>
                                </tr>
                                <tr>
                                    <td style="width:150px;">Sebab</td>
                                    <td style="width:5px;">:</td>
                                    <td>
                                        <asp:Label Text="text" runat="server" ID="lblSebabDitolak" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">Permohonan anda ditolak/dibatalkan.</td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="align-items:center;">
                                        <asp:LinkButton Text="Halaman Permohonan Kuarters" runat="server" ID="lbPermohonanBaru"/>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:View>
                    <%-- Permohonan diterima --%>
                    <asp:View runat="server">
                        <div class="maklumat-batal fbform">
                            <table>
                                <tr class="fbform_mheader">
                                    <td>Keputusan Permohonan</td>
                                </tr>
                                <tr>
                                    <td>Status Permohonan</td>
                                    <td>:</td>
                                    <td><asp:Label Text="text" runat="server" ID="lblKeputusanTerima"/></td>
                                </tr>
                                <tr>
                                    <td colspan="3">Surat Tawaran</td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div class="fbform">
                                            <p runat="server" id="pSuratTawaran"></p>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
        </div>
    </div>
    <table class="fbform">
        <tr>
            <td>
                <span id="MsgBottom" runat="server">
                    <asp:Label ID="strlbl_bottom" runat="server"></asp:Label>
                </span>
            </td>
        </tr>
    </table>
</div>
<script>
    //--TABS--
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
    //Tabs click default
    document.getElementById("default").click();
    //--TABS--

    //--COLLAPSIBLES
    var colls = document.getElementsByClassName("collapsible");
    for (var i = 0; i < colls.length; i++) {
        colls[i].addEventListener("click", function () {
            this.classList.toggle("active");
            var content = this.nextElementSibling;
            if (content.style.display === "block") {
                content.style.display = "none";
            } else {
                content.style.display = "block";
            }
        });
    }
    //--COLLAPSIBLES

    //MODAL
    var modal1 = document.getElementById("modal1");
    var modal2 = document.getElementById("modal2");
    var modal3 = document.getElementById("modal3");
    var btn1 = document.getElementById("openModal1");
    var btn2 = document.getElementById("openModal2");
    var btn3 = document.getElementById("openModal3")
    var closeSpan = document.getElementsByClassName("close")[0];

    if (typeof (btn1) != 'undefined' && btn1 != null) {
        btn1.onclick = function () {
            modal1.style.display = "block";
        }
    } else {
        console.log("btn1 undefined/null");
    }
    
    if (typeof (btn2) != 'undefined' && btn2 != null) {
        btn2.onclick = function () {
            modal2.style.display = "block";
        }
    } else {
        console.log("btn2 undefined/null");
    }


    if (typeof (btn3) != 'undefined' && btn3 != null) {
        btn3.onclick = function () {
            modal3.style.display = "block";
        }
    } else {
        console.log("btn3 undefined/null");
    }

    if (typeof (closeSpan) != 'undefined' && closeSpan != null) {
        closeSpan.onclick = function () {
            modal2.style.display = "none";
        }
    } else {
        console.log("closeSpan undefined/null");
    }
    //window.onclick = function (event) {
    //    if (event.target = modal) {
    //        modal.style.display = "none";
    //    }
    //}
    //--MODAL
</script>
