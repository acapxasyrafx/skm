<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Senarai.Pangkalan.aspx.vb" Inherits="QuartersManagement.Senarai_Pangkalan" %>

<%@ Register Src="~/commoncontrol/menu/navigation_menu.ascx" TagPrefix="uc1" TagName="navigation_menu" %>
<%@ Register Src="~/commoncontrol/management/senarai_pangkalan.ascx" TagPrefix="uc1" TagName="senarai_pangkalan" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:navigation_menu runat="server" ID="navigation_menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:senarai_pangkalan runat="server" id="senarai_pangkalan" />
</asp:Content>
