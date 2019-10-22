<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Pendaftaran.Penjawat.aspx.vb" Inherits="QuartersManagement.Pendaftaran_Penjawat1" %>
<%@ Register src="commoncontrol/menu/navigation_menu.ascx" tagname="navigation_menu" tagprefix="uc1" %>
<%@ Register src="commoncontrol/management/pendaftaran_penjawat.ascx" tagname="pendaftaran_penjawat" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu ID="navigation_menu1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc2:pendaftaran_penjawat ID="pendaftaran_penjawat1" runat="server" />
</asp:Content>
