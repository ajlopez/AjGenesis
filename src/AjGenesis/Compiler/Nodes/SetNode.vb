Public Class SetNode
    Inherits CommandNode

    Private mIdentifier As String
    Private mExpression As ExpressionNode

    Public Property Identifier() As String
        Get
            Return mIdentifier
        End Get
        Set(ByVal Value As String)
            mIdentifier = Value
        End Set
    End Property

    Public Property Expression() As ExpressionNode
        Get
            Return mExpression
        End Get
        Set(ByVal Value As ExpressionNode)
            mExpression = Value
        End Set
    End Property

    Overrides Sub Execute(ByVal env As Environment)
        env(mIdentifier) = mExpression.Evaluate(env)
    End Sub
End Class
