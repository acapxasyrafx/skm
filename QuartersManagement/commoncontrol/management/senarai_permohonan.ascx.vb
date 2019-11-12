Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Public Class senarai_permohonan
    Inherits System.Web.UI.UserControl

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

    Dim oCommon As New Commonfunction

    Dim penggunaID As Integer = 1
    Dim pangkalanID As Integer = 0
    Dim permohonanID As Integer = 14
    Dim statusPermohon As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Load_Page()
        End If
    End Sub

    Private Sub Load_Page()
        senaraiPermohonan()
    End Sub

    Private Sub senaraiPermohonan()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim ds As New DataSet
            Dim da As New SqlDataAdapter("SELECT 
	            A.permohonan_id,
	            A.pemohonan_tarikh,
	            B.kuarters_nama,
	            A.permohonan_status,
                A.permohonan_nota
            FROM 
	            spk_permohonan A
	            JOIN spk_kuarters B ON B.kuarters_id = A.kuarters_id
            WHERE A.pengguna_id = " & penggunaID & "
            ORDER BY A.pemohonan_tarikh DESC", conn)
            Try
                conn.Open()
                da.Fill(ds, "AnyTable")
                If ds.Tables(0).Rows.Count > 0 Then
                    tblSenaraiPermohonan.DataSource = ds
                    tblSenaraiPermohonan.DataBind()
                End If
            Catch ex As Exception
                Debug.WriteLine("Error(senaraiPermohonan): " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub tblSenaraiPermohonan_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles tblSenaraiPermohonan.RowCommand
        If e.CommandName = "View_Permohonan" Then
            Response.Redirect("Maklumat.Permohonan.Pengguna.aspx?permohonan=" & e.CommandArgument)
        ElseIf e.CommandName = "Delete_Permohonan" Then
            Debug.WriteLine("CommangArgument:" & e.CommandArgument)
        End If
    End Sub
End Class