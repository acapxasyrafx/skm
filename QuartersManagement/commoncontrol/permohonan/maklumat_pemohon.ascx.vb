Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Public Class maklumat_pemohon
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

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

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim penggunaID As Integer
    Dim countAnak As Integer = 0
    Dim dataAnak As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("user_id") Then
                penggunaID = Session("user_id")
                load_page()
            Else
                Response.Redirect("/")
            End If
        End If
    End Sub

    Protected Sub load_page()
        loadUser()
        readMaklumatAnak()
        loadMaklumatMata()
        update_notifikasi()
    End Sub

    Private Sub loadUser()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("SELECT
	            A.permohonan_id
	            , D.pengguna_id
	            , G.pengguna_nama
	            , G.pengguna_jantina
	            , G.pengguna_tarikh_lahir
	            ,D.historyPengguna_statusPerkahwinan
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
	            , E.historyKeluarga_tempat_tinggal
	            , E.historyKeluarga_tarikh_mula
	            , E.historyKeluarga_tarikh_akhir
            FROM 
	            spk_permohonan A
	            JOIN spk_kuarters B ON B.kuarters_id = A.kuarters_id
	            JOIN spk_pangkalan C ON C.pangkalan_id = B.pangkalan_id
	            JOIN spk_historyPengguna D ON D.permohonan_id = A.permohonan_id
	            JOIN spk_historyKeluarga E ON E.permohonan_id = A.permohonan_id
	            JOIN spk_pangkat F ON F.pangkat_id = D.pangkat_id
	            JOIN spk_pengguna G ON G.pengguna_id = D.pengguna_id
            WHERE
                A.permohonan_id =" & Request.QueryString("uid") & ";",
            conn)

            Try
                conn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                If reader.HasRows Then
                    Do While reader.Read()
                        pID.Value = reader("pengguna_id")
                        lblNama.InnerText = reader("pengguna_nama")
                        lblTarikhLahir.InnerText = reader("pengguna_tarikh_lahir")
                        lblJantina.InnerText = reader("pengguna_jantina")
                        lblStatusPerkahwinan.InnerText = reader("historyPengguna_statusPerkahwinan")
                        lblJawatan.InnerText = reader("pangkat_nama")
                        lblNoTentera.InnerText = reader("historyPengguna_penggunaNoTentera")
                        lblTarikhMulaBerkhidmat.InnerText = reader("pengguna_mula_perkhidmatan")
                        lbl_senaraiPangkalan.InnerText = reader("pangkalan_nama")
                        lbl_senaraiKuarters.InnerText = reader("kuarters_nama")
                        lblJenisPenempatan.Text = reader("historyKeluarga_tempat_tinggal")
                        lbltarikhPenempatan.Text = reader("historyKeluarga_tarikh_mula")
                        'lbl_poinDisplay.InnerText = reader("permohonan_mata")
                        lbl_senaraiPangkalan.InnerText = reader("pangkalan_nama")
                        lblTarikhAkhirBerkhidmat.InnerText = reader("pengguna_tamat_perkhidmatan")
                    Loop
                Else
                    Debug.WriteLine("Error(loadUser-makluamt_pemohon:104): NO ROWS")
                End If
            Catch ex As Exception
                Debug.WriteLine("ERROR(loadUser-makluamt_pemohon:107): " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub
    Private Function readMaklumatAnak() As Boolean
        Dim penggunaID = pID.Value
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim table As DataTable = New DataTable
            Dim da As New SqlDataAdapter("
                    SELECT 
                        * 
                    FROM 
                        spk_historyAnak 
                    WHERE permohonan_id = " & Request.QueryString("uid") & ";",
                    conn)
            Try
                conn.Open()
                da.Fill(dataAnak, "AnyTable")
                Dim nRows As Integer = 0
                Dim nCount As Integer = 1
                countAnak = dataAnak.Tables(0).Rows.Count
                If dataAnak.Tables(0).Rows.Count > 0 Then
                    datRespondent.DataSource = dataAnak
                    datRespondent.DataBind()
                End If
                Return True
            Catch ex As Exception
                Debug.WriteLine("ERROR(readMaklumatAnak-makluamt_pemohon:136): " & ex.Message)
                Return False
            Finally
                conn.Close()
            End Try
        End Using
    End Function


    Protected Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Dim query As String = ""
        Try
            query += String.Format("UPDATE spk_permohonan SET permohonan_tarikh = '{0}',permohonan_status 'PERMOHONAN MENUNGGU' WHERE permohonan_id = {1};", Date.Now().ToString("dd/MM/yyyy"), Request.QueryString("uid"))
            query += String.Format("INSERT INTO spk_logPermohonan(pengguna_id, permohonan_id, log_tarikh, log_status) VALUES({0},{1},{2},{3},'PERMOHONAN MENUNGGU');", Integer.Parse(pID.Value), Request.QueryString("uid"), Date.Now().ToString("dd/MM/yyyy"))
            If (e.CommandName = "Approved") Then
                Dim strCID = e.CommandArgument.ToString
                oCommon.ExecuteSQL(query)
            ElseIf (e.CommandName = "Rejected") Then
                Dim strCID = e.CommandArgument.ToString

                'chk session to prevent postback
                strSQL = "UPDATE spk_permohonan SET permohonan_status = 'PERMOHONAN ANDA DITOLAK' WHERE permohonan_id = '" & oCommon.FixSingleQuotes(strCID) & "'"
                oCommon.ExecuteSQL(strSQL)

                oCommon.ExecuteSQL("UPDATE spk_permohonan SET permohonan_nota = '" & hdnUserInput.Value.ToString & "' WHERE permohonan_id = '" & oCommon.FixSingleQuotes(strCID) & "'")

            End If

        Catch ex As Exception
            MsgBottom.Attributes("class") = "errorMsg"
            strlbl_bottom.Text = strSysErrorAlert & "<br>" & ex.Message
        End Try
    End Sub

    Protected Function icToAge(ByVal ic As String) As Integer
        Dim year = ic.Substring(0, 2)
        Dim month = ic.Substring(2, 2)
        Dim day = ic.Substring(4, 2)
        Dim dob_string = day & "/" & month & "/" & year
        Dim dob_date = Convert.ToDateTime(dob_string)
        Dim age = Date.Now().Year - dob_date.Year
        Return age
    End Function

    Protected Sub loadMaklumatMata()
        Dim jumlahPoint As Integer = 0
        Dim jumlahAnakLayak As Integer = 0
        Dim mataPangkat As Integer = 0
        Dim mataAnak As Integer = 0
        Dim mataLayak As Integer = 0
        Dim statusLayak As String

        mataAnak = Integer.Parse(oCommon.getFieldValue("SELECT config_parameter FROM general_config WHERE config_type = 'MATAANAK'"))
        mataPangkat = Integer.Parse(oCommon.getFieldValue("SELECT C.pangkat_mata FROM spk_permohonan A LEFT JOIN spk_pengguna B ON B.pengguna_id = A.pengguna_id LEFT JOIN spk_pangkat C ON C.pangkat_id = B.pangkat_id WHERE A.permohonan_id = " & Request.QueryString("uid")))
        mataLayak = Integer.Parse(oCommon.getFieldValue("SELECT config_parameter FROM general_config WHERE config_type = 'MATALULUS'"))

        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("SELECT historyAnak_ic FROM spk_historyAnak WHERE permohonan_id = @permohonanID")
                Try
                    conn.Open()
                    cmd.Connection = conn
                    cmd.Parameters.Add("@permohonanID", SqlDbType.Int).Value = Request.QueryString("uid")
                    Dim dr As SqlDataReader
                    dr = cmd.ExecuteReader
                    If dr.HasRows Then
                        While dr.Read
                            If icToAge(dr("historyAnak_ic")) < 18 Then
                                jumlahAnakLayak += 1
                            Else
                                Continue While
                            End If
                        End While
                    Else
                        jumlahAnakLayak = 0
                    End If
                Catch ex As Exception
                    Debug.WriteLine("Error(loadMakluamtMata-makluamt_pemohon:212): " & ex.Message)
                Finally
                    conn.Close()
                End Try
            End Using
        End Using

        If jumlahAnakLayak > 4 Then
            jumlahAnakLayak = 4
        End If
        If jumlahPoint >= 30 Then
            statusLayak = "LAYAK"
        Else
            statusLayak = "TIDAK LAYAK"
        End If
        jumlahPoint = mataPangkat + (jumlahAnakLayak * mataAnak)

        lblMataPangkat.Text = mataPangkat
        lblJumlahMatapangkat.Text = mataPangkat
        lblMataAnak.Text = mataAnak
        lblJumlahAnakLayak.Text = jumlahAnakLayak
        lblJumlahMataAnak.Text = mataAnak * jumlahAnakLayak
        lblJumlahMata.Text = jumlahPoint
        lblStatusKelayakan.Text = statusLayak
    End Sub

    Private Sub btnImg_lulus_Click(sender As Object, e As ImageClickEventArgs) Handles btnImg_lulus.Click
        confirmModal.Style.Add("display", "block")
    End Sub

    Private Sub btnImg_ditolak_Click(sender As Object, e As ImageClickEventArgs) Handles btnImg_ditolak.Click
        dialogModal.Style.Add("display", "block")
    End Sub

    Private Sub btnTutupModal_Click(sender As Object, e As EventArgs) Handles btnTutupModal1.Click
        dialogModal.Style.Add("display", "none")
    End Sub

    Private Sub btnTutupModal2_Click(sender As Object, e As EventArgs) Handles btnTutupModal2.Click
        confirmModal.Style.Add("display", "none")
    End Sub

    Private Sub closeBtn_ServerClick(sender As Object, e As EventArgs) Handles closeBtn.ServerClick
        dialogModal.Style.Add("display", "none")
    End Sub

    Private Sub btnTolakPermohonan_Click(sender As Object, e As EventArgs) Handles btnTolakPermohonan.Click
        Dim query = "UPDATE spk_permohonan SET permohonan_tarikh = @tarikh, permohonan_status = @status, permohonan_nota = @permohonanNota WHERE permohonan_id = @permohonanID;"
        query += "INSERT INTO spkLogPermohonan(pengguna_id, permohonan_id, log_tarikh, log_status) VALUES (@penggunaID, @permohonanID, @tarikh, @status);"
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand(query)
                cmd.Connection = conn
                cmd.Parameters.Add("@tarikh", SqlDbType.NVarChar, 50).Value = Date.Now.ToString("dd/MM/yyyy")
                cmd.Parameters.Add("@status", SqlDbType.NVarChar, 50).Value = "PERMOHONAN DITOLAK"
                cmd.Parameters.Add("@permohonanID", SqlDbType.Int).Value = Request.QueryString("uid")
                cmd.Parameters.Add("@penggunaID", SqlDbType.Int).Value = pID.Value
                cmd.Parameters.Add("@permohonanNota", SqlDbType.NVarChar, 50).Value = txtNota.Text
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Debug.WriteLine("Error(btnTolakTawaran-maklumat_permohonan:272): " & ex.Message)
                Finally
                    conn.Close()
                    newNotifikasi("USER", 36)
                    Response.Redirect("Senarai.Permohonan.Baru.aspx?P=Pengurusan%20Pentadbiran%20>%20Senarai%20Permohonan%20>%20Senarai%20Permohonan%20Baru")
                End Try
            End Using
        End Using
    End Sub

    Private Sub btnTerimaTawaran_Click(sender As Object, e As EventArgs) Handles btnTerimaTawaran.Click
        Dim query = "UPDATE spk_permohonan SET permohonan_tarikh=@tarikh, permohonan_status=@status WHERE permohonan_id=@permohonanID;"
        query += "INSERT INTO spk_logPermohonan(pengguna_id, permohonan_id, log_tarikh, log_status) VALUES (@penggunaID, @permohonanID, @tarikh, @status);"
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand(query)
                cmd.Connection = conn
                cmd.Parameters.Add("@tarikh", SqlDbType.NVarChar, 50).Value = Date.Now.ToString("dd/MM/yyyy")
                cmd.Parameters.Add("@status", SqlDbType.NVarChar, 50).Value = "PERMOHONAN MENUNGGU"
                cmd.Parameters.Add("@permohonanID", SqlDbType.Int).Value = Request.QueryString("uid")
                cmd.Parameters.Add("@penggunaID", SqlDbType.Int).Value = pID.Value
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Debug.WriteLine("Error(btnTerimaTawaran-maklumat_permohonan:301): " & ex.Message)
                Finally
                    conn.Close()
                    newNotifikasi("ADMIN", 30)
                    newNotifikasi("USER", 31)
                    Response.Redirect("Senarai.Permohonan.Baru.aspx?P=Pengurusan%20Pentadbiran%20>%20Senarai%20Permohonan%20>%20Senarai%20Permohonan%20Baru")
                End Try
            End Using
        End Using
    End Sub

    Private Sub Button1_ServerClick(sender As Object, e As EventArgs) Handles Button1.ServerClick
        confirmModal.Style.Add("display", "none")
    End Sub

    Protected Sub newNotifikasi(ByVal untuk As String, ByVal kumpulan As Integer)
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("INSERT INTO spk_notifikasi( permohonan_id , pengguna_id , notifikasi_untuk , notifikasi_kumpulan , notifikasi_tarikh ) 
                VALUES( @permohonanID , @penggunaID , @untuk , @kumpulan , @tarikh);")
                cmd.Connection = conn
                cmd.Parameters.Add("@permohonanID", SqlDbType.Int).Value = Request.QueryString("uid")
                cmd.Parameters.Add("@penggunaID", SqlDbType.Int).Value = pID.Value
                cmd.Parameters.Add("@untuk", SqlDbType.NVarChar, 50).Value = untuk
                cmd.Parameters.Add("@kumpulan", SqlDbType.Int).Value = kumpulan
                cmd.Parameters.Add("@tarikh", SqlDbType.NVarChar, 50).Value = Date.Now
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Debug.WriteLine("Error(newNotifikasi - maklumat_permohonan:351): " & ex.Message)
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Sub

    Protected Sub update_notifikasi()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("UPDATE spk_notifikasi SET notifikasi_checked = 1 WHERE permohonan_id = @permohonanID AND notifikasi_untuk='ADMIN'")
                cmd.Connection = conn
                cmd.Parameters.Add("@permohonanID", SqlDbType.Int).Value = Request.QueryString("uid")
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Debug.WriteLine("Error(updateNotifikasi-maklumat_pemohon:343): " & ex.Message)
                End Try
            End Using
        End Using
    End Sub
End Class