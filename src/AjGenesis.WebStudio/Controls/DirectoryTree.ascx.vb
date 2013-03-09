
Imports System.IO

Partial Class Controls_DirectoryTree
    Inherits System.Web.UI.UserControl

    Public Event DirectorySelected(ByVal dirname As String)

    Public Property InitialDirectory() As String
        Get
            Return ViewState("InitialDirectory")
        End Get
        Set(ByVal value As String)
            ViewState("InitialDirectory") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim di As New DirectoryInfo(DirectoryService.GetWorkingDirectory)

            Dim tn As New TreeNode(di.Name, di.FullName)

            trvDirectories.PathSeparator = "/"
            trvDirectories.Nodes.Add(tn)

            AddDirectories(tn, di)
        End If
    End Sub

    Private Sub ExpandNode(ByVal treenode As TreeNode)
        treenode.Expand()

        If Not treenode.Parent Is Nothing Then
            ExpandNode(treenode.Parent)
        End If
    End Sub

    Private Sub AddDirectories(ByVal treenode As TreeNode, ByVal dinfo As DirectoryInfo)
        treenode.Collapse()

        If Not InitialDirectory Is Nothing Then
            If DirectoryService.DirectoriesAreEqual(dinfo.FullName, InitialDirectory) Then
                ExpandNode(treenode)
            End If
        End If

        For Each sdi As DirectoryInfo In dinfo.GetDirectories
            Dim tn As New TreeNode(sdi.Name)

            treenode.ChildNodes.Add(tn)

            AddDirectories(tn, sdi)

        Next
    End Sub

    Protected Sub trvDirectories_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvDirectories.SelectedNodeChanged
        RaiseEvent DirectorySelected(trvDirectories.SelectedNode.ValuePath)
    End Sub
End Class
