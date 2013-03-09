Imports Microsoft.VisualBasic

Imports System.IO
Imports System.Collections.Generic

Public Class TechnologyService
    Public Function GetTechnologyDirectory(ByVal prjname As String) As DirectoryInfo
        Return New DirectoryInfo(Path.Combine(DirectoryService.GetWorkingDirectory(), "Projects/" & prjname & "/Technologies"))
    End Function

    Public Function GetTechnologies(ByVal prjname As String) As List(Of String)
        Dim di As DirectoryInfo = GetTechnologyDirectory(prjname)

        Dim technologies As New List(Of String)

        For Each fi As FileInfo In di.GetFiles("*.xml")
            technologies.Add(fi.Name.Substring(0, fi.Name.Length - 4))
        Next

        Return technologies
    End Function
End Class
