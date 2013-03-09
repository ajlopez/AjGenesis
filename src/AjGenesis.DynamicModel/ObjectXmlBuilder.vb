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

Imports System.IO
Imports System.Xml
Imports System.Collections.Specialized

Public Class ObjectXmlBuilder
    Private mCurrentPath As String

    Public Sub New()

    End Sub

    Property CurrentPath() As String
        Get
            Return mCurrentPath
        End Get
        Set(ByVal Value As String)
            mCurrentPath = Value
        End Set
    End Property

    Public Function GetObject(ByVal filename As String) As Object
        Dim CurrPath As String
        Dim docxml As New XmlDocument

        CurrPath = CurrentPath

        Try
            If Not CurrentPath Is Nothing Then
                filename = Path.Combine(CurrentPath, filename)
            End If
            CurrentPath = (New FileInfo(filename)).Directory.ToString
            docxml.Load(filename)
            Return GetObject(docxml.DocumentElement)
        Finally
            CurrentPath = CurrPath
        End Try
    End Function

    '' TODO To review, tolower, other endings
    Protected Overridable Function IsList(ByVal node As XmlNode) As Boolean
        If node.Name.EndsWith("s") AndAlso Not node.Name.EndsWith("ss") Then
            Return True
        End If

        Return False
    End Function

    Protected Function GetObject(ByVal node As XmlNode) As Object
        If Not node.Attributes("Source") Is Nothing Then
            Return GetObject(node.Attributes("Source").Value)
        End If

        If node.ChildNodes.Count = 1 AndAlso node.FirstChild.NodeType = XmlNodeType.Text AndAlso node.Attributes.Count = 0 Then
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

        If Not IsList(node) AndAlso repeatednames.Count = 0 Then
            dynobj = New DynamicObject
        Else
            dynobj = New DynamicListObject
        End If

        Dim attr As XmlAttribute

        For Each attr In node.Attributes
            dynobj.SetValue(attr.Name.Replace("."c, "_"c), attr.Value)
        Next

        dynobj.SetValue("TypeName", node.Name.Replace("."c, "_"c))

        If node.ChildNodes.Count = 1 AndAlso node.FirstChild.NodeType = XmlNodeType.Text Then
            dynobj.SetValue("TextValue", node.FirstChild.Value)
            Return dynobj
        End If

        For Each child In node.ChildNodes
            childname = child.Name.Replace("."c, "_"c)

            If child.NodeType <> XmlNodeType.Comment Then

                Dim childobj As Object = GetObject(child)

                'If TypeOf childobj Is IObject Then
                '    childobj.SetValue("Parent", dynobj)
                'End If

                If IsList(node) Or repeatednames.Contains(childname) Then
                    DirectCast(dynobj, DynamicListObject).AddValue(childobj)
                Else
                    dynobj.SetValue(childname, childobj)
                End If
            End If
        Next

        Return dynobj
    End Function
End Class
