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

Public Class Transformer
    Implements ITransformer

    Public Sub Transform(ByVal input As TextReader, ByVal output As TextWriter, ByVal env As Environment) Implements ITransformer.Transform
        Dim cmp As New TemplateCompiler(input)
        Dim pgm As Program = cmp.Compile
        pgm.Output = output
        pgm.Execute(env)
    End Sub

    Public Sub Transform(ByVal inputfile As String, ByVal outputfile As String, ByVal env As Environment) Implements ITransformer.Transform
        Dim input As New StreamReader(inputfile, System.Text.Encoding.Default)
        Dim output As New StreamWriter(outputfile, False, System.Text.Encoding.Default)
        Transform(input, output, env)
        input.Close()
        output.Close()
    End Sub
End Class
