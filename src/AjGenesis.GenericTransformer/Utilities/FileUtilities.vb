Imports System.IO

Public Module FileUtilities
    Private filepaths As IList = New ArrayList

    Public Function GetFileName(ByVal filename As String) As String
        If File.Exists(filename) Then
            Return filename
        End If

        Dim pathfile As String
        Dim newfilename As String

        For Each pathfile In filepaths
            newfilename = Path.Combine(pathfile, filename)
            If File.Exists(newfilename) Then
                Return newfilename
            End If
        Next

        Return filename
    End Function

    Public Sub PushFilePath(ByVal filename As String)
        filepaths.Insert(0, New FileInfo(filename).DirectoryName)
    End Sub

    Public Sub PopFilePath()
        filepaths.RemoveAt(0)
    End Sub
End Module
