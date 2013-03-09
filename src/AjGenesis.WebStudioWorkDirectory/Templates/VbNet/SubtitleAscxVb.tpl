
Public MustInherit Class Subtitle
    Inherits System.Web.UI.UserControl
    Protected WithEvents PageSubtitle As System.Web.UI.WebControls.Label

    Public Property Title() As String
        Get
            Return PageSubtitle.Text
        End Get
        Set(ByVal Value As String)
            PageSubtitle.Text = Value
        End Set
    End Property

#Region " Web Form Designer Generated Code "


    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        DataBind()
    End Sub

End Class
