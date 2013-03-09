Imports System.IO

Partial Class Controls_FileContent
    Inherits System.Web.UI.UserControl

    Public Property FileName() As String
        Get
            Return ViewState("FileName")
        End Get
        Set(ByVal value As String)
            ViewState("FileName") = value
            FillLabel(value)
        End Set
    End Property

    Private Sub FillLabel(ByVal filename As String)
        lblContent.Text = File.ReadAllText(filename)
    End Sub
End Class
