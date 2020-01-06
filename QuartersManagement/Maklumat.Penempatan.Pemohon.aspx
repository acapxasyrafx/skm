<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Maklumat.Penempatan.Pemohon.aspx.vb" Inherits="QuartersManagement.Maklumat_Penempatan_Pemohon" %>

<%@ Register Src="~/commoncontrol/menu/navigation_menu.ascx" TagPrefix="uc1" TagName="navigation_menu" %>
<%@ Register Src="~/commoncontrol/management/maklumat_penempatan_pemohon.ascx" TagPrefix="uc1" TagName="maklumat_penempatan_pemohon" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu runat="server" ID="navigation_menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:maklumat_penempatan_pemohon runat="server" id="maklumat_penempatan_pemohon" />
</asp:Content>
