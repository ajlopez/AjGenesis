Public Class ViewProperty
    Private mName As String
    Private mText As String
    Private mProperty As EntityProperty

    Public Property Name() As String
        Get
            Return mName
        End Get
        Set(ByVal Value As String)
            mName = Value
        End Set
    End Property

    Public Property Text() As String
        Get
            Return mText
        End Get
        Set(ByVal Value As String)
            mText = Value
        End Set
    End Property

    Public Property [Property]() As EntityProperty
        Get
            Return mProperty
        End Get
        Set(ByVal Value As EntityProperty)
            mProperty = Value
        End Set
    End Property
End Class
