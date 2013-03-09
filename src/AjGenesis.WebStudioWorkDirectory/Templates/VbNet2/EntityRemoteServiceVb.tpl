
#message	"Generating Remote Services for Entity ${Entity.Name}..."

#include "Templates/VbNet/VbFunctions.tpl"
#include "Templates/EntityFunctions.tpl"

#define	EntitySqlProperties	SqlProperties(Entity)
#define	EntityNoIdSqlProperties	SqlNoIdProperties(Entity)
#define	EntityIdProperty IdProperty(Entity)


'
'	Project ${Project.Name}
'		${Project.Description}
'	Entity	${Entity.Name}
'		${Entity.Description}
'	
'

Imports ${Project.Name}.Entities
Imports ${Project.Name}.Services

Public Class ${Entity.Name}RemoteService
    Inherits System.MarshalByRefObject

	Public Sub Insert(entity as ${Entity.Name})
		${Entity.Name}Service.Insert(entity)
	End Sub

	Public Sub Update(entity as ${Entity.Name})
		${Entity.Name}Service.Update(entity)
	End Sub

	Public Sub Delete(id as Integer)
		${Entity.Name}Service.Delete(id)
	End Sub

	Public Function GetById(id as Integer) as ${Entity.Name}
		return ${Entity.Name}Service.GetById(id)
	End Function

	Public Function GetAll() as ${Entity.Name}()
		Dim arr as ArrayList = DirectCast(${Entity.Name}Service.GetAll(),ArrayList)
		return DirectCast(arr.ToArray(GetType(${Entity.Name})),${Entity.Name}())
	End Function

	Public Function GetList() as DataSet
		return ${Entity.Name}Service.GetList()
	End Function
End Class
