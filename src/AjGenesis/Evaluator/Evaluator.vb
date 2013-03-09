Imports System.Reflection
Imports System.Text.RegularExpressions

Public Class Evaluator
    Private mEnvironment As New Environment()

    Property Environment() As Environment
        Get
            Return mEnvironment
        End Get
        Set(ByVal Value As Environment)
            mEnvironment = Value
        End Set
    End Property

    Default Property Item(ByVal key As String) As Object
        Get
            If Not mEnvironment Is Nothing Then
                Return mEnvironment(key)
            End If

            Return Nothing
        End Get
        Set(ByVal Value As Object)
            mEnvironment(key) = Value
        End Set
    End Property

    Public Sub PushEnvironment(ByVal env As Environment)
        env.Parent = mEnvironment
        mEnvironment = env
    End Sub

    Public Sub PopEnvironment()
        mEnvironment = mEnvironment.Parent
    End Sub

    Public Function Evaluate(ByVal expression As String) As Object
        Dim comp As New Compiler(expression)

        Return comp.Compile().Evaluate(mEnvironment)
        'Return Eval(comp.Compile())
    End Function

End Class
