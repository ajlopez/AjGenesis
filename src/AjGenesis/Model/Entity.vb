Imports System.Collections
Imports System.Collections.Specialized

Public Class Entity
    Private mName As String
    Private mSetName As String
    Private mDescription As String
    Private mModel As Model
    Private mDescriptor As String
    Private mSetDescriptor As String
    Private mGenre As String
    Private mSqlTable As String

    Private mProperties As IList = New ArrayList()
    Private mRelations As IList = New ArrayList()

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

    Public Property SetName() As String
        Get
            If mSetName Is Nothing Then
                Return Name & "s"
            End If
            Return mSetName
        End Get
        Set(ByVal Value As String)
            mSetName = Value
        End Set
    End Property

    Public Property Descriptor() As String
        Get
            If mDescriptor Is Nothing Then
                Return Name
            End If

            Return mDescriptor
        End Get
        Set(ByVal Value As String)
            mDescriptor = Value
        End Set
    End Property

    Public Property SetDescriptor() As String
        Get
            If mSetDescriptor Is Nothing Then
                Return SetName
            End If
            Return mSetDescriptor
        End Get
        Set(ByVal Value As String)
            mSetDescriptor = Value
        End Set
    End Property

    Public Property Genre() As String
        Get
            If mGenre Is Nothing Then
                Return "M"
            End If
            Return mGenre
        End Get
        Set(ByVal Value As String)
            mGenre = Value
        End Set
    End Property

    Public Property SqlTable() As String
        Get
            If mSqlTable Is Nothing Then
                Return SetName
            End If
            Return mSqlTable
        End Get
        Set(ByVal Value As String)
            mSqlTable = Value
        End Set
    End Property

    Public ReadOnly Property Properties() As IList
        Get
            Return mProperties
        End Get
    End Property

    Public ReadOnly Property Relations() As IList
        Get
            Return mRelations
        End Get
    End Property

    Public Function GetProperty(ByVal name As String) As EntityProperty
        Dim prop As EntityProperty

        For Each prop In Properties
            If prop.Name = name Then
                Return prop
            End If
        Next

        Return Nothing
    End Function

    Public Function GetRelation(ByVal name As String) As EntityRelation
        Dim rel As EntityRelation
        For Each rel In Relations
            If rel.Name = name Then
                Return rel
            End If
        Next

        Return Nothing
    End Function
End Class
