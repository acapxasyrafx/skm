<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Konfigurasi.Surat.Tawaran.aspx.vb" Inherits="QuartersManagement.Konfigurasi_Surat_Tawaran" %>

<%@ Register Src="~/commoncontrol/configuration/konfigurasi_surat.ascx" TagPrefix="uc1" TagName="konfigurasi_surat" %>
<%@ Register Src="~/commoncontrol/menu/navigation_menu.ascx" TagPrefix="uc1" TagName="navigation_menu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu runat="server" id="navigation_menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:konfigurasi_surat runat="server" id="konfigurasi_surat" />
</asp:Content>
