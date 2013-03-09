Public MustInherit Class ValueType
    Inherits Type

    Public Overrides Function IsReference() As Boolean
        Return False
    End Function
End Class
