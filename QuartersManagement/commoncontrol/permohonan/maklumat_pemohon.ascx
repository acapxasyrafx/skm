<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="maklumat_pemohon.ascx.vb" Inherits="QuartersManagement.maklumat_pemohon" %>

<style>
    .h5{
         font-weight:normal; 
    }
    .label{
        display: inline-block;
        margin:0;
        padding:5px;
        font-size: 100%;        
        font-weight:normal;
    }
    .left {
        display:flex;
        flex: initial;
    }
    .auto-style2 {
        width: 14px;
    }
    .auto-style3 {
        width: 320px;
    }
    .auto-style4 {
        width: 635px;
    }
    .auto-style5 {
        width: 133px;
    }
</style>

 <script>
        function GetUserValue() {
            var person = prompt("Please enter your name", "wmec");
            if (person != null && person != "") {
                document.getElementById("<%=hdnUserInput.ClientID%>").value = person;
                return true;
            }
            else
                return false;
        }
    </script>

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


<table class="fbform" style="width:100%;">
    <tr class="fbform_mheader">
        <td colspan="3">Butiran Peribadi</td>
    </tr>
    
    <tr>
        <td>Pangkat</td>
        <td>:</td>
        <td>
            <h5 class="label" style="font-weight:normal ;" runat="server" id="lblJawatan" ></h5>
        </td>
    </tr>
    <tr>
        <td>No. Tentera</td>
        <td>:</td>
        <td>
            <h5 class="label" style="font-weight:normal ;" runat="server" id="lblNoTentera"></h5>
        </td>
    </tr>
    <tr>
        <td style="width:100px;">Nama</td>
        <td style="width:5px;">:</td>
        <td>
            <h5 class="label" runat="server" id="lblNama"></h5>
            <asp:HiddenField runat="server" ID="pengguna_id" Value="" />
        </td>
    </tr>
    <tr>
        <td>Jantina</td>
        <td>:</td>
        <td>
            <h5 class="label" style="font-weight:normal ;" runat="server" id="lblJantina"></h5>
        </td>
    </tr>
    <tr>
        <td>Tarikh Lahir</td>
        <td>:</td>
        <td>
            <h5 class="label" style="font-weight:normal ;" runat="server" id="lblTarikhLahir"></h5>
        </td>
    </tr>
    <tr>
        <td>Tarikh Mula Berkhidmat</td>
        <td>:</td>
        <td>
            <h5 class="label" style="font-weight:normal ;" runat="server" id="lblTarikhMulaBerkhidmat">01/01/2010</h5>
        </td>
    </tr>
    <tr>
        <td>Tarikh Tamat Perkhidmatan</td>
        <td>:</td>
        <td>
            <h5 class="label" style="font-weight:normal ;" runat="server" id="lblTarikhAkhirBerkhidmat"></h5>
        </td>
    </tr>
</table>

<table class="fbform">
    <tr class="fbform_mheader">
        <td colspan="3">Butiran Keluarga</td>
    </tr>
    <tr>
        <td style="width:100px;">Maklumat Anak</td>
        <td style="width:5px;">:</td>
        <td>            
            <div>
                <table runat="server" id="tblMaklumatAnak">
                   
                    <tr>
                        <td colspan="3">
                            <asp:GridView 
                                ID="datRespondent" 
                                runat="server" 
                                DataKeyNames="anak_id"
                                AutoGenerateColumns="False" 
                                AllowPaging="false"
                                CellPadding="4" 
                                ForeColor="#333333" 
                                GridLines="None" 
                                Width="100%" 
                                PageSize="100" 
                                CssClass="gridview_footer">
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemStyle VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nama" >
                                       <ItemTemplate>
                                           <asp:Label ID="lblNamaAnak" runat="server" Text='<%# Bind("anak_nama")%>'></asp:Label>
                                       </ItemTemplate>
                                       <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="20%" />
                                       <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="IC" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblICAnak" runat="server" Text='<%# Bind("anak_ic")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="20%" />
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                        
                                    <asp:TemplateField HeaderText="UMUR" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblUmurAnak" runat="server" Text='<%# Bind("anak_umur")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="20%" />
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
                        </td>
                    </tr>
                </table>
            </div>
            
        </td>
    </tr>
    <tr>
        <td>Jenis Tempat Tinggal Akhir</td>
        <td>:</td>
        <td>
            <asp:Label runat="server" CssClass="label" ID="lblJenisPenempatan"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Mula Menetap Dari</td>
        <td>:</td>
        <td>
            <asp:Label runat ="server" ID ="lbltarikhPenempatan" cssclass="label"> </asp:Label>
        </td>
    </tr>
</table>

<table class="fbform">
    <tr class="fbform_mheader">
        <td colspan="4">Butiran Permohonan</td>
    </tr>
    <tr>
        <td style="width:100px;">Pangkalan</td>
        <td>:</td>
        <td colspan="2">
            <h5 class="label" runat="server" id="lbl_senaraiPangkalan"></h5>
    </tr>
    <tr>
        <td style="width:100px;">Kuarters/Rumah</td>
        <td>:</td>
        <td colspan="2">
            <h5 class="label" runat="server" id="lbl_senaraiKuarters"></h5>
        </td>
    </tr>
    <tr>
        <td colspan="4">
           
            <table runat="server" id="tblBertukar" visible="false">
                <tr>
                    <td>Dari Pasukan</td>
                    <td>:</td>
                    <td>
                        <h5 class="label" style="font-weight:normal ;" runat="server" id="lbl_pasukanLama"></h5>
                    </td>
                </tr>
                <tr>
                    <td>Ke Pasukan</td>
                    <td>:</td>
                    <td>
                        <h5 class="label" runat="server" id="lbl_pasukanBaru"></h5>
                        <asp:Label CssClass="label" runat="server" ID="lblPasukanBaru"></asp:Label></td>
                </tr>
                <tr>
                    <td>Tarikh Bertukar</td>
                    <td>:</td>
                    <td>                        
                        <h5 class="label" runat="server" id="H1"></h5>
                        <asp:Label runat ="server" CssClass ="label" ID="lbltarikhBertukar"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    
</table>
<table class="fbform" style="width:100%" cellpadding="0" cellspacing="0" border="0">
    <tr class="fbform_mheader">
        <td class="auto-style5">
            Total Poin Terkumpul
        </td>
        <td class="auto-style2">:</td>
        <td class="auto-style3">
            <h5 class="label" runat="server" id="lbl_poinDisplay" text=""></h5>
        </td>
    </tr>
    <tr>
        <td>Permohonan </td>
        <td>:</td>
        <td class="auto-style4" style ="text-align :left">
            <asp:ImageButton runat="server" ID="btnImg_lulus" CommandName ="Approved" CommandArgument ='<%#Eval("permohonan_id")%>' OnClientClick="javascript:return confirm('Adakah anda pasti mahu meluluskan permohonan ini? ')" ImageUrl="~/icons/checkmark_approve.png" ToolTip="Diterima" Height="39px"/>
            &nbsp&nbsp&nbsp&nbsp&nbsp
            <asp:ImageButton runat="server" ID="btnImg_ditolak" Height="39px" CommandName ="Rejected" CommandArgument ='<%#Eval("permohonan_id")%>' OnClientClick="javascript:return GetUserValue() && confirm('Adakah anda pasti mahu menolak permohonan ini? ')" ImageUrl="~/icons/checkmark_declined.png" ToolTip="Ditolak" />
            <asp:HiddenField runat="server" ID="hdnUserInput" />
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