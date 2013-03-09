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

Module Utilities
    Function IsTrue(ByVal value As Object) As Boolean
        If value Is Nothing Then
            Return False
        End If

        If TypeOf value Is Boolean Then
            Return DirectCast(value, Boolean)
        End If

        If TypeOf value Is Integer Then
            If value = 0 Then
                Return False
            Else
                Return True
            End If
        End If

        If TypeOf value Is Short Then
            If value = 0 Then
                Return False
            Else
                Return True
            End If
        End If

        If TypeOf value Is Long Then
            If value = 0 Then
                Return False
            Else
                Return True
            End If
        End If

        If TypeOf value Is Single Then
            If value = 0 Then
                Return False
            Else
                Return True
            End If
        End If

        If TypeOf value Is Double Then
            If value = 0 Then
                Return False
            Else
                Return True
            End If
        End If

        '' TODO "false" as string must be evaluated to false?
        If TypeOf value Is String Then
            If value = "" Then
                Return False
            Else
                Return True
            End If
        End If

        Return True
    End Function

    Function EvalDefaultProperty(ByVal obj As Object, ByVal prop As String) As Object
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

        Return Nothing
    End Function

    Function EvalPropertyField(ByVal obj As Object, ByVal prop As String) As Object
        If obj Is Nothing Then
            Return Nothing
        End If

        Dim type As Type

        If TypeOf obj Is Type Then
            type = DirectCast(obj, Type)
        Else
            type = obj.GetType
        End If

        Try
            Return type.InvokeMember(prop, Reflection.BindingFlags.GetProperty Or Reflection.BindingFlags.GetField, Nothing, obj, Nothing)
        Catch ex As MissingFieldException
            Return EvalDefaultProperty(obj, prop)
        Catch ex As MissingMethodException
            Return EvalDefaultProperty(obj, prop)
        End Try
    End Function
End Module
