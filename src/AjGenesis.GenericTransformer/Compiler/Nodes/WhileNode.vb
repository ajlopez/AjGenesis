Imports AjGenesis.Core

Public Class WhileNode
    Inherits CommandNode

    Dim mExpression As ExpressionNode
    Dim mCommands As CommandListNode

    Public Property Expression() As ExpressionNode
        Get
            Return mExpression
        End Get
        Set(ByVal Value As ExpressionNode)
            mExpression = Value
        End Set
    End Property

    Public Property Commands() As CommandListNode
        Get
            Return mCommands
        End Get
        Set(ByVal Value As CommandListNode)
            mCommands = Value
        End Set
    End Property

    Overrides Sub Execute(ByVal env As Environment)
        While IsTrue(mExpression.Evaluate(env))
            mCommands.Execute(env)
        End While
    End Sub
End Class
