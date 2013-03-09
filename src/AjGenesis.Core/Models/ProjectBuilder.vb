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

Public Class ProjectBuilder

    Private Shared mCurrentPath As String

    Shared Property CurrentPath() As String
        Get
            Return mCurrentPath
        End Get
        Set(ByVal Value As String)
            mCurrentPath = Value
        End Set
    End Property

    Private Sub GetModels(ByVal project As Project, ByVal models As XmlNode, ByVal modelbuilder As IModelBuilder)
        If models Is Nothing Then
            Throw New ArgumentNullException("No hay Models")
        End If

        If models.Name <> "Models" Then
            Throw New ArgumentException("No es Models")
        End If

        Dim child As XmlNode
        Dim model As IModel

        For Each child In models.SelectNodes("Model")
            model = modelbuilder.GetModel(child)
            project.Models.Add(model)
        Next
    End Sub

    Public Function GetProject(ByVal node As XmlNode, ByVal modelbuilder As IModelBuilder) As Project
        If node.Name <> "Project" Then
            Throw New ArgumentException("No es Project")
        End If

        Dim project As New project()

        project.Name = GetNodeProperty(node, "Name")
        project.Description = GetNodeProperty(node, "Description")
        project.Company = GetNodeProperty(node, "Company")

        GetModels(project, node.SelectSingleNode("Models"), modelbuilder)

        Return project
    End Function

    Public Function GetProject(ByVal input As TextReader, ByVal modelbuilder As IModelBuilder) As Project
        Dim docxml As New XmlDocument()

        docxml.Load(input)

        Return GetProject(docxml.DocumentElement, modelbuilder)
    End Function

    Public Function GetProject(ByVal filename As String, ByVal modelbuilder As IModelBuilder) As Project
        Dim CurrPath As String = Nothing
        Dim docxml As New XmlDocument()

        Try
            CurrPath = CurrentPath
            CurrentPath = (New FileInfo(filename)).Directory.ToString
            docxml.Load(filename)
            Return GetProject(docxml.DocumentElement, modelbuilder)
        Finally
            CurrentPath = CurrPath
        End Try
    End Function
End Class
