<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="konfigurasi_kuarters.ascx.vb" Inherits="QuartersManagement.konfigurasi_kuarters" %>
<style>
    .input {
        width: 20em;
    }
    .btn {
        margin-left: 10px;
    }
    .required {

    }
    .inline {
        display: flex;
        flex-direction: row;
        justify-content: stretch;
        overflow: auto;
        width:100%;
        height: 100%;
    }
    .left {
        width: 50%;
        display:block;
    }
    .right{
        width: 50%;
        display:block;
    }
</style>

<div>
    <asp:MultiView ActiveViewIndex="0" runat="server" ID="configKuarters">
        <asp:View runat="server">
            <table class="fbform" style="width: 100%">
                <tr class="fbform_header">
                    <td>
                        <span id="MsgTop" runat="server">
                            <asp:Label ID="strlbl_top" runat="server"></asp:Label>
                        </span>
                    </td>
                    <td>
                        <span class="buttonMenu">
                            <a href="#" runat="server" id="NewKuarters">
                                <img title="Tambah?" style="vertical-align: middle;" src="icons/add.png" width="25" height="25" alt="::" />
                            </a>
                        </span>
                    </td>
                </tr>
            </table>
            <%-- FIlter --%>
            <table class="fbform">
                <tr class="fbform_mheader">
                    <td colspan="3">Saringan</td> 
                </tr>
                <tr>
                    <td style="width: 150px;">Nama Pangkalan</td>
                    <td style="width: 5px;">:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlPangkalan" CssClass="input" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                
                <tr>
                    <td style="width: 150px;">Jenis Kuarters</td>
                    <td style="width: 5px;">:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlJenisKuarters" CssClass="input" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>

                <tr class="fbform_mheader">
                    <td colspan="3">Carian</td>
                </tr>
                <tr>
                    <td style="width: 150px;">Cari</td>
                    <td style="width: 5px;">:</td>
                    <td style="display: flex;">
                        <asp:TextBox runat="server" ID="tbCari" CssClass="input"/>
                        <asp:Button Text="Cari" runat="server" id="btnCari" CssClass="btn"/>
                    </td>
                </tr>
            </table>
            <%-- List --%>
            <table class="fbform">
                <tr class="fbform_header">
                    <td>Senarai Kuarters 
                        <asp:Label ID="lblConfig" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblQ" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel" runat="server" ScrollBars="vertical" Height="100%">
                            <asp:GridView 
                                ID="datRespondent" 
                                runat="server" 
                                AutoGenerateColumns="False" 
                                AllowPaging="true"
                                CellPadding="4" 
                                ForeColor="#333333" 
                                GridLines="None" 
                                DataKeyNames="kuarters_id"
                                Width="100%" 
                                PageSize="20" 
                                CssClass="gridview_footer">
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>

                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width="5%" />
                                        <ItemStyle VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="KUARTERS">
                                        <ItemTemplate>
                                            <asp:Label ID="kuarters_nama" runat="server" Text='<%# Bind("kuarters_nama")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="JENIS">
                                        <ItemTemplate>
                                            <asp:Label ID="jenisKuarters_nama" runat="server" Text='<%# Bind("jenisKuarters_nama")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="PANGKALAN">
                                        <ItemTemplate>
                                            <asp:Label ID="pangkalan_nama" runat="server" Text='<%# Bind("pangkalan_nama")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="NEGERI">
                                        <ItemTemplate>
                                            <asp:Label ID="kuarters_negeri" runat="server" Text='<%# Bind("kuarters_negeri")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ALAMAT">
                                        <ItemTemplate>
                                            <asp:Label ID="kuarters_alamat" runat="server" Text='<%# Bind("kuarters_alamat")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tindakan">
                                        <ItemTemplate>
                                            <asp:ImageButton
                                                Width="25px"
                                                Height="25px"
                                                runat="server"
                                                ID="edit_btn"
                                                CommandName="edit_kuarters"
                                                CommandArgument='<%#Eval("kuarters_id") %>'
                                                ImageUrl="~/icons/test.svg"
                                                ToolTip="Ubah?" />
                                            <asp:ImageButton
                                                Width="25px"
                                                Height="25px"
                                                runat="server"
                                                ID="delete_btn"
                                                CommandName="Delete"
                                                CommandArgument='<%#Eval("kuarters_id") %>'
                                                OnClientClick="javascript:return confirm('Adakah anda pasti mahu memadamkan item ini secara kekal? ')"
                                                ImageUrl="~/icons/delete.png"
                                                ToolTip="Padam?" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Underline="true" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                                    HorizontalAlign="Center" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <EmptyDataTemplate>
                                    <span style="color: red;">Tiada Bangunan Ditambah dalam kuarters ini.</span>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
            </table>  
            <table class ="fbform">
                <tr>
                    <td><span id ="MsgBottom" runat ="server" ><asp:Label ID ="strlbl_bottom" runat ="server" ></asp:Label></span></td>
                </tr>
            </table>
        </asp:View>
        <asp:View runat="server">
            <table class="fbform" style="width: 100%">
                <tr class="fbform_header">
                    <td>
                        <span id="Span1" runat="server">
                            <asp:Label ID="message_top" runat="server"></asp:Label>
                        </span>
                    </td>
                    <td>
                        <span class="buttonMenu">
                            <a href="#" runat="server" id="SaveTop">
                                <img title="Tambah?" style="vertical-align: middle;" src="icons/save.png" width="25" height="25" alt="::" />
                            </a> 
                            <a href="#" runat="server" id="UpdateTop" visible="false">
                                <img title="Ubah?" style="vertical-align: middle;" src="icons/save.png" width="25" height="25" alt="::" />
                            </a> |
                            <a href="#" runat="server" id="CancelTop">
                                <img title="Batal?" style="vertical-align: middle;" src="icons/cancel.png" width="25" height="25" alt="::" />
                            </a> 
                        </span>
                    </td>
                </tr>
            </table>
            <div>
                <table class="fbform" style="width: 100%;">
                    <tr class="fbform_mheader">
                        <td colspan="3">Maklumat Kuarters</td>
                    </tr>

                    <tr>
                        <td style="width: 150px;">Nama Pangkalan</td>
                        <td style="width: 5px;">:</td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlFormPangkalan" CssClass="input"></asp:DropDownList>
                            <asp:HiddenField runat="server" ID="hfPrevPangkalanID" />   
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 150px;">Jenis Kuarters</td>
                        <td style="width: 5px;">:</td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlFormJenisKuarters" CssClass="input"></asp:DropDownList>
                            <asp:HiddenField runat="server" id="hfKuartersID"/>
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="width: 150px;">Nama Kuarters</td>
                        <td style="width: 5px;">:</td>
                        <td>
                            <asp:TextBox runat="server" ID="tbFormNama" CssClass="input" />
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 150px;">Alamat</td>
                        <td style="width: 5px;">:</td>
                        <td>
                            <asp:TextBox runat="server" ID="tbFormAlamat" CssClass="input" />
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 150px;">Poskod</td>
                        <td style="width: 5px;">:</td>
                        <td>
                            <asp:TextBox runat="server" ID="tbFormPostcode" CssClass="input" type="number"/>
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 150px;">Bandar</td>
                        <td style="width: 5px;">:</td>
                        <td>
                            <asp:TextBox runat="server" ID="tbFormBandar" CssClass="input" />
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 150px;">Negeri</td>
                        <td style="width: 5px;">:</td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlFormNegeri" CssClass="input"></asp:DropDownList>
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="width: 150px;">No. Telefon</td>
                        <td style="width: 5px;">:</td>
                        <td>
                            <asp:TextBox runat="server" ID="tbFormTelefon" ClientIDMode="Static" CssClass="input"/>
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="width: 150px;">No. Fax</td>
                        <td style="width: 5px;">:</td>
                        <td>
                            <asp:TextBox runat="server" ID="tbFormNoFax" CssClass="input" ClientIDMode="Static"/>
                            <input id="sameAsCheckbox" type="checkbox" onclick="Same();"/>Sama dengan No. telefon?
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 150px;">Email</td>
                        <td style="width: 5px;">:</td>
                        <td>
                            <asp:TextBox runat="server" ID="tbFormEmail" CssClass="input" type="email"/>
                        </td>
                    </tr>
                </table>
                <asp:Panel runat="server" ID="panelTeres">

                </asp:Panel>
                <asp:Panel runat="server" ID="panelPangsapuri" Visible="false">
                    <div class="inline">
                        <div class="left">
                            <table class="fbform">
                                <tr class="fbform_mheader">
                                    <td colspan="3">Maklumat Bangunan Kuarters</td>
                                </tr>
                                <tr style="display: flex;">
                                    <td>Nama Blok/ Lot   :
                                        <asp:TextBox runat="server" ID="tbNamaBangunan" />
                                    </td>
                                    
                                    <td>Jumlah Aras / Baris Unit :
                                        <asp:TextBox runat="server" ID="tbJumlahArasBaris" />
                                        <asp:Button Text="Tambah" runat="server" ID="btnTambah" CssClass="btn" />
                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:GridView
                                            ID="buildingList"
                                            runat="server"
                                            AutoGenerateColumns="False"
                                            AllowPaging="false"
                                            CellPadding="4"
                                            ForeColor="#333333"
                                            GridLines="None"
                                            DataKeyNames="bangunan_id"
                                            Width="100%"
                                            PageSize="20"
                                            CssClass="gridview_footer">
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <Columns>

                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="1%" />
                                                    <ItemStyle VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Bangunan">
                                                    <ItemTemplate>
                                                        <asp:Label ID="bangunan_nama" runat="server" Text='<%# Bind("bangunan_nama")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%" />
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Jumlah Aras/ Baris">
                                                    <ItemTemplate>
                                                        <asp:Label ID="bangunan_jumlah_aras" runat="server" Text='<%# Bind("bangunan_jumlah_aras")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="2%" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Jumlah Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="bangunan_jumlah_unit" runat="server" Text='<%# Bind("jumlah_unit")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="2%" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Tindakan">
                                                    <ItemTemplate>
                                                         <asp:ImageButton
                                                            Width="25px"
                                                            Height="25px"
                                                            runat="server"
                                                            ID="ubah_btn"
                                                            CommandName="Ubah"
                                                            CommandArgument='<%#Eval("bangunan_id") %>'
                                                            ImageUrl="~/icons/test.svg"
                                                            ToolTip="Ubah?" />
                                                        <asp:ImageButton
                                                            Width="25px"
                                                            Height="25px"
                                                            runat="server"
                                                            ID="padam_btn"
                                                            CommandName="Padam"
                                                            CommandArgument='<%#Eval("bangunan_id") %>'
                                                            OnClientClick="javascript:return confirm('Adakah anda pasti mahu memadamkan item ini secara kekal? ')"
                                                            ImageUrl="~/icons/delete.png"
                                                            ToolTip="Padam?" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="2%" />
                                                    <ItemStyle  HorizontalAlign="Center" VerticalAlign="Middle" />
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
                        </div>
                        <div class="right">
                            <asp:Panel runat="server" ID="defaultPanel">
                                <table class="fbform">
                                    <tr class="fbform_mheader">
                                        <td colspan="3">Tamabah Unit</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="color:red;">
                                            <span style="">Sila pilih bangunan untuk di pamer</span>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="maklumatBangunan" Visible="false">
                                <table class="fbform">
                                    <tr class="fbform_mheader">
                                        <td colspan="3">Tambah Unit</td>
                                    </tr>
                                    
                                    <tr>
                                        <td style="width: 100px;">Nama Blok</td>
                                        <td style="width: 5px;">:</td>
                                        <td style="">
                                            <asp:Label Text="" runat="server" id="lblNamaBangunan"/>
                                            <asp:HiddenField runat="server" ID="hfBangunanID" />
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td style="width: 100px;">No. Aras/Baris</td>
                                        <td style="width: 5px;">:</td>
                                        <td style="">
                                            <asp:DropDownList runat="server" ID="ddlNoTingkat" CssClass="input" AutoPostBack="true"></asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 100px;">Jumlah Unit</td>
                                        <td style="width: 5px;">:</td>
                                        <td style="">
                                            <asp:TextBox runat="server" ID="tbJumlahUnit" type="number" CssClass="input"/>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td colspan="3" style="display: flex; justify-content:center;">
                                            <asp:Button Text="Tambah Unit" runat="server" id="btnTambahUnit"/>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                    </div>
                    
                </asp:Panel>
            </div>
           
             <table class="fbform" style="width: 100%">
                <tr class="fbform_header">
                    <td>
                        <span id="Span2" runat="server">
                            <asp:Label ID="message_bottom" runat="server"></asp:Label>
                        </span>
                    </td>
                    <td>
                        <span class="buttonMenu">
                            <a href="#" runat="server" id="SaveBottom">
                                <img title="Tambah?" style="vertical-align: middle;" src="icons/save.png" width="25" height="25" alt="::" />
                            </a> 
                            <a href="#" runat="server" id="UpdateBottom" visible="false">
                                <img title="Ubah?" style="vertical-align: middle;" src="icons/save.png" width="25" height="25" alt="::" />
                            </a> |
                            <a href="#" runat="server" id="CancelBottom">
                                <img title="Batal?" style="vertical-align: middle;" src="icons/cancel.png" width="25" height="25" alt="::" />
                            </a> 
                        </span>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</div>

<script type="text/javascript">
    var noTel = document.getElementById('tbFormTelefon');
    var noFax = document.getElementById('tbFormNoFax');
    function Same() {
        if (noTel !== null && noFax !== null) {
            noFax.value = noTel.value;
        }
    }
</script>


