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
                    cmd.Parameters.Add("@JenisKuarters", SqlDbType.Int).Value = ddlJenisKuarters.SelectedValue
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
        If ddlFormJenisKuarters.SelectedIndex > 0 Then
            panelPangsapuri.Visible = True
        End If
    End Sub

    Private Function validate_save() As Boolean
        Return True
    End Function

    Private Function validate_update() As Boolean
        Return True
    End Function

    Private Function save() As Boolean
        Return True
    End Function

    Private Function read() As Boolean
        Return True
    End Function

    Private Function insert() As Boolean
        Return True
    End Function


    ''--SAVE FUNCTION--'
    'Private Function Save() As Boolean

    '    If Not Request.QueryString("edit") = "" Then

    '        strSQL = "UPDATE spk_kuarters SET "

    '        strSQL += " jenisKuarters_id = UPPER('" & ddlJenisKuarters.SelectedValue & "'),"
    '        strSQL += " pangkalan_id = UPPER('" & ddlPangkalan.SelectedValue & "'),"
    '        strSQL += " kuarters_negeri = UPPER('" & ddlNegeri.SelectedValue & "'),"
    '        strSQL += " kuarters_bandar = UPPER('" & txtBandar.Text & "'),"
    '        strSQL += " kuarters_poskod = UPPER('" & txtPoskod.Text & "'),"
    '        strSQL += " kuarters_nama = UPPER('" & txtNamaKuarters.Text & "'),"
    '        strSQL += " kuarters_alamat = UPPER('" & txtAlamat.Text & "'),"
    '        strSQL += " kuarters_telefon = UPPER('" & txtTelefon.Text & "'),"
    '        strSQL += " kuarters_faks = '" & txtFaks.Text & "',"
    '        strSQL += " kuarters_emel = UPPER('" & txtEmel.Text & "')"


    '        strSQL += " WHERE kuarters_id = '" & Request.QueryString("edit") & "'"

    '    Else
    '        strSQL = "INSERT INTO spk_kuarters (jenisKuarters_id, pangkalan_id, kuarters_negeri, kuarters_bandar, kuarters_poskod, kuarters_nama, kuarters_alamat, kuarters_telefon, kuarters_faks, kuarters_emel)"

    '        strSQL += " VALUES ("
    '        strSQL += " UPPER('" & ddlJenisKuarters.SelectedValue & "'),"
    '        strSQL += " UPPER('" & ddlPangkalan.SelectedValue & "'),"
    '        strSQL += " UPPER('" & ddlNegeri.SelectedValue & "'),"
    '        strSQL += " UPPER('" & txtBandar.Text & "'),"
    '        strSQL += " UPPER('" & txtPoskod.Text & "'),"
    '        strSQL += " UPPER('" & txtNamaKuarters.Text & "'),"
    '        strSQL += " UPPER('" & txtAlamat.Text & "'),"
    '        strSQL += " UPPER('" & txtTelefon.Text & "'),"
    '        strSQL += " UPPER('" & txtFaks.Text & "'),"
    '        strSQL += " UPPER('" & txtEmel.Text & "'))"

    '    End If

    '    strRet = oCommon.ExecuteSQL(strSQL)

    '    If strRet = "0" Then
    '        Return True
    '    Else
    '        MsgTop.Attributes("class") = "errorMsg"
    '        strlbl_top.Text = strSysErrorAlert
    '        MsgBottom.Attributes("class") = "errorMsg"
    '        strlbl_bottom.Text = strSysErrorAlert & "<br>" & strRet
    '        Return False
    '    End If

    'End Function

    ''--DATA VALIDATION--'
    'Private Function ValidateData() As Boolean
    '    'If Not oCommon.isNumeric(txtidx.Text) Then
    '    '    txtidx.Focus()
    '    '    Return False

    '    'End If
    '    Return True
    'End Function

    'Private Sub SaveFunction_ServerClick(sender As Object, e As EventArgs) Handles SaveFunction.ServerClick

    '    strlbl_bottom.Text = ""
    '    strlbl_top.Text = ""
    '    '--validate--'
    '    If ValidateData() = False Then
    '        MsgTop.Attributes("class") = "errorMsg"
    '        strlbl_top.Text = strDataValAlert
    '        MsgBottom.Attributes("class") = "errorMsg"
    '        strlbl_bottom.Text = strDataValAlert
    '        Exit Sub
    '    End If
    '    Try
    '        '--execute--'
    '        If Save() = True Then
    '            MsgTop.Attributes("class") = "successMsg"
    '            strlbl_top.Text = strSaveSuccessAlert
    '            MsgBottom.Attributes("class") = "successMsg"
    '            strlbl_bottom.Text = strSaveSuccessAlert
    '        Else
    '            MsgTop.Attributes("class") = "errorMsg"
    '            strlbl_top.Text = strSaveFailAlert
    '            MsgBottom.Attributes("class") = "errorMsg"
    '            strlbl_bottom.Text = strSaveFailAlert
    '        End If
    '    Catch ex As Exception
    '        MsgTop.Attributes("class") = "errorMsg"
    '        strlbl_top.Text = strSysErrorAlert
    '        MsgBottom.Attributes("class") = "errorMsg"
    '        strlbl_bottom.Text = strSysErrorAlert & "<br>" & ex.Message
    '    End Try

    '    If Not Request.QueryString("edit") = "" Then
    '        Dim Pagelabel As String = lblConfig.Text & "&q=" & lblQ.Text & "&lblTop=" & strlbl_top.Text & "&lblBottom=" & strlbl_top.Text
    '        Response.Redirect("Konfigurasi.Kuarters.aspx?p=" & Pagelabel)
    '    Else
    '        strRet = BindData(datRespondent)
    '    End If



    'End Sub
    ''--REFRESH BUTTON--'
    'Private Sub Refresh_ServerClick(sender As Object, e As EventArgs) Handles Refresh.ServerClick
    '    Dim Pagelabel As String = lblConfig.Text & "&q=" & lblQ.Text
    '    Response.Redirect("Konfigurasi.Kuarters.aspx?p=" & Pagelabel)
    'End Sub

    'Private Sub requestPage()
    '    lblConfig.Text = Request.QueryString("p")
    '    lblQ.Text = Request.QueryString("q")
    '    If Not Request.QueryString("lblBottom") = "" Then
    '        strlbl_top.Text = Request.QueryString("lblTop")
    '        strlbl_bottom.Text = Request.QueryString("lblBottom")
    '    End If
    'End Sub

    'Private Sub clear()
    '    ddlNegeri.SelectedValue = ""
    '    ddlPangkalan.SelectedValue = ""
    '    ddlJenisKuarters.Text = ""
    '    txtNamaKuarters.Text = ""
    '    txtAlamat.Text = ""
    '    txtPoskod.Text = ""
    '    txtBandar.Text = ""
    '    txtTelefon.Text = ""
    '    txtFaks.Text = ""
    '    txtEmel.Text = ""
    'End Sub

    ''--DELETE FUNCTION--'
    'Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting

    '    Dim strCID = datRespondent.DataKeys(e.RowIndex).Values("kuarters_id").ToString

    '    'chk session to prevent postback
    '    If Not strCID = Session("strCID") Then
    '        strSQL = "DELETE FROM spk_kuarters WHERE kuarters_id = '" & strCID & "'"
    '        strRet = oCommon.ExecuteSQL(strSQL)

    '        Session("strCID") = ""
    '    End If
    '    strRet = BindData(datRespondent)

    'End Sub

    'Private Sub ddlNegeri_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegeri.SelectedIndexChanged
    '    requestPangkalan()
    '    strRet = BindData(datRespondent)
    'End Sub

End Class