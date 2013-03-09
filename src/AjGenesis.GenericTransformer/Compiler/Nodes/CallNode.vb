Imports AjGenesis.Core

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

            Throw New Exception("Unknown call '" & DirectCast(mSubject, NameNode).Name & "'")
        End If

        Dim obj As Object
        Dim name As String

        If TypeOf mSubject Is DotNode Then
            Dim dn As DotNode = DirectCast(mSubject, DotNode)

            obj = dn.Subject.Evaluate(env)

            ' Process type

            If TypeOf obj Is Type Then
                name = dn.Verb.Name
                Return EvalInvoke(DirectCast(obj, Type), name, args)
            End If

            If Not obj Is Nothing Then
                name = dn.Verb.Name
                Return EvalInvoke(obj, name, args)
            End If

            ' try type name (see above, process type)
            If TypeOf dn.Subject Is DotNode Then
                Try
                    Dim tpname As String = DirectCast(dn.Subject, DotNode).Name(env)
                    Dim tp As Type = System.Type.GetType(tpname)

                    name = dn.Verb.Name
                    Return EvalInvoke(tp, name, args)
                Catch
                    Return Nothing ' if not a type, assume all is nothing
                End Try
            End If

            ' if subject is nothing, then return nothing

            Return Nothing
        End If

        If TypeOf mSubject Is DotExNode Then
            obj = DirectCast(mSubject, DotExNode).Subject.Evaluate(env)
            name = DirectCast(mSubject, DotExNode).Name(env)
            Return EvalInvoke(obj, name, args)
        End If

        Throw New Exception("Error in CallNode")
    End Function

    Private Function EvalInvoke(ByVal obj As Object, ByVal prop As String, ByVal args As Object()) As Object
        If obj Is Nothing Then
            Return Nothing
        End If

        Return obj.GetType().InvokeMember(prop, Reflection.BindingFlags.GetProperty Or Reflection.BindingFlags.GetField Or Reflection.BindingFlags.InvokeMethod, Nothing, obj, args)
    End Function

    Private Function EvalInvoke(ByVal tp As Type, ByVal prop As String, ByVal args As Object()) As Object
        If tp Is Nothing Then
            Return Nothing
        End If

        Return tp.InvokeMember(prop, Reflection.BindingFlags.GetProperty Or Reflection.BindingFlags.GetField Or Reflection.BindingFlags.InvokeMethod, Nothing, Nothing, args)
    End Function
End Class
