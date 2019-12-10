<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="maklumat_pemohon.ascx.vb" Inherits="QuartersManagement.maklumat_pemohon" %>

<style>
    .h5{
         font-weight:normal; 
    }
    .label{
        display: inline-block;
        margin:0;
        padding:5px;
        font-size: 100%;        
        font-weight:normal;
    }
    .left {
        display:flex;
        flex: initial;
    }
    .auto-style2 {
        width: 14px;
    }
    .auto-style3 {
        width: 320px;
    }
    .auto-style4 {
        width: 635px;
    }
    .auto-style5 {
        width: 133px;
    }
    .wrapper{
        display:flex;
        height: 100%;
        width: 100%;
        margin:0;
        padding:0;
    }

    .left_content{
        width: 50%;
    }

    .right_content{
        width: 50%;
    }

    /* The Modal (background) */
    .modal {
        display: none; /* Hidden by default */
        position: fixed; /* Stay in place */
        z-index: 1; /* Sit on top */
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
        width: 50%;
        box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2),0 6px 20px 0 rgba(0,0,0,0.19);
        animation-name: animatetop;
        animation-duration: 0.4s
    }

    /* The Close Button */
    .close {
        color: #aaa;
        float: right;
        font-size: 28px;
        font-weight: bold;
        border: 1px solid black;
        border-radius: 5px;
    }

    .close:hover,
    .close:focus {
        color: black;
        text-decoration: none;
        cursor: pointer;
    }

    /* Modal Header */
    .modal-header {
        padding: 2px 16px;
        /*background-color: #f9a825;*/
        color: white;
    }

    .confirm{
        background-color:#71a95a;
    }

    .reject{
        background-color:#f9a825;
    }

    /* Modal Body */
    .modal-body {
        padding: 2px 10px;
    }

    /* Modal Footer */
    .modal-footer {
        padding: 2px 16px;
        /*background-color: #f9a825;*/
        /*color: white;*/
    }

    /* Add Animation */
    @keyframes animatetop {
        from {
            top: -300px;
            opacity: 0
        }

        to {
            top: 0;
            opacity: 1
        }
    }

    .button {
        border: 1px solid black;
        color: white;
        padding: 10px;
        text-align: center;
        text-decoration: none;
        display: inline-block;
        font-size: 16px;
        margin: 4px 2px;
        cursor: pointer;
        border-radius: 8px;
    }

    .save{
        background-color: #9E9E9E; /* Green */
    }

    .cancel{
        background-color: #ff8a65; /* AMBER 600 */
    }
    .rowJumlahMata{
        border-top-style: solid;
    }
    select{
        width: 20em;
    }
</style>

 <script>
        function GetUserValue() {
            var person = prompt("Sebab menolak permohonan ini:", "");
            if (person != null && person != "") {
                document.getElementById("<%=hdnUserInput.ClientID%>").value = person;
                return true;
            }
            else
                return false;
        }
    </script>

<table class="fbform" style="width: 100%">
    <tr class="fbform_header">
        <td><span id="MsgTop" runat="server">
            <asp:Label ID="strlbl_top" runat="server"></asp:Label></span></td>
        <td>
            <span class="buttonMenu">
                <a href="#" id="Refresh" runat="server">
                    <img title="Refresh" style="vertical-align: middle;" src="icons/refresh.png" width="22" height="22" alt="::" /></a>
                | <a href="#" id="Help">
                    <img title="Help" style="vertical-align: middle;" src="icons/help.png" width="22" height="22" alt="::" /></a>
            </span>
        </td>
    </tr>
