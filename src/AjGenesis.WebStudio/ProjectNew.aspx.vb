
Partial Class ProjectNew
    Inherits System.Web.UI.Page

    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        pnlOutput.Visible = False

        litOutput.Text = AjGenesisService.GenerateProject(txtName.Text, txtDescription.Text)
        pnlOutput.Visible = True
    End Sub
End Class
