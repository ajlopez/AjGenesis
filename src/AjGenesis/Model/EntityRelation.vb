Public Class EntityRelation
    Private mName As String
    Private mSourceEntity As Entity
    Private mTargetEntity As Entity
    Private mSourceEntityName As String
    Private mTargetEntityName As String
    Private mSourceProperty As EntityProperty
    Private mTargetProperty As EntityProperty
    Private mSourcePropertyName As String
    Private mTargetPropertyName As String
    Private mDescription As String
    Private mType As String

    Public Sub New()

    End Sub

    Public Property Name() As String
        Get
            Return mName
        End Get
        Set(ByVal Value As String)
            mName = Value
        End Set
    End Property

    Public Property SourceEntity() As Entity
        Get
            Return mSourceEntity
        End Get
        Set(ByVal Value As Entity)
            mSourceEntity = Value
        End Set
    End Property

    Public Property SourceEntityName() As String
        Get
            Return mSourceEntityName
        End Get
        Set(ByVal Value As String)
            mSourceEntityName = Value
        End Set
    End Property

    Public Property TargetEntity() As Entity
        Get
            Return mTargetEntity
        End Get
        Set(ByVal Value As Entity)
            mTargetEntity = Value
        End Set
    End Property

    Public Property TargetEntityName() As String
        Get
            Return mTargetEntityName
        End Get
        Set(ByVal Value As String)
            mTargetEntityName = Value
        End Set
    End Property

    Public Property SourceProperty() As EntityProperty
        Get
            Return mSourceProperty
        End Get
        Set(ByVal Value As EntityProperty)
            mSourceProperty = Value
        End Set
    End Property

    Public Property SourcePropertyName() As String
        Get
            Return mSourcePropertyName
        End Get
        Set(ByVal Value As String)
            mSourcePropertyName = Value
        End Set
    End Property

    Public Property TargetProperty() As EntityProperty
        Get
            Return mTargetProperty
        End Get
        Set(ByVal Value As EntityProperty)
            mTargetProperty = Value
        End Set
    End Property

    Public Property TargetPropertyName() As String
        Get
            Return mTargetPropertyName
        End Get
        Set(ByVal Value As String)
            mTargetPropertyName = Value
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

    Public Property Type() As String
        Get
            Return mType
        End Get
        Set(ByVal Value As String)
            mType = Value
        End Set
    End Property
End Class