</table>
<div class="wrapper">
    <div class="left_content">
        <%-- BUTIRAN PERIBADI --%>
        <table class="fbform" style="width: 100%;">
            <tr class="fbform_mheader">
                <td colspan="3">Butiran Peribadi</td>
            </tr>

            <tr>
                <td style="width: 150px;">Pangkat</td>
                <td style="width: 5px;">:</td>
                <td>
                    <h5 class="label" style="font-weight: normal;" runat="server" id="lblJawatan"></h5>
                </td>
            </tr>
            <tr>
                <td style="width: 150px;">No. Tentera</td>
                <td style="width: 5px;">:</td>
                <td>
                    <h5 class="label" style="font-weight: normal;" runat="server" id="lblNoTentera"></h5>
                </td>
            </tr>
            <tr>
                <td style="width: 150px;">Nama</td>
                <td style="width: 5px;">:</td>
                <td>
                    <h5 class="label" runat="server" id="lblNama"></h5>
                    <asp:HiddenField runat="server" ID="pengguna_id" Value="" />
                </td>
            </tr>
            <tr>
                <td style="width: 150px;">Jantina</td>
                <td style="width: 5px;">:</td>
                <td>
                    <h5 class="label" style="font-weight: normal;" runat="server" id="lblJantina"></h5>
                </td>
            </tr>
            <tr>
                <td style="width: 150px;">Tarikh Lahir</td>
                <td style="width: 5px;">:</td>
                <td>
                    <h5 class="label" style="font-weight: normal;" runat="server" id="lblTarikhLahir"></h5>
                </td>
            </tr>
            <tr>
                <td style="width: 150px;">Status Perkahwinan</td>
                <td style="width: 5px;">:</td>
                <td>
                    <h5 class="label" style="font-weight: normal;" runat="server" id="lblStatusPerkahwinan"></h5>
                </td>
            </tr>
            <tr>
                <td style="width: 150px;">Tarikh Mula Berkhidmat</td>
                <td style="width: 5px;">:</td>
                <td>
                    <h5 class="label" style="font-weight: normal;" runat="server" id="lblTarikhMulaBerkhidmat"></h5>
                </td>
            </tr>
            <tr>
                <td style="width: 150px;">Tarikh Tamat Perkhidmatan</td>
                <td style="width: 5px;">:</td>
                <td>
                    <h5 class="label" style="font-weight: normal;" runat="server" id="lblTarikhAkhirBerkhidmat"></h5>
                </td>
            </tr>
        </table>
        <%-- MAKLUMAT ANAK --%>
        <table runat="server" class="fbform" id="tblMaklumatAnak" style="width: 100%;">
            <tr class="fbform_mheader">
                <td colspan="3">Maklumat Anak
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView
                        ID="datRespondent"
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
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%" />
                                <ItemStyle VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Nama">
                                <ItemTemplate>
                                    <asp:Label ID="lblNamaAnak" runat="server" Text='<%# Bind("historyAnak_nama")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="IC">
                                <ItemTemplate>
                                    <asp:Label ID="lblICAnak" runat="server" Text='<%# Bind("historyAnak_ic")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="UMUR">
                                <ItemTemplate>
                                    <asp:Label ID="lblUmurAnak" runat="server" Text='<%# icToAge(Eval("historyAnak_ic"))%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Underline="true" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                            HorizontalAlign="Center" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <%-- BUTIRAN KELUARGA --%>
        <table class="fbform">
            <tr class="fbform_mheader">
                <td colspan="3">Butiran Keluarga</td>
            </tr>
            <tr>
                <td style="width: 150px;">Jenis Tempat Tinggal Akhir</td>
                <td style="width: 5px;">:</td>
                <td>
                    <asp:Label runat="server" CssClass="label" ID="lblJenisPenempatan"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 150px;">Mula Menetap Dari</td>
                <td style="width: 5px;">:</td>
                <td>
                    <asp:Label runat="server" ID="lbltarikhPenempatan" CssClass="label"> </asp:Label>
                </td>
            </tr>
        </table>
        <%-- BUTIRAN PERMOHONAN --%>
        <table class="fbform">
            <tr class="fbform_mheader">
                <td colspan="3">Butiran Permohonan</td>
            </tr>
            <tr>
                <td style="width: 150px;">Pangkalan</td>
                <td style="width: 5px;">:</td>
                <td colspan="2">
                    <h5 class="label" runat="server" id="lbl_senaraiPangkalan"></h5>
            </tr>
            <tr>
                <td style="width: 150px;">Kuarters/Rumah</td>
                <td style="width: 5px;">:</td>
                <td>
                    <h5 class="label" runat="server" id="lbl_senaraiKuarters"></h5>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table runat="server" id="tblBertukar" visible="false">
                        <tr>
                            <td>Dari Pasukan</td>
                            <td>:</td>
                            <td>
                                <h5 class="label" style="font-weight: normal;" runat="server" id="lbl_pasukanLama"></h5>
                            </td>
                        </tr>
                        <tr>
                            <td>Ke Pasukan</td>
                            <td>:</td>
                            <td>
                                <h5 class="label" runat="server" id="lbl_pasukanBaru"></h5>
                                <asp:Label CssClass="label" runat="server" ID="lblPasukanBaru"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Tarikh Bertukar</td>
                            <td>:</td>
                            <td>
                                <h5 class="label" runat="server" id="H1"></h5>
                                <asp:Label runat="server" CssClass="label" ID="lbltarikhBertukar"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div class="right_content">
        <div class="fbform">
            <table style="width: 100%; margin:0;padding:0;">
                <tr class="fbform_mheader">
                    <td>Pengiraan Mata Kelulusan</td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%; border: 1px solid black;">
                            <tr>
                                <td>Kategori</td>
                                <td>Mata</td>
                                <td>Bilangan</td>
                                <td>Jumlah</td>
                            </tr>
                            <tr>
                                <td>Pangkat</td>
                                <td><asp:Label Text="text" runat="server" ID="lblMataPangkat"/></td>
                                <td>-</td>
                                <td><asp:Label Text="text" runat="server" ID="lblJumlahMatapangkat"/></td>
                            </tr>
                            <tr>
                                <td>Bilangan Anak(Umur bawah 18 tahun)</td>
                                <td><asp:Label Text="text" runat="server" ID="lblMataAnak"/></td>
                                <td><asp:Label Text="text" runat="server" ID="lblJumlahAnakLayak"/></td>
                                <td><asp:Label Text="text" runat="server" ID="lblJumlahMataAnak"/></td>
                            </tr>
                            <tr>
                                <td colspan="3" style="border-top-style: solid;">Jumlah Mata</td>
                                <td style="border-top-style: solid;"><b><asp:Label Text="text" runat="server" ID="lblJumlahMata"/></b></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table  style="width: 100%;">
                <tr runat="server" visible="false">
                    <td style="width: 25em;">Status Permohonan(Mengikut Sistem) ?</td>
                    <td style="width: 5em;">:</td>
                    <td>
                        <asp:Label Text="text" runat="server" ID="lblStatusKelayakan"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px;">Luluskan Permohonan ?</td>
                    <td style="width: 5px;">:</td>
                    <td style="text-align: left">
                        <asp:ImageButton 
                            runat="server" 
                            ID="btnImg_lulus" 
                            CommandName="Approved" 
                            CommandArgument='<%#Eval("permohonan_id")%>' 
                            ImageUrl="~/icons/checkmark_approve.png" 
                            ToolTip="Diterima" Height="39px" 
                        />
                        &nbsp&nbsp&nbsp&nbsp&nbsp
                        <asp:ImageButton 
                            runat="server" 
                            ID="btnImg_ditolak" 
                            Height="39px" 
                            CommandName="Rejected" 
                            CommandArgument='<%#Eval("permohonan_id")%>' 
                            ImageUrl="~/icons/checkmark_declined.png" 
                            ToolTip="Ditolak" 
                        />
                        <asp:HiddenField runat="server" ID="hdnUserInput" />
                    </td>
                </tr>
            </table> 
        </div>
    </div>
