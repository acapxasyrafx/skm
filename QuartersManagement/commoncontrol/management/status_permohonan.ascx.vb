Imports System.Data.SqlClient
Public Class status_permohonan1
    Inherits System.Web.UI.UserControl

    Dim penggunaID As Integer = 1
    Dim pangkalanID As Integer = 0
    Dim permohonanID As Integer = 14
    Dim statusPermohon As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        permohonanBaharu.Attributes("class") = "progress-done"
        permohonanLayak.Attributes("class") = "progress-todo"
        permohonanMenunggu.Attributes("class") = "progress-todo"
        permohonanUnitDicadang.Attributes("class") = "progress-todo"
        suratTawaran.Attributes("class") = "progress-todo"
        Load_Page()
    End Sub

    Private Sub Load_Page()
        maklumatUser()
        maklumatAnak()
        statusPermohon = "LULUS TANPA KEKOSONGAN"
        If statusPermohon.Equals("PERMOHONAN BARU") Then
            mvStatusPermohonan.ActiveViewIndex = 0
        ElseIf statusPermohon.Equals("LULUS TANPA KEKOSONGAN") Then
            maklumatCadanganKuarters()
            mvStatusPermohonan.ActiveViewIndex = 1
        End If
    End Sub

    Private Sub maklumatUser()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("SELECT TOP 1
                A.pengguna_id,
                A.pengguna_nama,
                A.pengguna_status_perkahwinan,
                E.keluarga_anak,
                E.keluarga_tempat_tinggal,
                E.keluarga_tarikh_mula,
                A.pengguna_mula_perkhidmatan,
                B.unit_id,
                B.permohonan_status,
                D.kuarters_nama,
	            F.pangkalan_id,
	            F.pangkalan_nama,
                B.pemohonan_tarikh
            FROM 
                admin.spk_pengguna A
                JOIN admin.spk_permohonan B on B.pengguna_id = A.pengguna_id
                JOIN admin.spk_keluarga E ON E.pengguna_id = A.pengguna_id
                JOIN admin.spk_unit C ON C.unit_id = B.unit_id
                JOIN admin.spk_kuarters D ON D.kuarters_id = C.kuarters_id
	            JOIN dbo.spk_pangkalan F ON F.pangkalan_id = C.pangkalan_id
            WHERE
                A.pengguna_id = 1
            ORDER BY
                B.pemohonan_tarikh DESC;",
            conn)

            Try
                conn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader
                If reader.HasRows Then
                    Do While reader.Read = True
                        lblJenisTempatTinggal.Text = reader("keluarga_tempat_tinggal")
                        lblTarikhMulaMenetap.Text = reader("keluarga_tarikh_mula").ToString()
                        lblKuarterDipohon.Text = reader("kuarters_nama")
                        lblTarikhPermohonan.Text = reader("pemohonan_tarikh")
                        statusPermohon = reader("permohonan_status")
                        Debug.WriteLine("Success: maklumatUser")
                    Loop
                Else
                    Debug.WriteLine("Error(maklumatUser): No Rows")
                End If
                reader.Close()
            Catch ex As Exception
                Debug.WriteLine("Error(maklumatUser): " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Private Sub maklumatAnak()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim ds As New DataSet
            Dim da As New SqlDataAdapter("SELECT * FROM spk_anak WHERE pengguna_id = " & penggunaID & "", conn)
            Try
                conn.Open()
                da.Fill(ds, "AnyTable")
                If ds.Tables(0).Rows.Count > 0 Then
                    lblBilAnak.Text = ds.Tables(0).Rows.Count
                    tblMaklumatAnak.DataSource = ds
                    tblMaklumatAnak.DataBind()
                    Debug.WriteLine("Success: maklumatAnak")
                End If
            Catch ex As Exception
                Debug.WriteLine("Error(maklumatAnak):" & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Private Sub maklumatCadanganKuarters()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim ds As New DataSet
            Dim da As New SqlDataAdapter("SELECT 
	            A.cadanganKuarters_id,
	            A.permohonan_id,
	            A.pengguna_id,
	            A.kuarters_dicadang,
	            C.pangkalan_nama,
	            B.kuarters_nama,
	            B.kuarters_alamat + ',' + B.kuarters_poskod + ',' + B.kuarters_negeri AS 'kuarters_alamat'
            FROM 
	            admin.spk_cadanganKuarters A
	            JOIN admin.spk_kuarters B ON B.kuarters_id = A.kuarters_dicadang
	            JOIN dbo.spk_pangkalan C ON C.pangkalan_id = B.pangkalan_id
            WHERE
	            A.permohonan_id = " & permohonanID & ";",
            conn)
            Try
                conn.Open()
                da.Fill(ds, "AnyTable")
                If ds.Tables(0).Rows.Count > 0 Then
                    tblCadanganKuarters.DataSource = ds
                    tblCadanganKuarters.DataBind()
                    Debug.WriteLine("Success: maklumatCadanganKuarters")
                End If
            Catch ex As Exception
                Debug.WriteLine("Error(maklumatCadangantKuartes): " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub
End Class