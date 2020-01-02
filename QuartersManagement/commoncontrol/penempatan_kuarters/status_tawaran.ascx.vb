Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Drawing
Imports System.Net.Mail
Public Class status_tawaran
    Inherits System.Web.UI.UserControl

    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction
    Dim page_view As New Integer
    Dim cmd As SqlCommand
    Dim errCount As Integer
    Dim dispID As String
    Dim stdID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                'getDetail()
                'ddlUnit()
                loadPermohonan()
                loadDDLSuratTawaran()
            Catch ex As Exception

            End Try
        End If

    End Sub

    Private Sub loadDDLSuratTawaran()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectioNString"))
            Using cmd As New SqlCommand("SELECT * FROM spk_suratTawaranConfig")
                cmd.Connection = conn
                Try
                    conn.Open()
                    ddlJenisSuratTawaran.DataSource = cmd.ExecuteReader
                    ddlJenisSuratTawaran.DataValueField = "suratTawaranConfig_parameter"
                    ddlJenisSuratTawaran.DataTextField = "suratTawaranConfig_type"
                    ddlJenisSuratTawaran.DataBind()
                    ddlJenisSuratTawaran.Items.Insert(0, New ListItem("-- SILA PILIH --", String.Empty))
                Catch ex As Exception
                    Debug.WriteLine("Error(loadDDLSuratTawaran-proses_penempatan_kuarters:48): " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Private Sub loadPermohonan()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("
                SELECT 
	                A.permohonan_id
                    ,   A.permohonan_no_permohonan
	                ,	SUBSTRING(G.log_tarikh,1,10) 'permohonan_tarikh'
	                ,	B.pengguna_nama
	                ,	B.pengguna_no_tentera
	                ,	C.pangkat_nama
	                ,	B.pengguna_jantina
                    ,   F.pangkalan_nama
	                ,	E.kuarters_nama
	                ,	CONCAT(D.unit_blok,'-',D.unit_tingkat,'-',D.unit_nombor) 'unit_nama'
                FROM 
	                spk_permohonan A 
	                JOIN spk_pengguna B On B.pengguna_id = A.pengguna_id
	                JOIN spk_pangkat C ON C.pangkat_id = B.pangkat_id
	                JOIN spk_unit D ON D.unit_id = A.unit_id
	                JOIN spk_kuarters E ON E.kuarters_id = A.kuarters_id
                    JOIN spk_pangkalan F ON F.pangkalan_id = E.pangkalan_id
	                JOIN (SELECT * FROM spk_logPermohonan WHERE log_status = 'PERMOHONAN BARU') G ON G.permohonan_id = A.permohonan_id
                WHERE 
	                A.permohonan_id = @permohonanID")
                cmd.Connection = conn
                cmd.Parameters.Add("@permohonanID", SqlDbType.Int).Value = Request.QueryString("uid")
                Try
                    Dim reader As SqlDataReader
                    conn.Open()
                    reader = cmd.ExecuteReader
                    If reader.HasRows Then
                        While reader.Read
                            lblNama.Text = reader("pengguna_nama")
                            lblNoTentera.Text = reader("pengguna_no_tentera")
                            lblPangkat.Text = reader("pangkat_nama")
                            lblKuarters.Text = reader("kuarters_nama")
                            lblUnit.Text = reader("unit_nama")
                            lblNoPermohonan.Text = reader("permohonan_no_permohonan")
                            lblPangkalan.Text = reader("pangkalan_nama")
                        End While
                    Else
                        Debug.WriteLine("Error(loadPermohonan-proses_penempatan_kuarters:95): No Rows")
                    End If
                Catch ex As Exception
                    Debug.WriteLine("Error(loadPermohonan-proses_penempatan_kuarters:98): " & ex.Message)
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Dim uid As String = Request.QueryString("uid")
        Dim content = Server.HtmlEncode(editorSurattawaran.Content)
        Try
            If validate() Then
            strSQL = "INSERT INTO 
                        spk_suratTawaran(suratTawaran_content,permohonan_id,suratTawaran_tarikh_dibuat) 
                     VALUES 
                        (
                            '" & content & "'
                           ,'" & uid & "'
                            ,'" & Date.Now() & "'
                        )"
            strRet = oCommon.ExecuteSQL(strSQL)

                If strRet = "0" Then
                    strSQL = "UPDATE 
                            spk_permohonan
                        SET 
                            permohonan_tarikh = '" & Date.Now().ToString("dd/MM/yyyy") & "'
                        ,   permohonan_sub_status = 'TERIMA SURAT TAWARAN'
                        WHERE
                            permohonan_id = " & uid & ";"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If
                Response.Redirect("Proses.Penempatan.Kuarters.aspx?P=Pengurusan%20Pentadbiran%20>%20Kuarters%20>%20Proses%20Penempatan%20Kuarters")
            End If
        Catch ex As Exception
            Debug.WriteLine("Error(btnSimpan-proses_penempatan_kuarters:134): " & ex.Message)
        End Try
    End Sub

    Private Function validate() As Boolean
        If datepicker.Text.Length > 0 Then
            If ddlJenisSuratTawaran.SelectedIndex > 0 Then
                If editorSurattawaran.Content.Equals("") Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Private Sub ddlJenisSuratTawaran_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenisSuratTawaran.SelectedIndexChanged
        Dim content As String

        Try

            If datepicker.Text.Count > 0 Then
                content = Server.HtmlDecode(ddlJenisSuratTawaran.SelectedValue)
                content = content.Replace("{NAME}", lblNama.Text)
                content = content.Replace("{NoPermohonan}", lblNoPermohonan.Text)
                content = content.Replace("{ID}", lblNoTentera.Text)
                content = content.Replace("{KUARTERS}", lblKuarters.Text)
                content = content.Replace("{UNIT}", lblUnit.Text)
                content = content.Replace("{PANGKALAN}", lblPangkalan.Text)
                content = content.Replace("{TARIKH}", datepicker.Text)
                editorSurattawaran.Content = content
            End If
        Catch ex As Exception
            Debug.WriteLine("Error(ddlJenisSuratTawaran-status_tawaran:171): " & ex.Message)
        End Try

    End Sub
End Class