Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Resources
Public Class login_page
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim nMark As Integer

    Private rm As ResourceManager
    Dim ci As CultureInfo

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim adminID As Integer = 1
        Dim userID As Integer = 1
        If txtLoginID IsNot "" And txtPwd.Text IsNot "" Then
            If UserLogin().Equals("ADMIN") Then
                Response.Redirect("Admin.Homepage.aspx")
            ElseIf UserLogin().Equals("PENGGUNA") Then
                Response.Redirect("User.Homepage.aspx")
            ElseIf UserLogin().Equals("1") Then
                Debug.WriteLine("User not exist")
                alertMsg.InnerText = "Akaun tiada dalam sistem. Sila pastikan ID dan KATA LALUAN adalah betul."
            Else
                Debug.WriteLine("Error(btnLogin-login_page:33)")
            End If
        End If
    End Sub

    Private Function UserLogin() As String
        Dim da As SqlDataAdapter
        Dim dt As New DataTable
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("SELECT TOP(1) pengguna_id, pengguna_jenis, pangkat_id FROM spk_pengguna WHERE pengguna_no_tentera = @noTentera AND pengguna_pwd = @pwd")
                cmd.Connection = conn
                cmd.Parameters.Add("@noTentera", SqlDbType.NVarChar, 50).Value = txtLoginID.Text
                cmd.Parameters.Add("@pwd", SqlDbType.NVarChar, 50).Value = txtPwd.Text
                Try
                    conn.Open()
                    da = New SqlDataAdapter(cmd)
                    da.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        Session("user_type") = dt.Rows(0)("pengguna_jenis")
                        Session("user_id") = dt.Rows(0)("pengguna_id")
                        Session("pangkat_id") = dt.Rows(0)("pangkat_id")
                        Return dt.Rows(0)("pengguna_jenis")
                    Else
                        Debug.WriteLine("Error(UserLogin-login_page:56): Pengguna tiada dalam sistem.")
                        Debug.WriteLine("txtLoginID: " & txtLoginID.Text.ToUpper)
                        Debug.WriteLine("txtPwd: " & txtPwd.Text)
                        Return "1"
                    End If
                Catch ex As Exception
                    Debug.WriteLine("txtLoginID: " & txtLoginID.Text.ToUpper)
                    Debug.WriteLine("txtPwd: " & txtPwd.Text)
                    Debug.WriteLine("Error(UserLogin-login_page:64): " & ex.Message)
                    Return "0"
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Function
End Class