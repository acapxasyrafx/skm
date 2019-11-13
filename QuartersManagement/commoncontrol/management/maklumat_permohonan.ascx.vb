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
        maklumatUser()
        maklumatAnak()
        maklumatStatusPermohonan()

        If statusPermohon.Equals("PERMOHONAN BARU") Then
            permohonanBaharu.Attributes("class") = "progress-done"
            permohonanMenunggu.Attributes("class") = "progress-todo"
            permohonanKeputusan.Attributes("class") = "progress-todo"
            mvStatusPermohonan.ActiveViewIndex = 0
        ElseIf statusPermohon.Equals("PERMOHONAN SEDANG DIPROSES") Then
            If subStatusPermohonan.Equals("LULUS TANPA KEKOSONGAN") Then
                Debug.WriteLine("User belum memilih kuarters dicadang")
                maklumatCadanganKuarters()
                mvStatusPermohonan.ActiveViewIndex = 1
            ElseIf subStatusPermohonan.Equals("TUNGGU KELULUSAN") Then
                Debug.WriteLine("User telah memilih kuarters, tunggu persetujuan admin")
                mvStatusPermohonan.ActiveViewIndex = 2
            End If
            permohonanBaharu.Attributes("class") = "progress-done"
            permohonanMenunggu.Attributes("class") = "progress-done"
            permohonanKeputusan.Attributes("class") = "progress-todo"
        ElseIf statusPermohon.Equals("PERMOHONAN DITERIMA") Then
            permohonanBaharu.Attributes("class") = "progress-done"
            permohonanMenunggu.Attributes("class") = "progress-done"
            permohonanKeputusan.Attributes("class") = "progress-done"
            mvStatusPermohonan.ActiveViewIndex = 3
        ElseIf statusPermohon.Equals("PERMOHONAN DITOLAK") Then
            permohonanBaharu.Attributes("class") = "progress-done"
            permohonanMenunggu.Attributes("class") = "progress-done"
            permohonanKeputusan.Attributes("class") = "progress-done"
            mvStatusPermohonan.ActiveViewIndex = 4
        End If
    End Sub

    Private Sub maklumatUser()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("SELECT TOP 1
                A.pengguna_id,
                A.pengguna_nama,
                A.pengguna_status_perkahwinan,
                E.keluarga_anak,
                E.keluarga_tempat_tinggal,
                E.keluarga_tarikh_mula,
                A.pengguna_mula_perkhidmatan,
                B.unit_id,
                B.permohonan_status,
                B.permohonan_sub_status,
                D.kuarters_nama,
	            F.pangkalan_id,
	            F.pangkalan_nama,
                B.pemohonan_tarikh
            FROM 
                admin.spk_pengguna A
                JOIN admin.spk_permohonan B on B.pengguna_id = A.pengguna_id
                JOIN admin.spk_keluarga E ON E.pengguna_id = A.pengguna_id
                JOIN admin.spk_unit C ON C.unit_id = B.unit_id
                JOIN admin.spk_kuarters D ON D.kuarters_id = C.kuarters_id
	            JOIN dbo.spk_pangkalan F ON F.pangkalan_id = C.pangkalan_id
            WHERE
                A.pengguna_id = " & penggunaID & " AND B.permohonan_id = " & permohonanID & "
            ORDER BY
                B.pemohonan_tarikh DESC;",
            conn)

            Try
                conn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader
                If reader.HasRows Then
                    Do While reader.Read = True
                        lblJenisTempatTinggal.Text = reader("keluarga_tempat_tinggal")
                        lblTarikhMulaMenetap.Text = reader("keluarga_tarikh_mula").ToString()
                        lblKuarterDipohon.Text = reader("kuarters_nama")
                        lblTarikhPermohonan.Text = reader("pemohonan_tarikh")
                        statusPermohon = reader("permohonan_status")
                        subStatusPermohonan = reader("permohonan_sub_status").ToString
                        Debug.WriteLine("Success: maklumatUser")
                    Loop
                Else
                    Debug.WriteLine("Error(maklumatUser): No Rows")
                End If
                reader.Close()
            Catch ex As Exception
                Debug.WriteLine("Error(maklumatUser): " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Private Sub maklumatPermohonan()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("", conn)
            Try
                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader
                    If reader.HasRows Then
                        Do While reader.Read()

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
                                Debug.WriteLine(reader("log_status"))
                                permohonanBaharu.Attributes("class") = "progress-done"
                                lblTarikhBaharu.Text = reader("log_tarikh")
                            Case "PERMOHONAN SEDANG DIPROSES"
                                Debug.WriteLine(reader("log_status"))
                                permohonanMenunggu.Attributes("class") = "progress-done"
                                lblTarikhMenuggu.Text = reader("log_tarikh")
                            Case "PERMOHONAN DITERIMA", "PERMOHONAN DITOLAK"
                                Debug.WriteLine(reader("log_status"))
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
            Dim da As New SqlDataAdapter("SELECT * FROM spk_anak WHERE pengguna_id = " & penggunaID & "", conn)
            Try
                conn.Open()
                da.Fill(ds, "AnyTable")
                If ds.Tables(0).Rows.Count > 0 Then
                    lblBilAnak.Text = ds.Tables(0).Rows.Count
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
            e.Row.ToolTip = "Clik row untuk pilih kuarters."
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
                row.ToolTip = "Clik row untuk pilih kuarters."
            End If
        Next
    End Sub

    Private Sub SaveFunction_ServerClick(sender As Object, e As EventArgs) Handles SaveFunction.ServerClick
        Dim idKuartersDiplih As Integer
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
            Debug.WriteLine("Post-Check ID Dipilih:" & idKuartersDiplih)
            MsgTop.Attributes("class") = "successMsg"
            strlbl_top.Text = "Pemilihan kuarters berjaya. Pemohonan anda diprosess"
            MsgBottom.Attributes("class") = "successMsg"
            strlbl_bottom.Text = "Pemilihan kuarters bejaya. Pemohonan anda diprosess"
            SaveFunction.Disabled = True
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