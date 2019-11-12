<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="senarai_permohonan.ascx.vb" Inherits="QuartersManagement.senarai_permohonan" %>
<div>
    <table class="fbform" style="width:100%;">
        <tr class="fbform_mheader">
            <td>Senarai Permohonan</td>
        </tr>
        <tr>
            <td>
                <asp:GridView 
                    ID="tblSenaraiPermohonan" 
                    runat="server" 
                    DataKeyNames="permohonan_id"
                    AutoGenerateColumns="False" 
                    AllowPaging="false"
                    CellPadding="4" 
                    ForeColor="#333333" 
                    GridLines="None" 
                    Width="100%" 
                    PageSize="100" 
                    CssClass="gridview_footer"
                    >
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" width="2%"/>
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                                
                        <asp:TemplateField HeaderText="Tarikh Permohonan" >
                            <ItemTemplate>
                                <asp:Label ID="lblTarikhPermohonan" runat="server" Text='<%#Eval("pemohonan_tarikh", "{0:dd-MM-yyyy}")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="10%" />
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Kuarters Dipohon" >
                            <ItemTemplate>
                                <asp:Label ID="lblKuarters" runat="server" Text='<%# Bind("kuarters_nama")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="20%" />
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status" >
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("permohonan_status")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="20%" />
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Nota" >
                            <ItemTemplate>
                                <asp:Label ID="lblNota" runat="server" Text='<%# Bind("permohonan_nota")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="30%" />
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tindakan" >
                            <ItemTemplate>
                                <asp:Button Text="DETAIL" runat="server" ID="btnView" CommandName="View_Permohonan" CommandArgument='<%#Eval("permohonan_id") %>'/>
                                <asp:Button Text="PADAM" runat="server" ID="btnDelete" CommandName="Delete_Permohonan" CommandArgument='<%#Eval("permohonan_id") %>'/>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="40%" />
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