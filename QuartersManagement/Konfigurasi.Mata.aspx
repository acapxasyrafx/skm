<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Konfigurasi.Mata.aspx.vb" Inherits="QuartersManagement.Konfigurasi_Mata1" %>

<%@ Register Src="~/commoncontrol/configuration/konfigurasi_mata.ascx" TagPrefix="uc1" TagName="konfigurasi_mata" %>
<%@ Register Src="~/commoncontrol/menu/navigation_menu.ascx" TagPrefix="uc1" TagName="navigation_menu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu runat="server" ID="navigation_menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:konfigurasi_mata runat="server" ID="konfigurasi_mata" />
</asp:Content>
