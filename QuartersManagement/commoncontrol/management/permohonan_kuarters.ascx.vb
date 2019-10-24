Public Class permohonan_kuarters
    Inherits System.Web.UI.UserControl
    Dim perakuan_pemohon As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        perakuan_pemohon = "Saya dengna ini memohon sebuah Rumah Keluarga mengikut peraturan dan Undang-Undang PAT Jil III(3) dan mengaku iaitu butiran-butiran yang dinyatakan seperti berikut adalah benar."
        cbPerakuanPemohon.Text = perakuan_pemohon

    End Sub

End Class