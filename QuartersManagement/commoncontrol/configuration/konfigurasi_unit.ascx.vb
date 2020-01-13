Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Web.UI.HtmlControls

Public Class konfigurasi_unit
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            load_page()
        End If
    End Sub

    Protected Sub load_page()
        check_session()
        load_units()
        load_pangkalan()
    End Sub

    Protected Sub check_session()
        If Session("user_id") = Nothing Then
            Response.Redirect("default.aspx")
        Else
            If Session("user_type").Equals("ADMIN") Then

            Else
                Response.Redirect("default.aspx")
            End If
        End If
    End Sub

    Protected Function GetSQL()
        Dim query As String = ""
        Dim tempQuery As String = "SELECT 
                A.unit_id
	            , A.unit_nama
	            , (A.unit_blok + '-' + A.unit_tingkat + '-' + unit_nombor) AS 'unit_nama_lain'
	            , B.kuarters_nama
	            , C.pangkalan_nama
            FROM 
	            spk_unit A
	            LEFT JOIN spk_kuarters B ON B.kuarters_id = A.kuarters_id
	            LEFT JOIN spk_pangkalan C ON C.pangkalan_id = B.pangkalan_id"
        Dim whereQuery As String = " WHERE A.unit_id IS NOT NULL"
        Dim orderQuery As String = " ORDER BY C.pangkalan_nama ASC;"

        If ddlPangkalan.SelectedIndex > 0 Then
            whereQuery += " AND C.pangkalan_id = @pangkalanID"
        End If

        If ddlKuarters.SelectedIndex > 0 Then
            whereQuery += " AND B.kuarters_id = @kuartersID"
        End If

        If tbCari.Text.Length > 0 Then
            whereQuery += " AND (A.unit_nama LIKE '%@carian%')
                            OR ((A.unit_blok + '-' + A.unit_tingkat + '-' + A.unit_nombor) LIKE '%@carian%')"
        End If
        query = tempQuery & whereQuery & orderQuery
        Return query
    End Function

    Protected Sub load_units()
        Dim query = GetSQL()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand(query, conn)
                If tbCari.Text.Length > 0 Then
                    cmd.Parameters.Add("@carian", SqlDbType.Text).Value = tbCari.Text
                End If
                If ddlKuarters.SelectedIndex > 0 Then
                    cmd.Parameters.Add("@kuartersID", SqlDbType.Int).Value = ddlKuarters.SelectedValue
                End If
                If ddlPangkalan.SelectedIndex > 0 Then
                    cmd.Parameters.Add("@pangkalanID", SqlDbType.Int).Value = ddlPangkalan.SelectedValue
                End If
                Try
                    conn.Open()
                    Dim ds As New DataSet
                    Dim sda As New SqlDataAdapter(cmd)
                    sda.Fill(ds)
                    datRespondent.DataSource = ds
                    datRespondent.DataBind()
                Catch ex As Exception
                    Debug.WriteLine("Error(load_units-konfigurasi_unit: 37): " & ex.Message)
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Sub

    Protected Sub load_pangkalan()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("SELECT pangkalan_nama, pangkalan_id FROM spk_pangkalan", conn)
                Try
                    conn.Open()
                    Using sda As New SqlDataAdapter(cmd)
                        Dim ds As New DataSet
                        sda.Fill(ds)
                        ddlPangkalan.DataSource = ds
                        ddlInsertPangkalan.DataSource = ds
                        ddlPangkalan.DataTextField = "pangkalan_nama"
                        ddlInsertPangkalan.DataTextField = "pangkalan_nama"
                        ddlPangkalan.DataValueField = "pangkalan_id"
                        ddlInsertPangkalan.DataValueField = "pangkalan_id"
                        ddlPangkalan.DataBind()
                        ddlInsertPangkalan.DataBind()
                        ddlPangkalan.Items.Insert(0, New ListItem("-- PILIH --", Nothing))
                        ddlPangkalan.SelectedIndex = 0
                        ddlInsertPangkalan.Items.Insert(0, New ListItem("-- PILIH --", Nothing))
                        ddlInsertPangkalan.SelectedIndex = 0
                    End Using
                Catch ex As Exception

                End Try
            End Using
        End Using
    End Sub

    Protected Sub load_kuarters()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("SELECT kuarters_nama, kuarters_id from spk_kuarters WHERE pangkalan_id = @pangkalanID;", conn)
                cmd.Parameters.Add("@pangkalanID", SqlDbType.Int).Value = ddlPangkalan.SelectedValue
                Try
                    conn.Open()
                    Using sda As New SqlDataAdapter(cmd)
                        Dim ds As New DataSet
                        sda.Fill(ds)
                        ddlKuarters.DataSource = ds
                        ddlKuarters.DataTextField = "kuarters_nama"
                        ddlKuarters.DataValueField = "kuarters_id"
                        ddlKuarters.DataBind()
                        ddlKuarters.Items.Insert(0, New ListItem("-- PILIH --", Nothing))
                        ddlKuarters.SelectedIndex = 0
                    End Using
                Catch ex As Exception
                    Debug.WriteLine("Error(load_kuarters-konfigrasi_unit: 138): " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Protected Sub load_form_kuarters()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("SELECT kuarters_nama, kuarters_id from spk_kuarters WHERE pangkalan_id = @PangkalanID;", conn)
                cmd.Parameters.Add("@PangkalanID", SqlDbType.Int).Value = ddlInsertPangkalan.SelectedValue
                Try
                    conn.Open()
                    Using sda As New SqlDataAdapter(cmd)
                        Dim ds As New DataSet
                        sda.Fill(ds)
                        ddlInsertKuarters.DataSource = ds
                        ddlInsertKuarters.DataTextField = "kuarters_nama"
                        ddlInsertKuarters.DataValueField = "kuarters_id"
                        ddlInsertKuarters.DataBind()
                        ddlInsertKuarters.Items.Insert(0, New ListItem("-- PILIH --", Nothing))
                        ddlInsertKuarters.SelectedIndex = 0
                    End Using
                Catch ex As Exception
                    Debug.WriteLine("Error(load_kuarters-konfigrasi_unit: 138): " & ex.Message)
                End Try
            End Using
        End Using
    End Sub
    Private Sub ddlPangkalan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPangkalan.SelectedIndexChanged
        If ddlPangkalan.SelectedIndex > 0 Then
            load_kuarters()
            ddlKuarters.Enabled = True
        End If
        load_units()
    End Sub

    Private Sub ddlKuarters_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKuarters.SelectedIndexChanged
        load_units()
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        load_units()
    End Sub

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim unitID = datRespondent.DataKeys(e.RowIndex).Values("unit_id").ToString
        If Not unitID.Equals("") Then
            Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using cmd As New SqlCommand("DELETE spk_unit WHERE unit_id = @UnitID", conn)
                    cmd.Parameters.Add("@UnitID", SqlDbType.Int).Value = unitID
                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        Debug.WriteLine("Error(datRespondent_RowDeleting-konfigurati_unit: 173): " & ex.Message)
                    Finally
                        conn.Close()
                        Label1.Attributes("class") = "successMsg"
                        Label1.Text = "Padam Unit Berjaya"
                        Label2.Attributes("class") = "successMsg"
                        Label2.Text = "Padam Unit Berjaya"
                        load_units()
                    End Try
                End Using
            End Using
        End If
    End Sub

    Protected Function unit_status(ByVal status As String) As String
        If Not status.Equals("") Then
            Select Case status
                Case "Available"
                    Return "Tiada Penghuni"
                Case "On Hold"
                    Return "Dalam Proses Permohonan"
                Case "Occupied"
                    Return "Sedang Diduduki"
                Case "Not Available"
                    Return "Tidak Aktif"
                Case Else
                    Debug.WriteLine("Error(unit_status-konfigurasi_unit:190): ELSE case path")
                    Return "Tidak Aktif"
            End Select
        Else
            Return "-"
        End If
    End Function

    Private Sub open()
        viewConfig.ActiveViewIndex = 1
    End Sub

    Private Sub cancel()
        viewConfig.ActiveViewIndex = 0
    End Sub

    Private Sub CancelTop_ServerClick(sender As Object, e As EventArgs) Handles CancelTop.ServerClick
        cancel()
        Response.Redirect(Request.RawUrl)
        Session("tambah_unit") = Nothing
        Session("unit_id") = Nothing
    End Sub

    Private Sub CancelBottom_ServerClick(sender As Object, e As EventArgs) Handles CancelBottom.ServerClick
        cancel()
        Response.Redirect(Request.RawUrl)
        Session("tambah_unit") = Nothing
        Session("unit_id") = Nothing
    End Sub

    Private Function validate_insert() As Boolean
        If ddlInsertPangkalan.SelectedIndex > 0 Then
            If ddlInsertKuarters.SelectedIndex > 0 Then
                If panelBanglo.Visible.Equals(True) Then
                    If tbBangloNama.Text.Length > 0 Then
                        Return True
                    Else
                        message("ERROR", "SILA ISI NAMA BANGLO")
                        Return False
                    End If
                ElseIf panelTeres.Visible.Equals(True) Then
                    If tbTeresNoBaris.Text.Length > 0 Then
                        If tbTeresNoUnit.Text.Length > 0 Then
                            Return True
                        Else
                            message("ERROR", "SILA ISI LOT UNIT")
                            Return False
                        End If
                    Else
                        message("ERROR", "SILA ISI NO. UNIT")
                        Return False
                    End If
                ElseIf panelPangsapuri.Visible.Equals(True) Then
                    If tbPangsapuriBlok.Text.Length > 0 Then
                        If tbPangsapuriTingkat.Text.Length > 0 Then
                            If tbPangaspuriNoUnit.Text.Length > 0 Then
                                Return True
                            Else
                                message("ERROR", "SILA ISI NO. UNIT")
                                Return False
                            End If
                        Else
                            message("ERROR", "SILA ISI NO. TINGKAT")
                            Return False
                        End If
                    Else
                        message("ERROR", "SILA ISI NAMA BLOK")
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                message("ERROR", "SILA PILIH KUARTES")
                Return False
            End If
        Else
            message("ERROR", "SILA PILIH PANGKALAN")
            Return False
        End If
    End Function

    Private Function validate_update() As Boolean
        If panelBanglo.Visible.Equals(True) Then
            If tbBangloNama.Text.Length > 0 Then
                Return True
            Else
                message("ERROR", "SILA ISI NAMA BANGLO")
                Return False
            End If
        ElseIf panelTeres.Visible.Equals(True) Then
            If tbTeresNoBaris.Text.Length > 0 Then
                If tbTeresNoUnit.Text.Length > 0 Then
                    Return True
                Else
                    message("ERROR", "SILA ISI LOT UNIT")
                    Return False
                End If
            Else
                message("ERROR", "SILA ISI NO. UNIT")
                Return False
            End If
        ElseIf panelPangsapuri.Visible.Equals(True) Then
            If tbPangsapuriBlok.Text.Length > 0 Then
                If tbPangsapuriTingkat.Text.Length > 0 Then
                    If tbPangaspuriNoUnit.Text.Length > 0 Then
                        Return True
                    Else
                        message("ERROR", "SILA ISI NO. UNIT")
                        Return False
                    End If
                Else
                    message("ERROR", "SILA ISI NO. TINGKAT")
                    Return False
                End If
            Else
                message("ERROR", "SILA ISI NAMA BLOK")
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Private Sub message(ByVal msgType As String, ByVal msg As String)
        Select Case msgType
            Case "SUCCESS"
                MsgTop.Attributes("class") = "successMsg"
                MsgBottom.Attributes("class") = "successMsg"
            Case "ERROR"
                MsgTop.Attributes("class") = "errorMsg"
                MsgBottom.Attributes("class") = "errorMsg"
            Case Else
                Debug.WriteLine("Error(message-konfigurasi_unit): ELSE case message")
        End Select
        strlbl_top.Text = msg
        strlbl_bottom.Text = msg
    End Sub

    Private Sub tambahUnit_ServerClick(sender As Object, e As EventArgs) Handles tambahUnit.ServerClick
        load_pangkalan()
        Session("tambah_unit") = True
        open()
    End Sub

    Private Sub clear_form()
        ddlInsertKuarters.SelectedIndex = 0
        ddlInsertKuarters.SelectedIndex = 0
        tbBangloNama.Text = ""
        tbTeresNoBaris.Text = ""
        tbTeresNoUnit.Text = ""
        tbPangaspuriNoUnit.Text = ""
        tbPangsapuriTingkat.Text = ""
        tbPangsapuriBlok.Text = ""
    End Sub

    Private Sub ddlInsertPangkalan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlInsertPangkalan.SelectedIndexChanged
        If ddlInsertPangkalan.SelectedIndex > 0 Then
            load_form_kuarters()
            ddlInsertKuarters.Enabled = True
        End If
    End Sub

    Private Sub ddlInsertKuarters_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlInsertKuarters.SelectedIndexChanged
        If ddlInsertKuarters.SelectedIndex > 0 Then
            Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using cmd As New SqlCommand("SELECT A.jenisKuarters_nama FROM spk_jenisKuarters A JOIN spk_kuarters B ON B.jenisKuarters_id = A.jenisKuarters_id WHERE B.kuarters_id = @KuartersID;", conn)
                    cmd.Parameters.Add("@KuartersID", SqlDbType.Int).Value = ddlInsertKuarters.SelectedValue
                    Try
                        conn.Open()
                        Using sdr As SqlDataReader = cmd.ExecuteReader
                            If sdr.HasRows Then
                                panelMaklumatUnit.Visible = True
                                While sdr.Read()
                                    Dim jenisKuarters As String = sdr("jenisKuarters_nama")
                                    lblJenisKuarters.Text = jenisKuarters
                                    divJenisKuarters.Visible = True
                                    If jenisKuarters.Trim().Contains("BANGLO").Equals(True) Then
                                        panelPangsapuri.Visible = False
                                        panelTeres.Visible = False
                                        panelBanglo.Visible = True
                                    ElseIf jenisKuarters.Trim().Contains("TERES").Equals(True) Or jenisKuarters.Trim().Contains("BERKEMBAR") Then
                                        panelPangsapuri.Visible = False
                                        panelTeres.Visible = True
                                        panelBanglo.Visible = False
                                    ElseIf jenisKuarters.Trim().Contains("PANGSAPURI").Equals(True) Then
                                        panelPangsapuri.Visible = True
                                        panelTeres.Visible = False
                                        panelBanglo.Visible = False
                                    Else
                                        Debug.WriteLine("ELSE")
                                        Debug.WriteLine(jenisKuarters)
                                    End If
                                End While
                            Else
                                panelMaklumatUnit.Visible = False
                            End If
                        End Using
                    Catch ex As Exception
                        Debug.WriteLine("Error(ddl_insertKuarters_selectedIndexChange-konfigurasai_unit:389): " & ex.Message)
                    Finally
                        conn.Close()
                    End Try
                End Using
            End Using
        End If
    End Sub

    Private Function insert() As Boolean
        Dim banglo_nama As String = Nothing
        Dim teres_no_baris As String = Nothing
        Dim teres_no_unit As String = Nothing
        Dim pangsapuri_no_tingkat As String = Nothing
        Dim pangsapuri_no_blok As String = Nothing
        Dim pangsapuri_no_unit As String = Nothing
        If panelBanglo.Visible.Equals(True) Then
            banglo_nama = tbBangloNama.Text
        ElseIf panelTeres.Visible.Equals(True) Then
            teres_no_baris = tbTeresNoBaris.Text
            teres_no_unit = tbTeresNoUnit.Text
        ElseIf panelPangsapuri.Visible.Equals(True) Then
            pangsapuri_no_blok = tbPangsapuriBlok.Text
            pangsapuri_no_tingkat = tbPangsapuriTingkat.Text
            pangsapuri_no_unit = tbPangaspuriNoUnit.Text
        Else
            message("ERROR", "SILA PILIH KUARTERS TERLEBIH DAHULU")
            Return False
        End If

        If validate_insert() Then
            Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using cmd As New SqlCommand("INSERT INTO spk_unit VALUES(@KuartersID, @PangkalanID, @UnitNama, @UnitNo, @UnitTingkat, @UnitBlok, @UnitStatus);", conn)
                    cmd.Parameters.Add("@KuartersID", SqlDbType.Int).Value = ddlInsertKuarters.SelectedValue
                    cmd.Parameters.Add("@PangkalanID", SqlDbType.Int).Value = ddlInsertPangkalan.SelectedValue
                    cmd.Parameters.Add("@UnitNama", SqlDbType.NVarChar, 50).Value = IIf(panelBanglo.Visible.Equals(True), banglo_nama, "")
                    cmd.Parameters.Add("@UnitNo", SqlDbType.NVarChar, 50).Value = IIf(panelBanglo.Visible.Equals(True), "", IIf(panelPangsapuri.Visible.Equals(True), pangsapuri_no_unit, teres_no_unit))
                    cmd.Parameters.Add("@UnitTingkat", SqlDbType.NVarChar, 50).Value = IIf(panelBanglo.Visible.Equals(True), "", IIf(panelPangsapuri.Visible.Equals(True), pangsapuri_no_tingkat, teres_no_baris))
                    cmd.Parameters.Add("@UnitBlok", SqlDbType.NVarChar, 50).Value = IIf(panelBanglo.Visible.Equals(True), "", IIf(panelPangsapuri.Visible.Equals(True), pangsapuri_no_blok, ""))
                    cmd.Parameters.Add("@UnitStatus", SqlDbType.NVarChar, 50).Value = ddlStatusUnit.SelectedValue

                    Try
                        conn.Open()
                        If Session("tambah_unit") IsNot Nothing And Session("tambah_unit").Equals(True) Then
                            cmd.ExecuteNonQuery()
                            Return True
                        Else
                            Debug.WriteLine("ERROR(insert: 452): SESSION(tambah_unit) NOT EXIST")
                            Return False
                        End If
                    Catch ex As Exception
                        Debug.WriteLine("ERROR(insert-konfigurasi_unit:458): " & ex.Message)
                        Return False
                    Finally
                        conn.Close()
                    End Try

                End Using
            End Using
        Else
            Return False
        End If
    End Function

    Private Sub SaveTop_ServerClick(sender As Object, e As EventArgs) Handles SaveTop.ServerClick
        If insert() Then
            viewConfig.ActiveViewIndex = 0
            Response.Redirect(Request.RawUrl)
            message("SUCCESS", "Berjaya simpan unit baru.")
        End If
    End Sub

    Private Sub SaveBottom_ServerClick(sender As Object, e As EventArgs) Handles SaveBottom.ServerClick
        If insert() Then
            viewConfig.ActiveViewIndex = 0
            Response.Redirect(Request.RawUrl)
            message("SUCCESS", "Berjaya simpan unit baru.")
        End If
    End Sub

    Private Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles datRespondent.RowCommand
        If e.CommandName.Equals("edit_unit") Then
            Dim unitID = e.CommandArgument
            Session("unit_id") = unitID
            Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using cmd As New SqlCommand("SELECT 
	                a.unit_nama
	                , (a.unit_blok +'-'+a.unit_tingkat+'-'+a.unit_nombor) unit_nama_lain
	                , a.unit_blok
	                , a.unit_tingkat
	                , a.unit_nombor
                    , a.unit_status
                    , B.kuarters_id
	                , B.kuarters_nama
                    , C.jenisKuarters_nama
                    , B.pangkalan_id
	                , D.pangkalan_nama
                FROM 
	                spk_unit A
	                JOIN spk_kuarters B ON B.kuarters_id = A.kuarters_id
	                JOIN spk_jenisKuarters C ON C.jenisKuarters_id = B.jenisKuarters_id
	                JOIN spk_pangkalan D ON D.pangkalan_id = A.pangkalan_id
                WHERE unit_id = @UnitID", conn)
                    cmd.Parameters.Add("@UnitID", SqlDbType.Int).Value = unitID
                    Try
                        conn.Open()
                        Dim sdr As SqlDataReader = cmd.ExecuteReader
                        If sdr.HasRows Then
                            ddlInsertPangkalan.Visible = False
                            ddlInsertKuarters.Visible = False
                            lblNamaPangkalan.Visible = True
                            lblNamaKuarters.Visible = True
                            panelMaklumatUnit.Visible = True
                            panelBanglo.Visible = False
                            panelPangsapuri.Visible = False
                            panelTeres.Visible = False
                            While (sdr.Read)
                                lblNamaPangkalan.Text = sdr("pangkalan_nama").ToString
                                lblNamaKuarters.Text = sdr("kuarters_nama").ToString
                                ddlStatusUnit.SelectedValue = sdr("unit_status")
                                If sdr("jenisKuarters_nama").ToString.Contains("BANGLO") Then
                                    tbBangloNama.Text = sdr("unit_nama")
                                    panelBanglo.Visible = True
                                ElseIf sdr("jenisKuarters_nama").ToString.Contains("TERES") Or sdr("jenisKuarters_nama").ToString.Contains("BERKEMBAR") Then
                                    tbTeresNoBaris.Text = sdr("unit_tingkat")
                                    tbTeresNoUnit.Text = sdr("unit_nombor")
                                    panelTeres.Visible = True
                                ElseIf sdr("jenisKuarters_nama").ToString.Contains("PANGSAPURI") Then
                                    tbPangsapuriBlok.Text = sdr("unit_blok")
                                    tbPangsapuriTingkat.Text = sdr("unit_tingkat")
                                    tbPangaspuriNoUnit.Text = sdr("unit_nombor")
                                    panelPangsapuri.Visible = True
                                Else
                                    Debug.WriteLine("ERROR(RowCommand-konfigurasi_unit): Tiada Jenis Kuarters")
                                    message("ERROR", "SILA HUBUNGI PIHAK ADMIN ANDA.<br/> Tiada Jenis Kuarters")
                                End If
                                'tbNamaUnit.Text = IIf(sdr("unit_nama").ToString().Equals(""), sdr("unit_nama_lain"), sdr("unit_nama").ToString())
                                'tbBlok.Text = sdr("unit_blok")
                                'tbTingkat.Text = sdr("unit_tingkat")
                                'tbNoUnit.Text = sdr("unit_nombor")
                            End While
                        End If
                        open()
                    Catch ex As Exception
                        Debug.WriteLine("Error(datRespondent_RowCommand-konfigurasi_unit: 190): " & ex.Message)
                    Finally
                        conn.Close()
                        SaveBottom.Visible = False
                        UpdateBottom.Visible = True
                        SaveTop.Visible = False
                        UpdateTop.Visible = True
                    End Try
                End Using
            End Using
        End If
    End Sub

    Private Function update()
        Dim banglo_nama As String = Nothing
        Dim teres_no_baris As String = Nothing
        Dim teres_no_unit As String = Nothing
        Dim pangsapuri_no_tingkat As String = Nothing
        Dim pangsapuri_no_blok As String = Nothing
        Dim pangsapuri_no_unit As String = Nothing

        If validate_update() Then
            Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using cmd As New SqlCommand("UPDATE 
                    spk_unit 
                SET 
                    unit_nama = @UnitNama
                    , unit_nombor = @UnitNo
                    , unit_tingkat = @UnitTingkat
                    , unit_blok = @UnitBlok
                    , unit_status = @UnitStatus
                 WHERE unit_id = @UnitID;")
                    cmd.Connection = conn

                    If panelBanglo.Visible.Equals(True) Then
                        banglo_nama = tbBangloNama.Text
                    ElseIf panelTeres.Visible.Equals(True) Then
                        teres_no_baris = tbTeresNoBaris.Text
                        teres_no_unit = tbTeresNoUnit.Text
                    ElseIf panelPangsapuri.Visible.Equals(True) Then
                        pangsapuri_no_blok = tbPangsapuriBlok.Text
                        pangsapuri_no_tingkat = tbPangsapuriTingkat.Text
                        pangsapuri_no_unit = tbPangaspuriNoUnit.Text
                    End If

                    Try
                        cmd.Parameters.Add("@UnitNama", SqlDbType.NVarChar, 50).Value = IIf(panelBanglo.Visible.Equals(True), banglo_nama, "")
                        cmd.Parameters.Add("@UnitNo", SqlDbType.NVarChar, 50).Value = IIf(panelBanglo.Visible.Equals(True), "", IIf(panelPangsapuri.Visible.Equals(True), pangsapuri_no_unit, teres_no_unit))
                        cmd.Parameters.Add("@UnitTingkat", SqlDbType.NVarChar, 50).Value = IIf(panelBanglo.Visible.Equals(True), "", IIf(panelPangsapuri.Visible.Equals(True), pangsapuri_no_tingkat, teres_no_baris))
                        cmd.Parameters.Add("@UnitBlok", SqlDbType.NVarChar, 50).Value = IIf(panelBanglo.Visible.Equals(True), "", IIf(panelPangsapuri.Visible.Equals(True), pangsapuri_no_blok, ""))
                        cmd.Parameters.Add("@UnitStatus", SqlDbType.NVarChar, 50).Value = ddlStatusUnit.SelectedValue
                        conn.Open()
                        cmd.ExecuteNonQuery()
                        Return True
                    Catch ex As Exception
                        Return False
                    Finally
                        conn.Close()
                    End Try
                End Using
            End Using
        Else
            Return False
        End If
    End Function

    Private Sub UpdateTop_ServerClick(sender As Object, e As EventArgs) Handles UpdateTop.ServerClick
        If update() Then
            viewConfig.ActiveViewIndex = 0
            Response.Redirect(Request.RawUrl)
            message("SUCCESS", "Berjaya Ubah Unit.")
        End If
    End Sub

    Private Sub UpdateBottom_ServerClick(sender As Object, e As EventArgs) Handles UpdateBottom.ServerClick
        If update() Then
            viewConfig.ActiveViewIndex = 0
            Response.Redirect(Request.RawUrl)
            message("SUCCESS", "Berjaya Ubah Unit.")
        End If
    End Sub
End Class