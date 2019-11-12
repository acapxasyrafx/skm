<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/user.Master" CodeBehind="Status.Permohonan.aspx.vb" Inherits="QuartersManagement.Status_Permohonan" EnableEventValidation = "false"%>

<%@ Register Src="~/commoncontrol/menu/navigation_menu.ascx" TagPrefix="uc1" TagName="navigation_menu" %>
<%@ Register Src="~/commoncontrol/management/status_permohonan.ascx" TagPrefix="uc1" TagName="status_permohonan" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu runat="server" ID="navigation_menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:status_permohonan runat="server" ID="status_permohonan" />
</asp:Content>
