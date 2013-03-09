Imports System.IO
Imports System.Xml
Imports System.Collections.Specialized

Public Class ModelXml
    Private Shared mCurrentPath As String

    Shared Property CurrentPath() As String
        Get
            Return mCurrentPath
        End Get
        Set(ByVal Value As String)
            mCurrentPath = Value
        End Set
    End Property

    Private Shared Sub GetEntities(ByVal model As Model, ByVal entities As XmlNode)
        If entities Is Nothing Then
            Throw New ArgumentNullException("No hay Entities")
        End If

        If entities.Name <> "Entities" Then
            Throw New ArgumentException("No es Entities")
        End If

        Dim child As XmlNode
        Dim entity As Entity

        For Each child In entities.SelectNodes("Entity")
            entity = EntityXml.GetEntity(child)
            Model.Entities.Add(entity)
            entity.Model = model
            Dim rel As EntityRelation
            For Each rel In entity.Relations
                If Not model.Relations.Contains(rel) Then
                    model.Relations.Add(rel)
                End If
            Next
        Next
    End Sub

    Private Shared Function GetRelation(ByVal node As XmlNode) As EntityRelation
        If node.Name <> "Relation" Then
            Throw New ArgumentException("No es Relation")
        End If

        Dim rel As New EntityRelation()

        rel.Name = GetNodeProperty(node, "Name")
        rel.Type = GetNodeProperty(node, "Type")
        rel.SourceEntityName = GetNodeProperty(node, "SourceEntity")
        rel.TargetEntityName = GetNodeProperty(node, "TargetEntity")
        rel.SourcePropertyName = GetNodeProperty(node, "SourceProperty")
        rel.TargetPropertyName = GetNodeProperty(node, "TargetProperty")
        rel.Description = GetNodeProperty(node, "Description")

        Return rel
    End Function

    Private Shared Sub GetRelations(ByVal model As Model, ByVal node As XmlNode)
        If node Is Nothing Then
            Return
        End If
        If node.Name <> "Relations" Then
            Throw New ArgumentException("No es Relations")
        End If

        Dim child As XmlNode
        Dim rel As EntityRelation

        For Each child In node.SelectNodes("Relation")
            rel = GetRelation(child)
            model.Relations.Add(rel)
            Dim ent As Entity
            ent = model.GetEntity(rel.SourceEntityName)
            rel.SourceEntity = ent
            ent.Relations.Add(rel)
            ent = model.GetEntity(rel.TargetEntityName)
            rel.TargetEntity = ent
            ent.Relations.Add(rel)
        Next
    End Sub

    Public Shared Function GetModel(ByVal node As XmlNode) As Model
        If node.Name <> "Model" Then
            Throw New ArgumentException("No es Model")
        End If

        Dim model As New Model()

        model.Name = GetNodeProperty(node, "Name")
        model.Description = GetNodeProperty(node, "Description")
        model.Company = GetNodeProperty(node, "Company")

        GetEntities(model, node.SelectSingleNode("Entities"))
        GetRelations(model, node.SelectSingleNode("Relations"))

        Return model
    End Function

    Public Shared Function GetModel(ByVal input As TextReader) As Model
        Dim docxml As New XmlDocument()

        docxml.Load(input)

        Return GetModel(docxml.DocumentElement)
    End Function

    Public Shared Function GetModel(ByVal filename As String) As Model
        Dim CurrPath As String
        Dim docxml As New XmlDocument()

        Try
            CurrPath = CurrentPath
            CurrentPath = (New FileInfo(filename)).Directory.ToString
            docxml.Load(filename)
            Return GetModel(docxml.DocumentElement)
        Finally
            CurrentPath = CurrPath
        End Try
    End Function
End Class
