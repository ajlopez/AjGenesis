Imports Microsoft.VisualBasic

Imports System.IO

Imports AjGenesis.Core
Imports AjGenesis.Models.DynamicModel
Imports AjGenesis.Transformers.GenericTransformer

Public Class AjGenesisService
    Public Shared Function GenerateProject(ByVal projectname As String, ByVal projectdescription As String) As String
        Dim stroutput As New StringWriter()

        Try
            Dim env As New TopEnvironment

            Dim builder As New ObjectXmlBuilder

            env("WorkingDir") = DirectoryService.GetWorkingDirectory
            env("ProjectName") = projectname
            env("ProjectDescription") = projectdescription

            Dim taskfile As New StreamReader(Path.Combine(DirectoryService.GetWorkingDirectory(), "Tasks/CreateProject.ajg"))
            Dim comp As New TextCompiler(taskfile)
            Dim pgm As Program

            FileUtilities.PushFilePath(DirectoryService.GetWorkingDirectory)

            pgm = comp.Compile()
            taskfile.Close()
            pgm.Output = stroutput
            pgm.LogOutput = stroutput
            pgm.Execute(env)

            FileUtilities.PopFilePath()

            taskfile.Close()
        Catch ex As Exception
            stroutput.WriteLine()
            stroutput.WriteLine("Exception " & ex.GetType.FullName & ": " & ex.Message)
            stroutput.WriteLine(ex.StackTrace)
        End Try

        stroutput.Close()

        Return stroutput.ToString
    End Function

    Public Shared Function GenerateCode(ByVal project As String, ByVal technology As String) As String
        Dim stroutput As New StringWriter()

        Try

            Dim env As New TopEnvironment

            Dim builder As New ObjectXmlBuilder
            Dim obj As IObject

            env("WorkingDir") = DirectoryService.GetWorkingDirectory

            obj = builder.GetObject(Path.Combine(DirectoryService.GetWorkingDirectory(), "Projects/" & project & "/Project.xml"))

            If obj.GetValue("Id") Is Nothing Then
                env(obj.GetValue("TypeName")) = obj
            Else
                env(obj.GetValue("Id")) = obj
            End If

            Dim taskname = obj.GetValue("GenerateTask")

            If taskname Is Nothing Then
                taskname = "GenerateCode"
            End If

            obj = builder.GetObject(Path.Combine(DirectoryService.GetWorkingDirectory(), "Projects/" & project & "/Technologies/" & technology & ".xml"))

            If obj.GetValue("Id") Is Nothing Then
                env(obj.GetValue("TypeName")) = obj
            Else
                env(obj.GetValue("Id")) = obj
            End If

            Dim taskfile As New StreamReader(Path.Combine(DirectoryService.GetWorkingDirectory(), "Tasks/" & taskname & ".ajg"))
            Dim comp As New TextCompiler(taskfile)
            Dim pgm As Program

            FileUtilities.PushFilePath(DirectoryService.GetWorkingDirectory)

            pgm = comp.Compile()
            taskfile.Close()
            pgm.Output = stroutput
            pgm.LogOutput = stroutput
            pgm.Execute(env)

            FileUtilities.PopFilePath()

            taskfile.Close()
        Catch ex As Exception
            stroutput.WriteLine()
            stroutput.WriteLine("Exception " & ex.GetType.FullName & ": " & ex.Message)
            stroutput.WriteLine(ex.StackTrace)
        End Try

        stroutput.Close()

        Return stroutput.ToString
    End Function
End Class
