<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="senarai_penjawat.ascx.vb" Inherits="QuartersManagement.senarai_penjawat" %>

<style>
    .wrapper {
        display: flex;
        align-items: center;
    }
    .btn {
        border-radius: 5px;
        background-color: grey;
        color: white;
        border: solid 1px white;
        height: 2.5em;
        margin-left: 15em;
    }
    .btn:hover{
        border: solid 1px black;
        background-color:aliceblue;
        color:black;
    }
</style>

<table class ="fbform" style ="width :100%">
    <tr class ="fbform_header">
         <td><span id="MsgTop" runat ="server"><asp:Label ID ="strlbl_top" runat ="server" ></asp:Label></span></td>
         <td>
            <span class="buttonMenu">
             <a href ="#"  id ="Refresh" runat ="server"  ><img title="Refresh" style="vertical-align: middle;" src="icons/refresh.png" width="22" height="22" alt="::"/></a>
                | <a href ="#"  id ="Help" ><img title="Help" style="vertical-align: middle;" src="icons/help.png" width="22" height="22" alt="::"/></a>
            
            </span>
         </td>
    </tr>
</table>

<%-- SEARCH FIELD --%>
<div class="fbform">
<table>
    <tr class="fbform_mheader">
        <td>Carian Pegawai</td>
    </tr>
    <tr>
        <td style="width: 150px;">Nama Pegawai/No. Tentera</td>
        <td>:</td>
        <td>
            <asp:TextBox runat="server" Text="" ID="txtCarian" />
        </td>
    </tr>
    <tr>
        <td style="width: 150px;">Pangkat</td>
        <td>:</td>
        <td>
            <asp:DropDownList runat="server" ID="ddlCarianPangkat" AutoPostBack="true">
            </asp:DropDownList>
        </td>
    </tr>
</table>
<div class="wrapper">
    <asp:Button Text="Cari" CssClass="btn" runat="server" ID="btnCarian"/>
</div>
</div>

<%-- /SEARCH FIELD --%>

<%-- TABLE FIELD --%>
<table class="fbform">
    <tr class="fbform_header">
        <td> 
            <asp:Label ID="lblConfig" runat="server" Visible ="false"></asp:Label>
            <asp:Label ID="lblQ" runat="server" Visible ="false"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID="Panel" runat="server" ScrollBars="vertical" Height="350">
                <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                    CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="pengguna_id"
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

                        <asp:TemplateField HeaderText="No. Tentera">
                            <ItemTemplate>
                                <asp:Label ID="lblnoTentera" runat="server" Text='<%# Bind("no_tentera")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Pangkat">
                            <ItemTemplate>
                                <asp:Label ID="lblpangkat" runat="server" Text='<%# Bind("pangkat")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Nama">
                            <ItemTemplate>
                                <asp:Label ID="lblnama" runat="server" Text='<%# Bind("nama")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="40%" />
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="TINDAKAN">
                            <ItemTemplate>
                                <span runat="server" style="float: right">
                                    <a href="Pengguna.Pendaftaran.Edit.aspx?edit=<%#Eval("pengguna_id")%>&p=<%# lblConfig.Text  %>&z=<%# "Edit"  %>">
                                        <img title="Kemaskini" src="icons/edit.png" width="13" height="13" alt="::" />
                                    </a>
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
    <tr>
        <td colspan="3"></td>
    </tr>
</table>
<table class ="fbform">
    <tr>
        <td><span id ="MsgBottom" runat ="server" ><asp:Label ID ="strlbl_bottom" runat ="server" ></asp:Label></span></td>
    </tr>

</table>