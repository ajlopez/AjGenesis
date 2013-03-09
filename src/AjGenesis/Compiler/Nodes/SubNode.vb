Public Class SubNode
    Inherits CommandNode

    Dim mName As String
    Dim mParameters As String()
    Dim mCommands As CommandListNode

    Public Property Name() As String
        Get
            Return mName
        End Get
        Set(ByVal Value As String)
            mName = Value
        End Set
    End Property

    Public Property Parameters() As String()
        Get
            Return mParameters
        End Get
        Set(ByVal Value As String())
            mParameters = Value
        End Set
    End Property

    Public Property Commands() As CommandListNode
        Get
            Return mCommands
        End Get
        Set(ByVal Value As CommandListNode)
            mCommands = Value
        End Set
    End Property

    Overrides Sub Execute(ByVal env As Environment)
        Throw New Exception("No se evalúan rutinas")
    End Sub


    Sub Evaluate(ByVal env As Environment, ByVal args() As Object)
        Dim envfun As New Environment(env)
        Dim k As Integer

        If Not mParameters Is Nothing Then
            For k = 0 To mParameters.Length - 1
                envfun(mParameters(k)) = args(k)
            Next
        End If

        Try
            mCommands.Execute(envfun)
        Catch ex As ReturnException
        End Try
    End Sub
End Class
