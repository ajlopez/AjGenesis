Imports System.IO

Imports AjGenesis.Core

Public Class PrintLineNode
    Inherits CommandNode

    Private mExpression As ExpressionNode

    Public Property Expression() As ExpressionNode
        Get
            Return mExpression
        End Get
        Set(ByVal Value As ExpressionNode)
            mExpression = Value
        End Set
    End Property

    Public Overrides Sub Execute(ByVal env As Environment)
        Dim value As Object
        value = mExpression.Evaluate(env)
        If value Is Nothing Then
            value = ""
        End If
        env.Processor.Print(value)
        env.Processor.Print(vbCrLf)
    End Sub
End Class
