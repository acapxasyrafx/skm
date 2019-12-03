<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="permohonan_kuarters.ascx.vb" Inherits="QuartersManagement.permohonan_kuarters" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<style>
    .label{
        display: inline-block;
        margin:0;
        padding:0;
        padding:5px;
        font-size: 100%;
    }
    .txtbox{
        margin-right: 5px;
        padding:0;
        width: 250px;
        height: 25px;
    }
    .btn{
        border-radius: 5px;
        background-color: grey;
        color:white;
        border: solid 1px white;
        height: 2.5em;
    }
    .btn:hover{
        border: solid 1px black;
        background-color:aliceblue;
        color:black;
    }
</style>
<table class="fbform" style="width: 100%">
    <tr class="fbform_header">
        <td><span id="MsgTop" runat="server">
            <asp:Label ID="strlbl_top" runat="server"></asp:Label></span></td>
        <td>
            <span class="buttonMenu">
                <a href="#" runat="server" id="SaveFunction">
                    <img title="Save" style="vertical-align: middle;" src="icons/save.png" width="25" height="25" alt="::" />
                </a>
                | <a href="#" id="Refresh" runat="server"><img title="Refresh" style="vertical-align: middle;" src="icons/refresh.png" width="22" height="22" alt="::" /></a>
                | <a href="#" id="Help"><img title="Help" style="vertical-align: middle;" src="icons/help.png" width="22" height="22" alt="::" /></a>
            </span>
        </td>
    </tr>
</table>


<table class="fbform" style="width: 100%;">
    <tr class="fbform_mheader">
        <td colspan="3">Butiran Peribadi</td>
    </tr>

    <tr>
        <td style="width: 150px;">No. Tentera</td>
        <td>:</td>
        <td>
            <asp:Label runat="server" ID="lblNoTentera" />
        </td>
    </tr>

    <tr>
        <td style="width: 150px;">Pangkat</td>
        <td>:</td>
        <td>
            <asp:Label runat="server" ID="lblPangkat" />
            <asp:HiddenField ID="pangkatID" runat="server" />
        </td>
    </tr>

    <tr>
        <td style="width: 150px;">Nama</td>
        <td style="width: 5px;">:</td>
        <td>
            <asp:Label runat="server" ID="lblNama"/>
            <asp:HiddenField runat="server" ID="penggunaID"/>
        </td>
    </tr>

    <tr>
        <td style="width: 150px;">Jantina</td>
        <td>:</td>
        <td>
            <asp:Label runat="server" ID="lblJantina"/>
        </td>
    </tr>

    <tr>
        <td style="width: 150px;">Tarikh Lahir</td>
        <td>:</td>
        <td>
            <asp:Label runat="server" ID="lblTarikhLahir"/>
        </td>
    </tr>
    
    <tr>
        <td style="width: 150px;">Status Perkahwinan</td>
        <td>:</td>
        <td>
            <asp:Label runat="server" ID="lblStatusPerkahwinan"/>
        </td>
    </tr>

    <tr>
        <td style="width: 150px;">Tarikh Mula Berkhidmat</td>
        <td>:</td>
        <td>
            <asp:Label runat="server" ID="lblTarikhMulaBerkhidmat" />
        </td>
    </tr>
    <tr>
        <td>Tarikh Tamat Perkhidmatan</td>
        <td>:</td>
        <td>
            <asp:Label runat="server" ID="lblTarikhAkhirBerkhidmat" />
        </td>
    </tr>
</table>

<table class="fbform">
    <tr class="fbform_mheader">
        <td colspan="3">Masukkan Maklumat Anak </td>
    </tr>
    <tr>
        <td>
            <div>
                <table runat="server" id="tblMaklumatAnak" style="width:100%;">
                    <tr>
                        <td colspan="3">
                            <div class="label">
                                Nama Anak: <asp:TextBox runat="server" ID="txtNamaAnak" CssClass="txtbox" />
                                KP Anak: <asp:TextBox runat="server" ID="txtICAnak" CssClass="txtbox" />
                                <asp:Button Text="Tambah" runat="server" ID="btnTambahRow" CssClass="btn" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                    </tr>
                    <tr >
                        <td colspan="3" style="width:100%;">
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
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" WIdth="5%"/>
                                        <ItemStyle VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nama" >
                                       <ItemTemplate>
                                           <asp:Label ID="lblNamaAnak" runat="server" Text='<%# Bind("anak_nama")%>'></asp:Label>
                                       </ItemTemplate>
                                       <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="30%" />
                                       <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="KP" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblICAnak" runat="server" Text='<%# Bind("anak_ic")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="30%" />
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                        
                                    <asp:TemplateField HeaderText="UMUR" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblUmurAnak" runat="server" Text='<%# icToAge(Eval("anak_ic")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="20%" />
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Padam">
                                        <ItemTemplate>
                                            <span runat="server" style="float:right">
                                                <asp:ImageButton 
                                                    Width ="12" 
                                                    Height ="12" 
                                                    ID="btnDelete" 
                                                    CommandName ="Delete" 
                                                    CommandArgument ='<%#Eval("anak_id")%>' 
                                                    OnClientClick="javascript:return confirm('Adakah anda pasti mahu memadamkan item ini secara kekal? ')" 
                                                    runat="server" 
                                                    ImageUrl="~/icons/delete.png" 
                                                    ToolTip="Padam?"
                                                />
                                            </span> 
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="right" VerticalAlign="Top" width="5%" />
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
                        </td>
                    </tr>
                </table>
            </div>
