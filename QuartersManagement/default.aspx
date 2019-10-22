<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default.aspx.vb" Inherits="QuartersManagement._default" %>

<%@ Register src="commoncontrol/main/login_page.ascx" tagname="log" tagprefix="uc1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Prototype Pengurusan Kuarters</title>
    <link rel = "icon" href ="mini.ico">
    <link rel="stylesheet" type="text/css" href="css/styles.css" />
</head>
<body style ="background-image :url(img/backlog.jpg)">
    <form id="form1" runat="server">

     
                <uc1:log ID="log1" runat="server" />
                
                <div id="footer">
                    <p>Tentera Udara Diraja Malaysia (TUDM) @ 2019<br />Prototaip Versi 0.1
                   </p>
                </div>
    </form>
</body>
</html>