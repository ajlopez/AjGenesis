'
' +---------------------------------------------------------------------+
' | AjGenesis - Code and Artifacts Generator in .NET                    |
' +---------------------------------------------------------------------+
' | Copyright (c) 2003-2011 Angel J. Lopez. All rights reserved.        |
' | http://www.ajlopez.com                                              |
' | http://www.ajlopez.net                                              |
' +---------------------------------------------------------------------+
' | This source file is subject to the ajgenesis Software License,      |
' | Version 1.0, that is bundled with this package in the file LICENSE. |
' | If you did not receive a copy of this file, you may read it online  |
' | at http://www.ajlopez.net/ajgenesis/license.php.                    |
' +---------------------------------------------------------------------+
'
'

Imports System.IO

Public Class FileManager
    Public Sub CreateDirectory(ByVal name As String)
        Directory.CreateDirectory(name)
    End Sub

    Public Sub DeleteDirectory(ByVal name As String)
        Directory.Delete(name, True)
    End Sub

    Public Sub CopyDirectory(ByVal sourcedir As String, ByVal destinationdir As String)
        My.Computer.FileSystem.CopyDirectory(sourcedir, destinationdir, True)
    End Sub

    Public Sub MoveDirectory(ByVal sourcedir As String, ByVal destinationdir As String)
        My.Computer.FileSystem.MoveDirectory(sourcedir, destinationdir, True)
    End Sub

    Public Function DirectoryExists(ByVal name As String) As Boolean
        Return Directory.Exists(name)
    End Function

    Public Function FileExists(ByVal name As String) As Boolean
        Return File.Exists(name)
    End Function

    Public Sub CopyFile(ByVal source As String, ByVal target As String)
        File.Copy(source, target, True)
    End Sub

    Public Sub MoveFile(ByVal source As String, ByVal target As String)
        File.Move(source, target)
    End Sub

    Public Sub DeleteFile(ByVal filename As String)
        File.Delete(filename)
    End Sub
End Class
