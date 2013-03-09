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

Imports System.IO

Imports AjGenesis
Imports AjGenesis.Models.DynamicModel
Imports NUnit.Framework

<TestFixture()> Public Class TestObjectTextBuilder
    <Test()> _
    Public Sub TestCreate()
        Dim otb As New ObjectTextBuilder()

        Assert.IsNotNull(otb)
    End Sub

    <Test()> _
    Public Sub TestCreateObject01()
        Dim otb As New ObjectTextBuilder()

        Dim obj As Object = otb.GetObject("Entity" & vbCrLf & "End Entity")

        Assert.IsNotNull(obj)
        Assert.IsInstanceOfType(GetType(DynamicObject), obj)

        Dim dynobj As DynamicObject = DirectCast(obj, DynamicObject)

        Assert.AreEqual("Entity", dynobj.GetValue("TypeName"))
    End Sub

    <Test()> _
    Public Sub TestCreateObject02()
        Dim otb As New ObjectTextBuilder()

        Dim obj As Object = otb.GetObject("")

        Assert.IsNull(obj)
    End Sub


    <Test()> _
    Public Sub TestCreateObject03()
        Dim otb As New ObjectTextBuilder()

        Dim obj As Object = otb.GetObject("Entity Customer" & vbCrLf & "End Entity")

        Assert.IsNotNull(obj)
        Assert.IsInstanceOfType(GetType(DynamicObject), obj)

        Dim dynobj As DynamicObject = DirectCast(obj, DynamicObject)

        Assert.AreEqual("Entity", dynobj.GetValue("TypeName"))

        Assert.AreEqual("Customer", dynobj.GetValue("Name"))
    End Sub

    <Test(), ExpectedException(GetType(Exception))> _
    Public Sub TestShouldRaiseException01()
        Dim otb As New ObjectTextBuilder()

        Dim obj As Object = otb.GetObject("10")
    End Sub

    <Test(), ExpectedException(GetType(Exception))> _
    Public Sub TestShouldRaiseException02()
        Dim otb As New ObjectTextBuilder()

        Dim obj As Object = otb.GetObject("=")
    End Sub

    <Test()> _
    Public Sub ShouldBuildObjectWithParameters()
        Dim otb As New ObjectTextBuilder()

        Dim obj As Object = otb.GetObject("Entity Customer Address=""Customer Address"" Code=""Customer Code""")

        Assert.IsNotNull(obj)
        Assert.IsInstanceOfType(GetType(DynamicObject), obj)

        Dim dynobj As DynamicObject = DirectCast(obj, DynamicObject)

        Assert.AreEqual("Entity", dynobj.GetValue("TypeName"))

        Assert.AreEqual("Customer", dynobj.GetValue("Name"))
        Assert.AreEqual("Customer Address", dynobj.GetValue("Address"))
        Assert.AreEqual("Customer Code", dynobj.GetValue("Code"))
    End Sub

    <Test()> _
    Public Sub ShouldBuildMultiLineObject()
        Dim otb As New ObjectTextBuilder()

        Dim obj As Object = otb.GetObject("Entity Customer" & vbCrLf & _
            "Code = ""Customer Code""" & vbCrLf & _
            "Address = ""Customer Address""" & vbCrLf & _
            "End Entity")

        Assert.IsNotNull(obj)
        Assert.IsInstanceOfType(GetType(DynamicObject), obj)

        Dim dynobj As DynamicObject = DirectCast(obj, DynamicObject)

        Assert.AreEqual("Entity", dynobj.GetValue("TypeName"))

        Assert.AreEqual("Customer", dynobj.GetValue("Name"))
        Assert.AreEqual("Customer Address", dynobj.GetValue("Address"))
        Assert.AreEqual("Customer Code", dynobj.GetValue("Code"))
    End Sub

    <Test()> _
    Public Sub ShouldBuildObjectFromFile()
        Dim otb As New ObjectTextBuilder()

        Dim obj As Object = otb.GetObjectFromFile("TestModels\Object01.txt")

        Assert.IsNotNull(obj)
        Assert.IsInstanceOfType(GetType(DynamicObject), obj)

        Dim dynobj As DynamicObject = DirectCast(obj, DynamicObject)

        Assert.AreEqual("Person", dynobj.GetValue("TypeName"))

        Assert.AreEqual("Adam", dynobj.GetValue("Name"))
        Assert.AreEqual("800", dynobj.GetValue("Age"))
    End Sub

    <Test()> _
    Public Sub ShouldBuildListObjectFromFile()
        Dim otb As New ObjectTextBuilder()

        Dim obj As Object = otb.GetObjectFromFile("TestModels\Object02.txt")

        Assert.IsNotNull(obj)
        Assert.IsInstanceOfType(GetType(DynamicListObject), obj)

        Dim dynlist As DynamicListObject = DirectCast(obj, DynamicListObject)

        Assert.AreEqual(1, dynlist.GetList().Count)

        Dim dynobj As DynamicObject = dynlist.GetList()(0)

        Assert.IsNotNull(dynobj)
        Assert.AreEqual("Entity", dynobj.GetValue("TypeName"))
        Assert.AreEqual("Customer", dynobj.GetValue("Name"))
    End Sub

    <Test()> _
    Public Sub ShouldBuildListObjectFromFileWithTwoElements()
        Dim otb As New ObjectTextBuilder()

        Dim obj As Object = otb.GetObjectFromFile("TestModels\Object03.txt")

        Assert.IsNotNull(obj)
        Assert.IsInstanceOfType(GetType(DynamicListObject), obj)

        Dim dynlist As DynamicListObject = DirectCast(obj, DynamicListObject)

        Assert.AreEqual(2, dynlist.GetList().Count)

        Dim dynobj As DynamicObject = dynlist.GetList()(0)

        Assert.IsNotNull(dynobj)
        Assert.AreEqual("Entity", dynobj.GetValue("TypeName"))
        Assert.AreEqual("Customer", dynobj.GetValue("Name"))

        dynobj = dynlist.GetList()(1)

        Assert.IsNotNull(dynobj)
        Assert.AreEqual("Entity", dynobj.GetValue("TypeName"))
        Assert.AreEqual("Supplier", dynobj.GetValue("Name"))
    End Sub

    <Test()> _
    Public Sub ShouldBuildListObjectFromFileWithManyLevels()
        Dim otb As New ObjectTextBuilder()

        Dim obj As Object = otb.GetObjectFromFile("TestModels\Object04.txt")

        Assert.IsNotNull(obj)
        Assert.IsInstanceOfType(GetType(DynamicListObject), obj)

        Dim dynlist As DynamicListObject = DirectCast(obj, DynamicListObject)

        Assert.AreEqual(2, dynlist.GetList().Count)

        Dim dynobj As DynamicObject = dynlist.GetList()(0)

        Assert.IsNotNull(dynobj)
        Assert.AreEqual("Entity", dynobj.GetValue("TypeName"))
        Assert.AreEqual("Customer", dynobj.GetValue("Name"))
        Assert.AreEqual("Customer Entity", dynobj.GetValue("Description"))

        Dim dynlist2 As DynamicListObject

        dynlist2 = dynobj.GetValue("Properties")

        Assert.IsNotNull(dynlist2)
        Assert.AreEqual(3, dynlist2.GetList().Count)

        Dim dynobj2 As DynamicObject

        dynobj2 = dynlist2.GetList()(0)

        Assert.IsNotNull(dynobj2)
        Assert.AreEqual("Property", dynobj2.GetValue("TypeName"))
        Assert.AreEqual("Id", dynobj2.GetValue("Type"))
        Assert.AreEqual("Id", dynobj2.GetValue("Name"))

        dynobj = dynlist.GetList()(1)

        Assert.IsNotNull(dynobj)
        Assert.AreEqual("Entity", dynobj.GetValue("TypeName"))
        Assert.AreEqual("Supplier", dynobj.GetValue("Name"))
        Assert.AreEqual("Supplier Entity", dynobj.GetValue("Description"))

        dynlist2 = dynobj.GetValue("Properties")

        Assert.IsNotNull(dynlist2)
        Assert.AreEqual(3, dynlist2.GetList().Count)

        dynobj2 = dynlist2.GetList()(0)

        Assert.IsNotNull(dynobj2)
        Assert.AreEqual("Property", dynobj2.GetValue("TypeName"))
        Assert.AreEqual("Id", dynobj2.GetValue("Type"))
        Assert.AreEqual("Id", dynobj2.GetValue("Name"))

        dynobj = dynlist.GetList()(1)
    End Sub

    <Test()> _
    Public Sub ShouldBuildListOfSimpleValues()
        Dim otb As New ObjectTextBuilder()

        Dim obj As Object = otb.GetObjectFromFile("TestModels\Messages.txt")

        Assert.IsNotNull(obj)
        Assert.IsInstanceOfType(GetType(DynamicListObject), obj)

        Dim dynlist As DynamicListObject = DirectCast(obj, DynamicListObject)

        Assert.AreEqual(3, dynlist.GetList().Count)

        Assert.AreEqual("Message1", dynlist.GetList()(0))
        Assert.AreEqual("Message2", dynlist.GetList()(1))
        Assert.AreEqual("Message3", dynlist.GetList()(2))
    End Sub
End Class

