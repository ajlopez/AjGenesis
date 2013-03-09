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

Imports AjGenesis.Core

Public Class ModelBuilder
    Implements IModelBuilder

    Private mCurrentPath As String = Nothing
    Private mProjectBuilder As ProjectBuilder = Nothing

    Public Sub New()

    End Sub

    Public Sub New(ByVal prjbuilder As ProjectBuilder)
        mProjectBuilder = prjbuilder
    End Sub

    Property CurrentPath() As String
        Get
            If mCurrentPath Is Nothing And Not mProjectBuilder Is Nothing Then
                Return ProjectBuilder.CurrentPath
            End If

            Return mCurrentPath
        End Get
        Set(ByVal Value As String)
            mCurrentPath = Value
        End Set
    End Property

    Public Function GetObject(ByVal filename As String) As Object
        Dim CurrPath As String
        Dim docxml As New XmlDocument()

        CurrPath = CurrentPath

        Try
            filename = Path.Combine(CurrentPath, filename)
            CurrentPath = (New FileInfo(filename)).Directory.ToString
            docxml.Load(filename)
            Return GetObject(docxml.DocumentElement)
        Finally
            CurrentPath = CurrPath
        End Try
    End Function

    Private Function GetObject(ByVal node As XmlNode) As Object
        If Not node.Attributes("Source") Is Nothing Then
            Return GetObject(node.Attributes("Source").Value)
        End If

        If node.ChildNodes.Count = 1 AndAlso node.FirstChild.NodeType = XmlNodeType.Text Then
            Return node.FirstChild.Value
        End If

        Dim dynobj As DynamicObject

        Dim child As XmlNode
        Dim lastname As String = Nothing
        Dim repeatedname As String = Nothing

        For Each child In node.ChildNodes
            If child.Name.Equals(lastname) Then
                repeatedname = child.Name
            End If
            lastname = child.Name
        Next

        If repeatedname Is Nothing Then
            dynobj = New DynamicObject()
        Else
            dynobj = New DynamicListObject()
        End If

        Dim attr As XmlAttribute

        For Each attr In node.Attributes
            dynobj.SetValue(attr.Name, attr.Value)
        Next

        For Each child In node.ChildNodes
            Dim childobj As Object = GetObject(child)
            If child.Name.Equals(repeatedname) Then
                DirectCast(dynobj, DynamicListObject).AddValue(childobj)
            Else
                dynobj.SetValue(child.Name, childobj)
            End If
        Next

        Return dynobj
    End Function

    Public Function GetModel(ByVal node As XmlNode) As IModel Implements IModelBuilder.GetModel
        If node.Name <> "Model" Then
            Throw New ArgumentException("It's not a Model")
        End If

        Dim model As New model()

        Dim attr As XmlAttribute

        For Each attr In node.Attributes
            model.SetValue(attr.Name, attr.Value)
        Next

        Dim child As XmlNode

        For Each child In node.ChildNodes
            model.SetValue(child.Name, GetObject(child))
        Next

        Return model
    End Function

    Public Function GetModel(ByVal input As TextReader) As IModel Implements IModelBuilder.GetModel
        Dim docxml As New XmlDocument()

        docxml.Load(input)

        Return GetModel(docxml.DocumentElement)
    End Function

    Public Function GetModel(ByVal filename As String) As IModel Implements IModelBuilder.GetModel
        Dim CurrPath As String
        Dim docxml As New XmlDocument()

        CurrPath = CurrentPath

        Try
            filename = Path.Combine(CurrentPath, filename)
            CurrentPath = (New FileInfo(filename)).Directory.ToString
            docxml.Load(filename)
            Return GetModel(docxml.DocumentElement)
        Finally
            CurrentPath = CurrPath
        End Try
    End Function
End Class
