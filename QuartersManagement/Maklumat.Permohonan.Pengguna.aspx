<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/user.Master" CodeBehind="Maklumat.Permohonan.Pengguna.aspx.vb" Inherits="QuartersManagement.WebForm5" EnableEventValidation = "false" %>

<%@ Register Src="~/commoncontrol/management/maklumat_permohonan.ascx" TagPrefix="uc1" TagName="maklumat_permohonan" %>
<%@ Register Src="~/commoncontrol/menu/navigation_menu.ascx" TagPrefix="uc1" TagName="navigation_menu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu runat="server" ID="navigation_menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:maklumat_permohonan runat="server" ID="maklumat_permohonan" />
</asp:Content>
