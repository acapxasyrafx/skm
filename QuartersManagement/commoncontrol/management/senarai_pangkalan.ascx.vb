Imports System.Data.SqlClient

Public Class senarai_pangkalan1
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

        Catch ex As Exception

            MsgTop.Attributes("class") = "errorMsg"
            strlbl_top.Text = strSysErrorAlert
            MsgBottom.Attributes("class") = "errorMsg"
            strlbl_bottom.Text = strSysErrorAlert & "<br>" & ex.Message

        Finally

        End Try

    End Sub



    '-- BIND DATA --'
    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""

        Dim strOrder As String = " ORDER BY pangkalan_nama ASC"

        tmpSQL = "SELECT pangkalan_id, pangkalan_negara, pangkalan_negeri, pangkalan_nama, pangkalan_alamat, pangkalan_poskod, pangkalan_bandar, pangkalan_telefon, pangkalan_faks, pangkalan_emel FROM spk_pangkalan"

        strWhere += " WHERE pangkalan_id IS NOT NULL"


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
        'If Not oCommon.isNumeric(txtidx.Text) Then
        '    txtidx.Focus()
        '    Return False

        'End If
        Return True
    End Function

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

End Class