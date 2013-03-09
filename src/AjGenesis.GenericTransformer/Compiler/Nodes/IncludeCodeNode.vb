Imports System.IO

Imports AjGenesis.Core

Public Class IncludeCodeNode
    Inherits CommandNode

    Dim mExpression As ExpressionNode
    Dim mProgram As Program

    Public Property Expression() As ExpressionNode
        Get
            Return mExpression
        End Get
        Set(ByVal Value As ExpressionNode)
            mExpression = Value
        End Set
    End Property

    Overrides Sub Execute(ByVal env As Environment)
        If mProgram Is Nothing Then
            Dim filename As String
            filename = mExpression.Evaluate(env).ToString()
            filename = GetFileName(filename)
            Try
                PushFilePath(filename)
                mProgram = New Program(env)
                Dim cmp As New TextCompiler(New StreamReader(filename))
                cmp.Compile(mProgram)
            Finally
                PopFilePath()
            End Try
        End If

        mProgram.Execute()
    End Sub
End Class
