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

Imports AjGenesis
Imports AjGenesis.Transformers.GenericTransformer

Imports NUnit.Framework

<TestFixture()> Public Class TestTemplateCompiler
    Private Function ExecuteProgram(ByVal pgm As Program) As String
        Dim output As StringWriter = New StringWriter()
        pgm.Output = output
        pgm.Execute()
        output.Close()
        Return output.ToString
    End Function

    Private Function ExecuteProgramFromFile(ByVal fname As String) As String
        Dim cmp As New TemplateCompiler(New StreamReader(fname))
        Dim pgm As Program = cmp.Compile
        Dim output As StringWriter = New StringWriter()
        pgm.Output = output
        pgm.Execute()
        output.Close()
        Return output.ToString
    End Function

    <Test()> Sub TestCompiler01()
        Dim text As String = "<# print 1 #>"
        Dim cmp As New TemplateCompiler(text)
        Dim pgm As Program = cmp.Compile()
        Assert.AreEqual("1", ExecuteProgram(pgm))
    End Sub

    <Test()> Sub TestCompiler02()
        Dim text As String = "<# print 1" & vbCrLf & "print 2 #>"
        Dim cmp As New TemplateCompiler(text)
        Dim pgm As Program = cmp.Compile()
        Assert.AreEqual("12", ExecuteProgram(pgm))
    End Sub

    <Test()> Sub TestCompiler03()
        Dim text As String = "1"
        Dim cmp As New TemplateCompiler(text)
        Dim pgm As Program = cmp.Compile()
        Assert.AreEqual(text & vbCrLf, ExecuteProgram(pgm))
    End Sub

    <Test()> Sub TestCompiler04()
        Dim text As String = "Hola" & vbCrLf & "Mundo"
        Dim cmp As New TemplateCompiler(text)
        Dim pgm As Program = cmp.Compile()
        Assert.AreEqual(text & vbCrLf, ExecuteProgram(pgm))
    End Sub


    <Test()> Sub TestCompiler05()
        Dim text As String = "Hello" & vbCrLf & "<# print ""World"" #>"
        Dim cmp As New TemplateCompiler(text)
        Dim pgm As Program = cmp.Compile()
        Assert.AreEqual("Hello" & vbCrLf & "World", ExecuteProgram(pgm))
    End Sub

    <Test()> Sub TestCompiler06()
        Assert.AreEqual("1" & vbCrLf, ExecuteProgramFromFile("TestTemplates\Template1.txt"))
    End Sub

    <Test()> Sub TestCompiler07()
        Assert.AreEqual("1", ExecuteProgramFromFile("TestTemplates\Template2.txt"))
    End Sub

    <Test()> Sub TestCompiler08()
        Assert.AreEqual("1", ExecuteProgramFromFile("TestTemplates\Template3.txt"))
    End Sub

    <Test()> Sub TestCompiler09()
        Assert.AreEqual("24", ExecuteProgramFromFile("TestTemplates\Template4.txt"))
    End Sub

    <Test()> Sub TestCompiler10()
        Assert.AreEqual("24", ExecuteProgramFromFile("TestTemplates\Template5.txt"))
    End Sub

    <Test()> Sub TestIf01()
        Dim text As String = "<# if 1 then" & vbCrLf & "print 1" & vbCrLf & "end if #>"
        Dim cmp As New TemplateCompiler(text)
        Dim pgm As Program = cmp.Compile()
        Assert.AreEqual("1", ExecuteProgram(pgm))
    End Sub

    <Test()> Sub TestIf02()
        Dim text As String = "<# if 1 then" & vbCrLf & "print 1" & vbCrLf & "else" & vbCrLf & "print 2" & vbCrLf & "end if #>"
        Dim cmp As New TemplateCompiler(text)
        Dim pgm As Program = cmp.Compile()
        Assert.AreEqual("1", ExecuteProgram(pgm))
    End Sub

    <Test()> Sub TestIf03()
        Dim text As String = "<# if 0 then" & vbCrLf & "print 1" & vbCrLf & "else" & vbCrLf & "print 2" & vbCrLf & "end if #>"
        Dim cmp As New TemplateCompiler(text)
        Dim pgm As Program = cmp.Compile()
        Assert.AreEqual("2", ExecuteProgram(pgm))
    End Sub
End Class
