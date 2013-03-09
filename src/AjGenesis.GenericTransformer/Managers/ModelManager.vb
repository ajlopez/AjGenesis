Imports AjGenesis.Core
Imports AjGenesis.Models.DynamicModel


Public Class ModelManager
    Public Sub LoadModel(ByVal filename As String, ByVal env As Environment)
        Dim builder As New ObjectXmlBuilder
        Dim obj As IObject
        obj = builder.GetObject(filename)

        If obj.GetValue("Id") Is Nothing Then
            env(obj.GetValue("TypeName")) = obj
        Else
            env(obj.GetValue("Id")) = obj
        End If

    End Sub
End Class
