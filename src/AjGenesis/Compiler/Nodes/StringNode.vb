Imports System.Text.RegularExpressions

Class StringNode
    Inherits ExpressionNode

    Private Shared rex As New Regex("\$\{[^\}]*\}")

    Private mValue As String
    Private mEnv As Environment

    Sub New(ByVal value As String)
        mValue = value
    End Sub

    ReadOnly Property Value() As String
        Get
            Return mValue
        End Get
    End Property

    Overrides Function Evaluate(ByVal env As Environment) As Object
        Dim mev As New MatchEvaluator(AddressOf EvaluateMatch)
        mEnv = env
        Dim value As String = rex.Replace(mValue, mev)
        mEnv = Nothing
        Return value
    End Function

    Protected Function EvaluateMatch(ByVal match As Match) As String
        Dim text As String = match.Value.Substring(2, match.Value.Length - 3)
        Dim cmp As New Compiler(text)
        Return cmp.Compile.Evaluate(mEnv).ToString()
    End Function
End Class
