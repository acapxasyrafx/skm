Imports System.Data.SqlClient

Public Class senarai_penempatan_pemohon
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                Load_Page()
            End If

        Catch ex As Exception

            MsgTop.Attributes("class") = "errorMsg"
            strlbl_top.Text = strSysErrorAlert
            MsgBottom.Attributes("class") = "errorMsg"
            strlbl_bottom.Text = strSysErrorAlert & "<br>" & ex.Message

        Finally

        End Try
    End Sub

    Private Sub Load_Page()
        loadPangkalan()
        loadPangkat()
        BindData(datRespondent)
    End Sub

    Private Sub Refresh_ServerClick(sender As Object, e As EventArgs) Handles Refresh.ServerClick
        Response.Redirect("Senarai.Penempatan.Pemohon.aspx")
    End Sub

    Private Function ValidateData() As Boolean
        Return True
    End Function

    Private Function Save() As Boolean
        Return False
    End Function

    Private Function getSQL() As String
        Dim tempSQL As String
        Dim whereSQL As String = " WHERE A.permohonan_status = 'PERMOHONAN DITERIMA'"
        Dim orderSQL As String = "ORDER BY A.permohonan_tarikh DESC"
        tempSQL = "SELECT 
	        A.permohonan_id
	        , C.pengguna_nama
	        , D.pangkat_nama
	        , F.pangkalan_nama
	        , E.unit_nama
	        , (E.unit_blok + '-' + E.unit_tingkat + '-' + E.unit_nombor) AS 'unit_nama_lain'
	        , A.permohonan_tarikh_kemasukan
	        , G.kuarters_nama
            , F.pangkalan_id
            , D.pangkat_id
            , G.kuarters_id
        FROM 
	        spk_permohonan A
	        JOIN spk_historyPengguna B ON B.permohonan_id = A.permohonan_id
	        JOIN spk_pengguna C ON C.pengguna_id = A.pengguna_id
	        JOIN spk_pangkat D ON D.pangkat_id = B.pangkat_id
	        JOIN spk_unit E ON E.unit_id = A.unit_id
	        JOIN spk_pangkalan F ON F.pangkalan_id = E.pangkalan_id
	        JOIN spk_kuarters G ON G.kuarters_id = E.kuarters_id"
        If ddlCarianPangkalan.SelectedIndex > 0 Then
            whereSQL = whereSQL & " AND F.pangkalan_id = " & ddlCarianPangkalan.SelectedValue & ""
        End If
        If ddlCarianKuarters.SelectedIndex > 0 Then
            whereSQL = whereSQL & " AND G.kuarters_id = " & ddlCarianKuarters.SelectedValue & ""
        End If
        If ddlCarianPangkat.SelectedIndex > 0 Then
            whereSQL = whereSQL & " AND D.pangkat_id = " & ddlCarianPangkat.SelectedValue & ""
        End If
        If tbCarianNama.Text.Count > 0 Then
            whereSQL = whereSQL & " AND (C.pengguna_nama LIKE '%" & tbCarianNama.Text & "%'"
            whereSQL = whereSQL & " OR B.pengguna_no_tentera LIKE '%" & tbCarianNama.Text & "%')"
        End If
        getSQL = tempSQL & whereSQL & orderSQL
        Return getSQL
    End Function

    Private Function GetData(ByVal cmd As SqlCommand) As DataTable
        Dim dt As New DataTable
        Dim strConString As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim conn As New SqlConnection(strConString)
        Dim sda As New SqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.Connection = conn
        Try
            conn.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
            sda.Dispose()
            conn.Dispose()
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
                MsgBottom.InnerText = strDataBindAlert
            Else
                MsgBottom.Attributes("class") = "successMsg"
                MsgBottom.InnerText = strRecordBindAlert & myDataSet.Tables(0).Rows.Count
            End If
            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            MsgBottom.Attributes("class") = "errorMsg"
            Debug.WriteLine("Erro(BindData)" & ex.Message)
            Return False
        End Try
        Return False
    End Function

    Private Sub loadPangkalan()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("SELECT pangkalan_nama, pangkalan_id FROM spk_pangkalan;", conn)
            Dim ds As New DataSet
            Try
                conn.Open()
                Dim da As New SqlDataAdapter(cmd)
                da.Fill(ds, "AnyTable")
                ddlCarianPangkalan.DataSource = ds
                ddlCarianPangkalan.DataTextField = "pangkalan_nama"
                ddlCarianPangkalan.DataValueField = "pangkalan_id"
                ddlCarianPangkalan.DataBind()
                ddlCarianPangkalan.Items.Insert(0, New ListItem("--SILA PILIH--", String.Empty))
                ddlCarianPangkalan.SelectedIndex = 0
            Catch ex As Exception
                Debug.WriteLine("ERROR(loadPangkalan-senarai_penempatan_pemohon:165): " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Private Sub loadKuarters()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("SELECT kuarters_nama, kuarters_id FROM spk_kuarters WHERE pangkalan_id = " & ddlCarianPangkalan.SelectedValue & ";", conn)
            Dim ds As New DataSet
            Try
                conn.Open()
                Dim da As New SqlDataAdapter(cmd)
                da.Fill(ds, "AnyTable")
                ddlCarianKuarters.DataSource = ds
                ddlCarianKuarters.DataTextField = "kuarters_nama"
                ddlCarianKuarters.DataValueField = "kuarters_id"
                ddlCarianKuarters.DataBind()
                ddlCarianKuarters.Items.Insert(0, New ListItem("--SILA PILIH--", String.Empty))
                ddlCarianKuarters.SelectedIndex = 0
            Catch ex As Exception
                Debug.Write("ERROR(loadKuarters): " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Protected Sub loadPangkat()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("SELECT pangkat_nama, pangkat_id FROM spk_pangkat; ", conn)
            Dim ds As New DataSet

            Try
                conn.Open()
                Dim sda As New SqlDataAdapter(cmd)
                sda.Fill(ds)
                ddlCarianPangkat.DataSource = ds
                ddlCarianPangkat.DataTextField = "pangkat_nama"
                ddlCarianPangkat.DataValueField = "pangkat_id"
                ddlCarianPangkat.DataBind()
                ddlCarianPangkat.Items.Insert(0, New ListItem("-- SILA PILIH --", String.Empty))
                ddlCarianPangkat.SelectedIndex = 0
            Catch ex As Exception
                Debug.Write("ERROR(loadJawatan): " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub
    Private Sub ddlCarianPangkalan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCarianPangkalan.SelectedIndexChanged
        loadKuarters()
        If ddlCarianKuarters.Enabled = False Then
            ddlCarianKuarters.Enabled = True
        End If
        BindData(datRespondent)
    End Sub

    Private Sub ddlCarianKuarters_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCarianKuarters.SelectedIndexChanged
        BindData(datRespondent)
    End Sub

    Private Sub ddlCarianPangkat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCarianPangkat.SelectedIndexChanged
        BindData(datRespondent)
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        BindData(datRespondent)
    End Sub

    Private Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles datRespondent.RowCommand
        Try
            If (e.CommandName = "View_Permohonan") Then
                Dim strCID = e.CommandArgument.ToString
                Response.Redirect("Maklumat.Penempatan.Pemohon.aspx?uid=" & strCID)
            End If
        Catch ex As Exception
            MsgBottom.Attributes("class") = "errorMsg"
            strlbl_bottom.Text = strSysErrorAlert & "<br>" & ex.Message
        End Try
    End Sub
End Class