<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="homepage.ascx.vb" Inherits="QuartersManagement.homepage" %>
<style>
    .header-title{
        margin:0;
        padding: 20px 0;
    }
    .body{
        display: flex;
        flex-direction: row;
        justify-content: space-evenly;
        height: 100%;
    }
    .body-left {
        width: 70%;
    }
    .body-right {
        width: 30%;
    }
    .status-list {
        list-style: none;
        margin:0;
        padding:0;
    }
    .status-item {
        display: block;
        border-radius: 5px;
        margin: 2px 0;
        padding: 5px 5px;
        background: #e9f83c;
        height: 10%;
    }
    li.status-item>a {
        display: flex;
        justify-content: space-between;
        text-decoration: none;
        cursor: pointer;
    }
    .status-title {
        align-items: center;
        font-weight: bold;
        font-size: 1.2em;
    }
    .status-count {
        border-radius: 50%;
        background: red;
        color: white;
        padding: 5px;
    }
</style>
<div class="fbform" style="width: 90%; height: 70em; padding: 0 5%;">
    <div class="header-title">
        <h1>
            <span style="align-items: center">
                <asp:Label runat="server" ID="lblWelcome"></asp:Label>
            </span>
        </h1>
    </div>
    <hr/>
    <div class="body">
        <div class="body-left">
        </div>
        <div class="body-right">
            <div>
                <ul class="status-list">
                    <li class="status-item">
                        <a href="#">
                            <span class="status-title" runat="server" id="lblBaruText">Permohonan Baru</span>
                            <span class="status-count">
                                <asp:Label Text="0" runat="server" ID="lblBaruCount"/>
                            </span>
                        </a>
                    </li>
                    <li class="status-item">
                        <a href="#">
                            <span class="status-title" runat="server" id="lblMenunggguText">Permohonan Menunggu</span>
                            <span class="status-count">
                                <asp:Label Text="0" runat="server" ID="lblMenungguCount"/>
                            </span>
                        </a>
                    </li>
                    <li class="status-item">
                        <a href="#">
                            <span class="status-title" runat="server" id="lblTerimaText">Permohonan Terima</span>
                            <span class="status-count">
                                <asp:Label Text="0" runat="server" ID="lblTerimaCount"/>
                            </span>
                        </a>
                    </li>
                    <li class="status-item">
                        <a href="#">
                            <span class="status-title" runat="server" id="lblTolakText">Permohonan Tolak</span>
                            <span class="status-count">
                                <asp:Label Text="0" runat="server" ID="lblTolakCount"/>
                            </span>
                        </a>
                    </li>
                </ul>
            </div>

        </div>
    </div>
</div>

