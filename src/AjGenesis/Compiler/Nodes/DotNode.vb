Imports System.Reflection

Class DotNode
    Inherits ExpressionNode

    Private mSubject As ExpressionNode
    Private mVerb As NameNode

    Sub New(ByVal subject As ExpressionNode, ByVal verb As NameNode)
        mSubject = subject
        mVerb = verb
    End Sub

    ReadOnly Property Subject() As ExpressionNode
        Get
            Return mSubject
        End Get
    End Property

    ReadOnly Property Verb() As NameNode
        Get
            Return mVerb
        End Get
    End Property

    Private Function EvalDefaultProperty(ByVal obj As Object, ByVal prop As String)
        Dim type As Type

        type = obj.GetType

        Dim members() As MemberInfo

        members = type.GetDefaultMembers()

        Dim member As MemberInfo

        For Each member In members
            If member.MemberType = MemberTypes.Property Then
                Return type.InvokeMember(member.Name, BindingFlags.GetProperty, Nothing, obj, New Object() {prop})
            End If
        Next

        Return Nothing
    End Function

    Private Function EvalPropertyField(ByVal obj As Object, ByVal prop As String)
        If obj Is Nothing Then
            Return Nothing
        End If

        Dim type As Type

        type = obj.GetType

        Try
            Return type.InvokeMember(prop, Reflection.BindingFlags.GetProperty Or Reflection.BindingFlags.GetField, Nothing, obj, Nothing)
        Catch ex As MissingFieldException
            Return EvalDefaultProperty(obj, prop)
        Catch ex As MissingMethodException
            Return EvalDefaultProperty(obj, prop)
        End Try
    End Function

    Overrides Function Evaluate(ByVal env As Environment) As Object
        Dim obj As Object

        obj = mSubject.Evaluate(env)

        Return EvalPropertyField(obj, mVerb.Name)
    End Function

    ReadOnly Property Name() As String
        Get
            If TypeOf mSubject Is NameNode Then
                Return DirectCast(mSubject, NameNode).Name & "." & mVerb.Name
            End If
            If TypeOf mSubject Is DotNode Then
                Return DirectCast(mSubject, DotNode).Name & "." & mVerb.Name
            End If
            Throw New CompilerException("Se esperaba nombre")
        End Get
    End Property
End Class
