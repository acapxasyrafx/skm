Imports System.Data.SqlClient

Public Class konfigurasi_khusus

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

    '--LOAD FOR EDIT--'
    Private Sub Load_page()

        strSQL = " SELECT config_type, config_parameter, config_value, config_code, config_desc, config_idx FROM general_config"
        strSQL += " WHERE config_id = '" & Request.QueryString("edit") & "'"

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

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("config_type")) Then
                    lblQ.Text = ds.Tables(0).Rows(0).Item("config_type")
                Else
                    lblQ.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("config_parameter")) Then
                    txtParameter.Text = ds.Tables(0).Rows(0).Item("config_parameter")
                Else
                    txtParameter.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("config_value")) Then
                    txtValue.Text = ds.Tables(0).Rows(0).Item("config_value")
                Else
                    txtValue.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("config_code")) Then
                    txtCode.Text = ds.Tables(0).Rows(0).Item("config_code")
                Else
                    txtCode.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("config_desc")) Then
                    txtDesc.Text = ds.Tables(0).Rows(0).Item("config_desc")
                Else
                    txtDesc.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("config_idx")) Then
                    txtIdx.Text = ds.Tables(0).Rows(0).Item("config_idx")
                Else
                    txtIdx.Text = ""
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
        Dim strOrder As String = ""
        Dim Action As String = lblQ.Text

        tmpSQL = "SELECT config_id, config_access, config_type, config_parameter, config_value, config_code, config_desc, config_idx FROM general_config"

        strWhere += " WHERE config_type = '" & lblQ.Text & "'"

        strOrder = " ORDER BY config_idx, config_parameter ASC"

        If Not ddlAkses.SelectedValue = "" Then
            strWhere += " AND config_access = '" & oCommon.FixSingleQuotes(ddlAkses.SelectedValue) & "'"
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

    '--DATA VALIDATION--'
    Private Function ValidateData() As Boolean
        If Not oCommon.isNumeric(txtidx.Text) Then
            txtidx.Focus()
            Return False

        End If
        Return True
    End Function

    ''--SAVE FUNCTION--'
    Private Function Save() As Boolean

        If Not Request.QueryString("edit") = "" Then

            strSQL = "UPDATE general_config SET "

            strSQL += " config_parameter = UPPER('" & txtParameter.Text & "'),"
            strSQL += " config_value = UPPER('" & txtValue.Text & "'),"
            strSQL += " config_code = UPPER('" & txtCode.Text & "'),"
            strSQL += " config_desc = UPPER('" & txtDesc.Text & "'),"
            strSQL += " config_idx = '" & txtIdx.Text & "'"

            strSQL += " WHERE config_id='" & Request.QueryString("edit") & "'"

        Else

            strSQL = "INSERT INTO general_config(config_access, config_type, config_parameter, config_value, config_code, config_desc, config_idx)"

            strSQL += " VALUES ("
            strSQL += " UPPER('" & ddlAkses.SelectedValue & "'),"
            strSQL += " UPPER('" & lblQ.Text & "'),"
            strSQL += " UPPER('" & txtParameter.Text & "'),"
            strSQL += " UPPER('" & txtValue.Text & "'),"
            strSQL += " UPPER('" & txtCode.Text & "'),"
            strSQL += " UPPER('" & txtDesc.Text & "'),"
            strSQL += " UPPER('" & oCommon.FixSingleQuotes(txtIdx.Text) & "'))"

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
            Response.Redirect("Konfigurasi.Khusus.aspx?p=" & Pagelabel)
        Else
            strRet = BindData(datRespondent)
        End If



    End Sub
    '--REFRESH BUTTON--'
    Private Sub Refresh_ServerClick(sender As Object, e As EventArgs) Handles Refresh.ServerClick
        Dim Pagelabel As String = lblConfig.Text & "&q=" & lblQ.Text
        Response.Redirect("Konfigurasi.Khusus.aspx?p=" & Pagelabel)
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

        txtParameter.Text = ""
        ddlAkses.SelectedValue = "GLOBAL"
        txtCode.Text = ""
        txtValue.Text = ""
        txtIdx.Text = ""
        txtDesc.Text = ""

    End Sub

    '--DELETE FUNCTION--'
    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting

        Dim strCID = datRespondent.DataKeys(e.RowIndex).Values("config_id").ToString

        'chk session to prevent postback
        If Not strCID = Session("strCID") Then
            strSQL = "DELETE FROM general_config WHERE config_id='" & oCommon.FixSingleQuotes(strCID) & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                MsgTop.Attributes("class") = "successMsg"
                strlbl_top.Text = strSucDelAlert
                MsgBottom.Attributes("class") = "successMsg"
                strlbl_bottom.Text = strSucDelAlert
            Else
                MsgTop.Attributes("class") = "errorMsg"
                strlbl_top.Text = strFailDelAlert
                MsgBottom.Attributes("class") = "errorMsg"
                strlbl_bottom.Text = strFailDelAlert


            End If
            Session("strCID") = ""
        End If
        strRet = BindData(datRespondent)

    End Sub

End Class