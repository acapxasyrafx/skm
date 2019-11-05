Imports System.Data.SqlClient
Public Class pendaftaran_penjawat
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
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
        Try
            If Not IsPostBack Then

                If strlblMsgTop = 0 Then
                    strlbl_top.Visible = True
                Else
                    strlbl_top.Visible = False
                End If
                populateJawatan()
                populateNegara()
                populateNegeri()
                populateJabatan()
                populatePangkalan()

            End If

        Catch ex As Exception
            MsgTop.Attributes("class") = "errorMsg"
            strlbl_top.Text = strSysErrorAlert
        Finally
        End Try
    End Sub

    Protected Sub populateJawatan()
        Dim cmd As New SqlCommand("SELECT * FROM admin.spk_pangkat", conn)
        Dim ds As New DataSet
        Try
            conn.Open()
            Dim sda As New SqlDataAdapter(cmd)
            sda.Fill(ds)
            ddlJawatan.DataSource = ds
            ddlJawatan.DataTextField = "pangkat_nama"
            ddlJawatan.DataValueField = "pangkat_id"
            ddlJawatan.DataBind()
        Catch ex As Exception
            Debug.Write("ERROR")
            Debug.Write(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Protected Sub populateJabatan()

    End Sub

    Protected Sub populateNegara()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        strSQL = "SELECT config_id, config_parameter FROM general_config WHERE config_access = 'GLOBAL' AND config_type = 'NEGARA' ORDER BY config_idx, config_parameter"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlNegaraDaftar.DataSource = ds
            ddlNegaraDaftar.DataTextField = "config_parameter"
            ddlNegaraDaftar.DataValueField = "config_parameter"
            ddlNegaraDaftar.DataBind()
            ddlNegaraDaftar.Items.Insert(0, New ListItem("- PILIH -", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub populateNegeri()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        strSQL = "SELECT config_id, config_parameter FROM general_config WHERE config_access = 'GLOBAL' AND config_type = 'NEGERI' ORDER BY config_idx, config_parameter"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlNegeriDaftar.DataSource = ds
            ddlNegeriDaftar.DataTextField = "config_parameter"
            ddlNegeriDaftar.DataValueField = "config_parameter"
            ddlNegeriDaftar.DataBind()
            ddlNegeriDaftar.Items.Insert(0, New ListItem("- PILIH -", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub populateJantina()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        strSQL = "SELECT config_id, config_parameter FROM general_config WHERE config_access = 'GLOBAL' AND config_type = 'JANTINA' ORDER BY config_idx, config_parameter"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlJantina.DataSource = ds
            ddlJantina.DataTextField = "config_parameter"
            ddlJantina.DataValueField = "config_parameter"
            ddlJantina.DataBind()
            ddlJantina.Items.Insert(0, New ListItem("- PILIH -", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub populatePangkalan()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        strSQL = "SELECT pangkalan_id,pangkalan_nama,pangkalan_alamat FROM spk_pangkalan WHERE pangkalan_nama IS NOT NULL AND pangkalan_id IS NOT NULL ORDER BY pangkalan_negeri,pangkalan_bandar"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCawangan.DataSource = ds
            ddlCawangan.DataTextField = "pangkalan_nama"
            ddlCawangan.DataValueField = "pangkalan_id"
            ddlCawangan.DataBind()
            ddlCawangan.Items.Insert(0, New ListItem("- PILIH -", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    '--SAVE FUNCTION--'
    Private Function Save() As Boolean

        If Not Request.QueryString("edit") = "" Then

            strSQL = "UPDATE spk_pengguna SET "

            strSQL += " pangkalan_id = '" & oCommon.FixSingleQuotes(ddlCawangan.SelectedValue) & "',"
            strSQL += " pengguna_mykad = '" & oCommon.FixSingleQuotes(txtNoKP.Text) & "',"
            strSQL += " pengguna_kewarganegaraan = '" & oCommon.FixSingleQuotes(ddlKewarganegaraan.SelectedValue) & "',"
            strSQL += " pengguna_tarikh_lahir = '" & txt_MyDate.Text & "',"
            strSQL += " pengguna_nama = '" & oCommon.FixSingleQuotes(txtNamaPertama.Text) & "',"
            strSQL += " pengguna_alamat = '" & oCommon.FixSingleQuotes(txtAlamat1Daftar.Text + txtAlamat2Daftar.Text + txtAlamat3Daftar.Text) & "',"
            strSQL += " pengguna_poskod = '" & oCommon.FixSingleQuotes(txtPoskodDaftar.Text) & "',"
            strSQL += " pengguna_bandar = '" & oCommon.FixSingleQuotes(txtBandarDaftar.Text) & "',"
            strSQL += " pengguna_negeri = '" & oCommon.FixSingleQuotes(ddlNegeriDaftar.SelectedValue) & "',"
            strSQL += " pengguna_status_perkahwinan = '" & oCommon.FixSingleQuotes(ddlStatus.SelectedValue) & "',"
            strSQL += " pengguna_no_tentera = '" & oCommon.FixSingleQuotes(txtNoPekerja.Text) & "',"
            strSQL += " pangkat_id = '" & oCommon.FixSingleQuotes(ddlJawatan.SelectedValue) & "',"
            strSQL += " pengguna_negara = '" & oCommon.FixSingleQuotes(ddlNegaraDaftar.SelectedValue) & "'"

            strSQL += " WHERE pengguna_id = '" & Request.QueryString("edit") & "'"

        Else
            strSQL = "INSERT INTO spk_pengguna (pangkalan_id,pangkat_id,
                        pengguna_mykad,pengguna_kewarganegaraan,pengguna_tarikh_lahir,pengguna_nama,
                        pengguna_alamat,pengguna_poskod,pengguna_bandar,pengguna_negeri,pengguna_negara,
                        pengguna_status_perkahwinan,pengguna_no_tentera)"

            strSQL += " VALUES ("
            strSQL += " UPPER('" & ddlCawangan.SelectedIndex & "'),"
            strSQL += " UPPER('" & ddlJawatan.SelectedValue & "'),"
            strSQL += " UPPER('" & oCommon.FixSingleQuotes(txtNoKP.Text) & "'),"
            strSQL += " UPPER('" & ddlKewarganegaraan.SelectedValue & "'),"
            strSQL += " UPPER('" & oCommon.FixSingleQuotes(txt_MyDate.Text) & "'),"
            strSQL += " UPPER('" & oCommon.FixSingleQuotes(txtNamaPertama.Text) & "'),"
            strSQL += " UPPER('" & txtAlamat1Daftar.Text + txtAlamat2Daftar.Text + txtAlamat3Daftar.Text & "'),"
            strSQL += " UPPER('" & oCommon.FixSingleQuotes(txtPoskodDaftar.Text) & "'),"
            strSQL += " UPPER('" & oCommon.FixSingleQuotes(txtBandarDaftar.Text) & "'),"
            strSQL += " UPPER('" & oCommon.FixSingleQuotes(ddlNegeriDaftar.Text) & "'),"
            strSQL += " UPPER('" & oCommon.FixSingleQuotes(ddlNegaraDaftar.Text) & "'),"
            strSQL += " UPPER('" & oCommon.FixSingleQuotes(ddlStatus.SelectedValue) & "'),"
            strSQL += " UPPER('" & txtNoPekerja.Text & "'))"

        End If

        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            Return True
        Else
            MsgTop.Attributes("class") = "errorMsg"
            strlbl_top.Text = strSysErrorAlert
            Return False
        End If

    End Function

    '--DATA VALIDATION--'
    Private Function ValidateData() As Boolean
        'If Not oCommon.isNumeric(txtidx.Text) Then
        '    txtidx.Focus()
        '    Return False

        'End If
        Return True
    End Function

    Private Sub SaveFunction_ServerClick(sender As Object, e As EventArgs) Handles SaveFunction.ServerClick
        '--validate--'
        If ValidateData() = False Then
            MsgTop.Attributes("class") = "errorMsg"
            strlbl_top.Text = strDataValAlert
            Exit Sub
        End If
        Try
            '--execute--'
            If Save() = True Then
                MsgTop.Attributes("class") = "successMsg"
                strlbl_top.Text = strSaveSuccessAlert
            Else
                MsgTop.Attributes("class") = "errorMsg"
                strlbl_top.Text = strSaveFailAlert
            End If
        Catch ex As Exception
            MsgTop.Attributes("class") = "errorMsg"
            strlbl_top.Text = strSysErrorAlert
        End Try

        ''strRet = BindData(datRespondent)
    End Sub
    '--REFRESH BUTTON--'
    Private Sub Refresh_ServerClick(sender As Object, e As EventArgs) Handles Refresh.ServerClick
    End Sub

    Private Sub requestPage()

    End Sub

    Private Sub clear()
        ddlCawangan.SelectedValue = ""
        ddlJabatan.SelectedValue = ""
        ddlJantina.SelectedValue = ""
        ddlJawatan.SelectedValue = ""
        ddlKewarganegaraan.SelectedValue = ""
        ddlNegaraDaftar.SelectedValue = ""
        ddlNegeriDaftar.SelectedValue = ""
        ddlStatus.SelectedValue = ""
        txtAlamat1Daftar.Text = ""
        txtAlamat2Daftar.Text = ""
        txtAlamat3Daftar.Text = ""
        txtBandarDaftar.Text = ""
        txtEmail.Text = ""
        txtNamaPertama.Text = ""
        txtNoKP.Text = ""
        txtNoKWSP.Text = ""
        txtNoPekerja.Text = ""
        txtNoPERKESO.Text = ""
        txtPoskodDaftar.Text = ""
        txtPrefix.Text = ""
        txtSuffix.Text = ""
        txtTelefon.Text = ""
        txt_MyDate.Text = ""
    End Sub


End Class