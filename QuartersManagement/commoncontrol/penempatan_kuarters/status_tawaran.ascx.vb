Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Drawing
Imports System.Net.Mail
Public Class status_tawaran
    Inherits System.Web.UI.UserControl

    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction
    Dim page_view As New Integer
    Dim cmd As SqlCommand
    Dim errCount As Integer
    Dim dispID As String
    Dim stdID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                'getDetail()
                'ddlUnit()
                loadPermohonan()
                loadDDLSuratTawaran()
            Catch ex As Exception

            End Try
        End If

    End Sub

    Private Sub loadDDLSuratTawaran()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectioNString"))
            Using cmd As New SqlCommand("SELECT * FROM spk_suratTawaranConfig")

            End Using
        End Using
    End Sub

    Private Sub loadPermohonan()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("
                SELECT 
	                A.permohonan_id
	                ,	SUBSTRING(G.log_tarikh,1,10) 'permohonan_tarikh'
	                ,	B.pengguna_nama
	                ,	B.pengguna_no_tentera
	                ,	C.pangkat_nama
	                ,	B.pengguna_jantina
	                ,	E.kuarters_nama
	                ,	CONCAT(D.unit_blok,'-',D.unit_tingkat,'-',D.unit_nombor) 'unit_nama'
                FROM 
	                spk_permohonan A 
	                JOIN spk_pengguna B On B.pengguna_id = A.pengguna_id
	                JOIN spk_pangkat C ON C.pangkat_id = B.pangkat_id
	                JOIN spk_unit D ON D.unit_id = A.unit_id
	                JOIN spk_kuarters E ON E.kuarters_id = A.kuarters_id
	                JOIN (SELECT * FROM spk_logPermohonan WHERE log_status = 'PERMOHONAN BARU') G ON G.permohonan_id = A.permohonan_id
                WHERE 
	                A.permohonan_id = @permohonanID")
                cmd.Connection = conn
                cmd.Parameters.Add("@permohonanID", SqlDbType.Int).Value = Request.QueryString("uid")
                Try
                    Dim reader As SqlDataReader
                    conn.Open()
                    reader = cmd.ExecuteReader
                    If reader.HasRows Then
                        While reader.Read
                            lblNama.Text = reader("pengguna_nama")
                            lblNoTentera.Text = reader("pengguna_no_tentera")
                            lblPangkat.Text = reader("pangkat_nama")
                            lblKuarters.Text = reader("kuarters_nama")
                            lblUnit.Text = reader("unit_nama")
                        End While
                    Else
                        Debug.WriteLine("Error(loadPermohonan): No Rows")
                    End If
                Catch ex As Exception
                    Debug.WriteLine("Error(loadPermohonan): " & ex.Message)
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Sub
    'Private Sub ddlUnit()
    '    Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
    '        Dim cmd As New SqlCommand("
    '            SELECT 
    '                F.unit_id as unit_id 
    '                , concat(F.unit_blok,'-',F.unit_tingkat,'-',F.unit_nombor) as unit_no
    '                FROM 
    '                    spk_permohonan as B
    '                LEFT JOIN spk_pengguna A ON B.pengguna_id = A.pengguna_id
    '	LEFT JOIN spk_pangkalan C ON A.pangkalan_id = C.pangkalan_id 
    '	LEFT JOIN spk_pangkat D ON A.pangkat_id = D.pangkat_id
    '                LEFT JOIN spk_kuarters E ON B.kuarters_id = E.kuarters_id
    '                LEFT JOIN spk_unit F ON B.unit_id = F.unit_id
    '                WHERE 
    '                    permohonan_id = '" & Request.QueryString("uid") & "' AND F.unit_status = 'Available' and F.unit_status IS NOT NULL  
    '                ; ", conn)
    '        Dim ds As New DataSet

    '        Try
    '            conn.Open()
    '            Dim sda As New SqlDataAdapter(cmd)
    '            sda.Fill(ds)
    '            ddl_unit.DataSource = ds
    '            ddl_unit.DataTextField = "unit_no"
    '            ddl_unit.DataValueField = "unit_id"
    '            ddl_unit.DataBind()
    '            ddl_unit.Items.Insert(0, New ListItem("-- SILA PILIH --", String.Empty))
    '            ddl_unit.SelectedIndex = 0
    '        Catch ex As Exception
    '            Debug.Write("ERROR(ddlUnit): " & ex.Message)
    '        Finally
    '            conn.Close()
    '        End Try
    '    End Using
    'End Sub
    'Public Sub getDetail()

    '    Dim query As String = "SELECT 
    '                            A.permohonan_id as permohoanan_id
    '                            ,   B.pengguna_nama as pengguna_nama
    '                            ,   B.pengguna_no_tentera as pengguna_no_tentera
    '                            ,   C.kuarters_nama as kuarters_nama                               
    '                        FROM 
    '                            spk_permohonan A
    '                            LEFT JOIN spk_pengguna B on A.pengguna_id = B.pengguna_id
    '                            LEFT JOIN spk_kuarters C on A.kuarters_id = C.kuarters_id
    '                        WHERE 
    '                            A.permohonan_id ='" & Request.QueryString("uid") & "'"
    '    Dim sqlDA As New SqlDataAdapter(query, objConn)
    '    Try
    '        Dim ds As New DataSet
    '        sqlDA.Fill(ds, "AnyTable")

    '        Dim getklsrid As String = "SELECT 
    '                                    A.permohonan_id as permohoanan_id
    '                                    ,   B.pengguna_nama as pengguna_nama
    '                                    ,   B.pengguna_no_tentera as pengguna_no_tentera
    '                                    ,   C.kuarters_nama as kuarters_nama                                
    '                                    FROM 
    '                                        spk_permohonan A
    '                                        LEFT JOIN spk_pengguna B on A.pengguna_id = B.pengguna_id
    '                                        LEFT JOIN spk_kuarters C on A.kuarters_id = C.kuarters_id
    '                                    WHERE 
    '                                        A.permohonan_id ='" & Request.QueryString("uid") & "'"
    '        Dim klsrid As String = oCommon.getFieldValue(getklsrid)



    '        lbloutname.Text = ds.Tables(0).Rows(0).Item("pengguna_nama")
    '        lblOutKuarters.Text = ds.Tables(0).Rows(0).Item("kuarters_nama")
    '    Catch ex As Exception

    '    End Try

    'End Sub
    Public Sub ContentLetter()

        Try
            ''find name to replace
            Dim get_Name As String = "SELECT A.pengguna_nama 
                    FROM spk_permohonan as B
                    left join spk_pengguna A on B.pengguna_id = A.pengguna_id
					left join spk_pangkalan C on A.pangkalan_id = C.pangkalan_id 
					left join spk_pangkat D on A.pangkat_id = D.pangkat_id
                    left join spk_kuarters E on B.kuarters_id = E.kuarters_id
                    left join spk_unit F on B.unit_id = F.unit_id WHERE permohonan_id = '" & Request.QueryString("uid") & "'"
            Dim find_Name As String = oCommon.getFieldValue(get_Name)

            ''find id to replace
            Dim get_ID As String = "SELECT A.pengguna_no_tentera as no_tentera 
                    FROM spk_permohonan as B
                    left join spk_pengguna A on B.pengguna_id = A.pengguna_id
					left join spk_pangkalan C on A.pangkalan_id = C.pangkalan_id 
					left join spk_pangkat D on A.pangkat_id = D.pangkat_id
                    left join spk_kuarters E on B.kuarters_id = E.kuarters_id
                    left join spk_unit F on B.unit_id = F.unit_id WHERE permohonan_id = '" & Request.QueryString("uid") & "'"
            Dim find_ID As String = oCommon.getFieldValue(get_ID)

            ''find class to replace
            Dim get_class As String = "SELECT D.pangkat_nama
                    FROM spk_permohonan as B
                    left join spk_pengguna A on B.pengguna_id = A.pengguna_id
					left join spk_pangkalan C on A.pangkalan_id = C.pangkalan_id 
					left join spk_pangkat D on A.pangkat_id = D.pangkat_id
                    left join spk_kuarters E on B.kuarters_id = E.kuarters_id
                    left join spk_unit F on B.unit_id = F.unit_id WHERE permohonan_id = '" & Request.QueryString("uid") & "'"
            Dim find_class As String = oCommon.getFieldValue(get_class)

            ''find class to replace
            Dim get_kuarters As String = "SELECT E.kuarters_nama as unit
                    FROM spk_permohonan as B
                    left join spk_pengguna A on B.pengguna_id = A.pengguna_id
					left join spk_pangkalan C on A.pangkalan_id = C.pangkalan_id 
					left join spk_pangkat D on A.pangkat_id = D.pangkat_id
                    left join spk_kuarters E on B.kuarters_id = E.kuarters_id
                    left join spk_unit F on B.unit_id = F.unit_id WHERE permohonan_id = '" & Request.QueryString("uid") & "'"
            Dim find_kuarters As String = oCommon.getFieldValue(get_kuarters)

            ''find class to replace
            'Dim get_unit As String = "SELECT concat(F.unit_blok,'-',F.unit_tingkat,'-',F.unit_nombor) as unit_no
            '        FROM spk_unit as F
            '        WHERE F.unit_id = '" & ddl_unit.SelectedValue & "' and F.unit_status = 'Available' and F.unit_status IS NOT NULL"
            'Dim find_unit As String = oCommon.getFieldValue(get_unit)

            Dim find_date As String = datepicker.Text

            Dim content As String = Server.HtmlDecode(oCommon.getFieldValue("SELECT suratTawaranConfig_parameter FROM spk_suratTawaranConfig where suratTawaranConfig_id = 1 ").ToString)

            content = content.Replace("{NAME}", find_Name)
            content = content.Replace("{ID}", find_ID)
            content = content.Replace("{CLASS}", find_class)
            'content = content.Replace("{UNIT}", find_unit)
            content = content.Replace("{DATE}", find_date)

            editorSurattawaran.Content = content

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Dim uid As String = Request.QueryString("uid")
        Dim saveQuery As String = ""

        strSQL = "select id from spk_suratTawaran where permohonan_id = '" & uid & "'"
        strRet = oCommon.getFieldValue(strSQL)

        If strRet.Length > 0 Then
            saveQuery = "UPDATE spk_suratTawaran SET suratTawaran_content='" & Server.HtmlEncode(editorSurattawaran.Content) & "' WHERE permohonan_id='" & Request.QueryString("uid") & "'"
            strRet = oCommon.ExecuteSQL(saveQuery)

            If strRet = 0 Then

            End If
        Else

            strSQL = "INSERT INTO spk_suratTawaran(suratTawaran_content,permohonan_id) VALUES ('" & Server.HtmlEncode(editorSurattawaran.Content) & "','" & uid & "')"
            strRet = oCommon.ExecuteSQL(strSQL)

            If strRet = 0 Then

            End If

        End If

    End Sub
End Class