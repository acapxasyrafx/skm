Imports System.Data.SqlClient

Public Class konfigurasi_kuarters
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            load_page
        End If
    End Sub

    Private Sub load_page()
        load_pangkalan()
        load_jenis_kuarters()
        load_kuarters()
    End Sub

    Private Sub load_pangkalan()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("SELECT * FROM spk_pangkalan;", conn)
                Try
                    conn.Open()
                    Using sda As New SqlDataAdapter(cmd)
                        Dim ds As New DataSet
                        sda.Fill(ds)
                        ddlPangkalan.DataSource = ds
                        ddlPangkalan.DataValueField = "pangkalan_id"
                        ddlPangkalan.DataTextField = "pangkalan_nama"
                        ddlPangkalan.DataBind()
                        ddlPangkalan.Items.Insert(0, New ListItem("-- PILIH --", String.Empty))
                    End Using
                Catch ex As Exception
                    Debug.WriteLine("ERROR(load_pangkalan-konfigurasi-kuarters: 48): " & ex.Message)
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Sub

    Private Sub load_jenis_kuarters()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("SELECT * FROM spk_jenisKuarters;", conn)
                Try
                    conn.Open()
                    Using sda As New SqlDataAdapter(cmd)
                        Dim ds As New DataSet
                        sda.Fill(ds)
                        ddlJenisKuarters.DataSource = ds
                        ddlJenisKuarters.DataValueField = "jenisKuarters_id"
                        ddlJenisKuarters.DataTextField = "jenisKuarters_nama"
                        ddlJenisKuarters.DataBind()
                        ddlJenisKuarters.Items.Insert(0, New ListItem("-- PILIH --", String.Empty))
                    End Using
                Catch ex As Exception
                    Debug.WriteLine("ERROR(load_jenis_kuarters-konfigurasi-kuarters: 71): " & ex.Message)
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Sub

    Private Function getSQL() As String
        Dim tempQuery = "SELECT 
	        A.kuarters_id
	        , A.kuarters_nama
	        , C.jenisKuarters_nama
	        , A.kuarters_alamat
	        , B.pangkalan_nama
            , A.kuarters_negeri
        FROM 
	        spk_kuarters A
	        LEFT JOIN spk_pangkalan B ON B.pangkalan_id = A.pangkalan_id
	        LEFT JOIN spk_jenisKuarters C ON C.jenisKuarters_id = A.jenisKuarters_id"
        Dim whereQuery = " WHERE kuarters_nama IS NOT NULL"
        Dim orderQuery = " ORDER BY kuarters_nama ASC;"

        If ddlPangkalan.SelectedIndex > 0 Then
            whereQuery += " AND A.pangkalan_id = @PangkalanID"
        End If

        If ddlJenisKuarters.SelectedIndex > 0 Then
            whereQuery += " AND A.jenisKuarters_id = @JenisKuartersID"
        End If

        If tbCari.Text.Length > 0 Then
            whereQuery += " AND A.kuarters_nama LIKE '%@Cari%'"
        End If
        Dim query = tempQuery & whereQuery & orderQuery
        Return query
    End Function

    Private Sub load_kuarters()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand(getSQL, conn)
                If ddlPangkalan.SelectedIndex > 0 Then
                    cmd.Parameters.Add("@PangkalanID", SqlDbType.Int).Value = ddlPangkalan.SelectedValue
                End If

                If ddlJenisKuarters.SelectedIndex > 0 Then
                    cmd.Parameters.Add("@JenisKuartersID", SqlDbType.Int).Value = ddlJenisKuarters.SelectedValue
                End If

                If tbCari.Text.Length > 0 Then
                    cmd.Parameters.Add("@Cari", SqlDbType.NVarChar).Value = tbCari.Text
                End If
                Try
                    conn.Open()
                    Using sda As New SqlDataAdapter(cmd)
                        Dim ds As New DataSet
                        sda.Fill(ds)
                        datRespondent.DataSource = ds
                        datRespondent.DataBind()
                    End Using
                Catch ex As Exception
                    Debug.WriteLine("ERROR(load_kuarters-konfigurasi_kuarters:131)" & ex.Message)
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        load_kuarters()
    End Sub

    Private Sub open()
        configKuarters.ActiveViewIndex = 1
    End Sub

    Private Sub close()
        configKuarters.ActiveViewIndex = 0
    End Sub

    Private Sub load_form_negeri()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("SELECT 
                    config_parameter, 
                    config_value 
                FROM 
                    general_config 
                WHERE 
                    config_type = 'NEGERI' 
                ORDER BY 
                    config_parameter ASC;", conn)
                Try
                    conn.Open()
                    Using sda As New SqlDataAdapter(cmd)
                        Dim ds As New DataSet
                        sda.Fill(ds)
                        ddlFormNegeri.DataSource = ds
                        ddlFormNegeri.DataValueField = "config_value"
                        ddlFormNegeri.DataTextField = "config_parameter"
                        ddlFormNegeri.DataBind()
                        ddlFormNegeri.Items.Insert(0, New ListItem("-- PILIH --", String.Empty))
                    End Using
                Catch ex As Exception
                    Debug.WriteLine("ERROR(load_form_negeri-konfigurasi-kuarters: 169): " & ex.Message)
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Sub

    Private Sub load_form_pangkalan()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("SELECT pangkalan_id, pangkalan_nama FROM spk_pangkalan;", conn)
                Try
                    conn.Open()
                    Using sda As New SqlDataAdapter(cmd)
                        Dim ds As New DataSet
                        sda.Fill(ds)
                        ddlFormPangkalan.DataSource = ds
                        ddlFormPangkalan.DataValueField = "pangkalan_id"
                        ddlFormPangkalan.DataTextField = "pangkalan_nama"
                        ddlFormPangkalan.DataBind()
                        ddlFormPangkalan.Items.Insert(0, New ListItem("-- PILIH --", String.Empty))
                    End Using
                Catch ex As Exception
                    Debug.WriteLine("ERROR(load_form_pangkalan-konfigurasi-kuarters: 192): " & ex.Message)
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Sub

    Private Sub load_form_jenis_kuarters()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("SELECT jenisKuarters_id, jenisKuarters_nama FROM spk_jenisKuarters;", conn)
                Try
                    conn.Open()
                    Using sda As New SqlDataAdapter(cmd)
                        Dim ds As New DataSet
                        sda.Fill(ds)
                        ddlFormJenisKuarters.DataSource = ds
                        ddlFormJenisKuarters.DataValueField = "jenisKuarters_id"
                        ddlFormJenisKuarters.DataTextField = "jenisKuarters_nama"
                        ddlFormJenisKuarters.DataBind()
                        ddlFormJenisKuarters.Items.Insert(0, New ListItem("-- PILIH --", String.Empty))
                    End Using
                Catch ex As Exception
                    Debug.WriteLine("ERROR(load_form_jenis_kuarters-konfigurasi-kuarters: 215): " & ex.Message)
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Sub

    Private Sub NewKuarters_ServerClick(sender As Object, e As EventArgs) Handles NewKuarters.ServerClick
        load_form_negeri()
        load_form_pangkalan()
        load_form_jenis_kuarters()
        open()
    End Sub

    Private Sub ddlFormJenisKuarters_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFormJenisKuarters.SelectedIndexChanged

    End Sub

    Private Sub message_form(ByVal msgType As String, ByVal msgText As String)
        If msgType.Equals("ERROR") Then
            message_top.Attributes("class") = "errorMsg"
            message_bottom.Attributes("class") = "errorMsg"
            strlbl_top.Text = strSysErrorAlert & "<br/>" & msgText
            strlbl_bottom.Text = strSysErrorAlert & "<br/>" & msgText
        Else
            message_top.Attributes("class") = "successMsg"
            message_bottom.Attributes("class") = "successrMsg"
            strlbl_top.Text = msgText
            strlbl_bottom.Text = msgText
        End If
    End Sub

    Private Sub message_list(ByVal msgType As String, ByVal msgText As String)
        If msgType.Equals("ERROR") Then
            message_top.Attributes("class") = "errorMsg"
            message_bottom.Attributes("class") = "errorMsg"
            strlbl_top.Text = strSysErrorAlert & "<br/>" & msgText
            strlbl_bottom.Text = strSysErrorAlert & "<br/>" & msgText
        Else
            message_top.Attributes("class") = "successMsg"
            message_bottom.Attributes("class") = "successrMsg"
            strlbl_top.Text = msgText
            strlbl_bottom.Text = msgText
        End If
    End Sub

    Private Sub CancelTop_ServerClick(sender As Object, e As EventArgs) Handles CancelTop.ServerClick
        close()
        Response.Redirect(Request.RawUrl)
    End Sub

    Private Sub CancelBottom_ServerClick(sender As Object, e As EventArgs) Handles CancelBottom.ServerClick
        close()
        Response.Redirect(Request.RawUrl)
    End Sub

    Private Function validate_save_kuarters() As Boolean
        If ddlFormPangkalan.SelectedIndex > 0 Then
            If ddlFormJenisKuarters.SelectedIndex > 0 Then
                If tbFormNama.Text.Length > 0 Then
                    If tbFormAlamat.Text.Length > 0 Then
                        If tbFormPostcode.Text.Length > 0 And tbFormPostcode.Text.Length <= 5 Then
                            If tbFormBandar.Text.Length > 0 Then
                                If ddlFormNegeri.SelectedIndex > 0 Then
                                    If tbFormTelefon.Text.Length > 0 Then
                                        If tbFormNoFax.Text.Length > 0 Then
                                            If tbFormEmail.Text.Length > 0 Then
                                                Return True
                                            Else
                                                message_form("ERROR", "SILA MASUKKAN EMAIL")
                                                Return False
                                            End If
                                        Else
                                            message_form("ERROR", "SILA MASUKKAN NO. FAX")
                                            Return False
                                        End If
                                    Else
                                        message_form("ERROR", "SILA MASUKKAN NO. TELEFON")
                                        Return False
                                    End If
                                Else
                                    message_form("ERROR", "SILA PILIH NAMA NEGERI")
                                    Return False
                                End If
                            Else
                                message_form("ERROR", "SILA ISI NAMA BANDAR")
                                Return False
                            End If
                        Else
                            message_form("ERROR", "SILA ISI POSKOD DENGAN BETUL")
                            Return False
                        End If
                    Else
                        message_form("ERROR", "SILA ISI ALAMAT KUARTERS")
                        Return False
                    End If
                Else
                    message_form("ERROR", "SILA ISI NAMA KUARTERS")
                    Return False
                End If
            Else
                message_form("ERROR", "SILA PILIH JENIS KUARTERS")
                Return False
            End If
        Else
            message_form("ERROR", "SILA PILIH NAMA PANGKALAN")
            Return False
        End If
    End Function

    Private Function save_kuarters()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("
            INSERT INTO 
                spk_kuarters 
            VALUES(
                @JenisKuartersID
                , @PangkalanID
                , 'MALAYSIA'
                , @Negeri
                , @Bandar
                , @Poskod
                , @Nama
                , @Alamat
                , @Telefon
                , @Faks
                , @Emel
            )", conn)
                cmd.Parameters.Add("@JenisKuartersID", SqlDbType.Int).Value = ddlFormJenisKuarters.SelectedValue
                cmd.Parameters.Add("@PangkalanID", SqlDbType.Int).Value = ddlFormPangkalan.SelectedValue
                cmd.Parameters.Add("@Negeri", SqlDbType.NVarChar).Value = ddlFormNegeri.SelectedValue.ToUpper
                cmd.Parameters.Add("@Bandar", SqlDbType.NVarChar).Value = tbFormBandar.Text.ToUpper
                cmd.Parameters.Add("@Poskod", SqlDbType.NVarChar, 5).Value = tbFormPostcode.Text
                cmd.Parameters.Add("@Nama", SqlDbType.NVarChar).Value = tbFormNama.Text.ToUpper
                cmd.Parameters.Add("@Alamat", SqlDbType.NVarChar).Value = tbFormAlamat.Text.ToUpper
                cmd.Parameters.Add("@Telefon", SqlDbType.NVarChar, 10).Value = tbFormTelefon.Text
                cmd.Parameters.Add("@Faks", SqlDbType.NVarChar, 10).Value = tbFormNoFax.Text
                cmd.Parameters.Add("@Emel", SqlDbType.NVarChar).Value = tbFormEmail.Text
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Debug.Write("ERROR(save_kuarters-konfigurasi_kuarters:342): " & ex.Message)
                    message_form("ERROR", strSysErrorAlert & "<br/>" & ex.Message)
                    Return False
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Function

    Private Sub SaveTop_ServerClick(sender As Object, e As EventArgs) Handles SaveTop.ServerClick
        If validate_save_kuarters() Then
            If save_kuarters() Then
                close()
                Response.Redirect(Request.RawUrl)
            End If
        End If
    End Sub

    Private Sub SaveBottom_ServerClick(sender As Object, e As EventArgs) Handles SaveBottom.ServerClick
        If validate_save_kuarters() Then
            If save_kuarters() Then
                close()
                Response.Redirect(Request.RawUrl)
            End If
        End If
    End Sub

    Private Function read_kuarters() As Boolean
        If Session("kuarters_id") IsNot Nothing Then
            Debug.WriteLine("KUARTERS ID: " & Session("kuarters_id"))
            Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using cmd As New SqlCommand("SELECT * FROM spk_kuarters A WHERE A.kuarters_id = @KuartersID", conn)
                    cmd.Parameters.Add("@KuartersID", SqlDbType.Int).Value = Session("kuarters_id")
                    Try
                        conn.Open()
                        Using sdr As SqlDataReader = cmd.ExecuteReader
                            If sdr.HasRows Then
                                While sdr.Read
                                    hfKuartersID.Value = sdr("kuarters_id").ToString
                                    ddlFormPangkalan.SelectedValue = sdr("pangkalan_id").ToString
                                    hfPrevPangkalanID.Value = sdr("pangkalan_id").ToString
                                    ddlFormJenisKuarters.SelectedValue = sdr("jenisKuarters_id").ToString
                                    tbFormNama.Text = sdr("kuarters_nama").ToString
                                    tbFormAlamat.Text = sdr("kuarters_alamat").ToString
                                    tbFormPostcode.Text = sdr("kuarters_poskod").ToString
                                    tbFormBandar.Text = sdr("kuarters_bandar").ToString
                                    ddlFormNegeri.SelectedValue = sdr("kuarters_negeri").ToString
                                    tbFormTelefon.Text = sdr("kuarters_telefon").ToString
                                    tbFormNoFax.Text = sdr("kuarters_faks").ToString
                                    tbFormEmail.Text = sdr("kuarters_emel").ToString
                                End While
                                Return True
                            Else
                                Debug.Write("ERROR(read_kuarters-konfigurasi_kuarters:420): HAS NO ROWS")
                                Return False
                            End If
                        End Using
                    Catch ex As Exception
                        Debug.Write("ERROR(read_kuarters-konfigurasi_kuarters:409): " & ex.Message)
                        message_form("ERROR", strSysErrorAlert & "<br/>" & ex.Message)
                        Return False
                    Finally
                        conn.Close()
                    End Try
                End Using
            End Using
        Else
            Debug.Write("ERROR(read_kuarters-konfigurasi_kuarters:409): SESSION NOT EXIST")
            Return False
        End If
    End Function

    Private Function load_buildings() As Boolean
        If Session("kuarters_id") IsNot Nothing Then
            Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using cmd As New SqlCommand("SELECT * FROM spk_bangunan WHERE kuarters_id = @KuartersID;", conn)
                    cmd.Parameters.Add("@KuartersID", SqlDbType.Int).Value = Session("kuarters_id")
                    Try
                        conn.Open()
                        Using sda As New SqlDataAdapter(cmd)
                            Dim ds As New DataSet
                            sda.Fill(ds)
                            buildingList.DataSource = ds
                            buildingList.DataBind()
                        End Using
                        Return True
                    Catch ex As Exception
                        Debug.Write("ERROR(read_building-konfigurasi_kuarters:432): " & ex.Message)
                        message_list("ERROR", strSysErrorAlert & "<br/>" & ex.Message)
                        Return False
                    Finally
                        conn.Close()
                    End Try
                End Using
            End Using
            Return False
        Else
            Return False
        End If
    End Function

    Private Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles datRespondent.RowCommand
        If e.CommandName.Equals("edit_kuarters") Then
            Session("kuarters_id") = e.CommandArgument
            load_form_negeri()
            load_form_jenis_kuarters()
            load_form_pangkalan()
            If read_kuarters() Then
                Dim jenisKuarters = ddlFormJenisKuarters.SelectedItem.Text.ToUpper
                Debug.WriteLine("JENIS KUARTERS: " & jenisKuarters)
                SaveBottom.Visible = False
                UpdateBottom.Visible = True
                SaveTop.Visible = False
                UpdateTop.Visible = True
                If jenisKuarters.Contains("PANGSAPURI") Or jenisKuarters.Contains("TERES") Or jenisKuarters.Contains("BERKEMBAR") Then
                    If load_buildings() Then
                        panelPangsapuri.Visible = True
                    End If
                End If
                open()
            End If
        End If
    End Sub

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim kuartersID = datRespondent.DataKeys(e.RowIndex).Values("kuarters_id").ToString
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("DELETE FROM spk_kuarters WHERE kuarters_id = @KuartersID;", conn)
                cmd.Parameters.Add("@KuartersID", SqlDbType.Int).Value = kuartersID
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    message_list("SUCCESS", "Berjaya padam item.")
                Catch ex As Exception
                    Debug.WriteLine("ERROR(datRespondent_RowDeleting-konfigurasi_kuarters:497): " & ex.Message)
                    message_list("ERROR", strFailDelAlert & "<br/>" & ex.Message)
                Finally
                    conn.Close()
                    load_kuarters()
                End Try
            End Using
        End Using
    End Sub

    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        If Session("Kuarters_id") IsNot Nothing Then
            'TODO - Validate save on refresh.
            Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using cmd As New SqlCommand("
                        INSERT INTO 
                            spk_bangunan(bangunan_nama, bangunan_jumlah_aras, kuarters_id) 
                        VALUES (@Nama,@Aras,@KuartersID);
                    ")
                    cmd.Connection = conn
                    cmd.Parameters.Add("@Nama", SqlDbType.NVarChar).Value = tbNamaBangunan.Text
                    cmd.Parameters.Add("@Aras", SqlDbType.Int).Value = tbJumlahArasBaris.Text
                    cmd.Parameters.Add("@KuartersID", SqlDbType.Int).Value = hfKuartersID.Value
                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        Debug.WriteLine("ERROR(datRespondent_RowDeleting-konfigurasi_kuarters:497): " & ex.Message)
                        message_form("ERROR", strSaveFailAlert & "<br/>" & ex.Message)
                    Finally
                        conn.Close()
                        load_buildings()
                        message_form("SUCCESS", strSaveSuccessAlert)
                    End Try
                End Using
            End Using
        End If
    End Sub

    Private Function can_delete_building()
        If Session("building_id") Then
            Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using cmd As New SqlCommand("SELECT * FROM spk_unit WHERE bangunan_id = @BangunanID AND unit_status='Occupied'; ", conn)
                    cmd.Parameters.Add("@BangunanID", SqlDbType.Int).Value = Session("building_id")
                    Try
                        conn.Open()
                        Using sdr As SqlDataReader = cmd.ExecuteReader
                            If sdr.HasRows() Then
                                Return False
                            Else
                                Return True
                            End If
                        End Using
                    Catch ex As Exception
                        Debug.WriteLine("ERROR(can_delete_building-konfigurasi_kuarters:550): " & ex.Message)
                        Return False
                    Finally
                        conn.Close()
                    End Try
                End Using
            End Using
        Else
            Debug.WriteLine("ERROR(can_delete_building-konfigurasi_kuarters:560): SESIION NOT EXIST")
            Return False
        End If
    End Function

    Private Function delete_building()
        If Session("building_id") IsNot Nothing Then
            If can_delete_building() Then
                Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                    Using cmd As New SqlCommand("DELETE FROM spk_bangunan WHERE bangunan_id = @BangunanID;", conn)
                        cmd.Parameters.Add("@BangunanID", SqlDbType.Int).Value = Session("building_id")
                        Try
                            conn.Open()
                            cmd.ExecuteNonQuery()
                            message_list("SUCCESS", "Berjaya padam item.")
                            Return True
                        Catch ex As Exception
                            Debug.WriteLine("ERROR(datRespondent_RowDeleting-konfigurasi_kuarters:497): " & ex.Message)
                            message_form("ERROR", strFailDelAlert & "<br/>" & ex.Message)
                            Return False
                        Finally
                            conn.Close()
                            Session("building_id") = Nothing
                            load_buildings()
                        End Try
                    End Using
                End Using
            Else
                Debug.WriteLine("ERROR(delete_building-konfigurasi_kuarters:586): CANNOT DELETE")
                Return False
            End If
        Else
            Debug.WriteLine("ERROR(delete_building-konfigurasi_kuarters:590): SESSION NOT EXIST")
            Return False
        End If
    End Function

    Private Function read_building()
        If Session("building_id") IsNot Nothing Then
            Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using cmd As New SqlCommand("SELECT * FROM spk_bangunan WHERE bangunan_id = @BangunanID;", conn)
                    cmd.Parameters.Add("@BangunanID", SqlDbType.Int).Value = Session("building_id")
                    Try
                        conn.Open()
                        Using sdr As SqlDataReader = cmd.ExecuteReader
                            If sdr.HasRows Then
                                While sdr.Read
                                    hfBangunanID.Value = sdr("bangunan_id")
                                    lblNamaBangunan.Text = sdr("bangunan_nama")
                                    ddlNoTingkat.Items.Clear()
                                    Dim i As Integer
                                    For i = 1 To Integer.Parse(sdr("bangunan_jumlah_aras"))
                                        Dim item As New ListItem(i, i)
                                        ddlNoTingkat.Items.Add(item)
                                    Next
                                    ddlNoTingkat.Items.Insert(0, New ListItem("-- PILIH --", String.Empty))
                                End While
                                Return True
                            Else
                                Debug.WriteLine("ERROR(read_building-konfigurasi_kuarters:580): NO ROWS")
                                Return False
                            End If
                        End Using
                    Catch ex As Exception
                        Debug.WriteLine("ERROR(read_building-konfigurasi_kuarters:586): " & ex.Message)
                        message_form("ERROR", strRecordBindAlert & "<br/>" & ex.Message)
                        Return False
                    Finally
                        conn.Close()
                    End Try
                End Using
            End Using
        Else
            Debug.WriteLine("ERROR(read_building-konfigurasi_kuarters:625): SESSION NOT EXIST")
            defaultPanel.Visible = True
            maklumatBangunan.Visible = False
            Return False
        End If
    End Function

    Private Sub buildingList_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles buildingList.RowCommand
        If Session("kuarters_id") IsNot Nothing Then
            Session("building_id") = e.CommandArgument
            If e.CommandName.Equals("Ubah") Then
                If read_building() Then
                    defaultPanel.Visible = False
                    maklumatBangunan.Visible = True
                End If
            ElseIf e.CommandName.Equals("Padam") Then
                If can_delete_building() Then
                    If delete_building() Then
                        load_buildings()
                    End If
                Else
                    Debug.WriteLine("ERRORR(buildingList_RowCommand-konfigurasi_kuarters:644): CANNOT DELETE")
                End If
            End If
        End If
    End Sub

    Private Function validate_jumlah_unit()
        If ddlNoTingkat.SelectedIndex > 0 Then
            If tbJumlahUnit.Text.Length > 0 Then
                Return True
            Else
                message_form("ERROR", "Sila Masukkan Jumlah Unit")
                Return False
            End If
        Else
            message_form("ERROR", "Sila Pilih Nombor Tingkat/Baris Unit")
            Return False
        End If
    End Function

    Protected Function has_units() As Boolean
        If Session("building_id") IsNot Nothing Then
            Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using cmd As New SqlCommand("SELECT count(*) as jumlah_unit FROM spk_unit WHERE bangunan_id = @BangunanID;", conn)
                    Try
                        conn.Open()
                        Using sdr As SqlDataReader = cmd.ExecuteReader
                            If sdr.HasRows() Then
                                Return True
                            Else
                                Return False
                            End If
                        End Using
                    Catch ex As Exception
                        message_form("ERROR", strSysErrorAlert & "<br/>" & ex.Message)
                        Return False
                    Finally
                        conn.Close()
                    End Try
                End Using
            End Using
        Else
            Debug.WriteLine("ERROR(has_units-konfigurasi_kuarters:686): SESSION NOT EXIST")
            defaultPanel.Visible = True
            maklumatBangunan.Visible = False
            Return False
        End If
    End Function

    Private Sub btnTambahUnit_Click(sender As Object, e As EventArgs) Handles btnTambahUnit.Click
        If Session("building_id") IsNot Nothing Then
            If validate_jumlah_unit() Then
                Dim jumlahUnit = Integer.Parse(tbJumlahUnit.Text)
                Dim i As Integer = 1
                If has_units() Then
                    Debug.WriteLine("ERRORR(buildingList_RowCommand-konfigurasi_kuarters:644): CANNOT EDIT")
                Else
                    Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                        Using cmd As New SqlCommand("INSERT INTO 
                            spk_unit(kuarters_id, bangunan_id, unit_nama, unit_nombor, unit_tingkat, unit_blok, unit_status)
                            VALUES (@KuartersID, @PangkalanID, @BangunanID, @UnitNama, @UnitNo, @UnitTingkat, @UnitBlok, @UnitStatus);", conn)
                            For i = 1 To jumlahUnit
                                Try
                                    cmd.Parameters.Add("@KuartersID", SqlDbType.Int).Value = hfKuartersID.Value
                                    cmd.Parameters.Add("@PangkalanID", SqlDbType.Int).Value = hfPrevPangkalanID.Value
                                    cmd.Parameters.Add("@UnitNama", SqlDbType.NVarChar).Value = lblNamaBangunan.Text & "-" & ddlNoTingkat.SelectedValue & "-" & i
                                    cmd.Parameters.Add("@UnitTingkat", SqlDbType.Int).Value = ddlNoTingkat.SelectedValue
                                    cmd.Parameters.Add("@UnitNo", SqlDbType.Int).Value = i
                                    cmd.Parameters.Add("@UnitBlok", SqlDbType.NVarChar).Value = lblNamaBangunan.Text
                                    cmd.Parameters.Add("@BangunanID", SqlDbType.Int).Value = Session("building_id")
                                    cmd.Parameters.Add("@UnitStatus", SqlDbType.NVarChar).Value = "Under Maintenance"
                                    conn.Open()
                                    cmd.ExecuteNonQuery()
                                Catch ex As Exception
                                    Debug.WriteLine("ERROR(btnTambahUnit_click-konfigurasi_kuarters:708): " & ex.Message)
                                Finally
                                    conn.Close()
                                End Try
                            Next
                        End Using
                    End Using
                End If
            End If
        Else
            Debug.WriteLine("ERROR(btnTambahUnit-konfigurasi_kuarters:732): SESSION NOT EXIST")
            defaultPanel.Visible = True
            maklumatBangunan.Visible = False
        End If
    End Sub

    Private Function update_kuarters()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("UPDATE 
                spk_kuarters 
            SET 
                jenisKuarters_id = @JenisKuarters
                , pangkalan_id = @Pangkalan
                , kuarters_negara = @Negara
                , kuarters_negeri = @Negeri
                , kuarters_bandar = @Bandar
                , kuarters_poskod = @Poskod
                , kuarters_nama = @Nama
                , kuarters_alamat = @Alamat
                , kuarters_telefon = @Telefon
                , kuarters_faks = @Faks
                , kuarters_emel = @Emel
            WHERE kuarters_id = @KuartersID;", conn)
                cmd.Parameters.Add("@JenisKuarters", SqlDbType.Int).Value = ddlFormJenisKuarters.SelectedValue
                cmd.Parameters.Add("@Pangkalan", SqlDbType.Int).Value = ddlFormPangkalan.SelectedValue
                cmd.Parameters.Add("@Negara", SqlDbType.NVarChar).Value = "MALAYSIA"
                cmd.Parameters.Add("@Negeri", SqlDbType.NVarChar).Value = ddlFormNegeri.SelectedValue.ToUpper
                cmd.Parameters.Add("@Bandar", SqlDbType.NVarChar).Value = tbFormBandar.Text.ToUpper
                cmd.Parameters.Add("@Poskod", SqlDbType.NVarChar).Value = tbFormPostcode.Text
                cmd.Parameters.Add("@Nama", SqlDbType.NVarChar).Value = tbFormNama.Text.ToUpper
                cmd.Parameters.Add("@Alamat", SqlDbType.NVarChar).Value = tbFormAlamat.Text.ToUpper
                cmd.Parameters.Add("@Telefon", SqlDbType.NVarChar).Value = tbFormTelefon.Text
                cmd.Parameters.Add("@Faks", SqlDbType.NVarChar).Value = tbFormNoFax.Text
                cmd.Parameters.Add("@Emel", SqlDbType.NVarChar).Value = tbFormEmail.Text
                cmd.Parameters.Add("@KuartersID", SqlDbType.Int).Value = hfKuartersID.Value
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Debug.WriteLine("ERROR(update_kuarters-konfigurasi_kuarters:753): " & ex.Message)
                    Return False
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Function

    Private Sub UpdateTop_ServerClick(sender As Object, e As EventArgs) Handles UpdateTop.ServerClick
        If validate_save_kuarters() Then
            If update_kuarters() Then
                Response.Redirect(Request.RawUrl)
            End If
        End If
    End Sub

    Private Sub UpdateBottom_ServerClick(sender As Object, e As EventArgs) Handles UpdateBottom.ServerClick
        If validate_save_kuarters() Then
            If update_kuarters() Then
                Response.Redirect(Request.RawUrl)
            End If
        End If
    End Sub

    Private Sub ddlPangkalan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPangkalan.SelectedIndexChanged
        load_kuarters()
    End Sub

    Private Sub ddlJenisKuarters_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenisKuarters.SelectedIndexChanged
        load_kuarters()
    End Sub
End Class