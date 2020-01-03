<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="senarai_permohonan.ascx.vb" Inherits="QuartersManagement.senarai_permohonan" %>

<style>
    .modal {
        display: none; /* Hidden by default */
        position: fixed; /* Stay in place */
        z-index: 1; /* Sit on top */
        left: 0;
        top: 0;
        width: 100%; /* Full width */
        height: 100%; /* Full height */
        overflow: auto; /* Enable scroll if needed */
        background-color: rgb(0,0,0); /* Fallback color */
        background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
        margin:0;
        padding:0;
    }

    /*Modal Header*/
    .modal-header {
      padding: 2px 16px;
      background-color: #f54242;
      color: white;
    }

    /* Modal Body */
    .modal-body {
        padding: 2px 16px;
    }

    /* Modal Footer */
    .modal-footer {
        padding: 2px 16px;
        background-color: #f54242;
        color: white;
    }

    /* Modal Content/Box */
    .modal-content {
        background-color: #fefefe;
        margin: 15% auto; /* 15% from the top and centered */
        padding: 20px;
        border: 1px solid #888;
        width: 40%; /* Could be more or less, depending on screen size */
    }

    /* The Close Button */
    .close {
        color: #aaa;
        float: right;
        font-size: 28px;
        font-weight: bold;
    }

    .close:hover,
    .close:focus {
        color: black;
        text-decoration: none;
        cursor: pointer;
    }

    /* Add Animation */
    @-webkit-keyframes animatetop {
        from {
            top: -300px;
            opacity: 0
        }

        to {
            top: 0;
            opacity: 1
        }
    }

    @keyframes animatetop {
        from {
            top: -300px;
            opacity: 0
        }

        to {
            top: 0;
            opacity: 1
        }
    }
    select{
        width: 20em;
    }
</style>
<div>
    <table class="fbform" style="width: 100%">
        <tr class="fbform_header">
            <td>
                <span id="MsgTop" runat="server"><asp:Label ID="strlbl_top" runat="server"></asp:Label></span>
            </td>
        </tr>
    </table>
    <table id="tblCarian" class="fbform">
        <tr class="fbform_mheader">
            <td colspan="3">Saringan</td>
        </tr>
        <tr>
            <td style="width: 150px;">Pangkalan</td>
            <td>:</td>
            <td><asp:DropDownList runat="server" ID="ddlCarianPangkalan" AutoPostBack="true"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>Kuarters</td>
            <td>:</td>
            <td><asp:DropDownList runat="server" ID="ddlCarianKuarters" Enabled="false" AutoPostBack="true">
                <asp:ListItem Text="-- SILA PILIH PANGKALAN TERLEBIH DAHULU --" />
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>Status Permohonan</td>
            <td>:</td>
            <td>
                <asp:DropDownList runat="server" ID="ddlCarianStatus" AutoPostBack="true"></asp:DropDownList>
            </td>
        </tr>
    </table>
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
                    Height="100%"
                    PageSize="100" 
                    CssClass="gridview_footer"
                    >
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="50px" />
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" width="2%"/>
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                                
                        <asp:TemplateField HeaderText="Tarikh Akhir Di Kemas Kini">
                            <ItemTemplate>
                                <asp:Label ID="lblTarikhPermohonan" runat="server" Text='<%# Bind("permohonan_tarikh") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="10%" />
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status" >
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# changeStatus(Eval("permohonan_status")) %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="20%" />
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Kuarters Dipohon" >
                            <ItemTemplate>
                                <asp:Label ID="lblKuarters" runat="server" Text='<%# Bind("kuarters_nama")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="20%" />
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Keterangan" >
                            <ItemTemplate>
                                <asp:Label ID="lblNota" runat="server" Text='<%# Bind("permohonan_nota")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="30%" />
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tindakan" >
                            <ItemTemplate>
                                <asp:ImageButton
                                    Width="25px"
                                    Height="25px"
                                    runat="server" 
                                    ID="btnView" 
                                    CommandName="View_Permohonan" 
                                    CommandArgument='<%#Eval("permohonan_id") %>'
                                    ImageUrl="~/icons/test.svg"
                                    ToolTip="Buka?"
                                />
                                <asp:ImageButton
                                    Width="25px"
                                    Height="25px"
                                    runat="server" 
                                    ID="btnDelete" 
                                    CommandName="Delete_Permohonan" 
                                    CommandArgument='<%#Eval("permohonan_id") %>'
                                    Visible='<%# showButton(Eval("permohonan_id")) %>'
                                    ImageUrl="~/icons/delete.png" 
                                    ToolTip="Padam?"
                                />
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
    <table class="fbform">
        <tr>
            <td><span id="MsgBottom" runat="server">
                <asp:Label ID="strlbl_bottom" runat="server"></asp:Label></span></td>
        </tr>
    </table>
    <div>
        <div runat="server" class="modal" id="dialogModal">
            <asp:HiddenField runat="server" ID="hiddenPermohonanID"/>
        <table class="fbform modal-content">
            <tr class="modal-header">
                <td class="fbform_mheader" colspan="2">
                    <h3 style="">Batal Permohonan <button runat="server" class="close" id="closeBtn">&times;</button></h3>
                </td>
            </tr>
            <tr>
                <td colspan="3">Sila nyatakan sebab untuk batal permohonan?</td>
            </tr>
            <tr class="modal-body">
                <td colspan="3" style="width:100px;">
                    <asp:TextBox id="txtNota" TextMode="multiline" Columns="100" Rows="5" runat="server" />
                </td>
            </tr>
            <tr class="modal-footer">
                <td>
                    <asp:Button Text="Padam" runat="server" ID="btnBatalPermohonan"/>
                    <asp:Button Text="Tutup" runat="server" ID="btnTutupModal"/>
                </td>
            </tr>
        </table>
    </div>
    </div>
</div>