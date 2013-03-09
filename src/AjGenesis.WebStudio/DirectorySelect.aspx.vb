
Imports System.IO

Partial Class DirectorySelect
    Inherits System.Web.UI.Page

    Public Property FileName() As String
        Get
            Return ViewState("FileName")
        End Get
        Set(ByVal value As String)
            ViewState("FileName") = value
        End Set
    End Property

    Public Property SourceDirectory() As String
        Get
            Return ViewState("SourceDirectory")
        End Get
        Set(ByVal value As String)
            ViewState("SourceDirectory") = value
        End Set
    End Property

    Public Property Action() As String
        Get
            Return ViewState("Action")
        End Get
        Set(ByVal value As String)
            ViewState("Action") = value
        End Set
    End Property

    Protected Sub DirectoryTree1_DirectorySelected(ByVal dirname As String) Handles DirectoryTree1.DirectorySelected
        Dim name As String = txtName.Text

        If Action = "MoveContent" Then
            DirectoryService.MoveContent(SourceDirectory, dirname)
            Context.Items("DirectoryName") = dirname
            Server.Transfer("~/DirectoryView.aspx")
        ElseIf Action = "CopyContent" Then
            DirectoryService.CopyContent(SourceDirectory, dirname)
            Context.Items("DirectoryName") = dirname
            Server.Transfer("~/DirectoryView.aspx")
        ElseIf Action = "MoveDirectory" Then
            dirname = dirname & "/" & name
            DirectoryService.MoveDirectory(SourceDirectory, dirname)
            Context.Items("DirectoryName") = dirname
            Server.Transfer("~/DirectoryView.aspx")
        ElseIf Action = "CopyDirectory" Then
            dirname = dirname & "/" & name
            DirectoryService.CopyDirectory(SourceDirectory, dirname)
            Context.Items("DirectoryName") = dirname
            Server.Transfer("~/DirectoryView.aspx")
        ElseIf Action = "MoveFile" Then
            DirectoryService.MoveFile(FileName, dirname & "/" & name)
            Context.Items("DirectoryName") = dirname
            Server.Transfer("~/DirectoryView.aspx")
        ElseIf Action = "CopyFile" Then
            DirectoryService.CopyFile(FileName, dirname & "/" & name)
            Context.Items("DirectoryName") = dirname
            Server.Transfer("~/DirectoryView.aspx")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Context.Items("Action") Is Nothing Then
                Response.Redirect("~/Default.aspx")
            End If

            Action = Context.Items("Action")

            If Not Context.Items("SourceDirectory") Is Nothing Then
                SourceDirectory = Context.Items("SourceDirectory")
                lblSource.Text = "Source Directory " & DirectoryService.GetPartialDirectory(SourceDirectory)
                DirectoryTree1.InitialDirectory = SourceDirectory
                Dim di As New DirectoryInfo(SourceDirectory)
                lblEntry.Text = "Directory Name"
                txtName.Text = di.Name
                If Action = "CopyContent" Or Action = "MoveContent" Then
                    lblTarget.Text = "Select Target Directory"
                Else
                    lblTarget.Text = "Select Target Parent Directory"
                    parEntry.Visible = True
                End If
            ElseIf Not Context.Items("FileName") Is Nothing Then
                FileName = Context.Items("FileName")
                lblSource.Text = "Source File " & DirectoryService.GetPartialDirectory(FileName)
                Dim fi As New FileInfo(FileName)
                DirectoryTree1.InitialDirectory = fi.Directory.FullName
                lblEntry.Text = "File Name"
                txtName.Text = fi.Name
                parEntry.Visible = True
                If Action = "MoveFile" Or Action = "CopyFile" Then
                    lblTarget.Text = "Select Target Directory"
                Else
                    lblTarget.Text = "Select Target Parent Directory"
                End If
            Else
                Response.Redirect("~/Default.aspx")
            End If
        End If

        If Action = "MoveFile" Then
            Title = "Move File"
        ElseIf Action = "CopyFile" Then
            Title = "Copy File"
        ElseIf Action = "MoveContent" Then
            Title = "Move Content"
        ElseIf Action = "CopyContent" Then
            Title = "Copy Content"
        ElseIf Action = "MoveDirectory" Then
            Title = "Move Directory"
        ElseIf Action = "CopyDirectory" Then
            Title = "Copy Directory"
        End If
    End Sub
End Class

