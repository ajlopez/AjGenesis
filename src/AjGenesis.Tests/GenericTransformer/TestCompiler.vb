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

Imports System.Collections.Generic
Imports System.IO

Imports AjGenesis.Core
Imports AjGenesis.Transformers.GenericTransformer

Imports NUnit.Framework
Imports AjGenesis.Models.DynamicModel

<TestFixture()> Public Class TestCompiler
    Private Sub CompileNode(ByVal text As String, ByVal nodeName As String)
        Dim cmp As New Compiler(text)
        Dim node As node = cmp.Compile()

        Assert.AreEqual(nodeName, node.GetType().Name)
    End Sub

    Private Function CompileExpression(ByVal text As String) As ExpressionNode
        Dim cmp As New Compiler(text)
        Return cmp.Compile
    End Function

    <Test()> Sub TestTokenizerNode01()
        CompileNode("a", "NameNode")
    End Sub

    <Test()> Sub TestTokenizerNode01b()
        CompileNode("[any name]", "NameNode")
    End Sub

    <Test()> Sub TestTokenizerNode01c()
        CompileNode("[ns:name]", "NameNode")
    End Sub

    <Test()> Sub TestTokenizerNode02()
        CompileNode("1", "IntegerNode")
    End Sub

    <Test()> Sub TestTokenizerNode03()
        CompileNode("""Es Texto""", "StringNode")
    End Sub

    <Test()> Sub TestTokenizerNode04()
        CompileNode("'Es Texto'", "QuoteNode")
    End Sub

    <Test()> Sub TestTokenizerNode05()
        CompileNode("a+b", "BinaryOperatorNode")
    End Sub

    <Test()> Sub TestTokenizerNode06()
        CompileNode("a-b", "BinaryOperatorNode")
    End Sub

    <Test()> Sub TestTokenizerNode07()
        CompileNode("a+1*2", "BinaryOperatorNode")
    End Sub

    <Test()> Sub TestTokenizerNode08()
        CompileNode("sin(x)", "CallNode")
    End Sub

    <Test()> Sub TestTokenizerNode09()
        CompileNode("function(x,y)", "CallNode")
    End Sub

    <Test()> Sub TestTokenizerNode10()
        CompileNode("a.b", "DotNode")
    End Sub

    <Test()> Sub CompileCall()
        CompileNode("a.b()", "CallNode")
    End Sub

    <Test()> Sub CompileComposedDot()
        CompileNode("a.b.c", "DotNode")
    End Sub

    <Test()> Sub CompileCallAndDot()
        CompileNode("a.b().c", "DotNode")
    End Sub

    <Test()> Sub TestTokenizerNode10b()
        CompileNode("a.(b)", "DotExNode")
    End Sub

    <Test()> Sub TestTokenizerNode10c()
        CompileNode("a.(""b"")", "DotExNode")
    End Sub

    <Test()> Sub TestTokenizerNode11()
        CompileNode("a or b", "OrOperatorNode")
    End Sub

    <Test()> Sub TestTokenizerNode12()
        CompileNode("a and b", "AndOperatorNode")
    End Sub

    Private Sub CompileSingleCommand(ByVal text As String)
        Dim cmp As New Compiler(text)
        cmp.CompileSingleCommand()
    End Sub

    <Test()> Sub TestCommands01()
        CompileSingleCommand("print 1")
        CompileSingleCommand("set a = 1")
        CompileSingleCommand("if a=1 then")
    End Sub

    <Test()> Sub TestCommands02()
        CompileSingleCommand("a = 1")
        CompileSingleCommand("a = b")
        CompileSingleCommand("a = b+1")
    End Sub

    <Test()> Sub TestCommands03()
        CompileSingleCommand("a.c = 1")
        CompileSingleCommand("a.c = b")
        CompileSingleCommand("a.c = b+1")
    End Sub

    <Test()> Sub TestCommands04()
        CompileSingleCommand("for each k in ks")
    End Sub

    <Test()> Sub TestCommands04b()
        CompileSingleCommand("for each k in ks where k>0")
    End Sub

    <Test()> Sub TestCommands05()
        CompileSingleCommand("sks = select from k in ks")
    End Sub

    <Test()> Sub TestCommands05b()
        CompileSingleCommand("sks = select from k in ks where k>0")
    End Sub

    <Test()> Sub TestCommandIf01()
        CompileSingleCommand("if a=1 then")
    End Sub

    <Test()> Sub TestCommandIf02()
        CompileSingleCommand("if a>=1 then")
    End Sub

    <Test()> Sub TestCommandIf03()
        CompileSingleCommand("if a<=1 then")
    End Sub

    <Test()> Sub TestCommandIf04()
        CompileSingleCommand("if a<>1 then")
    End Sub

    <Test()> Sub TestCommandIf05()
        CompileSingleCommand("if a<1 then")
    End Sub

    <Test()> Sub TestCommandIf06()
        CompileSingleCommand("if a>1 then")
    End Sub

    <Test()> _
    Sub AssignValue()
        Dim env As New Environment
        env.Item("Person") = New DynamicObject()
        ExecuteCommand("Person.Name = ""Adam""", env)
        Assert.IsNotNull(env.Item("Person"))
        Assert.AreEqual("Adam", Evaluate("Person.Name", env))
    End Sub

    <Test()> _
    Sub AssignValueToUndefinedObject()
        Dim env As New Environment
        ExecuteCommand("Person.Name = ""Adam""", env)
        Assert.IsNotNull(env.Item("Person"))
        Assert.AreEqual("Adam", Evaluate("Person.Name", env))
    End Sub

    <Test()> _
    Sub AssignValueToUndefinedObjects()
        Dim env As New Environment
        ExecuteCommand("Person.Parent.Name = ""Adam""", env)
        Assert.IsNotNull(env.Item("Person"))
        Assert.AreEqual("Adam", Evaluate("Person.Parent.Name", env))
    End Sub

    <Test()> _
    Sub AssignValueToPartiallyUndefinedObjects()
        Dim env As New Environment
        ExecuteCommand("Person.Name = ""Caine""", env)
        Assert.AreEqual("Caine", Evaluate("Person.Name", env))
        ExecuteCommand("Person.Parent.Name = ""Adam""", env)
        Assert.IsNotNull(env.Item("Person"))
        Assert.AreEqual("Adam", Evaluate("Person.Parent.Name", env))
        Assert.AreEqual("Caine", Evaluate("Person.Name", env))
    End Sub

    <Test()> _
    Sub CompileErrorCommand()
        Dim cmd As CommandNode = CompileCommand("Error ""Error Message""")
        Assert.IsNotNull(cmd)
        Assert.IsInstanceOfType(GetType(ErrorNode), cmd)
        Dim errcmd As ErrorNode = DirectCast(cmd, ErrorNode)
        Assert.IsNotNull(errcmd.Expression)
        Assert.AreEqual("Error Message", errcmd.Expression.Evaluate(Nothing))
    End Sub

    Private Function ExecuteCommand(ByVal text As String) As String
        Dim env As New Environment
        Dim output As StringWriter = New StringWriter
        env.Processor.Output = output
        ExecuteCommand(text, env)
        output.Close()
        Return output.ToString
    End Function

    Private Function ExecuteCommand(ByVal cmd As CommandNode) As String
        Dim env As New Environment
        Dim output As StringWriter = New StringWriter
        env.Processor.Output = output
        cmd.Execute(env)
        output.Close()
        Return output.ToString
    End Function

    Private Function ExecuteCommand(ByVal cmd As CommandNode, ByVal env As Environment) As String
        Dim output As StringWriter = New StringWriter
        env.Processor.Output = output
        cmd.Execute(env)
        output.Close()
        Return output.ToString
    End Function

    Private Sub ExecuteCommand(ByVal text As String, ByVal env As Environment)
        Dim cmp As New Compiler(text)
        Dim cmd As CommandNode = cmp.CompileSingleCommand()
        cmd.Execute(env)
    End Sub

    <Test()> Sub TestExecuteCommands01()
        Assert.AreEqual("1", ExecuteCommand("print 1"))
        Assert.AreEqual("12", ExecuteCommand("print 12"))
        Assert.AreEqual("3", ExecuteCommand("print 1+2"))
        Assert.AreEqual("6", ExecuteCommand("print 2*3"))
        Assert.AreEqual("7", ExecuteCommand("print 1+2*3"))
        Assert.AreEqual("9", ExecuteCommand("print (1+2)*3"))
    End Sub

    <Test()> Sub TestExecuteCommands02()
        Dim env As New Environment
        Dim output As StringWriter = New StringWriter
        env.Processor.Output = output
        ExecuteCommand("set a = 1", env)
        Assert.AreEqual(1, env("a"))
        ExecuteCommand("set b = a+1", env)
        Assert.AreEqual(2, env("b"))
    End Sub

    Private Function CompileCommand(ByVal text As String) As CommandNode
        Dim cmp As New Compiler(text)
        Return cmp.CompileSingleCommand()
    End Function

    <Test()> Sub TestExecuteCommands03()
        Dim env As New Environment
        Dim output As StringWriter = New StringWriter
        env.Processor.Output = output
        Dim cmd As New CommandListNode
        cmd.AddCommand(CompileCommand("print 1"))
        cmd.AddCommand(CompileCommand("print 2"))
        Assert.AreEqual("12", ExecuteCommand(cmd))
    End Sub

    <Test()> Sub TestExecuteCommands04()
        Dim env As New Environment
        Dim output As StringWriter = New StringWriter
        env.Processor.Output = output
        Dim cmdif As New IfNode
        Dim cmdthen As New CommandListNode
        Dim cmdelse As New CommandListNode
        cmdif.Expression = CompileExpression("1")
        cmdthen.AddCommand(CompileCommand("print 1"))
        cmdelse.AddCommand(CompileCommand("print 2"))
        cmdif.ThenCommand = cmdthen
        cmdif.ElseCommand = cmdelse
        Assert.AreEqual("1", ExecuteCommand(cmdif))
        cmdif.Expression = CompileExpression("0")
        Assert.AreEqual("2", ExecuteCommand(cmdif))
    End Sub

    <Test()> Sub ShouldExecuteForEachOnList()
        Dim env As New Environment
        Dim output As StringWriter = New StringWriter
        env.Processor.Output = output
        Dim cmdforeach As New ForEachNode
        cmdforeach.Variable = "Name"
        cmdforeach.Expression = CompileExpression("Names")
        Dim cmds As New CommandListNode
        cmds.AddCommand(CompileCommand("print Name"))
        cmdforeach.Commands = cmds
        Dim names As New List(Of String)
        names.Add("Foo")
        names.Add("Bar")
        env.Item("Names") = names
        Assert.AreEqual("FooBar", ExecuteCommand(cmdforeach, env))
    End Sub

    <Test()> Sub ShouldExecuteForEachOnArray()
        Dim env As New Environment
        Dim output As StringWriter = New StringWriter
        env.Processor.Output = output
        Dim cmdforeach As New ForEachNode
        cmdforeach.Variable = "Name"
        cmdforeach.Expression = CompileExpression("Names")
        Dim cmds As New CommandListNode
        cmds.AddCommand(CompileCommand("print Name"))
        cmdforeach.Commands = cmds
        Dim names(1) As String
        names(0) = "Foo"
        names(1) = "Bar"
        env.Item("Names") = names
        Assert.AreEqual("FooBar", ExecuteCommand(cmdforeach, env))
    End Sub

    Private Function Evaluate(ByVal expression As String, ByVal env As Environment) As Object
        Dim comp As New Compiler(expression)

        Return comp.Compile().Evaluate(env)
    End Function
End Class
