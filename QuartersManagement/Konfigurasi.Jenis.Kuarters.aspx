<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Konfigurasi.Jenis.Kuarters.aspx.vb" Inherits="QuartersManagement.Konfigurasi_Jenis_Kuarters" %>

<%@ Register Src="~/commoncontrol/menu/navigation_menu.ascx" TagPrefix="uc1" TagName="navigation_menu" %>
<%@ Register Src="~/commoncontrol/configuration/konfigurasi_jenisKuarters.ascx" TagPrefix="uc1" TagName="konfigurasi_jenisKuarters" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu runat="server" ID="navigation_menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:konfigurasi_jenisKuarters runat="server" ID="konfigurasi_jenisKuarters" />
</asp:Content>
