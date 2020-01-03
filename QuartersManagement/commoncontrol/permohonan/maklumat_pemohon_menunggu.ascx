<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="maklumat_pemohon_menunggu.ascx.vb" Inherits="QuartersManagement.maklumat_pemohon_menunggu" %>
<%@ Register Assembly="TextboxioControl" Namespace="TextboxioControl" TagPrefix="textboxio" %>

<header>
    <meta charset="utf-8"/>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script>
        $(function () {
            $(".datepicker").datepicker({ dateFormat: 'dd/mm/yy' }).val();
        });
    </script>
    <style>
        .label {
            display: inline-block;
            margin: 0;
            padding: 5px;
            font-size: 100%;
            font-weight: normal;
        }

        .left {
            display: flex;
            flex: initial;
        }

        .auto-style1 {
            height: 30px;
        }

        .auto-style2 {
            width: 18px;
        }

        .wrapper {
            display: flex;
            justify-content: space-evenly;
            height: 100%;
            margin: 0;
            padding: 0;
        }

        .left {
            display: block;
            width: 50%;
            padding: 1px;
        }

        .right {
            display: block;
            width: 50%;
            padding: 1px;
        }

        .btn {
            border-radius: 5px;
            background-color: grey;
            color: white;
            border: solid 1px white;
            height: 2.5em;
        }

        .btn:hover {
            border: solid 1px black;
            background-color: aliceblue;
            color: black;
        }

        select, .select {
            width: 20em;
        }

        #editorSurattawaran {
            position: relative;
            z-index: -1;
        }

        .datepicker {
            z-index: 1;
        }
    </style>
</header>

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

