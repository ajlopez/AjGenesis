Public Class Compiler
    Private mTokenizer As Tokenizer

    Sub New(ByVal Tokenizer As Tokenizer)
        mTokenizer = Tokenizer
    End Sub

    Sub New(ByVal expr As String)
        Me.New(New Tokenizer(expr))
    End Sub

    Function CompileParameters() As String()
        Dim lp As New ArrayList()
        Dim param As ExpressionNode
        Dim token As Token

        token = mTokenizer.NextToken()

        If token Is Nothing Then
            Return Nothing
        End If

        If token.Type <> TokenType.TokPunctuation OrElse token.Value <> "(" Then
            Throw New CompilerException("Se esperaban par�metros")
        End If

        While True
            token = mTokenizer.NextToken

            If token.Type = TokenType.TokPunctuation And token.Value = ")" Then
                If lp.Count = 0 Then
                    Return Nothing
                End If
                Return lp.ToArray(GetType(String))
            End If

            If token.Type <> TokenType.TokName Then
                Throw New CompilerException("Se esperaba par�metro")
            End If

            lp.Add(token.Value)

            token = mTokenizer.NextToken

            If token.Type <> TokenType.TokPunctuation Then
                Throw New CompilerException("Se esperaba , o )")
            End If

            If token.Value = ")" Then
                Return lp.ToArray(GetType(String))
            End If

            If token.Value <> "," Then
                Throw New CompilerException("Se esperaba , o )")
            End If
        End While
    End Function

    Function CompileArguments() As ExpressionNode()
        Dim la As New ArrayList()
        Dim arg As ExpressionNode
        Dim token As Token

        arg = CompileExpression()

        While Not arg Is Nothing
            la.Add(arg)

            token = mTokenizer.NextToken

            If token Is Nothing OrElse token.Type <> TokenType.TokPunctuation Then
                Throw New CompilerException("Se esperaba argumento")
            End If

            If token.Value = ")" Then
                mTokenizer.PushToken(token)
                Exit While
            End If

            If token.Value <> "," Then
                Throw New CompilerException("Se esperaba argumento")
            End If

            arg = CompileExpression()
        End While

        token = mTokenizer.NextToken

        If token Is Nothing OrElse token.Type <> TokenType.TokPunctuation OrElse token.Value <> ")" Then
            Throw New CompilerException("Se esperaba )")
        End If

        Dim nodes(la.Count - 1) As ExpressionNode

        Dim k As Integer

        For k = 0 To la.Count - 1
            nodes(k) = la(k)
        Next

        Return nodes
    End Function

    ' Compila Termino

    Function CompileTerm() As ExpressionNode
        Dim token As Token
        Dim ExpressionNode As ExpressionNode

        token = mTokenizer.NextToken

        If token Is Nothing Then
            Return Nothing
        End If

        If token.Type = TokenType.TokInteger Then
            Return New IntegerNode(CInt(token.Value))
        End If

        If token.Type = TokenType.TokString Then
            Return New StringNode(token.Value)
        End If

        If token.Type = TokenType.TokQuote Then
            Return New QuoteNode(token.Value)
        End If

        If token.Type = TokenType.TokPunctuation AndAlso token.Value = "(" Then
            Dim expr As ExpressionNode
            expr = CompileExpression()
            CompileToken(")")
            Return expr
        End If

        If token.Type <> TokenType.TokName Then
            mTokenizer.PushToken(token)
            Return Nothing
        End If

        ExpressionNode = New NameNode(token.Value)

        Do
            token = mTokenizer.NextToken

            If token Is Nothing Then
                Return ExpressionNode
            End If

            If token.Type <> TokenType.TokPunctuation Then
                mTokenizer.PushToken(token)
                Return ExpressionNode
            End If

            If token.Value = "(" Then
                If Not TypeOf ExpressionNode Is NameNode AndAlso Not TypeOf ExpressionNode Is DotNode Then
                    Throw New CompilerException("No se esperaba (")
                End If
                Return New CallNode(ExpressionNode, CompileArguments())
            End If

            If token.Value <> "." Then
                mTokenizer.PushToken(token)
                Return ExpressionNode
            End If

            token = mTokenizer.NextToken

            If token Is Nothing OrElse token.Type <> TokenType.TokName Then
                Throw New CompilerException("Se esperaba nombre")
            End If

            ExpressionNode = New DotNode(ExpressionNode, New NameNode(token.Value))
        Loop
    End Function

    Function CompileBinaryExpressionLevel2() As ExpressionNode
        Dim token As Token
        Dim ExpressionNode As ExpressionNode

        ExpressionNode = CompileTerm()

        If ExpressionNode Is Nothing Then
            Return Nothing
        End If

        Do
            token = mTokenizer.NextToken

            If token Is Nothing Then
                Return ExpressionNode
            End If

            If token.Type <> TokenType.TokOperator Then
                mTokenizer.PushToken(token)
                Return ExpressionNode
            End If

            Select Case token.Value
                Case "*", "/"
                    ExpressionNode = New BinaryOperatorNode(token.Value, ExpressionNode, CompileBinaryExpressionLevel2())
                Case Else
                    mTokenizer.PushToken(token)
                    Return ExpressionNode
            End Select
        Loop
    End Function

    Function CompileBinaryExpression() As ExpressionNode
        Dim token As Token
        Dim ExpressionNode As ExpressionNode

        ExpressionNode = CompileBinaryExpressionLevel2()

        If ExpressionNode Is Nothing Then
            Return Nothing
        End If

        Do
            token = mTokenizer.NextToken

            If token Is Nothing Then
                Return ExpressionNode
            End If

            If token.Type <> TokenType.TokOperator Then
                mTokenizer.PushToken(token)
                Return ExpressionNode
            End If

            Select Case token.Value
                Case "+", "-"
                    ExpressionNode = New BinaryOperatorNode(token.Value, ExpressionNode, CompileBinaryExpression())
                Case Else
                    mTokenizer.PushToken(token)
                    Return ExpressionNode
            End Select
        Loop
    End Function

    Function CompileBasicExpression() As ExpressionNode
        Dim token As Token

        token = mTokenizer.NextToken

        If token Is Nothing Then
            Return Nothing
        End If

        If token.Type = TokenType.TokName And token.Value.ToLower = "not" Then
            Return New UnaryOperatorNode("not", CompileBasicExpression())
        End If

        mTokenizer.PushToken(token)

        Dim ExpressionNode As ExpressionNode

        ExpressionNode = CompileBinaryExpression()

        If ExpressionNode Is Nothing Then
            Return Nothing
        End If

        Do
            token = mTokenizer.NextToken

            If token Is Nothing Then
                Return ExpressionNode
            End If

            If token.Type <> TokenType.TokOperator Then
                mTokenizer.PushToken(token)
                Return ExpressionNode
            End If

            Select Case token.Value
                Case "=", "<", ">", "<=", ">=", "<>"
                    ExpressionNode = New BinaryOperatorNode(token.Value, ExpressionNode, CompileTerm())
                Case Else
                    mTokenizer.PushToken(token)
                    Return ExpressionNode
            End Select
        Loop
    End Function

    Function CompileExpression() As ExpressionNode
        Dim token As Token

        token = mTokenizer.NextToken

        If token Is Nothing Then
            Return Nothing
        End If

        If token.Type = TokenType.TokName AndAlso token.Value.ToLower = "new" Then
            Return New NewNode(CompileBasicExpression())
        End If

        mTokenizer.PushToken(token)

        Return CompileBasicExpression()
    End Function

    Function Compile() As ExpressionNode
        Dim ExpressionNode As ExpressionNode

        ExpressionNode = CompileExpression()

        If Not mTokenizer.NextToken() Is Nothing Then
            Throw New CompilerException("Se esperaba fin de expresi�n")
        End If

        Return ExpressionNode
    End Function

    Sub NoMoreTokens()
        Dim token As Token

        token = mTokenizer.NextToken

        If Not token Is Nothing Then
            mTokenizer.PushToken(token)
            Throw New CompilerException("No se esperaba " & token.Value)
        End If
    End Sub

    Function HasMoreTokens()
        Dim token As Token

        token = mTokenizer.NextToken

        If Not token Is Nothing Then
            mTokenizer.PushToken(token)
            Return True
        End If

        Return False
    End Function

    Function CompileIdentifier() As String
        Dim token As Token = mTokenizer.NextToken

        If token Is Nothing Then
            Return Nothing
        End If

        If Not token.Type = TokenType.TokName Then
            Throw New CompilerException("Se esperaba identificador")
        End If

        Return token.Value
    End Function

    Function CompileWord() As String
        Dim token As Token = mTokenizer.NextToken

        If token Is Nothing Then
            Return Nothing
        End If

        If Not token.Type = TokenType.TokName Then
            Throw New CompilerException("Se esperaba palabra")
        End If

        Return token.Value
    End Function

    Sub CompileToken(ByVal expected As String)
        Dim token As Token = mTokenizer.NextToken

        If token Is Nothing OrElse token.Value.ToLower <> expected Then
            Throw New CompilerException("Se esperaba " & expected)
        End If
    End Sub

    Function NextToken(ByVal expected As String)
        Dim token As Token = mTokenizer.NextToken

        If token Is Nothing OrElse token.Value.ToLower <> expected Then
            mTokenizer.PushToken(token)
            Return False
        End If

        Return True
    End Function

    Function CompileCommand() As CommandNode
        Dim word As String

        word = CompileWord()

        If word Is Nothing Then
            Return Nothing
        End If

        Select Case word.ToLower
            Case "if"
                Dim cmdif As New IfNode()
                cmdif.Expression = CompileExpression()
                CompileToken("then")
                Return cmdif
            Case "set"
                Dim cmdset As New SetNode()
                cmdset.Identifier = CompileIdentifier()
                CompileToken("=")
                cmdset.Expression = CompileExpression()
                Return cmdset
            Case "print"
                Dim cmdprint As New PrintNode()
                cmdprint.Expression = CompileExpression()
                Return cmdprint
            Case "printline"
                Dim cmdprint As New PrintLineNode()
                cmdprint.Expression = CompileExpression()
                Return cmdprint
            Case "include"
                Dim cmdinc As New IncludeNode()
                cmdinc.Expression = CompileExpression()
                Return cmdinc
            Case "message"
                Dim cmdmsg As New MessageNode()
                cmdmsg.Expression = CompileExpression()
                Return cmdmsg
            Case "end"
                Dim cmdend As New EndNode()
                cmdend.Word = CompileWord().ToLower
                Return cmdend
            Case "else"
                Dim cmdend As New EndNode()
                cmdend.Word = word.ToLower
                Return cmdend
            Case "while"
                Dim cmdwhile As New WhileNode()
                cmdwhile.Expression = CompileExpression()
                Return cmdwhile
            Case "for"
                CompileToken("each")
                Dim cmdforeach As New ForEachNode()
                cmdforeach.Variable = CompileIdentifier()
                CompileToken("in")
                cmdforeach.Expression = CompileExpression()
                If NextToken("where") Then
                    cmdforeach.Where = CompileExpression()
                End If
                Return cmdforeach
            Case "rem"
                Return Nothing
            Case "function"
                Dim cmdfunction As New FunctionNode()
                cmdfunction.Name = CompileIdentifier()
                cmdfunction.Parameters = CompileParameters()
                Return cmdfunction
            Case "sub"
                Dim cmdsub As New SubNode()
                cmdsub.Name = CompileIdentifier()
                cmdsub.Parameters = CompileParameters()
                Return cmdsub
            Case "return"
                Dim cmdret As New ReturnNode()
                Dim token As Token = mTokenizer.NextToken
                If token Is Nothing Then
                    Return cmdret
                End If
                mTokenizer.PushToken(token)
                cmdret.Expression = CompileExpression()
                Return cmdret
            Case Else
                Dim exp As ExpressionNode
                Dim tok As New Token()
                tok.Type = TokenType.TokName
                tok.Value = word
                mTokenizer.PushToken(tok)

                exp = CompileTerm()

                If TypeOf exp Is NameNode Then
                    If HasMoreTokens() Then
                        Dim cmdset As New SetNode()
                        cmdset.Identifier = word
                        CompileToken("=")
                        cmdset.Expression = CompileExpression()
                        Return cmdset
                    End If

                    Throw New CompilerException("No se esperaba '" & DirectCast(exp, NameNode).Name & "'")
                End If

                Dim cmddo As New DoNode()

                cmddo.Expression = exp

                Return cmddo
        End Select
    End Function

    Public Function CompileSingleCommand() As CommandNode
        Dim cmd As CommandNode
        cmd = CompileCommand()

        If Not cmd Is Nothing Then
            NoMoreTokens()
        End If

        Return cmd
    End Function
End Class

Class CompilerException
    Inherits Exception

    Sub New(ByVal msg As String)
        MyBase.New(msg)
    End Sub
End Class

