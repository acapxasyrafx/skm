Public Class permohonan_kuarters
    Inherits System.Web.UI.UserControl
    Dim perakuan_pemohon As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblNoTentera.Text = "ABCD1234"
        lblJawatan.Text = "Label Jawatan"
        lblTarikhMulaBerkhidmat.Text = "01/01/1990"
        lblTarikhAkhirBerkhidmat.Text = "01/01/1991"
        cbPerakuanPemohon.Text = "Saya dengan ini memohon sebuah Rumah Keluarga mengikut peraturan dan Undang-Undang PAT Jil III(3) dan mengaku iaitu butiran-butiran yang dinyatakan seperti berikut adalah benar."

    End Sub

End Class