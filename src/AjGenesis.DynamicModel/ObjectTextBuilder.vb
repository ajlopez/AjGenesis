
Imports System.Collections.Generic
Imports System.IO

Imports AjGenesis.Core

Public Class ObjectTextBuilder
    Private mTokenizer As Tokenizer
    Private mReader As TextReader

    Private Function NextLine() As String
        Return mReader.ReadLine
    End Function

    Private Function NextToken() As Token
        Return mTokenizer.NextToken
    End Function

    Private Function IsName(ByVal token As Token) As Boolean
        If token Is Nothing Then
            Return False
        End If

        If token.Type = TokenType.TokName Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function Tokenize(ByVal line As String) As List(Of Token)
        Dim tokens As New List(Of Token)
        Dim tokenizer As New Tokenizer(line)
        Dim token As Token

        token = tokenizer.NextToken

        While Not token Is Nothing
            tokens.Add(token)
            token = tokenizer.NextToken
        End While

        Return tokens
    End Function

    Private Function GetLineTokens(ByVal reader As TextReader) As List(Of Token)
        Dim line As String

        line = reader.ReadLine

        If line Is Nothing Then
            Return Nothing
        End If

        Dim tokens As List(Of Token) = Tokenize(line)

        While tokens.Count = 0
            line = reader.ReadLine

            If line Is Nothing Then
                Return Nothing
            End If

            tokens = Tokenize(line)
        End While

        Return tokens
    End Function

    Private Function UnexpectedToken(ByVal token As Token) As Exception
        Return New Exception(String.Format("Unexpected token: '{0}'", token.Value))
    End Function

    Private Function GetProperty(ByVal dynobj As DynamicObject, ByVal reader As TextReader) As Boolean
        Dim tokens As List(Of Token) = GetLineTokens(reader)

        If tokens Is Nothing OrElse tokens.Count = 0 Then
            Throw New Exception("Unexpected end of text")
        End If

        If tokens.Count = 2 AndAlso tokens(0).Value.ToLower().Equals("end") Then
            If tokens(1).Value.ToLower() <> dynobj.GetValue("TypeName").ToLower() Then
                Throw UnexpectedToken(tokens(1))
            End If

            Return False
        End If

        If tokens.Count <> 3 Then
            Dim obj As DynamicObject = GetObject(reader, tokens)

            If TypeOf dynobj Is DynamicListObject Then
                DirectCast(dynobj, DynamicListObject).AddValue(obj)
            Else
                dynobj.SetValue(obj.GetValue("TypeName"), obj)
            End If
            Return True
        End If

        If Not tokens(0).IsName Then
            Throw UnexpectedToken(tokens(0))
        End If

        If tokens(1).Value <> "=" Then
            Throw UnexpectedToken(tokens(1))
        End If

        If Not tokens(2).IsValue Then
            Throw UnexpectedToken(tokens(2))
        End If

        If tokens.Count > 3 Then
            Throw UnexpectedToken(tokens(3))
        End If

        If TypeOf dynobj Is DynamicListObject Then
            DirectCast(dynobj, DynamicListObject).AddValue(tokens(2).Value)
        Else
            dynobj.SetValue(tokens(0).Value, tokens(2).Value)
        End If

        Return True
    End Function

    Private Sub GetProperties(ByVal dynobj As DynamicObject, ByVal reader As TextReader)
        While GetProperty(dynobj, reader)

        End While
    End Sub

    Private Function GetObject(ByVal reader As TextReader) As DynamicObject
        Dim tokens As List(Of Token) = GetLineTokens(reader)

        If tokens Is Nothing OrElse tokens.Count = 0 Then
            Return Nothing
        End If

        Return GetObject(reader, tokens)
    End Function

    Private Function IsList(ByVal name As String) As Boolean
        Return name.ToLower.EndsWith("s")
    End Function

    Private Function GetObject(ByVal reader As TextReader, ByVal tokens As List(Of Token)) As DynamicObject
        Dim dynobj As DynamicObject

        If tokens(0).Type <> TokenType.TokName Then
            Throw UnexpectedToken(tokens(0))
        End If

        If IsList(tokens(0).Value) Then
            dynobj = New DynamicListObject
        Else
            dynobj = New DynamicObject
        End If

        dynobj.SetValue("TypeName", tokens(0).Value)

        If tokens.Count = 1 Then
            GetProperties(dynobj, reader)
            Return dynobj
        End If

        If tokens(1).Type <> TokenType.TokName Then
            Throw UnexpectedToken(tokens(1))
        End If

        dynobj.SetValue("Name", tokens(1).Value)

        Dim nt As Integer = 2
        Dim token As Token

        While nt < tokens.Count
            token = tokens(nt)

            If Not token.IsName Then
                Throw UnexpectedToken(token)
            End If

            If nt + 1 < tokens.Count AndAlso tokens(nt + 1).Value = "=" AndAlso nt + 2 < tokens.Count AndAlso tokens(nt + 2).IsValue Then
                '' TODO it could be better to set tokens().GetValue review ObjectXmlBuilder
                dynobj.SetValue(token.Value, tokens(nt + 2).Value)
                nt = nt + 3
            Else
                dynobj.SetValue(token.Value, True)
                nt = nt + 1
            End If
        End While

        If tokens.Count <= 2 Then
            GetProperties(dynobj, reader)
        End If

        Return dynobj
    End Function

    Public Function GetObject(ByVal text As String) As DynamicObject
        Dim reader As New StringReader(text)

        Return GetObject(reader)
    End Function

    Public Function GetObjectFromFile(ByVal filename As String) As DynamicObject
        Return GetObject(File.ReadAllText(filename))
    End Function

End Class

