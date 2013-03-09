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

Public Class DynamicListObject
    Inherits DynamicObject
    Implements IListObject

    Private mList As IList

    Public Sub New()
        mList = New ArrayList
    End Sub

    Public Sub New(ByVal list As IList)
        mList = list
    End Sub

    Public Overloads Sub AddValue(ByVal value As Object) Implements IListObject.AddValue
        mList.Add(value)
    End Sub

    Public Function GetList() As IList Implements IListObject.GetList
        Return mList
    End Function

    Public Sub SetList(ByVal list As IList) Implements IListObject.SetList
        Dim elem As Object
        mList = New ArrayList

        For Each elem In list
            mList.Add(elem)
        Next
    End Sub
End Class
