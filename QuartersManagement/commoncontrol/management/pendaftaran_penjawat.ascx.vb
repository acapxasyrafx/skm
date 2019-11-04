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

End Class