<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Senarai.Permohonan.Baru.aspx.vb" Inherits="QuartersManagement.Status_Permohonan_Baru" %>

<%@ Register Src="~/commoncontrol/permohonan/permohonan_baru.ascx" TagPrefix="uc1" TagName="permohonan_baru" %>
<%@ Register Src="~/commoncontrol/menu/navigation_menu.ascx" TagPrefix="uc1" TagName="navigation_menu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu runat="server" id="navigation_menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:permohonan_baru runat="server" id="permohonan_baru" />
</asp:Content>
