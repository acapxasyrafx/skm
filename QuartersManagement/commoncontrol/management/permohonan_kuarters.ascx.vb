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
    Dim permohonanID As Integer
    Dim pangkatMata As Integer
    Dim dataAnak As New DataSet
    Dim jumlahPoint As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ddlSenaraiKuarters.Enabled = False
            loadPangkalan()
            loadUser()
            readMaklumatAnak()
            'If countAnak > 0 Then
            '    cbTiadaAnak.Enabled = False
            'Else
            '    cbTiadaAnak.Enabled = True
            'End If
            'HARI
            populateDay(ddlTarikhTinggalHariMula)
            populateDay(ddlTarikhTukarHari)
            'BULAN
            populateMonth(ddlTarikhTinggalBulanMula)
            populateMonth(ddlTarikhTukarBulan)
            'TAHUN
            populateYear(ddlTarikhTinggalTahunMula)
            populateYear(ddlTarikhTukarTahun)
        End If
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
                FROM 
	                spk_pengguna A
	                JOIN spk_pangkat B ON B.pangkat_id = A.pangkat_id
                WHERE
	                A.pengguna_id = 1;",
            conn)

            Try
                conn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                If reader.HasRows Then
                    If reader.Read() Then
                        pengguna_id.Value = reader("pengguna_id")
                        lblNama.Text = reader("pengguna_nama")
                        lblTarikhLahir.Text = reader("pengguna_tarikh_lahir")
                        lblJantina.Text = reader("pengguna_jantina")
                        lblPangkat.Text = reader("pangkat_nama")
                        lblNoTentera.Text = reader("pengguna_no_tentera")
                        lblTarikhMulaBerkhidmat.Text = reader("pengguna_mula_perkhidmatan")
                        pangkatMata = Integer.Parse(reader("pangkat_mata"))
                        '-------------------
                        If reader.IsDBNull("pengguna_tamat_perkhidmatan") Then
                            lblTarikhAkhirBerkhidmat.Text = "Masih Berkhidmat"
                        Else
                            lblTarikhAkhirBerkhidmat.Text = reader("pengguna_tamat_perkhidmatan")
                        End If
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

    Private Sub loadPangkalan()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("SELECT * FROM spk_pangkalan;", conn)
            Dim ds As New DataSet
            Try
                conn.Open()
                Dim da As New SqlDataAdapter(cmd)
                da.Fill(ds)
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
                da.Fill(ds)
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
        For i As Integer = 1 To 20
            Dim item As Integer = startYear + i
            Dim temp As New ListItem(item, item)
            ddl.Items.Add(item)
        Next
    End Sub

    Private Function Save() As Boolean
        Dim kuartersId = ddlSenaraiKuarters.SelectedValue
        Dim penggunaId = pengguna_id.Value
        Dim jenisRumahSebelum = ddlJenisPenempatan.SelectedValue
        Dim mulaMenetap = getDate(ddlTarikhTinggalHariMula.SelectedValue, ddlTarikhTinggalBulanMula.SelectedValue, ddlTarikhTinggalTahunMula.SelectedValue)
        Dim tarikhPindah = getDate(ddlTarikhTukarHari.SelectedValue, ddlTarikhTukarBulan.SelectedValue, ddlTarikhTukarTahun.SelectedValue)
        Dim totalAnak = datRespondent.Rows.Count
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionSetting"))
            Dim cmd As New SqlCommand("
                INSERT INTO
	                spk_permohonan(
		                pengguna_id
		                , permohonan_no_permohonan
		                , kuarters_id
		                , pemohonan_tarikh
		                , permohonan_status
	                )
                VALUES (
	                " & penggunaId & "
	                , '" & genNoPermohonan(Date.Now.Year & Date.Now.Month & Date.Now.Date) & "'
	                , " & kuartersId & "
	                , '" & Date.Now() & "'
	                , 'PERMOHONAN BARU'
                ); SELECT SCOPE_IDENTITY()", conn)
            Return True
            Try
                conn.Open()
                permohonanID = cmd.ExecuteScalar
                If permohonanID = Nothing Then
                    Debug.WriteLine("ERROR(Save->permohonan): PermohonanID is nothing")
                Else
                    If insertHistroyKeluarga(permohonanID, totalAnak, jenisRumahSebelum, mulaMenetap) Then
                        If insertLogPermohonan(permohonanID) Then
                            Return True
                        Else
                            Debug.WriteLine("ERROR(Save->insertLogPermohonan)")
                            Return False
                        End If
                    Else
                        Debug.WriteLine("ERROR(Save->insertHistoryAnak)")
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
        'If cbPerakuanPemohon.Checked Then

        'Else
        '    lblCheckBoxAlert.Text = "Sila setuju dengan perkara berikut."
        '    lblCheckBoxAlert.Attributes.CssStyle.Add("color", "red")
        '    lblCheckBoxAlert.Visible = True
        'End If
        Try
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
        Catch ex As Exception
            MsgTop.Attributes("class") = "errorMsg"
            strlbl_top.Text = strSysErrorAlert
            MsgBottom.Attributes("class") = "errorMsg"
            strlbl_bottom.Text = strSysErrorAlert & "<br>" & ex.Message
            Debug.WriteLine("ERROR: " & ex.Message)
        End Try
    End Sub

    Private Sub Refresh_ServerClick(sender As Object, e As EventArgs) Handles Refresh.ServerClick
        Response.Redirect("Permohonan.Kuarters.aspx")
    End Sub

    Private Sub btnTambahRow_Click(sender As Object, e As EventArgs) Handles btnTambahRow.Click

        If insertMaklumatAnak() Then
            txtNamaAnak.Text = ""
            txtICAnak.Text = ""
            If readMaklumatAnak() Then
                Debug.WriteLine("OK(btnTambahRow): WRITE OK, READ OK")
            Else

            End If
        Else
        End If

    End Sub

    Private Function insertMaklumatAnak() As Boolean
        Dim namaAnak = txtNamaAnak.Text
        Dim icAnak = txtICAnak.Text
        Dim umurAnak = icToAge(icAnak)
        Dim penggunaID = pengguna_id.Value
        Dim strRet As String

        strRet = oCommon.ExecuteSQL("INSERT INTO spk_anak(pengguna_id,anak_nama,anak_ic,anak_umur) VALUES(" & penggunaID & ",'" & namaAnak & "','" & icAnak & "','" & umurAnak & "')")
        If strRet = "0" Then
            calcPoin()
            Return True
        Else
            Debug.WriteLine("ERROR (insertMaklumatAnak)")
            Return False
        End If
    End Function

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
                jumlahPoint = loadPoints(dataAnak)
                dataAnak = dataAnak
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

    Private Function calcPoin()
        Dim penggunaID = pengguna_id.Value
        Dim totalPoin
        Dim markahAnak
        Dim totalMarkahAnak
        Dim markahPangkat
        markahAnak = (oCommon.ExecuteSQL("select count(*) from (select anak_umur from spk_anak where anak_umur <18 and pengguna_id = '" & penggunaID & "') A")) * 5
        markahPangkat = oCommon.ExecuteSQL("select B.pangkat_mata from spk_pengguna A 
                                            left join spk_pangkat B on A.pangkat_id = B.pangkat_id
                                            where A.pengguna_id = '" & penggunaID & "'")
        If markahAnak > 20 Then
            totalMarkahAnak = 20
            totalPoin = markahPangkat + totalMarkahAnak
        ElseIf markahAnak <= 20 Then
            totalMarkahAnak = markahAnak
            totalPoin = markahPangkat + totalMarkahAnak

            strRet = oCommon.ExecuteSQL("insert into spk_permohonan (permohonan_mata) values ('" & totalPoin & "')  where pengguna_id = '" & penggunaID & "'")
            If strRet = 0 Then

                Debug.WriteLine(0)

            ElseIf strRet = 1 Then

                Debug.WriteLine(0)

            End If
        End If
    End Function

    Private Function icToAge(ByVal ic As String) As Integer
        Dim year = ic.Substring(0, 2)
        Dim month = ic.Substring(2, 2)
        Dim day = ic.Substring(4, 2)
        Dim dob_string = day & "/" & month & "/" & year
        Debug.WriteLine("icToAge: " & dob_string)
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
    Private Sub insertHistoryAnak(ByVal dataAnak As DataSet)
        For i As Integer = 0 To dataAnak.Tables(0).Rows.Count - 1
            Dim namaAnak = dataAnak.Tables(0).Rows(i)(2).ToString()
            Dim kpAnak = dataAnak.Tables(0).Rows(i)(3).ToString()
            Dim query As String
            Dim strRet As String
            query = String.Format("INSERT INTO spk_historyAnak(permohonan_id, historyAnak_nama, historyAnak, ic) VALUES({0},'{1}','{2}')", permohonanID, namaAnak, kpAnak)
            strRet = oCommon.ExecuteSQL(query)
            If strRet = "0" Then
                Debug.WriteLine("OK:" & namaAnak)
            Else
                Debug.WriteLine("Error(saveHistoryAnak): Failed to save(" & namaAnak & ", idx:" & i & ")")
            End If
        Next
    End Sub

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

    Private Function insertLogPermohonan(ByVal permohonanID As Integer) As Boolean
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("
                INSERT INTO spk_logPermohonan (
                      pengguna_id
                    , permohonan_id
                    , log_tarikh 
                    , log_status
                ) VALUES (
                    " & pengguna_id.Value & "
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
	                    , " & pengguna_id.Value & "
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
End Class