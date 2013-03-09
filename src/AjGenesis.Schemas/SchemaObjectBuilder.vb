Imports System.Xml
Imports System.IO

Imports AjGenesis.Models.DynamicModel

Public Class SchemaObjectBuilder
    Inherits ObjectXmlBuilder

    Public Overloads Function GetObject(ByVal filename As String, ByVal type As Type, ByVal propinfo As Boolean) As Object
        Dim CurrPath As String
        Dim docxml As New XmlDocument

        CurrPath = CurrentPath

        Try
            If Not CurrentPath Is Nothing Then
                filename = Path.Combine(CurrentPath, filename)
            End If
            CurrentPath = (New FileInfo(filename)).Directory.ToString
            docxml.Load(filename)
            Return GetObject(docxml.DocumentElement, type, propinfo)
        Finally
            CurrentPath = CurrPath
        End Try
    End Function

    Public Overloads Function GetObject(ByVal filename As String, ByVal type As Type) As Object
        Return GetObject(filename, type, False)
    End Function

    Protected Overloads Function GetObject(ByVal node As XmlNode, ByVal type As Type, ByVal propinfo As Boolean) As Object
        If type Is Nothing Then
            Return GetObject(node)
        End If

        If Not node.Attributes("Source") Is Nothing Then
            Return GetObject(node.Attributes("Source").Value, type, propinfo)
        End If

        If node.ChildNodes.Count = 1 AndAlso node.FirstChild.NodeType = XmlNodeType.Text Then
            Return node.FirstChild.Value
        End If

        Dim dynobj As DynamicObject

        Dim child As XmlNode
        Dim names As New ArrayList
        Dim repeatednames As New ArrayList
        Dim childname As String

        For Each child In node.ChildNodes
            childname = child.Name.Replace("."c, "_"c)
            If names.Contains(childname) Then
                repeatednames.Add(childname)
            End If
            names.Add(childname)
        Next

        If Not type.IsList And Not IsList(node) AndAlso repeatednames.Count = 0 Then
            dynobj = New DynamicObject
        Else
            dynobj = New DynamicListObject
        End If

        Dim attr As XmlAttribute

        For Each attr In node.Attributes
            dynobj.SetValue(attr.Name.Replace("."c, "_"c), attr.Value)
        Next

        dynobj.SetValue("TypeName", node.Name.Replace("."c, "_"c))

        Dim prop As [Property]
        Dim proptype As type

        For Each child In node.ChildNodes
            childname = child.Name.Replace("."c, "_"c)

            prop = type.GetProperty(childname)

            If prop Is Nothing Then
                proptype = StringType.Instance
            Else
                proptype = prop.Type
            End If

            Dim childobj As Object

            If prop Is Nothing Then
                childobj = GetObject(child)
            Else
                childobj = GetObject(child, type, propinfo)
            End If

            If type.IsList Or IsList(node) Or proptype.IsList Or repeatednames.Contains(childname) Then
                DirectCast(dynobj, DynamicListObject).AddValue(childobj)
            Else
                dynobj.SetValue(childname, childobj)
            End If
        Next

        Return dynobj
    End Function

    Protected Overloads Function GetObject(ByVal node As XmlNode, ByVal type As Type) As Object
        Return GetObject(node, type, False)
    End Function
End Class
