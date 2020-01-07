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
	                * 
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
                WHERE A.permohonan_id = @permohonanID;")
                cmd.Connection = conn
                cmd.Parameters.Add("@permohonanID", SqlDbType.Int).Value = Request.QueryString("uid")
                Try
                    conn.Open()
                    Using sdr As SqlDataReader = cmd.ExecuteReader
                        If sdr.HasRows Then
                            While sdr.Read

                            End While
                        Else
                            Debug.WriteLine("Error(maklumat_penempatan_pemohon: 37): NO ROWS")
                        End If
                    End Using
                Catch ex As Exception
                    conn.Close()
                    Debug.WriteLine("Maklumat_pemohon: Success")
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

                    End Using
                Catch ex As Exception
                    conn.Close()
                    Debug.WriteLine("Maklumat_anak: Success")
                End Try
            End Using
        End Using
    End Sub

End Class