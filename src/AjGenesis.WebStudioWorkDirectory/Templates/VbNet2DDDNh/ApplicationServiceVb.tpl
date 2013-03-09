<#

message	"Generating Application Services for Entity ${Entity.Name}..."

include "Templates/VbNet2DDDNh/VbFunctions.tpl"
include "Templates/EntityFunctions.tpl"

EntitySqlProperties	= SqlProperties(Entity)
EntityNoIdSqlProperties	= SqlNoIdProperties(Entity)
EntityIdProperty = IdProperty(Entity)
IdentityType = VbIdentityType()

#>

'
'	Project ${Project.Name}
'		${Project.Description}
'	Application Service for Entity	${Entity.Name}
'		${Entity.Description}
'	
'	Note: This implementation is "too much" oriented to CRUD operations
'

Imports ${Project.Name}.Domain

Public Class ${Entity.Name}Service

	' New Entity in the System, using a Domain Object

	Public Shared Sub New${Entity.Name}(entity as ${Entity.Name}<#
	for each Property in Entity.Properties where Property.Reference
		Print ", " & Property.VarName & " as " & Property.VbType
	end for
#>)
<#
	for each Property in Entity.Properties where Property.Reference
#>
		Dim ${Property.Reference.VarName} as ${Property.Reference.Name} = Nothing

		if ${Property.VarName} <> 0 then
			${Property.Reference.VarName} = ${Property.Reference.Name}Repository.GetById(${Property.VarName})
		end if

<#
	end for
#>
		${Entity.Name}Manager.AddNew(entity<#
	for each Property in Entity.Properties where Property.Reference
		Print ", " & Property.Reference.VarName
	end for
#>)
	End Sub


	' New Entity in the System, using a Message

	Public Shared Sub New${Entity.Name}(entityinfo as ${Entity.Name}Info)
		Dim entity as ${Entity.Name}

		entity = ${Entity.Name}.Create( _
<#
	p = 0
	for each Property in Entity.Properties where Property.Type<>"Id"
		if p then
			Print ", "
		end if
		p = p + 1
		if Property.Reference then
#>
			${Property.Reference.Name}Repository.GetById(entityinfo.${Property.Name}) _
<#
		else
#>
			entityinfo.${Property.Name} _
<#
		end if
	end for
#>
)

		${Entity.Name}Manager.Add(entity)
	End Sub

	' Update Entity

	Public Shared Sub Update${Entity.Name}(entity as ${Entity.Name})
		${Entity.Name}Manager.Update(entity)
	End Sub

	' Update Entity using a Message

	Public Shared Sub Update${Entity.Name}(entityinfo as ${Entity.Name}Info)
		Dim entity as ${Entity.Name}

		entity = ${Entity.Name}Repository.GetById(entityinfo.${Entity.IdProperty.Name})

<#
	for each Property in Entity.Properties
		if Property.Reference then
#>
		entity.${Property.Reference.Name} = ${Property.Reference.Name}Repository.GetById(entityinfo.${Property.Name})
<#
		else
#>
		entity.${Property.Name} = entityinfo.${Property.Name}
<#
		end if
	end for
#>

		${Entity.Name}Manager.Update(entity)
	End Sub

	Public Shared Sub Delete${Entity.Name}(id as ${IdentityType})
		Dim entity as ${Entity.Name}

		entity = ${Entity.Name}Repository.GetById(id)

		${Entity.Name}Manager.Delete(entity)
	End Sub

	Public Shared Function Get${Entity.Name}ById(id as Integer) as Domain.${Entity.Name}
		return ${Entity.Name}Repository.GetById(id)
	End Function

	Public Shared Function Get${Entity.SetName}() as IList
		return ${Entity.Name}Repository.GetAll()
	End Function
End Class

