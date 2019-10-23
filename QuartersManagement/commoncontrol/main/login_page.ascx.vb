Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Resources
Public Class login_page
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim nMark As Integer

    Private rm As ResourceManager
    Dim ci As CultureInfo

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Response.Redirect("Homepage.aspx")
    End Sub
    Private Function isExistL(ByVal strSQL As String) As String
        If strSQL.Length = 0 Then
            Return False
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
        Dim strConn1 As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")
            If ds.Tables(0).Rows.Count > 0 Then
                lblDebug.Text = "OK:" & strConn
                Return True
            Else
                lblDebug.Text = "NOK:" & strConn
                Return False
            End If

        Catch ex As Exception
            lblDebug.Text = "Err:" & ex.Message
            Return False
        Finally
            objConn.Dispose()
        End Try

    End Function
    Private Sub displayDebug(ByVal strMsg As String)
        If oCommon.getAppsettings("isDebug") = "Y" Then
            lblDebug.Text = strMsg
        End If
    End Sub
    Private Sub dummy()
        Dim strSQL1 As String = ""
        Dim strSQL2 As String = ""
        'strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "' AND Pwd='" & oDes.EncryptData(txtPwd.Text) & "'"

        strSQL = "select top 1 A.userLvlAccess from skp.dbo.user_detail A WITH (NOLOCK) 
                   
                where A.userID = '" & oCommon.FixSingleQuotes(txtLoginID.Text) & "' and A.userPass = '" & txtPwd.Text & "''"

        If oCommon.isExist(strSQL) = True Then
            
            Select Case getUserProfile_UserType()
                Case "Admin"
                    Response.Redirect("default.aspx")
                Case "User"
                    Response.Redirect("default.aspx")
                Case Else
                    lblMsg.Text = "Invalid User Type! " & getUserProfile_UserType()
            End Select
        Else
            oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "LOGIN-FAILED", "NA")
        End If
    End Sub

    Private Function getUserProfile_UserType() As String
        Dim usrType As String
        Dim tmpSQL As String = "select top 1 staff_position from staff_info where staff_login = '" & oCommon.FixSingleQuotes(txtLoginID.Text) & "' "


        If oCommon.isExist(strSQL) = True Then
            strRet = oCommon.getFieldValue(tmpSQL)
            usrType = strRet
        End If

        Return usrType

    End Function


End Class