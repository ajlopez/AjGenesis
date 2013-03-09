Imports AjGenesis.Core

Class BinaryOperatorNode
    Inherits ExpressionNode

    Private mLeft As ExpressionNode
    Private mRight As ExpressionNode
    Private mOperator As String

    Sub New(ByVal [operator] As String, ByVal left As Node, ByVal right As Node)
        mOperator = [operator]
        mLeft = left
        mRight = right
    End Sub

    ReadOnly Property Left() As ExpressionNode
        Get
            Return mLeft
        End Get
    End Property

    ReadOnly Property Right() As ExpressionNode
        Get
            Return mRight
        End Get
    End Property

    ReadOnly Property [Operator]() As String
        Get
            Return mOperator
        End Get
    End Property

    Overrides Function Evaluate(ByVal env As Environment) As Object
        Dim left As Object = mLeft.Evaluate(env)
        Dim right As Object = mRight.Evaluate(env)

        Select Case mOperator
            Case "+"
                Return left + right
            Case "-"
                Return left - right
            Case "*"
                Return left * right
            Case "/"
                Return left / right
            Case "&"
                Return left & right
            Case "="
                Return left = right
            Case "<>"
                Return left <> right
            Case ">"
                Return left > right
            Case "<"
                Return left < right
            Case ">="
                Return left >= right
            Case "<="
                Return left <= right
            Case Else
                Throw New Exception("Operador desconocido")
        End Select
    End Function
End Class
