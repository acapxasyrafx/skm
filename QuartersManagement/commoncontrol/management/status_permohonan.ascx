<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="status_permohonan.ascx.vb" Inherits="QuartersManagement.status_permohonan1" %>
<style>
    ul.progress {
        margin: 0;
        padding:0;
        list-style-type:none;
    }

    ul.progress li {
        display:inline-block;
        text-align:center;
        line-height: 3.5em;
    }

    ul.progress[data-pregoresstracker-step="2"] li { width: 49%;}
    ul.progress[data-pregoresstracker-step="3"] li { width: 33%;}
    ul.progress[data-pregoresstracker-step="3"] li { width: 24%;}

    ul.progress li.progress-done {
        color:black;
        border-bottom: 4px solid #e9f83c;
    }

    ul.progress li.progress-todo {
        color:silver;
        border-bottom: 4px solid silver;
    }

    ul.progress li:after{
        content:'\00a0\00a0'
    }
    ul.progress li:before{
        position:relative;
        bottom:-2.5em;
        float:left;
        left:50%;
        line-height: 1em;
    }

    ul.progress li.progress-done:before {
        content: "\2713";
        color: grey;
        background-color: #e9f83c;
        height: 2.2em;
        width: 2.2em;
        line-height: 2.2em;
        border: none;
        border-radius: 2.2em;
    }

    ul.progress li.progress-todo:before {
        content: "\039F";
        color: silver;
        background-color: white;
        font-size: 2.2em;
        bottom: -1.2em;
    }

    .div-center{
        display:flex;
        align-items:center;
        justify-content: center;
        margin-bottom: 15px;
    }
    .div-center ul {
        margin:0;
    }
    .div-right{
        position:relative;
        left:300px;
        top:10px;
    }
    .label{
        display:inline-block;
        margin:0;
        padding:5px;
        font-size:100%;
    }
    .hoverWrapper #hoverShow{
        display:none;
        position:absolute;
        background-color:#ccc;
        height:250px;
    }
    .hoverWrapper:hover #hoverShow{
        display:block;
    }
</style>

<div class="status-permohonan" style="height:85vh;">
    <div class="fbform">
        <div class="div-center">
            <ul class="progress" data-progresstracker-step="3">
                <li runat="server" id="permohonanBaharu">Permohonan Baharu</li>
                <li runat="server" id="permohonanLayak">Kelayakan Permohonan</li>
                <li runat="server" id="permohonanMenunggu">Kelayakan Menunggu</li>
                <li runat="server" id="permohonanUnitDicadang">Permohonan Unit Dicadang</li>
                <li runat="server" id="suratTawaran">Surat Tawaran</li>
            </ul>
            <div class="div-right hoverWrapper">
                <button>Help</button>
                <div id="hoverShow">
                    Help
                </div>
            </div>
        </div>
        <div class="">
           
            <asp:MultiView ActiveViewIndex="0" runat="server" ID="mvStatusPermohonan">
                <asp:View runat="server" ID="viewPemohonanDir">
                    <table class="fbform" style="width:100%;">
                        <tr class="fbform_mheader">
                            <td colspan="3">Maklumat Keluarga</td>
                        </tr>
                        <tr>
                            <td style="width:100px;">Bilangan Anak</td>
                            <td style="width:5px;">:</td>
                            <td>
                                <asp:Label CssClass="label" runat="server" ID="lblBilAnak" Text="4"></asp:Label>
                            </td>
                        </tr>
                    <tr>
                        <td>Jenis Tempat Tinggal</td>
                        <td>:</td>
                        <td>
                            <asp:Label Text="text" runat="server" ID="lblJenisTempatTinggal"/>
                        </td>
                    </tr>
                    <tr>
                        <td>Tarikh Mula Menetap</td>
                        <td>:</td>
                        <td>
                            <asp:Label Text="text" runat="server" ID="lblTarikhMulaMenetap"/>
                        </td>
                    </tr>
                    </table>
                    <table class="fbform" style="width:100%;">
                        <tr class="fbform_mheader">
                            <td colspan="3">Maklumat Kuarters Dimohon</td>
                        </tr>
                        <tr>
                            <td style="width:100px;">Nama Kuarters</td>
                            <td style="width:5px;">:</td>
                            <td>
                                <asp:Label runat="server" ID="lblKuarterDipohon" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width:100px;">Tarikh Permohonan</td>
                            <td style="width:5px;">:</td>
                            <td>
                                <asp:Label runat="server" ID="lblTarikhPermohonan" />
                            </td>
                        </tr>
                    </table>
                    <div class="">
                        <table class="fbform">
                            <tr class="fbform_mheader">
                                <td>Maklumat Anak</td>
                            </tr>
                            <tr>
                                <asp:GridView 
                                    ID="tblMaklumatAnak" 
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
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" width="10%"/>
                                        <ItemStyle VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nama" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblNamaAnak" runat="server" Text='<%# Bind("anak_nama")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="40%" />
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="IC" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblICAnak" runat="server" Text='<%# Bind("anak_ic")%>'></asp:Label>
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
                        </tr>
                    </table>
                </div>
            </asp:View>
                <asp:View runat="server" ID="viewCadanganKuarters">
                    <table class="fbform" style="width: 100%">
                        <tr class="fbform_header">
                            <td><span id="MsgTop" runat="server">
                                <asp:Label ID="strlbl_top" runat="server"></asp:Label></span></td>
                            <td>
                                <span class="buttonMenu">
                                    <a href="#" runat="server" id="SaveFunction">
                                        <img title="Save" style="vertical-align: middle;" src="icons/save.png" width="25" height="25" alt="::" />
                                    </a>
                                    | <a href="#" id="Refresh" runat="server">
                                        <img title="Refresh" style="vertical-align: middle;" src="icons/refresh.png" width="22" height="22" alt="::" /></a>
                                    | <a href="#" id="Help">
                                        <img title="Help" style="vertical-align: middle;" src="icons/help.png" width="22" height="22" alt="::" /></a>
                                </span>
                            </td>
                        </tr>
                    </table>
                    <table class="fbform">
                        <tr class="fbform_mheader">
                            <td colspan="3">Pemilihan Kuarters</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <h4>Kuarters yang dipohon pada waktu ini tiada sebarang kekosongan. Sila pilih dengan mengelik senarai kuarters yang dicadangkan dibawah jika Tuan/Puan masih berniat untuk meneruskan permohonan Tuan/Puan: </h4>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <div>
                                    <asp:GridView
                                        ID="tblCadanganKuarters"
                                        runat="server"
                                        DataKeyNames="kuarters_dicadang"
                                        AutoGenerateColumns="False"
                                        AllowPaging="false"
                                        CellPadding="4"
                                        ForeColor="#333333"
                                        GridLines="None"
                                        Width="100%"
                                        PageSize="100"
                                        CssClass="gridview_footer"
                                        Autopostback="true">
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="2%" />
                                                <ItemStyle VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Nama Kuarters">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNamaAnak" runat="server" Text='<%# Bind("kuarters_nama")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Lokasi Kuarters">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNamaAnak" runat="server" Text='<%# Bind("kuarters_alamat")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="40%" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Nama Pangkalan">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNamaAnak" runat="server" Text='<%# Bind("pangkalan_nama")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Nama Pangkalan">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNamaAnak" runat="server" Text='<%# Bind("pangkalan_nama")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
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
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>
        </div>
        
        <table class ="fbform">
            <tr>
                <td><span id ="MsgBottom" runat ="server" ><asp:Label ID ="strlbl_bottom" runat ="server" ></asp:Label></span></td>
            </tr>
        </table>

    </div>
</div>