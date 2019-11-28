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

    Dim dataAnak As New DataSet
    Dim countAnak As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Try
            loadUser()
            readMaklumatAnak()
        Catch ex As Exception

        End Try
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
				, E.historyKeluarga_tarikh_mula
				, E.historyKeluarga_tarikh_akhir
            FROM 
	            spk_permohonan A
	            JOIN spk_kuarters B ON B.kuarters_id = A.kuarters_id
	            JOIN spk_pangkalan C ON C.pangkalan_id = B.pangkalan_id
	            JOIN spk_pengguna D ON D.pengguna_id = A.pengguna_id
	            JOIN spk_historyKeluarga E ON E.permohonan_id = A.permohonan_id
	            JOIN spk_pangkat F ON F.pangkat_id = D.pangkat_id
            WHERE
                A.permohonan_id = " & Request.QueryString("uid") & ";",
            conn)

            Try
                conn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                If reader.HasRows Then
                    If reader.Read() Then
                        pID.Value = reader("pengguna_id")
                        lblNama.InnerText = reader("pengguna_nama")
                        lblTarikhLahir.InnerText = reader("pengguna_tarikh_lahir")
                        lblJantina.InnerText = reader("pengguna_jantina")
                        lblJawatan.InnerText = reader("pangkat_nama")
                        lblNoTentera.InnerText = reader("pengguna_no_tentera")
                        lblTarikhMulaBerkhidmat.InnerText = reader("pengguna_mula_perkhidmatan")
                        lblsebabTolak.Text = reader("permohonan_nota")
                        lblTarikhAkhirBerkhidmat.InnerText = reader("pengguna_tamat_perkhidmatan")
                        lblKuartersDimohon.InnerText = reader("kuarters_nama")
                        lblPangkalanDimohon.InnerText = reader("pangkalan_nama")
                        lblJenisPenempatan.InnerHtml = reader("historyKeluarga_tempat_tinggal")
                        lbltarikhPenempatan.InnerHtml = reader("historyKeluarga_tarikh_mula")
                    Else
                        Debug.Write("CANNOT READ")
                    End If
                Else
                    Debug.Write("NO ROWS")
                End If
            Catch ex As Exception
                Debug.WriteLine("ERROR(loadUser): " & ex.Message)
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
                Debug.WriteLine("ERROR(readMaklumatAnak): " & ex.Message)
                Return False
            Finally
                conn.Close()
            End Try
        End Using
    End Function

End Class