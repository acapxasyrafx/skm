<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Konfigurasi.Pangkalan.aspx.vb" Inherits="QuartersManagement.Konfigurasi_Pangkalan1" %>

<%@ Register Src="~/commoncontrol/configuration/konfigurasi_pangkalan.ascx" TagPrefix="uc1" TagName="konfigurasi_pangkalan" %>
<%@ Register Src="~/commoncontrol/menu/navigation_menu.ascx" TagPrefix="uc1" TagName="navigation_menu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu runat="server" ID="navigation_menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:konfigurasi_pangkalan runat="server" id="konfigurasi_pangkalan" />
</asp:Content>
