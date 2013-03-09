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

<TestFixture()> Public Class TestObjectXmlBuilder
    Private Function LoadObjectFromFile(ByVal fname As String) As Object
        Dim ob As New ObjectXmlBuilder

        Return ob.GetObject(fname)
    End Function

    <Test()> Sub TestObjectBuilder01()
        Dim dynobj As DynamicObject = LoadObjectFromFile("TestModels\Object01.xml")
        Assert.AreEqual("Adam", dynobj.GetValue("Name"))
        Assert.AreEqual("800", dynobj.GetValue("Age"))
    End Sub

    <Test()> Sub TestObjectBuilder02()
        Dim dynobj As DynamicObject = LoadObjectFromFile("TestModels\Object02.xml")
        Assert.AreEqual("Adam", dynobj.GetValue("Name"))
        Assert.AreEqual("800", dynobj.GetValue("Age"))
    End Sub

    <Test()> Sub TestObjectBuilder03()
        Dim dynobj As DynamicObject = LoadObjectFromFile("TestModels\Project01.xml")
        Assert.AreEqual("Project", dynobj.GetValue("TypeName"))
        Assert.AreEqual("AjFirstExample", dynobj.GetValue("Name"))
        Assert.AreEqual("First Example using AjGenesis", dynobj.GetValue("Description"))
        Assert.AreEqual("AjFE", dynobj.GetValue("Prefix"))
        Assert.AreEqual("com.ajlopez", dynobj.GetValue("Domain"))
        Assert.AreEqual("ajlopez", dynobj.GetValue("CompanyName"))
        Assert.IsNotNull(dynobj.GetValue("Model"))
        Assert.IsNull(dynobj.GetValue("AnyProperty"))
    End Sub

    <Test()> _
    Public Sub ShouldBuildListObjectFromXmlFileWithTwoElements()
        Dim obj As Object = LoadObjectFromFile("TestModels\Object03.xml")

        Assert.IsNotNull(obj)
        Assert.IsInstanceOfType(GetType(DynamicListObject), obj)

        Dim dynlist As DynamicListObject = DirectCast(obj, DynamicListObject)

        Assert.AreEqual(2, dynlist.GetList().Count)

        Dim dynobj As DynamicObject = dynlist.GetList()(0)

        Assert.IsNotNull(dynobj)
        Assert.AreEqual("Entity", dynobj.GetValue("TypeName"))
        Assert.AreEqual("Customer", dynobj.GetValue("Name"))

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
    Public Sub ShouldBuildListObjectFromSimpleHbm()
        Dim obj As Object = LoadObjectFromFile("TestModels\Customer.hbm.xml")

        Assert.IsNotNull(obj)
        Assert.IsInstanceOfType(GetType(DynamicObject), obj)

        Dim mapping As DynamicObject = DirectCast(obj, DynamicObject)

        Assert.AreEqual("hibernate-mapping", mapping.GetValue("TypeName"))

        Dim classresult As Object = mapping.GetValue("class")

        Assert.IsNotNull(classresult)
        Assert.IsInstanceOfType(GetType(DynamicListObject), classresult)

        Assert.AreEqual("com.ajlopez.ajfirstexample.model.Customer", mapping.GetValue("class.name"))
        Assert.AreEqual("customers", mapping.GetValue("class.table"))
        Assert.IsNotNull(mapping.GetValue("class.id"))
    End Sub
End Class
