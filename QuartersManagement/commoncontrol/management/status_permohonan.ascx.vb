Imports System.Data.SqlClient
Public Class status_permohonan1
    Inherits System.Web.UI.UserControl

    Dim conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        permohonanBaharu.Attributes("class") = "progress-done"
        permohonanLayak.Attributes("class") = "progress-todo"
        permohonanMenunggu.Attributes("class") = "progress-todo"
        permohonanUnitDicadang.Attributes("class") = "progress-todo"
        suratTawaran.Attributes("class") = "progress-todo"
        Load_Page()
    End Sub

    Private Sub Load_Page()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("SELECT TOP 1
              A.pengguna_id,
              A.pengguna_nama,
              A.pengguna_status_perkahwinan,
              E.keluarga_anak,
              E.keluarga_tempat_tinggal,
              A.pengguna_mula_perkhidmatan,
              B.unit_id,
              B.permohonan_status AS 'status',
              D.kuarters_nama,
              B.pemohonan_tarikh
            FROM 
              admin.spk_pengguna A
              JOIN admin.spk_permohonan B on B.pengguna_id = A.pengguna_id
              JOIN admin.spk_keluarga E ON E.pengguna_id = A.pengguna_id
              JOIN admin.spk_unit C ON C.unit_id = B.unit_id
              JOIN admin.spk_kuarters D ON D.kuarters_id = C.kuarters_id
            WHERE
              A.pengguna_id = 1
            ORDER BY
              B.pemohonan_tarikh DESC
            ;",
            conn)

            Try
                conn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader
                If reader.HasRows Then
                    If reader.Read() Then
                        lblBilAnak.Text = reader("keluarga_anak")
                        lblJenisTempatTinggal.Text = reader("keluarga_tempat_tinggal")
                        lblTarikhMulaMenetap.Text = "01/01/2019"
                        lblKuarterDipohon.Text = reader("kuarters_nama")
                        lblTarikhPermohonan.Text = reader("pemohonan_tarikh")
                    End If
                End If
            Catch ex As Exception
                Debug.WriteLine("Error: " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

End Class