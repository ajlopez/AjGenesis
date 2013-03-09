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
Imports AjGenesis.Core
Imports AjGenesis.Transformers.GenericTransformer

Imports NUnit.Framework

<TestFixture()> Public Class TestTextCompiler
    Private Function ExecuteProgram(ByVal pgm As Program) As String
        Dim output As StringWriter = New StringWriter()
        pgm.Output = output
        pgm.Execute()
        output.Close()
        Return output.ToString
    End Function

    Private Function ExecuteProgram(ByVal pgm As Program, ByVal env As Environment) As String
        Dim output As StringWriter = New StringWriter
        pgm.Output = output
        pgm.Execute(env)
        output.Close()
        Return output.ToString
    End Function

    Private Function ExecuteProgram(ByVal txt As String) As String
        Dim cmp As New TextCompiler(txt)
        Dim pgm As Program = cmp.Compile()
        Return ExecuteProgram(pgm)
    End Function

    <Test()> Sub TestCompiler01()
        Dim text As String = "print 1"
        Dim cmp As New TextCompiler(text)
        Dim pgm As Program = cmp.Compile()
        Assert.AreEqual("1", ExecuteProgram(pgm))
    End Sub

    <Test()> Sub TestCompiler02()
        Dim text As String = "print 1" & vbCrLf & "print 2"
        Dim cmp As New TextCompiler(text)
        Dim pgm As Program = cmp.Compile()
        Assert.AreEqual("12", ExecuteProgram(pgm))
    End Sub

    <Test()> Sub TestCompiler03()
        Dim text As String = "a=1" & vbCrLf & "print a" & vbCrLf & "b=2" & vbCrLf & "print b"
        Dim cmp As New TextCompiler(text)
        Dim pgm As Program = cmp.Compile()
        Assert.AreEqual("12", ExecuteProgram(pgm))
    End Sub

    <Test()> Sub TestCompiler04()
        Dim text As String = "print p.FirstName"
        Dim cmp As New TextCompiler(text)
        Dim pgm As Program = cmp.Compile()
        Dim env As New Environment
        Dim p As New Person

        p.FirstName = "Adam"

        env("p") = p
        Assert.AreEqual("Adam", ExecuteProgram(pgm, env))
    End Sub

    <Test()> Sub TestCompiler05()
        Dim text As String = "p.FirstName=""Abel""" & vbCrLf & "print p.FirstName"
        Dim cmp As New TextCompiler(text)
        Dim pgm As Program = cmp.Compile()
        Dim env As New Environment
        Dim p As New Person

        p.FirstName = "Adam"

        env("p") = p
        Assert.AreEqual("Abel", ExecuteProgram(pgm, env))
    End Sub

    <Test()> Sub TestIf01()
        Dim text As String = "if 1 then" & vbCrLf & "print 1" & vbCrLf & "end if"
        Dim cmp As New TextCompiler(text)
        Dim pgm As Program = cmp.Compile()
        Assert.AreEqual("1", ExecuteProgram(pgm))
    End Sub

    <Test()> Sub TestIf02()
        Dim text As String = "if 1 then" & vbCrLf & "print 1" & vbCrLf & "else" & vbCrLf & "print 2" & vbCrLf & "end if"
        Dim cmp As New TextCompiler(text)
        Dim pgm As Program = cmp.Compile()
        Assert.AreEqual("1", ExecuteProgram(pgm))
    End Sub

    <Test()> Sub TestIf03()
        Dim text As String = "if 0 then" & vbCrLf & "print 1" & vbCrLf & "else" & vbCrLf & "print 2" & vbCrLf & "end if"
        Dim cmp As New TextCompiler(text)
        Dim pgm As Program = cmp.Compile()
        Assert.AreEqual("2", ExecuteProgram(pgm))
    End Sub

    <Test()> Sub TestIf04()
        Dim text As String = "if 0>1 then" & vbCrLf & "print 1" & vbCrLf & "else" & vbCrLf & "print 2" & vbCrLf & "end if"
        Dim cmp As New TextCompiler(text)
        Dim pgm As Program = cmp.Compile()
        Assert.AreEqual("2", ExecuteProgram(pgm))
    End Sub

    <Test()> Sub TestIf05()
        Dim text As String = "if 0<1 then" & vbCrLf & "print 1" & vbCrLf & "else" & vbCrLf & "print 2" & vbCrLf & "end if"
        Dim cmp As New TextCompiler(text)
        Dim pgm As Program = cmp.Compile()
        Assert.AreEqual("1", ExecuteProgram(pgm))
    End Sub

    <Test()> Sub TestIf06()
        Dim text As String = "if 0>1 or 0>2 then" & vbCrLf & "print 1" & vbCrLf & "else" & vbCrLf & "print 2" & vbCrLf & "end if"
        Dim cmp As New TextCompiler(text)
        Dim pgm As Program = cmp.Compile()
        Assert.AreEqual("2", ExecuteProgram(pgm))
    End Sub

    <Test()> Sub TestIf07()
        Dim text As String = "if 0<1 and 0<2 then" & vbCrLf & "print 1" & vbCrLf & "else" & vbCrLf & "print 2" & vbCrLf & "end if"
        Dim cmp As New TextCompiler(text)
        Dim pgm As Program = cmp.Compile()
        Assert.AreEqual("1", ExecuteProgram(pgm))
    End Sub

    <Test()> Sub TestIf08()
        Dim text As String = "if not 0>1 then" & vbCrLf & "print 1" & vbCrLf & "else" & vbCrLf & "print 2" & vbCrLf & "end if"
        Dim cmp As New TextCompiler(text)
        Dim pgm As Program = cmp.Compile()
        Assert.AreEqual("1", ExecuteProgram(pgm))
    End Sub

    <Test()> Sub TestIf09()
        Dim text As String = "if not 0>1 or false then" & vbCrLf & "print 1" & vbCrLf & "else" & vbCrLf & "print 2" & vbCrLf & "end if"
        Dim cmp As New TextCompiler(text)
        Dim pgm As Program = cmp.Compile()
        Assert.AreEqual("1", ExecuteProgram(pgm))
    End Sub

    <Test()> Sub TestIf10()
        Dim text As String = "if 0 then" & vbCrLf & "print 1" & vbCrLf & "elseif 1 then" & vbCrLf & "print 2" & vbCrLf & "end if"
        Dim cmp As New TextCompiler(text)
        Dim pgm As Program = cmp.Compile()
        Assert.AreEqual("2", ExecuteProgram(pgm))
    End Sub

    <Test()> Sub TestIf11()
        Dim text As String = "if 0 then" & vbCrLf & "print 1" & vbCrLf & "elseif false then" & vbCrLf & "print 2" & vbCrLf & "else" & vbCrLf & "print 3" & vbCrLf & "end if"
        Dim cmp As New TextCompiler(text)
        Dim pgm As Program = cmp.Compile()
        Assert.AreEqual("3", ExecuteProgram(pgm))
    End Sub
    <Test()> Sub TestFunction01()
        Dim text As String = "function f()" & vbCrLf & "return 1" & vbCrLf & "end function"
        Dim cmp As New TextCompiler(text)
        cmp.Compile()
    End Sub

    <Test()> Sub TestFunction02()
        Dim text As String = "function f(a)" & vbCrLf & "print a" & vbCrLf & "end function"
        Dim cmp As New TextCompiler(text)
        cmp.Compile()
    End Sub

    <Test()> Sub TestFunction03()
        Dim text As String = "function f()" & vbCrLf & "return 1" & vbCrLf & "end function" & vbCrLf & "print f()"
        Assert.AreEqual("1", ExecuteProgram(text))
    End Sub

    <Test()> Sub TestFunction04()
        Dim text As String = "function f(a,b)" & vbCrLf & "return a+b" & vbCrLf & "end function" & vbCrLf & "print f(1,2)"
        Assert.AreEqual("3", ExecuteProgram(text))
    End Sub

    <Test()> Sub TestSubroutine01()
        Dim text As String = "sub f()" & vbCrLf & "print 1" & vbCrLf & "end sub"
        Dim cmp As New TextCompiler(text)
        cmp.Compile()
    End Sub

    <Test()> Sub TestSubroutine02()
        Dim text As String = "sub f(a,b)" & vbCrLf & "print a+b" & vbCrLf & "end sub"
        Dim cmp As New TextCompiler(text)
        cmp.Compile()
    End Sub

    <Test()> Sub TestSubroutine03()
        Dim text As String = "sub f()" & vbCrLf & "print 1" & vbCrLf & "end sub" & vbCrLf & "f()"
        Assert.AreEqual("1", ExecuteProgram(text))
    End Sub

    <Test()> Sub TestSubroutine04()
        Dim text As String = "sub f(a,b)" & vbCrLf & "print a+b" & vbCrLf & "end sub" & vbCrLf & "f(1,2)"
        Assert.AreEqual("3", ExecuteProgram(text))
    End Sub

    <Test()> _
    Sub CompileTryCatchCommand()
        Dim result As String = ExecuteProgram("Try" & vbCrLf & _
                                                "Throw ""We have a problem""" & vbCrLf & _
                                                "Catch ex" & vbCrLf & _
                                                "Print ""error""" & vbCrLf & _
                                                "End Try")
        Assert.IsNotNull(result)
        Assert.AreEqual("error", result)
    End Sub
End Class
