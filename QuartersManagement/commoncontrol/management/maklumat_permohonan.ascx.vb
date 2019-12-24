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
    Dim statusPermohonan As String = ""
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
                FROM 
	                spk_permohonan A
	                LEFT JOIN spk_kuarters B ON B.kuarters_id = A.kuarters_id
	                LEFT JOIN spk_pangkalan C ON C.pangkalan_id = B.pangkalan_id
	                LEFT JOIN spk_historyPengguna D ON D.permohonan_id = A.permohonan_id
	                LEFT JOIN spk_historyKeluarga E ON E.permohonan_id = A.permohonan_id
	                LEFT JOIN spk_pangkat F ON F.pangkat_id = D.pangkat_id
	                LEFT JOIN spk_pengguna G ON G.pengguna_id = D.pengguna_id
					LEFT JOIN spk_suratTawaran H ON H.permohonan_id = A.permohonan_id
                WHERE
                    A.permohonan_id = " & permohonanID & "
                ;
            ", conn)
            Try
                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader
                    If reader.HasRows Then
                        While reader.Read()
                            lblPangkat.Text = reader("pangkat_nama")
                            lblNoTentera.Text = reader("historyPengguna_penggunaNoTentera")
                            lblNama.Text = reader("pengguna_nama")
                            lblJantina.Text = reader("pengguna_jantina")
                            lblTarikhLahir.Text = reader("pengguna_tarikh_lahir")
                            lblStatusKahwin.Text = reader("historyPengguna_statusPerkahwinan")
                            lblMulaBerkhidmat.Text = reader("pengguna_mula_perkhidmatan")
                            lblTamatBerkhidmat.Text = reader("pengguna_tamat_perkhidmatan")
                            lblJenisTempatTinggal.Text = reader("historyKeluarga_tempat_tinggal")
                            lblTarikhMulaMenetap.Text = reader("historyKeluarga_tarikh_mula").ToString()
                            lblKuarterDipohon.Text = reader("kuarters_nama")
                            lblTarikhPermohonan.Text = reader("permohonan_tarikh")
                            statusPermohonan = reader("permohonan_status")
                            subStatusPermohonan = reader("permohonan_sub_status").ToString
                            Debug.WriteLine("Success: maklumatUser")
                            Select Case statusPermohonan
                                Case "PERMOHONAN BARU"
                                    mvMaklumatStatus.ActiveViewIndex = 0
                                Case "PERMOHONAN MENUNGGU"
                                    Select Case subStatusPermohonan
                                        Case "TAWARAN UNIT"
                                            btnField.Visible = True
                                            divSuratTawaran.InnerHtml = Server.HtmlDecode(reader("suratTawaran_content"))
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
                                    If subStatusPermohonan.Equals("DITOLAK") Then
                                        lblSebabDitolak.Text = reader("permohonan_nota")
                                        mvMaklumatStatus.ActiveViewIndex = 4
                                    ElseIf subStatusPermohonan.Equals("DIBATAL") Then
                                        lblSebabDibatal.Text = reader("permohonan_nota")
                                        mvMaklumatStatus.ActiveViewIndex = 5
                                    End If
                                Case Else
                                    Debug.WriteLine("Status(Else): " & statusPermohonan)
                                    mvMaklumatStatus.ActiveViewIndex = 0
                            End Select
                        End While
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
                WHERE 
                    pengguna_id = " & penggunaID & " 
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
                            Case "PERMOHONAN MENUNGGU"
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
            e.Row.ToolTip = "Klik row untuk pilih kuarters."
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

    'Private Sub SaveFunction_ServerClick(sender As Object, e As EventArgs) Handles SaveFunction.ServerClick
    '    Dim idKuartersDiplih As Integer
    '    Dim setRef As String = ""
    '    Dim query = "UPDATE spk_permohonan SET kuarters_id = {0}, permohonan_sub_status = 'TUNGGU KELULUSAN' WHERE permohonan_id = {1};"
    '    For Each row As GridViewRow In tblCadanganKuarters.Rows
    '        If row.RowIndex = tblCadanganKuarters.SelectedIndex Then
    '            idKuartersDiplih = Integer.Parse(tblCadanganKuarters.DataKeys(row.RowIndex).Value)
    '        End If
    '    Next

    '    If idKuartersDiplih = Nothing Then
    '        MsgTop.Attributes("class") = "errorMsg"
    '        strlbl_top.Text = "Sila pilih SATU kuarters untuk meneruskan proses permohonan."
    '        MsgBottom.Attributes("class") = "errorMsg"
    '        strlbl_bottom.Text = "Sila pilih SATU kuarters untuk meneruskan proses permohonan."
    '    ElseIf idKuartersDiplih > 0 Then
    '        setRef = oCommon.ExecuteSQL(String.Format(query, idKuartersDiplih, permohonanID))
    '        If setRef = "0" Then
    '            If setRef = "0" Then
    '                MsgTop.Attributes("class") = "successMsg"
    '                strlbl_top.Text = "Pemilihan kuarters berjaya. Pemohonan anda diprosess"
    '                MsgBottom.Attributes("class") = "successMsg"
    '                strlbl_bottom.Text = "Pemilihan kuarters bejaya. Pemohonan anda diprosess"
    '                SaveFunction.Disabled = True
    '                Load_Page()
    '            Else
    '                MsgTop.Attributes("class") = "errorMsg"
    '                strlbl_top.Text = strSaveFailAlert
    '                MsgBottom.Attributes("class") = "errorMsg"
    '                strlbl_bottom.Text = strSaveFailAlert
    '                Debug.WriteLine("Error(SaveFunction_ServerClick):" & setRef)
    '            End If
    '        Else
    '            MsgTop.Attributes("class") = "errorMsg"
    '            strlbl_top.Text = strSaveFailAlert
    '            MsgBottom.Attributes("class") = "errorMsg"
    '            strlbl_bottom.Text = strSaveFailAlert
    '            Debug.WriteLine("Error(SaveFunction_ServerClick):" & setRef)
    '        End If
    '    End If
    'End Sub

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
	        pengguna_id = " & penggunaID & " AND permohonan_id = " & permohonanID & ";"
        setRef = oCommon.ExecuteSQL(query)
        If setRef = "0" Then
            Debug.WriteLine("updateCadanganKuartersStatus")
        Else
            Debug.WriteLine("Error(updateCadanganKuartersStatus): Failed to update.")
        End If
    End Sub

    Private Sub btnBatalPermohonan_Click(sender As Object, e As EventArgs) Handles btnBatalPermohonan.Click
        Dim query = "UPDATE spk_permohonan SET permohonan_status = 'PERMOHONAN DITOLAK' , permohonan_sub_status = 'DIBATAL' , permohonan_nota = @sebab , permohonan_tarikh = '" & Date.Now.ToString("dd/MM/yy") & "' WHERE permohonan_id = @permohonanID; 
INSERT INTO spk_logPermohonan(pengguna_id, permohonan_id, log_tarikh, log_status) VALUES (" & penggunaID & ", @permohonanID, '" & Date.Now & "', 'PERMOHONAN DITOLAK')"
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
                        Response.Redirect("Maklumat.Permohonan.Pengguna.aspx?p=Maklumat%20Permohonan&permohonan=" & Request.QueryString("permohonan"))
                    End Try
                End Using
            End Using
        End If
    End Sub
End Class