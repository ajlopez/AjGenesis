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

Public MustInherit Class BaseObject
    Implements IObject

    Const Separator As Char = "."c

    Protected MustOverride Function GetSimpleValue(ByVal name As String) As Object
    Protected MustOverride Sub SetSimpleValue(ByVal name As String, ByVal value As Object)

    Protected MustOverride Function GetLeftValue(ByVal name As String) As Object Implements IObject.GetLeftValue

    Public Overridable Function ConvertValue(ByVal name As String, ByVal value As Object) As Object
        If TypeOf value Is ValueObject Then
            Return CType(value, ValueObject).GetObject
        End If

        Return value
    End Function

    Public Overridable Function GetValue(ByVal name As String) As Object Implements IObject.GetValue
        Dim pos As Integer

        pos = name.IndexOf(Separator)

        If pos > 0 Then
            Dim name2 As String
            name2 = name.Substring(pos + 1)
            name = Left(name, pos)
            Dim lvalue As Object
            lvalue = GetLeftValue(name)
            Return ToObject(lvalue).GetValue(name2)
        End If
        Return GetSimpleValue(name)
    End Function

    Public Overridable Sub SetValue(ByVal name As String, ByVal value As Object) Implements IObject.SetValue
        Dim pos As Integer

        pos = name.IndexOf(Separator)

        Dim lvalue As Object

        If pos > 0 Then
            Dim name2 As String
            name2 = name.Substring(pos + 1)
            name = Left(name, pos)
            lvalue = GetLeftValue(name)
            ToObject(lvalue).SetValue(name2, value)
            Return
        End If

        value = ConvertValue(name, value)

        SetSimpleValue(name, value)
    End Sub

    MustOverride Function GetNames() As IList Implements IObject.GetNames

    Overridable Function AcceptValue(ByVal name As String) As Boolean Implements IObject.AcceptValue
        Return GetNames().Contains(name)
    End Function

    Overridable Sub Copy(ByVal source As Object) Implements IObject.Copy
        Objects.Copy(Me, ToObject(source))
    End Sub

    Overloads Function Equals(ByVal obj As Object) As Boolean Implements IObject.Equals
        Objects.Equals(Me, ToObject(obj))
    End Function

    Protected Function GetLeftObject(ByVal name As String, ByRef lastname As String) As IObject
        Dim obj As Object = Me

        Dim pos As Integer

        pos = name.IndexOf(Separator)

        While pos >= 0
            Dim name2 As String
            name2 = Left(name, pos)
            name = name.Substring(pos + 1)
            obj = ToObject(obj).GetLeftValue(name2)
            pos = name.IndexOf(Separator)
        End While

        lastname = name
        Return obj
    End Function

    Protected Overridable Sub AddSimpleValue(ByVal name As String, ByVal value As Object)
        Dim lvalue As Object

        lvalue = GetValue(name)

        If lvalue Is Nothing Or Not IsList(lvalue) Then
            lvalue = New GenericList
            SetValue(name, lvalue)
        End If

        ToList(lvalue).AddValue(value)
    End Sub

    Public Overridable Sub AddValue(ByVal name As String, ByVal value As Object) Implements IObject.AddValue
        Dim pos As Integer

        pos = name.IndexOf(Separator)

        Dim lvalue As Object

        If pos > 0 Then
            Dim name2 As String
            name2 = name.Substring(pos + 1)
            name = Left(name, pos)
            lvalue = GetLeftValue(name)
            ToObject(lvalue).AddValue(name2, value)
            Return
        End If

        value = ConvertValue(name, value)

        AddSimpleValue(name, value)
    End Sub

    Public Overridable Sub RemoveValue(ByVal name As String, ByVal value As Object) Implements IObject.RemoveValue
        Throw New NotImplementedException("RemoveValue no implementada")
    End Sub
End Class
