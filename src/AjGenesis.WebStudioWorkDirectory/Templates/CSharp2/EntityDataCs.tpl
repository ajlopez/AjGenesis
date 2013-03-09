<#

message	"Generating Data Access Object for Entity ${Entity.Name}..."

include "Templates/CSharp2/CSharpFunctions.tpl"
include "Templates/EntityFunctions.tpl"

EntitySqlProperties	= SqlProperties(Entity)
EntityNoIdSqlProperties	= SqlNoIdProperties(Entity)
EntityIdProperty = IdProperty(Entity)

include "Templates/CSharp2/Prologue.tpl"	

#>

/*
 *	Project ${Project.Name}
 *		${Project.Description}
 *	Entity	${Entity.Name}
 *		${Entity.Description}
 *	
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using AjFramework.Data;

using ${Project.Name}.Entities;

namespace ${Project.SystemName}.Data {

	public class ${Entity.Name}Data {

		public void Insert(${Entity.Name} entity) {
			DataParameter dpid = new DataParameter();
			dpid.Value = entity.${EntityIdProperty.Name};

			DataService.ExecuteNonQuery("${Entity.Name}Insert", CommandType.StoredProcedure, 
				dpid,
<#
for each	Property in EntitySqlProperties where Property.Type<>"Id"
	message "Processing Field for Insert ${Property.Name}"
#>
				entity.${Property.Name}${OnLast("",",")} 
<#
end for
#>
			);

			entity.${EntityIdProperty.Name} = (int) dpid.Value;
		}

		public void Update(${Entity.Name} entity) {
			DataService.ExecuteNonQuery("${Entity.Name}Update", CommandType.StoredProcedure, 
<#
for each Property in EntitySqlProperties
	message	"Processing Field for Update ${Property.Name}"
#>
				entity.${Property.Name}${OnLast("",",")} 
<#
end for
#>
			);
		}

		public void Delete(int id) {
			DataService.ExecuteNonQuery("${Entity.Name}Delete", CommandType.StoredProcedure, id);
		}

		public ${Entity.Name} GetById(int id) {
			IDataReader reader = null;

			try {
				reader = DataService.ExecuteReader("${Entity.Name}GetById", CommandType.StoredProcedure, id);

				if (!reader.Read())
					return null;
			
				${Entity.Name} entity;

				entity = Make(reader);

				return entity;
			}
			finally {
				reader.Close();
			}
		}

		public List<${Entity.Name}> GetAll() {
			IDataReader reader = null;
			List<${Entity.Name}> list = new List<${Entity.Name}>();

			reader = DataService.ExecuteReader("${Entity.Name}GetAll", CommandType.StoredProcedure );
			${Entity.Name} entity;
	
			while (reader.Read()) {
				entity = Make(reader);
				list.Add(entity);
			}
			
			reader.Close();

			return list;
		}

		public DataSet GetAllAsDs() {
			return DataService.ExecuteDataSet("${Entity.Name}GetAll", CommandType.StoredProcedure );
		}
<#
	if Entity.HasReferences then
#>
		public DataSet GetAllEx() {
			return DataService.ExecuteDataSet("${Entity.Name}GetAllEx", CommandType.StoredProcedure );
		}

<#
	end if

	for each Property in Entity.Properties where Property.Reference
#>

		public List<${Entity.Name}> GetBy${Property.Reference.Name}(${CSharpType(Property)} ${Property.Name}) {
			IDataReader reader = null;
			List<${Entity.Name}> list = new List<${Entity.Name}>();

			reader = DataService.ExecuteReader("${Entity.Name}GetBy${Property.Reference.Name}", CommandType.StoredProcedure, ${Property.Name});

			${Entity.Name} entity;
	
			while (reader.Read()) {
				entity = Make(reader);
				list.Add(entity);
			}
			
			reader.Close();

			return list;
		}

		public DataSet GetBy${Property.Reference.Name}Ex(${CSharpType(Property)} ${Property.Name}) {
			return DataService.ExecuteDataSet("${Entity.Name}GetBy${Property.Reference.Name}Ex", CommandType.StoredProcedure, ${Property.Name});
		}
<#
	end for
#>

		private ${Entity.Name} Make(IDataReader reader) {
			${Entity.Name} entity = new ${Entity.Name}();

<#
message "Processing Make"
#>

<#
for each Property in EntitySqlProperties
#>
			if (reader["${Property.Name}"] == System.DBNull.Value)
				entity.${Property.Name} = ${CSharpNullValue(Property.Type)};
			else
				entity.${Property.Name} = (${CSharpType(Property)}) reader["${Property.Name}"];
<#		
end for
#>

			return entity;
		}
	}
}

