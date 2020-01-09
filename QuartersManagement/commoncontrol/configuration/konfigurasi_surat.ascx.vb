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
            If Session("user_id") IsNot Nothing Then
                penggunaID = Session("user_id")
            End If
            load_page()
        End If
    End Sub

    Private Sub load_page()
        BindData(datRespondent)
    End Sub

    Private Function getSQL() As String
        Dim tempSQL = "SELECT * from spk_suratTawaranConfig"
        Dim whereSQL = " WHERE suratTawaranConfig_parameter IS NOT NULL"
        Dim orderSQL = " ORDER BY suratTawaranConfig_type ASC;"

        If tbCarian.Text.Length > 0 Then
            Debug.WriteLine(tbCarian.Text)
            whereSQL += " AND suratTawaranConfig_parameter LIKE '%" & tbCarian.Text & "%'"
            whereSQL += " OR suratTawaranConfig_type LIKE '%" & tbCarian.Text & "%'"
        End If

        Dim query = tempSQL & whereSQL & orderSQL
        Return query
    End Function

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then

                MsgBottom.Attributes("class") = "errorMsg"
                strlbl_bottom.Text = strDataBindAlert
            Else

                MsgBottom.Attributes("class") = "successMsg"
                strlbl_bottom.Text = strRecordBindAlert & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception

            MsgBottom.Attributes("class") = "errorMsg"
            strlbl_bottom.Text = strSysErrorAlert & "<br>" & ex.Message
            Return False
        End Try
        Return True
    End Function

    Protected Function load_surat_content(ByVal suratID)
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("SELECT * from spk_suratTawaranConfig WHERE suratTawaranConfig_id = @SuratID", conn)
                cmd.Parameters.Add("@SuratID", SqlDbType.Int).Value = suratID
                Try
                    conn.Open()
                    Dim sdr As SqlDataReader
                    sdr = cmd.ExecuteReader
                    If sdr.HasRows Then
                        While sdr.Read
                            tbTajukSurat.Text = sdr("suratTawaranConfig_type")
                            editorContentSurat.InnerHtml = sdr("suratTawaranConfig_parameter")
                        End While
                        Return True
                    Else
                        Return False
                    End If
                Catch ex As Exception
                    Debug.WriteLine("Error(load_surat_content-konfigurasi_surat:86): " & ex.Message)
                    Return False
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Function

    Protected Function delete_surat(ByVal suratID)
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("DELETE FROM spk_suratTawaranConfig WHERE suratTawaranConfig_id = @SuratID", conn)
                cmd.Parameters.Add("@SuratID", SqlDbType.Int).Value = suratID
                Try
                    cmd.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Debug.WriteLine("Error(delete_surat-konfigurasi_surat:103): " & ex.Message)
                    Return False
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Function

    Private Sub openModal()
        If suratForm.Style.Item("display").Equals("none") Then
            suratForm.Attributes.CssStyle.Add("display", "block")
        End If
    End Sub

    Private Sub closeModal()
        If suratForm.Style.Item("display").Equals("block") Then
            suratForm.Attributes.CssStyle.Add("display", "none")
        End If
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        BindData(datRespondent)
    End Sub

    Private Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles datRespondent.RowCommand
        Debug.WriteLine("RowCommand")
    End Sub

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Debug.WriteLine("Editing")
    End Sub
End Class