<%--            <asp:CheckBox Text="Tiada Anak" runat="server" ID="cbTiadaAnak" AutoPostBack="true"/>--%>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <table>
                <tr>
                    <td>Jenis Tempat Tinggal Akhir</td>
                    <td>:</td>
                    <td>
                        <asp:DropDownList runat="server" CssClass="label" ID="ddlJenisPenempatan">
                            <asp:ListItem Value="">-- SILA PILIH --</asp:ListItem>
                            <asp:ListItem Value="Rumah Sewa">Rumah Sewa</asp:ListItem>
                            <asp:ListItem Value="Wisma">Wisma (Keluarga Di Kampung)</asp:ListItem>
                            <asp:ListItem Value="Seberang">Bertugas Di Seberang (Keluarga Berada Di Rumah Sewa Di Malaysia Barat)</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Mula Menetap Dari</td>
                    <td>:</td>
                    <td>
                        <asp:DropDownList CssClass="label" ID="ddlTarikhTinggalHariMula" runat="server"></asp:DropDownList>
                        / 
                        <asp:DropDownList CssClass="label" ID="ddlTarikhTinggalBulanMula" runat="server"></asp:DropDownList>
                        / 
                        <asp:DropDownList CssClass="label" ID="ddlTarikhTinggalTahunMula" runat="server"></asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

<table class="fbform">
    <tr class="fbform_mheader">
        <td colspan="4">Butiran Permohonan</td>
    </tr>
    <tr>
        <td style="width:150px;">Pangkalan</td>
        <td>:</td>
        <td colspan="2">
            <asp:DropDownList runat="server" CssClass="label"  ID="ddlSenaraiPangkalan" AutoPostBack="true"></asp:DropDownList></td>
    </tr>
    <tr>
        <td style="width:150px;">Kuarters/Rumah</td>
        <td>:</td>
        <td colspan="2">
            <asp:DropDownList runat="server" CssClass="label"  ID="ddlSenaraiKuarters" Enabled="false"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:CheckBox Text="Tanda ini untuk yang bertukar pangkalan" runat="server" ID="cbBertukarPangkalan" AutoPostBack="true"/>
            <table runat="server" id="tblBertukar" visible="false">
                <tr>
                    <td style="width: 100px;">Dari Pasukan</td>
                    <td>:</td>
                    <td>
                        <asp:DropDownList CssClass="label" runat="server" ID="ddlPasukanLama">
                            <asp:ListItem Text="Sila Pilih Pasukan Terkini Anda..."/>
                        <asp:ListItem Text="Pasukan A" Value="Pasukan A"/>
                        <asp:ListItem Text="Pasukan B" Value="Pasukan B"/>
                        <asp:ListItem Text="Pasukan C" Value="Pasukan C"/>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Ke Pasukan</td>
                    <td>:</td>
                    <td><asp:DropDownList CssClass="label" runat="server" ID="ddlPasukanBaru">
                        <asp:ListItem Text="Sila Pilih Pasukan Anda Akan Berpindah..."/>
                        <asp:ListItem Text="Pasukan A" Value="Pasukan A"/>
                        <asp:ListItem Text="Pasukan B" Value="Pasukan B"/>
                        <asp:ListItem Text="Pasukan C" Value="Pasukan C"/>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Tarikh Bertukar</td>
                    <td>:</td>
                    <td>
                        <asp:DropDownList CssClass="label"  ID="ddlTarikhTukarHari" runat="server"></asp:DropDownList> / 
                        <asp:DropDownList CssClass="label"  ID="ddlTarikhTukarBulan" runat="server"></asp:DropDownList> / 
                        <asp:DropDownList CssClass="label"  ID="ddlTarikhTukarTahun" runat="server"></asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <p><b>Dengan mengklik butang Hantar, anda telah bersetuju bahawa: </b></p>
            <ol>
                <li>Akan patuh kepada <a href="#" target="_blank"><u>Peraturan dan Undang-Undang PAT Jil III</u></a></li>
                <li>Maklumat yang diberi adalah <b>BENAR</b></li>
            </ol>
        </td>
    </tr>
</table>

<table class="fbform" style="width: 100%">
    <tr class="fbform_header">
        <td><span id="Span1" runat="server">
            <asp:Label ID="Label1" runat="server"></asp:Label></span></td>
        <td>
            <span class="buttonMenu">
                <a href="#" runat="server" id="saveBottom">
                    <img title="Save" style="vertical-align: middle;" src="icons/save.png" width="25" height="25" alt="::" />
                </a>
                | <a href="#" id="refreshBottom" runat="server">
                    <img title="Refresh" style="vertical-align: middle;" src="icons/refresh.png" width="22" height="22" alt="::" /></a>
                | <a href="#" id="helpBottom">
                    <img title="Help" style="vertical-align: middle;" src="icons/help.png" width="22" height="22" alt="::" /></a>
            </span>
        </td>
    </tr>
</table>

<table class ="fbform">
    <tr>
        <td><span id ="MsgBottom" runat ="server" ><asp:Label ID ="strlbl_bottom" runat ="server" ></asp:Label></span></td>
    </tr>
</table>
<script type="text/javascript">
    function showAlert() {
        alert('Testing alert')
    }
</script>