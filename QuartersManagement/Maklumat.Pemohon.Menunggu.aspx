<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Maklumat.Pemohon.Menunggu.aspx.vb" Inherits="QuartersManagement._Maklumat_Pemohon_Menunggu" %>

<%@ Register Src="~/commoncontrol/permohonan/maklumat_pemohon_menunggu.ascx" TagPrefix="uc1" TagName="maklumat_pemohon_menunggu" %>
<%@ Register Src="~/commoncontrol/menu/navigation_menu.ascx" TagPrefix="uc1" TagName="navigation_menu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu runat="server" ID="navigation_menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:maklumat_pemohon_menunggu runat="server" ID="maklumat_pemohon_menunggu" />
</asp:Content>
