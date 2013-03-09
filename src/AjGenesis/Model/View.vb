Public Class View
    Private mName As String
    Private mModel As Model
    Private mDescription As String
    Private mEntityName As String
    Private mEntity As Entity

    Private mProperties As New ArrayList()

    Public Property Name() As String
        Get
            Return mName
        End Get
        Set(ByVal Value As String)
            mName = Value
        End Set
    End Property

    Public Property Description() As String
        Get
            Return mDescription
        End Get
        Set(ByVal Value As String)
            mDescription = Value
        End Set
    End Property

    Public Property Model() As Model
        Get
            Return mModel
        End Get
        Set(ByVal Value As Model)
            mModel = Value
        End Set
    End Property

    Public Property Entity() As Entity
        Get
            Return mEntity
        End Get
        Set(ByVal Value As Entity)
            mEntity = Value
        End Set
    End Property

    Public Property EntityName() As String
        Get
            Return mEntityName
        End Get
        Set(ByVal Value As String)
            mEntityName = Value
        End Set
    End Property
End Class
