Imports Microsoft.VisualBasic

Imports System.IO
Imports System.Collections.Generic

Public Class DirectoryData
    Private mDirectory As String
    Private mName As String
    Private mIsFile As Boolean

    Public Property Name() As String
        Get
            Return mName
        End Get
        Set(ByVal value As String)
            mName = value
        End Set
    End Property

    Public Property Directory() As String
        Get
            Return mDirectory
        End Get
        Set(ByVal value As String)
            mDirectory = value
        End Set
    End Property

    Public Property IsFile() As Boolean
        Get
            Return mIsFile
        End Get
        Set(ByVal value As Boolean)
            mIsFile = value
        End Set
    End Property

    Public ReadOnly Property FullName() As String
        Get
            Return mDirectory & "/" & mName
        End Get
    End Property
End Class

Public Class DirectoryService
    Public Shared Function DirectoriesAreEqual(ByVal dir1 As String, ByVal dir2 As String) As Boolean
        If dir1.Length > 0 AndAlso (dir1.EndsWith("\") Or dir1.EndsWith("/")) Then
            dir1 = Left(dir1, dir1.Length - 1)
        End If

        If dir2.Length > 0 AndAlso (dir2.EndsWith("\") Or dir2.EndsWith("/")) Then
            dir2 = Left(dir2, dir2.Length - 1)
        End If

        If Path.GetFullPath(dir1) = Path.GetFullPath(dir2) Then
            Return True
        End If

        Return False
    End Function

    Public Shared Function IsWorkingDirectory(ByVal dir As String) As Boolean
        Return DirectoriesAreEqual(GetWorkingDirectory, dir)
    End Function

    Public Shared Function IsProjectsDirectory(ByVal dir As String) As Boolean
        Return DirectoriesAreEqual(GetProjectsDirectory, dir)
    End Function

    Public Shared Function IsTasksDirectory(ByVal dir As String) As Boolean
        Return DirectoriesAreEqual(GetTasksDirectory, dir)
    End Function

    Public Shared Function IsBuildsDirectory(ByVal dir As String) As Boolean
        Return DirectoriesAreEqual(GetBuildsDirectory, dir)
    End Function

    Public Shared Function IsTemplatesDirectory(ByVal dir As String) As Boolean
        Return DirectoriesAreEqual(GetTemplatesDirectory, dir)
    End Function

    Public Shared Sub SetWorkingDirectory(ByVal newpath As String)
        If newpath.StartsWith(".") Then
            newpath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/"), newpath)
            newpath = Path.GetFullPath(newpath)
        End If

        If Not newpath.EndsWith("/") And Not newpath.EndsWith("\") Then
            newpath &= "/"
        End If

        Dim di As New DirectoryInfo(newpath)

        If Not di.Parent.Exists() Then
            Throw New InvalidOperationException(String.Format("Parent Directory {0} doesn() 't exist", di.Parent.FullName))
        End If

        System.Web.HttpContext.Current.Session("WorkingDirectory") = newpath

        If Not Directory.Exists(newpath) Then
            Directory.CreateDirectory(newpath)
        End If

        Dim auxpath As String

        auxpath = GetProjectsDirectory()

        If Not Directory.Exists(auxpath) Then
            Directory.CreateDirectory(auxpath)
        End If

        auxpath = GetBuildsDirectory()

        If Not Directory.Exists(auxpath) Then
            Directory.CreateDirectory(auxpath)
        End If

        auxpath = GetTemplatesDirectory()

        If Not Directory.Exists(auxpath) Then
            Directory.CreateDirectory(auxpath)
        End If

        auxpath = GetTasksDirectory()

        If Not Directory.Exists(auxpath) Then
            Directory.CreateDirectory(auxpath)
        End If
    End Sub

    Public Shared Function GetDefaultWorkingDirectory() As String
        Dim wd As String
        wd = System.Configuration.ConfigurationManager.AppSettings("AjGenesisDirectory")

        If wd.StartsWith(".") Then
            wd = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/"), wd)
            wd = Path.GetFullPath(wd)
        End If

        If Not wd.EndsWith("/") And Not wd.EndsWith("\") Then
            wd &= "/"
        End If

        Return wd
    End Function

    Public Shared Function GetWorkingDirectory() As String
        If Not System.Web.HttpContext.Current.Session("WorkingDirectory") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("WorkingDirectory")
        End If

        Return GetDefaultWorkingDirectory()
    End Function

    Public Shared Sub ResetWorkingDirectory()
        SetWorkingDirectory(GetDefaultWorkingDirectory)
    End Sub

    Public Shared Function GetPartialDirectory(ByVal path As String)
        If IsWorkingDirectory(path) Then
            Return ""
        End If

        Dim fullpath As String = System.IO.Path.GetFullPath(path)
        Dim fullwork As String = System.IO.Path.GetFullPath(GetWorkingDirectory)
        Return fullpath.Substring(fullwork.Length)
    End Function

    Public Shared Function GetDirectoryData(ByVal path As String) As List(Of DirectoryData)
        Dim di As New DirectoryInfo(path)
        Dim data As New List(Of DirectoryData)

        For Each sdi As DirectoryInfo In di.GetDirectories()
            Dim dd As New DirectoryData
            dd.Name = sdi.Name
            dd.Directory = path
            dd.IsFile = False

            data.Add(dd)
        Next

        For Each fi As FileInfo In di.GetFiles()
            Dim fd As New DirectoryData
            fd.Name = fi.Name
            fd.Directory = path
            fd.IsFile = True

            data.Add(fd)
        Next

        Return data
    End Function

    Public Shared Function GetProjectsDirectory() As String
        Return GetWorkingDirectory() & "Projects"
    End Function

    Public Shared Function GetTasksDirectory() As String
        Return GetWorkingDirectory() & "Tasks"
    End Function

    Public Shared Function GetBuildsDirectory() As String
        Return GetWorkingDirectory() & "Build"
    End Function

    Public Shared Function GetTemplatesDirectory() As String
        Return GetWorkingDirectory() & "/Templates"
    End Function

    Public Shared Sub MoveContent(ByVal sourcedir As String, ByVal targetdir As String)
        Dim di As New DirectoryInfo(sourcedir)

        For Each fi As FileInfo In di.GetFiles
            fi.MoveTo(targetdir & "/" & fi.Name)
        Next

        For Each sdi As DirectoryInfo In di.GetDirectories
            sdi.MoveTo(targetdir & "/" & sdi.Name)
        Next
    End Sub

    Public Shared Sub CopyContent(ByVal sourcedir As String, ByVal targetdir As String)
        Dim di As New DirectoryInfo(sourcedir)

        For Each fi As FileInfo In di.GetFiles
            fi.CopyTo(targetdir & "/" & fi.Name, True)
        Next

        For Each sdi As DirectoryInfo In di.GetDirectories
            Directory.CreateDirectory(targetdir & "/" & sdi.Name)
            CopyContent(sourcedir & "/" & sdi.Name, targetdir & "/" & sdi.Name)
        Next
    End Sub

    Public Shared Sub MoveFile(ByVal filename As String, ByVal targetfile As String)
        Dim fi As New FileInfo(filename)
        fi.MoveTo(targetfile)
    End Sub

    Public Shared Sub CopyFile(ByVal filename As String, ByVal targetfile As String)
        Dim fi As New FileInfo(filename)
        fi.CopyTo(targetfile)
    End Sub

    Public Shared Sub MoveDirectory(ByVal sourcedir As String, ByVal targetdir As String)
        Dim di As New DirectoryInfo(sourcedir)
        di.MoveTo(targetdir)
    End Sub

    Public Shared Sub CopyDirectory(ByVal sourcedir As String, ByVal targetdir As String)
        Dim di As New DirectoryInfo(sourcedir)
        Directory.CreateDirectory(targetdir)
        CopyContent(sourcedir, targetdir)
    End Sub
End Class
