Public Class navigation_menu
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblNavigationMenu.Text = Request.QueryString("p")
            End If

        Catch ex As Exception

        End Try
    End Sub

End Class