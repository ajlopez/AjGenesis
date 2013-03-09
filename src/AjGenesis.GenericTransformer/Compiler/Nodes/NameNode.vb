Imports AjGenesis.Core
Imports AjGenesis.Models.DynamicModel

Class NameNode
    Inherits ExpressionNode

    Private mName As String

    Sub New(ByVal name As String)
        mName = name
    End Sub

    ReadOnly Property Name() As String
        Get
            Return mName
        End Get
    End Property

    Overrides Function Evaluate(ByVal env As Environment) As Object
        Select Case mName.ToLower
            Case "nothing"
                Return Nothing
            Case "true"
                Return True
            Case "false"
                Return False
        End Select

        Return env(mName)
    End Function

    Function EvaluateLeftValue(ByVal env As Environment) As Object
        Select Case mName.ToLower
            Case "nothing"
                Return Nothing
            Case "true"
                Return True
            Case "false"
                Return False
        End Select

        Dim result As Object = env(mName)

        If result Is Nothing Then
            result = New DynamicObject()
            env(mName) = result
        End If

        Return result
    End Function
End Class
