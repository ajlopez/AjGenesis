Imports ${Project.Name}.Services
Imports ${Project.Name}.Entities

Public Class ${Entity.Name}DeletePage
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
		Dim IdEntity as Integer

		IdEntity = CInt(Request("Id"))
		${Entity.Name}Service.Delete(IdEntity)
		Response.Redirect("${Entity.SetName}.aspx")
		Response.End
    End Sub

End Class
