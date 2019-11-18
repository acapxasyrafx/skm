Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
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

    Dim penggunaID As Integer = 1
    Dim pangkalanID As Integer = 0
    Dim permohonanID As Integer
    Dim statusPermohon As String = ""
    Dim subStatusPermohonan As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            permohonanID = Request.QueryString("permohonan")
            Load_Page()
        End If
    End Sub

    Private Sub Load_Page()
        maklumatPermohonan()
        maklumatAnak()
        maklumatStatusPermohonan()

        If statusPermohon.Equals("PERMOHONAN DITOLAK") Then
            mvStatusPermohonan.ActiveViewIndex = 3
        ElseIf statusPermohon.Equals("PERMOHONAN DITERIMA") Then
            mvStatusPermohonan.ActiveViewIndex = 2
        ElseIf statusPermohon.Equals("PERMOHONAN SEDANG DIPROSES") Then
            If subStatusPermohonan.Equals("LULUS TANPA KEKOSONGAN") Then
                Debug.WriteLine("User belum memilih kuarters dicadang")
                maklumatCadanganKuarters()
                mvStatusPermohonan.ActiveViewIndex = 1
            ElseIf subStatusPermohonan.Equals("TUNGGU KELULUSAN") Then
                Debug.WriteLine("User telah memilih kuarters, tunggu maklumbalas admin")
                mvStatusPermohonan.ActiveViewIndex = 0
            End If
        ElseIf statusPermohon.Equals("PERMOHONAN BARU") Then
            mvStatusPermohonan.ActiveViewIndex = 0
        Else
            Debug.WriteLine("Error(Load_Page): Status Permohonan UKNOWN")
        End If

    End Sub

    Private Sub maklumatPermohonan()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("
                SELECT 
	                  A.permohonan_id
	                , A.pemohonan_tarikh
	                , A.permohonan_status
                    , A.permohonan_sub_status
	                , B.pengguna_nama
	                , C.pangkat_nama
	                , D.kuarters_nama
	                , E.historyKeluarga_tempat_tinggal
	                , E.historyKeluarga_tarikh_mula
                FROM 
	                spk_permohonan A
	                JOIN spk_pengguna B ON B.pengguna_id = A.pengguna_id
	                JOIN spk_pangkat C ON C.pangkat_id = B.pangkat_id
	                JOIN spk_kuarters D ON D.kuarters_id = A.kuarters_id
	                JOIN spk_historyKeluarga E ON E.permohonan_id = A.permohonan_id
                WHERE 
	                A.permohonan_id = " & permohonanID & "
                ;
            ", conn)
            Try
                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader
                    If reader.HasRows Then
                        Do While reader.Read()
                            lblJenisTempatTinggal.Text = reader("historyKeluarga_tempat_tinggal")
                            lblTarikhMulaMenetap.Text = reader("historyKeluarga_tarikh_mula").ToString()
                            lblKuarterDipohon.Text = reader("kuarters_nama")
                            lblTarikhPermohonan.Text = reader("pemohonan_tarikh")
                            statusPermohon = reader("permohonan_status")
                            subStatusPermohonan = reader("permohonan_sub_status").ToString
                            Debug.WriteLine("Success: maklumatUser")
                        Loop
                    Else
                        Debug.WriteLine("Error(maklumatPermohonan): Reader has no row")
                    End If
                End Using
            Catch ex As Exception
                Debug.WriteLine("Error(maklumatPermohonan): " & ex.Message)
            Finally
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
                WHERE pengguna_id = " & penggunaID & " AND permohonan_id = " & permohonanID & ";", conn)
            Try
                conn.Open()
                reader = cmd.ExecuteReader
                If reader.HasRows() Then
                    Do While reader.Read()
                        Select Case reader("log_status")
                            Case "PERMOHONAN BARU"
                                Debug.WriteLine("Permohonan Status: " & reader("log_status"))
                                permohonanBaharu.Attributes("class") = "progress-done"
                                lblTarikhBaharu.Text = reader("log_tarikh")
                            Case "PERMOHONAN SEDANG DIPROSES"
                                Debug.WriteLine("Permohonan Status: " & reader("log_status"))
                                permohonanMenunggu.Attributes("class") = "progress-done"
                                lblTarikhMenuggu.Text = reader("log_tarikh")
                            Case "PERMOHONAN DITERIMA", "PERMOHONAN DITOLAK"
                                Debug.WriteLine("Permohonan Status: " & reader("log_status"))
                                permohonanKeputusan.Attributes("class") = "progress-done"
                                lblTarikhKeputusan.Text = reader("log_tarikh")
                            Case Else
                                Debug.WriteLine("ELSE")
                        End Select
                    Loop
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
                    tblCadanganKuarters.DataSource = ds
                    tblCadanganKuarters.DataBind()
                    Debug.WriteLine("Success: maklumatCadanganKuarters")
                End If
            Catch ex As Exception
                Debug.WriteLine("Error(maklumatCadangantKuartes): " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Private Sub tblCadanganKuarters_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles tblCadanganKuarters.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(tblCadanganKuarters, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Klik row untuk pilih kuarters."
        End If
    End Sub

    Private Sub tblCadanganKuarters_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tblCadanganKuarters.SelectedIndexChanged
        For Each row As GridViewRow In tblCadanganKuarters.Rows
            If row.RowIndex = tblCadanganKuarters.SelectedIndex Then
                row.BackColor = ColorTranslator.FromHtml(" #e9f83c")
                Debug.WriteLine("Selected DataKeyValue: " & Integer.Parse(tblCadanganKuarters.DataKeys(row.RowIndex).Value))
                Debug.WriteLine("Selected Index: " & row.RowIndex)
                SaveFunction.Disabled = False
                row.ToolTip = String.Empty
            Else
                row.BackColor = ColorTranslator.FromHtml("#FFFFFF")
                row.ToolTip = "Klik row untuk pilih kuarters."
            End If
        Next
    End Sub

    Private Sub SaveFunction_ServerClick(sender As Object, e As EventArgs) Handles SaveFunction.ServerClick
        Dim idKuartersDiplih As Integer
        Dim setRef As String = ""
        Dim query = "UPDATE spk_permohonan SET kuarters_id = {0}, permohonan_sub_status = 'TUNGGU KELULUSAN' WHERE permohonan_id = {1};"
        For Each row As GridViewRow In tblCadanganKuarters.Rows
            If row.RowIndex = tblCadanganKuarters.SelectedIndex Then
                idKuartersDiplih = Integer.Parse(tblCadanganKuarters.DataKeys(row.RowIndex).Value)
            End If
        Next

        If idKuartersDiplih = Nothing Then
            MsgTop.Attributes("class") = "errorMsg"
            strlbl_top.Text = "Sila pilih SATU kuarters untuk meneruskan proses permohonan."
            MsgBottom.Attributes("class") = "errorMsg"
            strlbl_bottom.Text = "Sila pilih SATU kuarters untuk meneruskan proses permohonan."
        ElseIf idKuartersDiplih > 0 Then
            setRef = oCommon.ExecuteSQL(String.Format(query, idKuartersDiplih, permohonanID))
            If setRef = "0" Then
                If setRef = "0" Then
                    MsgTop.Attributes("class") = "successMsg"
                    strlbl_top.Text = "Pemilihan kuarters berjaya. Pemohonan anda diprosess"
                    MsgBottom.Attributes("class") = "successMsg"
                    strlbl_bottom.Text = "Pemilihan kuarters bejaya. Pemohonan anda diprosess"
                    SaveFunction.Disabled = True
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

    'Private Function checkCadanganKuarters() As Boolean
    '    Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
    '        Dim cmd As New SqlCommand("SELECT count(*) FROM spk_cadanganKuarters WHERE permohonan_id = " & permohonanID & ";", conn)
    '        Try
    '            conn.Open()
    '            Dim count As Integer = cmd.ExecuteScalar
    '            If count > 0 Then
    '                Return True
    '            Else
    '                Return False
    '            End If
    '        Catch ex As Exception
    '            Debug.WriteLine("Error(checkCadanganKuarters): " & ex.Message)
    '            Return False
    '        Finally
    '            conn.Close()
    '        End Try
    '    End Using
    'End Function

    Private Sub updateCadanganKuartersStatus()
        Dim setRef As String = ""
        Dim query As String = "UPDATE 
	        spk_cadanganKuarters
        SET	
	        status_dicadang = 'DIRUJUK'
        WHERE
	        pengguna_id = " & penggunaID & " AND permohonan_id = " & permohonanID & ";"
        setRef = oCommon.ExecuteSQL(query)
        If setRef = "0" Then
            Debug.WriteLine("updateCadanganKuartersStatus")
        Else
            Debug.WriteLine("Error(updateCadanganKuartersStatus): Failed to update.")
        End If
    End Sub

End Class