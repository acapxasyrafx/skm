Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Web.UI.HtmlControls
Public Class maklumat_permohonan
    Inherits System.Web.UI.UserControl

    Dim strlblMsgBottom As String = ConfigurationManager.AppSettings("lblMessage_bottom")
    Dim strlblMsgTop As String = ConfigurationManager.AppSettings("lblMessage_top")
    Dim strSucDelAlert As String = ConfigurationManager.AppSettings("deleteSuccessAlert")
    Dim strFailDelAlert As String = ConfigurationManager.AppSettings("deleteFailAlert")
    Dim strSaveSuccessAlert As String = ConfigurationManager.AppSettings("saveSuccessAlert")
    Dim strSaveFailAlert As String = ConfigurationManager.AppSettings("saveFailAlert")
    Dim strDataBindAlert As String = ConfigurationManager.AppSettings("dataBindAlert")
    Dim strRecordBindAlert As String = ConfigurationManager.AppSettings("recordBindAlert")
    Dim strSysErrorAlert As String = ConfigurationManager.AppSettings("systemErrorAlert")
    Dim strDataValAlert As String = ConfigurationManager.AppSettings("dataValidationAlert")

    Dim oCommon As New Commonfunction

    Dim pangkalanID As Integer = 0
    Dim permohonanID As Integer
    Dim statusPermohonan As String = ""
    Dim subStatusPermohonan As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("user_id") IsNot Nothing Then
            pID.Value = Session("user_id")
            permohonanID = Request.QueryString("permohonan")
            Load_Page()
        Else
            Response.Redirect("/")
        End If
    End Sub

    Private Sub Load_Page()

        maklumatPermohonan()
        maklumatAnak()
        maklumatStatusPermohonan()
        update_notifikasi(Request.QueryString("uid"))
    End Sub

    Private Sub maklumatPermohonan()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("
                SELECT
	                A.permohonan_id
	                , D.pengguna_id
	                , G.pengguna_nama
	                , G.pengguna_jantina
	                , G.pengguna_tarikh_lahir
					, G.pengguna_no_tentera
	                , D.historyPengguna_statusPerkahwinan
	                , F.pangkat_nama
	                , D.historyPengguna_penggunaNoTentera
	                , G.pengguna_mula_perkhidmatan
	                , G.pengguna_tamat_perkhidmatan
	                , A.permohonan_no_permohonan
	                , A.kuarters_id
	                , B.kuarters_nama
	                , C.pangkalan_nama
	                , A.permohonan_tarikh
	                , A.permohonan_status
	                , A.permohonan_sub_status
	                , A.permohonan_mata
					, A.permohonan_nota
	                , E.historyKeluarga_tempat_tinggal
	                , E.historyKeluarga_tarikh_mula
	                , E.historyKeluarga_tarikh_akhir
					, H.suratTawaran_content
					, H.suratTawaran_tarikh_dibuat
                    , I.unit_id
					, (I.unit_blok + '-' + I.unit_tingkat + '-' + I.unit_nombor) as 'unit_nama'
                    , A.permohonan_tarikh_kemasukan
                FROM 
	                spk_permohonan A
	                LEFT JOIN spk_kuarters B ON B.kuarters_id = A.kuarters_id
	                LEFT JOIN spk_pangkalan C ON C.pangkalan_id = B.pangkalan_id
	                LEFT JOIN spk_historyPengguna D ON D.permohonan_id = A.permohonan_id
	                LEFT JOIN spk_historyKeluarga E ON E.permohonan_id = A.permohonan_id
	                LEFT JOIN spk_pangkat F ON F.pangkat_id = D.pangkat_id
	                LEFT JOIN spk_pengguna G ON G.pengguna_id = D.pengguna_id
					LEFT JOIN spk_suratTawaran H ON H.permohonan_id = A.permohonan_id
					LEFT JOIN spk_unit I ON I.unit_id = A.unit_id
                WHERE
                    A.permohonan_id = " & permohonanID & "
                ;
            ", conn)
            Try
                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader
                    If reader.HasRows Then
                        While reader.Read()
                            lblPangkat.Text = reader("pangkat_nama").ToString()
                            lblNoTentera.Text = reader("historyPengguna_penggunaNoTentera").ToString()
                            lblNama.Text = reader("pengguna_nama").ToString()
                            lblJantina.Text = reader("pengguna_jantina").ToString()
                            lblTarikhLahir.Text = reader("pengguna_tarikh_lahir").ToString()
                            lblStatusKahwin.Text = reader("historyPengguna_statusPerkahwinan").ToString()
                            lblMulaBerkhidmat.Text = reader("pengguna_mula_perkhidmatan").ToString()
                            lblTamatBerkhidmat.Text = reader("pengguna_tamat_perkhidmatan").ToString()
                            lblJenisTempatTinggal.Text = reader("historyKeluarga_tempat_tinggal").ToString()
                            lblTarikhMulaMenetap.Text = reader("historyKeluarga_tarikh_mula").ToString()
                            lblKuarterDipohon.Text = reader("kuarters_nama").ToString()
                            lblTarikhPermohonan.Text = reader("permohonan_tarikh").ToString()
                            statusPermohonan = reader("permohonan_status").ToString()
                            subStatusPermohonan = reader("permohonan_sub_status").ToString()
                            hfUnitID.Value = reader("unit_id").ToString()
                            Select Case statusPermohonan
                                Case "PERMOHONAN BARU"
                                    mvMaklumatStatus.ActiveViewIndex = 0
                                Case "PERMOHONAN MENUNGGU"
                                    Select Case subStatusPermohonan
                                        Case "TAWARAN UNIT"
                                            pText.InnerText = "Berikut adalah surat tawaran unit. Jika bersetuju dengan unit yang diterima, sila klik 'Terima'"
                                            divSuratTawaran.InnerHtml = Server.HtmlDecode(reader("suratTawaran_content"))
                                            lblUnit.Text = reader("unit_nama")
                                            lblTarikhMasuk.Text = reader("permohonan_tarikh_kemasukan")
                                            trUnitDitawarkan.Visible = True
                                            trTarikhMasuk.Visible = True
                                            mvMaklumatStatus.ActiveViewIndex = 2
                                        Case "TERIMA TAWARAN UNIT"
                                            pText.InnerText = "Berikut adalah surat tawaran yang dihantar."
                                            divSuratTawaran.InnerHtml = Server.HtmlDecode(reader("suratTawaran_content"))
                                            lblUnit.Text = reader("unit_nama")
                                            lblTarikhMasuk.Text = reader("permohonan_tarikh_kemasukan")
                                            trUnitDitawarkan.Visible = True
                                            trTarikhMasuk.Visible = True
                                            btnGroupTerimaTawaran.Visible = False
                                            mvMaklumatStatus.ActiveViewIndex = 2
                                        Case "CADANGKAN KUARTERS LAIN"
                                            btnField.Visible = True
                                            maklumatCadanganKuarters()
                                            mvMaklumatStatus.ActiveViewIndex = 3
                                        Case Else
                                            Debug.WriteLine("SubStatus(Else): " & subStatusPermohonan)
                                            mvMaklumatStatus.ActiveViewIndex = 1
                                    End Select
                                Case "PERMOHONAN DITOLAK"
                                    lblSebabDitolak.Text = reader("permohonan_nota").ToString
                                    lblKeputusanTolak.Text = reader("permohonan_sub_status")
                                    mvMaklumatStatus.ActiveViewIndex = 4
                                Case "PERMOHONAN DITERIMA"
                                    lblKeputusanTerima.Text = statusPermohonan
                                    pSuratTawaran.InnerHtml = Server.HtmlDecode(reader("suratTawaran_content"))
                                    lblUnit.Text = reader("unit_nama")
                                    lblTarikhMasuk.Text = reader("permohonan_tarikh_kemasukan")
                                    trUnitDitawarkan.Visible = True
                                    trTarikhMasuk.Visible = True
                                    mvMaklumatStatus.ActiveViewIndex = 5
                            End Select
                        End While
                    Else
                        Debug.WriteLine("Error(maklumatPermohonan): Reader has no row")
                    End If
                End Using
            Catch ex As Exception
                Debug.WriteLine("Error(maklumatPermohonan): " & ex.Message)
            Finally
                Debug.WriteLine("status permohonan: " & statusPermohonan.ToString())
                Debug.WriteLine("sub status permohonan: " & subStatusPermohonan.ToString())
                Debug.WriteLine("Success: maklumatUser")
                conn.Close()
            End Try
        End Using
    End Sub

    Private Sub maklumatStatusPermohonan()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim reader As SqlDataReader
            Dim cmd As New SqlCommand("
                SELECT log_tarikh, log_status 
                FROM spk_logPermohonan 
                WHERE 
                    pengguna_id = " & pID.Value & " 
                    AND permohonan_id = " & permohonanID & ";", conn)
            Try
                conn.Open()
                reader = cmd.ExecuteReader
                If reader.HasRows() Then
                    While reader.Read()
                        Select Case reader("log_status")
                            Case "PERMOHONAN BARU"
                                Debug.WriteLine("Permohonan Status: " & reader("log_status"))
                                permohonanBaharu.Attributes("class") = "progress-done"
                                lblTarikhBaharu.Text = reader("log_tarikh")
                            Case "PERMOHONAN DITERIMA", "PERMOHONAN DITOLAK"
                                Debug.WriteLine("Permohonan Status: " & reader("log_status"))
                                permohonanKeputusan.Attributes("class") = "progress-done"
                                lblTarikhKeputusan.Text = reader("log_tarikh")
                                permohonanMenunggu.Visible = False
                            Case "PERMOHONAN MENUNGGU"
                                Debug.WriteLine("Permohonan Status: " & reader("log_status"))
                                permohonanMenunggu.Attributes("class") = "progress-done"
                                lblTarikhMenuggu.Text = reader("log_tarikh")
                                permohonanMenunggu.Visible = True
                            Case Else
                                Debug.WriteLine("ELSE")
                        End Select
                    End While
                End If
            Catch ex As Exception

            End Try
        End Using
    End Sub

    Private Sub maklumatAnak()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim ds As New DataSet
            Dim da As New SqlDataAdapter("
                SELECT
	                *
                FROM
	                spk_historyAnak A
                WHERE
	                A.permohonan_id = " & permohonanID & "
                ;
            ", conn)
            Try
                conn.Open()
                da.Fill(ds, "AnyTable")
                If ds.Tables(0).Rows.Count > 0 Then
                    tblMaklumatAnak.DataSource = ds
                    tblMaklumatAnak.DataBind()
                    Debug.WriteLine("Success: maklumatAnak")
                End If
            Catch ex As Exception
                Debug.WriteLine("Error(maklumatAnak):" & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Private Sub maklumatCadanganKuarters()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim ds As New DataSet
            Dim da As New SqlDataAdapter("SELECT 
             A.cadanganKuarters_id,
             A.permohonan_id,
             A.pengguna_id,
             A.kuarters_dicadang,
             C.pangkalan_nama,
             B.kuarters_nama,
             B.kuarters_alamat + ',' + B.kuarters_poskod + ',' + B.kuarters_negeri AS 'kuarters_alamat'
            FROM 
             admin.spk_cadanganKuarters A
             JOIN admin.spk_kuarters B ON B.kuarters_id = A.kuarters_dicadang
             JOIN dbo.spk_pangkalan C ON C.pangkalan_id = B.pangkalan_id
            WHERE
             A.permohonan_id = " & permohonanID & ";",
            conn)
            Try
                conn.Open()
                da.Fill(ds, "AnyTable")
                If ds.Tables(0).Rows.Count > 0 Then
                    gvSenaraiKuarters.DataSource = ds
                    gvSenaraiKuarters.DataBind()
                    Debug.WriteLine("Success: maklumatCadanganKuarters")
                End If
            Catch ex As Exception
                Debug.WriteLine("Error(maklumatCadangantKuartes): " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Private Sub gvSenaraiKuarters_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvSenaraiKuarters.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvSenaraiKuarters, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Klik untuk pilih kuarters."
        End If
    End Sub

    Private Sub tblCadanganKuarters_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvSenaraiKuarters.SelectedIndexChanged
        For Each row As GridViewRow In gvSenaraiKuarters.Rows
            If row.RowIndex = gvSenaraiKuarters.SelectedIndex Then
                row.BackColor = ColorTranslator.FromHtml(" #e9f83c")
                Debug.WriteLine("Selected DataKeyValue: " & Integer.Parse(gvSenaraiKuarters.DataKeys(row.RowIndex).Value))
                Debug.WriteLine("Selected Index: " & row.RowIndex)
                'SaveFunction.Disabled = False
                row.ToolTip = String.Empty
            Else
                row.BackColor = ColorTranslator.FromHtml("#FFFFFF")
                row.ToolTip = "Klik row untuk pilih kuarters."
            End If
        Next
    End Sub

    Private Function checkCadanganKuarters() As Boolean
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("SELECT count(*) FROM spk_cadanganKuarters WHERE permohonan_id = " & permohonanID & ";", conn)
            Try
                conn.Open()
                Dim count As Integer = cmd.ExecuteScalar
                If count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Debug.WriteLine("Error(checkCadanganKuarters): " & ex.Message)
                Return False
            Finally
                conn.Close()
            End Try
        End Using
    End Function

    Private Sub updateCadanganKuartersStatus()
        Dim setRef As String = ""
        Dim query As String = "UPDATE 
	        spk_cadanganKuarters
        SET	
	        status_dicadang = 'DIRUJUK'
        WHERE
	        pengguna_id = " & pID.Value & " AND permohonan_id = " & Request.QueryString("permohonan") & ";"
        setRef = oCommon.ExecuteSQL(query)
        If setRef = "0" Then
            Debug.WriteLine("updateCadanganKuartersStatus")
        Else
            Debug.WriteLine("Error(updateCadanganKuartersStatus): Failed to update.")
        End If
    End Sub

    Private Sub btnBatalPermohonan_Click(sender As Object, e As EventArgs) Handles btnBatalPermohonan.Click
        Dim query = "UPDATE spk_permohonan SET permohonan_status = 'PERMOHONAN DITOLAK' , permohonan_sub_status = 'DIBATAL' , permohonan_nota = @sebab , permohonan_tarikh = '" & Date.Now.ToString("dd/MM/yy") & "' WHERE permohonan_id = @permohonanID; 
INSERT INTO spk_logPermohonan(pengguna_id, permohonan_id, log_tarikh, log_status) VALUES (" & pID.Value & ", @permohonanID, '" & Date.Now & "', 'PERMOHONAN DITOLAK')"
        If tbSebabBatal.Text.Length > 0 Then
            Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using cmd As New SqlCommand(query)
                    cmd.Connection = conn
                    cmd.Parameters.Add("@sebab", SqlDbType.NVarChar, 50).Value = tbSebabBatal.Text
                    cmd.Parameters.Add("@permohonanID", SqlDbType.Int).Value = Request.QueryString("permohonan")
                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        Debug.WriteLine("Error(btnBatalPermohonan): " & ex.Message)
                    Finally
                        conn.Close()
                        newNotifikasi(Request.QueryString("permohonan"), "ADMIN", 35)
                        Load_Page()
                    End Try
                End Using
            End Using
        End If
    End Sub

    Private Sub btnTerimaTawaran_Click(sender As Object, e As EventArgs) Handles btnTerimaTawaran.Click
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("UPDATE spk_permohonan SET permohonan_status='PERMOHONAN DITERIMA', permohonan_sub_status = 'TERIMA TAWARAN UNIT' WHERE permohonan_id = @permohonanID;
                UPDATE spk_unit SET unit_status='Occupied' WHERE unit_id = @unitID
            ")
                cmd.Connection = conn
                cmd.Parameters.Add("@permohonanID", SqlDbType.Int).Value = Request.QueryString("permohonan")
                cmd.Parameters.Add("@unitID", SqlDbType.Int).Value = hfUnitID.Value
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Debug.WriteLine("Error(btnTerimaTawaran): " & ex.Message)
                Finally
                    conn.Close()
                    Load_Page()
                End Try
            End Using
        End Using
    End Sub

    Private Sub btnTolakTawaran_Click(sender As Object, e As EventArgs) Handles btnTolakTawaran.Click
        Dim query = "
        UPDATE spk_permohonan SET permohonan_status = 'PERMOHONAN DITOLAK' , permohonan_sub_status = 'TOLAK TAWARAN UNIT' , permohonan_nota = @sebab , permohonan_tarikh = '" & Date.Now.ToString("dd/MM/yy") & "' WHERE permohonan_id = @permohonanID; 
        INSERT INTO spk_logPermohonan(pengguna_id, permohonan_id, log_tarikh, log_status) VALUES (" & pID.Value & ", @permohonanID, '" & Date.Now & "', 'PERMOHONAN DITOLAK');
        UPDATE spk_unit SET unit_status='Available' WHERE unit_id = @unitID"
        If tbSebabBatal.Text.Length > 0 Then
            Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using cmd As New SqlCommand(query)
                    cmd.Connection = conn
                    cmd.Parameters.Add("@sebab", SqlDbType.NVarChar, 50).Value = tbSebabTolak.Text
                    cmd.Parameters.Add("@permohonanID", SqlDbType.Int).Value = Request.QueryString("permohonan")
                    cmd.Parameters.Add("@unitID", SqlDbType.Int).Value = hfUnitID.Value
                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        Debug.WriteLine("Error(btnBatalPermohonan): " & ex.Message)
                    Finally
                        conn.Close()
                        newNotifikasi(Request.QueryString("permohonan"), "ADMIN", 35)
                        Load_Page()
                    End Try
                End Using
            End Using
        End If
    End Sub

    Private Sub lbPermohonanBaru_Click(sender As Object, e As EventArgs) Handles lbPermohonanBaru.Click
        Response.Redirect("Permohonan.Kuarters.aspx?p=Permohonan%20Kuarters")
    End Sub

    Private Sub btnTerimaCadangan_Click(sender As Object, e As EventArgs) Handles btnTerimaCadangan.Click
        Dim idKuartersDiplih As Integer
        Dim setRef As String = ""
        Dim query = "UPDATE spk_permohonan SET kuarters_id = {0}, permohonan_sub_status = 'TERIMA CADANGAN KUARTERS' WHERE permohonan_id = {1};"
        For Each row As GridViewRow In gvSenaraiKuarters.Rows
            If row.RowIndex = gvSenaraiKuarters.SelectedIndex Then
                idKuartersDiplih = Integer.Parse(gvSenaraiKuarters.DataKeys(row.RowIndex).Value)
            End If
        Next

        If idKuartersDiplih = Nothing Then
            MsgTop.Attributes("class") = "errorMsg"
            strlbl_top.Text = "Sila pilih SATU kuarters untuk meneruskan proses permohonan."
            MsgBottom.Attributes("class") = "errorMsg"
            strlbl_bottom.Text = "Sila pilih SATU kuarters untuk meneruskan proses permohonan."
        ElseIf idKuartersDiplih > 0 Then
            setRef = oCommon.ExecuteSQL(String.Format(query, idKuartersDiplih, Request.QueryString("permohonan")))
            If setRef = "0" Then
                If setRef = "0" Then
                    MsgTop.Attributes("class") = "successMsg"
                    strlbl_top.Text = "Pemilihan kuarters berjaya. Pemohonan anda diprosess"
                    MsgBottom.Attributes("class") = "successMsg"
                    strlbl_bottom.Text = "Pemilihan kuarters bejaya. Pemohonan anda diprosess"
                    bgCadangnaKuarters.Visible = False
                    updateCadanganKuartersStatus()
                    newNotifikasi(Request.QueryString("permohonan"), "ADMIN", 30)
                    Load_Page()
                Else
                    MsgTop.Attributes("class") = "errorMsg"
                    strlbl_top.Text = strSaveFailAlert
                    MsgBottom.Attributes("class") = "errorMsg"
                    strlbl_bottom.Text = strSaveFailAlert
                    Debug.WriteLine("Error(SaveFunction_ServerClick):" & setRef)
                End If
            Else
                MsgTop.Attributes("class") = "errorMsg"
                strlbl_top.Text = strSaveFailAlert
                MsgBottom.Attributes("class") = "errorMsg"
                strlbl_bottom.Text = strSaveFailAlert
                Debug.WriteLine("Error(SaveFunction_ServerClick):" & setRef)
            End If
        End If
    End Sub

    Private Sub btnTolakCadangan_Click(sender As Object, e As EventArgs) Handles btnTolakCadangan.Click
        Dim query = "
        UPDATE spk_permohonan SET permohonan_status = 'PERMOHONAN DITOLAK' , permohonan_sub_status = 'TOLAK TAWARAN UNIT' , permohonan_nota = @sebab , permohonan_tarikh = '" & Date.Now.ToString("dd/MM/yy") & "' WHERE permohonan_id = @permohonanID; 
        INSERT INTO spk_logPermohonan(pengguna_id, permohonan_id, log_tarikh, log_status) VALUES (" & pID.Value & ", @permohonanID, '" & Date.Now & "', 'PERMOHONAN DITOLAK');"
        If tbSebabBatal.Text.Length > 0 Then
            Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using cmd As New SqlCommand(query)
                    cmd.Connection = conn
                    cmd.Parameters.Add("@sebab", SqlDbType.NVarChar, 50).Value = tbTolakCadanganKuarters.Text
                    cmd.Parameters.Add("@permohonanID", SqlDbType.Int).Value = Request.QueryString("permohonan")
                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        Debug.WriteLine("Error(btnBatalPermohonan): " & ex.Message)
                    Finally
                        conn.Close()
                        newNotifikasi(Request.QueryString("permohonan"), "ADMIN", 35)
                        Load_Page()
                    End Try
                End Using
            End Using
        End If
    End Sub

    Protected Function newNotifikasi(ByVal permohonanID As Integer, ByVal untuk As String, ByVal kumpulan As Integer) As Boolean
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("INSERT INTO 
                spk_notifikasi(
                    permohonan_id
                    , pengguna_id
                    , notifikasi_untuk
                    , notifikasi_kumpulan
                    , notifikasi_tarikh
                ) 
                VALUES(
                    @permohonanID
                    , @penggunaID
                    , @untuk
                    , @kumpulan
                    , @tarikh);"
                )
                cmd.Connection = conn
                cmd.Parameters.Add("@permohonanID", SqlDbType.Int).Value = permohonanID
                cmd.Parameters.Add("@penggunaID", SqlDbType.Int).Value = pID.Value
                cmd.Parameters.Add("@untuk", SqlDbType.NVarChar, 50).Value = untuk
                cmd.Parameters.Add("@kumpulan", SqlDbType.Int).Value = kumpulan
                cmd.Parameters.Add("@tarikh", SqlDbType.NVarChar, 50).Value = Date.Now
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Debug.WriteLine("Error(newNotifikasi): " & ex.Message)
                    Return False
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Function

    Private Sub update_notifikasi(ByVal notifikasiID As Integer)
        If notifikasiID > 0 Then
            Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using cmd As New SqlCommand("UPDATE spk_notifikasi SET notifikasi_checked = 1 WHERE permohonan_id = @notifikasiID")
                    cmd.Connection = conn
                    cmd.Parameters.Add("@notifikasiID", SqlDbType.Int).Value = notifikasiID
                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        Debug.WriteLine("Error(update_notikasi): " & ex.Message)
                    Finally
                        conn.Close()
                    End Try
                End Using
            End Using
        End If
    End Sub
End Class