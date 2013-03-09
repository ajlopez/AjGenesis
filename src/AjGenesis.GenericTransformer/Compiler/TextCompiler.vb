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

Public Class TextCompiler
    Private mInput As TextReader
    Private mLastLine As String

    Public Sub New(ByVal input As TextReader)
        mInput = input
    End Sub

    Public Sub New(ByVal input As String)
        mInput = New StringReader(input)
    End Sub

    Protected Overridable Function NextLine() As String
        mLastLine = mInput.ReadLine
        Return mLastLine
    End Function

    Protected Overridable Function CompileLine(ByVal line As String) As CommandNode
        Dim cmp As New Compiler(line)
        Return cmp.CompileSingleCommand
    End Function

    Private Function CompileBlock(ByRef lastcmd As EndNode) As CommandListNode
        Dim cmds As CommandListNode = New CommandListNode()

        Dim line As String
        Dim cmd As CommandNode = Nothing

        line = NextLine()

        While Not line Is Nothing
            cmd = CompileCommand(line)

            If TypeOf cmd Is EndNode Then
                Exit While
            Else
                cmds.AddCommand(cmd)
            End If

            line = NextLine()
        End While

        lastcmd = cmd

        Return cmds
    End Function

    Private Function CompileIf(ByVal ifcmd As IfNode) As CommandNode
        Dim lastcmd As EndNode = Nothing

        ifcmd.ThenCommand = CompileBlock(lastcmd)

        If Not lastcmd Is Nothing AndAlso lastcmd.Word = "elseif" Then
            ifcmd.ElseCommand = CompileElseIf(DirectCast(lastcmd, ElseIfNode))
        ElseIf Not lastcmd Is Nothing AndAlso lastcmd.Word = "else" Then
            ifcmd.ElseCommand = CompileBlock(lastcmd)
        End If

        Return ifcmd
    End Function

    Private Function CompileTryCatch(ByVal trycmd As TryCatchNode) As CommandNode
        Dim lastcmd As EndNode = Nothing

        trycmd.TryCommand = CompileBlock(lastcmd)

        If Not lastcmd Is Nothing AndAlso lastcmd.Word = "catch" Then
            trycmd.CatchCommand = CompileCatch(DirectCast(lastcmd, CatchNode))
        End If

        Return trycmd
    End Function

    Private Function CompileElseIf(ByVal elseifcmd As ElseIfNode) As CommandNode
        Dim lastcmd As EndNode = Nothing

        elseifcmd.ThenCommand = CompileBlock(lastcmd)

        If Not lastcmd Is Nothing AndAlso lastcmd.Word = "elseif" Then
            elseifcmd.ElseCommand = CompileElseIf(DirectCast(lastcmd, ElseIfNode))
        ElseIf Not lastcmd Is Nothing AndAlso lastcmd.Word = "else" Then
            elseifcmd.ElseCommand = CompileBlock(lastcmd)
        End If

        Return elseifcmd
    End Function

    Private Function CompileCatch(ByVal catchcmd As CatchNode) As CommandNode
        Dim lastcmd As EndNode = Nothing

        catchcmd.CatchCommand = CompileBlock(lastcmd)

        Return catchcmd
    End Function

    Private Function CompileWhile(ByVal cmdwhile As WhileNode) As CommandNode
        Dim lastcmd As EndNode = Nothing

        cmdwhile.Commands = CompileBlock(lastcmd)

        If lastcmd Is Nothing OrElse lastcmd.Word <> "while" Then
            Throw New CompilerException("Se esperaba fin de while")
        End If

        Return cmdwhile
    End Function

    Private Function CompileForEach(ByVal cmdforeach As ForEachNode) As CommandNode
        Dim lastcmd As EndNode = Nothing

        cmdforeach.Commands = CompileBlock(lastcmd)

        If lastcmd Is Nothing OrElse lastcmd.Word <> "for" Then
            Throw New CompilerException("Se esperaba fin de for each")
        End If

        Return cmdforeach
    End Function

    Private Function CompileFunction(ByVal funcmd As FunctionNode) As CommandNode
        Dim lastcmd As EndNode = Nothing

        funcmd.Commands = CompileBlock(lastcmd)

        If lastcmd Is Nothing OrElse lastcmd.Word <> "function" Then
            Throw New CompilerException("Se esperaba fin de función")
        End If

        Return funcmd
    End Function

    Private Function CompileSub(ByVal subcmd As SubNode) As CommandNode
        Dim lastcmd As EndNode = Nothing

        subcmd.Commands = CompileBlock(lastcmd)

        If lastcmd Is Nothing OrElse lastcmd.Word <> "sub" Then
            Throw New CompilerException("Se esperaba fin de rutina")
        End If

        Return subcmd
    End Function

    Function CompileCommand(ByVal line As String) As CommandNode
        If line Is Nothing Then
            Return Nothing
        End If

        Dim cmd As CommandNode
        cmd = CompileLine(line)

        If TypeOf cmd Is IfNode Then
            Return CompileIf(cmd)
        End If

        If TypeOf cmd Is TryCatchNode Then
            Return CompileTryCatch(cmd)
        End If

        If TypeOf cmd Is WhileNode Then
            Return CompileWhile(cmd)
        End If

        If TypeOf cmd Is ForEachNode Then
            Return CompileForEach(cmd)
        End If

        If TypeOf cmd Is FunctionNode Then
            Return CompileFunction(cmd)
        End If

        If TypeOf cmd Is SubNode Then
            Return CompileSub(cmd)
        End If

        Return cmd
    End Function

    Public Sub Compile(ByVal pgm As Program)
        Try
            Dim line As String

            line = NextLine()

            While Not line Is Nothing
                Dim cmd As CommandNode = CompileCommand(line)
                If TypeOf cmd Is IfNode Then
                    pgm.AddCommand(cmd)
                ElseIf TypeOf cmd Is FunctionNode Then
                    pgm.AddFunction(cmd)
                ElseIf TypeOf cmd Is SubNode Then
                    pgm.AddSubroutine(cmd)
                Else
                    pgm.AddCommand(cmd)
                End If

                line = NextLine()
            End While
        Catch ex As CompilerException
            Throw New CompilerException(ex.Message & ":" & vbCrLf & mLastLine)
        End Try
    End Sub

    Public Function Compile() As Program
        Dim pgm As Program = New Program

        Compile(pgm)

        Return pgm
    End Function
End Class
