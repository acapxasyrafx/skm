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
            load_page()
            Dim test1 As String = "0"
            Dim test2 As Boolean = True
            If test1.GetType = GetType(String) Then
                Debug.WriteLine("TEST1: " & test1.GetType.ToString)
                Debug.WriteLine("TEST2: " & test2.GetType.ToString)
            End If
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
                Using cmd As New SqlCommand("SELECT 
	                A.bangunan_id
	                , A.bangunan_nama
                    , A.bangunan_jumlah_aras
	                , COUNT(B.unit_id) jumlah_unit
                FROM 
	                spk_bangunan A
                LEFT JOIN spk_unit B ON B.bangunan_id = A.bangunan_id
                WHERE A.kuarters_id = @KuartersID
                GROUP BY 
                    A.bangunan_id
                    , A.bangunan_jumlah_aras
                    , A.bangunan_nama;", conn)
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
        Session("kuarters_id") = kuartersID
        If can_delete_kuarters() Then
            Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using cmd As New SqlCommand("DELETE FROM spk_unit WHERE kuarters_id = @KuartersID;
                    DELETE FROM spk_bangunan WHERE kuarters_id = @KuartersID;
                    DELETE FROM spk_kuarters WHERE kuarters_id = @KuartersID;", conn)
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
        Else

        End If
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
                    cmd.Parameters.Add("@Nama", SqlDbType.NVarChar).Value = tbNamaBangunan.Text.ToUpper
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
                    Using cmd As New SqlCommand("DELETE FROM spk_unit WHERE bangunan_id = @BangunanID; DELETE FROM spk_bangunan WHERE bangunan_id = @BangunanID;")
                        cmd.Connection = conn
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
                                    tbJumlahUnit.Text = 0
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
                    Debug.WriteLine("ERROR(buildingList_RowCommand-konfigurasi_kuarters:644): CANNOT DELETE")
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
                Dim i As Integer = 1
                Dim jumlahUnitBaru = Integer.Parse(tbJumlahUnit.Text)
                Dim jumlahUnitSimpan As Integer = total_units()
                If jumlahUnitSimpan - jumlahUnitBaru > 0 Then
                    Debug.WriteLine("btnTambah_click: REMOVE UNIT")
                    Dim jumlahOccupied = total_occupied_unit()
                    If jumlahOccupied >= jumlahUnitBaru Then
                        Debug.WriteLine("Unit occupied/on hold more than to remove")
                        'TODO: ADD MESSAGE CANNOT REMOVE(Occupied > removed)
                    Else
                        If delete_other_unit() Then
                            For i = 1 To jumlahUnitBaru
                                Dim unit_nama = lblNamaBangunan.Text & "-" & ddlNoTingkat.SelectedValue & "-" & i
                                Dim isExist = unit_exist(unit_nama)
                                'If the return is boolean
                                If isExist.GetType = GetType(Boolean) Then
                                    If isExist Then

                                    Else
                                        insert_unit(i)
                                    End If
                                Else
                                    'TODO: ADD ERRORS MESSAGES
                                End If
                            Next
                        End If
                    End If
                ElseIf jumlahUnitSimpan - jumlahUnitBaru <= 0 Then
                    Debug.WriteLine("btnTambah_click: ADD UNIT")
                    For i = 1 To jumlahUnitBaru
                        Dim unit_nama = lblNamaBangunan.Text & "-" & ddlNoTingkat.SelectedValue & "-" & i
                        Dim isExist = unit_exist(unit_nama)
                        If isExist.GetType = GetType(Boolean) Then
                            If unit_exist(unit_nama) Then
                                Continue For
                            Else
                                If insert_unit(i) Then
                                    Session("building_id") = Nothing
                                    load_buildings()
                                Else
                                    'TODO: INSERT ERROR MESSAGE
                                End If
                            End If
                        Else
                            'TODO: ADD ERRORS MESSAGES
                        End If
                    Next
                Else
                    Debug.WriteLine("btnTambah_click: ELSE")
                    Return
                End If
            End If
        Else
            Debug.WriteLine("ERROR(btnTambahUnit-konfigurasi_kuarters:790): building_id SESSION NOT EXIST")
            'TODO: ADD ERROR MESSAGE
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

    Protected Function total_units() As Integer
        If Session("building_id") IsNot Nothing Then
            Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using cmd As New SqlCommand("SELECT COUNT(*) FROM spk_unit WHERE bangunan_id = @BangunanID AND unit_tingkat = @Tingkat", conn)
                    cmd.Parameters.Add("@BangunanID", SqlDbType.Int).Value = Session("building_id")
                    cmd.Parameters.Add("@Tingkat", SqlDbType.Int).Value = ddlNoTingkat.SelectedValue
                    Try
                        conn.Open()
                        Dim jumlahUnit As Integer = Integer.Parse(cmd.ExecuteScalar)
                        Return jumlahUnit
                    Catch ex As Exception
                        Debug.WriteLine("ERROR(total_units-konfigurasi_kuarters:841): " & ex.Message)
                        Return Nothing
                    Finally
                        conn.Close()
                    End Try
                End Using
            End Using
        Else
            Debug.WriteLine("ERROR(total_units-konfigurasi_kuarters:848): SESSION NOT EXIST")
            defaultPanel.Visible = True
            maklumatBangunan.Visible = False
            Return Nothing
        End If
    End Function

    Private Sub ddlNoTingkat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNoTingkat.SelectedIndexChanged
        Dim jumlahUnit = total_units()
        If jumlahUnit.ToString IsNot Nothing Then
            tbJumlahUnit.Text = jumlahUnit
        End If
    End Sub

    Private Function can_delete_kuarters() As Boolean
        If Session("kuarters_id") IsNot Nothing Then
            Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using cmd As New SqlCommand("SELECT count(*) FROM spk_unit WHERE kuarters_id = @KuartersID AND unit_status='Occupied';", conn)
                    cmd.Parameters.Add("@KuartersID", SqlDbType.Int).Value = Session("kuarters_id")
                    Try
                        conn.Open()
                        Using sdr As SqlDataReader = cmd.ExecuteReader
                            If sdr.HasRows Then
                                Return False
                            Else
                                Return True
                            End If
                        End Using
                    Catch ex As Exception
                        Debug.WriteLine("ERROR(can_delete_kuarters-konfigurasi_kuarters:879): " & ex.Message)
                        Return False
                    End Try
                End Using
            End Using
        Else
            Debug.WriteLine("ERROR(total_units-konfigurasi_kuarters:848): SESSION NOT EXIST")
            defaultPanel.Visible = True
            maklumatBangunan.Visible = False
            Return False
        End If
    End Function

    Private Function unit_exist(ByVal unit_name As String)
        If Session("kuarters_id") IsNot Nothing Then
            If Session("building_id") IsNot Nothing Then
                Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                    Using cmd As New SqlCommand("SELECT * FROM spk_unit WHERE bangunan_id = @BangunanID AND kuarters_id = @KuartersID AND pangkalan_id = @PangkalanID AND unit_tingkat = @UnitTingkat AND unit_nama = @UnitNama", conn)
                        cmd.Parameters.Add("@BangunanID", SqlDbType.Int).Value = hfBangunanID.Value
                        cmd.Parameters.Add("@KuartersID", SqlDbType.Int).Value = hfKuartersID.Value
                        cmd.Parameters.Add("@PangkalanID", SqlDbType.Int).Value = hfPrevPangkalanID.Value
                        cmd.Parameters.Add("@UnitTingkat", SqlDbType.Int).Value = ddlNoTingkat.SelectedValue
                        cmd.Parameters.Add("@UnitNama", SqlDbType.NVarChar).Value = unit_name
                        Try
                            conn.Open()
                            Dim sdr As SqlDataReader = cmd.ExecuteReader
                            If sdr.HasRows Then
                                Return True
                            Else
                                Return False
                            End If
                        Catch ex As Exception
                            Debug.WriteLine("ERROR(unit_exist-konfigurasi_kuarters(925)): " & ex.Message)
                            'TODO: ADD ERROR MESSAGE HERE
                            Return "0"
                        End Try
                    End Using
                End Using
            Else
                Debug.WriteLine("ERROR(unit_exist-konfigurasi_kuarters:934): building_id SESSION NOT EXIST")
                Return "0"
            End If
        Else
            Debug.WriteLine("ERROR(unit_exist-konfigurasi_kuarters:938): kuarters_id SESSION NOT EXIST")
            Return "0"
        End If
    End Function

    Private Function insert_unit(ByVal i As Integer)
        If Session("building_id") Then
            Dim unit_nama = lblNamaBangunan.Text & "-" & ddlNoTingkat.SelectedValue & "-" & i
            Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using cmd As New SqlCommand("INSERT INTO 
                            spk_unit(kuarters_id, pangkalan_id, bangunan_id, unit_nama, unit_nombor, unit_tingkat, unit_blok, unit_status)
                            VALUES (@KuartersID, @PangkalanID, @BangunanID, @UnitNama, @UnitNo, @UnitTingkat, @UnitBlok, @UnitStatus);", conn)
                    cmd.Parameters.Add("@KuartersID", SqlDbType.Int).Value = hfKuartersID.Value
                    cmd.Parameters.Add("@PangkalanID", SqlDbType.Int).Value = hfPrevPangkalanID.Value
                    cmd.Parameters.Add("@UnitNama", SqlDbType.NVarChar).Value = unit_nama
                    cmd.Parameters.Add("@UnitTingkat", SqlDbType.Int).Value = ddlNoTingkat.SelectedValue
                    cmd.Parameters.Add("@UnitNo", SqlDbType.Int).Value = i
                    cmd.Parameters.Add("@UnitBlok", SqlDbType.NVarChar).Value = lblNamaBangunan.Text
                    cmd.Parameters.Add("@BangunanID", SqlDbType.Int).Value = Session("building_id")
                    cmd.Parameters.Add("@UnitStatus", SqlDbType.NVarChar).Value = "Under Maintenance"
                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()
                        Return True
                    Catch ex As Exception
                        Debug.WriteLine("ERROR(insert_unit-konfigurasi_kuarters:953(" & i & ")): " & ex.Message)
                        'TODO: ADD ERROR MESSAGE
                        Return False
                    Finally
                        conn.Close()
                    End Try
                End Using
            End Using
        Else
            Debug.WriteLine("ERROR(insert_unit-konfigurasi_kuarters:962): building_id SESSION NOT EXIST")
            Return False
        End If
    End Function

    Private Function total_occupied_unit()
        If Session("kuarters_id") IsNot Nothing Then
            If Session("building_id") IsNot Nothing Then
                Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                    Using cmd As New SqlCommand("SELECT 
                Count(*)  
            FROM  
                spk_unit  
            WHERE 
                bangunan_id = @BangunanID 
                AND kuarters_id = @KuartersID 
                AND pangkalan_id = @PangkalanID 
                AND unit_tingkat = @UnitTingkat
                AND (unit_status = 'Occupied' OR unit_status = 'On Hold');",
                    conn)
                        cmd.Parameters.Add("@BangunanID", SqlDbType.Int).Value = hfBangunanID.Value
                        cmd.Parameters.Add("@KuartersID", SqlDbType.Int).Value = hfKuartersID.Value
                        cmd.Parameters.Add("@PangkalanID", SqlDbType.Int).Value = hfPrevPangkalanID
                        cmd.Parameters.Add("@UnitTingkat", SqlDbType.Int).Value = ddlNoTingkat.SelectedValue
                        Try
                            conn.Open()
                            Return Integer.Parse(cmd.ExecuteScalar)
                        Catch ex As Exception
                            Debug.WriteLine("ERROR(total_occupied_unit-konfigurasi_kuarters): " & ex.Message)
                            'TODO: ADD ERROR MESSAGE HERE
                            Return False
                        End Try
                    End Using
                End Using
            Else
                Debug.WriteLine("ERROR(total_occupied_unit-konfigurasi_kuarters:1024): building_id SESSION NOT EXIST")
                Return "0"
            End If
        Else
            Debug.WriteLine("ERROR(total_occupied_unit-konfigurasi_kuarters:1027): kuarters_id SESSION NOT EXIST")
            Return "0"
        End If

    End Function

    Private Function delete_other_unit()
        If Session("kuarters_id") IsNot Nothing Then
            If Session("building_id") IsNot Nothing Then
                Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                    Using cmd As New SqlCommand("DELETE FROM 
	                spk_unit 
                WHERE 
	                bangunan_id = @BangunanID
	                AND pangkalan_id = @PangkalanID
	                AND kuarters_id = @KuartersID
	                AND unit_tingkat = @NoTingkat
	                AND (unit_status <> @StatusOccupied AND unit_status <> @StatusOnHold)
                ;",
                    conn)
                        cmd.Parameters.Add("@BangunanID", SqlDbType.Int).Value = hfBangunanID.Value
                        cmd.Parameters.Add("@KuartersID", SqlDbType.Int).Value = hfKuartersID.Value
                        cmd.Parameters.Add("@PangkalanID", SqlDbType.Int).Value = hfPrevPangkalanID
                        cmd.Parameters.Add("@NoTIngkat", SqlDbType.Int).Value = ddlNoTingkat.SelectedValue
                        cmd.Parameters.Add("@StatusOccupied", SqlDbType.NVarChar).Value = "Occupied"
                        cmd.Parameters.Add("@StatusOnHold", SqlDbType.NVarChar).Value = "On Hold"
                        Try
                            conn.Open()
                            Return Integer.Parse(cmd.ExecuteNonQuery)
                        Catch ex As Exception
                            Debug.WriteLine("ERROR(delete_other_unit-konfigurasi_kuarters): " & ex.Message)
                            'TODO: ADD ERROR MESSAGE HERE
                            Return False
                        Finally
                            conn.Close()
                        End Try
                    End Using
                End Using
            Else
                Debug.WriteLine("ERROR(delete_other_unit-konfigurasi_kuarters:1067): building_id SESSION NOT EXIST")
                Return "0"
            End If
        Else
            Debug.WriteLine("ERROR(delete_other_unit-konfigurasi_kuarters:1071): kuarters_id SESSION NOT EXIST")
            Return "0"
        End If
    End Function
End Class