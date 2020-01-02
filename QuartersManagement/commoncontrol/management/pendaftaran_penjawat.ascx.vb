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
        Dim Status As String = Request.QueryString("z")
        Try
            If Request.QueryString("z") = "Edit" Then
                If Not IsPostBack Then

                    If strlblMsgTop = 0 Then
                        strlbl_top.Visible = True
                    Else
                        strlbl_top.Visible = False
                    End If
                    populateJawatan()
                    populateNegeri()
                    populateJabatan()
                    populatePangkalan()
                    Load_page()

                End If

            Else

                If Not IsPostBack Then

                    If strlblMsgTop = 0 Then
                        strlbl_top.Visible = True
                    Else
                        strlbl_top.Visible = False
                    End If
                    populateJawatan()
                    populateNegeri()
                    populateJabatan()
                    populatePangkalan()

                End If

            End If
        Catch ex As Exception
            MsgTop.Attributes("class") = "errorMsg"
            strlbl_top.Text = strSysErrorAlert
        Finally
        End Try
    End Sub

    Private Sub Load_page()

        strSQL = " SELECT pengguna_id,pangkalan_id,pangkat_id,pengguna_jenis,pengguna_mykad,pengguna_jantina,pengguna_kewarganegaraan
                    ,pengguna_tarikh_lahir,pengguna_nama,pengguna_alamat,pengguna_poskod,pengguna_bandar,pengguna_negeri
                    ,pengguna_negara,pengguna_status_perkahwinan,pengguna_no_tentera,pengguna_mula_perkhidmatan,pengguna_tamat_perkhidmatan FROM spk_pengguna"
        strSQL += " WHERE pengguna_id = '" & Request.QueryString("edit") & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("pangkalan_id")) Then
                    ddlCawangan.SelectedValue = ds.Tables(0).Rows(0).Item("pangkalan_id")
                Else
                    ddlCawangan.SelectedValue = ""
                End If

                'If Not IsDBNull(ds.Tables(0).Rows(0).Item("pengguna_mykad")) Then
                '    txtNoKP.Text = ds.Tables(0).Rows(0).Item("pengguna_mykad")
                'Else
                '    txtNoKP.Text = ""
                'End If

                'If Not IsDBNull(ds.Tables(0).Rows(0).Item("pengguna_kewarganegaraan")) Then
                '    ddlKewarganegaraan.SelectedValue = ds.Tables(0).Rows(0).Item("pengguna_kewarganegaraan")
                'Else
                '    ddlKewarganegaraan.SelectedValue = ""
                'End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("pengguna_tarikh_lahir")) Then
                    txt_MyDate.Text = ds.Tables(0).Rows(0).Item("pengguna_tarikh_lahir")
                Else
                    txt_MyDate.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("pengguna_nama")) Then
                    txtNamaPertama.Text = ds.Tables(0).Rows(0).Item("pengguna_nama")
                Else
                    txtNamaPertama.Text = ""
                End If

                'If Not IsDBNull(ds.Tables(0).Rows(0).Item("pengguna_telefon")) Then
                '    txtTelefon.Text = ds.Tables(0).Rows(0).Item("pengguna_telefon")
                'Else
                '    txtTelefon.Text = ""
                'End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("pengguna_poskod")) Then
                    txtPoskodDaftar.Text = ds.Tables(0).Rows(0).Item("pengguna_poskod")
                Else
                    txtPoskodDaftar.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("pengguna_negeri")) Then
                    ddlNegeriDaftar.SelectedValue = ds.Tables(0).Rows(0).Item("pengguna_negeri")
                Else
                    ddlNegeriDaftar.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("pengguna_no_tentera")) Then
                    txtNoPekerja.Text = ds.Tables(0).Rows(0).Item("pengguna_no_tentera")
                Else
                    txtNoPekerja.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("pangkat_id")) Then
                    ddlJawatan.SelectedValue = ds.Tables(0).Rows(0).Item("pangkat_id")
                Else
                    ddlJawatan.SelectedValue = ""
                End If

                'If Not IsDBNull(ds.Tables(0).Rows(0).Item("pengguna_negara")) Then
                '    ddlNegaraDaftar.SelectedValue = ds.Tables(0).Rows(0).Item("pengguna_negara")
                'Else
                '    ddlNegaraDaftar.SelectedValue = ""
                'End If

            End If
        Catch ex As Exception
            MsgTop.Attributes("class") = "errorMsg"
            'strlbl_top.Text = strSysErrorAlert
            strlbl_top.Text = ex.ToString
        Finally
            objConn.Dispose()
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

    'Protected Sub populateNegara()
    '    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    '    Dim objConn As SqlConnection = New SqlConnection(strConn)

    '    strSQL = "SELECT config_id, config_parameter FROM general_config WHERE config_access = 'GLOBAL' AND config_type = 'NEGARA' ORDER BY config_idx, config_parameter"
    '    Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

    '    Try
    '        Dim ds As DataSet = New DataSet
    '        sqlDA.Fill(ds, "AnyTable")

    '        ddlNegaraDaftar.DataSource = ds
    '        ddlNegaraDaftar.DataTextField = "config_parameter"
    '        ddlNegaraDaftar.DataValueField = "config_parameter"
    '        ddlNegaraDaftar.DataBind()
    '        ddlNegaraDaftar.Items.Insert(0, New ListItem("- PILIH -", String.Empty))

    '    Catch ex As Exception

    '    Finally
    '        objConn.Dispose()
    '    End Try
    'End Sub

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
        Try
            If Request.QueryString("z").Equals("Edit") Then

                strSQL = "UPDATE spk_pengguna SET "

                strSQL += " pangkalan_id = '" & oCommon.FixSingleQuotes(ddlCawangan.SelectedValue) & "',"
                strSQL += " pengguna_tarikh_lahir = '" & txt_MyDate.Text & "',"
                strSQL += " pengguna_nama = '" & oCommon.FixSingleQuotes(txtNamaPertama.Text) & "',"
                strSQL += " pengguna_alamat = '" & oCommon.FixSingleQuotes(txtAlamat1Daftar.Text + txtAlamat2Daftar.Text + txtAlamat3Daftar.Text) & "',"
                strSQL += " pengguna_poskod = '" & oCommon.FixSingleQuotes(txtPoskodDaftar.Text) & "',"
                strSQL += " pengguna_bandar = '" & oCommon.FixSingleQuotes(txtBandarDaftar.Text) & "',"
                strSQL += " pengguna_negeri = '" & oCommon.FixSingleQuotes(ddlNegeriDaftar.SelectedValue) & "',"
                strSQL += " pengguna_no_tentera = '" & oCommon.FixSingleQuotes(txtNoPekerja.Text) & "',"
                strSQL += " pangkat_id = '" & oCommon.FixSingleQuotes(ddlJawatan.SelectedValue) & "',"
                strSQL += " pengguna_jantina = '" & oCommon.FixSingleQuotes(ddlJantina.SelectedValue) & "'"

                strSQL += " WHERE pengguna_id = '" & Request.QueryString("edit") & "'"
            ElseIf Request.QueryString("z").Equals("Insert") Then
                strSQL = "INSERT INTO spk_pengguna (pangkalan_id,pangkat_id,
                        pengguna_mykad,pengguna_kewarganegaraan,pengguna_tarikh_lahir,pengguna_nama,
                        pengguna_alamat,pengguna_poskod,pengguna_bandar,pengguna_negeri,pengguna_negara,
                        pengguna_status_perkahwinan,pengguna_no_tentera)"

                strSQL += " VALUES ("
                strSQL += " UPPER('" & ddlCawangan.SelectedIndex & "'),"
                strSQL += " UPPER('" & ddlJawatan.SelectedValue & "'),"
                strSQL += " UPPER('" & oCommon.FixSingleQuotes(txt_MyDate.Text) & "'),"
                strSQL += " UPPER('" & oCommon.FixSingleQuotes(txtNamaPertama.Text) & "'),"
                strSQL += " UPPER('" & txtAlamat1Daftar.Text + txtAlamat2Daftar.Text + txtAlamat3Daftar.Text & "'),"
                strSQL += " UPPER('" & oCommon.FixSingleQuotes(txtPoskodDaftar.Text) & "'),"
                strSQL += " UPPER('" & oCommon.FixSingleQuotes(txtBandarDaftar.Text) & "'),"
                strSQL += " UPPER('" & oCommon.FixSingleQuotes(ddlNegeriDaftar.Text) & "'),"
                strSQL += " UPPER('" & txtNoPekerja.Text & "'))"
            Else
                Debug.WriteLine("Error(Save-pendaftaran_penjawat:319): Save type bukan Insert/Edit")
                Return False
            End If

            strRet = oCommon.ExecuteSQL(strSQL)

            If strRet = "0" Then
                Return True
            Else
                MsgTop.Attributes("class") = "errorMsg"
                strlbl_top.Text = strSysErrorAlert
                Return False
            End If
        Catch ex As Exception
            Debug.WriteLine("Error(Save-pendaftaran_penjawat:333): " & ex.Message)
            Return False
        End Try
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
        ddlJantina.SelectedValue = ""
        ddlJawatan.SelectedValue = ""
        ddlNegeriDaftar.SelectedValue = ""
        txtAlamat1Daftar.Text = ""
        txtAlamat2Daftar.Text = ""
        txtAlamat3Daftar.Text = ""
        txtBandarDaftar.Text = ""
        txtEmail.Text = ""
        txtNamaPertama.Text = ""
        txtNoPekerja.Text = ""
        txtPoskodDaftar.Text = ""
        txtTelefon.Text = ""
        txt_MyDate.Text = ""
    End Sub
End Class