Public Delegate Function CallDelegate(ByVal args As Object()) As Object

Class CallNode
    Inherits ExpressionNode


    Private mSubject As ExpressionNode
    Private mArguments As ExpressionNode()

    Sub New(ByVal subject As ExpressionNode, ByVal args As ExpressionNode())
        mSubject = subject
        mArguments = args
    End Sub

    ReadOnly Property Subject() As ExpressionNode
        Get
            Return mSubject
        End Get
    End Property

    ReadOnly Property Arguments() As ExpressionNode()
        Get
            Return mArguments
        End Get
    End Property

    Overrides Function Evaluate(ByVal env As Environment) As Object
        Dim args(mArguments.Length - 1) As Object

        Dim arg As ExpressionNode

        Dim na As Integer

        For na = 0 To mArguments.Length - 1
            args(na) = mArguments(na).Evaluate(env)
        Next

        If TypeOf mSubject Is NameNode Then
            Dim callee As Object
            callee = mSubject.Evaluate(env)

            If TypeOf callee Is FunctionNode Then
                Dim funct As FunctionNode
                funct = DirectCast(callee, FunctionNode)
                Return funct.Evaluate(env, args)
            End If

            If TypeOf callee Is SubNode Then
                Dim subr As SubNode
                subr = DirectCast(callee, SubNode)
                subr.Evaluate(env, args)
                Return Nothing
            End If

            If TypeOf callee Is CallDelegate Then
                Dim functor As CallDelegate
                functor = DirectCast(callee, CallDelegate)
                Return functor(args)
            End If


            Throw New Exception("Llamada '" & DirectCast(mSubject, NameNode).Name & "' desconocida")
        End If

        Dim obj As Object
        Dim name As String

        If TypeOf mSubject Is DotNode Then
            obj = DirectCast(mSubject, DotNode).Subject.Evaluate(env)
            name = DirectCast(mSubject, DotNode).Verb.Name
            Return EvalInvoke(obj, name, args)
        End If

        Throw New Exception("Error en CallNode")
    End Function

    Private Function EvalInvoke(ByVal obj As Object, ByVal prop As String, ByVal args As Object())
        If obj Is Nothing Then
            Return Nothing
        End If

        Return obj.GetType().InvokeMember(prop, Reflection.BindingFlags.GetProperty Or Reflection.BindingFlags.GetField Or Reflection.BindingFlags.InvokeMethod, Nothing, obj, args)
    End Function
End Class
