Imports System.Collections.Generic
Imports System.IO

Partial Class Generate
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim ps As New ProjectService

            ps.GetProjects()

            Dim projects As List(Of String) = ps.GetProjects()

            ddlProjects.Items.Add("")

            For Each project As String In projects
                ddlProjects.Items.Add(project)
            Next
        End If
    End Sub

    Protected Sub ddlProjects_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProjects.SelectedIndexChanged
        If ddlProjects.SelectedValue = "" Then
            ddlTechnologies.Items.Clear()
            ddlTechnologies.Enabled = False
            pnlOutput.Visible = False
            Return
        End If

        Dim ts As New TechnologyService

        Dim technologies As List(Of String) = ts.GetTechnologies(ddlProjects.SelectedValue)

        ddlTechnologies.Items.Clear()

        For Each technology As String In technologies
            ddlTechnologies.Items.Add(technology)
        Next

        ddlTechnologies.Enabled = True
    End Sub

    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        pnlOutput.Visible = False

        litOutput.Text = AjGenesisService.GenerateCode(ddlProjects.SelectedValue, ddlTechnologies.SelectedValue)
        pnlOutput.Visible = True
    End Sub
End Class