<div class="wrapper fbform">
    <div class="left">
        <%-- Butiran Peribadi --%>
        <table class="fbform" style="width: 100%;">
            <tr class="fbform_mheader">
                <td colspan="3">Butiran Peribadi</td>
            </tr>
            <tr>
                <td style="width: 150px;">No. Tentera</td>
                <td style="width: 5px;">:</td>
                <td>
                    <h5 class="label" runat="server" id="lblNoTentera"></h5>
                </td>
            </tr>
            <tr>
                <td style="width: 150px;">Pangkat</td>
                <td style="width: 5px;">:</td>
                <td>
                    <h5 class="label" runat="server" id="lblJawatan"></h5>
                </td>
            </tr>
            <tr>
                <td style="width: 100px;">Nama</td>
                <td style="width: 5px;">:</td>
                <td>
                    <h5 class="label" runat="server" id="lblNama"></h5>
                    <asp:HiddenField runat="server" ID="pID" Value="" />
                </td>
            </tr>
            <tr>
                <td>Jantina</td>
                <td>:</td>
                <td>
                    <h5 class="label" runat="server" id="lblJantina"></h5>
                </td>
            </tr>
            <tr>
                <td>Status Perkahwinan</td>
                <td>:</td>
                <td>
                    <h5 class="label" runat="server" id="lblStatusPerkahwinan"></h5>
                </td>
            </tr>
            <tr>
                <td>Tarikh Lahir</td>
                <td>:</td>
                <td>
                    <h5 class="label" runat="server" id="lblTarikhLahir"></h5>
                </td>
            </tr>
            <tr>
                <td>Tarikh Mula Berkhidmat</td>
                <td>:</td>
                <td>
                    <h5 class="label" runat="server" id="lblTarikhMulaBerkhidmat" />
                </td>
            </tr>
            <tr>
                <td>Tarikh Tamat Perkhidmatan</td>
                <td>:</td>
                <td>
                    <h5 class="label" runat="server" id="lblTarikhAkhirBerkhidmat" />
                </td>
            </tr>
        </table>
        <%--  --%>

        <%-- Maklumat Anak --%>
        <table class="fbform" style="width: 100%;">
            <tr class="fbform_mheader">
                <td>Maklumat Anak</td>
            </tr>
            <tr>
                <td style="width: 100%;">
                    <asp:GridView
                        ID="datRespondent"
                        runat="server"
                        DataKeyNames="historyAnak_id"
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
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%" />
                                <ItemStyle VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Nama">
                                <ItemTemplate>
                                    <asp:Label ID="lblNamaAnak" runat="server" Text='<%# Bind("historyAnak_nama")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="IC">
                                <ItemTemplate>
                                    <asp:Label ID="lblICAnak" runat="server" Text='<%# Bind("historyAnak_ic")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="UMUR">
                                <ItemTemplate>
                                    <asp:Label ID="lblUmurAnak" runat="server" Text='<%# icToAge(Eval("historyAnak_ic")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
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
        <%--  --%>

        <%-- Butiran Keluarga --%>
        <table class="fbform">
            <tr class="fbform_mheader">
                <td colspan="3">Maklumat Keluarga</td>
            </tr>
            <tr>
                <td style="width: 150px;">Jenis Tempat Tinggal Akhir</td>
                <td style="width: 5px;">:</td>
                <td>
                    <h5 class="label" runat="server" id="lblJenisPenempatan"></h5>
                </td>
            </tr>
            <tr>
                <td style="width: 150px;">Mula Menetap Dari</td>
                <td style="width: 5px;">:</td>
                <td>
                    <h5 class="label" runat="server" id="lbltarikhPenempatan"></h5>
                </td>
            </tr>
        </table>
        <%--  --%>

        <%-- Butiran Permohonan --%>
        <table class="fbform" style="width: 100%;">
            <tr class="fbform_mheader">
                <td colspan="4">Butiran Permohonan</td>
                <asp:HiddenField runat="server" ID="hfNoPermohonan" />
            </tr>
            <tr>
                <td style="width: 100px;">Pangkalan</td>
                <td class="auto-style2">:</td>
                <td colspan="2">
                    <h5 class="label" runat="server" id="lbl_senaraiPangkalan"></h5>
                    <asp:HiddenField runat="server" ID="hfPangkalanID"/>
                </td>
            </tr>
            <tr>
                <td style="width: 100px;">Kuarters/Rumah</td>
                <td class="auto-style2">:</td>
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
                                <h5 class="label" runat="server" id="lbl_pasukanLama"></h5>
                            </td>
                        </tr>
                        <tr>
                            <td>Ke Pasukan</td>
                            <td>:</td>
                            <td>
                                <h5 class="label" runat="server" id="lbl_pasukanBaru"></h5>

                            </td>
                        </tr>
                        <tr>
                            <td>Tarikh Bertukar</td>
                            <td>:</td>
                            <td>
                                <h5 class="label" runat="server" id="lbl_tarikhBertukar"></h5>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button runat="server" CssClass="btn" ID="TerimaPermohonanKuarters" Text="Terima Permohonan Kuarters" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <%--  --%>
    </div>
    <div class="right">
        <%-- Status Kuarters Dipohon --%>
        <table class="fbform">
            <tr class="fbform_mheader">
                <td colspan="3">Status Kuarters Dipohon</td>
            </tr>
            <tr>
                <td style="width: 150px;">Kuarters</td>
                <td style="width: 5px;">:</td>
                <td><asp:Label Text="text" runat="server" ID="lblKuartersDipohon"/></td>
            </tr>
            <tr runat="server" visible="true" id="trStatusKuarters">
                <td style="width: 150px;">Status Kuarters</td>
                <td style="width: 5px;">:</td>
                <td><asp:Label Text="text" runat="server" ID="lblStatusKuarter" /></td>
            </tr>
            <tr runat="server" id="trUnitDitawarkan" visible="false">
                <td style="width: 150px;">Unit Yang Ditawarkan</td>
                <td style="width: 5px;">:</td>
                <td><b><asp:Label Text="text" runat="server" ID="lblUnitDitawarkan"/></b></td>
            </tr>
             <tr runat="server" id="trTarikhKemasukan" visible="false">
                <td style="width: 150px;">Tarikh Kemasukan</td>
                <td style="width: 5px;">:</td>
                <td><b><asp:Label Text="text" runat="server" ID="lblTarikhKemasukan" CssClass="select" /></b></td>
            </tr>
            <tr runat="server" id="trSuratTawaran" visible="false">
                <td colspan="3">
                    <table class="fbform">
                        <tr class="fbform_mheader">
                            <td colspan="3">
                                <textboxio:Textboxio
                                    runat="server"
                                    ID="editorViewSuratTawaran"
                                    ScriptSrc="textboxio/textboxio.js"
                                    Enabled="false" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr runat="server" visible="false">
                <td>
                    <asp:CheckBox Text="Cadang Kuarters Lain?" runat="server" ID="cbCadangKuartersLain" AutoPostBack="true" />
                </td>
            </tr>
        </table>
        <%--  --%>

        <%-- Actions --%>
        <asp:Panel runat="server" ID="pnlPemilihanUnit" Visible="false">
            <table class="fbform">
                <tr class="fbform_mheader">
                    <td colspan="3">Pemilihan Unit</td>
                </tr>
                <tr>
                    <td style="width: 150px;">Unit Kuarters</td>
                    <td style="width: 5px;">:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlUnitKuarters"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px;">Tarikh Kemasukan</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox CssClass="datepicker" ID="datepicker" runat="server" />
                        &#160; <i class="fa fa-calendar w3-medium w3-text-black"></i>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px;">Jenis Surat Tawaran</td>
                    <td>:</td>
                    <td>
                        <asp:DropDownList runat="server" id="ddlJenisSuratTawaran"/>
                    </td>
                </tr>
                <tr class="fbform_mheader">
                    <td colspan="3">Kandungan Surat Tawaran</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <textboxio:Textboxio
                            runat="server"
                            ID="editorSurattawaran"
                            ScriptSrc="textboxio/textboxio.js"
                            Content="<p></p>" 
                        />
                    </td>
                </tr>
                <tr>
                    <td><asp:Button CssClass="btn" Text="Simpan" runat="server" ID="btnSimpanTawaranUnit" /></td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlCadanganKuarters" Visible="false">
            <table class="fbform" style="width: 100%;">
                <tr class="fbform_mheader">
                    <td colspan="3">Cadangan Kuarters</td>
                </tr>
                <tr>
                    <td style="width: 150px;">Cadangan Kuarters Lain</td>
                    <td style="width: 5px;">:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlCadanganKuarters" Width="50%"></asp:DropDownList>
                        <asp:Button CssClass="btn" Text="Tambah" runat="server" ID="btnTambahCadangan"/>
                    </td>
                </tr>
                <tr>
                    <td runat="server" id="divTableCadangan" colspan="3">
                        <asp:GridView
                            ID="gvCadanganKuarters"
                            runat="server"
                            DataKeyNames="cadanganKuarters_id"
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
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%" />
                                    <ItemStyle VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Nama Pangkalan">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNamaAnak" runat="server" Text='<%# Bind("pangkalan_nama")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30%" />
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Nama Kuarters">
                                    <ItemTemplate>
                                        <asp:Label ID="lblICAnak" runat="server" Text='<%# Bind("kuarters_nama")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30%" />
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Padam">
                                    <ItemTemplate>
                                        <span runat="server" style="float: right">
                                            <asp:ImageButton
                                                Width="12"
                                                Height="12"
                                                ID="btnDelete"
                                                CommandName="Delete"
                                                CommandArgument='<%#Eval("cadanganKuarters_id")%>'
                                                runat="server"
                                                ImageUrl="~/icons/delete.png"
                                                ToolTip="Padam?" />
                                        </span>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="right" VerticalAlign="Top" Width="5%" />
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
                <tr>
                    <td><asp:Button CssClass="btn" Text="Simpan" runat="server" ID="btnSimpanCadanganKuarters" /></td>
                </tr>
            </table>
        </asp:Panel>
        <%--  --%>
    </div>
</div>

<table class ="fbform">
    <tr>
        <td><span id ="MsgBottom" runat ="server" ><asp:Label ID ="strlbl_bottom" runat ="server" ></asp:Label></span></td>
    </tr>
</table>