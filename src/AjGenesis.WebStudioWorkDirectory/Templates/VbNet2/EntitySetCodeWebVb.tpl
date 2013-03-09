<#
include "Templates/VbNet2/Prologue.tpl"
#>

Imports ${Project.Name}.Services
Imports ${Project.Name}.Entities

Public Partial Class ${WebPage.Prefix}${Entity.SetName}Page
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
<#
	if Entity.HasReferences then
#>
            gvwData.DataSource = ${Entity.Name}Service.GetAllEx
<#
	else
#>
            gvwData.DataSource = ${Entity.Name}Service.GetList
<#
	end if
#>
            gvwData.DataBind()
        End If
    End Sub

End Class
