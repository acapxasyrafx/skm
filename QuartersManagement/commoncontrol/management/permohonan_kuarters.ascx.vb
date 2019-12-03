Imports System.Data.SqlClient
Imports System.Web.UI.ScriptManager

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
    Dim tempID As Integer = 1
    Dim permohonanID As Integer
    Dim pangkatMata As Integer
    Dim jumlahPoint As Integer
    Dim dataAnak As New DataSet
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Load_Page()
        End If
    End Sub

    Private Sub Load_Page()
        ddlSenaraiKuarters.Enabled = False
        loadPangkalan()
        loadUser()
        readMaklumatAnak()
        'HARI
        populateDay(ddlTarikhTinggalHariMula)
        populateDay(ddlTarikhTukarHari)
        'BULAN
        populateMonth(ddlTarikhTinggalBulanMula)
        populateMonth(ddlTarikhTukarBulan)
        'TAHUN
        populateYear(ddlTarikhTinggalTahunMula)
        populateYear(ddlTarikhTukarTahun)
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
	                A.pengguna_id = " & tempID & ";",
            conn)

            Try
                conn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                If reader.HasRows Then
                    If reader.Read() Then
                        penggunaID.Value = reader("pengguna_id")
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
                Debug.WriteLine("ERROR(loadUser): " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

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
                Debug.WriteLine("ERROR(loadPangkalan): " & ex.Message)
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
                ddlSenaraiKuarters.Items.Insert(0, New ListItem("Senarai Kuarters...", String.Empty))
                ddlSenaraiKuarters.SelectedIndex = 0
            Catch ex As Exception
                Debug.Write("ERROR(loadKuarters): " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Private Function getDate(ByVal hari As String, ByVal bulan As String, ByVal tahun As String) As String
        Return (hari & "/" & bulan & "/" & tahun)
    End Function

    Private Sub populateDay(ByVal ddl As DropDownList)
        For i As Integer = 1 To 31
            Dim temp As ListItem
            If i < 10 Then
                temp = New ListItem("0" & i, i)
            Else
                temp = New ListItem(i, i)
            End If
            ddl.Items.Add(temp)
        Next
    End Sub

    Private Sub populateMonth(ByVal ddl As DropDownList)
        For i As Integer = 1 To 12
            Dim temp As ListItem
            If i < 10 Then
                temp = New ListItem("0" & i, i)
            Else
                temp = New ListItem(i, i)
            End If
            ddl.Items.Add(temp)
        Next
    End Sub

    Private Sub populateYear(ByVal ddl As DropDownList)
        Dim startYear As Integer = Date.Now().Year - 30
        For i As Integer = 1 To 30
            Dim item As Integer = startYear + i
            Dim temp As New ListItem(item, item)
            ddl.Items.Add(item)
        Next
    End Sub

    Private Function Save() As Boolean
        Dim kuartersId = ddlSenaraiKuarters.SelectedValue
        Dim jenisRumahSebelum = ddlJenisPenempatan.SelectedValue
        Dim mulaMenetap = getDate(ddlTarikhTinggalHariMula.SelectedValue, ddlTarikhTinggalBulanMula.SelectedValue, ddlTarikhTinggalTahunMula.SelectedValue)
        Dim tarikhPindah = getDate(ddlTarikhTukarHari.SelectedValue, ddlTarikhTukarBulan.SelectedValue, ddlTarikhTukarTahun.SelectedValue)
        Dim totalAnak = datRespondent.Rows.Count

        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("
            INSERT INTO
	            spk_permohonan(
		            pengguna_id
		            , permohonan_no_permohonan
		            , kuarters_id
		            , permohonan_tarikh
		            , permohonan_status
                    , permohonan_mata
	            )
            VALUES (
	            " & penggunaID.Value & "
	            , '" & genNoPermohonan(Date.Now.Year & Date.Now.Month & Date.Now.Day) & "'
	            , " & kuartersId & "
	            , '" & Date.Now().ToString("dd'/'MM'/'yyyy") & "'
	            , 'PERMOHONAN BARU'
                , " & jumlahPoint & "
            );SELECT SCOPE_IDENTITY()", conn)
            Try
                conn.Open()
                permohonanID = cmd.ExecuteScalar
                If permohonanID = Nothing Then
                    Debug.WriteLine("ERROR(Save->permohonan): PermohonanID is nothing")
                Else
                    If insertHistroyKeluarga(permohonanID, totalAnak, jenisRumahSebelum, mulaMenetap) Then
                        If insertHistoryPengguna(permohonanID) Then
                            If insertLogPermohonan(permohonanID) Then
                                If insertHistoryAnak(permohonanID) Then
                                    Return True
                                Else
                                    Debug.WriteLine("Error(Save -> insertHistoryAnak)")
                                    Return False
                                End If
                            Else
                                Debug.WriteLine("Error(Save -> insertLogPermohonan)")
                                Return False
                            End If
                        Else
                            Debug.WriteLine("Error(Save -> insertHistoryPengguna)")
                            Return False
                        End If
                    Else
                        Debug.WriteLine("Error(Save -> insertHistroyKeluarga)")
                        Return False
                    End If
                End If
            Catch ex As Exception
                Debug.WriteLine("Error(Save -> permohonan): " & ex.Message)
                MsgTop.Attributes("class") = "errorMsg"
                strlbl_top.Text = strSysErrorAlert
                MsgBottom.Attributes("class") = "errorMsg"
                strlbl_bottom.Text = strSysErrorAlert & "<br>" & strRet
                Return False
            Finally
                conn.Close()
            End Try
        End Using
    End Function

    Private Sub SaveFunction_ServerClick(sender As Object, e As EventArgs) Handles SaveFunction.ServerClick
        Try
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
        Catch ex As Exception
            MsgTop.Attributes("class") = "errorMsg"
            strlbl_top.Text = strSysErrorAlert
            MsgBottom.Attributes("class") = "errorMsg"
            strlbl_bottom.Text = strSysErrorAlert & "<br>" & ex.Message
            Debug.WriteLine("ERROR(saveFunction): " & ex.Message)
        End Try
    End Sub

    Private Sub Refresh_ServerClick(sender As Object, e As EventArgs) Handles Refresh.ServerClick
        Response.Redirect("Permohonan.Kuarters.aspx?p=Permohonan+Kuarters")
    End Sub

    Private Function validateSave() As Boolean
        If ddlJenisPenempatan.SelectedValue = Nothing Then
            showMessage("ALERT", "Sila pilih jenis penempatan sebelum.")
            Return False
        ElseIf ddlTarikhTinggalHariMula.SelectedValue = Nothing Then
            showMessage("ALERT", "Sila pilih hari yang betul untuk tarikh mula menetak.")
            Return False
        ElseIf ddlTarikhTinggalBulanMula.SelectedValue = Nothing Then
            showMessage("ALERT", "Sila pilih bulan yang betul untuk tarikh mula menetak.")
            Return False
        ElseIf ddlTarikhTinggalTahunMula.SelectedValue = Nothing Then
            showMessage("ALERT", "Sila pilih TAHUN yang betul untuk tarikh mula menetak.")
            Return False
        ElseIf ddlSenaraiPangkalan.SelectedValue = Nothing Then
            showMessage("ALERT", "Bahagian Pangkalan adalah perlu.")
            Return False
        ElseIf ddlSenaraiKuarters.SelectedValue = Nothing Then
            showMessage("ALERT", "Bahagian Kuarters/Rumah adalah perlu.")
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
            If readMaklumatAnak() Then
                Debug.WriteLine("OK(btnTambahRow): WRITE OK, READ OK")
            Else
                Debug.WriteLine("ERROR(btnTambahRow): Error readMaklumatAnak")
            End If
        Else
            Debug.WriteLine("ERROR(btnTambahRow): Error insertMaklumatAnak")
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
                    " & penggunaID.Value & "
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

    Private Function readMaklumatAnak() As Boolean
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
                        WHERE pengguna_id = " & penggunaID.Value & ";",
                    conn)
            Try
                conn.Open()
                da.Fill(dataAnak, "AnyTable")
                Dim nRows As Integer = 0
                Dim nCount As Integer = 1
                countAnak = dataAnak.Tables(0).Rows.Count
                jumlahPoint = loadPoints(dataAnak)
                If countAnak > 0 Then
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

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strCID = datRespondent.DataKeys(e.RowIndex).Values("anak_id").ToString
        If Not strCID = "" Then
            strSQL = "DELETE FROM spk_anak WHERE anak_id='" & oCommon.FixSingleQuotes(strCID) & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                readMaklumatAnak()
            Else
                Debug.WriteLine("ERROR(datRespondent_RowDeleting)")
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

    Private Function loadPoints(ByVal dataAnak As DataSet)
        Dim totalAnakLayak As Integer = 0

        For i As Integer = 0 To dataAnak.Tables(0).Rows.Count - 1
            If icToAge(dataAnak.Tables(0).Rows(i)(3).ToString) < 18 Then
                Continue For
            Else
                totalAnakLayak += 1
            End If
        Next

        If totalAnakLayak > 5 Then
            totalAnakLayak = 5
        End If

        Dim totalPoint As Integer = pangkatMata + (totalAnakLayak * 5)
        Debug.WriteLine("Total Anak Layak: " & totalAnakLayak)
        Debug.WriteLine("Mata pangkat: " & pangkatMata)
        Debug.WriteLine("Total Point: " & totalPoint)
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
                    Debug.WriteLine("Error(saveHistoryAnak): Failed to save(" & namaAnak & ", idx:" & i & ")")
                End If
            Next
            Return True
        Catch ex As Exception
            Debug.WriteLine("Error(insertHistoryAnak): " & ex.Message)
            Return False
        End Try
    End Function

    Private Function insertLogPermohonan(ByVal permohonanID As Integer) As Boolean
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("
                INSERT INTO spk_logPermohonan (
                      pengguna_id
                    , permohonan_id
                    , log_tarikh 
                    , log_status
                ) VALUES (
                    " & penggunaID.Value & "
                    , " & permohonanID & "
                    , '" & Date.Now & "'
                    , 'PERMOHONAN BARU'
                )", conn)
            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                Debug.WriteLine("Error(insertLogPermohonan): " & ex.Message)
                Return False
            Finally
                conn.Close()
            End Try
        End Using
    End Function
    Private Function insertHistroyKeluarga(ByVal permohoananID As Integer, ByVal totalAnak As Integer, ByVal jenisRumahSebelum As String, ByVal mulaMenetap As String) As Boolean
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("
                    INSERT INTO
	                    spk_historyKeluarga(
		                    permohonan_id
		                    , pengguna_id
		                    , historyKeluarga_tarikh
		                    , historyKeluarga_anak
		                    , historyKeluarga_tempat_tinggal
		                    , historyKeluarga_tarikh_mula
	                    )
                    VALUES (
	                    " & permohoananID & "
	                    , " & penggunaID.Value & "
	                    , '" & Date.Now().ToString("dd'/'MM'/'yyyy") & "'
	                    , " & totalAnak & "
	                    , '" & jenisRumahSebelum & "'
	                    , '" & mulaMenetap & "'
                    )", conn)
            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                Debug.WriteLine("ERROR(inserHistroyKeluarga): " & ex.Message)
                Return False
            Finally
                conn.Close()
            End Try
        End Using
    End Function

    Private Function insertHistoryPengguna(ByVal permohonanID As Integer) As Boolean
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("
                INSERT INTO 
                    spk_historyPengguna(
                        permohonan_id
                        , pangkat_id
                        , historyPengguna_statusPerkahwinan
                        , historyPengguna_penggunaNoTentera
                )
                VALUES
                (
                    @permohonanID
                    ,   @pangkatId
                    ,   @statusPerkahwinan
                    ,   @penggunaNoTentera
                )
            ")
                cmd.Connection = conn
                cmd.Parameters.Add("@permohonanID", SqlDbType.Int).Value = permohonanID
                cmd.Parameters.Add("@pangkatID", SqlDbType.Int).Value = Integer.Parse(pangkatID.Value)
                cmd.Parameters.Add("@penggunaID", SqlDbType.Int).Value = Integer.Parse(penggunaID.Value)
                cmd.Parameters.Add("@statusPerkahwinan", SqlDbType.NVarChar, 50).Value = lblStatusPerkahwinan.Text
                cmd.Parameters.Add("@penggunaNoTentera", SqlDbType.NVarChar, 50).Value = lblNoTentera.Text
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Debug.WriteLine("Error(insertHistoryPengguna): " & ex.Message)
                    Return False
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
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
            Debug.WriteLine("ERROR(saveFunction): " & ex.Message)
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
End Class