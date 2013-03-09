Public Class EntityProperty
    Private mName As String
    Private mType As String
    Private mSqlName As String
    Private mSqlType As String
    Private mDescription As String

    Public Sub New()

    End Sub

    Public Sub New(ByVal name As String, ByVal type As String)
        mName = name
        mType = type
    End Sub

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
            If mDescription Is Nothing Then
                Return mName
            End If

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

    Public Property SqlType() As String
        Get
            Return mSqlType
        End Get
        Set(ByVal Value As String)
            mSqlType = Value
        End Set
    End Property

    Public Property SqlName() As String
        Get
            Return mSqlName
        End Get
        Set(ByVal Value As String)
            mSqlName = Value
        End Set
    End Property
End Class
