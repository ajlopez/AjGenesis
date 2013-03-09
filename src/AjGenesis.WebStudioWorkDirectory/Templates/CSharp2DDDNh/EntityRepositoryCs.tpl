<#

message	"Generating Services for Entity ${Entity.Name}..."

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
using System.Collections;

using AjNHibernate;

namespace ${Project.SystemName}.Domain {

	public class ${Entity.Name}Repository {
		public static void Add(${Entity.Name} entity) {
			Repository.Current.SaveObject(entity);
		}

		public static void Update(${Entity.Name} entity) {
			Repository.Current.UpdateObject(entity);
		}

		public static void Remove(${Entity.Name} entity) {
			Repository.Current.DeleteObject(entity);
		}

		public static ${Entity.Name} GetById(int id) {
			if (id==0)
				return null;
		
			return (${Entity.Name}) Repository.Current.GetObjectById(typeof(${Entity.Name}),id);
		}

		public static IList GetAll() {
			return Repository.Current.GetAll(typeof(${Entity.Name}));
		}
	}
}

