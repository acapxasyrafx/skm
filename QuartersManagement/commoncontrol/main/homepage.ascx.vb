Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Resources

Public Class homepage
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("user_id") IsNot Nothing Then
                load_page()
            Else
                Response.Redirect("/")
            End If
        End If
    End Sub

    Protected Sub load_page()
        load_user()
        load_status_count()
    End Sub

    Protected Sub load_user()
        Dim userID = Session("user_id")
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("SELECT * FROM spk_pengguna WHERE pengguna_id = @penggunaID")
                cmd.Connection = conn
                cmd.Parameters.Add("@penggunaID", SqlDbType.Int).Value = userID
                Try
                    conn.Open()
                    Dim sdr As SqlDataReader
                    sdr = cmd.ExecuteReader
                    If sdr.HasRows Then
                        While sdr.Read
                            lblWelcome.Text = String.Format("Selamat Datang, {0}", sdr("pengguna_nama"))
                        End While
                    End If
                Catch ex As Exception
                    Debug.WriteLine("Error(load_user): " & ex.Message)
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Sub

    Protected Sub load_status_count()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("SELECT permohonan_status, count(*) as 'count' FROM spk_permohonan GROUP BY permohonan_status;")
                cmd.Connection = conn
                Try
                    conn.Open()
                    Dim sdr As SqlDataReader
                    sdr = cmd.ExecuteReader()
                    If sdr.HasRows() Then
                        While (sdr.Read)
                            Select Case sdr("permohonan_status")
                                Case "PERMOHONAN BARU"
                                    lblBaruText.InnerText = sdr("permohonan_status")
                                    lblBaruCount.Text = sdr("count")
                                Case "PERMOHONAN MENUNGGU"
                                    lblMenunggguText.InnerText = sdr("permohonan_status")
                                    lblMenungguCount.Text = sdr("count")
                                Case "PERMOHONAN DITERIMA"
                                    lblTerimaText.InnerText = sdr("permohonan_status")
                                    lblTerimaCount.Text = sdr("count")
                                Case "PERMOHONAN DITOLAK"
                                    lblTolakText.InnerText = sdr("permohonan_status")
                                    lblTolakCount.Text = sdr("count")
                            End Select
                        End While
                    End If
                Catch ex As Exception
                    Debug.WriteLine("Error(load_status_count) : " & ex.Message)
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Sub
End Class