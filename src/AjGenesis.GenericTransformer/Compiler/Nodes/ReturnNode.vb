Imports AjGenesis.Core

Public Class ReturnException
    Inherits Exception

    Private mValue As Object

    Public Sub New()

    End Sub

    Public Sub New(ByVal value As Object)
        mValue = value
    End Sub

    Public ReadOnly Property Value() As Object
        Get
            Return mValue
        End Get
    End Property
End Class

Public Class ReturnNode
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
        If mExpression Is Nothing Then
            Throw New ReturnException()
        End If

        Throw New ReturnException(mExpression.Evaluate(env))
    End Sub
End Class
