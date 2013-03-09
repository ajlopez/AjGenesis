Imports ${Project.Name}.Services
Imports ${Project.Name}.Entities

Public Class ${Entity.Name}Page
    Inherits System.Web.UI.Page

    Public Entity As ${Entity.Name}

    Public Property IdEntity() As Integer
        Get
            Return DirectCast(ViewState("IdEntity"), Integer)
        End Get
        Set(ByVal Value As Integer)
            ViewState("IdEntity") = Value
        End Set
    End Property

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
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
        If Not IsPostBack Then
			IdEntity = CInt(Request("Id"))
			Entity = ${Entity.Name}Service.GetById(IdEntity)
            	DataBind()
        End If
    End Sub

End Class
