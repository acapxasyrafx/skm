<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="permohonan_tolak.ascx.vb" Inherits="QuartersManagement.permohonan_tolak" %>

<style type="text/css">
    .auto-style1 {
        width: 107px;
    }
    .auto-style2 {
        width: 13px;
    }
</style>

<table class="fbform" style="width: 100%">
    <tr class="fbform_header">
        <td><span id="MsgTop" runat="server">
            <asp:Label ID="strlbl_top" runat="server"></asp:Label></span></td>
        <td>
            <span class="buttonMenu"><a href="#" runat="server" id="SaveFunction">
                <img title="Save" style="vertical-align: middle;" src="icons/save.png" width="25" height="25" alt="::" /></a>
                | <a href="#" id="Refresh" runat="server"><img title="Refresh" style="vertical-align: middle;" src="icons/refresh.png" width="22" height="22" alt="::" /></a>
                | <a href="#" id="Help"><img title="Help" style="vertical-align: middle;" src="icons/help.png" width="22" height="22" alt="::" /></a>

            </span>
        </td>
    </tr>
</table>

<table class="fbform" style="width: 100%">
    <tr class="fbform_mheader">
        <td class="auto-style1">
            Aturan Mengikut
        </td>
        <td class="auto-style2">:</td>
        <td>
            <asp:DropDownList runat ="server" ID="ddlSort" AutoPostBack ="true" ></asp:DropDownList>
        </td>
    </tr>
</table>

<table class="fbform" style="width: 100%">
    <tr class="fbform_mheader">
         <td>
            <asp:Panel ID="Panel" runat="server" ScrollBars="vertical" Height="350">
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="false"  
             CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="permohonan_id"
                Width="100%" PageSize="100" CssClass="gridview_footer" OnRowCommand ="datRespondent_RowCommand">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="Tarikh Permohonan" >
                        <ItemTemplate>
                            <asp:Label ID="lbl_tarikhPermohonan" runat="server" Text='<%# Bind("tarikhMohon")%>' ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="10%" /><ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                    </asp:TemplateField>   
                    <asp:TemplateField HeaderText="No.Tentera" >
                        <ItemTemplate>
                            <asp:Label ID="lbl_noTentera" runat="server" Text='<%# Bind("no_tentera")%>' ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="10%" /><ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                    </asp:templatefield>
                    <asp:TemplateField HeaderText="Pangkat" >
                        <ItemTemplate>
                            <asp:Label ID="lbl_pangkat" runat="server" Text='<%# Bind("pangkat")%>' ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="10%" /><ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                    </asp:TemplateField>    
                    <asp:TemplateField HeaderText="Nama" >
                        <ItemTemplate>
                            <asp:Label ID="lbl_nama" runat="server" Text='<%# Bind("nama")%>' ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="10%" /><ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                    </asp:TemplateField>   
                    <asp:TemplateField HeaderText="Pangkalan" >
                        <ItemTemplate>
                            <asp:Label ID="lbl_pangkalanTerknini" runat="server" Text='<%# Bind("pangkalan")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="10%" /><ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                    </asp:TemplateField>   
                    <asp:TemplateField HeaderText="Kuarters Dipohon" >
                        <ItemTemplate>
                            <asp:Label ID="lbl_kuartersDipohon" runat="server" Text='<%# Bind("unit")%>'> </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="10%" /><ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kuarters Dipohon" >
                        <ItemTemplate>
                            <asp:Label ID="lbl_kuartersDipohon" runat="server" Text='<%# Bind("total_poin")%>'> </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="10%" /><ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                    </asp:TemplateField>   
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <span runat="server" style="float: right">
                                 <asp:ImageButton Width="12" Height="12" ID="btnView" CommandName ="ViewApllicant" CommandArgument ='<%#Eval("permohonan_id")%>' OnClientClick="javascript:return confirm('Adakah anda pasti mahu memproses permohonan ini? ')" runat="server" ImageUrl="~/icons/form_process.png" ToolTip="View" />
                                |
                                <asp:ImageButton Width="12" Height="12" ID="btnProcess" CommandName ="Process" CommandArgument ='<%#Eval("permohonan_id")%>' OnClientClick="javascript:return confirm('Adakah anda pasti mahu memproses permohonan ini? ')" runat="server" ImageUrl="~/icons/form_process.png" ToolTip="Update" />
                                |
                                <asp:ImageButton Width="12" Height="12" ID="btnDelete" CommandName="Batal" OnClientClick="javascript:return confirm('Adakah anda pasti mahu memadamkan item ini secara kekal? ')" runat="server" ImageUrl="~/icons/delete.png" ToolTip="Delete" />
                            </span>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="right" VerticalAlign="Top" />
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

<table>
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