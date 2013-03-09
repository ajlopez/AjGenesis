Imports System.Xml.Serialization
Imports System.IO

Imports AjGenesis.Transformers.GenericTransformer

Public Class Form1
    Private ser As New XmlSerializer(GetType(Recipes))
    Dim env As New TopEnvironment
    Dim recipes As Recipes

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        If Me.OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.OK Then
            Return
        End If

        Try
            Dim mustpop As Boolean
            If Not recipes Is Nothing Then
                mustpop = True
            End If
            recipes = ser.Deserialize(New FileStream(OpenFileDialog1.FileName, FileMode.Open))
            RecipesToTree(recipes, TreeView1)
            If mustpop Then
                FileUtilities.PopFilePath()
            End If
            FileUtilities.PushFilePath(OpenFileDialog1.FileName)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TreeView1_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        If e.Node.Tag Is Nothing Then
            Return
        End If

        If TypeOf e.Node.Tag Is Recipe Then
            ShowRecipe(DirectCast(e.Node.Tag, Recipe))
        End If
    End Sub

    Private Sub TreeView1_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseDoubleClick
        If e.Node.Tag Is Nothing Then
            Return
        End If

        If TypeOf e.Node.Tag Is Recipe Then
            ExecuteRecipe(DirectCast(e.Node.Tag, Recipe))
        End If
    End Sub

    Private Sub ExecuteRecipe(ByVal recipe As Recipe)
        If recipe.Task Is Nothing Then
            Return
        End If

        Dim taskfilename As String

        If File.Exists(recipe.Task) Then
            taskfilename = recipe.Task
        ElseIf File.Exists(Path.Combine("../..", recipe.Task)) Then
            taskfilename = Path.Combine("../..", recipe.Task)
        Else
            taskfilename = recipe.Task
        End If

        Try
            Dim taskfile As New StreamReader(taskfilename)
            Dim comp As New TextCompiler(taskfile)
            Dim pgm As Program

            pgm = comp.Compile()
            'pgm.Output = System.Console.Out
            pgm.Execute(env)

            taskfile.Close()
        Catch ex As Exception
            If Not ex.InnerException Is Nothing Then
                MsgBox(ex.InnerException.Message)
            Else
                MsgBox(ex.Message)
            End If
        End Try
    End Sub

    Private Sub ShowRecipe(ByVal recipe As Recipe)
        If recipe.Documentation Is Nothing Then
            Return
        End If

        Dim docfilename As String

        Try
            If File.Exists(recipe.Documentation) Then
                docfilename = recipe.Documentation
            ElseIf File.Exists(Path.Combine("..\..", recipe.Documentation)) Then
                docfilename = Path.Combine("..\..", recipe.Documentation)
            Else
                docfilename = recipe.Documentation
            End If

            docfilename = Path.GetFullPath(docfilename)

            WebBrowser1.Navigate(docfilename)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class

