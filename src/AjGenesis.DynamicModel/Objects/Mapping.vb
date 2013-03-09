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

Public Class Mapping
    Implements IMapping

    Private mSourceNames As IList
    Private mTargetNames As IList
    Private mRelation As IDictionary

    Public Sub New()
        mSourceNames = New ArrayList
        mTargetNames = New ArrayList

        mRelation = New Hashtable
    End Sub

    Public Sub New(ByVal sourcenames As IList, ByVal targetnames As IList)
        mSourceNames = sourcenames
        mTargetNames = targetnames

        Dim k As Integer

        mRelation = New Hashtable

        For k = 0 To mSourceNames.Count - 1
            mRelation.Add(mSourceNames(k), mTargetNames(k))
        Next
    End Sub

    Public Sub Add(ByVal source As String, ByVal target As String)
        mSourceNames.Add(source)
        mTargetNames.Add(target)
        mRelation.Add(source, target)
    End Sub

    Public Function Map(ByVal name As String) As String Implements IMapping.Map
        Dim result As String

        result = mRelation(name)

        If result Is Nothing Then
            Throw New ArgumentException("Dato " + name + " desconocido")
        End If

        Return result
    End Function

    Public Function GetTargetNames() As IList Implements IMapping.GetTargetNames
        Return mTargetNames
    End Function

    Public Function GetSourceNames() As IList Implements IMapping.GetSourceNames
        Return mSourceNames
    End Function

    Public Function Reverse() As IMapping Implements IMapping.Reverse
        Return New Mapping(mTargetNames, mSourceNames)
    End Function
End Class
