<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Konfigurasi.aspx.vb" Inherits="QuartersManagement.Konfigurasi1" %>

<%@ Register Src="~/commoncontrol/menu/navigation_menu.ascx" TagPrefix="uc1" TagName="navigation_menu" %>
<%@ Register Src="~/commoncontrol/configuration/konfigurasi.ascx" TagPrefix="uc1" TagName="konfigurasi" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu runat="server" ID="navigation_menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:konfigurasi runat="server" ID="konfigurasi" />
</asp:Content>
