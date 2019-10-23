Imports System.Data.SqlClient

Public Class konfigurasi_pangkat
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            setDDLPangkat()
        Catch ex As Exception

        End Try

    End Sub
    Private Sub setDDLPangkat()
        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT id,pangkat FROM dbo.pangkat"

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        Dim quantity As Integer = attachmentsTable.Rows.Count

        ddlPangkat.Items.Clear()

        ddlPangkat.Items.Add(New ListItem("-- Pilih Jenis Rumah --", 0))

        For k = 0 To quantity - 1
            ddlPangkat.Items.Add(New ListItem(attachmentsTable.Rows(k).Item(1).ToString, attachmentsTable.Rows(k).Item(0).ToString))
        Next
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        Dim strSQL1 As String = ""

        strSQL1 = "select * from namaPangkat where id = '" & ddlPangkat.SelectedValue & "' AND namaPangkat = '" & txtNamaPangkat.Text & "'"

    End Sub
End Class