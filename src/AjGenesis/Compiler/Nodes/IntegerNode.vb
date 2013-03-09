Class IntegerNode
    Inherits ExpressionNode

    Private mValue As Integer

    Sub New(ByVal value As Integer)
        mValue = value
    End Sub

    ReadOnly Property Value() As Integer
        Get
            Return mValue
        End Get
    End Property

    Overrides Function Evaluate(ByVal env As Environment) As Object
        Return mValue
    End Function
End Class
