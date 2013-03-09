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

Imports System.Windows.Forms

Imports Flobbster.Windows.Forms

Imports AjGenesis.Models.DynamicModel

Module Utilities
    Function CreateNode(ByVal obj As IObject) As TreeNode
        Dim node As New TreeNode

        Dim typename As String
        Dim name As String

        typename = obj.GetValue("TypeName")
        name = obj.GetValue("Name")
        If name Is Nothing Then
            name = obj.GetValue("name")
        End If
        If name Is Nothing Then
            name = obj.GetValue("Id")
        End If
        If name Is Nothing Then
            name = obj.GetValue("id")
        End If

        If Not name Is Nothing Then
            node.Text = typename + " " + name
        Else
            node.Text = typename
        End If

        node.Tag = obj

        For Each name In obj.GetNames
            Dim value As Object
            value = obj.GetValue(name)
            If TypeOf value Is IObject Then
                node.Nodes.Add(CreateNode(value))
            Else
                'node.Nodes.Add(New TreeNode(name + ": " + value.ToString()))
            End If
        Next

        If TypeOf obj Is IListObject Then
            Dim list As IListObject = DirectCast(obj, IListObject)
            Dim child As Object

            For Each child In list.GetList
                node.Nodes.Add(CreateNode(child))
            Next
        End If

        Return node
    End Function

    Public Sub LoadTree(ByVal tree As TreeView, ByVal obj As IObject)
        tree.Nodes.Add(CreateNode(obj))
    End Sub

    Public Sub ShowData(ByVal lv As ListView, ByVal obj As IObject)
        lv.Items.Clear()

        Dim name As String

        For Each name In obj.GetNames
            If name <> "TypeName" Then
                Dim value As Object
                value = obj.GetValue(name)
                If Not TypeOf value Is IObject Then
                    lv.Items.Add(New ListViewItem(New String() {name, value}))
                End If
            End If
        Next
    End Sub


    Public Sub ShowData2(ByVal grid As PropertyGrid, ByVal obj As IObject)
        Dim pb As New PropertyBag
        Dim pm As New PropertyManager(obj)

        Dim name As String

        For Each name In obj.GetNames
            If name <> "TypeName" Then
                Dim value As Object
                value = obj.GetValue(name)
                If Not TypeOf value Is IObject Then
                    pb.Properties.Add(New PropertySpec(name, GetType(System.String), "Values"))
                End If
            End If
        Next

        AddHandler pb.GetValue, New PropertySpecEventHandler(AddressOf pm.GetValue)
        AddHandler pb.SetValue, New PropertySpecEventHandler(AddressOf pm.SetValue)

        grid.SelectedObject = pb
    End Sub
End Module

Class PropertyManager
    Private mObject As IObject

    Sub New(ByVal obj As IObject)
        mObject = obj
    End Sub

    Public Sub GetValue(ByVal sender As Object, ByVal e As PropertySpecEventArgs)
        Dim value As Object
        value = mObject.GetValue(e.Property.Name)
        If value Is Nothing Then
            e.Value = ""
        Else
            e.Value = value.ToString()
        End If
    End Sub

    Public Sub SetValue(ByVal sender As Object, ByVal e As PropertySpecEventArgs)
        mObject.SetValue(e.Property.Name, e.Value)
    End Sub
End Class