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

Public Class Processor
    Private mOutput As TextWriter
    Private mLogOutput As TextWriter

    Public Property Output() As TextWriter
        Get
            Return mOutput
        End Get
        Set(ByVal Value As TextWriter)
            mOutput = Value
        End Set
    End Property

    Public Property LogOutput() As TextWriter
        Get
            Return mLogOutput
        End Get
        Set(ByVal Value As TextWriter)
            mLogOutput = Value
        End Set
    End Property

    Public Sub Print(ByVal text As String)
        If Not Output Is Nothing Then
            Output.Write(text)
        End If
    End Sub

    Public Sub PrintLine(ByVal text As String)
        If Not Output Is Nothing Then
            Output.Write(text)
        End If
    End Sub

    Public Sub Log(ByVal text As String)
        If Not LogOutput Is Nothing Then
            LogOutput.WriteLine(text)
        Else
            Console.WriteLine(text)
        End If
    End Sub

    Public Sub Debug(ByVal text As String)
        If Not LogOutput Is Nothing Then
            LogOutput.WriteLine(text)
        Else
            Console.WriteLine(text)
        End If
    End Sub
End Class
