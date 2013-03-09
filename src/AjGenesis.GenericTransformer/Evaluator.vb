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

Imports System.Reflection
Imports System.Text.RegularExpressions

Imports AjGenesis.Core

Public Class Evaluator
    Private mEnvironment As New Environment()

    Property Environment() As Environment
        Get
            Return mEnvironment
        End Get
        Set(ByVal Value As Environment)
            mEnvironment = Value
        End Set
    End Property

    Default Property Item(ByVal key As String) As Object
        Get
            If Not mEnvironment Is Nothing Then
                Return mEnvironment(key)
            End If

            Return Nothing
        End Get
        Set(ByVal Value As Object)
            mEnvironment(key) = Value
        End Set
    End Property

    Public Sub PushEnvironment(ByVal env As Environment)
        env.Parent = mEnvironment
        mEnvironment = env
    End Sub

    Public Sub PopEnvironment()
        mEnvironment = mEnvironment.Parent
    End Sub

    Public Function Evaluate(ByVal expression As String) As Object
        Dim comp As New Compiler(expression)

        Return comp.Compile().Evaluate(mEnvironment)
        'Return Eval(comp.Compile())
    End Function

End Class
