Public Class StringType
    Inherits ValueType

    Private Shared mInstance As StringType = New StringType

    Private Sub New()
        Name = "String"
    End Sub

    Public Shared ReadOnly Property Instance() As StringType
        Get
            Return mInstance
        End Get
    End Property
End Class
