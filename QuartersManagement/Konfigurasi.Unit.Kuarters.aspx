<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Konfigurasi.Unit.Kuarters.aspx.vb" Inherits="QuartersManagement.Konfigurasi_Unit_Kuarters" %>

<%@ Register Src="~/commoncontrol/menu/navigation_menu.ascx" TagPrefix="uc1" TagName="navigation_menu" %>
<%@ Register Src="~/commoncontrol/configuration/konfigurasi_unit.ascx" TagPrefix="uc1" TagName="konfigurasi_unit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu runat="server" ID="navigation_menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:konfigurasi_unit runat="server" id="konfigurasi_unit" />
</asp:Content>
