Imports System.Data.SqlClient

Public Class proses_penempatan_kuarters1
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

                If strlblMsgBottom = 0 Then
                    strlbl_bottom.Visible = True
                Else
                    strlbl_bottom.Visible = False
                End If
                If strlblMsgTop = 0 Then
                    strlbl_top.Visible = True
                Else
                    strlbl_top.Visible = False
                End If


                If Not Request.QueryString("edit") = "" Then
                    '' lblConfig.Text = Request.QueryString("p")
                    ''Load_page()
                Else
                    ''requestPage()
                    strRet = BindData(datRespondent)
                    loadJawatan()
                    loadKuarters()
                    loadPangkalan()

                End If

            End If

        Catch ex As Exception
            MsgTop.Attributes("class") = "errorMsg"
            strlbl_top.Text = strSysErrorAlert
            MsgBottom.Attributes("class") = "errorMsg"
            strlbl_bottom.Text = strSysErrorAlert & "<br>" & ex.Message
        Finally

        End Try

    End Sub


    Private Sub loadPangkalan()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("SELECT * FROM spk_pangkalan;", conn)
            Dim ds As New DataSet
            Try
                conn.Open()
                Dim da As New SqlDataAdapter(cmd)
                da.Fill(ds)
                ddlfilterPangkalan.DataSource = ds
                ddlfilterPangkalan.DataTextField = "pangkalan_nama"
                ddlfilterPangkalan.DataValueField = "pangkalan_id"
                ddlfilterPangkalan.DataBind()
                ddlfilterPangkalan.Items.Insert(0, New ListItem("-- SILA PILIH --", String.Empty))
                ddlfilterPangkalan.SelectedIndex = 0
            Catch ex As Exception
                Debug.WriteLine("ERROR(loadPangkalan): " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Private Sub loadKuarters()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("SELECT * FROM spk_kuarters;", conn)
            Dim ds As New DataSet

            Try
                conn.Open()
                Dim da As New SqlDataAdapter(cmd)
                da.Fill(ds)
                ddlfilterKuarters.DataSource = ds
                ddlfilterKuarters.DataTextField = "kuarters_nama"
                ddlfilterKuarters.DataValueField = "kuarters_id"
                ddlfilterKuarters.DataBind()
                ddlfilterKuarters.Items.Insert(0, New ListItem("-- SILA PILIH --", String.Empty))
                ddlfilterKuarters.SelectedIndex = 0
            Catch ex As Exception
                Debug.Write("ERROR(loadKuarters): " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Protected Sub loadJawatan()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("SELECT * FROM spk_pangkat; ", conn)
            Dim ds As New DataSet

            Try
                conn.Open()
                Dim sda As New SqlDataAdapter(cmd)
                sda.Fill(ds)
                ddlfilterPangkat.DataSource = ds
                ddlfilterPangkat.DataTextField = "pangkat_nama"
                ddlfilterPangkat.DataValueField = "pangkat_id"
                ddlfilterPangkat.DataBind()
                ddlfilterPangkat.Items.Insert(0, New ListItem("-- SILA PILIH --", String.Empty))
                ddlfilterPangkat.SelectedIndex = 0
            Catch ex As Exception
                Debug.Write("ERROR(loadJawatan): " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    '-- BIND DATA --'
    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""

        Dim strOrder As String = "ORDER BY A.permohonan_tarikh DESC"

        tmpSQL = "SELECT 
	        A.permohonan_id
            ,   A.permohonan_no_permohonan
	        ,	SUBSTRING(G.log_tarikh,1,10) 'permohonan_tarikh'
	        ,	A.permohonan_mata
	        ,	B.pengguna_no_tentera
	        ,	B.pengguna_nama
            ,   C.pangkat_nama
	        ,	D.pangkalan_nama
	        ,	E.kuarters_nama
	        ,	(F.unit_blok + '-' + F.unit_tingkat + '-' + F.unit_nombor) 'unit_nama'
        FROM 
	        spk_permohonan A
	        LEFT JOIN spk_pengguna B ON B.pengguna_id = A.pengguna_id
	        LEFT JOIN spk_pangkat C ON C.pangkat_id = B.pangkat_id
	        LEFT JOIN spk_pangkalan D ON D.pangkalan_id = B.pangkalan_id
	        LEFT JOIN spk_kuarters E ON E.kuarters_id = A.kuarters_id
	        LEFT JOIN spk_unit F ON F.unit_id = A.unit_id
	        LEFT JOIN (SELECT * FROM spk_logPermohonan WHERE log_status = 'PERMOHONAN BARU') G ON G.permohonan_id = A.permohonan_id"
        strWhere += " WHERE 
	        A.permohonan_status = 'PERMOHONAN SEDANG DIPROSES'
	        AND A.permohonan_sub_status = 'TERIMA TAWARAN UNIT'"

        Try
            If Not ddlfilterKuarters.SelectedValue = "" Then
                strWhere += " AND E.kuarters_id = '" & ddlfilterKuarters.SelectedValue & "'"
            End If
            If Not ddlfilterPangkalan.SelectedValue = "" Then
                strWhere += " AND D.pangkalan_id = '" & ddlfilterPangkalan.SelectedValue & "'"
            End If
            If Not ddlfilterPangkat.SelectedValue = "" Then
                strWhere += " AND B.pangkat_id = '" & ddlfilterPangkat.SelectedValue & "'"
            End If

        Catch ex As Exception
            MsgBottom.InnerText = ex.ToString
        End Try

        If Not txt_nama.Text = "" Then
            strWhere += " AND (
                B.pengguna_nama LIKE '%" & txt_nama.Text & "%' OR  B.pengguna_no_tentera LIKE '%" & txt_nama.Text & "%')"
        End If

        getSQL = tmpSQL & strWhere & strOrder

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

    Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Try

            If (e.CommandName = "ViewApllicant") Then
                Dim strCID = e.CommandArgument.ToString

                Response.Redirect("Proses.Penempatan.Tawaran.aspx?uid=" + strCID)
            ElseIf (e.CommandName = "Batal") Then
                Dim strCID = e.CommandArgument.ToString

                'chk session to prevent postback
                strSQL = "UPDATE spk_permohonan SET permohonan_status = 'PERMOHONAN ANDA DITOLAK' WHERE permohonan_id = '" & oCommon.FixSingleQuotes(strCID) & "'"
                oCommon.ExecuteSQL(strSQL)

            End If
            BindData(datRespondent)

        Catch ex As Exception
            MsgBottom.Attributes("class") = "errorMsg"
            strlbl_bottom.Text = strSysErrorAlert & "<br>" & ex.Message
        End Try
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)
    End Sub

    Private Sub ddlfilterPangkat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlfilterPangkat.SelectedIndexChanged
        strRet = BindData(datRespondent)
    End Sub

    Private Sub ddlfilterPangkalan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlfilterPangkalan.SelectedIndexChanged
        strRet = BindData(datRespondent)
    End Sub

    Private Sub ddlfilterKuarters_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlfilterKuarters.SelectedIndexChanged
        strRet = BindData(datRespondent)
    End Sub
End Class

'Private Sub Sortdata()
'    Dim listItem1 As ListItem
'    listItem1 = New ListItem("Default", "0")
'    listItem1.Selected = True

'    Dim listItem2 As ListItem
'    listItem2 = New ListItem("Pangkat", "1")
'    listItem2.Selected = False

'    Dim listItem3 As ListItem
'    listItem3 = New ListItem("Mata Poin", "2")
'    listItem3.Selected = False

'    ddlSort.Items.Add(listItem1)

'    ddlSort.Items.Add(listItem2)

'    ddlSort.Items.Add(listItem3)

'End Sub
