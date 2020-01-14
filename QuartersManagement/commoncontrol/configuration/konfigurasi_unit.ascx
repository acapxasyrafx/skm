<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="konfigurasi_unit.ascx.vb" Inherits="QuartersManagement.konfigurasi_unit" %>
<style>
    .input {
        width: 20em;
    }
    .required {
        color:red;
    }
</style>
<div>
    <asp:MultiView ActiveViewIndex="0" runat="server" ID="viewConfig">
        <asp:View runat="server">
            <table class="fbform">
                <tr>
                    <td>
                        <span id="Span1" runat="server">
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                        </span>
                    </td>
                    <td>
                        <span class="buttonMenu">
                            <a href="#" runat="server" id="tambahUnit">
                                <img title="Tamabah Baru?" style="vertical-align: middle;" src="icons/add.png" width="25" height="25" alt="" />
                            </a>
                        </span>
                    </td>
                </tr>
            </table>
            <table class="fbform" style="width: 100%">
                <tr class="fbform_mheader">
                    <td colspan="4">Saringan</td>
                </tr>
                <tr>
                    <td style="width: 150px;">Nama Pangkalan</td>
                    <td style="width: 5px;">:</td>
                    <td colspan="2">
                        <asp:DropDownList runat="server" ID="ddlPangkalan" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px;">Nama Kuarters</td>
                    <td style="width: 5px;">:</td>
                    <td colspan="2">
                        <asp:DropDownList runat="server" ID="ddlKuarters" AutoPostBack="true" Enabled="false">
                            <asp:ListItem Text="--Sila Pilih Pangkalan Terlebih Dahulu--" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px;">Status Unit</td>
                    <td style="width: 5px;">:</td>
                    <td>
                        <asp:DropDownList runat="server" id="ddlStatusUnit" CssClass="input" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr class="fbform_mheader">
                    <td colspan="4">Carian Unit</td>
                </tr>
                <tr>
                    <td style="width: 150px;">Nama Unit</td>
                    <td style="width: 5px;">:</td>
                    <td>
                        <asp:TextBox runat="server" ID="tbCari" /></td>
                </tr>
                <tr>
                    <td style="width: 150px;"></td>
                    <td>
                        <asp:Button Text="Cari" runat="server" ID="btnCari" /></td>
                </tr>
            </table>
            <table class="fbform" style="width: 100%;">
                <tr class="fbform_mheader">
                    <td colspan="3">Senarai Unit
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel" runat="server" ScrollBars="Vertical" Height="90%">
                            <asp:GridView
                                ID="datRespondent"
                                runat="server"
                                AutoGenerateColumns="False"
                                AllowPaging="true"
                                CellPadding="4"
                                ForeColor="#333333"
                                GridLines="None"
                                DataKeyNames="unit_id"
                                Width="100%"
                                PageSize="20"
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

                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="unit_nama" runat="server" Text='<%# nama_kuarters(Eval("unit_id")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Kuarters (Jenis Kuarters)">
                                        <ItemTemplate>
                                            <asp:Label ID="kuarters_nama" runat="server" Text='<%# Bind("kuarters_nama") %>'></asp:Label>
                                            (<asp:Label ID="jenis_kuarters" runat="server" Text='<%# Bind("jenisKuarters_nama") %>'></asp:Label>)
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Pangkalan">
                                        <ItemTemplate>
                                            <asp:Label ID="pangkalan_nama" runat="server" Text='<%# Bind("pangkalan_nama")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30%" />
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit Status">
                                        <ItemTemplate>
                                            <asp:Label ID="unit_status" runat="server" Text='<%# Bind("config_parameter") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tindakan">
                                        <ItemTemplate>
                                            <asp:ImageButton
                                                Width="25px"
                                                Height="25px"
                                                runat="server"
                                                ID="edit_btn"
                                                CommandName="edit_unit"
                                                CommandArgument='<%#Eval("unit_id") %>'
                                                ImageUrl="~/icons/test.svg"
                                                ToolTip="Ubah?" />
                                            <asp:ImageButton
                                                Width="25px"
                                                Height="25px"
                                                runat="server"
                                                ID="delete_btn"
                                                CommandName="Delete"
                                                CommandArgument='<%#Eval("unit_id") %>'
                                                OnClientClick="javascript:return confirm('Adakah anda pasti mahu memadamkan item ini secara kekal? ')"
                                                ImageUrl="~/icons/clear.svg"
                                                ToolTip="Padam?" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%" />
                                        <ItemStyle VerticalAlign="Middle" />
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
            </table>
            <table class="fbform">
                <tr>
                    <td>
                        <span id="Span2" runat="server">
                            <asp:Label ID="Label2" runat="server"></asp:Label>
                        </span>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View runat="server">
            <table class="fbform" style="width: 100%">
                <tr class="fbform_header">
                    <td><span id="MsgTop" runat="server">
                        <asp:Label ID="strlbl_top" runat="server"></asp:Label></span></td>
                    <td>
                        <span class="buttonMenu">
                            <a href="#" runat="server" id="SaveTop">
                                <img title="Simpan?" style="vertical-align: middle;" src="icons/save.png" width="25" height="25" alt="" />
                            </a>
                            <a href="#" runat="server" id="UpdateTop" visible="false">
                                <img title="Simpan?" style="vertical-align: middle;" src="icons/save.png" width="25" height="25" alt="" />
                            </a>|
                            <a href="#" runat="server" id="CancelTop">
                                <img title="Kembali?" style="vertical-align: middle;" src="icons/x-button.png" width="25" height="25" alt="" />
                            </a>
                        </span>
                    </td>
                </tr>
            </table>
            <table class="fbform" style="width: 100%">
                <tr class="fbform_mheader">
                    <td colspan="3">Maklumat Unit</td>
                </tr>
                <tr>
                    <td style="width: 150px;">Pangkalan</td>
                    <td style="width: 5px;">:</td>
                    <td>
                        <asp:Label Text="" runat="server" id="lblNamaPangkalan" Visible="false"/>
                        <asp:DropDownList runat="server" ID="ddlInsertPangkalan" CssClass="input" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px;">Kuarters</td>
                    <td style="width: 5px;">:</td>
                    <td style="display: flex;">
                        <asp:Label Text="" runat="server" id="lblNamaKuarters" Visible="false"/>
                        <asp:DropDownList runat="server" ID="ddlInsertKuarters" CssClass="input" AutoPostBack="true" Enabled="false">
                            <asp:ListItem Text="Sila PIlih Pangkalan Terlebih Dahulu" />
                        </asp:DropDownList>
                        <div style="padding-left: 5px;" runat="server" id="divJenisKuarters" visible="false">
                            (Jenis Kuarters:
                            <asp:Label runat="server" ID="lblJenisKuarters"></asp:Label>)
                        </div>
                    </td>
                </tr>
            </table>
            <asp:Panel runat="server" ID="panelMaklumatUnit" Visible="false">
                <table class="fbform">
                    <asp:Panel runat="server" ID="panelBanglo">
                        <tr class="fbform_mheader" runat="server" id="trbanglo" visible="false">
                            <td colspan="4">Maklumat Banglo</td>
                        </tr>
                        <tr>
                            <td style="width: 150px;">Nama Bangunan</td>
                            <td style="width: 5px;">:</td>
                            <td>
                                <asp:TextBox runat="server" ID="tbBangloNama" CssClass="input" />
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="panelTeres" Visible="false">
                        <tr class="fbform_mheader">
                            <td colspan="4">Maklumat Rumah Teres</td>
                        </tr>
                        <tr>
                            <td style="width: 150px;">Lot Unit</td>
                            <td style="width: 5px;">:</td>
                            <td>
                                <asp:TextBox runat="server" ID="tbTeresNoBaris" CssClass="input" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 150px;">No. Unit</td>
                            <td style="width: 5px;">:</td>
                            <td>
                                <asp:TextBox runat="server" ID="tbTeresNoUnit" CssClass="input" />
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="panelPangsapuri" Visible="false">
                        <tr class="fbform_mheader">
                            <td colspan="4">Maklumat Pangsapuri</td>
                        </tr>
                        <tr>
                            <td style="width: 150px;">Nama Blok</td>
                            <td style="width: 5px;">:</td>
                            <td>
                                <asp:TextBox runat="server" ID="tbPangsapuriBlok" CssClass="input" />
                                <asp:Label Text="" runat="server" id="lblBlok"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 150px;">No. Tingkat</td>
                            <td style="width: 5px;">:</td>
                            <td>
                                <asp:TextBox runat="server" ID="tbPangsapuriTingkat" CssClass="input" />
                                <asp:Label Text="" runat="server" id="lblTingkat"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 150px;">No. Unit</td>
                            <td style="width: 5px;">:</td>
                            <td>
                                <asp:TextBox runat="server" ID="tbPangaspuriNoUnit" CssClass="input" />
                                <asp:Label Text="" runat="server" id="lblNoUnit"/>
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td style="width: 150px;">Status Unit</td>
                        <td style="width: 5px;">:</td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlInsertStatusUnit" CssClass="input"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <table class="fbform">
                <tr>
                    <td>
                        <span id="MsgBottom" runat="server">
                            <asp:Label ID="strlbl_bottom" runat="server"></asp:Label>
                        </span>
                    </td>
                    <td>
                        <span class="buttonMenu">
                            <a href="#" runat="server" id="SaveBottom">
                                <img title="Simpan?" style="vertical-align: middle;" src="icons/save.png" width="25" height="25" alt="" />
                            </a>
                            <a href="#" runat="server" id="UpdateBottom" visible="false">
                                <img title="Simpan?" style="vertical-align: middle;" src="icons/save.png" width="25" height="25" alt="" />
                            </a>|
                            <a href="#" runat="server" id="CancelBottom">
                                <img title="Kembali?" style="vertical-align: middle;" src="icons/x-button.png" width="25" height="25" alt="" />
                            </a>
                        </span>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</div>