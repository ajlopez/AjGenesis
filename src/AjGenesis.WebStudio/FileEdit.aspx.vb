
Imports System.IO

Partial Class FileEdit
    Inherits System.Web.UI.Page

    Public Property FileName() As String
        Get
            Return ViewState("FileName")
        End Get
        Set(ByVal value As String)
            ViewState("FileName") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            FileName = Context.Items("FileName")
            Dim fi As New FileInfo(filename)

            If Not fi.Exists Then
                Server.Transfer("~/Default.aspx")
            End If

            txtContent.Text = File.ReadAllText(FileName)
            Title = fi.Name
        End If
    End Sub

    Protected Sub lnkDirectory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDirectory.Click
        Dim fi As New FileInfo(FileName)
        Context.Items("DirectoryName") = fi.DirectoryName
        Server.Transfer("~/DirectoryView.aspx")
    End Sub

    Protected Sub lnkView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkView.Click
        Context.Items("FileName") = FileName
        Server.Transfer("~/FileView.aspx")
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        File.WriteAllText(FileName, txtContent.Text)
        Context.Items("FileName") = FileName
        Server.Transfer("~/FileView.aspx")
    End Sub
End Class
