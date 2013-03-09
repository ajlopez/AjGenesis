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

Public Class Program
    Private mCommands As New CommandListNode()
    Private mEnvironment As Environment

    Public Sub New()
        mEnvironment = New Environment()
    End Sub

    Public Sub New(ByVal env As Environment)
        mEnvironment = env
    End Sub

    Public Property Output() As TextWriter
        Get
            Return mEnvironment.Processor.Output
        End Get
        Set(ByVal Value As TextWriter)
            mEnvironment.Processor.Output = Value
        End Set
    End Property

    Public Property LogOutput() As TextWriter
        Get
            Return mEnvironment.Processor.LogOutput
        End Get
        Set(ByVal Value As TextWriter)
            mEnvironment.Processor.LogOutput = Value
        End Set
    End Property

    Public Sub AddCommand(ByVal cmd As CommandNode)
        mCommands.AddCommand(cmd)
    End Sub

    Public Sub AddFunction(ByVal f As FunctionNode)
        mEnvironment.MethodItem(f.Name) = f
    End Sub

    Public Sub AddSubroutine(ByVal s As SubNode)
        mEnvironment.MethodItem(s.Name) = s
    End Sub

    Public Sub Execute()
        mCommands.Execute(mEnvironment)
    End Sub

    Public Sub Execute(ByVal env As Environment)
        If Not mEnvironment Is env Then
            mEnvironment.Parent = env
        End If
        Try
            mCommands.Execute(mEnvironment)
        Catch ex As ErrorException
            mEnvironment.Processor.PrintLine(ex.Message)
        End Try

        mEnvironment.Parent = Nothing
    End Sub
End Class
