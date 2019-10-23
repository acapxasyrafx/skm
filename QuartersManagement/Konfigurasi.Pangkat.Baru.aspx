<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Konfigurasi.Pangkat.Baru.aspx.vb" Inherits="QuartersManagement.Konfigurasi_Pangkat_Baru1" %>

<%@ Register Src="~/commoncontrol/configuration/konfigurasi_pangkat_baru.ascx" TagPrefix="uc1" TagName="konfigurasi_pangkat_baru" %>
<%@ Register Src="~/commoncontrol/menu/navigation_menu.ascx" TagPrefix="uc1" TagName="navigation_menu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu runat="server" id="navigation_menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:konfigurasi_pangkat_baru runat="server" id="konfigurasi_pangkat_baru" />
</asp:Content>
