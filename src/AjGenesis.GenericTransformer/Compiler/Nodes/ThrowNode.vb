Imports System.IO

Imports AjGenesis.Core

Public Class ThrowNode
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
        If TypeOf value Is Exception Then
            Throw DirectCast(value, Exception)
        End If

        Throw New Exception(value.ToString())
    End Sub
End Class
