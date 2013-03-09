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
Imports System.Collections.Generic

Imports AjGenesis.Core
Imports AjGenesis.Transformers.GenericTransformer

Imports NUnit.Framework

<TestFixture()> Public Class TestTokenizer
    Private Function Tokenize(ByVal text As String) As List(Of Token)
        Dim tokens As New List(Of Token)
        Dim tokenizer As New Tokenizer(text)

        Dim token As Token

        token = tokenizer.NextToken

        While Not token Is Nothing
            tokens.Add(token)
            token = tokenizer.NextToken
        End While

        Return tokens
    End Function

    Private Function TokenizeWithLines(ByVal text As String) As List(Of Token)
        Dim tokens As New List(Of Token)
        Dim tokenizer As New Tokenizer(text)
        tokenizer.UseLines = True

        Dim token As Token

        token = tokenizer.NextToken

        While Not token Is Nothing
            tokens.Add(token)
            token = tokenizer.NextToken
        End While

        Return tokens
    End Function

    Private Sub GetToken(ByVal text As String, ByVal tokenvalue As String, ByVal tokentype As TokenType)
        Dim tokens As List(Of Token) = Tokenize(text)

        Assert.IsNotNull(tokens)
        Assert.AreEqual(1, tokens.Count)

        If Not tokenvalue Is Nothing Then
            Assert.AreEqual(tokenvalue, tokens(0).Value)
        End If

        Assert.AreEqual(tokentype, tokens(0).Type)
    End Sub

    <Test()> Sub ShouldGetNameToken()
        GetToken("a", "a", TokenType.TokName)
    End Sub

    <Test()> Sub ShouldGetSpecialNameToken()
        GetToken("[any name]", "any name", TokenType.TokName)
    End Sub

    <Test()> Sub ShouldGetCompositeNameToken()
        GetToken("[ns:name]", "ns:name", TokenType.TokName)
    End Sub

    <Test()> Sub ShouldGetIntegerToken()
        GetToken("1", "1", TokenType.TokInteger)
    End Sub

    <Test()> Sub ShouldGetStringToken()
        GetToken("""Es Texto""", "Es Texto", TokenType.TokString)
    End Sub

    <Test()> Sub ShouldGetQuoteToken()
        GetToken("'Es Texto'", "Es Texto", TokenType.TokQuote)
    End Sub

    <Test()> Sub ShouldGetAddOpTokens()
        Dim tokens As List(Of Token) = Tokenize("a+b")
        Assert.IsNotNull(tokens)
        Assert.AreEqual(3, tokens.Count)
        Assert.AreEqual("a", tokens(0).Value)
        Assert.AreEqual(TokenType.TokName, tokens(0).Type)
        Assert.AreEqual("+", tokens(1).Value)
        Assert.AreEqual(TokenType.TokOperator, tokens(1).Type)
        Assert.AreEqual("b", tokens(2).Value)
        Assert.AreEqual(TokenType.TokName, tokens(2).Type)
    End Sub

    <Test()> Sub ShouldGetSubOpTokens()
        Dim tokens As List(Of Token) = Tokenize(" a - b ")
        Assert.IsNotNull(tokens)
        Assert.AreEqual(3, tokens.Count)
        Assert.AreEqual("a", tokens(0).Value)
        Assert.AreEqual(TokenType.TokName, tokens(0).Type)
        Assert.AreEqual("-", tokens(1).Value)
        Assert.AreEqual(TokenType.TokOperator, tokens(1).Type)
        Assert.AreEqual("b", tokens(2).Value)
        Assert.AreEqual(TokenType.TokName, tokens(2).Type)
    End Sub

    <Test()> Sub ShouldGetTwoOpsTokens()
        Dim tokens As List(Of Token) = Tokenize("a+b*c")
        Assert.IsNotNull(tokens)
        Assert.AreEqual(5, tokens.Count)
        Assert.AreEqual("a", tokens(0).Value)
        Assert.AreEqual(TokenType.TokName, tokens(0).Type)
        Assert.AreEqual("+", tokens(1).Value)
        Assert.AreEqual(TokenType.TokOperator, tokens(1).Type)
        Assert.AreEqual("b", tokens(2).Value)
        Assert.AreEqual(TokenType.TokName, tokens(2).Type)
        Assert.AreEqual("*", tokens(3).Value)
        Assert.AreEqual(TokenType.TokOperator, tokens(3).Type)
        Assert.AreEqual("c", tokens(4).Value)
        Assert.AreEqual(TokenType.TokName, tokens(4).Type)
    End Sub

    <Test()> Sub ShouldGetOneLineTokens()
        Dim tokens As List(Of Token) = Tokenize("a" & vbCrLf & "b")

        Assert.IsNotNull(tokens)
        Assert.AreEqual(2, tokens.Count)

        Assert.AreEqual("a", tokens(0).Value)
        Assert.AreEqual(TokenType.TokName, tokens(0).Type)

        Assert.AreEqual("b", tokens(1).Value)
        Assert.AreEqual(TokenType.TokName, tokens(1).Type)
    End Sub

    <Test()> Sub ShouldGetTwoLinesTokens()
        Dim tokens As List(Of Token) = TokenizeWithLines("a" & vbCrLf & "b")

        Assert.IsNotNull(tokens)
        Assert.AreEqual(3, tokens.Count)

        Assert.AreEqual("a", tokens(0).Value)
        Assert.AreEqual(TokenType.TokName, tokens(0).Type)

        Assert.AreEqual(TokenType.TokEoL, tokens(1).Type)

        Assert.AreEqual("b", tokens(2).Value)
        Assert.AreEqual(TokenType.TokName, tokens(2).Type)
    End Sub
End Class
