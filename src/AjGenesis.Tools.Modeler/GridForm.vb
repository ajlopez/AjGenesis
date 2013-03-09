Public Class GridForm
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
    Friend WithEvents dtgValues As System.Windows.Forms.DataGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.dtgValues = New System.Windows.Forms.DataGrid
        CType(Me.dtgValues, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtgValues
        '
        Me.dtgValues.DataMember = ""
        Me.dtgValues.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtgValues.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dtgValues.Location = New System.Drawing.Point(0, 0)
        Me.dtgValues.Name = "dtgValues"
        Me.dtgValues.Size = New System.Drawing.Size(292, 266)
        Me.dtgValues.TabIndex = 0
        '
        'GridForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Controls.Add(Me.dtgValues)
        Me.Name = "GridForm"
        Me.Text = "GridForm"
        CType(Me.dtgValues, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Property DataSource() As Object
        Get
            Return dtgValues.DataSource
        End Get
        Set(ByVal Value As Object)
            dtgValues.DataSource = Value
        End Set
    End Property

    Public Property ValuesReadOnly() As Boolean
        Get
            Return dtgValues.ReadOnly
        End Get
        Set(ByVal Value As Boolean)
            dtgValues.ReadOnly = Value
        End Set
    End Property

End Class
