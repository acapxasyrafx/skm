Imports System.Data.SqlClient
Imports System.Web.UI.ScriptManager
Imports System.Web.UI.HtmlControls

Public Class permohonan_kuarters
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
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim countAnak As Integer = 0
    Dim permohonanID As Integer
    Dim pangkatMata As Integer
    Dim jumlahPoint As Integer
    Dim dataAnak As New DataSet
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("user_id") IsNot Nothing Then
                pID.Value = Session("user_id")
                Load_Page()
            Else
                Response.Redirect("/")
            End If
        End If
    End Sub

    Private Sub Load_Page()
        ddlSenaraiKuarters.Enabled = False
        loadPangkalan()
        loadUser()
        loadMaklumatAnak()
        loadJenisTempatTinggal()
        loadPoints()
    End Sub
    Private Sub loadUser()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("
                SELECT 
                    A.pengguna_id
	                , A.pengguna_nama
	                , A.pengguna_jantina
	                , A.pengguna_tarikh_lahir
	                , A.pengguna_mula_perkhidmatan
	                , A.pengguna_tamat_perkhidmatan
	                , A.pengguna_no_tentera
	                , A.pangkat_id
	                , B.pangkat_nama
                    , B.pangkat_mata
                    , A.pengguna_status_perkahwinan
                FROM 
	                spk_pengguna A
	                JOIN spk_pangkat B ON B.pangkat_id = A.pangkat_id
                WHERE
	                A.pengguna_id = " & pID.Value & ";",
            conn)

            Try
                conn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                If reader.HasRows Then
                    If reader.Read() Then
                        pID.Value = reader("pengguna_id")
                        lblNama.Text = reader("pengguna_nama")
                        lblTarikhLahir.Text = reader("pengguna_tarikh_lahir")
                        lblJantina.Text = reader("pengguna_jantina")
                        pangkatID.Value = reader("pangkat_id")
                        lblPangkat.Text = reader("pangkat_nama")
                        lblNoTentera.Text = reader("pengguna_no_tentera")
                        lblTarikhMulaBerkhidmat.Text = reader("pengguna_mula_perkhidmatan")
                        lblTarikhAkhirBerkhidmat.Text = reader("pengguna_tamat_perkhidmatan")
                        lblStatusPerkahwinan.Text = reader("pengguna_status_perkahwinan")
                        pangkatMata = Integer.Parse(reader("pangkat_mata"))
                    Else
                        Debug.Write("ERROR(loadUser): CANNOT READ")
                    End If
                Else
                    Debug.Write("ERROR(loadUser): NO ROWS")
                End If
            Catch ex As Exception
                Debug.WriteLine("ERROR(loadUser-permohonan_kuarters:91): " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Private Function loadMaklumatAnak() As Boolean
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
                        WHERE pengguna_id = " & pID.Value & ";",
                    conn)
            Try
                conn.Open()
                da.Fill(dataAnak, "AnyTable")
                Dim nRows As Integer = 0
                Dim nCount As Integer = 1
                countAnak = dataAnak.Tables(0).Rows.Count
                If countAnak > 0 Then
                    datRespondent.DataSource = dataAnak
                    datRespondent.DataBind()
                End If
                Return True
            Catch ex As Exception
                Debug.WriteLine("ERROR(loadMaklumatAnak-permohonan_kuarters:123): " & ex.Message)
                Return False
            Finally
                conn.Close()
            End Try
        End Using
    End Function


    Private Sub loadPangkalan()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("SELECT * FROM spk_pangkalan;", conn)
            Dim ds As New DataSet
            Try
                conn.Open()
                Dim da As New SqlDataAdapter(cmd)
                da.Fill(ds, "AnyTable")
                ddlSenaraiPangkalan.DataSource = ds
                ddlSenaraiPangkalan.DataTextField = "pangkalan_nama"
                ddlSenaraiPangkalan.DataValueField = "pangkalan_id"
                ddlSenaraiPangkalan.DataBind()
                ddlSenaraiPangkalan.Items.Insert(0, New ListItem("Senarai Pangkalan...", String.Empty))
                ddlSenaraiPangkalan.SelectedIndex = 0
            Catch ex As Exception
                Debug.WriteLine("ERROR(loadPangkalan-permohonan_kuarters:147): " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Private Sub loadKuarters()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("SELECT * FROM spk_kuarters WHERE pangkalan_id = " & ddlSenaraiPangkalan.SelectedValue & ";", conn)
            Dim ds As New DataSet
            Try
                conn.Open()
                Dim da As New SqlDataAdapter(cmd)
                da.Fill(ds, "AnyTable")
                ddlSenaraiKuarters.DataSource = ds
                ddlSenaraiKuarters.DataTextField = "kuarters_nama"
                ddlSenaraiKuarters.DataValueField = "kuarters_id"
                ddlSenaraiKuarters.DataBind()
                ddlSenaraiKuarters.Items.Insert(0, New ListItem("-- SILA PILIH --", String.Empty))
                ddlSenaraiKuarters.SelectedIndex = 0
            Catch ex As Exception
                Debug.Write("ERROR(loadKuarters): " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Protected Sub loadJenisTempatTinggal()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("SELECT config_parameter, config_value FROM general_config WHERE config_type = 'JENIS PENEMPATAN SEBELUM';", conn)
                Using sda As New SqlDataAdapter(cmd)
                    Dim ds As New DataSet
                    sda.Fill(ds)
                    ddlJenisPenempatan.DataSource = ds
                    ddlJenisPenempatan.DataTextField = "config_parameter"
                    ddlJenisPenempatan.DataValueField = "config_value"
                    ddlJenisPenempatan.DataBind()
                    ddlJenisPenempatan.Items.Insert(0, New ListItem("-- SILA PILIH --"))
                End Using
            End Using
        End Using
    End Sub

    Protected Function Save()
        'Save permohonan
        Dim query As String = "
            DECLARE @permohonanID INT;
            INSERT INTO
	            spk_permohonan( pengguna_id , permohonan_no_permohonan , kuarters_id , permohonan_tarikh , permohonan_status , permohonan_mata) 
            VALUES ( @penggunaID, @noPermohonan, @kuartersID, @tarikh, 'PERMOHONAN BARU', @mata );"
        'Scope Identity
        query += "SELECT @permohonanID = SCOPE_IDENTITY();"
        'History Keluarga
        query += "INSERT INTO spk_historyKeluarga( permohonan_id , pengguna_id , historyKeluarga_tarikh , historyKeluarga_anak , historyKeluarga_tempat_tinggal , historyKeluarga_tarikh_mula )
                    VALUES ( @permohonanID, @penggunaID, @tarikh, @jumlahAnak, @rumahSebelum, @tarikhMulaMenetap);"
        'History Pengguna
        query += "INSERT INTO  spk_historyPengguna( pengguna_id , permohonan_id , pangkat_id , historyPengguna_statusPerkahwinan , historyPengguna_penggunaNoTentera, historyPengguna_mulaBerkhidmat, historyPengguna_tamatBerkhidmat)
                VALUES ( @penggunaID ,   @permohonanID ,   @pangkatId ,   @statusPerkahwinan ,   @penggunaNoTentera, @tarikhMulaBerkhidmat, @tarikhTamatBerkhidmat );"
        'Log Status Permohonan
        query += " INSERT INTO spk_logPermohonan ( pengguna_id , permohonan_id , log_tarikh  , log_status ) 
                VALUES ( @penggunaID, @permohonanID, @tarikh, 'PERMOHONAN BARU');"
        query += "SELECT @permohonanID as 'permohonan_id'"
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand(query)
                cmd.Connection = conn
                cmd.Parameters.Add("@penggunaID", SqlDbType.Int).Value = pID.Value
                cmd.Parameters.Add("@noPermohonan", SqlDbType.NVarChar, 50).Value = genNoPermohonan(Date.Now.Year & Date.Now.Month & Date.Now.Day)
                cmd.Parameters.Add("@kuartersID", SqlDbType.Int).Value = ddlSenaraiKuarters.SelectedValue
                cmd.Parameters.Add("@tarikh", SqlDbType.NVarChar, 50).Value = Date.Now.ToString("dd/MM/yy")
                cmd.Parameters.Add("@mata", SqlDbType.NVarChar, 50).Value = Integer.Parse(loadPoints())
                cmd.Parameters.Add("@jumlahAnak", SqlDbType.NVarChar, 50).Value = datRespondent.Rows.Count
                cmd.Parameters.Add("@rumahSebelum", SqlDbType.NVarChar, 50).Value = ddlJenisPenempatan.SelectedValue
                cmd.Parameters.Add("@tarikhMulaMenetap", SqlDbType.NVarChar, 50).Value = DateTime.ParseExact(dpTarikhMulaMenetap.Text, "dd/MM/yyyy", Nothing)
                cmd.Parameters.Add("@pangkatID", SqlDbType.Int).Value = pangkatID.Value
                cmd.Parameters.Add("@statusPerkahwinan", SqlDbType.NVarChar, 50).Value = lblStatusPerkahwinan.Text
                cmd.Parameters.Add("@penggunaNoTentera", SqlDbType.NVarChar, 50).Value = lblNoTentera.Text
                cmd.Parameters.Add("@tarikhMulaBerkhidmat", SqlDbType.NVarChar, 50).Value = lblTarikhMulaBerkhidmat.Text
                cmd.Parameters.Add("@tarikhTamatBerkhidmat", SqlDbType.NVarChar, 50).Value = lblTarikhAkhirBerkhidmat.Text
                Try
                    conn.Open()
                    Dim reader As SqlDataReader
                    reader = cmd.ExecuteReader
                    If reader.HasRows() Then
                        While reader.Read()
                            insertHistoryAnak(reader("permohonan_id"))
                            newNotifikasi(reader("permohonan_id"), "ADMIN", 29)
                        End While
                    End If
                    Return True
                Catch ex As Exception
                    Debug.WriteLine("Error(new_save-permohonan_kuarters:239): " & ex.Message)
                    Return False
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Function

    Private Sub SaveFunction_ServerClick(sender As Object, e As EventArgs) Handles SaveFunction.ServerClick
        If validateSave() = True Then
            If Save() = True Then
                MsgTop.Attributes("class") = "successMsg"
                strlbl_top.Text = strSaveSuccessAlert
                MsgBottom.Attributes("class") = "successMsg"
                strlbl_bottom.Text = strSaveSuccessAlert
            Else
                MsgTop.Attributes("class") = "errorMsg"
                strlbl_top.Text = strSaveFailAlert
                MsgBottom.Attributes("class") = "errorMsg"
                strlbl_bottom.Text = strSaveFailAlert
            End If
        End If
    End Sub

    Private Sub Refresh_ServerClick(sender As Object, e As EventArgs) Handles Refresh.ServerClick
        Response.Redirect("Permohonan.Kuarters.aspx?p=Permohonan+Kuarters")
    End Sub

    Private Function validateSave() As Boolean
        If ddlJenisPenempatan.SelectedValue = Nothing Then
            showMessage("ALERT", "Sila pilih jenis penempatan sebelum.")
            Return False
        ElseIf ddlSenaraiPangkalan.SelectedValue = Nothing Then
            showMessage("ALERT", "Bahagian Pangkalan adalah perlu dipilih.")
            Return False
        ElseIf ddlSenaraiKuarters.SelectedValue = Nothing Then
            showMessage("ALERT", "Bahagian Kuarters/Rumah adalah perlu dipilih.")
            Return False
        ElseIf dpTarikhMulaMenetap.Text.Length <= 0 Then
            showMessage("ALERT", "Sila pilih tarikh mula menetap.")
            Return False
        ElseIf IsDate(dpTarikhMulaMenetap.Text) = False Then
            showMessage("ALERT", "Sila masukkan tarikh mula yang betul.")
            Return False
        Else
            Return True
        End If
    End Function

    Private Function validateAnak() As Boolean
        If txtNamaAnak.Text = Nothing Then
            Return False
        ElseIf icToAge(txtICAnak.Text) = Nothing Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub btnTambahRow_Click(sender As Object, e As EventArgs) Handles btnTambahRow.Click

        If insertMaklumatAnak() Then
            txtNamaAnak.Text = ""
            txtICAnak.Text = ""
            If loadMaklumatAnak() Then
                Debug.WriteLine("OK(btnTambahRow): WRITE OK, READ OK")
            Else
                Debug.WriteLine("ERROR(btnTambahRow-permohonan_kuarters:307): Error loadMaklumatAnak")
            End If
        Else
            Debug.WriteLine("ERROR(btnTambahRow-permohonan_kuarters:310): Error insertMaklumatAnak")
        End If

    End Sub

    Private Function insertMaklumatAnak() As Boolean
        Dim namaAnak = txtNamaAnak.Text
        Dim icAnak = txtICAnak.Text
        Dim umurAnak = icToAge(icAnak)
        Dim strRet As String

        strRet = oCommon.ExecuteSQL("INSERT INTO 
                spk_anak(
                    pengguna_id
                    ,   anak_nama
                    ,   anak_ic
                    ,   anak_umur
                ) 
                VALUES(
                    " & pID.Value & "
                    ,   '" & namaAnak & "'
                    ,   '" & icAnak & "'
                    ,   '" & umurAnak & "'
                )")
        If strRet = "0" Then
            Return True
        Else
            Debug.WriteLine("ERROR (insertMaklumatAnak)")
            Return False
        End If
    End Function

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strCID = datRespondent.DataKeys(e.RowIndex).Values("anak_id").ToString
        If Not strCID = "" Then
            strSQL = "DELETE FROM spk_anak WHERE anak_id='" & oCommon.FixSingleQuotes(strCID) & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                loadMaklumatAnak()
            Else
                Debug.WriteLine("ERROR(datRespondent_RowDeleting-permohonan_kuarters:350)")
            End If
        End If
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

    Private Sub cbBertukarPangkalan_CheckedChanged(sender As Object, e As EventArgs) Handles cbBertukarPangkalan.CheckedChanged
        If cbBertukarPangkalan.Checked = True Then
            tblBertukar.Visible = True
        ElseIf cbBertukarPangkalan.Checked = False Then
            tblBertukar.Visible = False
        End If
    End Sub

    Private Sub ddlSenaraiPangkalan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSenaraiPangkalan.SelectedIndexChanged
        loadKuarters()
        ddlSenaraiKuarters.Enabled = True
    End Sub

    Private Function genNoPermohonan(ByVal datePermohonan As String) As String
        Dim noPermohonan As String = ""
        Dim s As String = "1234567890"
        Dim r As New Random
        Dim sb As New StringBuilder
        For i As Integer = 1 To 4
            Dim idx As Integer = r.Next(0, 9)
            sb.Append(s.Substring(idx, 1))
        Next
        noPermohonan = datePermohonan & "_" & sb.ToString
        Return noPermohonan
    End Function

    Private Function loadPoints()
        Dim totalAnakLayak As Integer = 0
        For i As Integer = 0 To datRespondent.Rows.Count - 1
            If icToAge(CType(datRespondent.Rows(i).FindControl("lblICAnak"), Label).Text) > 18 Then
                Continue For
            Else
                totalAnakLayak += 1
            End If
        Next
        If totalAnakLayak > 5 Then
            totalAnakLayak = 5
        End If
        Dim totalPoint As Integer = pangkatMata + (totalAnakLayak * 5)
        Return totalPoint
    End Function

    Private Function insertHistoryAnak(ByVal permohonanID As Integer) As Boolean
        Try
            For i As Integer = 0 To datRespondent.Rows.Count - 1
                Dim namaAnak = CType(datRespondent.Rows(i).FindControl("lblNamaAnak"), Label).Text 'Nama anak
                Dim kpAnak = CType(datRespondent.Rows(i).FindControl("lblICAnak"), Label).Text 'IC Anak
                Dim query As String
                Dim strRet As String
                query = String.Format("
                INSERT INTO 
                    spk_historyAnak(
                        permohonan_id
                        ,   historyAnak_nama
                        ,   historyAnak_ic
                    ) 
                VALUES(
                    {0}
                    ,   '{1}'
                    ,   '{2}'
                );", permohonanID, namaAnak, kpAnak)
                strRet = oCommon.ExecuteSQL(query)
                If strRet = "0" Then
                    Continue For
                Else
                    Debug.WriteLine("Error(saveHistoryAnak-permohonan_kuarters:430): Failed to save(" & namaAnak & ", idx:" & i & ")")
                End If
            Next
            Return True
        Catch ex As Exception
            Debug.WriteLine("Error(insertHistoryAnak-permohonan_kuarters:435): " & ex.Message)
            Return False
        End Try
    End Function

    Private Sub saveBottom_ServerClick(sender As Object, e As EventArgs) Handles saveBottom.ServerClick
        Try
            If Save() = True Then
                MsgTop.Attributes("class") = "successMsg"
                strlbl_top.Text = strSaveSuccessAlert
                MsgBottom.Attributes("class") = "successMsg"
                strlbl_bottom.Text = strSaveSuccessAlert
                Response.Redirect("Senarai.Permohonan.Pengguna.aspx?p=Senarai%20Kuarters")
            Else
                MsgTop.Attributes("class") = "errorMsg"
                strlbl_top.Text = strSaveFailAlert
                MsgBottom.Attributes("class") = "errorMsg"
                strlbl_bottom.Text = strSaveFailAlert
            End If
        Catch ex As Exception
            MsgTop.Attributes("class") = "errorMsg"
            strlbl_top.Text = strSysErrorAlert
            MsgBottom.Attributes("class") = "errorMsg"
            strlbl_bottom.Text = strSysErrorAlert & "<br>" & ex.Message
            Debug.WriteLine("ERROR(saveFunction-permohonan_kuarters:459): " & ex.Message)
        End Try
    End Sub

    Public Sub showMessage(ByVal messageType As String, ByVal message As String)
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

        If messageType.Equals("ALERT") Then
            MsgTop.Attributes("class") = "errorMsg"
            MsgBottom.Attributes("class") = "errorMsg"
        ElseIf messageType.Equals("SUCCESS") Then
            MsgTop.Attributes("class") = "successMsg"
            MsgBottom.Attributes("class") = "successMsg"
        End If
        strlbl_top.Text = message
        strlbl_bottom.Text = message
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
                    Debug.WriteLine("Error(newNotifikasi - permohonan_kuarters:514): " & ex.Message)
                    Return False
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Function

End Class