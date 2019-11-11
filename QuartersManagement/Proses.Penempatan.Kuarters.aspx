<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Proses.Penempatan.Kuarters.aspx.vb" Inherits="QuartersManagement.Proses_Penempatan_Kuarters" %>

<%@ Register Src="~/commoncontrol/menu/navigation_menu.ascx" TagPrefix="uc1" TagName="navigation_menu" %>
<%@ Register Src="~/commoncontrol/penempatan_kuarters/proses_penempatan_kuarters.ascx" TagPrefix="uc1" TagName="proses_penempatan_kuarters" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu runat="server" ID="navigation_menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:proses_penempatan_kuarters runat="server" id="proses_penempatan_kuarters" />
</asp:Content>
