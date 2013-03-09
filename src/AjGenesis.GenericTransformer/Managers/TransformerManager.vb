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

Class TransformerManager
    Public Sub Transform(ByVal input As TextReader, ByVal output As TextWriter, ByVal env As Environment)
        Dim cmp As New TemplateCompiler(input)
        Dim pgm As Program = cmp.Compile
        pgm.Output = output
        pgm.Execute(env)
    End Sub

    Public Sub Transform(ByVal inputfile As String, ByVal outputfile As String, ByVal env As Environment)
        inputfile = GetFileName(inputfile)
        PushFilePath(inputfile)

        Try
            Dim input As New StreamReader(inputfile)
            Dim output As New StreamWriter(outputfile, False, System.Text.Encoding.Default)
            Transform(input, output, env)
            input.Close()
            output.Close()
        Finally
            PopFilePath()
        End Try
    End Sub

    Public Sub Transform(ByVal inputfile As String, ByVal outputfile As String, ByVal env As Environment, ByVal preservemark As String)
        inputfile = GetFileName(inputfile)
        PushFilePath(inputfile)

        Try
            If String.IsNullOrEmpty(preservemark) Then
                Transform(inputfile, outputfile, env)
                Return
            End If

            If Not File.Exists(outputfile) Then
                Transform(inputfile, outputfile, env)
                Return
            End If

            Dim tempfile As String = outputfile & ".tmp"

            Transform(inputfile, tempfile, env)

            Dim mergefile As String = outputfile & ".merge"

            Dim merger As New FileMerger

            merger.Merge(tempfile, outputfile, mergefile, preservemark)

            File.Delete(tempfile)
            File.Delete(outputfile)

            File.Move(mergefile, outputfile)
        Finally
            PopFilePath()
        End Try
    End Sub
End Class
