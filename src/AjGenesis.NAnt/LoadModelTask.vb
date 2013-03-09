Imports AjGenesis.Models.DynamicModel
Imports AjGenesis.Transformers.GenericTransformer

Imports NAnt.Core
Imports NAnt.Core.Attributes

<TaskName("loadmodel")> Public Class LoadModelTask
    Inherits Task

    Private mModel As String

    <TaskAttributeAttribute("model", Required:=True)> Public Property Model() As String
        Get
            Return mModel
        End Get
        Set(ByVal Value As String)
            mModel = Value
        End Set
    End Property

    Protected Overrides Sub ExecuteTask()
        Dim obj As IObject

        If mModel.EndsWith(".txt") Then
            Dim txtbuilder As New ObjectTextBuilder
            obj = txtbuilder.GetObjectFromFile(mModel)
        Else
            Dim builder As New ObjectXmlBuilder
            obj = builder.GetObject(mModel)
        End If

        If obj.GetValue("Id") Is Nothing Then
            topenv(obj.GetValue("TypeName")) = obj
        Else
            topenv(obj.GetValue("Id")) = obj
        End If
    End Sub
End Class
