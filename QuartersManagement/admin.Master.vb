Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Resources

Public Class admin
    Inherits System.Web.UI.MasterPage
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            load_page()
        End If
    End Sub

    Protected Sub load_page()

    End Sub

    Protected Function getSQL() As String
        Dim currUserType As String = Session("user_type")
        Dim tempSQL As String = "SELECT * FROM spk_notifikasi"
        Dim whereSQL As String = "WHERE notifikasi_untuk = '" & currUserType.ToUpper & "'"
        Dim orderBy As String = "ORDER BY notifikasi_tarikh ASC;"
        getSQL = tempSQL & whereSQL & orderBy
        Return getSQL
    End Function

    Protected Sub count_notification()

    End Sub
    Protected Sub load_menu()
        Using conn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using cmd As New SqlDataAdapter(getSQL(), conn)
                Try

                Catch ex As Exception

                End Try
            End Using
        End Using
    End Sub
End Class