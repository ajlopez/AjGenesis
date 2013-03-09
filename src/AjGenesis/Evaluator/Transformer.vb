Imports System.IO

Public Class Transformer
    Public Sub Transform(ByVal input As TextReader, ByVal output As TextWriter, ByVal env As Environment)
        Dim cmp As New TemplateCompiler(input)
        Dim pgm As Program = cmp.Compile
        pgm.Output = output
        pgm.Execute(env)
    End Sub

    Public Sub Transform(ByVal inputfile As String, ByVal outputfile As String, ByVal env As Environment)
        Dim input As New StreamReader(inputfile)
        Dim output As New StreamWriter(outputfile)
        Transform(input, output, env)
        input.Close()
        output.Close()
    End Sub
End Class
