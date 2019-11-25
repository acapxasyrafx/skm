Imports System.Data.SqlClient

Public Class maklumat_pemohon_menunggu
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
            'ddlunit_load()
            readMaklumatAnak()

        Catch ex As Exception

        End Try
    End Sub
    Private Function readMaklumatAnak() As Boolean
        Dim penggunaID = pengguna_id.Value
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

    Private Sub loadUser()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("
            SELECT
                A.permohonan_id
                ,   D.pengguna_id
                ,   D.pengguna_nama
                ,   D.pengguna_jantina
	            ,   D.pengguna_tarikh_lahir
                ,   F.pangkat_nama
                ,   D.pengguna_no_tentera
                ,   D.pengguna_mula_perkhidmatan
                ,   D.pengguna_tamat_perkhidmatan
                ,   A.permohonan_no_permohonan
                ,   A.kuarters_id
                ,   B.kuarters_nama
                ,   C.pangkalan_nama
                ,   A.permohonan_tarikh
                ,   A.permohonan_status
                ,   A.permohonan_sub_status
                ,   A.permohonan_mata
	            ,	E.historyKeluarga_tempat_tinggal
	            ,	E.historyKeluarga_tarikh_mula
            FROM 
                spk_permohonan A
                JOIN spk_kuarters B ON B.kuarters_id = A.kuarters_id
                JOIN spk_pangkalan C ON C.pangkalan_id = B.pangkalan_id
                JOIN spk_pengguna D ON D.pengguna_id = A.pengguna_id
                JOIN spk_historyKeluarga E ON E.permohonan_id = A.permohonan_id
                JOIN spk_pangkat F ON F.pangkat_id = D.pangkat_id	
            WHERE A.permohonan_id = '" & Request.QueryString("uid") & "';",
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
                        lblKuartersDipohon.Text = reader("kuarters_nama")
                        lblTarikhAkhirBerkhidmat.InnerText = reader("pengguna_tamat_perkhidmatan")
                        lblJenisPenempatan.InnerText = reader("historyKeluarga_tempat_tinggal")
                        lbltarikhPenempatan.InnerText = reader("historyKeluarga_tarikh_mula")
                        If checkKekosongan(Integer.Parse(reader("kuarters_id"))) Then
                            lblStatusKuarter.Text = "ADA KEKOSONGAN"
                            pnlPemilihanUnit.Visible = True
                        Else
                            lblStatusKuarter.Text = "TIADA KEKOSONGAN"
                            pnlCadanganKuarters.Visible = True
                        End If
                    Else
                        Debug.Write("Error(loadUser): CANNOT READ")
                    End If
                Else
                    Debug.Write("Error(loadUser): NO ROWS")
                End If
            Catch ex As Exception
                Debug.WriteLine("ERROR(loadUser): " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    'Private Sub ddlunit_load()
    '    Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
    '        Dim cmd As New SqlCommand("select a.kuarters_id as kuarters_id ,a.kuarters_nama as kuarters_nama from spk_kuarters a 
    '                                        ", conn)
    '        Dim ds As New DataSet
    '        Dim dt As New DataSet
    '        Dim dr As New DataSet

    '        Try
    '            conn.Open()

    '            Dim da As New SqlDataAdapter(cmd)
    '            da.Fill(ds)
    '            ddlcadanganUnit1.DataSource = ds
    '            ddlcadanganUnit1.DataTextField = "kuarters_nama"
    '            ddlcadanganUnit1.DataValueField = "kuarters_id"
    '            ddlcadanganUnit1.DataBind()

    '            Dim db As New SqlDataAdapter(cmd)
    '            db.Fill(dt)
    '            ddlcadanganUnit2.DataSource = dt
    '            ddlcadanganUnit2.DataTextField = "kuarters_nama"
    '            ddlcadanganUnit2.DataValueField = "kuarters_id"
    '            ddlcadanganUnit2.DataBind()

    '            Dim dc As New SqlDataAdapter(cmd)
    '            dc.Fill(dr)
    '            ddlcadanganUnit3.DataSource = dr
    '            ddlcadanganUnit3.DataTextField = "kuarters_nama"
    '            ddlcadanganUnit3.DataValueField = "kuarters_id"
    '            ddlcadanganUnit3.DataBind()
    '        Catch ex As Exception
    '            Debug.Write("ERROR: " & ex.Message)
    '        Finally
    '            conn.Close()
    '        End Try
    '    End Using
    'End Sub

    'Protected Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs)
    '    Try

    '        If (e.CommandName = "Cadangan") Then
    '            Dim strCID = e.CommandArgument.ToString

    '            Try
    '                Dim Getid = oCommon.ExecuteSQL("select permohonan_id from spk_permohonan where pengguna_id = '" & Request.QueryString("uid") & "'")
    '                strSQL = "INSERT INTO spk_cadanganUnit (permohonan_id,unit_id1,unit_id2,unit_id3) VALUES ('" & Getid & "','" & ddlcadanganUnit1.SelectedValue.ToString & "','" & ddlcadanganUnit2.SelectedValue.ToString & "','" & ddlcadanganUnit3.SelectedIndex.ToString & "')"
    '                strRet = oCommon.ExecuteSQL(strSQL)
    '                If strRet = "0" Then
    '                    strlbl_bottom.Text = "Cadangan Kuarters Sudah Dimasukkan"

    '                End If
    '            Catch ex As Exception
    '                MsgTop.Attributes("class") = "errorMsg"
    '                strlbl_top.Text = strSysErrorAlert
    '                MsgBottom.Attributes("class") = "errorMsg"
    '                strlbl_bottom.Text = strSysErrorAlert & "<br>" & ex.Message

    '            End Try

    '        End If

    '    Catch ex As Exception
    '        MsgBottom.Attributes("class") = "errorMsg"
    '        strlbl_bottom.Text = strSysErrorAlert & "<br>" & ex.Message
    '    End Try
    'End Sub

    Private Sub TerimaPermohonanKuarters_Click(sender As Object, e As EventArgs) Handles TerimaPermohonanKuarters.Click
        Dim Getid = oCommon.ExecuteSQL("select permohonan_id from spk_permohonan where pengguna_id = '" & Request.QueryString("uid") & "'")
        strSQL = "UPDATE spk_permohonan SET permohonan_sub_status = 'PERMOHONAN KUARTERS DITERIMA, MENANTI PEMBERIAN UNIT' WHERE permohonan_id = '" & Getid & "' "
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            strlbl_bottom.Text = "Cadangan Kuarters Sudah Dimasukkan"

        End If
    End Sub

    Private Function checkKekosongan(ByVal kuartersID As Integer) As Boolean
        Dim jumlahKekosongan As Integer = 0
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("SELECT COUNT(*) jumlah_kekosongan FROM spk_unit WHERE unit_status = 'Available' AND kuarters_id = @kuartersID;")
                cmd.Connection = conn
                cmd.Parameters.Add("@kuartersID", SqlDbType.Int).Value = kuartersID
                Try
                    conn.Open()
                    jumlahKekosongan = cmd.ExecuteScalar
                Catch ex As Exception
                    Debug.WriteLine("Error(checkKekosongan): " & ex.Message)
                    Return False
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
        lblKekosonganUnit.Text = jumlahKekosongan
        If jumlahKekosongan > 0 Then
            Debug.WriteLine("checkKekosongan: " & jumlahKekosongan)
            loadUnitAvailable(kuartersID)
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub loadUnitAvailable(ByVal kuartersID As Integer)
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("SELECT 
                    unit_id, 
                    (unit_blok + '-' + unit_tingkat + '-' + unit_nombor) AS nama_unit 
                FROM 
                    spk_unit 
                WHERE 
                    kuarters_id = @kuartersID AND unit_status = 'Available';")
                Dim ds As New DataSet
                cmd.Connection = conn
                cmd.Parameters.Add("@kuartersID", SqlDbType.Int).Value = kuartersID
                Try
                    conn.Open()
                    Dim da As New SqlDataAdapter(cmd)
                    da.Fill(ds, "AnyTable")
                    ddlUnitKuarters.DataSource = ds
                    ddlUnitKuarters.DataValueField = "unit_id"
                    ddlUnitKuarters.DataTextField = "nama_unit"
                    ddlUnitKuarters.DataBind()
                    ddlUnitKuarters.Items.Insert(0, New ListItem("-- SILA PILIH --", String.Empty))
                Catch ex As Exception
                    Debug.WriteLine("Error(loadUnitAvailable): " & ex.Message)
                Finally
                    conn.Close()
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

    Private Sub btnSimpanTawaranUnit_Click(sender As Object, e As EventArgs) Handles btnSimpanTawaranUnit.Click
        If ddlUnitKuarters.SelectedIndex > 0 Then
            Dim query As String = "
            UPDATE spk_permohonan
            SET 
                permohonan_status = 'PERMOHONAN DITERIMA',
                unit_id = " & ddlUnitKuarters.SelectedValue & "
            WHERE permohonan_id = " & Request.QueryString("uid") & ";"
            strRet = oCommon.ExecuteSQL(query)
            If strRet = "0" Then
                Response.Redirect("Senarai.Permohonan.Menunggu.aspx?P=Pengurusan%20Pentadbiran%20>%20Senarai%20Permohonan%20>%20Senarai%20Permohonan%20Menunggu")
            End If
        Else
            Debug.WriteLine("Error(btnSimpanTawaranUnit): UNIT TAK DIPILIH")
        End If

    End Sub
End Class