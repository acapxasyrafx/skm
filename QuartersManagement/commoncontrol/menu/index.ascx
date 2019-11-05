<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="index.ascx.vb" Inherits="QuartersManagement.index" %>
<div>
<%-- Menu pengurusan pentadbiran --%>
<asp:Panel ID ="PnlPengurusan" runat ="server" visible ="true">
    <section class ="appPentadbiran">
        <aside class="sidebar">
        <header>> BAHAGIAN PENTADBIRAN</header>
            <nav class="sidebar-nav">
                <ul>
                    <li>
                        <a href="#"><span class="">Rekod Senarai Penjawat</span></a>
                        <ul class="nav-flyout">
                            <li><p>Rekod Senarai Penjawat</p></li>
                            <li>
                                <a href="Pendaftaran.Penjawat.aspx?P=<%=lbl1.Text %>"><i></i><img src ="icons/bullet_red.png" />Pendaftaran Baharu</a>
                            </li>
                            <li>
                                <a href="Senarai.Penjawat.aspx?P=<%=lbl2.Text %>"><i></i><img src ="icons/bullet_red.png" /> Carian Senarai Penjawat</a>
                            </li>
                        </ul>
                    </li>
            
                    <li>
                        <a href="#"><span class="">Rekod Lokasi Pangkalan TUDM</span></a>
                        <ul class="nav-flyout">
                            <li><p>Rekod Lokasi Pangkalan TUDM</p></li>
                            <li>
                                <a href="#"><i></i><img src ="icons/bullet_red.png" /> Pendaftaran Baharu</a>
                            </li>
                            <li>
                                <a href="#"><i></i><img src ="icons/bullet_red.png" /> Carian Senarai Pangkalan</a>
                            </li>
                        </ul>
                    </li>
                    
                    <li>
                        <a href="#"><span class="">Rekod Kuarters</span></a>
                        <ul class="nav-flyout">
                            <li><p>Rekod Kuarters</p></li>
                            <li>
                                <a href="#"><i></i><img src ="icons/bullet_red.png" /> Konfigurasi Jenis Kuarters</a>
                            </li>
                            <li>
                                <a href="#"><i></i><img src ="icons/bullet_red.png" /> Konfigurasi Koata Kuarters</a> 
                            </li>
                            <li>
                                <a href="#"><i></i><img src ="icons/bullet_red.png" /> Penempatan Lokasi Kuaters</a>
                            </li>
                        </ul>
                    </li>
       
                    <li>
                        <a href="#"><span class="">Senarai Permohonan </span></a>
                        <ul class="nav-flyout">
                            <li><p>Senarai Permohonan</p></li>
                            <li>
                                <a href="Senarai.Permohonan.Baru.aspx?P=<%=lbl8.Text %>"><i></i><img src ="icons/bullet_red.png" /> Senarai Permohonan Baharu</a>
                            </li>
                            <li>
                                <a href="Senarai.Permohonan.Menunggu.aspx?P=<%=lbl9.Text %>"><i></i><img src ="icons/bullet_red.png" />Senarai Permohonan Menunggu</a>
                            </li>
                            <li>
                                <a href="Senarai.Permohonan.Tolak.aspx?P=<%=lbl10.Text %>"><i></i><img src ="icons/bullet_red.png" />Senarai Permohonan Tolak</a>
                            </li>
                        </ul>
                    </li>
        
                    <li>
                        <a href="#"><span>Senarai Penempatan Kuarters Pemohon</span></a>
                        <ul class="nav-flyout">
                            <li><p>Senarai Penempatan Kuarters Pemohon</p></li>
                            <li>
                                <a href="#"><i></i><img src ="icons/bullet_red.png" /> Proses Penempatan Kuarters</a>
                            </li>
                        </ul>
                    </li>
          
                    <li>
                        <a href="#"><span class="">Penyelenggaraan Kuarters</span></a>
                        <ul class="nav-flyout">
                            <li><p>Penyelenggaraan Kuarters</p></li>
                            <li>
                                <a href="#"><i></i><img src ="icons/bullet_red.png" /> Rekod Penyelenggaraan</a>
                            </li>
                            <li>
                                <a href="#"><i></i><img src ="icons/bullet_red.png" /> Jadual Penyelenggaraan</a>
                            </li>
                        </ul>
                    </li>

                </ul>
            </nav>
        </aside>
    </section>
</asp:Panel>

<%-- Menu Konfigurasi --%>
<asp:Panel ID="PnlKonfigurasi" runat="server" Visible="true">
    <section class="appKonfigurasi">
        <aside class="sidebar">
            <header>> PENETAPAN UTAMA</header>
            <nav class="sidebar-nav">
                <ul>
                    <li>
                        <a href="#"><span class="">Konfigurasi Sistem</span></a>
                         <ul class="nav-flyout">
                            <li><p>Konfigurasi Sistem Kuarters</p></li>
                            <li>
                                <a href="Konfigurasi.aspx?P=<%=lblKonfigurasi.Text %>"><i></i><img src ="icons/bullet_red.png" /> Konfigurasi Sistem </a>
                            </li>
                        </ul>
                    </li>                   
                </ul>
            </nav>
        </aside>
    </section>
</asp:Panel>
</div>


<%-- Menu Indeks Permohonan --%>
<asp:Panel ID ="PnlPemohon" runat ="server" visible ="true">
    <section class ="appIndeks">
      <aside class="sidebar">
        <header>> PERMOHONAN KUARTERS</header>
        <nav class="sidebar-nav">
          <ul>
            <li>
                <a href="Permohonan.Kuarters.aspx">Permohonan Kuarters</a>
            </li>
            <li runat="server" id="linkStatusPermohonan">
                <a href="Status.Permohonan.aspx">Status Permohonan</a>
            </li>    
          </ul>
        </nav>
      </aside>
    </section>
</asp:Panel>


<%-- Menu Label Indicator --%>
<asp:Panel ID="PnlIndicator" runat="server">

    <asp:Label ID="lbl1" runat="server" Visible="false">Pengurusan Pentadbiran > Rekod Senarai Penjawat > Pendaftaran Baharu</asp:Label>
    <asp:Label ID="lbl2" runat="server" Visible="false">Pengurusan Pentadbiran > Rekod Senarai Penjawat > Carian Senarai Penjawat</asp:Label>

    <asp:Label ID="lbl8" runat="server" Visible="false">Pengurusan Pentadbiran > Senarai Permohonan > Senarai Permohonan Baru</asp:Label>
    <asp:Label ID="lbl9" runat="server" Visible="false">Pengurusan Pentadbiran > Senarai Permohonan > Senarai Permohonan Menunggu</asp:Label>
    <asp:Label ID="lbl10" runat="server" Visible="false">Pengurusan Pentadbiran > Senarai Permohonan > Senarai Permohonan Tolak</asp:Label>

    <asp:Label ID="lblKonfigurasi" runat="server" Visible="false">Penetapan Utama > Konfigurasi</asp:Label>

</asp:Panel>