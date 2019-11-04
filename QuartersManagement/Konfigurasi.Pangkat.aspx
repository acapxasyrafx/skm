<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Konfigurasi.Pangkat.aspx.vb" Inherits="QuartersManagement.Konfigurasi_Pangkat1" %>

<%@ Register Src="~/commoncontrol/configuration/konfigurasi_pangkat.ascx" TagPrefix="uc1" TagName="konfigurasi_pangkat" %>
<%@ Register Src="~/commoncontrol/menu/navigation_menu.ascx" TagPrefix="uc1" TagName="navigation_menu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu runat="server" ID="navigation_menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:konfigurasi_pangkat runat="server" ID="konfigurasi_pangkat" />
</asp:Content>
