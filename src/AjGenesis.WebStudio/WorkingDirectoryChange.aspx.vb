
Partial Class WorkingDirectoryChange
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtWorkingDirectory.Text = DirectoryService.GetWorkingDirectory
        End If
    End Sub

    Protected Sub btnChange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChange.Click
        If txtWorkingDirectory.Text > "" Then
            DirectoryService.SetWorkingDirectory(txtWorkingDirectory.Text)
            Response.Redirect("~/WorkingDirectoryView.aspx")
        End If
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        DirectoryService.ResetWorkingDirectory()
        Response.Redirect("~/WorkingDirectoryView.aspx")
    End Sub
End Class
