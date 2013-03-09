<#

message	"Generating Application Services for Entity ${Entity.Name}..."

include "Templates/CSharp2DDDNh/CSharpFunctions.tpl"
include "Templates/EntityFunctions.tpl"

EntitySqlProperties	= SqlProperties(Entity)
EntityNoIdSqlProperties	= SqlNoIdProperties(Entity)
EntityIdProperty = IdProperty(Entity)
IdentityType = CSharpIdentityType()

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
using System.Collections;

using ${Project.Name}.Domain;

namespace ${Project.SystemName}.Application {

	public class ${Entity.Name}Service {

		// New Entity in the System, using a Domain Object

		public static void New${Entity.Name}(${Entity.Name} entity<#
	for each Property in Entity.Properties where Property.Reference
		Print ", " & Property.CsType & " " & Property.VarName
 	end for
#>) {
<#
	for each Property in Entity.Properties where Property.Reference
#>
			${Property.Reference.Name} ${Property.Reference.VarName} = null;

			if (${Property.VarName} != 0)
				${Property.Reference.VarName} = ${Property.Reference.Name}Repository.GetById(${Property.VarName});

<#
	end for
#>
			${Entity.Name}Manager.AddNew(entity<#
	for each Property in Entity.Properties where Property.Reference
		Print ", " & Property.Reference.VarName
	end for
#>);
		}

		// New Entity in the System, using a Message

		public static void New${Entity.Name}(${Entity.Name}Info entityinfo) {
			${Entity.Name} entity;

			entity = ${Entity.Name}.Create${Entity.Name}( 
<#
	p = 0
	for each Property in Entity.Properties where Property.Type<>"Id"
		if p then
			Print ", "
		end if
		p = p + 1
		if Property.Reference then
#>
			${Property.Reference.Name}Repository.GetById(entityinfo.${Property.Name}) 
<#
		else
#>
			entityinfo.${Property.Name} 
<#
		end if
	end for
#>
			);

			${Entity.Name}Manager.Add(entity);
		}

		// Update Entity

		public static void Update${Entity.Name}(${Entity.Name} entity) {
			${Entity.Name}Manager.Update(entity);
		}

		// Update Entity using a Message

		public static void Update${Entity.Name}(${Entity.Name}Info entityinfo) {
			${Entity.Name} entity;

			entity = ${Entity.Name}Repository.GetById(entityinfo.${Entity.IdProperty.Name});

<#
	for each Property in Entity.Properties
		if Property.Reference then
#>
			entity.${Property.Reference.Name} = ${Property.Reference.Name}Repository.GetById(entityinfo.${Property.Name});
<#
		else
#>
			entity.${Property.Name} = entityinfo.${Property.Name};
<#
		end if
	end for
#>

			${Entity.Name}Manager.Update(entity);
		}

		public static void Delete${Entity.Name}(${IdentityType} id) {
			${Entity.Name} entity;

			entity = ${Entity.Name}Repository.GetById(id);

			${Entity.Name}Manager.Delete(entity);
		}

		public static Domain.${Entity.Name} Get${Entity.Name}ById(int id) {
			return ${Entity.Name}Repository.GetById(id);
		}

		public static IList Get${Entity.SetName}() {
			return ${Entity.Name}Repository.GetAll();
		}
	}
}

