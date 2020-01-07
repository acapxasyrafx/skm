<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="maklumat_penempatan_pemohon.ascx.vb" Inherits="QuartersManagement.maklumat_penempatan_pemohon1" %>

<style>
    .maklumat-pemohon {
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        width: 100%;
        height: 100%;
        margin-top: 5px;
    }
    .pemohon-left, .permohonan-left {
        width: 49.5%
    }
    .pemohon-right, .permohonan-right {
        width: 49.5%;
    }
    .permohon-left, .pemohon-right {

    }
    .container {
        display: flex;
        width: 99.2%;
        justify-content: space-between;
        flex-direction: row;
    }
    .left, .right {
        display: flex;
        width: 49%;
        margin-top: 5px;
        flex-direction: column;
    }
</style>
<div class="container">
    <div class="left">
        <table class="fbform" style="width: 100%;">
            <tr class="fbform_mheader">
                <td colspan="3">Maklumat Pemohon</td>
            </tr>
            <tr>
                <td style="width: 150px;">Nama Pemohon</td>
                <td style="width: 5px;">:</td>
                <td>
                    <asp:Label Text="" runat="server" ID="lblNama" />
                </td>
            </tr>
            <tr>
                <td style="width: 150px;">Pangkat</td>
                <td style="width: 5px;">:</td>
                <td>
                    <asp:Label Text="" runat="server" ID="lblPangkat" />
                </td>
            </tr>
            <tr>
                <td style="width: 150px;">No. Tentera</td>
                <td style="width: 5px;">:</td>
                <td>
                    <asp:Label Text="" runat="server" ID="lblNoTentera" />
                </td>
            </tr>
            <tr>
                <td style="width: 150px;">Tarikh Lahir</td>
                <td style="width: 5px;">:</td>
                <td>
                    <asp:Label Text="" runat="server" ID="lblTarikhLahir" />
                </td>
            </tr>
            <tr>
                <td style="width: 150px;">Status Perkahwinan</td>
                <td style="width: 5px;">:</td>
                <td>
                    <asp:Label Text="" runat="server" ID="lblStatusPerkahwinan" />
                </td>
            </tr>
        </table>
        <table class="fbform" style="width: 100%;">
            <tr class="fbform_mheader">
                <td colspan="3">Maklumat Kuarters</td>
            </tr>
            <tr>
                <td style="width: 150px;">Nama Unit</td>
                <td style="width: 5px;">:</td>
                <td>
                    <asp:Label Text="" runat="server" ID="lblNamaUnit" />
                </td>
            </tr>
            <tr>
                <td style="width: 150px;">Kuarters</td>
                <td style="width: 5px;">:</td>
                <td>
                    <asp:Label Text="" runat="server" ID="lblNamaKuarters" />
                </td>
            </tr>
            <tr>
                <td style="width: 150px;">Pangkalan</td>
                <td style="width: 5px;">:</td>
                <td>
                    <asp:Label Text="" runat="server" ID="lblNamaPangkalan" />
                </td>
            </tr>
        </table>
    </div>
    <div class="right">
        <table class="fbform" style="width: 100%;">
            <tr class="fbform_mheader">
                <td colspan="3">Maklumat Keluarga</td>
            </tr>
            <tr>
                <td style="width: 150px;">Tempat Tinggal Akhir</td>
                <td style="width: 5px;">:</td>
                <td>
                    <asp:Label Text="" runat="server" ID="lblTempatTinggalAkhir" />
                </td>
            </tr>
            <tr>
                <td style="width: 150px;">Tarikh Mula Menetap</td>
                <td style="width: 5px;">:</td>
                <td>
                    <asp:Label Text="" runat="server" ID="lblTarikhMulaMenetap" />
                </td>
            </tr>
            <tr runat="server" visible="false">
                <td style="width: 150px;">Tarikh Akhir Menetap</td>
                <td style="width: 5px;">:</td>
                <td>
                    <asp:Label Text="" runat="server" ID="lblTarikhAkhirMenetap" />
                </td>
            </tr>
        </table>
        <%-- Maklumat Anak --%>
        <table class="fbform">
            <tr class="fbform_mheader">
                <td colspan="3">Makluamt Anak</td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView
                        ID="tblMaklumatAnak"
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
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                                <ItemStyle VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Nama">
                                <ItemTemplate>
                                    <asp:Label ID="lblHistoryNamaAnak" runat="server" Text='<%# Bind("historyAnak_nama")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="40%" />
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="KP">
                                <ItemTemplate>
                                    <asp:Label ID="lblHistoryAnak" runat="server" Text='<%# Bind("historyAnak_ic")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="40%" />
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
        <table class="fbform" style="width: 100%;">
            <tr class="fbform_mheader">
                <td colspan="3">Surat Tawaran kuarters</td>
            </tr>
            <tr>
                <td style="width: 150px;">Tarikh Diberi</td>
                <td style="width: 5px;">:</td>
                <td>
                    <asp:Label Text="" runat="server" ID="lblTarikhSuratTawaran"/>
                </td>
            </tr>
            <tr class="fbform_mheader">
                <td colspan="3">Kandungan Surat Tawaran :</td>
            </tr>
            <tr>
                <td colspan="3">
                    <div runat="server" id="divSuratTawaran"></div>
                </td>
            </tr>
        </table>
    </div>
</div>