<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="konfigurasi_surat.ascx.vb" Inherits="QuartersManagement.konfigurasi_surat" %>
<%@ Register Assembly="TextboxioControl" Namespace="TextboxioControl" TagPrefix="textboxio" %>

<style>

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
        from {
            top: -300px;
            opacity: 0
        }

        to {
            top: 0;
            opacity: 1
        }
    }

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
        background-color: #5cb85c;
        color: white;
    }

    .modal-body {
        padding: 2px 16px;
    }

    .modal-footer {
        padding: 2px 16px;
        background-color: #5cb85c;
        color: white;
    }
    .form-container{
        padding: 5px;
        margin: 0;
    }
    .form-inline {
        display:flex;
        flex-direction: row;
        justify-content: space-between;
    }
    .form-inline textarea {
        width: 80%;
        height: 100%;
    }
    .btn-column {
        display: flex;
        flex-direction: column;
        justify-content: space-evenly;
        width: 20%;
    }
    .btn {
        width: 100%;
        margin: 5px;
        padding: 10px;
    }

    .footer-btn{
        display:flex;
        justify-content: space-between;
    }
</style>
<table class ="fbform" style ="width :100%">
    <tr class ="fbform_header">
         <td>
             <span id="MsgTop" runat ="server"><asp:Label ID ="strlbl_top" runat ="server" ></asp:Label></span>
         </td>
         <td>
            <span class="buttonMenu">
                <a href="javascript:void(0)" runat ="server" id="AddNew" name="AddNew">
                    <img title="Tambah" style="vertical-align: middle;" src="icons/add.png" width="25" height="25" alt="::"/>
                </a>
            </span>
         </td>
    </tr>
</table>
<table class="fbform" style="width :100%">
    <tr class="fbform_mheader">
        <td colspan="3">Carian</td>
    </tr>
    <tr>
         <td style="width: 150px;">Carian Surat</td>
        <td style="width: 5px;">:</td>
        <td>
            <asp:TextBox runat="server" ID="tbCarian"/>
        </td>
    </tr>
    <tr>
        <td><asp:Button Text="Cari" runat="server" id="btnCari"/></td>
    </tr>
</table>

<br />
<script src="textboxio/textboxio.js"></script>
<%-- List --%>
<table class="fbform">
    <tr class="fbform_mheader">
        <td colspan="3">Senarai Format Surat</td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID="Panel" runat="server" ScrollBars="vertical" Height="350">
                <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                    CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="suratTawaranConfig_id"
                    Width="100%" PageSize="100" CssClass="gridview_footer">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>

                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tajuk Surat">
                            <ItemTemplate>
                                <asp:Label ID="pangkalan_negeri" runat="server" Text='<%# Bind("suratTawaranConfig_type")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Isi">
                            <ItemTemplate>
                                <asp:Label ID="pangkalan_negara" runat="server" Text='<%# Bind("suratTawaranConfig_parameter")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50%" />
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:ImageButton
                                    Width="25px"
                                    Height="25px"
                                    runat="server"
                                    ID="Ubah_Surat"
                                    CommandName="Ubah"
                                    CommandArgument='<%#Eval("suratTawaranConfig_id") %>'
                                    ImageUrl="~/icons/test.svg"
                                    ToolTip="Edit?" />
                                <asp:ImageButton
                                    Width="25px"
                                    Height="25px"
                                    ID="Padam_Surat"
                                    CommandName="Padam"
                                    CommandArgument='<%#Eval("suratTawaranConfig_id")%>'
                                    OnClientClick="javascript:return confirm('Adakah anda pasti mahu memadamkan item ini secara kekal? ')"
                                    runat="server"
                                    ImageUrl="~/icons/delete.png"
                                    ToolTip="Delete" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="40%" />
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
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="3"></td>
    </tr>
</table>
<table class ="fbform">
    <tr>
        <td><span id ="MsgBottom" runat ="server" ><asp:Label ID ="strlbl_bottom" runat ="server" ></asp:Label></span></td>
    </tr>

</table>

<div id="suratForm" name="suratForm" class="modal" runat="server">
  <div class="modal-content">
    <div class="modal-header">
      <span class="close">&times;</span>
      <h2>Tambah Surat Tawaran Baru</h2>
    </div>
    <div class="modal-body">
      <div class="form-container">
          <table style="width: 100%;" class="fbform">
              <tr>
                  <td style="width: 150px;">Tajuk Surat</td>
                  <td style="width: 5px;">:</td>
                  <td>
                      <asp:TextBox runat="server" id="tbTajukSurat" ClientIDMode="Static"/>
                  </td>
              </tr>
              <tr class="fbform_mheader">
                  <td colspan="3">Kandungan Surat:</td>
              </tr>
              <tr>
                  <td colspan="3">
                      <div class="form-inline">
                          <textarea 
                              id="editorContentSurat" 
                              name="editorContentSurat"
                              runat="server"
                               ClientIDMode="Static"
                              rows="100"
                            ></textarea>
                          <div class="btn-column">
                              <%--<asp:Repeater runat="server" ID="repeaterButtons">
                                  <ItemTemplate>
                                    <button type="button" onclick='insertText('<%# %>')' class="btn" id="btnPemohon">Nama Pemohon</button>
                                  </ItemTemplate>
                              </asp:Repeater>--%>
                              <button type="button" onclick="insertText('{NAMA_PEMOHON}')" class="btn" id="btnPemohon">Nama Pemohon</button>
                              <button type="button" onclick="insertText('{NAMA_UNIT}')" class="btn" id="btnUnit">Nama Unit</button>
                              <button type="button" onclick="insertText('{NAMA_KUARTERS}')" class="btn" id="btnKuarters">Nama Kuarters</button>
                              <button type="button" onclick="insertText('{NAMA_PANGKALAN}')" class="btn" id="btnPangkalan">Nama Pangkalan</button>
                          </div>
                      </div>
                  </td>
              </tr>
          </table>
      </div>
    </div>
    <div class="modal-footer">
        <div class="footer-btn">
            <asp:Button Text="Simpan" runat="server" id="btnSimpan" CssClass="btn"/>
            <button type="button" id="btnClose" class="btn">Batal</button>
        </div>
    </div>
  </div>

</div>

<script type="text/javascript">
    var btn = document.getElementsByName("AddNew")[0];
    var modal = document.getElementsByName("suratForm")[0];
    var span = document.getElementsByClassName("close")[0];
    var editor = textboxio.replace('#editorContentSurat');
    var btnClose = document.getElementById("btnClose");

    editor.content.set('Masukkan surat tawaran disini...');

    btn.onclick = function () {
        modal.style.display = "block";
    }

    span.onclick = function () {
        modal.style.display = "none";
    }

    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }

    btnClose.onclick = function () {
        modal.style.display = "none";
    }

    function insertText(text) {
        editor.content.insertHtmlAtCursor(text);
    }
</script>

<script type="text/vbscript">

</script>

