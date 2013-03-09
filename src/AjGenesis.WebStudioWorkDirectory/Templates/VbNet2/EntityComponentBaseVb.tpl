<#

message	"Generating Base Business Component for Entity ${Entity.Name}..."

include "Templates/VbNet2/VbFunctions.tpl"
include "Templates/EntityFunctions.tpl"

EntitySqlProperties	= SqlProperties(Entity)
EntityNoIdSqlProperties	= SqlNoIdProperties(Entity)
EntityIdProperty = IdProperty(Entity)

include "Templates/VbNet2/Prologue.tpl"

#>

'
'	Project ${Project.Name}
'		${Project.Description}
'	Entity	${Entity.Name}
'		${Entity.Description}
'	
'

Imports System.Collections.Generic

Imports ${Project.Name}.Entities
Imports ${Project.Name}.Data

Public Class ${Entity.Name}ComponentBase
	Protected Shared m${Entity.Name}Data as new ${Entity.Name}Data

	Public Overridable Sub Validate(entity as ${Entity.Name})
<#
	for each Property in Entity.Properties where Property.Required
		if IsNumeric(Property) then
#>
		if entity.${Property.Name} = 0 then
<#
		else
#>
		if entity.${Property.Name} is nothing orelse entity.${Property.Name} = "" then
<#
		end if
#>
			throw new Exception("${Property.Description} required")
		end if
<#
	end for
#>
	End Sub

	Public Overridable Sub ValidateNew(entity as ${Entity.Name})
<#
	for each Property in Entity.Properties where Property.Required
		if IsNumeric(Property) then
#>
		if entity.${Property.Name} = 0 then
<#
		else
#>
		if entity.${Property.Name} is nothing orelse entity.${Property.Name} = "" then
<#
		end if
#>
			throw new Exception("${Property.Description} required")
		end if
<#
	end for
#>
	End Sub

	Public Overridable Sub ValidateDelete(entity as ${Entity.Name})

	End Sub

	Public Sub Insert(entity as ${Entity.Name})
		ValidateNew(entity)
		m${Entity.Name}Data.Insert(entity)
	End Sub

	Public Sub Update(entity as ${Entity.Name})
		Validate(entity)
		m${Entity.Name}Data.Update(entity)
	End Sub

	Public Sub Delete(id as Integer)
		ValidateDelete(GetById(id))
		m${Entity.Name}Data.Delete(id)
	End Sub

	Public Function GetById(id as Integer) as ${Entity.Name}
		return m${Entity.Name}Data.GetById(id)
	End Function

	Public Function GetAll() as List(of ${Entity.Name})
		return m${Entity.Name}Data.GetAll()
	End Function

	Public Function GetAllAsDs() as DataSet
		return m${Entity.Name}Data.GetAllAsDs()
	End Function
<#
	if Entity.HasReferences then
#>

	Public Function GetAllEx() as DataSet
		return m${Entity.Name}Data.GetAllEx()
	End Function
<#
	end if

	for each Property in Entity.Properties where Property.Reference
#>

	Public Function GetBy${Property.Reference.Name}(${Property.Name} as ${VbType(Property)}) as List(of ${Entity.Name})
		return m${Entity.Name}Data.GetBy${Property.Reference.Name}(${Property.Name})
	End Function

	Public Function GetBy${Property.Reference.Name}Ex(${Property.Name} as ${VbType(Property)}) as DataSet
		return m${Entity.Name}Data.GetBy${Property.Reference.Name}Ex(${Property.Name})
	End Function
<#
	end for
#>
End Class

