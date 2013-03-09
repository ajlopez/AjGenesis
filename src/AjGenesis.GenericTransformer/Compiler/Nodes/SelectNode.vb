Imports AjGenesis.Core
Imports AjGenesis.Models.DynamicModel

Public Class SelectNode
    Inherits ExpressionNode

    Dim mVariable As String
    Dim mExpression As ExpressionNode
    Dim mWhere As ExpressionNode

    Public Property Variable() As String
        Get
            Return mVariable
        End Get
        Set(ByVal Value As String)
            mVariable = Value
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

    Public Property Where() As ExpressionNode
        Get
            Return mWhere
        End Get
        Set(ByVal Value As ExpressionNode)
            mWhere = Value
        End Set
    End Property

    Overrides Function Evaluate(ByVal env As Environment) As Object
        Dim items As Object

        items = mExpression.Evaluate(env)

        If TypeOf items Is IListObject Then
            items = DirectCast(items, IListObject).GetList()
        End If

        Dim item As Object

        Dim result As New System.Collections.ArrayList

        For Each item In items
            env(mVariable) = item
            If mWhere Is Nothing OrElse IsTrue(mWhere.Evaluate(env)) Then
                result.Add(item)
            End If
        Next

        Return result
    End Function
End Class
