Imports System.Data.SqlClient

Public Class senarai_penjawat
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
            Try
                BindData(datRespondent)
                loadDDLCarianPangkat()
            Catch ex As Exception

                MsgTop.Attributes("class") = "errorMsg"
                strlbl_top.Text = strSysErrorAlert
                MsgBottom.Attributes("class") = "errorMsg"
                strlbl_bottom.Text = strSysErrorAlert & "<br>" & ex.Message

            Finally

            End Try
        End If


    End Sub

    '-- BIND DATA --'
    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""

        Dim strOrder As String = " ORDER BY Pangkat_idx ASC"

        tmpSQL = "SELECT
	        A.pengguna_id as pengguna_id 
	        , B.pangkat_nama as pangkat 
	        , A.pengguna_no_tentera as no_tentera 
	        , A.pengguna_nama as nama 
        FROM 
	        spk_pengguna A
	        LEFT JOIN spk_pangkat B ON B.pangkat_id = A.pangkat_id"
        strWhere += " WHERE A.pengguna_id IS NOT NULL"

        If txtCarian.Text.Length > 0 Then
            strWhere += String.Format(" AND A.pengguna_nama LIKE '%{0}%' OR A.pengguna_no_tentera LIKE '%{0}%'", txtCarian.Text)
        End If

        If ddlCarianPangkat.SelectedIndex > 0 Then
            strWhere += " AND B.pangkat_id = " & ddlCarianPangkat.SelectedValue & ""
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

    Private Sub loadDDLCarianPangkat()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Try
                conn.Open()
                Dim ds As New DataSet
                Dim da As New SqlDataAdapter("SELECT pangkat_id,pangkat_nama FROM spk_pangkat", conn)
                da.Fill(ds, "AnyTable")
                ddlCarianPangkat.DataSource = ds
                ddlCarianPangkat.DataTextField = "pangkat_nama"
                ddlCarianPangkat.DataValueField = "pangkat_id"
                ddlCarianPangkat.DataBind()
                ddlCarianPangkat.Items.Insert(0, New ListItem("-- SILA PILIH --", ""))
            Catch ex As Exception
                Debug.WriteLine("Error(loadDDDLCarianPangkat): " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Private Sub btnCarian_Click(sender As Object, e As EventArgs) Handles btnCarian.Click
        BindData(datRespondent)
    End Sub

    Private Sub ddlCarianPangkat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCarianPangkat.SelectedIndexChanged
        BindData(datRespondent)
    End Sub
End Class