<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Senarai.Pemohon.Maklumat.Pemohon.aspx.vb" Inherits="QuartersManagement.Senarai_Pemohon_Maklumat_Pemohon" %>

<%@ Register Src="~/commoncontrol/permohonan/maklumat_pemohon.ascx" TagPrefix="uc1" TagName="maklumat_pemohon" %>
<%@ Register Src="~/commoncontrol/menu/navigation_menu.ascx" TagPrefix="uc1" TagName="navigation_menu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu runat="server" id="navigation_menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:maklumat_pemohon runat="server" id="maklumat_pemohon" />
</asp:Content>
