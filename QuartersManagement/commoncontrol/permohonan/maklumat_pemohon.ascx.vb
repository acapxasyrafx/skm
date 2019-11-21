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

    Dim countAnak As Integer = 0
    Dim dataAnak As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            loadUser()
            readMaklumatAnak()
            loadPengiraanMata()
        End If
    End Sub

    Private Sub loadUser()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("SELECT
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
                    Do While reader.Read()
                        pengguna_id.Value = reader("pengguna_id")
                        lblNama.InnerText = reader("pengguna_nama")
                        lblTarikhLahir.InnerText = reader("pengguna_tarikh_lahir")
                        lblJantina.InnerText = reader("pengguna_jantina")
                        lblJawatan.InnerText = reader("pangkat_nama")
                        lblNoTentera.InnerText = reader("pengguna_no_tentera")
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
                Debug.WriteLine("ERROR(readMaklumatAnak): " & ex.Message)
                Return False
            Finally
                conn.Close()
            End Try
        End Using
    End Function


    Protected Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Try

            If (e.CommandName = "Approved") Then
                Dim strCID = e.CommandArgument.ToString

                strSQL = "UPDATE spk_permohonan SET permohonan_status = 'PERMOHONAN SEDANG DIPROSES' WHERE permohonan_id = '" & oCommon.FixSingleQuotes(strCID) & "'"
                oCommon.ExecuteSQL(strSQL)
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
        Debug.WriteLine("icToAge: " & dob_string & "|Age: " & age & "")
        Return age
    End Function

    Private Sub poin_load()

    End Sub

    Private Sub loadPengiraanMata()
        Dim dt As New DataTable()
        dt.Columns.AddRange(New DataColumn(3) {New DataColumn("itemColumn"), New DataColumn("ItemPoint"), New DataColumn("itemCount"), New DataColumn("itemTotal")})
        dt.Rows.Add("Pangkat", "11", "-", "11")
        dt.Rows.Add("Anak Bawah 18 tahun", "5", "1", "5")
        tblPengiraanMata.DataSource = dt
        tblPengiraanMata.DataBind()
    End Sub

    Private Sub tblPengiraanMata_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles tblPengiraanMata.RowDataBound

    End Sub
End Class