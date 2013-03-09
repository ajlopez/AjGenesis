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

Imports System
Imports System.IO

Imports AjGenesis.Core
Imports AjGenesis.Models.DynamicModel
Imports AjGenesis.Transformers.GenericTransformer

Module AjGenesisConsole
    Dim env As New TopEnvironment
    Dim obj As New AjGenesis.Models.DynamicModel.DynamicObject
    Dim templatefile As String

    Sub ProcessArgument(ByVal arg As String)
        If Not templatefile Is Nothing Then
            Dim input As New StreamReader(templatefile)
            Dim output As New StreamWriter(arg, False, System.Text.Encoding.Default)
            Dim cmp As New TemplateCompiler(input)
            Dim pgm As Program = cmp.Compile
            pgm.Output = output
            pgm.LogOutput = System.Console.Out
            pgm.Execute(env)

            input.Close()
            output.Close()

            templatefile = Nothing
            Return
        End If

        If arg.StartsWith("-D") Then
            arg = arg.Substring(2)
            Dim pos As Integer
            pos = arg.IndexOf("=")

            Dim name As String = arg.Substring(0, pos)
            Dim value As String = arg.Substring(pos + 1)

            env(name) = value
        ElseIf arg.EndsWith(".xml") Then
            Dim builder As New ObjectXmlBuilder
            Dim obj As IObject

            obj = builder.GetObject(arg)

            If obj.GetValue("Id") Is Nothing Then
                env(obj.GetValue("TypeName")) = obj
            Else
                env(obj.GetValue("Id")) = obj
            End If

            Return
        ElseIf arg.EndsWith(".txt") Then
            Dim builder As New ObjectTextBuilder
            Dim obj As IObject

            obj = builder.GetObjectFromFile(arg)

            If obj.GetValue("Id") Is Nothing Then
                env(obj.GetValue("TypeName")) = obj
            Else
                env(obj.GetValue("Id")) = obj
            End If

            Return
        ElseIf arg.EndsWith(".ajg") Then
            Dim taskfile As New StreamReader(arg)
            Dim comp As New TextCompiler(taskfile)
            Dim pgm As Program

            pgm = comp.Compile()
            pgm.Output = System.Console.Out
            pgm.LogOutput = System.Console.Out
            pgm.Execute(env)

            taskfile.Close()

            Return
        ElseIf arg.EndsWith(".tpl") Then
            templatefile = arg

            Return
        Else
            Throw New Exception("Invalid Argument: " & arg)
        End If
    End Sub

    Sub Main(ByVal args As String())
        Dim arg As String

        '        Try
        For Each arg In args
            ProcessArgument(arg)
        Next
        '        Catch ex As Exception
        '       System.Console.WriteLine(ex.Message)
        '      End Try
    End Sub
End Module
