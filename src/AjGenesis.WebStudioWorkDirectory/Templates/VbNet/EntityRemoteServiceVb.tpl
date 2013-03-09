<#
message	"Generating Remote Services for Entity ${Entity.Name}..."

include "Templates/VbNet/VbFunctions.tpl"
include "Templates/EntityFunctions.tpl"

EntitySqlProperties =	SqlProperties(Entity)
EntityNoIdSqlProperties =	SqlNoIdProperties(Entity)
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
<#
	if Entity.HasReferences then
#>

	Public Shared Function GetAllEx() as DataSet
		return ${Entity.Name}Service.GetAllEx()
	End Function
<#
	end if

	for each Property in Entity.Properties where Property.Reference
#>

	Public Shared Function GetBy${Property.Reference.Name}(${Property.Name} as ${VbType(Property)}) as IList
		return ${Entity.Name}Service.GetBy${Property.Reference.Name}(${Property.Name})
	End Function

	Public Shared Function GetBy${Property.Reference.Name}Ex(${Property.Name} as ${VbType(Property)}) as DataSet
		return ${Entity.Name}Service.GetBy${Property.Reference.Name}Ex(${Property.Name})
	End Function
<#
	end for
#>
End Class
