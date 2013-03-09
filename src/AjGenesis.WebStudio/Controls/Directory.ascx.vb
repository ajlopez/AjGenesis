
Imports System.IO
Imports System.Collections.Generic

Partial Class Controls_Directory
    Inherits System.Web.UI.UserControl

    Public Property Path() As String
        Get
            Return ViewState("Path")
        End Get
        Set(ByVal value As String)
            ViewState("Path") = value
            FillGrid(value)
        End Set
    End Property

    Private Sub FillGrid(ByVal path As String)
        Dim data As List(Of DirectoryData)

        data = DirectoryService.GetDirectoryData(path)

        grdDirectory.DataSource = data
        grdDirectory.DataBind()
    End Sub

    Protected Sub grdDirectory_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdDirectory.RowCommand
        Dim itemname As String = Path & "/" & e.CommandArgument

        If e.CommandName = "View" Then
            If System.IO.File.Exists(itemname) Then
                Context.Items("FileName") = itemname
                Server.Transfer("~/FileView.aspx")
            ElseIf System.IO.Directory.Exists(itemname) Then
                Context.Items("DirectoryName") = itemname
                Server.Transfer("~/DirectoryView.aspx")
            End If
        ElseIf e.CommandName = "Download" Then
            If System.IO.Directory.Exists(itemname) Then
                Dim zipfilename As String
                Dim di As New DirectoryInfo(itemname)

                zipfilename = ZipService.ZipDirectory(itemname, di.Name)

                Response.Redirect(zipfilename)
            End If
        ElseIf e.CommandName = "Edit" Then
        If System.IO.File.Exists(itemname) Then
            Context.Items("FileName") = itemname
            Server.Transfer("~/FileEdit.aspx")
        End If
        ElseIf e.CommandName = "DeleteFile" Then
        If System.IO.File.Exists(itemname) Then
            System.IO.File.Delete(itemname)
            FillGrid(Path)
        End If
        End If
    End Sub
End Class

