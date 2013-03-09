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

Imports AjGenesis.Core

Public Class TopEnvironment
    Inherits Environment

    Public Sub New()
        Me.Item("TransformerManager") = New TransformerManager
        Me.Item("FileManager") = New FileManager
        Me.Item("AssemblyManager") = New AssemblyManager
        Me.Item("UIManager") = New UIManager
        Me.Item("ModelManager") = New ModelManager
        'Me.Item("DataManager") = New DataManager
    End Sub
End Class
