<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="user_homepage.ascx.vb" Inherits="QuartersManagement.user_homepage" %>
<style>
    .header-title{
        margin:0;
        padding: 10px 0;
        display:inline-flex;
    }
    .body{
        display: flex;
        flex-direction: row;
        height: 100%;
    }
    .body-left {
        width: 80%;
    }
    .body-right {
        width: 20%;
    }
</style>
<div class="fbform" style="width: 90%; height: 70em; padding: 0 5%;">
    <div class="header-title">
        <h1>
            <span style="align-items: center">
                <asp:Label runat="server" ID="lblWelcome">Selamat Datang ADMIN</asp:Label>
            </span>
        </h1>
    </div>
    <hr />
    <div class="body">
        <div class="body-left">
            
        </div>
        <div class="body-right">
            
        </div>
    </div>
</div>