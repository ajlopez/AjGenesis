Imports System.Reflection

Imports AjGenesis.Core
Imports AjGenesis.Models.DynamicModel

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

    Overrides Function Evaluate(ByVal env As Environment) As Object
        Dim obj As Object

        obj = mSubject.Evaluate(env)

        If TypeOf obj Is IObject Then
            Return DirectCast(obj, IObject).GetValue(mVerb.Name)
        End If

        ' Try Type
        If obj Is Nothing Then
            Dim tpname As String = Me.Name(env)
            Dim tp As Type = System.Type.GetType(tpname)

            If Not tp Is Nothing Then
                Return tp
            End If
        End If

        Return EvalPropertyField(obj, mVerb.Name)
    End Function

    Function EvaluateLeftValue(ByVal env As Environment) As Object
        Dim obj As Object

        If TypeOf mSubject Is DotNode Then
            obj = DirectCast(mSubject, DotNode).EvaluateLeftValue(env)
        ElseIf TypeOf mSubject Is NameNode Then
            obj = DirectCast(mSubject, NameNode).EvaluateLeftValue(env)
        Else
            obj = mSubject.Evaluate(env)
        End If

        Dim result As Object

        If TypeOf obj Is IObject Then
            result = DirectCast(obj, IObject).GetValue(mVerb.Name)

            If result Is Nothing Then
                result = New DynamicObject
                DirectCast(obj, IObject).SetValue(mVerb.Name, result)
            End If

            Return result
        End If

        result = EvalPropertyField(obj, mVerb.Name)

        If result Is Nothing Then
            result = New DynamicObject

            Dim type As Type

            type = obj.GetType

            type.InvokeMember(mVerb.Name, Reflection.BindingFlags.SetProperty Or Reflection.BindingFlags.SetField, Nothing, obj, New Object() {result})
        End If

        Return result
    End Function

    ReadOnly Property Name(ByVal env As Environment) As String
        Get
            If TypeOf mSubject Is NameNode Then
                Return DirectCast(mSubject, NameNode).Name & "." & mVerb.Name
            End If
            If TypeOf mSubject Is DotNode Then
                Return DirectCast(mSubject, DotNode).Name(env) & "." & mVerb.Name
            End If
            If TypeOf mSubject Is DotExNode Then
                If env Is Nothing Then
                    Throw New CompilerException("Environment expected")
                End If
                Return DirectCast(mSubject, DotExNode).Name(env) & "." & mVerb.Name
            End If
            Throw New CompilerException("Name expected")
        End Get
    End Property
End Class
