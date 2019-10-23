Public Class login_page
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If txtLoginID IsNot "" And txtPwd.Text IsNot "" Then
            If txtLoginID.Text.Equals("admin") And txtPwd.Text.Equals("admin") Then
                Session("user_type") = "Admin"
                Response.Redirect("Admin.Homepage.aspx")
            ElseIf txtLoginID.Text.Equals("user") And txtPwd.Text.Equals("user") Then
                Session("user_type") = "User"
                Response.Redirect("User.Homepage.aspx")
            End If
        End If
    End Sub
End Class