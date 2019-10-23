<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Admin.Homepage.aspx.vb" Inherits="QuartersManagement.Homepage1" %>
<%@ Register src="commoncontrol/main/homepage.ascx" tagname="homepage" tagprefix="uc1" %>
<%@ Register src="commoncontrol/menu/navigation_menu.ascx" tagname="navigation_menu" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc2:navigation_menu ID="navigation_menu1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:homepage ID="homepage1" runat="server" />
</asp:Content>
