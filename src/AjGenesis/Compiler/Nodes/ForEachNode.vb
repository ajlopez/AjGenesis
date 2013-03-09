Public Class ForEachNode
    Inherits CommandNode

    Dim mVariable As String
    Dim mExpression As ExpressionNode
    Dim mWhere As ExpressionNode
    Dim mCommands As CommandListNode

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

    Public Property Commands() As CommandListNode
        Get
            Return mCommands
        End Get
        Set(ByVal Value As CommandListNode)
            mCommands = Value
        End Set
    End Property

    Overrides Sub Execute(ByVal env As Environment)
        Dim items As Object

        items = mExpression.Evaluate(env)

        Dim item As Object
        Dim index As Integer = 0

        env("_count") = items.Count

        For Each item In items
            env("_index") = index
            env(mVariable) = item
            If mWhere Is Nothing OrElse IsTrue(mWhere.Evaluate(env)) Then
                mCommands.Execute(env)
            End If
            index += 1
        Next
    End Sub
End Class
