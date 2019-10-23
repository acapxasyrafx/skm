<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Konfigurasi.Jenis.aspx.vb" Inherits="QuartersManagement.Konfigurasi_Jenis" %>

<%@ Register Src="~/commoncontrol/configuration/konfigurasi_jenis.ascx" TagPrefix="uc1" TagName="konfigurasi_jenis" %>
<%@ Register Src="~/commoncontrol/menu/navigation_menu.ascx" TagPrefix="uc1" TagName="navigation_menu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu runat="server" id="navigation_menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:konfigurasi_jenis runat="server" id="konfigurasi_jenis" />
</asp:Content>
