<#

message	"Generating Services for Entity ${Entity.Name}..."

include "Templates/VbNetNh/VbFunctions.tpl"
include "Templates/EntityFunctions.tpl"

EntitySqlProperties	= SqlProperties(Entity)
EntityNoIdSqlProperties	= SqlNoIdProperties(Entity)
EntityIdProperty = IdProperty(Entity)

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

Public Class ${Entity.Name}Service
	Public Shared Sub Insert(entity as ${Entity.Name})
		${Entity.Name}DAO.Insert(entity)
	End Sub

	Public Shared Sub Update(entity as ${Entity.Name})
		${Entity.Name}DAO.Update(entity)
	End Sub

	Public Shared Sub Delete(entity as ${Entity.Name})
		${Entity.Name}DAO.Delete(entity)
	End Sub

	Public Shared Sub Delete(id as Integer)
		${Entity.Name}DAO.Delete(id)
	End Sub

	Public Shared Function GetById(id as Integer) as ${Entity.Name}
		return ${Entity.Name}DAO.GetById(id)
	End Function

	Public Shared Function GetAll() as IList
		return ${Entity.Name}DAO.GetAll()
	End Function

	Public Shared Function GetList() as IList
		return ${Entity.Name}DAO.GetAll()
	End Function
End Class

