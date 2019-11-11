<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Pengguna.Pendaftaran.Edit.aspx.vb" Inherits="QuartersManagement.Pengguna_Pendaftaran_Edit" %>

<%@ Register Src="~/commoncontrol/management/pendaftaran_penjawat.ascx" TagPrefix="uc1" TagName="pendaftaran_penjawat" %>
<%@ Register Src="~/commoncontrol/menu/navigation_menu.ascx" TagPrefix="uc1" TagName="navigation_menu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu runat="server" ID="navigation_menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:pendaftaran_penjawat runat="server" ID="pendaftaran_penjawat" />
</asp:Content>
