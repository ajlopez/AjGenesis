<#

message	"Generating Form Code Behind for Entity ${Entity.Name}..."

include "Templates/VbNetNh/VbFunctions.tpl"
include "Templates/EntityFunctions.tpl"

EntitySqlProperties	= SqlProperties(Entity)
EntityNoIdSqlProperties	= SqlNoIdProperties(Entity)
EntityIdProperty = IdProperty(Entity)
#>

Imports ${Project.Name}.Services
Imports ${Project.Name}.Entities

Public Class ${Entity.Name}UpdatePage
    Inherits System.Web.UI.Page

    Protected WithEvents btnAccept As System.Web.UI.WebControls.Button

<#
for each Property in EntityNoIdSqlProperties
#>
    Protected WithEvents txt${Property.Name} As System.Web.UI.WebControls.TextBox
<#
end for
#>

    Protected WithEvents lblMensaje As System.Web.UI.WebControls.Label
    
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
			If Request("Id") Is Nothing then
				IdEntity = 0
				Entity = new ${Entity.Name}
			else
				IdEntity = CInt(Request("Id"))
				Entity = ${Entity.Name}Service.Get${Entity.Name}ById(IdEntity)
			End If
            	DataBind()
        End If
    End Sub

    Private Function FormValidate() As Boolean
        Return True
    End Function

    Private Sub Update()
		if IdEntity>0 then
			Entity = ${Entity.Name}Service.GetById(IdEntity)
		else
			Entity = New ${Entity.Name}()
        end if

<#
for each Property in EntityNoIdSqlProperties
#>        
        Entity.${Property.Name} = txt${Property.Name}.Text
<#
end for
#>        

        If IdEntity = 0 Then
            ${Entity.Name}Service.Insert(Entity)
        Else
            ${Entity.Name}Service.Update(Entity)
        End If
    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        If Not IsValid Then
            Return
        End If

        Try
            If FormValidate() Then
                Update()
		        If IdEntity = 0 Then
		            Server.Transfer("${Entity.SetName}.aspx")
				Else
					Server.Transfer("${Entity.Name}.aspx?Id=" & IdEntity)
		        End If
            End If
        Catch Ex As Exception
            lblMensaje.Visible = True
            lblMensaje.Text = Ex.Message
        End Try
    End Sub
End Class
