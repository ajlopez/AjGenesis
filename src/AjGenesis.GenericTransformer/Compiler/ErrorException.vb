Public Class ErrorException
    Inherits Exception

    Public Sub New(ByVal msg As String)
        MyBase.New(msg)
    End Sub
End Class
