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
            Using cmd As New SqlCommand("SELECT kuarters_nama, kuarters_id from spk_kuarters;", conn)
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
            ddlKuarters.Enabled = True
            load_kuarters()
        End If
        load_units()
    End Sub

    Private Sub ddlKuarters_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKuarters.SelectedIndexChanged
        load_units()
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        load_units()
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
	                , b.kuarters_nama
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
                            While (sdr.Read)
                                ddlInsertPangkalan.DataTextField = sdr("pangkalan_nama")
                                ddlInsertKuarters.DataTextField = sdr("kuarters_nama")
                                tbNamaUnit.Text = IIf(sdr("unit_nama").ToString().Equals(""), sdr("unit_nama_lain"), sdr("unit_nama").ToString())
                                tbBlok.Text = sdr("unit_blok")
                                tbTingkat.Text = sdr("unit_tingkat")
                                tbNoUnit.Text = sdr("unit_nombor")
                            End While
                        End If
                        viewConfig.ActiveViewIndex = 1
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

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim unitID = datRespondent.DataKeys(e.RowIndex).Values("unit_id").ToString
        If Not unitID.Equals("") Then
            Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using cmd As New SqlCommand("DELETE spk_unit WHERE unit_id = @UnitID", conn)
                    cmd.Parameters.Add("@UnitID", SqlDbType.Int).Value = unitID
                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()
                        load_units()
                    Catch ex As Exception
                        Debug.WriteLine("Error(datRespondent_RowDeleting-konfigurati_unit: 173): " & ex.Message)
                    Finally
                        conn.Close()
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
                    Return "Tunggu Keputusan Pemohon"
                Case "Occupied"
                    Return "Ada Pengghuni"
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

    Protected Sub cancel()
        tbNamaUnit.Text = ""
        tbBlok.Text = ""
        tbNoUnit.Text = ""
        viewConfig.ActiveViewIndex = 0
    End Sub

    Private Sub CancelTop_ServerClick(sender As Object, e As EventArgs) Handles CancelTop.ServerClick
        cancel()
        Session("tambah_unit") = Nothing
        Session("unit_id") = Nothing
    End Sub

    Private Sub CancelBottom_ServerClick(sender As Object, e As EventArgs) Handles CancelBottom.ServerClick
        cancel()
        Session("tambah_unit") = Nothing
        Session("unit_id") = Nothing
    End Sub

    Private Function validate() As Boolean
        If tbNamaUnit.Text.Length > 0 Then
            If tbBlok.Text.Length > 0 Then
                If tbTingkat.Text.Length > 0 Then
                    If tbNoUnit.Text.Length > 0 Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Private Function update() As Boolean
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("
            UPDATE 
                spk_unit 
            SET 
                unit_nama = @UnitNama, 
                unit_nombor = @UnitNombor, 
                unit_tingkat = @UnitTingkat,
                kuarters_id = @KuartersID,
                pangkalan_id = @PangkalanID,
                unit_blok = @UnitBlok
                unit_status = @UnitStatus
            ", conn)
                cmd.Parameters.Add("@UnitNama", SqlDbType.NVarChar, 50).Value = tbNamaUnit.Text
                cmd.Parameters.Add("@UnitNo", SqlDbType.NVarChar, 50).Value = tbNoUnit.Text
                cmd.Parameters.Add("@UnitTingkat", SqlDbType.NVarChar, 50).Value = tbTingkat.Text
                cmd.Parameters.Add("@UnitBlok", SqlDbType.NVarChar, 50).Value = tbBlok.Text
                cmd.Parameters.Add("@KuartersID", SqlDbType.Int).Value = ddlInsertKuarters.SelectedValue
                cmd.Parameters.Add("@UnitStatus", SqlDbType.NVarChar, 50).Value = "Available"
                Try
                    conn.Open()
                    If Session("unit_id") IsNot Nothing Then
                        cmd.ExecuteNonQuery()
                        Return True
                    Else
                        Return False
                    End If
                Catch ex As Exception
                    Debug.WriteLine("Debug(update-konfigurasi_unit:280): " & ex.Message)
                    Return False
                Finally
                    conn.Close()
                    load_units()
                    Session("unit_id") = Nothing
                    viewConfig.ActiveViewIndex = 0
                End Try
            End Using
        End Using
    End Function

    Private Function insert() As Boolean
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("INSERT INTO spk_unit VALUES(@KuartersID, @PangkalanID, @UnitNama, @UnitNo, @UnitTingkat, @UnitBlok, @UnitStatus);", conn)
                cmd.Parameters.Add("@KuartersID", SqlDbType.Int).Value = 1
                cmd.Parameters.Add("@PangkalanID", SqlDbType.Int).Value = 1
                cmd.Parameters.Add("@UnitNama", SqlDbType.NVarChar, 50).Value = ""
                cmd.Parameters.Add("@UnitNo", SqlDbType.NVarChar, 50).Value = ""
                cmd.Parameters.Add("@UnitTingkat", SqlDbType.NVarChar, 50).Value = ""
                cmd.Parameters.Add("@UnitBlok", SqlDbType.NVarChar, 50).Value = ""
                cmd.Parameters.Add("@UnitStatus", SqlDbType.NVarChar, 50).Value = ""
                Try
                    conn.Open()
                    If Session("tambah_unit") IsNot Nothing And Session("tambah_unit") Then
                        cmd.ExecuteNonQuery()
                        Return True
                    Else
                        Return False
                    End If
                Catch ex As Exception
                    Debug.WriteLine("Debug(insert-konfigurasi_unit:299): " & ex.Message)
                    Return False
                Finally
                    conn.Close()
                    load_units()
                    viewConfig.ActiveViewIndex = 0
                End Try
            End Using
        End Using
    End Function

    Private Sub message(ByVal msgType As String, ByVal msg As String)
        Select Case msgType
            Case "SUCCESS"
                MsgTop.Attributes("class") = "successMsg"
                MsgBottom.Attributes("class") = "successMsg"
            Case "FAILED"
                MsgTop.Attributes("class") = "errorMsg"
                MsgBottom.Attributes("class") = "errorMsg"
            Case Else
                Debug.WriteLine("Error(message): ELSE case message")
        End Select
        strlbl_top.Text = msg
        strlbl_bottom.Text = msg
    End Sub

    Private Sub tambahUnit_ServerClick(sender As Object, e As EventArgs) Handles tambahUnit.ServerClick
        UpdateTop.Visible = False
        UpdateBottom.Visible = False
        SaveTop.Visible = True
        SaveBottom.Visible = True
        Session("tambah_unit") = True
        load_form_kuarters()
        viewConfig.ActiveViewIndex = 1
    End Sub

    Private Sub SaveTop_ServerClick(sender As Object, e As EventArgs) Handles SaveTop.ServerClick
        If validate() Then
            insert()
        End If
    End Sub

    Private Sub SaveBottom_ServerClick(sender As Object, e As EventArgs) Handles SaveBottom.ServerClick
        If validate() Then
            insert()
        End If
    End Sub

    Private Sub UpdateTop_ServerClick(sender As Object, e As EventArgs) Handles UpdateTop.ServerClick
        If validate() Then
            update()
        End If
    End Sub

    Private Sub UpdateBottom_ServerClick(sender As Object, e As EventArgs) Handles UpdateBottom.ServerClick
        If validate() Then
            update()
        End If
    End Sub

    Private Sub ddlInsertPangkalan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlInsertPangkalan.SelectedIndexChanged
        If ddlInsertPangkalan.SelectedIndex > 0 Then

        End If
    End Sub
End Class