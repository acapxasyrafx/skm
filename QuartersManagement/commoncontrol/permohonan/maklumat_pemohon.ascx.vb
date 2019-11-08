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
	            B.pangkat_nama,
                
				D.keluarga_anak,
				D.keluarga_tempat_tinggal,
				D.keluarga_tarikh_mula
                (((SELECT COUNT(anak_nama) FROM spk_anak where anak_umur < 18) * 5) + pangkat_mata ) as jumlahPoin

            FROM 
	            admin.spk_pengguna A
	            JOIN admin.spk_pangkat B ON A.pangkat_id = B.pangkat_id
	            JOIN dbo.spk_pangkalan C ON A.pangkalan_id = C.pangkalan_id
				JOIN admin.spk_keluarga D on A.pengguna_id = D.pengguna_id
                left join spk_anak E on A.pengguna_id = E.pengguna_id

            WHERE A.pengguna_id = '" & Request.QueryString("uid") & "'",
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
                        lblNama.Text = reader("pengguna_nama")
                        lblLahirTahun.Text = reader("pengguna_tarikh_lahir")
                        lblJantina.Text = reader("pengguna_jantina")
                        lblKewarganegaraan.Text = reader("pengguna_kewarganegaraan")
                        lblJawatan.Text = reader("pangkat_nama")
                        lblNoTentera.Text = reader("pengguna_no_tentera")
                        lblTarikhMulaBerkhidmat.Text = reader("pengguna_mula_perkhidmatan")
                        ''
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