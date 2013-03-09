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

Public Class Tokenizer
    Private input As TextReader
    Private tokenValue As String
    Private lastChar As Char
    Private hasChar As Boolean
    Private lastToken As Token
    Private hasToken As Boolean

    Public UseLines As Boolean

    Private Const operators As String = "<>=-+*/&"
    Private Const separators As String = ".(),"
    Private Shared ReadOnly biOperators As String() = {"<>", ">=", "<="}

    Sub New(ByVal input As TextReader)
        Me.input = input
    End Sub

    Sub New(ByVal input As String)
        Me.New(New StringReader(input))
    End Sub

    Sub PushChar(ByVal ch As Char)
        lastChar = ch
        hasChar = True
    End Sub

    Function NextChar() As Char
        If hasChar Then
            hasChar = False
            Return lastChar
        End If

        Dim ch As Integer

        ch = input.Read()

        If ch < 0 Then
            Throw New EndOfInputException()
        End If

        Return ChrW(ch)
    End Function

    Sub SkipToControl()
        Dim ch As Char

        ch = NextChar()

        While Not Char.IsControl(ch)
            ch = NextChar()
        End While
    End Sub

    Function NextCharSkipBlanks() As Char
        Dim ch As Char

        ch = NextChar()

        While Char.IsWhiteSpace(ch)
            ch = NextChar()
        End While

        Return ch
    End Function

    Function NextCharSkipBlanksNoCrLf() As Char
        Dim ch As Char

        ch = NextChar()

        While Char.IsWhiteSpace(ch) And ch <> vbCr And ch <> vbLf
            ch = NextChar()
        End While

        Return ch
    End Function

    Function NextName(ByVal firstChar As Char) As Token
        Dim name As String

        name = firstChar

        Dim ch As Char

        Try
            ch = NextChar()

            While Char.IsLetterOrDigit(ch) Or ch = "_"c
                name += ch
                ch = NextChar()
            End While

            PushChar(ch)
        Catch ex As EndOfInputException

        End Try

        Dim token As New Token()

        token.Type = TokenType.TokName
        token.Value = name

        Return token
    End Function

    Function NextSpecialName(ByVal lastChar As Char) As Token
        Dim name As String

        name = ""

        Dim ch As Char

        Try
            ch = NextChar()

            While ch <> lastChar
                name += ch
                ch = NextChar()
            End While
        Catch ex As EndOfInputException

        End Try

        Dim token As New Token

        token.Type = TokenType.TokName
        token.Value = name

        Return token
    End Function

    Function NextString() As Token
        Dim value As String = ""

        Dim ch As Char

        ch = NextChar()

        While ch <> """"c
            value += ch
            ch = NextChar()
        End While

        Dim token As New token

        token.Type = TokenType.TokString
        token.Value = value

        Return token
    End Function

    Function NextQuote() As Token
        Dim value As String = ""

        Dim ch As Char

        ch = NextChar()

        While ch <> "'"c
            value += ch
            ch = NextChar()
        End While

        Dim token As New token

        token.Type = TokenType.TokQuote
        token.Value = value

        Return token
    End Function

    Private Sub PopChar(ByVal popch As Char)
        Dim ch As Char

        Try
            ch = NextChar()

            If ch <> popch Then
                PushChar(ch)
            End If
        Catch ex As EndOfInputException

        End Try
    End Sub

    Function NextInteger(ByVal firstDigit As Char) As Token
        Dim value As String

        value = firstDigit

        Dim ch As Char

        Try
            ch = NextChar()

            While Char.IsDigit(ch)
                value += ch
                ch = NextChar()
            End While

            PushChar(ch)
        Catch ex As EndOfInputException
        End Try

        Dim token As New token

        token.Type = TokenType.TokInteger
        token.Value = value

        Return token
    End Function

    Function NextPunctuation(ByVal ch As Char) As Token
        Dim token As New token

        token.Type = TokenType.TokPunctuation
        token.Value = ch

        Return token
    End Function

    Function NextOperator(ByVal ch As Char) As Token
        Dim token As New token

        token.Type = TokenType.TokOperator
        token.Value = ch

        Dim ch2 As Char

        Try
            ch2 = NextChar()

            If Array.IndexOf(biOperators, ch & ch2) >= 0 Then
                token.Value = ch & ch2
            Else
                PushChar(ch2)
            End If
        Catch e As EndOfInputException

        End Try

        Return token
    End Function

    Private Function NextEoL() As Token
        Dim token As New Token
        token.Type = TokenType.TokEoL
        Return token
    End Function

    Function NextToken() As Token
        If hasToken Then
            hasToken = False
            Return lastToken
        End If

        Dim ch As Char

        Try
            If UseLines Then
                ch = NextCharSkipBlanksNoCrLf()
            Else
                ch = NextCharSkipBlanks()
            End If

            If Char.IsLetter(ch) Or ch = "_"c Then
                Return NextName(ch)
            End If

            If ch = "["c Then
                Return NextSpecialName("]"c)
            End If

            If ch = """"c Then
                Return NextString()
            End If

            If ch = "'"c Then
                Return NextQuote()
            End If

            If Char.IsDigit(ch) Then
                Return NextInteger(ch)
            End If

            If UseLines Then
                If ch = vbCr Then
                    PopChar(vbLf)
                    Return NextEoL()
                End If

                If ch = vbLf Then
                    PopChar(vbCr)
                    Return NextEoL()
                End If
            End If

            If operators.IndexOf(ch) >= 0 Then
                Return NextOperator(ch)
            End If

            If separators.IndexOf(ch) >= 0 Then
                Return NextPunctuation(ch)
            End If

            Throw New TokenizerException("Invalid Character '" & ch & "'")
        Catch ex As EndOfInputException
            Return Nothing
        End Try
    End Function

    Sub PushToken(ByVal token As Token)
        lastToken = token
        hasToken = True
    End Sub
End Class

Public Enum TokenType
    TokName = 0
    TokInteger = 1
    TokString = 2
    TokPunctuation = 3
    TokOperator = 4
    TokQuote = 5
    TokEoL = 6
End Enum

Public Class Token
    Public Type As TokenType
    Public Value As String

    Public Function IsName() As Boolean
        If Type = TokenType.TokName Then
            Return True
        End If

        Return False
    End Function

    Public Function IsValue() As Boolean
        If Type = TokenType.TokInteger Or Type = TokenType.TokQuote Or Type = TokenType.TokString Then
            Return True
        End If

        Return False
    End Function

    Public Function GetValue() As Object
        If Type = TokenType.TokInteger Then
            Return CInt(Value)
        End If

        Return Value
    End Function
End Class

Class EndOfInputException
    Inherits Exception

    Sub New()
        MyBase.New("End of Input")
    End Sub
End Class

Class TokenizerException
    Inherits Exception

    Sub New(ByVal msg As String)
        MyBase.New(msg)
    End Sub
End Class
