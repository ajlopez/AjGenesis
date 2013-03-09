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

Public Class MappedObject
    Inherits BaseObject

    Private mObj As IObject
    Private mMapping As IMapping

    Public Sub New(ByVal obj As Object, ByVal mapping As IMapping)
        mObj = ToObject(obj)
        mMapping = mapping
    End Sub

    Public Sub New(ByVal obj As Object, ByVal sourcenames As IList, ByVal targetnames As IList)
        Me.New(obj, New Mapping(sourcenames, targetnames))
    End Sub

    Protected Overrides Function GetLeftValue(ByVal name As String) As Object
        Return mObj.GetLeftValue(mMapping.Map(name))
    End Function

    Protected Overrides Function GetSimpleValue(ByVal name As String) As Object
        Return mObj.GetValue(mMapping.Map(name))
    End Function

    Protected Overrides Sub SetSimpleValue(ByVal name As String, ByVal value As Object)
        mObj.SetValue(mMapping.Map(name), value)
    End Sub

    Public Overrides Function GetNames() As IList
        Return mMapping.GetSourceNames
    End Function
End Class
