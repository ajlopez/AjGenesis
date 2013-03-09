'
' +---------------------------------------------------------------------+
' | AjGenesis - Code and Artifacts Generator in .NET                    |
' +---------------------------------------------------------------------+
' | Copyright (c) 2003-2011 Angel J. Lopez. All rights reserved.        |
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

Public Class DynamicObject
    Inherits BaseObject

    Private mValues As IDictionary

    Public Sub New()
        mValues = New ListDictionary()
    End Sub

    Public Sub New(ByVal obj As Object)
        Me.new()
        Copy(obj)
    End Sub

    Protected Overrides Function GetSimpleValue(ByVal name As String) As Object
        Return mValues(name)
    End Function

    Protected Overrides Sub SetSimpleValue(ByVal name As String, ByVal value As Object)
        mValues(name) = value
    End Sub

    Protected Overrides Function GetLeftValue(ByVal name As String) As Object
        Dim value As Object

        value = GetSimpleValue(name)

        If value Is Nothing Then
            value = New DynamicObject()
            SetSimpleValue(name, value)
        End If

        Return value
    End Function

    Public Overrides Function GetNames() As IList
        Dim names(mValues.Count - 1) As String

        mValues.Keys.CopyTo(names, 0)

        Return names
    End Function

    Public Overrides Function AcceptValue(ByVal name As String) As Boolean
        Return True
    End Function
End Class
