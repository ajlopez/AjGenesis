Imports System.IO

Public Class Processor
    Private mOutput As TextWriter

    Public Property Output() As TextWriter
        Get
            Return mOutput
        End Get
        Set(ByVal Value As TextWriter)
            mOutput = Value
        End Set
    End Property

    Public Sub Print(ByVal text As String)
        Output.Write(text)
    End Sub

    Public Sub PrintLine(ByVal text As String)
        Output.Write(text)
    End Sub

    Public Sub Log(ByVal text As String)
        Console.WriteLine(text)
    End Sub

    Public Sub Debug(ByVal text As String)
        Console.WriteLine(text)
    End Sub
End Class
