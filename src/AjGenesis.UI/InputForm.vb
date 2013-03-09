Imports System.Windows.Forms

Public Class InputForm
    Dim x As Integer = 5
    Dim y As Integer = 5
    Dim controlheight As Integer = 20
    Dim controlsep As Integer = 2
    Dim controlsep2 As Integer = 5

    Dim fields As New Dictionary(Of String, TextBox)

    Private Sub IncrementTop()
        y += controlheight + controlsep
    End Sub

    Public Sub AddTextField(ByVal name As String, ByVal text As String, ByVal value As String)
        Dim lbl As New Label
        lbl.Text = text
        lbl.Top = y
        lbl.Left = x
        lbl.Width = Me.ClientRectangle.Width - 2 * x
        lbl.Height = controlheight
        y += controlheight + controlsep
        Controls.Add(lbl)
        Dim txt As New TextBox
        txt.Text = value
        txt.Top = y
        txt.Left = x
        txt.Width = Me.ClientRectangle.Width - 2 * x
        txt.Height = controlheight
        y += controlheight + controlsep2
        Controls.Add(txt)
        fields(name) = txt
    End Sub

    Public Sub AddFolderField(ByVal name As String, ByVal text As String, ByVal value As String)
        Dim lbl As New Label
        lbl.Text = text
        lbl.Top = y
        lbl.Left = x
        lbl.Width = Me.ClientRectangle.Width - 2 * x
        lbl.Height = controlheight
        y += controlheight + controlsep
        Controls.Add(lbl)
        Dim txt As New TextBox
        txt.Text = value
        txt.Top = y
        txt.Left = x
        txt.Width = Me.ClientRectangle.Width - 30 - 2 * x
        txt.Height = controlheight
        Dim btn As New Button
        btn.Text = "..."
        btn.Top = y
        btn.Left = x + txt.Width + 2
        btn.Width = 26
        btn.Height = controlheight
        y += controlheight + controlsep2
        Controls.Add(txt)
        Controls.Add(btn)
        btn.Tag = name
        AddHandler btn.Click, AddressOf Me.btnFolder_Click
        fields(name) = txt
    End Sub

    Public Sub AddFileField(ByVal name As String, ByVal text As String, ByVal value As String)
        Dim lbl As New Label
        lbl.Text = text
        lbl.Top = y
        lbl.Left = x
        lbl.Width = Me.ClientRectangle.Width - 2 * x
        lbl.Height = controlheight
        y += controlheight + controlsep
        Controls.Add(lbl)
        Dim txt As New TextBox
        txt.Text = value
        txt.Top = y
        txt.Left = x
        txt.Width = Me.ClientRectangle.Width - 30 - 2 * x
        txt.Height = controlheight
        Dim btn As New Button
        btn.Text = "..."
        btn.Top = y
        btn.Left = x + txt.Width + 2
        btn.Width = 26
        btn.Height = controlheight
        y += controlheight + controlsep2
        Controls.Add(txt)
        Controls.Add(btn)
        btn.Tag = name
        AddHandler btn.Click, AddressOf Me.btnFile_Click
        fields(name) = txt
    End Sub

    Public Sub SetField(ByVal name As String, ByVal value As String)
        fields(name).Text = value
    End Sub

    Public Function GetField(ByVal name As String) As String
        Return fields(name).Text
    End Function

    Private Sub btnFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim fd As New OpenFileDialog
        If fd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            SetField(DirectCast(sender, Button).Tag.ToString(), fd.FileName)
        End If
    End Sub

    Private Sub btnFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim fd As New FolderBrowserDialog
        If fd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            SetField(DirectCast(sender, Button).Tag.ToString(), fd.SelectedPath)
        End If
    End Sub
End Class