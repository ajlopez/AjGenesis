Imports AjGenesis.Core

Public Class CatchNode
    Inherits EndNode

    Dim mName As String
    Dim mCatchCommand As CommandNode

    Public Property Name() As String
        Get
            Return mName
        End Get
        Set(ByVal Value As String)
            mName = Value
        End Set
    End Property

    Public Property CatchCommand() As CommandNode
        Get
            Return mCatchCommand
        End Get
        Set(ByVal Value As CommandNode)
            mCatchCommand = Value
        End Set
    End Property

    Overrides Sub Execute(ByVal env As Environment)
        mCatchCommand.Execute(env)
    End Sub

    Sub ExecuteWithException(ByVal env As Environment, ByVal ex As Exception)
        Dim newenv As New Environment(env)
        newenv(mName) = ex
        Me.Execute(newenv)
    End Sub
End Class
