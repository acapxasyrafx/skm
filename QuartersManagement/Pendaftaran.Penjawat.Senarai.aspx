<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Pendaftaran.Penjawat.Senarai.aspx.vb" Inherits="QuartersManagement.Pendaftaran_Penjawat_Senarai1" %>

<%@ Register Src="~/commoncontrol/management/senarai_penjawat.ascx" TagPrefix="uc1" TagName="senarai_penjawat" %>
<%@ Register Src="~/commoncontrol/menu/navigation_menu.ascx" TagPrefix="uc1" TagName="navigation_menu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu runat="server" ID="navigation_menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:senarai_penjawat runat="server" ID="senarai_penjawat" />
</asp:Content>
