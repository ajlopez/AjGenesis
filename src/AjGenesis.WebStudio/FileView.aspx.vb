Imports System.IO

Partial Class FileView
    Inherits System.Web.UI.Page

    Public Property FileName() As String
        Get
            Return ViewState("FileName")
        End Get
        Set(ByVal value As String)
            ViewState("FileName") = value
        End Set
    End Property

    Public Property ShortFileName() As String
        Get
            Return ViewState("ShortFileName")
        End Get
        Set(ByVal value As String)
            ViewState("ShortFileName") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            FileName = Context.Items("FileName")
            Dim fi As New FileInfo(filename)

            If Not fi.Exists Then
                Server.Transfer("~/Default.aspx")
            End If

            FileContent1.FileName = FileName
            ShortFileName = fi.Name

            lblFileName.Text = DirectoryService.GetPartialDirectory(fi.FullName)
        End If

        Title = ShortFileName
    End Sub

    Protected Sub lnkDirectory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDirectory.Click
        Dim fi As New FileInfo(FileName)
        Context.Items("DirectoryName") = fi.DirectoryName
        Server.Transfer("~/DirectoryView.aspx")
    End Sub

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkEdit.Click
        Context.Items("FileName") = FileName
        Server.Transfer("~/FileEdit.aspx")
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDelete.Click
        Dim fi As New FileInfo(FileName)
        Dim dirname As String = fi.Directory.FullName
        File.Delete(FileName)

        Context.Items("DirectoryName") = dirname
        Server.Transfer("~/DirectoryView.aspx")
    End Sub

    Protected Sub lnkCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCopy.Click
        Context.Items("FileName") = FileName
        Context.Items("Action") = "CopyFile"
        Server.Transfer("~/DirectorySelect.aspx")
    End Sub

    Protected Sub lnkMove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMove.Click
        Context.Items("FileName") = FileName
        Context.Items("Action") = "MoveFile"
        Server.Transfer("~/DirectorySelect.aspx")
    End Sub
End Class
