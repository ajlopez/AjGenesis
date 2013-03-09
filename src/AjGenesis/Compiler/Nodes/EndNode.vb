Public Class EndNode
    Inherits CommandNode

    Private mWord As String

    Public Property Word() As String
        Get
            Return mWord
        End Get
        Set(ByVal Value As String)
            mWord = Value
        End Set
    End Property

    Overrides Sub Execute(ByVal env As Environment)
        Throw New Exception("No debe ejecutarse end")
    End Sub
End Class
