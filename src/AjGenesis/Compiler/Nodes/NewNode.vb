Public Class NewNode
    Inherits ExpressionNode

    Dim mTypeName As String
    Dim mArguments As ExpressionNode()

    Sub New(ByVal exp As ExpressionNode)
        If TypeOf exp Is NameNode Then
            mTypeName = DirectCast(exp, NameNode).Name
        ElseIf TypeOf exp Is DotNode Then
            mTypeName = DirectCast(exp, DotNode).Name
        ElseIf TypeOf exp Is CallNode Then
            Dim cal As CallNode = exp
            mArguments = cal.Arguments

            If TypeOf cal.Subject Is NameNode Then
                mTypeName = DirectCast(cal.Subject, NameNode).Name
            ElseIf TypeOf cal.Subject Is DotNode Then
                mTypeName = DirectCast(cal.Subject, DotNode).Name
            Else : Throw New CompilerException("Se esperaba tipo")
            End If
        Else
            Throw New CompilerException("Se esperaba tipo")
        End If
    End Sub

    Sub New(ByVal exp As ExpressionNode, ByVal args As ExpressionNode())
        Me.New(exp)
        mArguments = args
    End Sub

    Overrides Function Evaluate(ByVal env As Environment) As Object
        Dim tp As Type = Type.GetType(mTypeName)

        If Not mArguments Is Nothing AndAlso mArguments.Length > 0 Then
            Dim args(mArguments.Length - 1) As Object

            Dim arg As ExpressionNode

            Dim na As Integer

            For na = 0 To mArguments.Length - 1
                args(na) = mArguments(na).Evaluate(env)
            Next

            Return Activator.CreateInstance(tp, mArguments)
        Else
            Return Activator.CreateInstance(tp)
        End If
    End Function
End Class
