Imports Microsoft.VisualBasic

Imports System.IO

Imports Ionic.Utils.Zip

Public Class ZipService
    Public Shared Function GetZipDirectory() As String
        Return System.Web.HttpContext.Current.Server.MapPath("~/Zips")
    End Function

    Public Shared Function GetZipFileName(ByVal name As String)
        Return GetZipDirectory() & "/" & name & ".zip"
    End Function

    Public Shared Function GetZipHttpFileName(ByVal name As String)
        Return "~/Zips/" & name & ".zip"
    End Function

    Private Shared Sub AddDirectory(ByVal zipfile As ZipFile, ByVal di As DirectoryInfo, ByVal path As String)
        For Each fi As FileInfo In di.GetFiles
            zipfile.AddFile(fi.FullName, path)
        Next

        For Each sdi As DirectoryInfo In di.GetDirectories
            AddDirectory(zipfile, sdi, path & "/" & sdi.Name)
        Next
    End Sub

    Public Shared Function ZipDirectory(ByVal dirname As String, ByVal path As String) As String
        Dim di As New DirectoryInfo(dirname)
        Dim diname As String = di.Name
        Dim zipfilename = GetZipFileName(diname)

        If File.Exists(zipfilename) Then
            File.Delete(zipfilename)
        End If

        Dim zipfile As New ZipFile(zipfilename)

        AddDirectory(zipfile, di, path)

        zipfile.Save()

        Return GetZipHttpFileName(diname)
    End Function

    Public Shared Sub UnzipOnDirectory(ByVal zipfilename As String, ByVal dirname As String)
        Dim zipfile As New ZipFile(zipfilename)

        zipfile.ExtractAll(dirname)
    End Sub
End Class
