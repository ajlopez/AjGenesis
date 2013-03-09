Imports AjGenesis.Core

Public Class TryCatchNode
    Inherits CommandNode

    Dim mName As String
    Dim mTryCommand As CommandNode
    Dim mCatchCommand As CatchNode

    Public Property Name() As String
        Get
            Return mName
        End Get
        Set(ByVal Value As String)
            mName = Value
        End Set
    End Property

    Public Property TryCommand() As CommandNode
        Get
            Return mTryCommand
        End Get
        Set(ByVal Value As CommandNode)
            mTryCommand = Value
        End Set
    End Property

    Public Property CatchCommand() As CatchNode
        Get
            Return mCatchCommand
        End Get
        Set(ByVal Value As CatchNode)
            mCatchCommand = Value
        End Set
    End Property

    Overrides Sub Execute(ByVal env As Environment)
        Try
            If Not mTryCommand Is Nothing Then
                mTryCommand.Execute(env)
            End If
        Catch ex As Exception
            If mCatchCommand Is Nothing Then
                Throw ex
            End If
            mCatchCommand.ExecuteWithException(env, ex)
        End Try
    End Sub
End Class
