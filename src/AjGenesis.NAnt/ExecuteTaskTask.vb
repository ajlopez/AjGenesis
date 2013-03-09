Imports System.IO

Imports AjGenesis.Models.DynamicModel
Imports AjGenesis.Transformers.GenericTransformer

Imports NAnt.Core
Imports NAnt.Core.Attributes

<TaskName("executetask")> Public Class ExecuteTaskTask
    Inherits Task

    Private mTask As String

    <TaskAttributeAttribute("task", Required:=True)> Public Property Task() As String
        Get
            Return mTask
        End Get
        Set(ByVal Value As String)
            mTask = Value
        End Set
    End Property

    Protected Overrides Sub ExecuteTask()
        Dim taskfile As New StreamReader(mTask)
        Dim comp As New TextCompiler(taskfile)
        Dim pgm As Program

        pgm = comp.Compile()
        pgm.Output = System.Console.Out
        pgm.LogOutput = System.Console.Out
        pgm.Execute(topenv)

        taskfile.Close()
    End Sub
End Class
