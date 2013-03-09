Imports System.IO

Imports Ionic.Utils.Zip

Partial Class DirectoryView
    Inherits System.Web.UI.Page

    Public Property DirectoryName() As String
        Get
            Return ViewState("DirectoryName")
        End Get
        Set(ByVal value As String)
            ViewState("DirectoryName") = value
        End Set
    End Property

    Public Property ShortDirectoryName() As String
        Get
            Return ViewState("ShortDirectoryName")
        End Get
        Set(ByVal value As String)
            ViewState("ShortDirectoryName") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            DirectoryName = Context.Items("DirectoryName")

            If DirectoryService.IsProjectsDirectory(DirectoryName) Then
                Server.Transfer("~/ProjectList.aspx")
            End If

            If DirectoryService.IsTemplatesDirectory(DirectoryName) Then
                Server.Transfer("~/TemplateList.aspx")
            End If

            If DirectoryService.IsTasksDirectory(DirectoryName) Then
                Server.Transfer("~/TaskList.aspx")
            End If

            If DirectoryService.IsBuildsDirectory(DirectoryName) Then
                Server.Transfer("~/BuildList.aspx")
            End If

            Dim di As New DirectoryInfo(DirectoryName)

            If Not di.Exists Then
                Server.Transfer("~/Default.aspx")
            End If

            Directory1.Path = DirectoryName
            ShortDirectoryName = di.Name

            lblFullPath.Text = DirectoryService.GetPartialDirectory(DirectoryName)

            If DirectoryService.IsWorkingDirectory(DirectoryName) Then
                spanNoWorkingDirectory.Visible = False
                'lnkParent.Enabled = False
                spanWorkingDirectory.Visible = True
            End If
        End If

        Title = ShortDirectoryName
    End Sub

    Protected Sub lnkParent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkParent.Click
        Dim di As New DirectoryInfo(DirectoryName)
        Context.Items("DirectoryName") = di.Parent.FullName
        Server.Transfer("~/DirectoryView.aspx")
    End Sub

    Protected Sub lnkDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDownload.Click
        Dim zipfilename As String

        zipfilename = ZipService.ZipDirectory(DirectoryName, "")

        Response.Redirect(zipfilename)
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDelete.Click
        Dim di As New DirectoryInfo(DirectoryName)
        Dim parentname = di.Parent.FullName
        System.IO.Directory.Delete(DirectoryName, True)
        Context.Items("DirectoryName") = parentname
        Server.Transfer("~/DirectoryView.aspx")
    End Sub

    Protected Sub lnkMoveContent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMoveContent.Click
        Context.Items("SourceDirectory") = DirectoryName
        Context.Items("Action") = "MoveContent"
        Server.Transfer("~/DirectorySelect.aspx")
    End Sub

    Protected Sub lnkCopyContent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCopyContent.Click
        Context.Items("SourceDirectory") = DirectoryName
        Context.Items("Action") = "CopyContent"
        Server.Transfer("~/DirectorySelect.aspx")
    End Sub

    Protected Sub lnkNewSubdirectory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNewSubdirectory.Click
        Context.Items("DirectoryName") = DirectoryName
        Server.Transfer("~/DirectoryNew.aspx")
    End Sub

    Protected Sub lnkNewFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNewFile.Click
        Context.Items("DirectoryName") = DirectoryName
        Server.Transfer("~/FileNew.aspx")
    End Sub

    Protected Sub lnkUploadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkUploadFile.Click
        Context.Items("DirectoryName") = DirectoryName
        Server.Transfer("~/FileUpload.aspx")
    End Sub

    Protected Sub lnkCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCopy.Click
        Context.Items("SourceDirectory") = DirectoryName
        Context.Items("Action") = "CopyDirectory"
        Server.Transfer("~/DirectorySelect.aspx")
    End Sub

    Protected Sub lnkMove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMove.Click
        Context.Items("SourceDirectory") = DirectoryName
        Context.Items("Action") = "MoveDirectory"
        Server.Transfer("~/DirectorySelect.aspx")
    End Sub

    Protected Sub lnkUploadZip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkUploadZip.Click
        Context.Items("DirectoryName") = DirectoryName
        Server.Transfer("~/ZipUpload.aspx")
    End Sub

    Protected Sub lnkChange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkChange.Click
        Response.Redirect("~/WorkingDirectoryChange.aspx")
    End Sub
End Class

