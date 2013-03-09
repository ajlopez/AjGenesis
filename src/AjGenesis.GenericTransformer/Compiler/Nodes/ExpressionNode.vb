Imports AjGenesis.Core

Public MustInherit Class ExpressionNode
    Inherits Node

    MustOverride Function Evaluate(ByVal env As Environment) As Object
End Class
