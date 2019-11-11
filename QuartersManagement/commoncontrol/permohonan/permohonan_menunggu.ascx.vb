Imports System.Data.SqlClient

Public Class permohonan_menunggu
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
                Sortdata()

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
    Private Sub Sortdata()
        Dim listItem1 As ListItem
        listItem1 = New ListItem("Default", "0")
        listItem1.Selected = True

        Dim listItem2 As ListItem
        listItem2 = New ListItem("Pangkat", "1")
        listItem2.Selected = False

        Dim listItem3 As ListItem
        listItem3 = New ListItem("Mata Poin", "2")
        listItem3.Selected = False

        ddlSort.Items.Add(listItem1)

        ddlSort.Items.Add(listItem2)

        ddlSort.Items.Add(listItem3)

    End Sub

    '-- BIND DATA --'
    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""

        Dim strOrder As String = ""

        If ddlSort.SelectedValue = "0" Then
            strOrder = " ORDER BY A.pengguna_id ASC"
        ElseIf ddlSort.SelectedValue = "1" Then
            strOrder = " ORDER BY D.pangkat_idx ASC"
        ElseIf ddlSort.SelectedValue = "2" Then
            strOrder = " ORDER BY B.permohonan_mata ASC"
        End If

        tmpSQL = "SELECT A.pengguna_id as pengguna_id ,A.pengguna_no_tentera as no_tentera ,A.pengguna_nama as nama ,C.pangkalan_nama as pangkalan
                    ,D.pangkat_nama as pangkat ,B.pengguna_id as pengguna_idx,E.kuarters_nama as unit,B.pemohonan_tarikh as tarikhMohon,B.permohonan_status as status
                    , B.permohonan_id as permohonan_id ,B.permohonan_mata as total_poin
                    FROM spk_permohonan as B
                    left join spk_pengguna A on B.pengguna_id = A.pengguna_id
					left join spk_pangkalan C on A.pangkalan_id = C.pangkalan_id 
					left join spk_pangkat D on A.pangkat_id = D.pangkat_id
                    left join spk_kuarters E on B.kuarters_id = E.kuarters_id
                    left join spk_unit F on B.unit_id = F.unit_id
					"
        strWhere += " WHERE B.permohonan_status = 'PERMOHONAN SEDANG DIPROSES'"

        If Not txt_nama.Text = "" Then
            strWhere += " AND (A.pengguna_nama LIKE '%" & txt_nama.Text & "%' or  A.pengguna_nama = '" & txt_nama.Text & "')"
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

                Response.Redirect("Maklumat.Pemohon.Menunggu.aspx?uid=" + strCID)
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
End Class