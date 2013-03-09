<#

message	"Generating Form Code Behind for Entity ${Entity.Name}..."

include "Templates/VbNet2DDDNh/VbFunctions.tpl"
include "Templates/EntityFunctions.tpl"

EntitySqlProperties	= SqlProperties(Entity)
EntityNoIdSqlProperties	= SqlNoIdProperties(Entity)
EntityIdProperty = IdProperty(Entity)

include "Templates/VbNet2DDDNh/Prologue.tpl"

#>

Imports System
Imports System.Data

Imports ${Project.Name}.Application
Imports ${Project.Name}.Domain

Public Partial Class ${WebPage.Prefix}${Entity.Name}UpdatePage
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
	Public ${Property.Reference.SetName} as IList
<#
	end for
#>

<#
	for each Property in Entity.Properties where Property.Enumeration
#>
	Public ReadOnly Property ${Property.Enumeration.Name}List as IList
		Get
			return Enumerations.${Property.Enumeration.Name}List
		End Get
	End Property
<#
	end for
#>

    Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
			If Request("Id") Is Nothing then
				IdEntity = 0
				Entity = new ${Entity.Name}
			else
				IdEntity = CInt(Request("Id"))
				Entity = ${Entity.Name}Service.Get${Entity.Name}ById(IdEntity)
			End If
<#
	for each Property in Entity.Properties where Property.Reference
#>
			${Property.Reference.SetName} = Get${Property.Reference.SetName}
<#
	end for
#>
            	DataBind()

			if IdEntity>0 then
<#
	for each Property in Entity.Properties where Property.Reference
#>
				ddl${Property.Reference.SetName}.SelectedValue = Entity.${Property.Reference.Name}.${Property.Reference.IdProperty.Name}
<#
	end for
#>
<#
	for each Property in Entity.Properties where Property.Enumeration
#>
				ddl${Property.Name}.SelectedValue = Entity.${Property.Name}
<#
	end for
#>
			else
<#
	for each Property in Entity.Properties where Property.Reference
#>
				if not Request("${Property.Name}") is nothing then
					ddl${Property.Reference.SetName}.SelectedValue = CInt(Request("${Property.Name}"))
				end if
<#
	end for
#>
			end if
        End If
    End Sub

    Private Function FormValidate() As Boolean
        Return True
    End Function

<#
	for each Property in Entity.Properties where Property.Reference
#>
    Private Function Get${Property.Reference.SetName}() As IList
        return ${Property.Reference.Name}Service.Get${Property.Reference.SetName}()
    End Function

<#
	end for
#>
    Private Sub Update()
		Dim entity as new ${Entity.Name}Info

		entity.${Entity.IdProperty.Name} = IdEntity

<#
for each Property in EntityNoIdSqlProperties
	if Property.Reference then
#>
        Entity.${Property.Name} = ddl${Property.Reference.SetName}.SelectedValue
<#
	else
		if Property.Enumeration then
#>
        Entity.${Property.Name} = ddl${Property.Name}.SelectedValue
<#
		else
#>        
		Entity.${Property.Name} = txt${Property.Name}.Text
<#
		end if
	end if
end for
#>        

        If IdEntity = 0 Then
            ${Entity.Name}Service.New${Entity.Name}(Entity)
        Else
            ${Entity.Name}Service.Update${Entity.Name}(Entity)
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
