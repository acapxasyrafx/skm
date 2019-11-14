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
                                <a href="Pendaftaran.Penjawat.Senarai.aspx?P=<%=lbl2.Text %>"><i></i><img src ="icons/bullet_red.png" /> Carian Senarai Penjawat</a>
                            </li>
                        </ul>
                    </li>
            
                    <li>
                        <a href="#"><span class="">Rekod Lokasi Pangkalan TUDM</span></a>
                        <ul class="nav-flyout">
                            <li><p>Rekod Lokasi Pangkalan TUDM</p></li>
                            <li>
                                <a href="Konfigurasi.Pangkalan.aspx?P=<%=lbl5.Text %>"><i></i><img src ="icons/bullet_red.png" /> Pendaftaran Baharu</a>
                            </li>
                            <%--<li>
                                <a href="Senarai.Pangkalan.aspx?P=<%=lbl4.Text %>"><i></i><img src ="icons/bullet_red.png" /> Carian Senarai Pangkalan</a>
                            </li>--%>
                        </ul>
                    </li>
                    
                    <li>
                        <a href="#"><span class="">Rekod Kuarters</span></a>
                        <ul class="nav-flyout">
                            <li><p>Rekod Kuarters</p></li>
                            <li>
                                <a href="Konfigurasi.Jenis.Kuarters.aspx?P=<%=lbl3.Text %>"><i></i><img src ="icons/bullet_red.png" /> Konfigurasi Jenis Kuarters</a>
                            </li>
                            <li>
                                <a href="Konfigurasi.Kuota.aspx?P=<%=lbl2.Text %>"><i></i><img src ="icons/bullet_red.png" /> Konfigurasi Koata Kuarters</a> 
                            </li>
                            <li>
                                <a href="Konfigurasi.Kuarters.aspx?P=<%=lbl11.Text %>"><i></i><img src ="icons/bullet_red.png" /> Penempatan Lokasi Kuaters</a>
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
                                <a href="Proses.Penempatan.Kuarters.aspx?P=<%=lbl7.Text %>"><i></i><img src ="icons/bullet_red.png" /> Proses Penempatan Kuarters</a>
                                <a href="Senarai.Penempatan.Pemohon.aspx?P=<%=lbl6.Text %>"><i></i><img src ="icons/bullet_red.png" /> Senarai Penempatan Pemohon</a>
                            </li>
                        </ul>
                    </li>
          
                    <li>
                        <a href="#"><span class="">Penyelenggaraan Kuarters</span></a>
                        <ul class="nav-flyout">
                            <li><p>Penyelenggaraan Kuarters</p></li>
                            <li>
                                <a href="Rekod.Penyelenggaraan.aspx?P=<%=lblMaintenance1.Text %>"><i></i><img src ="icons/bullet_red.png" /> Rekod Penyelenggaraan</a>
                            </li>
                            <li>
                                <a href="Rekod.Penyelenggaraan.aspx?P=<%=lblMaintenance2.Text %>"><i></i><img src ="icons/bullet_red.png" /> Jadual Penyelenggaraan</a>
                            </li>
                        </ul>
                    </li>

                </ul>
            </nav>
        </aside>
    </section>
</asp:Panel>

<%-- Menu Konfigurasi --%>

<asp:Panel ID ="pnlKonfigurasi" runat ="server" visible ="true">
    <section class ="appKonfigurasi">
      <aside class="sidebar">
        <header>> KONFIGURASI</header>
        <nav class="sidebar-nav">
          <ul>
            <li>
                <span><a href="Konfigurasi.aspx?P=<%=lblKonfigurasiUmum.Text %>">Konfigurasi Umum</a></span>
            </li>
              <li>
                <span><a href="Konfigurasi.Sistem.aspx?P=<%=lblKonfigurasiSistem.Text %>">Konfigurasi Sistem</a></span>
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
                <a href="Senarai.Permohonan.Pengguna.aspx">Senarai Permohonan</a>
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

    
    <asp:Label ID="lbl5" runat="server" Visible="false">Pengurusan Pentadbiran > Rekod Lokasi Pangkalan TUDM > Pendaftaran Baharu</asp:Label>
    <asp:Label ID="lbl4" runat="server" Visible="false">Pengurusan Pentadbiran > Rekod Lokasi Pangkalan TUDM > Carian Senarai Pangkalan</asp:Label>

    
    <asp:Label ID="lbl3" runat="server" Visible="false">Pengurusan Pentadbiran > Rekod Kuarters > Konfigurasi Jenis Kuarters</asp:Label>
    <asp:Label ID="lvl2" runat="server" Visible="false">Pengurusan Pentadbiran > Rekod Kuarters > Konfigurasi Kuota Kuarters</asp:Label>
    <asp:Label ID="lbl11" runat="server" Visible="false">Pengurusan Pentadbiran > Rekod Kuarters > Penempatan Lokasi Kuarters</asp:Label>
    
    <asp:Label ID="lbl7" runat="server" Visible="false">Pengurusan Pentadbiran > Kuarters > Proses Penempatan Kuarters</asp:Label>    
    <asp:Label ID="lbl6" runat="server" Visible="false">Pengurusan Pentadbiran > Kuarters > Senarai Penempatan Kuarters</asp:Label>

    <asp:Label ID="lblKonfigurasiUmum" runat="server" Visible="false">Penetapan Utama > Konfigurasi Umum</asp:Label>
    <asp:Label ID="lblKonfigurasiSistem" runat="server" Visible="false">Penetapan Utama > Konfigurasi Sistem</asp:Label>

    <asp:Label ID="lblMaintenance1" runat="server" Visible="false">Penyelenggaraan Kuarters > Rekod Penyelenggaraan</asp:Label>
    <asp:Label ID="lblMaintenance2" runat="server" Visible="false">Penyelenggaraan Kuarters > Jadual Penyelenggaraan</asp:Label>

</asp:Panel>