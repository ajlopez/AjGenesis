Class QuoteNode
    Inherits ExpressionNode

    Private mValue As String

    Sub New(ByVal value As String)
        mValue = value
    End Sub

    ReadOnly Property Value() As String
        Get
            Return mValue
        End Get
    End Property

    Overrides Function Evaluate(ByVal env As Environment) As Object
        Return mValue
    End Function
End Class
