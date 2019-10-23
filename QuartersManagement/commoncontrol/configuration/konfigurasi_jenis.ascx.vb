Imports System.Data.SqlClient

Public Class konfigurasi_jenis1
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            setDDLJenis()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub setDDLJenis()
        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT id,jenis FROM admin.kuartersJenis"

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        Dim quantity As Integer = attachmentsTable.Rows.Count

        ddlJenis.Items.Clear()

        ddlJenis.Items.Add(New ListItem("-- Pilih Jenis Rumah --", 0))

        For k = 0 To quantity - 1
            ddlJenis.Items.Add(New ListItem(attachmentsTable.Rows(k).Item(1).ToString, attachmentsTable.Rows(k).Item(0).ToString))
        Next
    End Sub
End Class