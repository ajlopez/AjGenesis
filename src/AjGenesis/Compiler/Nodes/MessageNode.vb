Imports System.IO

Public Class MessageNode
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
        env.Processor.Log(mExpression.Evaluate(env).ToString())
    End Sub
End Class
