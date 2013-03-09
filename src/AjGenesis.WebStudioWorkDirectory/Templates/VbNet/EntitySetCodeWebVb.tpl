Imports ${Project.Name}.Services
Imports ${Project.Name}.Entities

Public Class ${Entity.SetName}Page
    Inherits System.Web.UI.Page
    Protected WithEvents dtgData As System.Web.UI.WebControls.DataGrid

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
<#
	if Entity.HasReferences then
#>
            dtgData.DataSource = ${Entity.Name}Service.GetAllEx
<#
	else
#>
            dtgData.DataSource = ${Entity.Name}Service.GetList
<#
	end if
#>
            dtgData.DataBind()
        End If
    End Sub

End Class