</div>

<table>
    <tr>
        <td colspan="3"></td>
    </tr>
</table>

<table class="fbform">
    <tr>
        <td>
            <span id="MsgBottom" runat="server">
            <asp:Label ID="strlbl_bottom" runat="server"></asp:Label></span>
        </td>
    </tr>
</table>

<div runat="server" class="modal" id="dialogModal">
    <table class="fbform modal-content">
        <tr class="modal-header reject">
            <td class="fbform_mheader" colspan="2">
                <h3 style="">Menolak Permohonan<button runat="server" class="close" id="closeBtn">&times;</button></h3>
            </td>
        </tr>
        <tr>
            <td colspan="3">Sila nyatakan sebab untuk permohonan ditolak?</td>
        </tr>
        <tr class="modal-body">
            <td colspan="3" style="width: 100px;">
                <asp:TextBox ID="txtNota" TextMode="multiline" Columns="100" Rows="5" runat="server" Text="Permohonan Tidak Layak/Tidak Diterima" />
            </td>
        </tr>
        <tr class="modal-footer reject">
            <td>
                <asp:Button CssClass="button save" Text="Simpan" runat="server" ID="btnTolakPermohonan" />
                <asp:Button CssClass="button cancel" Text="Tutup" runat="server" ID="btnTutupModal1" />
            </td>
        </tr>
    </table>
</div>

<div runat="server" class="modal" id="confirmModal">
    <table class="modal-content">
        <tr class="modal-header confirm">
            <td class="fbform_mheader" colspan="3">
                <h3 class="">Anda Pasti?<button runat="server" class="close" id="Button1">&times;</button></h3>
            </td>
        </tr>
        <tr class="modal-body">
            <td colspan="3" style="height: 5em; display: flex; justify-content: center;">
                Adakah anda pasti untuk MELULUSKAN permohonan ini?
            </td>
        </tr>
        <tr class="modal-footer confirm">
            <td>
                <asp:Button CssClass="button save" Text="Simpan" runat="server" ID="btnTerimaTawaran" />
                <asp:Button CssClass="button cancel" Text="Tutup" runat="server" ID="btnTutupModal2" />
            </td>
        </tr>
    </table>
</div>
