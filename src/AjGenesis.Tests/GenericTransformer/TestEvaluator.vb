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

Imports AjGenesis
Imports AjGenesis.Transformers.GenericTransformer

Imports NUnit.Framework

<TestFixture()> Public Class TestEvaluator
    <Test()> Sub TestEvaluator01()
        Dim ev As New Evaluator()
        ev("Nombre") = "Adan"
        ev("Apellido") = "NN"

        Assert.AreEqual("Adan", ev("Nombre"))
        Assert.AreEqual("NN", ev("Apellido"))
        Assert.IsNull(ev("Cosa"))
    End Sub

    <Test()> Sub TestEvaluator02()
        Dim ev As New Evaluator()
        Dim p As New Person()

        p.FirstName = "Adam"
        p.LastName = "Doe"
        p.Age = 500

        ev("p") = p
        Assert.AreEqual("Adam", ev.Evaluate("p.FirstName"))
        Assert.AreEqual("Doe", ev.Evaluate("p.LastName"))
        Assert.AreEqual(500, ev.Evaluate("p.Age"))
        Assert.AreEqual(4, ev.Evaluate("p.FirstName.Length"))
    End Sub

    <Test()> Sub TestEvaluator03()
        Dim ev As New Evaluator
        Dim p As New Person

        p.FirstName = "Adam"
        p.LastName = "Doe"
        p.Age = 500

        ev("p") = p
        Assert.AreEqual("Adam", ev.Evaluate("p.(""FirstName"")"))
        Assert.AreEqual("Adam", ev.Evaluate("p.(""First"" & ""Name"")"))
        Assert.AreEqual("Doe", ev.Evaluate("p.(""LastName"")"))
        Assert.AreEqual(500, ev.Evaluate("p.(""Age"")"))
        Assert.AreEqual(4, ev.Evaluate("p.(""FirstName"").(""Length"")"))
    End Sub

    <Test()> Sub TestExpressions01()
        Dim ev As New Evaluator

        Assert.AreEqual(1, ev.Evaluate("1"))
        Assert.AreEqual(1, ev.Evaluate("1+0"))
        Assert.AreEqual(3, ev.Evaluate("1+2"))
        Assert.AreEqual(7, ev.Evaluate("1+2*3"))
        Assert.AreEqual(9, ev.Evaluate("(1+2)*3"))
        Assert.AreEqual(12, ev.Evaluate("4*(1+2)"))
    End Sub

    <Test()> Sub TestStrings01()
        Dim ev As New Evaluator

        Assert.AreEqual("1", ev.Evaluate("""${1}"""))
        Assert.AreEqual("1", ev.Evaluate("""${1+0}"""))
        Assert.AreEqual("3", ev.Evaluate("""${1+2}"""))
        Assert.AreEqual("7", ev.Evaluate("""${1+2*3}"""))
        Assert.AreEqual("9", ev.Evaluate("""${(1+2)*3}"""))
        Assert.AreEqual("12", ev.Evaluate("""${4*(1+2)}"""))
    End Sub

    <Test()> Sub TestFunction01()
        Dim ev As New Evaluator
        Dim p As New Person

        p.FirstName = "Adam"
        p.LastName = "Doe"
        p.Age = 500

        ev("p") = p
        Assert.AreEqual("Adam", ev.Evaluate("p.FirstName.ToString()"))
        Assert.AreEqual("Doe", ev.Evaluate("p.LastName.ToString()"))
        Assert.AreEqual("500", ev.Evaluate("p.Age.ToString()"))
        Assert.AreEqual("4", ev.Evaluate("p.FirstName.Length.ToString()"))
    End Sub

    <Test()> Sub TestFunction03()
        Dim ev As New Evaluator
        Dim p As New Person

        p.FirstName = "Adam"
        p.LastName = "Doe"
        p.Age = 500

        ev("p") = p
        Assert.AreEqual("Adam", ev.Evaluate("p.(""FirstName"").ToString()"))
        Assert.AreEqual("Doe", ev.Evaluate("p.(""LastName"").ToString()"))
        Assert.AreEqual("500", ev.Evaluate("p.(""Age"").ToString()"))
        Assert.AreEqual("4", ev.Evaluate("p.(""FirstName"").Length.ToString()"))
    End Sub

    Private Function ToUpper(ByVal args() As Object) As Object
        Return UCase(args(0).ToString())
    End Function

    <Test()> Sub TestFunction02()
        Dim ev As New Evaluator
        Dim name As String

        name = "Adam"

        ev("name") = "Adam"
        ev("ToUpper") = New CallDelegate(AddressOf ToUpper)
        Assert.AreEqual("ADAM", ev.Evaluate("ToUpper(name)"))
    End Sub

    <Test()> Sub TestNew01()
        Dim ev As New Evaluator
        Assert.AreEqual(GetType(System.Collections.ArrayList), ev.Evaluate("new System.Collections.ArrayList").GetType())
    End Sub

    <Test()> Sub TestNew02()
        Dim ev As New Evaluator
        Assert.AreEqual(GetType(Person), ev.Evaluate("new AjGenesis.Tests.Person").GetType())
    End Sub

    <Test()> Sub TestNew02b()
        Dim ev As New Evaluator
        Dim obj As Object = ev.Evaluate("new AjGenesis.Tests.Person(""Adam"")")
        Assert.IsInstanceOfType(GetType(Person), obj)
        Dim p As Person = DirectCast(obj, Person)
        Assert.AreEqual("Adam", p.FirstName)
    End Sub

    <Test()> Sub TestNew02c()
        Dim ev As New Evaluator
        Dim obj As Object = ev.Evaluate("new System.IO.DirectoryInfo(""."")")
        Assert.IsNotNull(obj)
        Assert.IsInstanceOfType(GetType(System.IO.DirectoryInfo), obj)
    End Sub

    <Test()> Sub TestNew03()
        Dim ev As New Evaluator
        Assert.AreEqual("DataSet", ev.Evaluate("new System.Data.DataSet").GetType().Name)
    End Sub

    <Test()> Sub TestStatic01()
        Dim ev As New Evaluator
        Assert.AreEqual(GetType(System.String), ev.Evaluate("System.Type.GetType(""System.String"")"))
    End Sub

    <Test()> Sub TestOr01()
        Dim ev As New Evaluator
        Assert.AreEqual(True, ev.Evaluate("true or false"))
    End Sub

    <Test()> Sub TestOr02()
        Dim ev As New Evaluator
        Assert.AreEqual(True, ev.Evaluate("1 or 0"))
    End Sub

    <Test()> Sub TestAnd01()
        Dim ev As New Evaluator
        Assert.AreEqual(False, ev.Evaluate("true and false"))
    End Sub

    <Test()> Sub TestAnd02()
        Dim ev As New Evaluator
        Assert.AreEqual(False, ev.Evaluate("1 and 0"))
    End Sub

    <Test()> Sub ShouldEvaluateMinusOne()
        Dim ev As New Evaluator
        Assert.AreEqual(-1, ev.Evaluate("-1"))
    End Sub

    <Test()> Sub ShouldEvaluateMinusTwoByMinusOne()
        Dim ev As New Evaluator
        Assert.AreEqual(-2, ev.Evaluate("2*-1"))
    End Sub

    <Test()> Sub EvaluateEnumValue()
        Dim ev As New Evaluator
        Assert.IsNotNull(ev.Evaluate("System.Text.Encoding.ASCII"))
    End Sub
End Class
