Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Resources

Public Class user
    Inherits System.Web.UI.MasterPage
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim currUserType As String = ""
    Dim currUserID As Integer
    Dim totalNotification As Integer = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("user_type") IsNot Nothing Then
                currUserType = Session("user_type")
                currUserID = Session("user_id")
                load_page()
            Else
                Response.Redirect("default.aspx")
            End If
        End If
    End Sub

    Protected Sub load_page()
        jumlahNotifikasi.InnerText = totalNotification
        If load_notifikasi() Then
            adaNotifikasi.Visible = True
            tiadaNotifikasi.Visible = False
            jumlahNotifikasi.InnerText = totalNotification
        Else
            adaNotifikasi.Visible = False
            tiadaNotifikasi.Visible = True
        End If
    End Sub

    Private Sub logKeluar_ServerClick(sender As Object, e As EventArgs) Handles logKeluar.ServerClick
        Session("user_type") = Nothing
        Session("user_id") = Nothing
        Session("pangkat_id") = Nothing
        Response.Redirect("default.aspx")
    End Sub

    Protected Function getSQL() As String
        Dim tempSQL As String = "SELECT 
	        A.permohonan_id
	        , A.pengguna_id
	        , A.notifikasi_id
	        , A.notifikasi_untuk
	        , A.notifikasi_tarikh
	        , A.notifikasi_checked
	        , B.config_parameter 'notifkasi_kumpulan'
	        , REPLACE(B.config_value,'{NO_PERMOHONAN}',C.permohonan_no_permohonan) 'notifikasi_log'
        FROM 
	        spk_notifikasi A
	        JOIN general_config B ON B.config_id = A.notifikasi_kumpulan
	        JOIN spk_permohonan C ON C.permohonan_id = A.permohonan_id"
        Dim whereSQL As String = " WHERE 
	        A.notifikasi_untuk = '" & currUserType.ToUpper & "'
            AND A.pengguna_id = " & currUserID & "
            AND A.notifikasi_checked = 0"
        Dim orderBy As String = " ORDER BY notifikasi_tarikh DESC;"
        getSQL = tempSQL & whereSQL & orderBy
        Return getSQL
    End Function

    Protected Function countRows(ByVal dt As DataTable) As Integer
        totalNotification += dt.Rows.Count
        Return dt.Rows.Count
    End Function

    Protected Function load_notifikasi() As Boolean
        Dim query = getSQL()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlCommand(query, conn)
                Using sda As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable
                    sda.Fill(dt)
                    If countRows(dt) > 0 Then
                        rptNotifikasi.DataSource = dt
                        rptNotifikasi.DataBind()
                        Return True
                    Else
                        Return False
                    End If
                End Using
            End Using
        End Using
    End Function
End Class