<#

message	"Generating Business Component for Entity ${Entity.Name}..."

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

Imports ${Project.Name}.Entities
Imports ${Project.Name}.Data

Public Class ${Entity.Name}Component
	Inherits ${Entity.Name}ComponentBase

	Public Overrides Sub Validate(entity as ${Entity.Name})
		MyBase.Validate(entity)
	End Sub

	Public Overrides Sub ValidateNew(entity as ${Entity.Name})
		MyBase.ValidateNew(entity)
	End Sub

	Public Overrides Sub ValidateDelete(entity as ${Entity.Name})
		MyBase.ValidateDelete(entity)
	End Sub
End Class

