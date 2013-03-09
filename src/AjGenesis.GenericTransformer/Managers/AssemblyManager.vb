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
Imports AjGenesis.Core

Class AssemblyManager
    Public Sub Load(ByVal name As String)
        System.Reflection.Assembly.Load(name)
    End Sub

    Public Sub LoadFile(ByVal filename As String)
        System.Reflection.Assembly.LoadFile(filename)
    End Sub

    Public Sub LoadFrom(ByVal filename As String)
        System.Reflection.Assembly.LoadFrom(filename)
    End Sub

    Public Sub LoadWithPartialName(ByVal filename As String)
        System.Reflection.Assembly.LoadWithPartialName(filename)
    End Sub
End Class
