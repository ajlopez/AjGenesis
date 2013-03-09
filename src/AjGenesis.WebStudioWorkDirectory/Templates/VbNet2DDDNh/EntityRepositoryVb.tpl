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

Imports AjNHibernate

Public Class ${Entity.Name}Repository
	Public Shared Sub Add(entity as ${Entity.Name})
		Repository.Current.SaveObject(entity)
	End Sub

	Public Shared Sub Update(entity as ${Entity.Name})
		Repository.Current.UpdateObject(entity)
	End Sub

	Public Shared Sub Remove(entity as ${Entity.Name})
		Repository.Current.DeleteObject(entity)
	End Sub

	Public Shared Function GetById(id as Integer) as ${Entity.Name}
		if id=0 then
			return Nothing
		end if

		return DirectCast(Repository.Current.GetObjectById(GetType(${Entity.Name}),id), ${Entity.Name})
	End Function

	Public Shared Function GetAll() as IList
		return Repository.Current.GetAll(GetType(${Entity.Name}))
	End Function
End Class

