<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Senarai.Penempatan.Pemohon.aspx.vb" Inherits="QuartersManagement.WebForm2" %>

<%@ Register Src="~/commoncontrol/menu/navigation_menu.ascx" TagPrefix="uc1" TagName="navigation_menu" %>
<%@ Register Src="~/commoncontrol/management/senarai_penempatan_pemohon.ascx" TagPrefix="uc1" TagName="senarai_penempatan_pemohon" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu runat="server" ID="navigation_menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:senarai_penempatan_pemohon runat="server" id="senarai_penempatan_pemohon" />
</asp:Content>
