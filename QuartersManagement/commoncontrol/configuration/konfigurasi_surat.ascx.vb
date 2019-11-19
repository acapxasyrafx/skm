Imports System.Data.SqlClient

Public Class konfigurasi_surat
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

    Private Sub Load_page()

        strSQL = " SELECT * FROM spk_suratTawaranConfig"
        strSQL += " WHERE suratTawaranConfig_id = '" & Request.QueryString("edit") & "'"

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

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("suratTawaranConfig_parameter")) Then
                    txtIsiSurat.Text = ds.Tables(0).Rows(0).Item("suratTawaranConfig_parameter")
                Else
                    txtIsiSurat.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("suratTawaranConfig_type")) Then
                    txtJenissurat.Text = ds.Tables(0).Rows(0).Item("suratTawaranConfig_type")
                Else
                    txtJenissurat.Text = ""
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

        Dim strOrder As String = " ORDER BY suratTawaranConfig_id ASC"

        tmpSQL = "SELECT * FROM spk_suratTawaranConfig"

        strWhere += " WHERE suratTawaranConfig_id IS NOT NULL"

        If Not txtIsiSurat.Text = "" Then

            strWhere += " AND suratTawaranConfig_parameter LIKE '%" & txtIsiSurat.Text & "%'"

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

            strSQL = "UPDATE spk_suratTawaranConfig SET "

            strSQL += " suratTawaranConfig_parameter = '" & txtIsiSurat.Text & "',"
            strSQL += " suratTawaranConfig_type = UPPER('" & txtJenissurat.Text & "')"

            strSQL += " WHERE suratTawaranConfig_id = '" & Request.QueryString("edit") & "'"

        Else
            strSQL = "INSERT INTO spk_suratTawaranConfig (suratTawaranConfig_parameter, suratTawaranConfig_type)"

            strSQL += " VALUES ("
            strSQL += " '" & txtIsiSurat.Text & "',"
            strSQL += " UPPER('" & txtJenissurat.Text & "'))"

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
            Response.Redirect("Konfigurasi.Surat.Tawaran.aspx?p=" & Pagelabel)
        Else
            strRet = BindData(datRespondent)
        End If



    End Sub
    '--REFRESH BUTTON--'
    Private Sub Refresh_ServerClick(sender As Object, e As EventArgs) Handles Refresh.ServerClick
        Dim Pagelabel As String = lblConfig.Text & "&q=" & lblQ.Text
        Response.Redirect("Konfigurasi.Jenis.Kuarters.aspx?p=" & Pagelabel)
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
        txtIsiSurat.Text = ""
        txtJenissurat.Text = ""
    End Sub

    '--DELETE FUNCTION--'
    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting

        Dim strCID = datRespondent.DataKeys(e.RowIndex).Values("suratTawaranConfig_id").ToString

        'chk session to prevent postback
        If Not strCID = Session("strCID") Then
            strSQL = "DELETE FROM spk_suratTawaranConfig WHERE suratTawaranConfig_id = '" & oCommon.FixSingleQuotes(strCID) & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            Session("strCID") = ""
        End If
        strRet = BindData(datRespondent)

    End Sub

End Class