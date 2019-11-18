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

    Dim countAnak As Integer = 0
    Dim dataAnak As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            loadUser()
            readMaklumatAnak()

        Catch ex As Exception

        End Try
    End Sub

    'Private Sub data_poinLoad()
    '    Dim strSQL2 As String = ""
    '    Dim strSQL3 As String = ""
    '    Dim dataJumlah As Integer = ""
    '    Dim dataPangkatPoin As Integer = ""
    '    Dim dataUmurAnak As Integer = ""
    '    Dim jumlah_anak As Integer = ""

    '    strSQL = "select count(anak_nama) from spk_anak where anak_umur <= 18"
    '    strSQL2 = "select B.pangkat_mata from spk_pengguna A left join spk_pangkat B on A.pangkat_id = B.pangkat_id"

    '    Dim jumlah_anakUmur18 = oCommon.ExecuteSQL(strSQL)
    '    Dim jumlah_poinPangkat = oCommon.ExecuteSQL(strSQL2)


    '    Dim jumlah_mataTerkumpul = jumlah_poinPangkat + (jumlah_anakUmur18 * 5)

    '    lblpoinDisplay.Text = jumlah_mataTerkumpul.ToString


    'End Sub

    Private Sub loadUser()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("SELECT TOP 1
	            A.pengguna_id as pengguna_id,
	            A.pengguna_nama as pengguna_nama,
	            A.pengguna_mykad as pengguna_mykad,
	            A.pengguna_jantina as pengguna_jantina,
	            A.pengguna_tarikh_lahir as pengguna_tarikh_lahir,
                A.pengguna_mula_perkhidmatan as pengguna_mula_perkhidmatan,
                A.pengguna_tamat_perkhidmatan as pengguna_tamat_perkhidmatan,
                A.pengguna_no_tentera as pengguna_no_tentera,
	            B.pangkat_id as pangkat_id,
	            B.pangkat_nama as pangkat_nama,
                C.pangkalan_nama as pangkalan_nama,
                E.kuarters_nama as kuarters_nama,
				G.keluarga_tempat_tinggal as keluarga_tempat_tinggal,
				G.keluarga_tarikh_mula as keluarga_tarikh_mula,
				G.keluarga_anak as keluarga_anak,
				D.permohonan_mata as permohonan_mata
            FROM 
	            admin.spk_pengguna A
	            JOIN admin.spk_pangkat B ON A.pangkat_id = B.pangkat_id
	            JOIN dbo.spk_pangkalan C ON A.pangkalan_id = C.pangkalan_id
				JOIN spk_permohonan D on A.pengguna_id = D.pengguna_id 
				JOIN spk_kuarters E on D.kuarters_id = E.kuarters_id
				JOIN spk_keluarga G on A.pengguna_id = G.pengguna_id
				JOIN spk_anak F on A.pengguna_id = F.pengguna_id	
				
            WHERE D.permohonan_id = '" & Request.QueryString("uid") & "' ",
            conn)

            Try
                conn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                If reader.HasRows Then
                    If reader.Read() Then
                        pengguna_id.Value = reader("pengguna_id")
                        lblNama.InnerText = reader("pengguna_nama")
                        lblTarikhLahir.InnerText = reader("pengguna_tarikh_lahir")
                        lblJantina.InnerText = reader("pengguna_jantina")
                        lblJawatan.InnerText = reader("pangkat_nama")
                        lblNoTentera.InnerText = reader("pengguna_no_tentera")
                        lblTarikhMulaBerkhidmat.InnerText = reader("pengguna_mula_perkhidmatan")
                        lbl_senaraiPangkalan.InnerText = reader("pangkalan_nama")
                        lbl_senaraiKuarters.InnerText = reader("kuarters_nama")
                        lblJenisPenempatan.Text = reader("keluarga_tempat_tinggal")
                        lbltarikhPenempatan.Text = reader("keluarga_tarikh_mula")
                        lbl_poinDisplay.InnerText = reader("permohonan_mata")
                        lbl_senaraiPangkalan.InnerText = reader("pangkalan_nama")

                        '-------------------
                        'If reader.IsDBNull("pengguna_tamat_perkhidmatan") Then
                        '    lblTarikhAkhirBerkhidmat.InnerText = "Masih Berkhidmat"
                        'Else
                        '    lblTarikhAkhirBerkhidmat.InnerText = reader("pengguna_tamat_perkhidmatan")
                        'End If
                        '-------------------

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
        Dim penggunaID = pengguna_id.Value
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim table As DataTable = New DataTable
            Dim da As New SqlDataAdapter(
                    "SELECT 
                        anak_id,
                        pengguna_id,
                        anak_nama,
                        anak_ic,
                        anak_umur
                        FROM spk_anak
                        WHERE pengguna_id = " & penggunaID & ";",
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


    Protected Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Try

            If (e.CommandName = "Approve") Then
                Dim strCID = e.CommandArgument.ToString

                strSQL = "UPDATE spk_permohonan SET permohonan_status = 'PERMOHONAN SEDANG DIPROSES' WHERE permohonan_id = '" & oCommon.FixSingleQuotes(strCID) & "'"
                oCommon.ExecuteSQL(strSQL)
            ElseIf (e.CommandName = "Reject") Then
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
    Private Sub poin_load()

    End Sub
End Class