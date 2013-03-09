Public Class Type
    Private mName As String

    Private mProperties As IList = New ArrayList

    Public Property Name() As String
        Get
            Return mName
        End Get
        Set(ByVal Value As String)
            mName = Value
        End Set
    End Property

    Public Property Properties() As IList
        Get
            Return mProperties
        End Get
        Set(ByVal Value As IList)
            mProperties = Value
        End Set
    End Property

    Public Sub AddProperty(ByVal prop As [Property])
        mProperties.Add(prop)
        prop.Parent = Me
    End Sub

    Public Function GetProperty(ByVal name As String) As [Property]
        Dim prop As [Property]

        For Each prop In Properties
            If prop.Name = name Then
                Return prop
            End If
        Next

        Return Nothing
    End Function

    Public Overridable Function IsReference() As Boolean
        Return True
    End Function

    Public Function IsList() As Boolean
        Dim prop As [Property]

        For Each prop In Properties
            If prop.IsItem Then
                Return True
            End If
        Next

        Return False
    End Function
End Class
