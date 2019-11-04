<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="konfigurasi_pangkat.ascx.vb" Inherits="QuartersManagement.konfigurasi_pangkat" %>

<script type="text/javascript">
   function check()
   {
      if( valid ) return true;
      return false;
   }
</script>

<div>
    
<div>
    <table class="fbform" style="width :100%">
    <tr>
        <td colspan="4">Penambahan Data Pangkat
        </td>
    </tr>
        <tr>
            <td>
                Tahap Pangkat
            </td>
            <td>:</td>
            <td colspan ="4">
                <asp:DropDownList runat ="server" ID ="ddl_tahapPangkat" >
                    <asp:ListItem Text ="Pegawai Kanan" Value ="Pegawai Kanan"></asp:ListItem>
                    <asp:ListItem Text ="Pegawai Muda" Value ="Pegawai Kanan"></asp:ListItem>
                    <asp:ListItem Text ="Lain - Lain Pangkat Rendah" Value="Lain - Lain Pangkat Rendah"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Nama Pangkat
            </td>
            <td>:</td>
            <td>
                <asp:TextBox runat ="server" ID ="txt_NamaPangkat" CausesValidation ="true" CssClass ="" ></asp:TextBox>
            </td>
        </tr>
        <tr style="align-items:center ">
            <td colspan="3" style="position:center ">
                <asp:Button runat ="server" OnClientClick="return confirm('Anda Pasti Dengan Penambahan Data Ini ?')" ID="btnAddNamaPangkat" Text ="Tambah Data" />
                <span id ="Span1" runat ="server" ><asp:Label ID ="strlbl_bottom" runat ="server" Text=""></asp:Label></span>
            </td>
        </tr>
    </table>
</div>

<%-- List --%>
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Pangkat
        </td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID="Panel" runat="server" ScrollBars="vertical" Height="200">
                <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                    CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="pangkat_id"
                    Width="100%" PageSize="100" CssClass="gridview_footer">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tahap">
                            <ItemTemplate>
                                <asp:Label ID="lbl_pangkatTahap" runat="server" Text='<%# Bind("pangkat_jenis")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="40%" />
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pangkat">
                            <ItemTemplate>
                                <asp:Label ID="lbl_pangkat" runat="server" Text='<%# Bind("pangkat_nama")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="40%" />
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Pangkat">
                            <ItemTemplate>
                                <asp:Label ID="lbl_pangkat" runat="server" Text='<%# Bind("pangkat_lencana")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="40%" />
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Pangkat">
                            <ItemTemplate>
                                <asp:Label ID="lbl_pangkat" runat="server" Text='<%# Bind("pangkat_idx")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
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


</div>
