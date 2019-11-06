Imports System.Data.SqlClient

Public Class senarai_penempatan_pemohon
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
                Load_Page()
            End If

        Catch ex As Exception

            MsgTop.Attributes("class") = "errorMsg"
            strlbl_top.Text = strSysErrorAlert
            MsgBottom.Attributes("class") = "errorMsg"
            strlbl_bottom.Text = strSysErrorAlert & "<br>" & ex.Message

        Finally

        End Try
    End Sub

    Private Sub Load_Page()
        strSQL = "
            SELECT 
	            A.pengguna_id,
	            A.pengguna_nama,
	            B.pangkalan_nama,
	            C.kuarters_nama,
	            D.unit_nombor,
	            D.unit_tingkat,
	            D.unit_blok,
	            (D.unit_nombor + D.unit_tingkat + D.unit_blok) as 'unit_location'
            FROM 
	            spk_pengguna A
	            JOIN spk_pangkalan B ON B.pangkalan_id = A.pangkalan_id
	            JOIN spk_kuarters C ON C.pangkalan_id = B.pangkalan_id
	            JOIN spk_unit D ON D.kuarters_id = C.kuarters_id
            ;
        "

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectioNString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")
            Dim nRow As Integer = 0
            Dim nCount As Integer = 1
            Dim myTable As DataTable = New DataTable
            myTable = ds.Tables(0)
            If myTable.Rows.Count > 0 Then

            End If
            strRet = BindData(datRespondent)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Refresh_ServerClick(sender As Object, e As EventArgs) Handles Refresh.ServerClick
        Response.Redirect("Senarai.Penempatan.Pemohon.aspx")
    End Sub

    Private Function ValidateData() As Boolean
        Return True
    End Function

    Private Function Save() As Boolean
        Return False
    End Function

    Private Function getSQL() As String
        getSQL = "
            SELECT 
	            A.pengguna_id,
	            A.pengguna_nama,
	            B.pangkalan_nama,
	            C.kuarters_nama,
	            D.unit_nombor,
	            D.unit_tingkat,
	            D.unit_blok,
	            (D.unit_blok + '-' + D.unit_tingkat + '-' + D.unit_nombor) as 'unit_location'
            FROM 
	            spk_pengguna A
	            JOIN spk_pangkalan B ON B.pangkalan_id = A.pangkalan_id
	            JOIN spk_kuarters C ON C.pangkalan_id = B.pangkalan_id
	            JOIN spk_unit D ON D.kuarters_id = C.kuarters_id
            ;
        "
        Return getSQL
    End Function

    Private Function GetData(ByVal cmd As SqlCommand) As DataTable
        Dim dt As New DataTable
        Dim strConString As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim conn As New SqlConnection(strConString)
        Dim sda As New SqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.Connection = conn
        Try
            conn.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
            sda.Dispose()
            conn.Dispose()
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
            Else
                MsgBottom.Attributes("class") = "successMsg"
            End If
            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            MsgBottom.Attributes("class") = "errorMsg"
            Return False
        End Try
        Return False
    End Function

End Class