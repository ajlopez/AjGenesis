<#
include "Templates/VbNet2DDDNh/Prologue.tpl"
#>

Imports ${Project.Name}.Application
Imports ${Project.Name}.Domain

Public Partial Class ${WebPage.Prefix}${Entity.Name}Page
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

<#
	for each Property in Entity.Properties where Property.Reference
#>
	Public Entity${Property.Reference.Name} as ${Property.Reference.Name}
<#
	end for
#>

<#
	for each Property in Entity.Properties where Property.Enumeration
#>
	Public ReadOnly Property ${Property.Name}Description as String
		Get
			return Enumerations.Translate(Enumerations.${Property.Enumeration.Name}List, Entity.${Property.Name})
		End Get
	End Property
<#
	end for
#>

    Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
			IdEntity = CInt(Request("Id"))
			Entity = ${Entity.Name}Service.Get${Entity.Name}ById(IdEntity)
            	DataBind()
        End If
    End Sub

End Class
