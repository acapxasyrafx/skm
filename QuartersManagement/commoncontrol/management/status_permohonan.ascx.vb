Public Class status_permohonan1
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblAlamatDiminta.Text = "Level 18.05, Wisma Pertahanan, Jalan Padang Tembak, 50634, KUALA LUMPUR."
        permohonanDiterima.Attributes("class") = "progress-done"
        permohonanDiproses.Attributes("class") = "progress-todo"
        permohonanKeputusan.Attributes("class") = "progress-todo"
    End Sub

End Class