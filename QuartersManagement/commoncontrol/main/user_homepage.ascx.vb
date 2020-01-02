Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Resources
Public Class user_homepage
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
    End Sub

    Protected Sub load_user()
        Dim userID = Session("user_id")
        Dim userType = Session("user_type")
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
                    Debug.WriteLine("Error(load_user-user_homepage:38): " & ex.Message)
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Sub
End Class