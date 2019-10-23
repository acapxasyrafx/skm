Public Class index
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim user_type = CType(Session("user_type"), String)
        If user_type.Equals("Admin") Then
        ElseIf user_type.Equals("User") Then
            PnlPengurusan.Visible = False
        End If
    End Sub

End Class