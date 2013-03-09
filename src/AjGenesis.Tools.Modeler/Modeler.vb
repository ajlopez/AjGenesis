Imports AjGenesis.Schemas

Public Class Form1
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.TreeView1 = New System.Windows.Forms.TreeView
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.MenuItem4 = New System.Windows.Forms.MenuItem
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem2, Me.MenuItem3, Me.MenuItem4})
        Me.MenuItem1.Text = "&File"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 0
        Me.MenuItem2.Text = "&Open Schema..."
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 1
        Me.MenuItem3.Text = "E&xit"
        '
        'TreeView1
        '
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Left
        Me.TreeView1.ImageIndex = -1
        Me.TreeView1.Location = New System.Drawing.Point(0, 0)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.SelectedImageIndex = -1
        Me.TreeView1.Size = New System.Drawing.Size(184, 417)
        Me.TreeView1.TabIndex = 0
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(184, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 417)
        Me.Splitter1.TabIndex = 3
        Me.Splitter1.TabStop = False
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 2
        Me.MenuItem4.Text = "New WIndow"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(664, 417)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.TreeView1)
        Me.IsMdiContainer = True
        Me.Menu = Me.MainMenu1
        Me.Name = "Form1"
        Me.Text = "AjGenesis Tools Modeler"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private currentChild As Form

    Private Sub MenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem3.Click
        End
    End Sub

    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem2.Click
        If OpenFileDialog1.ShowDialog <> DialogResult.OK Then
            Return
        End If

        Dim schema As schema

        Try
            schema = LoadSchemaFromFile(OpenFileDialog1.FileName)
            LoadTree(schema)
        Catch ex As Exception
            MsgBox(ex)
        End Try
    End Sub

    Private Sub LoadTree(ByVal schema As Schema)
        TreeView1.Nodes.Clear()
        Dim tn As TreeNode
        tn = TreeView1.Nodes.Add("Schema " & schema.Name)
        tn = tn.Nodes.Add("Types")
        tn.Tag = schema.Types

        Dim tp As Type

        For Each tp In schema.Types
            LoadType(tn, tp)
        Next
    End Sub

    Private Sub LoadType(ByVal tn As TreeNode, ByVal tp As Type)
        tn = tn.Nodes.Add("Type " & tp.Name)
        tn.Tag = tp

        tn = tn.Nodes.Add("Properties")
        tn.Tag = tp.Properties

        Dim prop As [Property]

        For Each prop In tp.Properties
            LoadProperty(tn, prop)
        Next
    End Sub

    Private Sub LoadProperty(ByVal tn As TreeNode, ByVal prop As [Property])
        tn = tn.Nodes.Add("Property " & prop.Name)
        tn.Tag = prop
    End Sub

    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        If TreeView1.SelectedNode.Tag Is Nothing Then
            SHowElse()
            Return
        End If

        If TypeOf TreeView1.SelectedNode.Tag Is [Property] Then
            ShowProperty(TreeView1.SelectedNode.Tag)
        ElseIf TypeOf TreeView1.SelectedNode.Tag Is Type Then
            ShowType(TreeView1.SelectedNode.Tag)
        ElseIf TypeOf TreeView1.SelectedNode.Tag Is IList Then
            ShowList(TreeView1.SelectedNode.Tag)
        Else
            SHowElse()
        End If

    End Sub

    Private Sub ShowProperty(ByVal prop As [Property])
        Dim ds As DataSet
        Dim dv As DataView

        ds = PropertyAsDataSet(prop)
        dv = New DataView(ds.Tables(0))
        dv.AllowNew = False
        dv.AllowDelete = False

        CloseChildren()

        Dim cf As New GridForm

        cf.Text = "Property " & prop.Name
        cf.DataSource = dv
        cf.ValuesReadOnly = False
        cf.MdiParent = Me
        cf.Show()
        currentChild = cf
    End Sub

    Private Sub CloseChildren()
        'If Not currentChild Is Nothing Then
        '    currentChild.Close()
        '    currentChild = Nothing
        'End If
    End Sub

    Private Sub ShowList(ByVal list As IList)
        CloseChildren()

        Dim cf As New GridForm

        cf.Text = "List"
        cf.DataSource = list
        cf.ValuesReadOnly = True
        cf.MdiParent = Me
        cf.Show()
        currentChild = cf
    End Sub

    Private Sub ShowType(ByVal type As Type)
        Dim ds As DataSet
        Dim dv As DataView

        ds = TypeAsDataSet(type)
        dv = New DataView(ds.Tables(0))
        dv.AllowNew = False
        dv.AllowDelete = False

        CloseChildren()

        Dim cf As New GridForm

        cf.Text = "Type " & type.Name
        cf.DataSource = dv
        cf.ValuesReadOnly = False
        cf.MdiParent = Me
        cf.Show()
        currentChild = cf
    End Sub

    Private Sub SHowElse()
        CloseChildren()
    End Sub

    Private Sub MenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem4.Click
        CloseChildren()

        Dim gf As New GridForm
        gf.MdiParent = Me
        gf.Show()
        currentChild = gf
    End Sub
End Class
