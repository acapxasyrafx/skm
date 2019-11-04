Imports System.Data.SqlClient

Public Class konfigurasi_negara
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

        Try

            If Not IsPostBack Then

                If strlblMsgBottom = 0 Then
                    strlbl_bottom.Visible = True
                Else
                    strlbl_bottom.Visible = False
                End If
                If strlblMsgTop = 0 Then
                    strlbl_top.Visible = True
                Else
                    strlbl_top.Visible = False
                End If

                requestNegara()
                requestNegeri()

                If Not Request.QueryString("edit") = "" Then
                    lblConfig.Text = Request.QueryString("p")
                    Load_page()
                Else
                    requestPage()
                    strRet = BindData(datRespondent)
                End If

            End If

        Catch ex As Exception

            MsgTop.Attributes("class") = "errorMsg"
            strlbl_top.Text = strSysErrorAlert
            MsgBottom.Attributes("class") = "errorMsg"
            strlbl_bottom.Text = strSysErrorAlert & "<br>" & ex.Message

        Finally

        End Try

    End Sub

    Private Sub requestNegara()

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        strSQL = "SELECT config_id, config_parameter FROM general_config WHERE config_access = 'GLOBAL' AND config_type = 'NEGARA' ORDER BY config_idx, config_parameter"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlNegara.DataSource = ds
            ddlNegara.DataTextField = "config_parameter"
            ddlNegara.DataValueField = "config_parameter"
            ddlNegara.DataBind()
            ddlNegara.Items.Insert(0, New ListItem("- PILIH -", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub requestNegeri()

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        strSQL = "SELECT config_id, config_parameter FROM general_config WHERE config_access = 'GLOBAL' AND config_type = 'NEGERI' ORDER BY config_idx, config_parameter"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlNegeri.DataSource = ds
            ddlNegeri.DataTextField = "config_parameter"
            ddlNegeri.DataValueField = "config_parameter"
            ddlNegeri.DataBind()
            ddlNegeri.Items.Insert(0, New ListItem("- PILIH -", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub Load_page()

        strSQL = " SELECT pangkalan_id, pangkalan_negara, pangkalan_negeri, pangkalan_nama, pangkalan_alamat, pangkalan_poskod, pangkalan_bandar, pangkalan_telefon, pangkalan_faks, pangkalan_emel FROM spk_pangkalan"
        strSQL += " WHERE pangkalan_id = '" & Request.QueryString("edit") & "'"

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

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("pangkalan_negara")) Then
                    ddlNegara.SelectedValue = ds.Tables(0).Rows(0).Item("pangkalan_negara")
                Else
                    ddlNegara.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("pangkalan_negeri")) Then
                    ddlNegeri.SelectedValue = ds.Tables(0).Rows(0).Item("pangkalan_negeri")
                Else
                    ddlNegeri.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("pangkalan_nama")) Then
                    txtNamaPangkalan.Text = ds.Tables(0).Rows(0).Item("pangkalan_nama")
                Else
                    txtNamaPangkalan.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("pangkalan_alamat")) Then
                    txtAlamat.Text = ds.Tables(0).Rows(0).Item("pangkalan_alamat")
                Else
                    txtAlamat.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("pangkalan_poskod")) Then
                    txtPoskod.Text = ds.Tables(0).Rows(0).Item("pangkalan_poskod")
                Else
                    txtPoskod.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("pangkalan_bandar")) Then
                    txtBandar.Text = ds.Tables(0).Rows(0).Item("pangkalan_bandar")
                Else
                    txtBandar.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("pangkalan_telefon")) Then
                    txtTelefon.Text = ds.Tables(0).Rows(0).Item("pangkalan_telefon")
                Else
                    txtTelefon.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("pangkalan_faks")) Then
                    txtFaks.Text = ds.Tables(0).Rows(0).Item("pangkalan_faks")
                Else
                    txtFaks.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("pangkalan_emel")) Then
                    txtEmel.Text = ds.Tables(0).Rows(0).Item("pangkalan_emel")
                Else
                    txtEmel.Text = ""
                End If

            End If
            strRet = BindData(datRespondent)
        Catch ex As Exception
            MsgTop.Attributes("class") = "errorMsg"
            strlbl_top.Text = strSysErrorAlert
            MsgBottom.Attributes("class") = "errorMsg"
            strlbl_bottom.Text = strSysErrorAlert & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    '-- BIND DATA --'
    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""

        Dim strOrder As String = " ORDER BY pangkalan_nama ASC"

        tmpSQL = "SELECT pangkalan_id, pangkalan_negara, pangkalan_negeri, pangkalan_nama, pangkalan_alamat, pangkalan_poskod, pangkalan_bandar, pangkalan_telefon, pangkalan_faks, pangkalan_emel FROM spk_pangkalan"

        strWhere += " WHERE pangkalan_id IS NOT NULL"

        If Not ddlNegara.SelectedValue = "" Then
            strWhere += " AND pangkalan_negara = '" & ddlNegara.SelectedValue & "'"
        End If

        If Not ddlNegeri.SelectedValue = "" Then
            strWhere += " AND pangkalan_negeri = '" & ddlNegeri.SelectedValue & "'"
        End If

        If Not txtNamaPangkalan.Text = "" Then
            strWhere += " AND pangkalan_nama LIKE '%" & txtNamaPangkalan.Text & "%'"
        End If

        getSQL = tmpSQL & strWhere & strOrder

        Return getSQL

    End Function

    Private Function GetData(ByVal cmd As SqlCommand) As DataTable
        Dim dt As New DataTable()
        Dim strConnString As [String] = ConfigurationManager.AppSettings("ConnectionString")
        Dim con As New SqlConnection(strConnString)
        Dim sda As New SqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        Try
            con.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
            sda.Dispose()
            con.Dispose()
        End Try
    End Function

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then

                MsgBottom.Attributes("class") = "errorMsg"
                strlbl_bottom.Text = strDataBindAlert
            Else

                MsgBottom.Attributes("class") = "successMsg"
                strlbl_bottom.Text = strRecordBindAlert & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception

            MsgBottom.Attributes("class") = "errorMsg"
            strlbl_bottom.Text = strSysErrorAlert & "<br>" & ex.Message
            Return False
        End Try

        Return True

    End Function

    '--SAVE FUNCTION--'
    Private Function Save() As Boolean

        If Not Request.QueryString("edit") = "" Then

            strSQL = "UPDATE spk_pangkalan SET "

            strSQL += " pangkalan_negara = '" & oCommon.FixSingleQuotes(ddlNegara.SelectedValue) & "',"
            strSQL += " pangkalan_negeri = '" & oCommon.FixSingleQuotes(ddlNegeri.SelectedValue) & "',"
            strSQL += " pangkalan_nama = '" & oCommon.FixSingleQuotes(txtNamaPangkalan.Text) & "',"
            strSQL += " pangkalan_alamat = '" & txtAlamat.Text & "',"
            strSQL += " pangkalan_poskod = '" & oCommon.FixSingleQuotes(txtPoskod.Text) & "',"
            strSQL += " pangkalan_bandar = '" & oCommon.FixSingleQuotes(txtBandar.Text) & "',"
            strSQL += " pangkalan_telefon = '" & oCommon.FixSingleQuotes(txtTelefon.Text) & "',"
            strSQL += " pangkalan_faks = '" & oCommon.FixSingleQuotes(txtFaks.Text) & "',"
            strSQL += " pangkalan_emel = '" & txtEmel.Text & "'"


            strSQL += " WHERE pangkalan_id = '" & Request.QueryString("edit") & "'"

        Else
            strSQL = "INSERT INTO spk_pangkalan (pangkalan_negara, pangkalan_negeri, pangkalan_nama, pangkalan_alamat, pangkalan_poskod, pangkalan_bandar, pangkalan_telefon, pangkalan_faks, pangkalan_emel)"

            strSQL += " VALUES ("
            strSQL += " UPPER('" & ddlNegara.SelectedValue & "'),"
            strSQL += " UPPER('" & ddlNegeri.SelectedValue & "'),"
            strSQL += " UPPER('" & oCommon.FixSingleQuotes(txtNamaPangkalan.Text) & "'),"
            strSQL += " UPPER('" & txtAlamat.Text & "'),"
            strSQL += " UPPER('" & oCommon.FixSingleQuotes(txtPoskod.Text) & "'),"
            strSQL += " UPPER('" & oCommon.FixSingleQuotes(txtBandar.Text) & "'),"
            strSQL += " UPPER('" & oCommon.FixSingleQuotes(txtTelefon.Text) & "'),"
            strSQL += " UPPER('" & oCommon.FixSingleQuotes(txtFaks.Text) & "'),"
            strSQL += " UPPER('" & txtEmel.Text & "'))"

        End If

        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            Return True
        Else
            MsgTop.Attributes("class") = "errorMsg"
            strlbl_top.Text = strSysErrorAlert
            MsgBottom.Attributes("class") = "errorMsg"
            strlbl_bottom.Text = strSysErrorAlert & "<br>" & strRet
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

        strlbl_bottom.Text = ""
        strlbl_top.Text = ""
        '--validate--'
        If ValidateData() = False Then
            MsgTop.Attributes("class") = "errorMsg"
            strlbl_top.Text = strDataValAlert
            MsgBottom.Attributes("class") = "errorMsg"
            strlbl_bottom.Text = strDataValAlert
            Exit Sub
        End If
        Try
            '--execute--'
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
        End Try

        If Not Request.QueryString("edit") = "" Then
            Dim Pagelabel As String = lblConfig.Text & "&q=" & lblQ.Text & "&lblTop=" & strlbl_top.Text & "&lblBottom=" & strlbl_top.Text
            Response.Redirect("Konfigurasi.Pangkalan.aspx?p=" & Pagelabel)
        Else
            strRet = BindData(datRespondent)
        End If



    End Sub
    '--REFRESH BUTTON--'
    Private Sub Refresh_ServerClick(sender As Object, e As EventArgs) Handles Refresh.ServerClick
        Dim Pagelabel As String = lblConfig.Text & "&q=" & lblQ.Text
        Response.Redirect("Konfigurasi.Pangkalan.aspx?p=" & Pagelabel)
    End Sub

    Private Sub requestPage()
        lblConfig.Text = Request.QueryString("p")
        lblQ.Text = Request.QueryString("q")
        If Not Request.QueryString("lblBottom") = "" Then
            strlbl_top.Text = Request.QueryString("lblTop")
            strlbl_bottom.Text = Request.QueryString("lblBottom")
        End If
    End Sub

    Private Sub clear()
        ddlNegara.SelectedValue = ""
        ddlNegeri.SelectedValue = ""
        txtNamaPangkalan.Text = ""
        txtAlamat.Text = ""
        txtPoskod.Text = ""
        txtBandar.Text = ""
        txtTelefon.Text = ""
        txtFaks.Text = ""
        txtEmel.Text = ""
    End Sub

    '--DELETE FUNCTION--'
    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting

        Dim strCID = datRespondent.DataKeys(e.RowIndex).Values("pangkalan_id").ToString

        'chk session to prevent postback
        If Not strCID = Session("strCID") Then
            strSQL = "DELETE FROM spk_pangkalan WHERE pangkalan_id = '" & oCommon.FixSingleQuotes(strCID) & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            Session("strCID") = ""
        End If
        strRet = BindData(datRespondent)

    End Sub

    '--INDEX CHANGE--'
    Private Sub ddlNegara_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegara.SelectedIndexChanged
        strRet = BindData(datRespondent)
    End Sub

    Private Sub ddlNegeri_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegeri.SelectedIndexChanged
        strRet = BindData(datRespondent)
    End Sub

End Class