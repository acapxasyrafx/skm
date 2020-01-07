Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Web.UI.HtmlControls

Public Class konfigurasi_unit
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            load_page()
        End If
    End Sub

    Protected Sub load_page()
        check_session()
        load_units()
        load_pangkalan()
    End Sub

    Protected Sub check_session()
        If Session("user_id") = Nothing Then
            Response.Redirect("default.aspx")
        Else
            If Session("user_type").Equals("ADMIN") Then

            Else
                Response.Redirect("default.aspx")
            End If
        End If
    End Sub

    Protected Function GetSQL()
        Dim query As String = ""
        Dim tempQuery As String = "SELECT 
                A.unit_id
	            , A.unit_nama
	            , (A.unit_blok + '-' + A.unit_tingkat + '-' + unit_nombor) AS 'unit_nama_lain'
	            , B.kuarters_nama
	            , C.pangkalan_nama
            FROM 
	            spk_unit A
	            LEFT JOIN spk_kuarters B ON B.kuarters_id = A.kuarters_id
	            LEFT JOIN spk_pangkalan C ON C.pangkalan_id = B.pangkalan_id"
        Dim whereQuery As String = " WHERE A.unit_id IS NOT NULL"
        Dim orderQuery As String = " ORDER BY C.pangkalan_nama ASC;"

        If ddlPangkalan.SelectedIndex > 0 Then
            whereQuery += " AND C.pangkalan_id = @pangkalanID"
        End If

        If ddlKuarters.SelectedIndex > 0 Then
            whereQuery += " AND B.kuarters_id = @kuartersID"
        End If

        If tbCari.Text.Length > 0 Then
            whereQuery += " AND (A.unit_nama LIKE '%@carian%')
                            OR ((A.unit_blok + '-' + A.unit_tingkat + '-' + A.unit_nombor) LIKE '%@carian%')"
        End If
        query = tempQuery & whereQuery & orderQuery
        Return query
    End Function

    Protected Sub load_units()
        Dim query = GetSQL()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand(query, conn)
                If tbCari.Text.Length > 0 Then
                    cmd.Parameters.Add("@carian", SqlDbType.Text).Value = tbCari.Text
                End If
                If ddlKuarters.SelectedIndex > 0 Then
                    cmd.Parameters.Add("@kuartersID", SqlDbType.Int).Value = ddlKuarters.SelectedValue
                End If
                If ddlPangkalan.SelectedIndex > 0 Then
                    cmd.Parameters.Add("@pangkalanID", SqlDbType.Int).Value = ddlPangkalan.SelectedValue
                End If
                Try
                    conn.Open()
                    Dim ds As New DataSet
                    Dim sda As New SqlDataAdapter(cmd)
                    sda.Fill(ds)
                    datRespondent.DataSource = ds
                    datRespondent.DataBind()
                Catch ex As Exception
                    Debug.WriteLine("Error(load_units-konfigurasi_unit: 37): " & ex.Message)
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Sub

    Protected Sub load_pangkalan()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("SELECT pangkalan_nama, pangkalan_id FROM spk_pangkalan", conn)
                Try
                    conn.Open()
                    Using sda As New SqlDataAdapter(cmd)
                        Dim ds As New DataSet
                        sda.Fill(ds)
                        ddlPangkalan.DataSource = ds
                        ddlPangkalan.DataTextField = "pangkalan_nama"
                        ddlPangkalan.DataValueField = "pangkalan_id"
                        ddlPangkalan.DataBind()
                        ddlPangkalan.Items.Insert(0, New ListItem("-- PILIH --", Nothing))
                        ddlPangkalan.SelectedIndex = 0
                    End Using
                Catch ex As Exception

                End Try
            End Using
        End Using
    End Sub

    Protected Sub load_kuarters()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand("SELECT kuarters_nama, kuarters_id from spk_kuarters WHERE pangkalan_id = @pangkalanID;", conn)
                cmd.Parameters.Add("@pangkalanID", SqlDbType.Int).Value = ddlPangkalan.SelectedValue
                Try
                    conn.Open()
                    Using sda As New SqlDataAdapter(cmd)
                        Dim ds As New DataSet
                        sda.Fill(ds)
                        ddlKuarters.DataSource = ds
                        ddlKuarters.DataTextField = "kuarters_nama"
                        ddlKuarters.DataValueField = "kuarters_id"
                        ddlKuarters.DataBind()
                        ddlKuarters.Items.Insert(0, New ListItem("-- PILIH --", Nothing))
                        ddlKuarters.SelectedIndex = 0
                    End Using
                Catch ex As Exception

                End Try
            End Using
        End Using
    End Sub

    Private Sub ddlPangkalan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPangkalan.SelectedIndexChanged
        If ddlPangkalan.SelectedIndex > 0 Then
            ddlKuarters.Enabled = True
            load_kuarters()
        End If
        load_units()
    End Sub

    Private Sub ddlKuarters_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKuarters.SelectedIndexChanged
        load_units()
    End Sub
End Class