Class UnaryOperatorNode
    Inherits ExpressionNode

    Private mExpr As ExpressionNode
    Private mOperator As String

    Sub New(ByVal operator As String, ByVal expr As ExpressionNode)
        mOperator = operator
        mExpr = expr
    End Sub

    ReadOnly Property Expression() As ExpressionNode
        Get
            Return mExpr
        End Get
    End Property

    ReadOnly Property Operator() As String
        Get
            Return mOperator
        End Get
    End Property

    Overrides Function Evaluate(ByVal env As Environment) As Object
        Dim expr As Object = mExpr.Evaluate(env)

        Select Case mOperator
            Case "not"
                Return Not expr
            Case Else
                Throw New Exception("Operador desconocido")
        End Select
    End Function
End Class
