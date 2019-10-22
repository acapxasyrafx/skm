<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Konfigurasi.Pangkalan.Baru.aspx.vb" Inherits="QuartersManagement.Konfigurasi_Pangkalan_Baru1" %>

<%@ Register Src="~/commoncontrol/configuration/konfigurasi_pangkalan_baru.ascx" TagPrefix="uc1" TagName="konfigurasi_pangkalan_baru" %>
<%@ Register Src="~/commoncontrol/menu/navigation_menu.ascx" TagPrefix="uc1" TagName="navigation_menu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu runat="server" id="navigation_menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:konfigurasi_pangkalan_baru runat="server" id="konfigurasi_pangkalan_baru" />
</asp:Content>
