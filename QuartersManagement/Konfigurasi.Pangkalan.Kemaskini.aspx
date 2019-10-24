<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Konfigurasi.Pangkalan.Kemaskini.aspx.vb" Inherits="QuartersManagement.Konfigurasi_Pangkalan_Kemaskini" %>

<%@ Register Src="~/commoncontrol/menu/navigation_menu.ascx" TagPrefix="uc1" TagName="navigation_menu" %>
<%@ Register Src="~/commoncontrol/configuration/konfigurasi_pangkalan_kemaskini.ascx" TagPrefix="uc1" TagName="konfigurasi_pangkalan_kemaskini" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu runat="server" ID="navigation_menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:konfigurasi_pangkalan_kemaskini runat="server" id="konfigurasi_pangkalan_kemaskini" />
</asp:Content>
