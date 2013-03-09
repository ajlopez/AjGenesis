Imports AjGenesis.Core

Public Class CommandListNode
    Inherits CommandNode

    Private mCommands As New ArrayList()

    Sub AddCommand(ByVal cmd As CommandNode)
        If Not cmd Is Nothing Then
            mCommands.Add(cmd)
        End If
    End Sub

    Overrides Sub Execute(ByVal env As Environment)
        Dim cmd As CommandNode

        For Each cmd In mCommands
            cmd.Execute(env)
        Next
    End Sub
End Class
