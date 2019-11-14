<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/user.Master" CodeBehind="Senarai.Permohonan.Pengguna.aspx.vb" Inherits="QuartersManagement.WebForm4" EnableEventValidation = "false"%>

<%@ Register Src="~/commoncontrol/menu/navigation_menu.ascx" TagPrefix="uc1" TagName="navigation_menu" %>
<%@ Register Src="~/commoncontrol/management/senarai_permohonan.ascx" TagPrefix="uc1" TagName="senarai_permohonan" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu runat="server" ID="navigation_menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:senarai_permohonan runat="server" ID="senarai_permohonan" />
</asp:Content>
