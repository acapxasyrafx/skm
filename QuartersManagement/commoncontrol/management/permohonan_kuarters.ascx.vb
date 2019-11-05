Imports System.Data.SqlClient

Public Class permohonan_kuarters
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

    Dim perakuan_pemohon As String = ""
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cbPerakuanPemohon.Text = "Saya dengan ini memohon sebuah Rumah Keluarga mengikut peraturan dan Undang-Undang PAT Jil III(3) dan mengaku iaitu butiran-butiran yang dinyatakan seperti berikut adalah benar."
        populateDDLKuarters()
        loadUser()
        'HARI
        populateDay(ddlSewaMulaHari)
        populateDay(ddlSewaAkhirHari)
        populateDay(ddlWismaMulaHari)
        populateDay(ddlWismaAkhirHari)
        populateDay(ddlSeberangMulaHari)
        populateDay(ddlSeberangAkhirHari)
        populateDay(ddlTarikhMulaHari)
        'BULAN
        populateMonth(ddlSewaMulaBulan)
        populateMonth(ddlSewaAkhirBulan)
        populateMonth(ddlWismaMulaBulan)
        populateMonth(ddlWismaAKhirBulan)
        populateMonth(ddlSeberangMulaBulan)
        populateMonth(ddlSeberangAkhirBulan)
        populateMonth(ddlTarikhMulaBulan)
        'TAHUN
        populateYear(ddlSewaMulaTahun)
        populateYear(ddlSewaAkhirTahun)
        populateYear(ddlWismaMulaTahun)
        populateYear(ddlWismaAKhirTahun)
        populateYear(ddlSeberangMulaTahun)
        populateYear(ddlSeberangAkhirTahun)
        populateYear(ddlTarikhMulaTahun)
    End Sub

    Private Sub populateDDLKuarters()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("SELECT * FROM spk_kuarters", conn)
            Dim ds As New DataSet
            Try
                conn.Open()
                Dim da As New SqlDataAdapter(cmd)
                da.Fill(ds)
                ddlSenaraiRumah.DataSource = ds
                ddlSenaraiRumah.DataTextField = "kuarters_nama"
                ddlSenaraiRumah.DataValueField = "kuarters_id"
                ddlSenaraiRumah.DataBind()
            Catch ex As Exception
                Debug.Write("ERROR: " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Private Function getDate(ByVal hari As String, ByVal bulan As String, ByVal tahun As String) As String
        Return (tahun & "-" & bulan & "-" & hari)
    End Function

    Private Sub populateDay(ByVal ddl As DropDownList)
        For i As Integer = 1 To 31
            Dim temp As ListItem
            If i < 10 Then
                temp = New ListItem("0" & i, i)
            Else
                temp = New ListItem(i, i)
            End If
            ddl.Items.Add(temp)
        Next
    End Sub

    Private Sub populateMonth(ByVal ddl As DropDownList)
        For i As Integer = 1 To 12
            Dim temp As ListItem
            If i < 10 Then
                temp = New ListItem("0" & i, i)
            Else
                temp = New ListItem(i, i)
            End If
            ddl.Items.Add(temp)
        Next
    End Sub

    Private Sub populateYear(ByVal ddl As DropDownList)
        Dim startYear As Integer = Date.Now().Year - 20
        For i As Integer = 1 To 20
            Dim item As String = startYear + i
            Dim temp As New ListItem(item, item)
            If item.Equals(Date.Now().Year) Then
                temp.Selected = True
            End If
            ddl.Items.Add(temp)
        Next
    End Sub

    Private Sub loadUser()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim cmd As New SqlCommand("SELECT 
	            A.pengguna_id,
	            A.pengguna_nama,
	            A.pengguna_mykad,
	            A.pengguna_jantina,
	            A.pengguna_tarikh_lahir,
	            A.pengguna_kewarganegaraan,
                A.pengguna_mula_perkhidmatan,
                A.pengguna_tamat_perkhidmatan,
                A.pengguna_no_tentera,
	            B.pangkat_id,
	            B.pangkat_nama
            FROM 
	            admin.spk_pengguna A
	            JOIN admin.spk_pangkat B ON A.pangkat_id = B.pangkat_id
	            JOIN dbo.spk_pangkalan C ON A.pangkalan_id = C.pangkalan_id
            WHERE pengguna_id = 1",
            conn)

            Try
                conn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                If reader.HasRows Then
                    If reader.Read() Then
                        pengguna_id.Value = reader("pengguna_id")
                        lblNama.Text = reader("pengguna_nama")
                        lblTarikhLahir.Text = reader("pengguna_tarikh_lahir")
                        lblJantina.Text = reader("pengguna_jantina")
                        lblKewarganegaraan.Text = reader("pengguna_kewarganegaraan")
                        lblJawatan.Text = reader("pangkat_nama")
                        lblNoTentera.Text = reader("pengguna_no_tentera")
                        lblTarikhMulaBerkhidmat.Text = reader("pengguna_mula_perkhidmatan")
                        '-------------------
                        If IsDBNull(reader("pengguna_tamat_perkhidmatan")) Then
                            lblTarikhAkhirBerkhidmat.Text = "Masih Berkhidmat"
                        Else
                            lblTarikhAkhirBerkhidmat.Text = reader("pengguna_tamat_perkhidmatan")
                        End If
                        '-------------------
                    Else
                        Debug.Write("CANNOT READ")
                    End If
                Else
                    Debug.Write("NO ROWS")
                End If
            Catch ex As Exception
                Debug.Write("ERROR: " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Private Function Save() As Boolean
        Dim kuartersId = ddlSenaraiRumah.SelectedValue
        Dim penggunaId = pengguna_id.Value
        Dim bilAnak = txtBilAnak.Text
        Dim tarikhMula As String = getDate(ddlTarikhMulaHari.SelectedValue, ddlTarikhMulaBulan.SelectedValue, ddlTarikhMulaTahun.SelectedValue)
        'SEWA
        Dim tarikhSewaMula As String = getDate(ddlSewaMulaHari.SelectedValue, ddlSewaMulaBulan.SelectedValue, ddlSewaMulaTahun.SelectedValue)
        Dim tarikhSewaAkhir As String = getDate(ddlSewaAkhirHari.SelectedValue, ddlSewaAkhirBulan.SelectedValue, ddlSewaAkhirTahun.SelectedValue)
        'WISMA
        Dim tarikWismaMula As String = getDate(ddlWismaMulaHari.SelectedValue, ddlWismaMulaBulan.SelectedValue, ddlWismaMulaTahun.SelectedValue)
        Dim tarikWismaAkhir As String = getDate(ddlWismaAkhirHari.SelectedValue, ddlWismaAKhirBulan.SelectedValue, ddlWismaAKhirTahun.SelectedValue)
        'Seberang
        Dim tarikhSeberangMula As String = getDate(ddlSeberangMulaHari.SelectedValue, ddlSeberangMulaBulan.SelectedValue, ddlSeberangMulaTahun.SelectedValue)
        Dim tarikhSeberangAkhir As String = getDate(ddlSeberangAkhirHari.SelectedValue, ddlSeberangAkhirBulan.SelectedValue, ddlSeberangAkhirTahun.SelectedValue)
        'Transfer Pasukan
        Dim dariPasukan = ddlDariPasukan.SelectedValue
        Dim kePasukan = ddlKePasukan.SelectedValue

        strSQL += "INSERT INTO spk_permohonan (pengguna_id,unit_id,pemohonan_tarikh,permohonan_status) "
        strSQL += "VALUES (" & penggunaId & ", " & kuartersId & ", '" & Date.Now & "', 'Permohonan Baru')"

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

    Private Sub SaveFunction_ServerClick(sender As Object, e As EventArgs) Handles SaveFunction.ServerClick
        If cbPerakuanPemohon.Checked Then
            Try
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
                Debug.WriteLine("ERROR: " & ex.Message)
            End Try

        Else
            lblCheckBoxAlert.Text = "Sila setuju dengan perkara diatas."
            lblCheckBoxAlert.Attributes.CssStyle.Add("color", "red")
            lblCheckBoxAlert.Visible = True
        End If
    End Sub

    Private Sub Refresh_ServerClick(sender As Object, e As EventArgs) Handles Refresh.ServerClick
        Response.Redirect("Permohonan.Kuarters.aspx")
    End Sub
End Class