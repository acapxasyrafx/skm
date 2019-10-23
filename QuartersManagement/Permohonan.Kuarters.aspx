<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/user.Master" CodeBehind="Permohonan.Kuarters.aspx.vb" Inherits="QuartersManagement.Permohonan_Kuarters1" %>

<%@ Register Src="~/commoncontrol/menu/navigation_menu.ascx" TagPrefix="uc1" TagName="navigation_menu" %>
<%@ Register Src="~/commoncontrol/management/permohonan_kuarters.ascx" TagPrefix="uc1" TagName="permohonan_kuarters" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu runat="server" ID="navigation_menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:permohonan_kuarters runat="server" id="permohonan_kuarters" />
</asp:Content>
