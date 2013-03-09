<#

message	"Generating Services for Entity ${Entity.Name}..."

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
Imports ${Project.Name}.Business

Imports AjFramework.Data

Public Class ${Entity.Name}Service
	Private Shared m${Entity.Name}Component as new ${Entity.Name}Component

	Public Shared Sub Insert(entity as ${Entity.Name})
		m${Entity.Name}Component.Insert(entity)
	End Sub

	Public Shared Sub Update(entity as ${Entity.Name})
		m${Entity.Name}Component.Update(entity)
	End Sub

	Public Shared Sub Delete(id as Integer)
		m${Entity.Name}Component.Delete(id)
	End Sub

	Public Shared Function GetById(id as Integer) as ${Entity.Name}
		return m${Entity.Name}Component.GetById(id)
	End Function

	Public Shared Function GetAll() as List(Of ${Entity.Name})
		return m${Entity.Name}Component.GetAll()
	End Function

	Public Shared Function GetList() as DataSet
		return m${Entity.Name}Component.GetAllAsDs()
	End Function
<#
	if Entity.HasReferences then
#>

	Public Shared Function GetAllEx() as DataSet
		return m${Entity.Name}Component.GetAllEx()
	End Function
<#
	end if

	for each Property in Entity.Properties where Property.Reference
#>

	Public Shared Function GetBy${Property.Reference.Name}(${Property.Name} as ${VbType(Property)}) as List(Of ${Entity.Name})
		return m${Entity.Name}Component.GetBy${Property.Reference.Name}(${Property.Name})
	End Function

	Public Shared Function GetBy${Property.Reference.Name}Ex(${Property.Name} as ${VbType(Property)}) as DataSet
		return m${Entity.Name}Component.GetBy${Property.Reference.Name}Ex(${Property.Name})
	End Function
<#
	end for
#>
End Class

