Imports System.IO

Public Class Tokenizer
    Private input As TextReader
    Private tokenValue As String
    Private lastChar As Char
    Private hasChar As Boolean
    Private lastToken As Token
    Private hasToken As Boolean

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

    Function NextName(ByVal firstChar As Char) As Token
        Dim name As String

        name = firstChar

        Dim ch As Char

        Try
            ch = NextChar()

            While Char.IsLetterOrDigit(ch)
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

    Function NextString() As Token
        Dim value As String = ""

        Dim ch As Char

        ch = NextChar()

        While ch <> """"c
            value += ch
            ch = NextChar()
        End While

        Dim token As New Token()

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

        Dim token As New Token()

        token.Type = TokenType.TokQuote
        token.Value = value

        Return token
    End Function

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

        Dim token As New Token()

        token.Type = TokenType.TokInteger
        token.Value = value

        Return token
    End Function

    Function NextPunctuation(ByVal ch As Char) As Token
        Dim token As New Token()

        token.Type = TokenType.TokPunctuation
        token.Value = ch

        Return token
    End Function

    Function NextOperator(ByVal ch As Char) As Token
        Dim token As New Token()

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

    Function NextToken() As Token
        If hasToken Then
            hasToken = False
            Return lastToken
        End If

        Dim ch As Char

        Try
            ch = NextCharSkipBlanks()

            If Char.IsLetter(ch) Or ch = "_"c Then
                Return NextName(ch)
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

            If operators.IndexOf(ch) >= 0 Then
                Return NextOperator(ch)
            End If

            If separators.IndexOf(ch) >= 0 Then
                Return NextPunctuation(ch)
            End If

            Throw New TokenizerException("Caracter inválido '" & ch & "'")
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
End Enum

Public Class Token
    Public Type As TokenType
    Public Value As String
End Class

Class EndOfInputException
    Inherits Exception

    Sub New()
        MyBase.New("Fin de Entrada")
    End Sub
End Class

Class TokenizerException
    Inherits Exception

    Sub New(ByVal msg As String)
        MyBase.New(msg)
    End Sub
End Class
