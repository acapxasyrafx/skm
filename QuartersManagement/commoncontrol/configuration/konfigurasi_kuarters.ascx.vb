Imports System.Data.SqlClient

Public Class konfigurasi_kuarters
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

                requestPangkalan()
                requestJenis()
                requestNegeri()

                If Not Request.QueryString("edit") = "" Then
                    lblConfig.Text = Request.QueryString("p")
                    Load_page()
                Else
                    requestPage()
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

    Private Sub requestPangkalan()

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        strSQL = "SELECT pangkalan_id, pangkalan_nama FROM spk_pangkalan WHERE pangkalan_id IS NOT NULL"

        If Not ddlNegeri.SelectedValue = "" Then
            strSQL += " AND pangkalan_negeri = '" & ddlNegeri.SelectedValue & "'"
        End If

        strSQL += " ORDER BY pangkalan_nama"

        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPangkalan.DataSource = ds
            ddlPangkalan.DataTextField = "pangkalan_nama"
            ddlPangkalan.DataValueField = "pangkalan_id"
            ddlPangkalan.DataBind()
            ddlPangkalan.Items.Insert(0, New ListItem("- PILIH -", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub requestJenis()

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        strSQL = "SELECT jenisKuarters_id, jenisKuarters_nama FROM spk_jenisKuarters ORDER BY jenisKuarters_nama"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlJenisKuarters.DataSource = ds
            ddlJenisKuarters.DataTextField = "jenisKuarters_nama"
            ddlJenisKuarters.DataValueField = "jenisKuarters_id"
            ddlJenisKuarters.DataBind()
            ddlJenisKuarters.Items.Insert(0, New ListItem("- PILIH -", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub requestNegeri()

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        strSQL = "SELECT config_id, config_parameter FROM general_config WHERE config_access = 'GLOBAL' AND config_type = 'NEGERI' ORDER BY config_idx, config_parameter"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlNegeri.DataSource = ds
            ddlNegeri.DataTextField = "config_parameter"
            ddlNegeri.DataValueField = "config_parameter"
            ddlNegeri.DataBind()
            ddlNegeri.Items.Insert(0, New ListItem("- PILIH -", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub Load_page()

        strSQL = "  SELECT spk_pangkalan.pangkalan_negeri, spk_pangkalan.pangkalan_id, 
                    spk_jenisKuarters.jenisKuarters_id, 
                    spk_kuarters.kuarters_nama, spk_kuarters.kuarters_alamat, spk_kuarters.kuarters_poskod, spk_kuarters.kuarters_bandar, spk_kuarters.kuarters_telefon, spk_kuarters.kuarters_faks, spk_kuarters.kuarters_emel
                    FROM spk_kuarters
                    LEFT JOIN spk_jenisKuarters ON spk_jenisKuarters.jenisKuarters_id = spk_kuarters.jenisKuarters_id
                    LEFT JOIN spk_pangkalan ON spk_pangkalan.pangkalan_id = spk_kuarters.pangkalan_id"

        strSQL += " WHERE kuarters_id = '" & Request.QueryString("edit") & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("pangkalan_negeri")) Then
                    ddlNegeri.SelectedValue = ds.Tables(0).Rows(0).Item("pangkalan_negeri")
                Else
                    ddlNegeri.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("pangkalan_id")) Then
                    ddlPangkalan.SelectedValue = ds.Tables(0).Rows(0).Item("pangkalan_id")
                Else
                    ddlPangkalan.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("jenisKuarters_id")) Then
                    ddlJenisKuarters.SelectedValue = ds.Tables(0).Rows(0).Item("jenisKuarters_id")
                Else
                    ddlJenisKuarters.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("kuarters_nama")) Then
                    txtNamaKuarters.Text = ds.Tables(0).Rows(0).Item("kuarters_nama")
                Else
                    txtNamaKuarters.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("kuarters_alamat")) Then
                    txtAlamat.Text = ds.Tables(0).Rows(0).Item("kuarters_alamat")
                Else
                    txtAlamat.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("kuarters_poskod")) Then
                    txtPoskod.Text = ds.Tables(0).Rows(0).Item("kuarters_poskod")
                Else
                    txtPoskod.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("kuarters_bandar")) Then
                    txtBandar.Text = ds.Tables(0).Rows(0).Item("kuarters_bandar")
                Else
                    txtBandar.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("kuarters_telefon")) Then
                    txtTelefon.Text = ds.Tables(0).Rows(0).Item("kuarters_telefon")
                Else
                    txtTelefon.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("kuarters_faks")) Then
                    txtFaks.Text = ds.Tables(0).Rows(0).Item("kuarters_faks")
                Else
                    txtFaks.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("kuarters_emel")) Then
                    txtEmel.Text = ds.Tables(0).Rows(0).Item("kuarters_emel")
                Else
                    txtEmel.Text = ""
                End If

            End If
            strRet = BindData(datRespondent)
        Catch ex As Exception
            MsgTop.Attributes("class") = "errorMsg"
            strlbl_top.Text = strSysErrorAlert
            MsgBottom.Attributes("class") = "errorMsg"
            strlbl_bottom.Text = strSysErrorAlert & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    '-- BIND DATA --'
    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""

        Dim strOrder As String = " ORDER BY spk_pangkalan.pangkalan_negeri, spk_pangkalan.pangkalan_nama, spk_kuarters.kuarters_nama ASC"

        tmpSQL = "  SELECT spk_pangkalan.pangkalan_negeri, spk_pangkalan.pangkalan_nama, 
                    spk_jenisKuarters.jenisKuarters_nama, 
                    spk_kuarters.kuarters_id, spk_kuarters.kuarters_nama, spk_kuarters.kuarters_alamat, spk_kuarters.kuarters_poskod, spk_kuarters.kuarters_bandar, spk_kuarters.kuarters_negeri, spk_kuarters.kuarters_telefon, spk_kuarters.kuarters_faks, spk_kuarters.kuarters_emel
                    FROM spk_kuarters
                    LEFT JOIN spk_jenisKuarters ON spk_jenisKuarters.jenisKuarters_id = spk_kuarters.jenisKuarters_id
                    LEFT JOIN spk_pangkalan ON spk_pangkalan.pangkalan_id = spk_kuarters.pangkalan_id"

        strWhere += " WHERE kuarters_id IS NOT NULL"


        If Not ddlNegeri.SelectedValue = "" Then
            strWhere += " AND spk_kuarters.kuarters_negeri = '" & ddlNegeri.SelectedValue & "'"
        End If

        If Not ddlPangkalan.SelectedValue = "" Then
            strWhere += " AND spk_kuarters.pangkalan_id = '" & ddlPangkalan.SelectedValue & "%'"
        End If

        If Not ddlJenisKuarters.SelectedValue = "" Then
            strWhere += " AND spk_jenisKuarters.jenisKuarters_id = '" & ddlJenisKuarters.SelectedValue & "%'"
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

    '--SAVE FUNCTION--'
    Private Function Save() As Boolean

        If Not Request.QueryString("edit") = "" Then

            strSQL = "UPDATE spk_kuarters SET "

            strSQL += " jenisKuarters_id = UPPER('" & ddlJenisKuarters.SelectedValue & "'),"
            strSQL += " pangkalan_id = UPPER('" & ddlPangkalan.SelectedValue & "'),"
            strSQL += " kuarters_negeri = UPPER('" & ddlNegeri.SelectedValue & "'),"
            strSQL += " kuarters_bandar = UPPER('" & txtBandar.Text & "'),"
            strSQL += " kuarters_poskod = UPPER('" & txtPoskod.Text & "'),"
            strSQL += " kuarters_nama = UPPER('" & txtNamaKuarters.Text & "'),"
            strSQL += " kuarters_alamat = UPPER('" & txtAlamat.Text & "'),"
            strSQL += " kuarters_telefon = UPPER('" & txtTelefon.Text & "'),"
            strSQL += " kuarters_faks = '" & txtFaks.Text & "',"
            strSQL += " kuarters_emel = UPPER('" & txtEmel.Text & "')"


            strSQL += " WHERE kuarters_id = '" & Request.QueryString("edit") & "'"

        Else
            strSQL = "INSERT INTO spk_kuarters (jenisKuarters_id, pangkalan_id, kuarters_negeri, kuarters_bandar, kuarters_poskod, kuarters_nama, kuarters_alamat, kuarters_telefon, kuarters_faks, kuarters_emel)"

            strSQL += " VALUES ("
            strSQL += " UPPER('" & ddlJenisKuarters.SelectedValue & "'),"
            strSQL += " UPPER('" & ddlPangkalan.SelectedValue & "'),"
            strSQL += " UPPER('" & ddlNegeri.SelectedValue & "'),"
            strSQL += " UPPER('" & txtBandar.Text & "'),"
            strSQL += " UPPER('" & txtPoskod.Text & "'),"
            strSQL += " UPPER('" & txtNamaKuarters.Text & "'),"
            strSQL += " UPPER('" & txtAlamat.Text & "'),"
            strSQL += " UPPER('" & txtTelefon.Text & "'),"
            strSQL += " UPPER('" & txtFaks.Text & "'),"
            strSQL += " UPPER('" & txtEmel.Text & "'))"

        End If

        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            Return True
        Else
            MsgTop.Attributes("class") = "errorMsg"
            strlbl_top.Text = strSysErrorAlert
            MsgBottom.Attributes("class") = "errorMsg"
            strlbl_bottom.Text = strSysErrorAlert & "<br>" & strRet
            Return False
        End If

    End Function

    '--DATA VALIDATION--'
    Private Function ValidateData() As Boolean
        'If Not oCommon.isNumeric(txtidx.Text) Then
        '    txtidx.Focus()
        '    Return False

        'End If
        Return True
    End Function

    Private Sub SaveFunction_ServerClick(sender As Object, e As EventArgs) Handles SaveFunction.ServerClick

        strlbl_bottom.Text = ""
        strlbl_top.Text = ""
        '--validate--'
        If ValidateData() = False Then
            MsgTop.Attributes("class") = "errorMsg"
            strlbl_top.Text = strDataValAlert
            MsgBottom.Attributes("class") = "errorMsg"
            strlbl_bottom.Text = strDataValAlert
            Exit Sub
        End If
        Try
            '--execute--'
            If Save() = True Then
                MsgTop.Attributes("class") = "successMsg"
                strlbl_top.Text = strSaveSuccessAlert
                MsgBottom.Attributes("class") = "successMsg"
                strlbl_bottom.Text = strSaveSuccessAlert
            Else
                MsgTop.Attributes("class") = "errorMsg"
                strlbl_top.Text = strSaveFailAlert
                MsgBottom.Attributes("class") = "errorMsg"
                strlbl_bottom.Text = strSaveFailAlert
            End If
        Catch ex As Exception
            MsgTop.Attributes("class") = "errorMsg"
            strlbl_top.Text = strSysErrorAlert
            MsgBottom.Attributes("class") = "errorMsg"
            strlbl_bottom.Text = strSysErrorAlert & "<br>" & ex.Message
        End Try

        If Not Request.QueryString("edit") = "" Then
            Dim Pagelabel As String = lblConfig.Text & "&q=" & lblQ.Text & "&lblTop=" & strlbl_top.Text & "&lblBottom=" & strlbl_top.Text
            Response.Redirect("Konfigurasi.Kuarters.aspx?p=" & Pagelabel)
        Else
            strRet = BindData(datRespondent)
        End If



    End Sub
    '--REFRESH BUTTON--'
    Private Sub Refresh_ServerClick(sender As Object, e As EventArgs) Handles Refresh.ServerClick
        Dim Pagelabel As String = lblConfig.Text & "&q=" & lblQ.Text
        Response.Redirect("Konfigurasi.Kuarters.aspx?p=" & Pagelabel)
    End Sub

    Private Sub requestPage()
        lblConfig.Text = Request.QueryString("p")
        lblQ.Text = Request.QueryString("q")
        If Not Request.QueryString("lblBottom") = "" Then
            strlbl_top.Text = Request.QueryString("lblTop")
            strlbl_bottom.Text = Request.QueryString("lblBottom")
        End If
    End Sub

    Private Sub clear()
        ddlNegeri.SelectedValue = ""
        ddlPangkalan.SelectedValue = ""
        ddlJenisKuarters.Text = ""
        txtNamaKuarters.Text = ""
        txtAlamat.Text = ""
        txtPoskod.Text = ""
        txtBandar.Text = ""
        txtTelefon.Text = ""
        txtFaks.Text = ""
        txtEmel.Text = ""
    End Sub

    '--DELETE FUNCTION--'
    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting

        Dim strCID = datRespondent.DataKeys(e.RowIndex).Values("kuarters_id").ToString

        'chk session to prevent postback
        If Not strCID = Session("strCID") Then
            strSQL = "DELETE FROM spk_kuarters WHERE kuarters_id = '" & strCID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            Session("strCID") = ""
        End If
        strRet = BindData(datRespondent)

    End Sub

    Private Sub ddlNegeri_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegeri.SelectedIndexChanged
        requestPangkalan()
        strRet = BindData(datRespondent)
    End Sub

End Class