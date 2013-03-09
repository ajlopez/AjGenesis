<#

message	"Generating Data Access Object for Entity ${Entity.Name}..."

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

Imports AjFramework.Data

Imports ${Project.Name}.Entities

Public Class ${Entity.Name}Data

	Public Sub Insert(entity as ${Entity.Name})
		Dim dpid as new DataParameter

		dpid.Value = entity.${EntityIdProperty.Name}

		DataService.ExecuteNonQuery("${Entity.Name}Insert", CommandType.StoredProcedure, _
			dpid, _
<#
for each	Property in EntitySqlProperties where Property.Type<>"Id"
	message "Processing Field in Insert ${Property.Name}"
#>
			entity.${Property.Name}${OnLast("",",")} _
<#
end for
#>
		)

		entity.${EntityIdProperty.Name} = dpid.Value
	End Sub

	Public Sub Update(entity as ${Entity.Name})
		DataService.ExecuteNonQuery("${Entity.Name}Update", CommandType.StoredProcedure, _
<#
for each Property in EntitySqlProperties
	message	"Procesando Campo en Update ${Property.Name}"
#>
			entity.${Property.Name}${OnLast("",",")} _
<#
end for
#>
		)
	End Sub

	Public Sub Delete(id as Integer)
		DataService.ExecuteNonQuery("${Entity.Name}Delete", CommandType.StoredProcedure, id)
	End Sub

	Public Function GetById(id as Integer) as ${Entity.Name}
		Dim reader as IDataReader = Nothing

		try
			reader = DataService.ExecuteReader("${Entity.Name}GetById", CommandType.StoredProcedure, id)

			if not reader.Read() then
				return Nothing
			end if
			
			Dim entity as ${Entity.Name}

			entity = Make(reader)

			return entity
		finally
			reader.Close()
		end try
	End Function

	Public Function GetAll() as List(of ${Entity.Name})
		Dim reader as IDataReader
		Dim list as new List(of ${Entity.Name})()

		reader = DataService.ExecuteReader("${Entity.Name}GetAll", CommandType.StoredProcedure )
		Dim entity as ${Entity.Name}
	
		while reader.Read()
			entity = Make(reader)
			list.Add(entity)
		end while
			
		reader.Close()

		return list
	End Function

	Public Function GetAllAsDs() as DataSet
		return DataService.ExecuteDataSet("${Entity.Name}GetAll", CommandType.StoredProcedure )
	End Function

<#
	if Entity.HasReferences then
#>
	Public Function GetAllEx() as DataSet
		return DataService.ExecuteDataSet("${Entity.Name}GetAllEx", CommandType.StoredProcedure )
	End Function

<#
	end if

	for each Property in Entity.Properties where Property.Reference
#>

	Public Function GetBy${Property.Reference.Name}(${Property.Name} as ${VbType(Property)}) as List(of ${Entity.Name})
		Dim reader as IDataReader
		Dim list as new List(of ${Entity.Name})()

		reader = DataService.ExecuteReader("${Entity.Name}GetBy${Property.Reference.Name}", CommandType.StoredProcedure, ${Property.Name})
		Dim entity as ${Entity.Name}
	
		while reader.Read()
			entity = Make(reader)
			list.Add(entity)
		end while
			
		reader.Close()

		return list
	End Function

	Public Function GetBy${Property.Reference.Name}Ex(${Property.Name} as ${VbType(Property)}) as DataSet
		return DataService.ExecuteDataSet("${Entity.Name}GetBy${Property.Reference.Name}Ex", CommandType.StoredProcedure, ${Property.Name})
	End Function
<#
	end for
#>

	Private Function Make(reader as IDataReader) as ${Entity.Name}
		Dim entity as new ${Entity.Name}

<#
message "Procesando Make"
#>

<#
for each Property in EntitySqlProperties
#>
		if reader("${Property.Name}") is System.DbNull.Value then
			entity.${Property.Name} = ${VbNullValue(Property.Type)}
		else
			entity.${Property.Name} = CType(reader("${Property.Name}"),${VbType(Property)})
		end if
<#		
end for
#>

		return entity
	End Function
End Class

