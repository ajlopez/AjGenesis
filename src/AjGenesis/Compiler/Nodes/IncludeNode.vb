Imports System.IO

Public Class IncludeNode
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
            mProgram = New Program(env)
            Dim cmp As New TemplateCompiler(New StreamReader(filename))
            cmp.Compile(mProgram)
        End If

        mProgram.Execute()
    End Sub
End Class
