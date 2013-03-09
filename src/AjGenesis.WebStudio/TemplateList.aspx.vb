
Partial Class TemplateList
    Inherits System.Web.UI.Page

    Public Property DirectoryName() As String
        Get
            Return ViewState("DirectoryName")
        End Get
        Set(ByVal value As String)
            ViewState("DirectoryName") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            DirectoryName = DirectoryService.GetTemplatesDirectory
            Directory1.Path = DirectoryName
        End If
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

    Protected Sub lnkDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDownload.Click
        Dim zipfilename As String

        zipfilename = ZipService.ZipDirectory(DirectoryName, "")

        Response.Redirect(zipfilename)
    End Sub

    Protected Sub lnkUploadZip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkUploadZip.Click
        Context.Items("DirectoryName") = DirectoryName
        Server.Transfer("~/ZipUpload.aspx")
    End Sub
End Class
