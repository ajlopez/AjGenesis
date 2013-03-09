Imports AjGenesis.Core

Public MustInherit Class CommandNode
    Inherits Node

    MustOverride Sub Execute(ByVal env As Environment)
End Class
