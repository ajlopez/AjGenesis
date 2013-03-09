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

Public Module Objects
    Sub Copy(ByVal target As IObject, ByVal source As IObject)
        Dim name As String
        Dim names As IList

        names = target.GetNames

        For Each name In source.GetNames
            If names.Contains(name) OrElse target.AcceptValue(name) Then
                target.SetValue(name, source.GetValue(name))
            End If
        Next
    End Sub

    Sub Copy(ByVal target As Object, ByVal source As Object)
        Copy(ToObject(target), ToObject(source))
    End Sub

    Function ToObject(ByVal obj As Object) As IObject
        If TypeOf obj Is IObject Then
            Return CType(obj, IObject)
        End If

        If TypeOf obj Is XmlDocument Then
            Return New XmlObject(CType(obj, XmlDocument))
        End If

        If TypeOf obj Is XmlNode Then
            Return New XmlObject(CType(obj, XmlNode))
        End If

        Return New ValueObject(obj)
    End Function

    Function ToList(ByVal obj As Object) As IListObject
        If TypeOf obj Is IListObject Then
            Return CType(obj, IListObject)
        End If

        If TypeOf obj Is IList Then
            Return New GenericList(obj)
        End If

        Throw New ArgumentException("No se puede convertir a IListObject")
    End Function

    Function Equals(ByVal obj As IObject, ByVal obj2 As IObject) As Boolean
        Dim name As String
        Dim names1 As IList = obj.GetNames
        Dim names2 As IList = obj2.GetNames

        Dim value1 As Object
        Dim value2 As Object

        For Each name In names1
            If names2.Contains(name) Or obj2.AcceptValue(name) Then
                value1 = obj.GetValue(name)
                value2 = obj2.GetValue(name)
                If value1 <> value2 Then
                    Return False
                End If
            Else
                Return False
            End If
        Next

        If names1.Count = names2.Count Then
            Return True
        End If

        For Each name In names2
            If names1.Contains(name) Or obj.AcceptValue(name) Then
            Else
                Return False
            End If
        Next

        Return True
    End Function

    Function Equals(ByVal obj1 As Object, ByVal obj2 As Object) As Boolean
        Return Equals(ToObject(obj1), ToObject(obj2))
    End Function

    Function IsList(ByVal obj As Object) As Boolean
        If TypeOf obj Is IList Then
            Return True
        End If

        If TypeOf obj Is IListObject Then
            Return True
        End If

        Return False
    End Function
End Module
