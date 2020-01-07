Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Web.UI.HtmlControls
Public Class maklumat_penempatan_pemohon1
    Inherits System.Web.UI.UserControl

    Dim userID As Integer
    Dim userType As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            load_page()
        End If
    End Sub

    Protected Sub load_page()
        check_session()
        maklumat_pemohon()
        maklumat_anak()
    End Sub

    Protected Sub check_session()
        If Not Session("user_id").Equals("") Then
            userID = Session("user_id")
        Else
            Response.Redirect("default.aspx")
        End If
    End Sub

    Protected Sub maklumat_pemohon()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("SELECT 
	            A.permohonan_id
	            , B.pengguna_id
	            , B.pengguna_nama
	            , B.pengguna_jantina
	            , B.pengguna_tarikh_lahir
	            , C.historyPengguna_penggunaNoTentera
	            , C.historyPengguna_statusPerkahwinan
	            , D.pangkat_nama
	            , B.pengguna_mula_perkhidmatan
	            , B.pengguna_tamat_perkhidmatan
	            , A.permohonan_no_permohonan
	            , A.kuarters_id
	            , E.kuarters_nama
	            , F.pangkalan_nama
	            , A.permohonan_tarikh
	            , H.historyKeluarga_tempat_tinggal
	            , H.historyKeluarga_tarikh_mula
	            , H.historyKeluarga_tarikh_akhir
	            , I.suratTawaran_content
	            , I.suratTawaran_tarikh_dibuat
	            , G.unit_nama
	            , (G.unit_blok + '-' + G.unit_tingkat + '-' + G.unit_nombor) AS 'unit_nama_lain'
	            , A.permohonan_tarikh_kemasukan
            FROM 
	            spk_permohonan A 
	            JOIN spk_pengguna B ON B.pengguna_id = A.pengguna_id
	            JOIN spk_historyPengguna C ON C.pengguna_id = B.pengguna_id
	            JOIN spk_pangkat D ON D.pangkat_id = C.pangkat_id
	            JOIN spk_kuarters E ON E.kuarters_id = A.kuarters_id
	            JOIN spk_pangkalan F ON F.pangkalan_id = E.pangkalan_id
	            JOIN spk_unit G ON G.unit_id = A.unit_id
	            JOIN spk_historyKeluarga H ON H.permohonan_id = A.permohonan_id 
	            JOIN spk_suratTawaran I ON I.permohonan_id = A.permohonan_id
            WHERE A.permohonan_id =  @permohonanID;")
                cmd.Connection = conn
                cmd.Parameters.Add("@permohonanID", SqlDbType.Int).Value = Request.QueryString("uid")
                Try
                    conn.Open()
                    Using sdr As SqlDataReader = cmd.ExecuteReader
                        If sdr.HasRows Then
                            While sdr.Read
                                lblNama.Text = sdr("pengguna_nama")
                                lblPangkat.Text = sdr("pangkat_nama")
                                lblNoTentera.Text = sdr("historyPengguna_penggunaNoTentera")
                                lblTarikhLahir.Text = sdr("pengguna_tarikh_lahir")
                                lblStatusPerkahwinan.Text = sdr("historyPengguna_statusPerkahwinan")
                                lblNamaUnit.Text = IIf(sdr("unit_nama").ToString.Equals(""), sdr("unit_nama_lain").ToString, sdr("unit_nama").ToString)
                                lblNamaKuarters.Text = sdr("kuarters_nama")
                                lblNamaPangkalan.Text = sdr("pangkalan_nama")
                                lblTempatTinggalAkhir.Text = sdr("historyKeluarga_tempat_tinggal")
                                lblTarikhMulaMenetap.Text = Convert.ToDateTime(sdr("historyKeluarga_tarikh_mula")).ToString("dd/MM/yyyy")
                                divSuratTawaran.InnerHtml = Server.HtmlDecode(sdr("suratTawaran_content"))
                                lblTarikhSuratTawaran.Text = sdr("suratTawaran_tarikh_dibuat")
                            End While
                        Else
                            Debug.WriteLine("Error(maklumat_penempatan_pemohon: 37): NO ROWS")
                        End If
                    End Using
                    Debug.WriteLine("Maklumat_pemohon: Success")
                Catch ex As Exception
                    Debug.WriteLine("Error(maklumat_pemohon-maklumat_penempatan_pemohon:91): " & ex.Message)
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Sub

    Protected Sub maklumat_anak()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("SELECT * FROM spk_historyAnak WHERE permohonan_id = @permohonanID;")
                cmd.Connection = conn
                cmd.Parameters.Add("@permohonanID", SqlDbType.Int).Value = Request.QueryString("uid")
                Dim ds As New DataSet
                Try
                    conn.Open()
                    Using sda As New SqlDataAdapter(cmd)
                        sda.Fill(ds)
                        tblMaklumatAnak.DataSource = ds
                        tblMaklumatAnak.DataBind()
                    End Using
                Catch ex As Exception
                    conn.Close()
                    Debug.WriteLine("Maklumat_anak: Success")
                End Try
            End Using
        End Using
    End Sub

End Class