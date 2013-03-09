Public Class IfNode
    Inherits CommandNode

    Dim mExpression As ExpressionNode
    Dim mThenCommand As CommandNode
    Dim mElseCommand As CommandNode

    Public Property Expression() As ExpressionNode
        Get
            Return mExpression
        End Get
        Set(ByVal Value As ExpressionNode)
            mExpression = Value
        End Set
    End Property

    Public Property ThenCommand() As CommandNode
        Get
            Return mThenCommand
        End Get
        Set(ByVal Value As CommandNode)
            mThenCommand = Value
        End Set
    End Property

    Public Property ElseCommand() As CommandNode
        Get
            Return mElseCommand
        End Get
        Set(ByVal Value As CommandNode)
            mElseCommand = Value
        End Set
    End Property

    Overrides Sub Execute(ByVal env As Environment)
        If IsTrue(mExpression.Evaluate(env)) Then
            If Not mThenCommand Is Nothing Then
                mThenCommand.Execute(env)
            End If
        Else
            If Not mElseCommand Is Nothing Then
                mElseCommand.Execute(env)
            End If
        End If
    End Sub
End Class
