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

Imports System.Reflection

Public Class ValueObject
    Inherits BaseObject

    Private mObject As Object

    Public Sub New(ByVal obj As Object)
        mObject = obj
    End Sub

    Public Function GetObject() As Object
        Dim obj As Object

        obj = mObject

        While TypeOf obj Is ValueObject
            obj = CType(obj, ValueObject).GetObject
        End While

        Return obj
    End Function

    Protected Overrides Function GetLeftValue(ByVal name As String) As Object
        Dim fi As FieldInfo
        Dim value As Object

        value = GetSimpleValue(name)

        If Not value Is Nothing Then
            Return value
        End If

        fi = mObject.GetType.GetField(name)

        If Not fi Is Nothing Then
            value = System.Activator.CreateInstance(fi.FieldType)
            SetPropertyField(mObject, name, value)
            Return value
        End If

        Dim pi As PropertyInfo

        pi = mObject.GetType.GetProperty(name)

        If Not pi Is Nothing Then
            value = System.Activator.CreateInstance(pi.PropertyType)
            SetPropertyField(mObject, name, value)
            Return value
        End If

        Throw New ArgumentException("Dato " + name + " desconocido")
    End Function

    Protected Overrides Sub SetSimpleValue(ByVal name As String, ByVal value As Object)
        SetPropertyField(mObject, name, value)
    End Sub

    Protected Overrides Function GetSimpleValue(ByVal name As String) As Object
        Return GetPropertyField(mObject, name)
    End Function

    Public Overrides Function GetNames() As IList
        Return Types.GetNames(mObject.GetType)
    End Function

    Function GetDefaultProperty(ByVal obj As Object, ByVal prop As String) As Object
        Dim type As Type

        type = obj.GetType

        Dim members() As MemberInfo

        members = type.GetDefaultMembers()

        Dim member As MemberInfo

        For Each member In members
            If member.MemberType = MemberTypes.Property Then
                Return type.InvokeMember(member.Name, BindingFlags.GetProperty, Nothing, obj, New Object() {prop})
            End If
        Next

        Throw New ArgumentException("Dato " + prop + " desconocido")
    End Function

    Function GetPropertyField(ByVal obj As Object, ByVal prop As String) As Object
        If obj Is Nothing Then
            Return Nothing
        End If

        Dim type As Type

        type = obj.GetType

        Try
            Return type.InvokeMember(prop, Reflection.BindingFlags.GetProperty Or Reflection.BindingFlags.GetField, Nothing, obj, Nothing)
        Catch ex As MissingFieldException
            Return GetDefaultProperty(obj, prop)
        Catch ex As MissingMethodException
            Return GetDefaultProperty(obj, prop)
        End Try
    End Function

    Sub SetPropertyField(ByVal obj As Object, ByVal prop As String, ByVal value As Object)
        If obj Is Nothing Then
            Throw New NullReferenceException("No hay Objecto en ObjectValue")
        End If

        Dim type As type

        type = obj.GetType

        Try
            type.InvokeMember(prop, Reflection.BindingFlags.SetProperty Or Reflection.BindingFlags.SetField, Nothing, obj, New Object() {value})
        Catch ex As MissingFieldException
            SetDefaultProperty(obj, prop, value)
        Catch ex As MissingMethodException
            SetDefaultProperty(obj, prop, value)
        End Try
    End Sub

    Sub SetDefaultProperty(ByVal obj As Object, ByVal prop As String, ByVal value As Object)
        Dim type As type

        type = obj.GetType

        Dim members() As MemberInfo

        members = type.GetDefaultMembers()

        Dim member As MemberInfo

        For Each member In members
            If member.MemberType = MemberTypes.Property Then
                type.InvokeMember(member.Name, BindingFlags.SetProperty, Nothing, obj, New Object() {prop, value})
                Return
            End If
        Next

        Throw New ArgumentException("No se encontró campo o propiedad " + prop + " en " + obj.GetType().ToString)
    End Sub
End Class
