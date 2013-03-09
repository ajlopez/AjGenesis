Imports System.IO

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
        env.Processor.Print(mExpression.Evaluate(env).ToString())
        env.Processor.Print(vbCrLf)
    End Sub
End Class
