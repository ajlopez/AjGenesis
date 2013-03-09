
Partial Class WorkingDirectoryView
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Context.Items("DirectoryName") = DirectoryService.GetWorkingDirectory()
            Server.Transfer("~/DirectoryView.aspx")
        End If
    End Sub
End Class
