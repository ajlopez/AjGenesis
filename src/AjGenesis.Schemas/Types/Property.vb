Public Class [Property]
    Private mName As String
    Private mTypeName As String
    Private mType As Type
    Private mIsItem As Boolean
    Private mParent As Type

    Private mXmlFile As String
    Private mXmlIsElement As Boolean

    Public Property Name() As String
        Get
            Return mName
        End Get
        Set(ByVal Value As String)
            mName = Value
        End Set
    End Property

    Public Property TypeName() As String
        Get
            If mTypeName Is Nothing Then
                Return Type.Name
            End If

            Return mTypeName
        End Get
        Set(ByVal Value As String)
            mTypeName = Value
        End Set
    End Property

    Public Property Type() As Type
        Get
            If mType Is Nothing Then
                Return StringType.Instance
            End If

            Return mType
        End Get
        Set(ByVal Value As Type)
            mType = Value
        End Set
    End Property

    Public Property IsItem() As Boolean
        Get
            Return mIsItem
        End Get
        Set(ByVal Value As Boolean)
            mIsItem = Value
        End Set
    End Property

    Public Property Parent() As Type
        Get
            Return mParent
        End Get
        Set(ByVal Value As Type)
            mParent = Value
        End Set
    End Property

    Public Property XmlIsElement() As Boolean
        Get
            If Type.IsReference Then
                Return True
            End If

            Return mXmlIsElement
        End Get
        Set(ByVal Value As Boolean)
            If Type.IsReference Then
                Return
            End If

            mXmlIsElement = Value
        End Set
    End Property
End Class
