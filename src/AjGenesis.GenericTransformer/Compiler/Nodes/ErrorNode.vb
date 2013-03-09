Imports System.IO

Imports AjGenesis.Core

Public Class ErrorNode
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
        Throw New ErrorException(mExpression.Evaluate(env).ToString())
    End Sub
End Class
