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

Imports System.Collections
Imports System.Collections.Specialized

Public Class Project
    Private mName As String
    Private mDescription As String
    Private mCompany As String
    Private mModels As IList = New ArrayList()
    Private mActions As IList = New ArrayList()

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

    Public ReadOnly Property Models() As IList
        Get
            Return mModels
        End Get
    End Property

    Public ReadOnly Property Actions() As IList
        Get
            Return mActions
        End Get
    End Property

    'Public Function GetModel(ByVal name As String) As Entity
    '    Dim ent As IModel

    '    For Each ent In mModels
    '        If ent.Name = name Then
    '            Return ent
    '        End If
    '    Next

    '    Return Nothing
    'End Function
End Class
