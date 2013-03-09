Imports System.IO

Public Class Program
    Private mCommands As New CommandListNode()
    Private mEnvironment As Environment

    Public Sub New()
        mEnvironment = New Environment()
    End Sub

    Public Sub New(ByVal env As Environment)
        mEnvironment = env
    End Sub

    Public Property Output() As TextWriter
        Get
            Return mEnvironment.Processor.Output
        End Get
        Set(ByVal Value As TextWriter)
            mEnvironment.Processor.Output = Value
        End Set
    End Property

    Public Sub AddCommand(ByVal cmd As CommandNode)
        mCommands.AddCommand(cmd)
    End Sub

    Public Sub AddFunction(ByVal f As FunctionNode)
        mEnvironment.TopParent(f.Name) = f
    End Sub

    Public Sub AddSubroutine(ByVal s As SubNode)
        mEnvironment.TopParent(s.Name) = s
    End Sub

    Public Sub Execute()
        mCommands.Execute(mEnvironment)
    End Sub

    Public Sub Execute(ByVal env As Environment)
        If Not mEnvironment Is env Then
            mEnvironment.Parent = env
        End If
        mCommands.Execute(mEnvironment)
        mEnvironment.Parent = Nothing
    End Sub
End Class
