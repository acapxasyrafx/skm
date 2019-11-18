<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="proses_penempatan_kuarters.ascx.vb" Inherits="QuartersManagement.proses_penempatan_kuarters1" %>

<style type="text/css">
    .auto-style3 {
        width: 120px;
    }
    .auto-style4 {
        width: 17px;
    }
    .auto-style5 {
        width: 265px;
    }
</style>

<table class="fbform" style="width: 100%">
    <tr class="fbform_header">
        <td><span id="MsgTop" runat="server">
            <asp:Label ID="strlbl_top" runat="server"></asp:Label></span></td>
        <td>
            <span class="buttonMenu">
                <a href="#" id="Refresh" runat="server"><img title="Refresh" style="vertical-align: middle;" src="icons/refresh.png" width="22" height="22" alt="::" /></a>
                | <a href="#" id="Help"><img title="Help" style="vertical-align: middle;" src="icons/help.png" width="22" height="22" alt="::" /></a>

            </span>
        </td>
    </tr>
</table>

<table class="fbform" style="width: 100%">
    <tr class="fbform_mheader">
        <td colspan="4">
            <a>Saringan</a>
        </td>
    </tr>
    <tr>
        <td class="auto-style3">Pangkat</td>
        <td class="auto-style4">:</td>
        <td colspan="2">
            <asp:DropDownList runat ="server" id="ddlfilterPangkat" AutoPostBack ="true"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="auto-style3">Pangkalan</td>
        <td class="auto-style4">:</td>
        <td colspan="2">
            <asp:DropDownList runat ="server" id="ddlfilterPangkalan" AutoPostBack ="true"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="auto-style3">Kuarters Dipohon</td>
        <td class="auto-style4">:</td>
        <td colspan="2">
            <asp:DropDownList runat ="server" id="ddlfilterKuarters" AutoPostBack ="true"></asp:DropDownList>
        </td>
    </tr>
   
    <tr><td colspan ="4"></td></tr>
    <tr class="fbform_mheader">
        <td colspan="4"><a>Carian</a> </td>
    </tr>
    <tr>
        <td class="auto-style3">
            <asp:Label runat="server" ID="lbl_nama">Nama</asp:Label>
        </td>
        <td class="auto-style4">:
        </td>
        <td class="auto-style5" >
            <asp:TextBox runat="server" ID="txt_nama" CssClass="width:60px" Width="247px"></asp:TextBox>
        </td>
        <td>
            <asp:Button runat="server" ID="btnSearch" Text="Cari" Width="75px" />
        </td>
    </tr>
</table>

<br />


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
                        <ItemStyle VerticalAlign="Middle" width="3%"/>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="No.Permohonan" >
                        <ItemTemplate>
                            <asp:Label ID="lbl_noPermohonan" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="10%" /><ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                    </asp:templatefield>
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
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="6%" /><ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
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
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="40%" /><ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                    </asp:TemplateField>   
                    <asp:TemplateField HeaderText="Kuarters Dipohon" >
                        <ItemTemplate>
                            <asp:Label ID="lbl_kuartersDipohon" runat="server" Text='<%# Bind("unit")%>'> </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="10%" /><ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                    </asp:TemplateField>   
                    <asp:TemplateField HeaderText="Markah" >
                        <ItemTemplate>
                            <asp:Label ID="lbl_matapoin" runat="server" Text='<%# Bind("total_poin")%>'> </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="10%" /><ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Status" >
                        <ItemTemplate>
                            <asp:Label ID="lbl_status" runat="server" > </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="left" VerticalAlign="Top"  Width ="10%" /><ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Pemprosesan">
                        <ItemTemplate>
                            <span runat="server" style="float: right">
                                <asp:ImageButton Width="12" Height="12" ID="btnView" CommandName ="ViewApllicant" CommandArgument ='<%#Eval("permohonan_id")%>' runat="server" ImageUrl="~/icons/form_process.png" ToolTip="Pilih Unit Dan Buat Surat Tawaran" />
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