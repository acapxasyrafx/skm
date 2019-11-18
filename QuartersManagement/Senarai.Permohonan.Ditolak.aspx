<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Senarai.Permohonan.Ditolak.aspx.vb" Inherits="QuartersManagement.Senarai_Permohonan_Ditolak" %>

<%@ Register Src="~/commoncontrol/permohonan/maklumat_pemohon_ditolak.ascx" TagPrefix="uc1" TagName="maklumat_pemohon_ditolak" %>
<%@ Register Src="~/commoncontrol/menu/navigation_menu.ascx" TagPrefix="uc1" TagName="navigation_menu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu runat="server" ID="navigation_menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:maklumat_pemohon_ditolak runat="server" ID="maklumat_pemohon_ditolak" />
</asp:Content>
