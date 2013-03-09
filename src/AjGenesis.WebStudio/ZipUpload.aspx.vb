
Partial Class ZipUpload
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
            If Context.Items("DirectoryName") Is Nothing Then
                Response.Redirect("~/Default.aspx")
            End If

            DirectoryName = Context.Items("DirectoryName")
            lblFullPath.Text = DirectoryService.GetPartialDirectory(DirectoryName)
        End If
    End Sub
    Protected Sub lnkDirectory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDirectory.Click
        Context.Items("DirectoryName") = DirectoryName
        Server.Transfer("~/DirectoryView.aspx")
    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        If fupFile.HasFile Then
            fupFile.SaveAs(ZipService.GetZipDirectory() & "/" & fupFile.FileName)
            ZipService.UnzipOnDirectory(ZipService.GetZipDirectory() & "/" & fupFile.FileName, DirectoryName)
            Context.Items("DirectoryName") = DirectoryName
            Server.Transfer("~/DirectoryView.aspx")
        End If
    End Sub
End Class
