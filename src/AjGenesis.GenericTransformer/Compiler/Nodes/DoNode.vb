Imports AjGenesis.Core

Public Class DoNode
    Inherits CommandNode

    Dim mExpression As ExpressionNode

    Public Property Expression() As ExpressionNode
        Get
            Return mExpression
        End Get
        Set(ByVal Value As ExpressionNode)
            mExpression = Value
        End Set
    End Property

    Overrides Sub Execute(ByVal env As Environment)
        mExpression.Evaluate(env)
    End Sub
End Class
