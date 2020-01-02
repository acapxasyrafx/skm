Imports System.Data.SqlClient

Public Class maklumat_pemohon_ditolak
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
    Dim dataAnak As New DataSet
    Dim countAnak As Integer = 0

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
        update_notifikasi()
    End Sub

    Private Sub loadUser()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("
            SELECT
                A.permohonan_id
                , D.pengguna_id
	            , D.pengguna_nama
	            , D.pengguna_jantina
	            , D.pengguna_tarikh_lahir
	            , F.pangkat_nama
	            , D.pengguna_no_tentera
                , G.historyPengguna_penggunaNoTentera
                , G.historyPengguna_statusPerkahwinan
	            , D.pengguna_mula_perkhidmatan
	            , D.pengguna_tamat_perkhidmatan
	            , A.permohonan_no_permohonan
	            , A.kuarters_id
	            , B.kuarters_nama
	            , C.pangkalan_nama
	            , A.permohonan_tarikh
	            , A.permohonan_status
	            , A.permohonan_sub_status
                , A.permohonan_nota
	            , A.permohonan_mata
				, E.historyKeluarga_tempat_tinggal
                , G.historyPengguna_mulaBerkhidmat
                , G.historyPengguna_tamatBerkhidmat
                , E.historyKeluarga_tarikh_mula
            FROM 
	            spk_permohonan A
	            JOIN spk_kuarters B ON B.kuarters_id = A.kuarters_id
	            JOIN spk_pangkalan C ON C.pangkalan_id = B.pangkalan_id
                JOIN spk_historyPengguna G ON G.permohonan_id = A.permohonan_id
	            JOIN spk_pengguna D ON D.pengguna_id = G.pengguna_id
	            JOIN spk_historyKeluarga E ON E.permohonan_id = A.permohonan_id
	            JOIN spk_pangkat F ON F.pangkat_id = G.pangkat_id
            WHERE
                A.permohonan_id = " & Request.QueryString("uid") & ";",
            conn)

            Try
                conn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                If reader.HasRows Then
                    While reader.Read()
                        pID.Value = reader("pengguna_id")
                        lblNama.InnerText = reader("pengguna_nama")
                        lblTarikhLahir.InnerText = reader("pengguna_tarikh_lahir")
                        lblJantina.InnerText = reader("pengguna_jantina")
                        lblJawatan.InnerText = reader("pangkat_nama")
                        lblNoTentera.InnerText = reader("historyPengguna_penggunaNoTentera")
                        lblTarikhMulaBerkhidmat.InnerText = reader("historyPengguna_mulaBerkhidmat")
                        lblsebabTolak.Text = reader("permohonan_nota")
                        lblTarikhAkhirBerkhidmat.InnerText = reader("historyPengguna_tamatBerkhidmat")
                        lblKuartersDimohon.InnerText = reader("kuarters_nama")
                        lblPangkalanDimohon.InnerText = reader("pangkalan_nama")
                        lblJenisPenempatan.InnerHtml = reader("historyKeluarga_tempat_tinggal")
                        lbltarikhPenempatan.InnerHtml = reader("historyKeluarga_tarikh_mula")
                        lblStatusPerkahwinan.InnerText = reader("historyPengguna_statusPerkahwinan")
                    End While
                Else
                    Debug.Write("ERROR(loadUser-makluamt_pemohon_ditolak:105): NO ROWS")
                End If
            Catch ex As Exception
                Debug.WriteLine("ERROR(loadUser-makluamt_pemohon_ditolak:108): " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Private Function readMaklumatAnak() As Boolean
        Dim penggunaID = pID.Value
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim table As DataTable = New DataTable
            Dim da As New SqlDataAdapter(
                    "SELECT
                        * 
                    FROM
                        spk_historyAnak
                    WHERE
                        permohonan_id = " & Request.QueryString("uid") & ";",
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
                Debug.WriteLine("ERROR(readMaklumatAnak-makluamt_pemohon_ditolak:139): " & ex.Message)
                Return False
            Finally
                conn.Close()
            End Try
        End Using
    End Function

    Protected Sub update_notifikasi()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("UPDATE spk_notifikasi SET notifikasi_checked = 1 WHERE permohonan_id = @permohonanID AND notifikasi_untuk='ADMIN'")
                cmd.Connection = conn
                cmd.Parameters.Add("@permohonanID", SqlDbType.Int).Value = Request.QueryString("uid")
                Try
                    conn.Open()
                    cmd.ExecuteScalar()
                Catch ex As Exception
                    Debug.WriteLine("Error(updateNotifikasi-maklumat_pemohon_ditolak:156): " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Protected Function icToAge(ByVal ic As String) As Integer
        Dim year = ic.Substring(0, 2)
        Dim month = ic.Substring(2, 2)
        Dim day = ic.Substring(4, 2)
        Dim dob_string = day & "/" & month & "/" & year
        Dim dob_date = Convert.ToDateTime(dob_string)
        Dim age = Date.Now().Year - dob_date.Year
        Debug.WriteLine("icToAge: " & dob_string & "|Age: " & age & "")
        Return age
    End Function
End Class