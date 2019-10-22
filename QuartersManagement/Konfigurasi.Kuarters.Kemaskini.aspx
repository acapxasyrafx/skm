<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Konfigurasi.Kuarters.Kemaskini.aspx.vb" Inherits="QuartersManagement.Konfigurasi_Kuarters_Kemaskini" %>

<%@ Register Src="~/commoncontrol/menu/navigation_menu.ascx" TagPrefix="uc1" TagName="navigation_menu" %>
<%@ Register Src="~/commoncontrol/configuration/konfigurasi_kuarters_kemaskini.ascx" TagPrefix="uc1" TagName="konfigurasi_kuarters_kemaskini" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu runat="server" ID="navigation_menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:konfigurasi_kuarters_kemaskini runat="server" ID="konfigurasi_kuarters_kemaskini" />
</asp:Content>
