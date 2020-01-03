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
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim penggunaID As Integer = 1
    Dim pangkalanID As Integer = 0
    Dim permohonanID As Integer = Nothing
    Dim statusPermohon As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("user_id") IsNot Nothing Then
                penggunaID = Session("user_id")
            End If
            Load_Page()
        End If
    End Sub

    Private Sub Load_Page()
        loadPangkalan()
        loadStatusPermohonan()
        BindData(tblSenaraiPermohonan)
    End Sub

    Private Function getSQL() As String
        Dim tempSQL As String = "
        SELECT 
            * 
        FROM spk_permohonan A
        LEFT JOIN spk_kuarters B ON B.kuarters_id = A.kuarters_id
        LEFT JOIN spk_pangkalan C ON C.pangkalan_id = B.pangkalan_id
        "
        Dim whereSQL As String = " WHERE A.pengguna_id = " & penggunaID & ""
        Dim orderSQL As String = " ORDER BY SUBSTRING(A.permohonan_tarikh,4,2) DESC, A.permohonan_tarikh DESC"

        If ddlCarianPangkalan.SelectedIndex > 0 Then
            whereSQL = whereSQL & " AND B.pangkalan_id = " & ddlCarianPangkalan.SelectedValue & ""
        End If

        If ddlCarianKuarters.SelectedIndex > 0 Then
            whereSQL = whereSQL & " AND B.kuarters_id = " & ddlCarianKuarters.SelectedValue & ""
        End If

        If ddlCarianStatus.SelectedIndex > 0 Then
            whereSQL = whereSQL & " AND A.permohonan_status = '" & ddlCarianStatus.SelectedValue & "'"
        End If

        getSQL = tempSQL + whereSQL + orderSQL
        Return getSQL
    End Function

    Private Function GetData(ByVal cmd As SqlCommand) As DataTable
        Dim dt As New DataTable()
        Dim strConnString As [String] = ConfigurationManager.AppSettings("ConnectionString")
        Dim con As New SqlConnection(strConnString)
        Dim sda As New SqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        Try
            con.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
            sda.Dispose()
            con.Dispose()
        End Try
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
        If canCancel(hiddenPermohonanID.Value) Then
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
        Else
            closeModal()
            MsgTop.Attributes("class") = "errorMsg"
            strlbl_top.Text = "Hanya Permohonan Baru boleh dibatalkan."
            MsgBottom.Attributes("class") = "errorMsg"
            strlbl_bottom.Text = "Hanya Permohonan Baru boleh dibatalkan."
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
                Debug.WriteLine("Error(batalPermohonan-senarai_permohonan:194): " & strRef)
                Return False
            End If
        Catch ex As Exception
            Debug.WriteLine("Error(batalPermohonan-senarai_permohonan:198): " & ex.Message)
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
                            Return False
                        ElseIf reader("permohonan_status").Equals("PERMOHONAN MENUNGGU") Then
                            Return False
                        ElseIf reader("permohonan_status").Equals("PERMOHONAN BARU") Then
                            Return True
                        End If
                    Loop
                Else
                    Debug.WriteLine("Error(showButton-senarai_permohonan:223): No Rows")
                    Return False
                End If
            Catch ex As Exception
                Debug.WriteLine("Error(showButton-senarai_permohonan:227): " & ex.Message)
                Return False
            Finally
                conn.Close()
            End Try
        End Using
        Return False
    End Function

    Protected Function changeStatus(ByVal status As String) As String
        Select Case status
            Case "PERMOHONAN MENUNGGU"
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

    Private Sub loadPangkalan()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("SELECT pangkalan_id, pangkalan_nama FROM spk_pangkalan;", conn)
            Dim ds As New DataSet
            Try
                conn.Open()
                Dim da As New SqlDataAdapter(cmd)
                da.Fill(ds)
                ddlCarianPangkalan.Items.Insert(0, New ListItem("-- SILA PILIH --", String.Empty))
                ddlCarianPangkalan.SelectedIndex = 0
                ddlCarianPangkalan.DataSource = ds
                ddlCarianPangkalan.DataTextField = "pangkalan_nama"
                ddlCarianPangkalan.DataValueField = "pangkalan_id"
                ddlCarianPangkalan.DataBind()
                ddlCarianPangkalan.Items.Insert(0, New ListItem("-- SILA PILIH --", String.Empty))
                ddlCarianPangkalan.SelectedIndex = 0
            Catch ex As Exception
                Debug.WriteLine("ERROR(loadPangkalan-senarai_permohonan:271): " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Private Sub loadKuarters()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("SELECT kuarters_id, kuarters_nama FROM spk_kuarters WHERE pangkalan_id = " & ddlCarianPangkalan.SelectedValue & "; ", conn)
            Dim ds As New DataSet

            Try
                conn.Open()
                Dim da As New SqlDataAdapter(cmd)
                da.Fill(ds)
                ddlCarianKuarters.DataSource = ds
                ddlCarianKuarters.DataTextField = "kuarters_nama"
                ddlCarianKuarters.DataValueField = "kuarters_id"
                ddlCarianKuarters.DataBind()
                ddlCarianKuarters.Items.Insert(0, New ListItem("-- SILA PILIH --", String.Empty))
                ddlCarianKuarters.SelectedIndex = 0
            Catch ex As Exception
                Debug.Write("ERROR(loadKuarters): " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Private Sub loadStatusPermohonan()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("SELECT config_parameter, config_value FROM general_config WHERE config_type = 'STATUS PERMOHONAN';", conn)
                Using sda As New SqlDataAdapter(cmd)
                    Try
                        Dim ds As New DataSet
                        sda.Fill(ds)
                        ddlCarianStatus.DataSource = ds
                        ddlCarianStatus.DataValueField = "config_value"
                        ddlCarianStatus.DataTextField = "config_parameter"
                        ddlCarianStatus.DataBind()
                        ddlCarianStatus.Items.Insert(0, New ListItem("-- SILA PILIH --", String.Empty))
                        ddlCarianStatus.SelectedIndex = 0
                    Catch ex As Exception
                        Debug.WriteLine("ERROR(loadStatusPermohonan-senarai_permohonan:315): " & ex.Message)
                    End Try
                End Using
            End Using
        End Using
    End Sub

    Private Sub ddlCarianPangkalan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCarianPangkalan.SelectedIndexChanged
        BindData(tblSenaraiPermohonan)
        If ddlCarianPangkalan.SelectedIndex > 0 Then
            loadKuarters()
            ddlCarianKuarters.Enabled = True
        Else
            ddlCarianKuarters.SelectedIndex = 0
            ddlCarianKuarters.Enabled = False
        End If
    End Sub

    Private Sub ddlCarianKuarters_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCarianKuarters.SelectedIndexChanged
        BindData(tblSenaraiPermohonan)
    End Sub

    Private Sub ddlCarianStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCarianStatus.SelectedIndexChanged
        BindData(tblSenaraiPermohonan)
    End Sub

    Protected Function canCancel(ByVal pID As Integer) As Boolean
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("SELECT permohonan_status_permohonan FROM spk_permohonan WHERE permohonan_id = " & pID & ";", conn)
                Using reader As SqlDataReader = cmd.ExecuteReader
                    If reader.HasRows() Then
                        While (reader.Read())
                            Select Case reader("permohonan_status_permohonan")
                                Case "PERMOHONAN BARU"
                                    Return True
                                Case Else
                                    Return False
                            End Select
                        End While
                    Else
                        Debug.WriteLine("ERROR(canCancel-senarai_permohonan:355): Not found pID(" & pID & ")")
                        Return False
                    End If
                End Using
            End Using
        End Using
        Return False
    End Function

    Protected Function test(ByVal query As String, Optional dt As DropDownList = Nothing)

    End Function


    Protected Sub newNotifkasi(ByVal untuk As String, ByVal kumpulan As Integer, ByVal permohonanID As Integer)
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("", conn)

            End Using
        End Using
    End Sub
End Class