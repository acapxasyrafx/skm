Imports System.Data.SqlClient
Public Class maklumat_pemohon
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
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
            data_load()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub data_poinLoad()
        Dim strSQL2 As String = ""
        Dim strSQL3 As String = ""
        Dim dataJumlah As Integer = ""
        Dim dataPangkatPoin As Integer = ""
        Dim dataUmurAnak As Integer = ""
        Dim jumlah_anak As Integer = ""

        strSQL = "select count(anak_nama) from spk_anak where anak_umur <= 18"
        strSQL2 = "select B.pangkat_mata from spk_pengguna A left join spk_pangkat B on A.pangkat_id = B.pangkat_id"

        Dim jumlah_anakUmur18 = oCommon.ExecuteSQL(strSQL)
        Dim jumlah_poinPangkat = oCommon.ExecuteSQL(strSQL2)


        Dim jumlah_mataTerkumpul = jumlah_poinPangkat + (jumlah_anakUmur18 * 5)

        lblpoinDisplay.Text = jumlah_mataTerkumpul.ToString


    End Sub

    Private Sub data_load()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("SELECT TOP 1
	            A.pengguna_id , 
	            A.permohonan_mata as jumlahPoin,
	            C.pangkat_nama as pangkat_nama,
	            D.pangkalan_nama as pangkalan_nama,
	            B.pengguna_id,
	            B.pengguna_nama as pengguna_nama,
	            B.pengguna_mykad as pengguna_mykad,
	            B.pengguna_jantina as pengguna_jantina,
	            B.pengguna_tarikh_lahir as pengguna_tarikh_lahir,
	            B.pengguna_kewarganegaraan as pengguna_kewarganegaraan,
                B.pengguna_mula_perkhidmatan as pengguna_mula_perkhidmatan,
                B.pengguna_tamat_perkhidmatan as pengguna_tamat_perkhidmatan,
                B.pengguna_no_tentera as pengguna_no_tentera,
	            E.keluarga_anak as keluarga_anak,
	            E.keluarga_tempat_tinggal as keluarga_tempat_tinggal,
	            E.keluarga_tarikh_mula as keluarga_tarikh_mula,
                G.kuarters_nama as kuarters_nama

            FROM

	            spk_permohonan A
                LEFT JOIN spk_pengguna B ON A.pengguna_id = B.pengguna_id
                LEFT JOIN spk_pangkat C ON  B.pangkat_id = C.pangkat_id
                LEFT JOIN spk_pangkalan D on B.pangkalan_id = D.pangkalan_id
                LEFT JOIN spk_keluarga E on B.pengguna_id = E.pengguna_id
                LEFT JOIN spk_anak F on B.pengguna_id = F.pengguna_id
                LEFT JOIN spk_kuarters G on A.kuarters_id = G.kuarters_id

            WHERE A.permohonan_id = '" & Request.QueryString("uid") & "'",
            conn)
            'D.keluarga_tarikh_akhir,
            'D.keluarga_tarikh_sewa_mula,
            'D.keluarga_tarikh_sewa_akhir,
            'D.keluarga_tarikh_wisma_mula,
            'D.keluarga_tarikh_wisma_akhir,
            'D.keluarga_tarikh_seberang_mula,
            'D.keluarga_tarikh_seberang_akhir

            Try
                conn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                If reader.HasRows Then
                    If reader.Read() Then

                        lblSenaraiRumah.Text = reader("kuarters_nama")
                        lblNama.Text = reader("pengguna_nama")
                        lblLahirTahun.Text = reader("pengguna_tarikh_lahir")
                        lblJantina.Text = reader("pengguna_jantina")
                        lblKewarganegaraan.Text = reader("pengguna_kewarganegaraan")
                        lblJawatan.Text = reader("pangkat_nama")
                        lblNoTentera.Text = reader("pengguna_no_tentera")
                        lblTarikhMulaBerkhidmat.Text = reader("pengguna_mula_perkhidmatan")
                        lblBilAnak.Text = reader("keluarga_anak")
                        lblSewaMulaHari.Text = reader("keluarga_tarikh_sewa_mula")
                        lblWismaMulaHari.Text = reader("keluarga_tarikh_wisma_mula")
                        lblSeberangMulaHari.Text = reader("keluarga_tarikh_seberang_mula")
                        lblpoinDisplay.Text = reader("jumlahPoin")

                        'lblSenaraiRumah.Text = reader("pengguna_mula_perkhidmatan")
                        'lblDariPasukan.Text = reader("pengguna_mula_perkhidmatan")
                        'lblKePasukan.Text = reader("pengguna_mula_perkhidmatan")
                        'lblTarikhMulaHari.Text = reader("pengguna_mula_perkhidmatan")
                        '''
                        '-------------------
                        If IsDBNull(reader("pengguna_tamat_perkhidmatan")) Then
                            lblTarikhAkhirBerkhidmat.Text = "Masih Berkhidmat"
                        Else
                            lblTarikhAkhirBerkhidmat.Text = reader("pengguna_tamat_perkhidmatan")
                        End If

                        If IsDBNull(reader("keluarga_tarikh_akhir")) Then
                            lblSewaAkhirHari.Text = "Masih Menetap"
                        Else
                            lblSewaAkhirHari.Text = reader("keluarga_tarikh_akhir")
                        End If

                        If IsDBNull(reader("keluarga_tarikh_wisma_akhir")) Then
                            lblWismaAkhir.Text = "Masih Menetap"
                        Else
                            lblTarikhAkhirBerkhidmat.Text = reader("keluarga_tarikh_wisma_akhir")
                        End If

                        If IsDBNull(reader("keluarga_tarikh_seberang_akhir")) Then
                            lblSeberangAkhirTahun.Text = "Masih Berkhidmat"
                        Else
                            lblSeberangAkhirTahun.Text = reader("keluarga_tarikh_seberang_akhir")
                        End If
                        '-------------------
                    Else
                        Debug.Write("CANNOT READ")
                    End If
                Else
                    Debug.Write("NO ROWS")
                End If
            Catch ex As Exception
                Debug.Write("ERROR: " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using

    End Sub
    Protected Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Try

            If (e.CommandName = "Approve") Then
                Dim strCID = e.CommandArgument.ToString

                strSQL = "UPDATE spk_permohonan SET permohonan_status = 'Diluluskan' WHERE permohonan_id = '" & oCommon.FixSingleQuotes(strCID) & "'"
                oCommon.ExecuteSQL(strSQL)
            ElseIf (e.CommandName = "Reject") Then
                Dim strCID = e.CommandArgument.ToString

                'chk session to prevent postback
                strSQL = "UPDATE spk_permohonan SET permohonan_status = 'PERMOHONAN ANDA DITOLAK' WHERE permohonan_id = '" & oCommon.FixSingleQuotes(strCID) & "'"
                oCommon.ExecuteSQL(strSQL)

            End If

        Catch ex As Exception
            MsgBottom.Attributes("class") = "errorMsg"
            strlbl_bottom.Text = strSysErrorAlert & "<br>" & ex.Message
        End Try
    End Sub
    Private Sub poin_load()

    End Sub
End Class