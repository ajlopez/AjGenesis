<#

message	"Generating Domain Services for Entity ${Entity.Name}..."

include "Templates/CSharp2DDDNh/CSharpFunctions.tpl"
include "Templates/EntityFunctions.tpl"

EntitySqlProperties	= SqlProperties(Entity)
EntityNoIdSqlProperties	= SqlNoIdProperties(Entity)
EntityIdProperty = IdProperty(Entity)

#>

/*
 *	Project ${Project.Name}
 *		${Project.Description}
 *	Entity	${Entity.Name}
 *		${Entity.Description}
 *	
 *	Note: This implementation is too much oriented to CRUD operations
 *	
 */

using System;

namespace ${Project.SystemName}.Domain {

	public class ${Entity.Name}Manager {

		public static void AddNew(${Entity.Name} entity <#
	for each Property in Entity.Properties where Property.Reference
		Print ", " & Property.Reference.Name & " " & Property.Reference.VarName 
	end for
#>) {
<#
	for each Property in Entity.Properties where Property.Reference
#>
			entity.${Property.Reference.Name} = ${Property.Reference.VarName};
<#
	end for
#>

			Add(entity);
		}

		public static void Add(${Entity.Name} entity) {
			// TODO: Apply Specifications

			${Entity.Name}Repository.Add(entity);
		}

		public static void Update(${Entity.Name} entity) {
			// TODO: Apply Specifications

			${Entity.Name}Repository.Update(entity);
		}

		public static void Delete(${Entity.Name} entity) {
			// TODO: Apply Specifications

<#
	for each Property in Entity.Properties where Property.Reference
#>
			entity.${Property.Reference.Name}.${Entity.SetName}.Remove(entity);
<#
	end for
#>

			${Entity.Name}Repository.Remove(entity);
		}
	}
}

