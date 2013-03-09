Module Utilities
    Function IsTrue(ByVal value As Object) As Boolean
        If value Is Nothing Then
            Return False
        End If

        If TypeOf value Is Boolean Then
            Return DirectCast(value, Boolean)
        End If

        If TypeOf value Is Integer Then
            If value = 0 Then
                Return False
            Else
                Return True
            End If
        End If

        If TypeOf value Is String Then
            If value = "" Then
                Return False
            Else
                Return True
            End If
        End If

        Throw New Exception("Error en IsTrue")
    End Function
End Module
