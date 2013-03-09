Public Class Schema
    Private mName As String
    Private mRootName As String
    Private mTypes As IList = New ArrayList

    Public Property Name() As String
        Get
            Return mName
        End Get
        Set(ByVal Value As String)
            mName = Value
        End Set
    End Property

    Public Property RootName() As String
        Get
            Return mRootName
        End Get
        Set(ByVal Value As String)
            mRootName = Value
        End Set
    End Property

    Public ReadOnly Property Root() As Type
        Get
            Dim type As Type

            For Each type In Types
                If type.Name = RootName Then
                    Return type
                End If
            Next
            Return Nothing
        End Get
    End Property

    Public Property Types() As IList
        Get
            Return mTypes
        End Get
        Set(ByVal Value As IList)
            mTypes = Value
        End Set
    End Property

    Public Sub AddType(ByVal type As Type)
        Types.Add(type)
    End Sub
End Class
