Imports System.IO
Imports System.Xml
Imports System.Collections.Specialized

Public Class EntityXml
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

    Private Shared Sub GetRelations(ByVal entity As Entity, ByVal node As XmlNode)
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
            entity.Relations.Add(rel)
        Next
    End Sub

    Private Shared Function GetProperty(ByVal node As XmlNode) As EntityProperty
        If node.Name <> "Property" Then
            Throw New ArgumentException("No es Property")
        End If

        Dim prop As New EntityProperty()

        prop.Name = GetNodeProperty(node, "Name")
        prop.Description = GetNodeProperty(node, "Description")
        prop.Type = GetNodeProperty(node, "Type")
        prop.SqlType = GetNodeProperty(node, "SqlType")

        Return prop
    End Function

    Private Shared Sub GetProperties(ByVal entity As Entity, ByVal node As XmlNode)
        If node Is Nothing Then
            Throw New ArgumentNullException("No hay Properties")
        End If
        If node.Name <> "Properties" Then
            Throw New ArgumentException("No es Properties")
        End If

        Dim child As XmlNode
        Dim prop As EntityProperty

        For Each child In node.SelectNodes("Property")
            prop = GetProperty(child)
            entity.Properties.Add(prop)
        Next
    End Sub

    Public Shared Function GetEntity(ByVal node As XmlNode) As Entity
        If node.Name <> "Entity" Then
            Throw New ArgumentException("No es Entity")
        End If

        If Not node.Attributes("Source") Is Nothing Then
            Return GetEntity(node.Attributes("Source").Value)
        End If

        Dim entity As New Entity()

        entity.Name = GetNodeProperty(node, "Name")
        entity.SetName = GetNodeProperty(node, "SetName")
        entity.Description = GetNodeProperty(node, "Description")
        entity.SqlTable = GetNodeProperty(node, "SqlTable")
        entity.Descriptor = GetNodeProperty(node, "Descriptor")
        entity.SetDescriptor = GetNodeProperty(node, "SetDescriptor")
        entity.Genre = GetNodeProperty(node, "Genre")

        GetProperties(entity, node.SelectSingleNode("Properties"))
        GetRelations(entity, node.SelectSingleNode("Relations"))

        Return entity
    End Function

    Public Shared Function GetEntity(ByVal input As TextReader) As Entity
        Dim docxml As New XmlDocument()

        docxml.Load(input)

        Return GetEntity(docxml.DocumentElement)
    End Function

    Public Shared Function GetEntity(ByVal inputFile As String) As Entity
        Dim docxml As New XmlDocument()

        docxml.Load(Path.Combine(ModelXml.CurrentPath, inputFile))

        Return GetEntity(docxml.DocumentElement)
    End Function
End Class
