Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Drawing
Imports System.Net.Mail
Public Class status_tawaran
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

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
            Dim get_unit As String = "SELECT A.pengguna_id as pengguna_id ,A.pengguna_no_tentera as no_tentera ,A.pengguna_nama as nama ,C.pangkalan_nama as pangkalan 
                    ,D.pangkat_singkatan as pangkat ,B.pengguna_id as pengguna_idx,E.kuarters_nama as unit,substring (B.pemohonan_tarikh,1,10) as tarikhMohon,B.permohonan_status as status
                    , B.permohonan_id as permohonan_id ,B.permohonan_mata as total_poin, B.permohonan_nota as nota
                    FROM spk_permohonan as B
                    left join spk_pengguna A on B.pengguna_id = A.pengguna_id
					left join spk_pangkalan C on A.pangkalan_id = C.pangkalan_id 
					left join spk_pangkat D on A.pangkat_id = D.pangkat_id
                    left join spk_kuarters E on B.kuarters_id = E.kuarters_id
                    left join spk_unit F on B.unit_id = F.unit_id WHERE permohonan_id = '" & Request.QueryString("uid") & "'"
            Dim find_unit As String = oCommon.getFieldValue(get_unit)

            'Dim content As String = Server.HtmlDecode(ds.Tables(0).Rows(0).Item("status_tawaran").ToString)

            'content = content.Replace("{NAME}", find_Name)
            'content = content.Replace("{ID}", find_ID)
            'content = content.Replace("{CLASS}", find_class)
            'content = content.Replace("{}")

            'status_tawaran.Content = content

        Catch ex As Exception

        End Try
    End Sub

End Class