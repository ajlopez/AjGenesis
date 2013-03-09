Class NameNode
    Inherits ExpressionNode

    Private mName As String

    Sub New(ByVal name As String)
        mName = name
    End Sub

    ReadOnly Property Name() As String
        Get
            Return mName
        End Get
    End Property

    Overrides Function Evaluate(ByVal env As Environment) As Object
        Return env(mName)
    End Function
End Class
