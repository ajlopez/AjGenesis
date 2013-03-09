<#

message	"Generating Services for Entity ${Entity.Name}..."

include "Templates/CSharp/CSharpFunctions.tpl"
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
 */

using System;
using System.Data;
using System.Collections;

using ${Project.Name}.Entities;
using ${Project.Name}.Business;

using AjFramework.Data;

namespace ${Project.Name}.Services {
	public class ${Entity.Name}Service {
		private static ${Entity.Name}Component component = new ${Entity.Name}Component();

		public static void Insert(${Entity.Name} entity) {
			component.Insert(entity);
		}

		public static void Update(${Entity.Name} entity) {
			component.Update(entity);
		}

		public static void Delete(int id) {
			component.Delete(id);
		}

		public static ${Entity.Name} GetById(int id) {
			return component.GetById(id);
		}

		public static IList GetAll() {
			return component.GetAll();
		}

		public static DataSet GetList() {
			return component.GetAllAsDs();
		}
<#
	if Entity.HasReferences then
#>

		public static DataSet GetAllEx() {
			return component.GetAllEx();
		}
<#
	end if

	for each Property in Entity.Properties where Property.Reference
#>

		public static void IList GetBy${Property.Reference.Name}(${CSharpType(Property)} ${Property.Name}) {
			return component.GetBy${Property.Reference.Name}(${Property.Name});
		}

		public static DataSet GetBy${Property.Reference.Name}Ex(${CSharpType(Property)} ${Property.Name}) {
			return component.GetBy${Property.Reference.Name}Ex(${Property.Name});
		}
<#
	end for
#>
	}
}


