Imports System.Reflection

Imports AjGenesis.Core
Imports AjGenesis.Models.DynamicModel

Class DotExNode
    Inherits ExpressionNode

    Private mSubject As ExpressionNode
    Private mVerb As ExpressionNode

    Sub New(ByVal subject As ExpressionNode, ByVal verb As ExpressionNode)
        mSubject = subject
        mVerb = verb
    End Sub

    ReadOnly Property Subject() As ExpressionNode
        Get
            Return mSubject
        End Get
    End Property

    ReadOnly Property Verb() As ExpressionNode
        Get
            Return mVerb
        End Get
    End Property

    Overrides Function Evaluate(ByVal env As Environment) As Object
        Dim obj As Object
        Dim name As String

        obj = mSubject.Evaluate(env)

        name = mVerb.Evaluate(env).ToString

        If TypeOf obj Is IObject Then
            Return DirectCast(obj, IObject).GetValue(name)
        End If

        Return EvalPropertyField(obj, name)
    End Function

    ReadOnly Property Name(ByVal env As Environment) As String
        Get
            Dim nm As String = mVerb.Evaluate(env).ToString

            If TypeOf mSubject Is NameNode Then
                Return DirectCast(mSubject, NameNode).Name & "." & nm
            End If

            If TypeOf mSubject Is DotNode Then
                Return DirectCast(mSubject, DotNode).Name(env) & "." & nm
            End If

            If TypeOf mSubject Is DotExNode Then
                Return DirectCast(mSubject, DotExNode).Name(env) & "." & nm
            End If

            Throw New CompilerException("Name expected")

        End Get
    End Property
End Class
