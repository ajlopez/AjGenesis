Imports System.IO

Public Class TemplateCompiler
    Inherits TextCompiler

    Private Const OPENCODE = "<#"
    Private Const CLOSECODE = "#>"

    Private mNextLine As String
    Private mInCode As Boolean

    Public Sub New(ByVal input As TextReader)
        MyBase.New(input)
    End Sub

    Public Sub New(ByVal input As String)
        MyBase.New(input)
    End Sub

    Protected Overrides Function NextLine() As String
        If mNextLine Is Nothing Then
            Return MyBase.NextLine
        End If

        Dim line As String = mNextLine
        mNextLine = Nothing

        Return line
    End Function

    Private Sub PutLine(ByVal line As String)
        If line Is Nothing Then
            Return
        End If
        If line = "" Then
            Return
        End If
        mNextLine = line
    End Sub

    Protected Overrides Function CompileLine(ByVal line As String) As CommandNode
        Dim p As Integer

        If mInCode Then
            p = InStr(line, CLOSECODE)
            If p > 0 Then
                PutLine(line.Substring(p - 1 + CLOSECODE.Length))
                line = Left(line, p - 1)
                mInCode = False
            End If

            If line = "" Then
                Return Nothing
            End If

            Dim cmp As New Compiler(line)
            Return cmp.CompileSingleCommand
        End If

        p = InStr(line, OPENCODE)

        If p > 0 Then
            PutLine(line.Substring(p - 1 + OPENCODE.Length))
            line = Left(line, p - 1)
            mInCode = True
        End If

        'If line = "" Then
        '    Return Nothing
        'End If

        If p <= 0 Then
            line = line & vbCrLf
        End If

        If line = "" Then
            Return Nothing
        End If

        Dim cmd As New PrintNode()
        cmd.Expression = New StringNode(line)

        Return cmd
    End Function
End Class
