Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Resources

Public Class konfigurasi_pangkalan_baru
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub SaveFunction_ServerClick(sender As Object, e As EventArgs) Handles SaveFunction.ServerClick, btnHantar.Click
        Dim strSQL1 As String = ""
        Try
            strSQL1 = "INSERT INTO [dbo].[pangkalan]
           ([namaPangkalan]
           ,[alamat]
           ,[bandar]
           ,[poskod]
           ,[negeri]
           ,[noTelefon]
           ,[noFaks]
           ,[emel])
     VALUES
           ('" & txtNama.Text & "'
           ,'" & txtAlamat1.Text + txtAlamat2.Text + txtAlamat3.Text & "'
           ,'" & ddlBandar.SelectedValue & "'
           ,'" & txtPoskod.Text & "'
           ,'" & ddlNegeri.SelectedValue & "'
           ,'" & txtTelefon.Text & "'
           ,'" & txtFaks.Text & "'
           ,'" & txtEmel.Text & "')"

            oCommon.ExecuteSQL(strSQL1)
        Catch ex As Exception
            strlbl_top.Text = "Penamabahan data gagal."
        End Try

        strlbl_top.Text = "Penamabahan data Berjaya."

    End Sub
End Class