Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Web.UI
Public Class senarai_permohonan
    Inherits System.Web.UI.UserControl

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

    Dim oCommon As New Commonfunction

    Dim penggunaID As Integer = 1
    Dim pangkalanID As Integer = 0
    Dim permohonanID As Integer = Nothing
    Dim statusPermohon As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Load_Page()
        End If
    End Sub

    Private Sub Load_Page()
        senaraiPermohonan()
    End Sub

    Private Sub senaraiPermohonan()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim ds As New DataSet
            Dim da As New SqlDataAdapter("SELECT 
	            A.permohonan_id,
	            A.pemohonan_tarikh,
	            B.kuarters_nama,
	            A.permohonan_status,
                A.permohonan_nota
            FROM 
	            spk_permohonan A
	            JOIN spk_kuarters B ON B.kuarters_id = A.kuarters_id
            WHERE A.pengguna_id = " & penggunaID & "
            ORDER BY A.pemohonan_tarikh DESC", conn)
            Try
                conn.Open()
                da.Fill(ds, "AnyTable")
                If ds.Tables(0).Rows.Count > 0 Then
                    tblSenaraiPermohonan.DataSource = ds
                    tblSenaraiPermohonan.DataBind()
                End If
            Catch ex As Exception
                Debug.WriteLine("Error(senaraiPermohonan): " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub tblSenaraiPermohonan_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles tblSenaraiPermohonan.RowCommand
        permohonanID = e.CommandArgument
        hiddenPermohonanID.Value = e.CommandArgument
        If e.CommandName = "View_Permohonan" Then
            Debug.WriteLine("ViewPermohonan")
            Response.Redirect("Maklumat.Permohonan.Pengguna.aspx?p=Maklumat%20Permohonan&permohonan=" & e.CommandArgument)
        ElseIf e.CommandName = "Delete_Permohonan" Then
            Debug.WriteLine("DeletePermohonan")
            Debug.WriteLine("CommandgArgument:" & e.CommandArgument)
            'openModal()
            dialogModal.Attributes.CssStyle.Add("display", "block")
        End If
    End Sub

    Private Sub closeBtn_ServerClick(sender As Object, e As EventArgs) Handles closeBtn.ServerClick
        closeModal()
    End Sub


    Private Sub btnTutupModal_Click(sender As Object, e As EventArgs) Handles btnTutupModal.Click
        closeModal()
    End Sub

    Private Sub btnBatalPermohonan_Click(sender As Object, e As EventArgs) Handles btnBatalPermohonan.Click
        Dim batal As Boolean
        Debug.WriteLine("permohonanID:" & hiddenPermohonanID.Value)
        Debug.WriteLine("Note:" & txtNota.Text)
        batal = batalPermohonan(hiddenPermohonanID.Value)
        If batal Then
            MsgTop.Attributes("class") = "successMsg"
            strlbl_top.Text = strSaveSuccessAlert
            MsgBottom.Attributes("class") = "successMsg"
            strlbl_bottom.Text = strSaveSuccessAlert
            closeModal()
            Load_Page()
        Else
            MsgTop.Attributes("class") = "errorMsg"
            strlbl_top.Text = strSaveFailAlert
            MsgBottom.Attributes("class") = "errorMsg"
            strlbl_bottom.Text = strSaveFailAlert
        End If
    End Sub

    Private Sub closeModal()
        If dialogModal.Style.Item("display").Equals("block") Then
            dialogModal.Attributes.CssStyle.Add("display", "none")
        End If
        permohonanID = Nothing
    End Sub

    Private Sub openModal()
        If dialogModal.Style.Item("display").Equals("none") Then
            dialogModal.Attributes.CssStyle.Add("display", "block")
        End If
    End Sub

    Private Function batalPermohonan(ByVal pID As Integer) As Boolean
        Dim strRef = ""
        Dim query = "UPDATE spk_permohonan
                    SET 
	                    permohonan_nota = '" & txtNota.Text & "',
	                    permohonan_status = 'PERMOHONAN DITOLAK'
                    WHERE permohonan_id = " & pID & ";"
        Try
            strRef = oCommon.ExecuteSQL(query)
            If strRef = "0" Then
                Return True
            Else
                Debug.WriteLine("Error(batalPermohonan): " & strRef)
                Return False
            End If
        Catch ex As Exception
            Debug.WriteLine("Error(batalPermohonan): " & ex.Message)
            Return False
        End Try
    End Function

    Protected Function showButton(ByVal pID As Integer) As Boolean
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("SELECT permohonan_status FROM spk_permohonan WHERE permohonan_id = " & pID & ";", conn)
            Try
                conn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader
                If reader.HasRows Then
                    Do While reader.Read()
                        If reader("permohonan_status").Equals("PERMOHONAN DITERIMA") Or reader("permohonan_status").Equals("PERMOHONAN DITOLAK") Then
                            Debug.WriteLine("Status(showButton): " & reader("permohonan_status"))
                            Return False
                        ElseIf reader("permohonan_status").Equals("PERMOHONAN SEDANG DIPROSES") Then
                            Debug.WriteLine("Status(showButton): " & reader("permohonan_status"))
                            Return True
                        ElseIf reader("permohonan_status").Equals("PERMOHONAN BARU") Then
                            Debug.WriteLine("Status(showButton): " & reader("permohonan_status"))
                            Return True
                        End If
                    Loop
                Else
                    Debug.WriteLine("Error(showButton): No Rows")
                    Return False
                End If
            Catch ex As Exception
                Debug.WriteLine("Error(showButton): " & ex.Message)
                Return False
            Finally
                conn.Close()
            End Try
        End Using
        Return False
    End Function

    Protected Function changeStatus(ByVal status As String) As String
        Select Case status
            Case "PERMOHONAN SEDANG DIPROSES"
                Return "SEDANG DIPROSES"
            Case "PERMOHONAN DITOLAK"
                Return "DITOLAK"
            Case "PERMOHONAN DITERIMA"
                Return "DITERIMA"
            Case Else
                Return status
        End Select
    End Function

    Protected Function changeDate(ByVal d As String) As String
        Dim formatedDate As String = Convert.ToDateTime(d).ToString("dd/MM/yyyy")
        Return formatedDate
    End Function
End Class