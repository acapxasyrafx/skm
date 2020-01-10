Imports System.Data.SqlClient

Public Class konfigurasi_surat
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strlblMsgBottom As String = ConfigurationManager.AppSettings("lblMessage_bottom")
    Dim strlblMsgTop As String = ConfigurationManager.AppSettings("lblMessage_top")
    Dim strSucDelAlert As String = ConfigurationManager.AppSettings("deleteSuccessAlert")
    Dim strFailDelAlert As String = ConfigurationManager.AppSettings("deleteFailAlert")
    Dim strSaveSuccessAlert As String = ConfigurationManager.AppSettings("saveSuccessAlert")
    Dim strSaveFailAlert As String = ConfigurationManager.AppSettings("saveFailAlert")
    Dim strDataBindAlert As String = ConfigurationManager.AppSettings("dataBindAlert")
    Dim strRecordBindAlert As String = ConfigurationManager.AppSettings("recordBindAlert")
    Dim strSysErrorAlert As String = ConfigurationManager.AppSettings("systemErrorAlert")
    Dim strDataValAlert As String = ConfigurationManager.AppSettings("dataValidationAlert")

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim penggunaID As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            load_page()
        End If
    End Sub

    Protected Sub load_page()
        Try
            load_senarai_surat()
        Catch ex As Exception
            Debug.WriteLine("Error(load_page-konfigurasi_surat:35): " & ex.Message)
        End Try
    End Sub

    Private Function getSQL()
        Dim tempSQL = "SELECT * FROM spk_suratTawaranConfig"
        Dim whereSQL = " WHERE suratTawaranConfig_parameter IS NOT NULL"
        Dim orderSQL = " ORDER BY suratTawaranConfig_type DESC;"

        If tbCarian.Text.Length > 0 Then
            whereSQL += " AND (suratTawaranConfig_parameter LIKE @Search OR suratTawaranConfig_type LIKE @Search)"
        End If
        Return (tempSQL & whereSQL & orderSQL)
    End Function

    Protected Sub load_senarai_surat()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand(getSQL, conn)
                If tbCarian.Text.Length > 0 Then
                    cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 50).Value = "%" & tbCarian.Text & "%"
                End If
                Try
                    conn.Open()
                    Using sda As New SqlDataAdapter(cmd)
                        Dim ds As New DataSet
                        sda.Fill(ds)
                        datRespondent.DataSource = ds
                        datRespondent.DataBind()
                    End Using
                Catch ex As Exception
                    Debug.WriteLine("Error(load_senarai_surat-konfigurasi_surat:65): " & ex.Message)
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        load_senarai_surat()
    End Sub

    Protected Sub openModal()
        modalSurat.Style.Add("display", "block")
    End Sub
    Protected Sub closeModal()
        modalSurat.Style.Add("display", "none")
    End Sub

    Private Sub AddNew_ServerClick(sender As Object, e As EventArgs) Handles AddNew.ServerClick
        openModal()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        closeModal()
    End Sub

    Private Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles datRespondent.RowCommand
        Try
            Session("surat_id") = e.CommandArgument
            If e.CommandName.Equals("Ubah") Then
                If read(e.CommandArgument) Then
                    openModal()
                End If
            ElseIf e.CommandName.Equals("Padam") Then
                If destroy(e.CommandArgument) Then
                    load_senarai_surat()
                End If
            Else

            End If
        Catch ex As Exception
            Debug.WriteLine("Error(datRespondent_RowCommand-konfigurasi_surat:107): " & ex.Message)
        End Try

    End Sub

    Private Function insert()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("INSERT INTO spk_suratTawaranConfig VALUES(@KandunganSurat, @Tajuk)", conn)
                cmd.Parameters.Add("@Tajuk", SqlDbType.NVarChar).Value = tajukSurat.Text
                cmd.Parameters.Add("@KandunganSurat", SqlDbType.NVarChar).Value = Server.HtmlEncode(editorSuratContent.Content)
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Debug.WriteLine("Error(insert-konfigurasi_surat:122): " & ex.Message)
                    Return False
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Function

    Private Function read(ByVal suratID As Integer)
        If Not Session.Item("surat_id") Is Nothing Then
            Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using cmd As New SqlCommand("SELECT * FROM spk_suratTawaranConfig WHERE suratTawaranConfig_id = @SuratID;", conn)
                    cmd.Parameters.Add("@SuratID", SqlDbType.Int).Value = suratID
                    Try
                        conn.Open()
                        Dim sdr As SqlDataReader
                        sdr = cmd.ExecuteReader
                        If sdr.HasRows Then
                            While sdr.Read
                                tajukSurat.Text = sdr("suratTawaranConfig_type")
                                editorSuratContent.Content = Server.HtmlDecode(sdr("suratTawaranConfig_parameter"))
                                openModal()
                            End While
                        Else
                            Debug.WriteLine("Error(read-konfigurasi_surat:147): NO ROWS")
                            Return False
                        End If
                        Return True
                    Catch ex As Exception
                        Debug.WriteLine("Error(read-konfigurasi_surat:151): " & ex.Message)
                        Return False
                    Finally
                        conn.Close()
                    End Try
                End Using
            End Using
        Else
            Debug.WriteLine("Error(read-konfigurasi_surat:160): SESSION NOT EXIST")
            Return False
        End If
    End Function

    Private Function update()
        If Not Session.Item("surat_id") Is Nothing Then
            Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using cmd As New SqlCommand("UPDATE 
                            spk_suratTawaranConfig 
                        SET 
                            suratTawaranConfig_parameter = @KandunganSurat
                            , suratTawaranConfig_type = @TajukSurat
                        WHERE 
                            suratTawaranConfig_id = @SuratID
                    ", conn)
                    cmd.Parameters.Add("@KandunganSurat", SqlDbType.NVarChar).Value = Server.HtmlEncode(editorSuratContent.Content)
                    cmd.Parameters.Add("@TajukSurat", SqlDbType.NVarChar).Value = tajukSurat.Text
                    cmd.Parameters.Add("@SuratID", SqlDbType.Int).Value = Session("surat_id")
                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()
                        closeModal()
                        Return True
                    Catch ex As Exception
                        Debug.WriteLine("Error(update-konfigurasi_surat:185): " & ex.Message)
                        Return False
                    Finally
                        conn.Close()
                        Session("surat_id") = Nothing
                    End Try
                End Using
            End Using
        Else
            Debug.WriteLine("Error(update-konfigurasi_surat:194): SESSION NOT EXIST")
            Return False
        End If
    End Function

    Private Function destroy(ByVal suratID As Integer)
        If Not Session.Item("surat_id") Is Nothing Then
            Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using cmd As New SqlCommand("DELETE FROM spk_suratTawaranConfig WHERE suratTawaranConfig_id = @SuratID", conn)
                    cmd.Parameters.Add("@SuratID", SqlDbType.Int).Value = Session("surat_id")
                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()
                        Return True
                    Catch ex As Exception
                        Debug.WriteLine("Error(destroy-konfigurasi_surat:209): " & ex.Message)
                        Return False
                    Finally
                        conn.Close()
                        Session("surat_id") = Nothing
                    End Try
                End Using
            End Using
        Else
            Debug.WriteLine("Error(destroy-konfigurasi_surat:218): SESSION NOT EXIST")
            Return False
        End If
    End Function

    Private Sub closeSpan_ServerClick(sender As Object, e As EventArgs) Handles closeSpan.ServerClick
        closeModal()
    End Sub

    Private Sub reset_form()
        tajukSurat.Text = Nothing
        editorSuratContent.Content = "<p></p>"
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If Session.Item("surat_id") Is Nothing Then
            If insert() Then
                reset_form()
                closeModal()
            End If
        Else
            If update() Then
                reset_form()
                closeModal()
            End If
        End If
        load_senarai_surat()
    End Sub
End Class