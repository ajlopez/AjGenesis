<#

message	"Generating Domain Services for Entity ${Entity.Name}..."

include "Templates/VbNet2DDDNh/VbFunctions.tpl"
include "Templates/EntityFunctions.tpl"

EntitySqlProperties	= SqlProperties(Entity)
EntityNoIdSqlProperties	= SqlNoIdProperties(Entity)
EntityIdProperty = IdProperty(Entity)

#>

'
'	Project ${Project.Name}
'		${Project.Description}
'	Domain Services for Entity	${Entity.Name}
'		${Entity.Description}
'	
'	Note: This implementation is too much oriented to CRUD operations
'

Public Class ${Entity.Name}Manager

	Public Shared Sub AddNew(entity as ${Entity.Name}<#
	for each Property in Entity.Properties where Property.Reference
		Print ", " & Property.Reference.VarName & " as " & Property.Reference.Name
	end for
#>)

<#
	for each Property in Entity.Properties where Property.Reference
#>
		entity.${Property.Reference.Name} = ${Property.Reference.VarName}
<#
	end for
#>

		Add(entity)
	End Sub

	Public Shared Sub Add(entity as ${Entity.Name})

		'' TODO: Apply Specifications

		${Entity.Name}Repository.Add(entity)
	End Sub

	Public Shared Sub Update(entity as ${Entity.Name})

		'' TODO: Apply Specifications

		${Entity.Name}Repository.Update(entity)
	End Sub

	Public Shared Sub Delete(entity as ${Entity.Name})

		'' TODO: Apply Specifications

<#
	for each Property in Entity.Properties where Property.Reference
#>
		entity.${Property.Reference.Name}.${Entity.SetName}.Remove(entity)
<#
	end for
#>

		${Entity.Name}Repository.Remove(entity)
	End Sub
End Class

