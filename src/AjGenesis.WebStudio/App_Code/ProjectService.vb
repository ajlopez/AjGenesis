Imports Microsoft.VisualBasic

Imports System.IO
Imports System.Collections.Generic

Public Class ProjectService
    Public Function GetProjectDirectory() As DirectoryInfo
        Return New DirectoryInfo(Path.Combine(DirectoryService.GetWorkingDirectory(), "Projects"))
    End Function

    Public Function GetProjects() As List(Of String)
        Dim di As DirectoryInfo = GetProjectDirectory()

        Dim projects As New List(Of String)

        For Each sdi As DirectoryInfo In di.GetDirectories
            projects.Add(sdi.Name)
        Next

        Return projects
    End Function
End Class
