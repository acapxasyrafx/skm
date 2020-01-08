<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="konfigurasi_unit.ascx.vb" Inherits="QuartersManagement.konfigurasi_unit" %>
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
                <tr class="fbform_mheader">
                    <td colspan="4">Carian Unit</td>
                </tr>
                <tr>
                    <td style="width: 150px;">Nama Unit</td>
                    <td style="width:5px;">:</td>
                    <td><asp:TextBox runat="server" ID="tbCari"/></td>
                </tr>
                <tr>
                    <td style="width: 150px;"></td>
                    <td><asp:Button Text="Cari" runat="server" ID="btnCari"/></td>
                </tr>
            </table>
            <table class="fbform" style="width: 100%;">
                <tr class="fbform_mheader">
                    <td colspan="3">
                        Senarai Unit
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel" runat="server" ScrollBars="Vertical" Height="90%">
                            <asp:GridView 
                                ID="datRespondent" 
                                runat="server" 
                                AutoGenerateColumns="False" 
                                AllowPaging="false"  
                                CellPadding="4" 
                                ForeColor="#333333"
                                GridLines="None"
                                DataKeyNames="unit_id"
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

                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="unit_nama" runat="server" Text='<%# IIf(Eval("unit_nama").ToString.Equals(""), Eval("unit_nama_lain").ToString, Eval("unit_nama").ToString) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Kuarters">
                                        <ItemTemplate>
                                            <asp:Label ID="kuarters_nama" runat="server" Text='<%# Bind("kuarters_nama")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30%" />
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Pangkalan">
                                        <ItemTemplate>
                                            <asp:Label ID="pangkalan_nama" runat="server" Text='<%# Bind("pangkalan_nama")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30%" />
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
                    <td colspan="3">Ubah Maklumat Unit</td>
                </tr>
                <tr>
                    <td style="width: 150px;">Pangkalan</td>
                    <td style="width: 5px;">:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlInsertPangkalan" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px;">Kuarters</td>
                    <td style="width: 5px;">:</td>
                    <td>
                          <asp:DropDownList runat="server" ID="ddlInsertKuarters"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px;">Nama Unit</td>
                    <td style="width: 5px;">:</td>
                    <td>
                        <asp:TextBox runat="server" ID="tbNamaUnit"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px;">Blok/Baris</td>
                    <td style="width: 5px;">:</td>
                    <td>
                        <asp:TextBox runat="server" ID="tbBlok"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px;">No. Tingkat</td>
                    <td style="width: 5px;">:</td>
                    <td>
                        <asp:TextBox runat="server" ID="tbTingkat"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px;">No. Unit</td>
                    <td style="width: 5px;">:</td>
                    <td>
                        <asp:TextBox runat="server" ID="tbNoUnit"/>
                    </td>
                </tr>
            </table>

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