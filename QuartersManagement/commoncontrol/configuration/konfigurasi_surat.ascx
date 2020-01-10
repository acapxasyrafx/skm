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
        width: 65%;
        height: 65%;
        min-width: 60%;
        min-height: 60%;
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
    .primary, .warning {
        color: white;
    }
    .primary {
        background: #2196f3;
    }
    .warning {
        background: #ffab00;
    }

    .footer-btn{
        display:flex;
        justify-content: space-between;
    }

    .editor {
        display: flex;
        align-items: center;
    }
</style>
<table class="fbform" style="width: 100%">
    <tr class="fbform_header">
        <td>
            <span id="MsgTop" runat="server">
                <asp:Label ID="strlbl_top" runat="server"></asp:Label></span>
        </td>
        <td>
            <span class="buttonMenu">
                <a href="#" runat="server" id="AddNew" name="AddNew">
                    <img title="Tambah" style="vertical-align: middle;" src="icons/add.png" width="25" height="25" alt="::" />
                </a>
            </span>
        </td>
    </tr>
</table>
<table class="fbform" style="width: 100%">
    <tr class="fbform_mheader">
        <td colspan="4">Carian</td>
    </tr>
    <tr>
        <td style="width: 150px;">Carian Surat</td>
        <td style="width: 5px;">:</td>
        <td>
            <asp:TextBox runat="server" ID="tbCarian" />
            <asp:Button Text="Cari" runat="server" ID="btnCari" />
        </td>
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

                        <asp:TemplateField HeaderText="Kandungan">
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
                                    ID="Edit_Surat"
                                    CommandName="Ubah"
                                    CommandArgument='<%#Eval("suratTawaranConfig_id") %>'
                                    ImageUrl="~/icons/test.svg"
                                    ToolTip="Edit?" />
                                <asp:ImageButton
                                    Width="25px"
                                    Height="25px"
                                    ID="Delete_Surat"
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
<table class="fbform">
    <tr>
        <td><span id="MsgBottom" runat="server">
            <asp:Label ID="strlbl_bottom" runat="server"></asp:Label></span></td>
    </tr>

</table>

<div id="modalSurat" name="suratForm" class="modal" runat="server">
    <div class="fbform modal-content">
        <div class="modal-header">
            <a href="#" runat="server" ClientIDMode="Static" id="closeSpan">
                <span class="close">&times;</span>
            </a>
            <h2>Tambah Surat Tawaran Baru</h2>
        </div>
        <div class="modal-body">

            <div class="form-container">
                <table style="width: 100%;" class="fbform">
                    <tr>
                        <td style="width: 150px;">Tajuk Surat</td>
                        <td style="width: 5px;">:</td>
                        <td>
                            <asp:TextBox runat="server" ID="tajukSurat" ClientIDMode="Static" />
                        </td>
                    </tr>
                    <tr class="fbform_mheader">
                        <td colspan="3">Kandungan Surat:</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <textboxio:Textboxio
                                runat="server"
                                ID="editorSuratContent"
                                Content="<p></p>" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="modal-footer">
            <div class="footer-btn">
                <asp:Button Text="Simpan" runat="server" ID="btnSubmit" CssClass="btn primary" />
                <asp:Button Text="Tutup" runat="server" ID="btnClose" CssClass="btn warning" />
            </div>
        </div>
    </div>
</div>

<script src="textboxio/textboxio.js"></script>
<script type="text/javascript">

</script>

<script type="text/vbscript">

</script>

