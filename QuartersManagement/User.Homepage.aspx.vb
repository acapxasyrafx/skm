Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim user_type = CType(Session("user_type"), String)
        If user_type.Equals("User") Then

        Else
            Response.Redirect("Admin.Homepage.aspx")
        End If
    End Sub

End Class