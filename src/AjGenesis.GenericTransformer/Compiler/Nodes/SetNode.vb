Imports AjGenesis.Core
Imports AjGenesis.Models.DynamicModel

Public Class SetNode
    Inherits CommandNode

    '    Private mIdentifier As String
    Private mTarget As ExpressionNode
    Private mExpression As ExpressionNode

    'Public Property Identifier() As String
    '    Get
    '        Return mIdentifier
    '    End Get
    '    Set(ByVal Value As String)
    '        mIdentifier = Value
    '    End Set
    'End Property

    Public Property Target() As ExpressionNode
        Get
            Return mTarget
        End Get
        Set(ByVal Value As ExpressionNode)
            mTarget = Value
        End Set
    End Property

    Public Property Expression() As ExpressionNode
        Get
            Return mExpression
        End Get
        Set(ByVal Value As ExpressionNode)
            mExpression = Value
        End Set
    End Property

    Overrides Sub Execute(ByVal env As Environment)
        If TypeOf mTarget Is NameNode Then
            env(DirectCast(mTarget, NameNode).Name) = mExpression.Evaluate(env)
            Return
        End If

        If TypeOf mTarget Is DotNode Then
            Dim dn As DotNode = DirectCast(mTarget, DotNode)
            Dim obj As Object

            If TypeOf dn.Subject Is DotNode Then
                obj = DirectCast(dn.Subject, DotNode).EvaluateLeftValue(env)
            ElseIf TypeOf dn.Subject Is NameNode Then
                obj = DirectCast(dn.Subject, NameNode).EvaluateLeftValue(env)
            Else
                obj = dn.Subject.Evaluate(env)
            End If

            Dim value As Object = mExpression.Evaluate(env)

            If TypeOf obj Is IObject Then
                DirectCast(obj, IObject).SetValue(dn.Verb.Name, value)
                Return
            End If

            Dim type As type

            type = obj.GetType

            type.InvokeMember(dn.Verb.Name, Reflection.BindingFlags.SetProperty Or Reflection.BindingFlags.SetField, Nothing, obj, New Object() {value})

            Return
        End If

        Throw New NotSupportedException("Not supported assignment")
    End Sub
End Class
