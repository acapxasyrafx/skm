Public Class index
    Inherits System.Web.UI.UserControl
    Dim user_type As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        user_type = CType(Session("user_type"), String)
        If user_type.Equals("Admin") Then
        ElseIf user_type.Equals("User") Then
            PnlPengurusan.Visible = False
        Else
            Response.Redirect("default.aspx")
        End If
    End Sub

End Class