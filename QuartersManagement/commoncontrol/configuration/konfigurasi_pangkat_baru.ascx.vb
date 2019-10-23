Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Resources
Public Class konfigurasi_pangkat_baru
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub SaveFunction_ServerClick(sender As Object, e As EventArgs) Handles SaveFunction.ServerClick, btnHantar.Click
        Dim strSQL1 As String = ""
        Try
            strSQL1 = "INSERT INTO [dbo].[pangkat]
           ([namaPangkat]
           ,[point])
     VALUES
           ('" & ddlPangkat.SelectedValue & "','" & txtNamaPangkat.Text & "')"

            oCommon.ExecuteSQL(strSQL1)
        Catch ex As Exception
            strlbl_top.Text = "Penamabahan data gagal."
        End Try

        strlbl_top.Text = "Penamabahan data Berjaya."

    End Sub
End Class