Imports System.IO

Imports AjGenesis.Models.DynamicModel
Imports AjGenesis.Transformers.GenericTransformer

Imports NAnt.Core
Imports NAnt.Core.Attributes
Imports NAnt.Core.Types


<TaskName("executescript")> Public Class ExecuteScriptTask
    Inherits Task

    Private mText As String = ""
    Private mCode As RawXml
    Private mExpandProperties As Boolean = True

    <TaskAttributeAttribute("expandproperties")> Public Property ExpandProperties() As Boolean
        Get
            Return mExpandProperties
        End Get
        Set(ByVal Value As Boolean)
            mExpandProperties = Value
        End Set
    End Property

    <BuildElement("code", Required:=True)> Public Property Code() As RawXml
        Get
            Return mCode
        End Get
        Set(ByVal Value As RawXml)
            mCode = Value
        End Set
    End Property

    Protected Overrides Sub ExecuteTask()
        Dim tasktxt As String = mCode.Xml.InnerText

        If ExpandProperties Then
            tasktxt = Project.ExpandProperties(tasktxt, Location)
        End If

        Dim taskfile As New StringReader(tasktxt)
        Dim comp As New TextCompiler(taskfile)
        Dim pgm As Program

        pgm = comp.Compile()
        pgm.Output = System.Console.Out
        pgm.Execute(topenv)

        taskfile.Close()
    End Sub
End Class
