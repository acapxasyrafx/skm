<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="login_page.ascx.vb" Inherits="QuartersManagement.login_page" %>
<table style ="width :35%; margin-left :400px;margin-top:50px;background-color :whitesmoke  ;padding: 20px 20px 20px 20px ; border-radius : 25px 25px 25px 25px">
    <tr>
        <td colspan ="3" style =" text-align :center ">
             <img src="img/TudmLogo.png" style="height: 60%; width: 60%; text-align:center " />
        </td>
    </tr>
     <tr>
        <td colspan="3" style ="text-align :center "><strong>SELAMAT DATANG</strong></td>
    </tr>
     <tr>
        <td colspan="3" style ="text-align :center "><strong>KE</strong></td>
    </tr>
    <tr>
        <td colspan="3" style ="text-align :center "><strong>SISTEM PENGURUSAN KUARTERS</strong></td>
    </tr>
    <tr>
        <td colspan ="3">&nbsp</td>
    </tr>
    <tr>
        <td style ="width :30%">ID PENGGUNA</td>
        <td style ="width :2%">:</td>
        <td><asp:TextBox ID="txtLoginID" runat="server" Text="" Width="200px" AutoComplete="OFF"></asp:TextBox></td>  
    </tr>
    <tr>
         <td>KATA LALUAN</td>
         <td>:</td>
         <td><asp:TextBox ID="txtPwd" runat="server" TextMode="Password" Width="200px" AutoComplete="OFF"></asp:TextBox></td>
   
    </tr>
    <tr>
        <td colspan="3"><p runat="server" style="color: red" id="alertMsg"></p></td>
    </tr>
    <tr>
        <td>&nbsp</td>
    </tr>
    <tr>
         <td colspan ="3"><span style ="float :right "><asp:Button ID="btnLogin" runat="server" Text="Log Masuk" /></span> </td>
    </tr>
</table>
<br />
<br />

<asp:Label ID="lblDebug" runat="server" Text=""></asp:Label>
<asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
<br />
<br />

<asp:Label ID="lbl1" runat ="server" Visible ="false">Paparan Utama</asp:Label>