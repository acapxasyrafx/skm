Imports System.Data.SqlClient
Public Class status_permohonan1
    Inherits System.Web.UI.UserControl

    Dim conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblAlamatDiminta.Text = "Level 18.05, Wisma Pertahanan, Jalan Padang Tembak, 50634, KUALA LUMPUR."
        permohonanDiterima.Attributes("class") = "progress-done"
        permohonanDiproses.Attributes("class") = "progress-todo"
        permohonanKeputusan.Attributes("class") = "progress-todo"
    End Sub

    Private Sub Load_Page()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("SELECT 
	            A.pengguna_id,
	            A.pengguna_nama,
	            A.pengguna_mykad,
	            A.pengguna_jantina,
	            A.pengguna_tarikh_lahir,
	            A.pengguna_kewarganegaraan,
                A.pengguna_mula_perkhidmatan,
                A.pengguna_tamat_perkhidmatan,
                A.pengguna_no_tentera,
	            B.pangkat_id,
	            B.pangkat_nama
            FROM 
	            admin.spk_pengguna A
	            JOIN admin.spk_pangkat B ON A.pangkat_id = B.pangkat_id
	            JOIN dbo.spk_pangkalan C ON A.pangkalan_id = C.pangkalan_id
            WHERE pengguna_id = 1",
            conn)

            Try
                conn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader
                If reader.HasRows Then
                    If reader.Read() Then

                    End If
                End If
            Catch ex As Exception
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

End Class