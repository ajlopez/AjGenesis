'
' +---------------------------------------------------------------------+
' | AjGenesis - Code and Artifacts Generator in .NET                    |
' +---------------------------------------------------------------------+
' | Copyright (c) 2003-2011 Angel J. Lopez. All rights reserved.        |
' | http://www.ajlopez.com                                              |
' | http://www.ajlopez.net                                              |
' +---------------------------------------------------------------------+
' | This source file is subject to the ajgenesis Software License,      |
' | Version 1.0, that is bundled with this package in the file LICENSE. |
' | If you did not receive a copy of this file, you may read it online  |
' | at http://www.ajlopez.net/ajgenesis/license.php.                    |
' +---------------------------------------------------------------------+
'
'

Imports System.Xml

Public Class XmlObject
    Inherits BaseObject

    Private mNode As XmlNode

    Public Sub New(ByVal rootname As String)
        Dim doc As New XmlDocument()
        Dim root As XmlNode

        root = doc.CreateElement(rootname)

        doc.AppendChild(root)

        mNode = root
    End Sub

    Public Sub New(ByVal rootname As String, ByVal obj As Object)
        Me.New(rootname)
        Copy(obj)
    End Sub

    Public Sub New(ByVal obj As Object)
        Me.New(obj.GetType.Name, obj)
    End Sub

    Public Sub New(ByVal doc As XmlDocument)
        mNode = doc.DocumentElement
    End Sub

    Public Sub New(ByVal node As XmlNode)
        mNode = node
    End Sub

    Public Sub New(ByVal doc As XmlDocument, ByVal name As String, ByVal obj As Object)
        mNode = doc.CreateElement(name)
        Copy(obj)
    End Sub

    Public Sub New(ByVal node As XmlNode, ByVal obj As Object)
        mNode = node
        Copy(obj)
    End Sub

    Public Function GetNode() As XmlNode
        Return mNode
    End Function

    Protected Overrides Function GetLeftValue(ByVal name As String) As Object
        Dim n As XmlNode

        n = SearchNode(name)

        If n Is Nothing Then
            n = mNode.OwnerDocument.CreateElement(name)
            mNode.AppendChild(n)
        End If

        Return n
    End Function

    Private Function AppendNode(ByVal name As String) As XmlNode
        Dim node As XmlNode

        For Each node In mNode.ChildNodes
            If node.Name = name Then
                Return node
            End If
        Next

        Dim newNode As XmlNode = mNode.OwnerDocument.CreateElement(name)
        mNode.AppendChild(newNode)

        Return newNode
    End Function

    Private Function SearchNode(ByVal name As String) As XmlNode
        Dim node As XmlNode

        For Each node In mNode.ChildNodes
            If node.Name = name Then
                Return node
            End If
        Next

        Return Nothing
    End Function

    Protected Overrides Sub SetSimpleValue(ByVal name As String, ByVal value As Object)
        Dim node As XmlNode = SearchNode(name)

        If node Is Nothing Then
            node = AppendNode(name)
        Else
            node.RemoveAll()
        End If

        If value Is Nothing Then
            Return
        End If

        If value.GetType.IsPrimitive() OrElse value.GetType.IsValueType() OrElse TypeOf value Is String Then
            Dim text As XmlNode = node.OwnerDocument.CreateTextNode(value.ToString)
            node.AppendChild(text)
            Return
        End If

        Dim obj As New XmlObject(node)

        obj.Copy(value)
    End Sub

    Protected Overrides Function GetSimpleValue(ByVal name As String) As Object
        Dim node As XmlNode = SearchNode(name)

        If Not node Is Nothing Then
            If node.ChildNodes.Count = 0 Then
                'Return Nothing
                Return ""
            End If

            If node.ChildNodes.Count = 1 AndAlso node.FirstChild.NodeType = XmlNodeType.Text Then
                Return node.FirstChild.Value()
            End If

            Return node
        End If

        Return Nothing
    End Function

    Protected Overrides Sub AddSimpleValue(ByVal name As String, ByVal value As Object)
        Dim lvalue As Object

        lvalue = GetLeftValue(name)

        Dim n As New XmlObject(DirectCast(lvalue, XmlNode))

        n.AddValue(value)
    End Sub

    Private Sub AppendNode(ByVal parent As XmlNode, ByVal node As XmlNode)
        Dim newnode As XmlNode = Nothing

        Select Case node.NodeType
            Case XmlNodeType.Text
                newnode = parent.OwnerDocument.CreateTextNode(node.Value)
            Case XmlNodeType.Element
                newnode = parent.OwnerDocument.CreateElement(node.Name)
        End Select

        If newnode Is Nothing Then
            Return
        End If

        parent.AppendChild(newnode)

        Dim child As XmlNode

        For Each child In node.ChildNodes
            AppendNode(newnode, child)
        Next
    End Sub

    Public Overloads Sub AddValue(ByVal value As Object)
        Dim child As XmlNode

        If TypeOf value Is XmlObject Then
            AppendNode(mNode, DirectCast(value, XmlObject).GetNode)
            Return
            'child = mNode.OwnerDocument.CreateElement(CType(value, XmlObject).GetNode.Name)
        ElseIf TypeOf value Is XmlNode Then
            AppendNode(mNode, DirectCast(value, XmlNode))
            Return
            'child = mNode.OwnerDocument.CreateElement(CType(value, XmlNode).Name)
        ElseIf TypeOf value Is XmlDocument Then
            AppendNode(mNode, DirectCast(value, XmlDocument).DocumentElement())
            Return
            'child = mNode.OwnerDocument.CreateElement(CType(value, XmlDocument).DocumentElement.Name)
        Else
            child = mNode.OwnerDocument.CreateElement("Item")
        End If

        ToObject(child).Copy(value)

        mNode.AppendChild(child)
    End Sub

    Public Overrides Function GetNames() As IList
        Dim names As New ArrayList()
        Dim child As XmlNode

        For Each child In mNode.ChildNodes
            names.Add(child.Name)
        Next

        Return names
    End Function

    Public Overrides Function AcceptValue(ByVal name As String) As Boolean
        Return True
    End Function
End Class
