Imports System.Collections
Imports System.Collections.Specialized

Public Class Model
    Private mName As String
    Private mDescription As String
    Private mCompany As String
    Private mEntities As IList = New ArrayList()
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

    Public Property Company() As String
        Get
            Return mCompany
        End Get
        Set(ByVal Value As String)
            mCompany = Value
        End Set
    End Property

    Public ReadOnly Property Entities() As IList
        Get
            Return mEntities
        End Get
    End Property

    Public ReadOnly Property Relations() As IList
        Get
            Return mRelations
        End Get
    End Property

    Public Function GetEntity(ByVal name As String) As Entity
        Dim ent As Entity

        For Each ent In mEntities
            If ent.Name = name Then
                Return ent
            End If
        Next

        Return Nothing
    End Function

    Public Function GetRelation(ByVal name As String) As EntityRelation
        Dim rel As EntityRelation

        For Each rel In mRelations
            If rel.Name = name Then
                Return rel
            End If
        Next

        Return Nothing
    End Function
End Class
