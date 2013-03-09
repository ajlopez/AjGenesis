<#

message	"Generating Services for Entity ${Entity.Name}..."

include "Templates/JavaDDDHb/JavaFunctions.tpl"
include "Templates/EntityFunctions.tpl"

EntitySqlProperties	= SqlProperties(Entity)
EntityNoIdSqlProperties	= SqlNoIdProperties(Entity)
EntityIdProperty = IdProperty(Entity)

#>

/*
 *	Project ${Project.Name}
 *		${Project.Description}
 *	Repository for Entity	${Entity.Name}
 *		${Entity.Description}
 *	
 */

package ${Project.PackageName}.domain.repositories;

import java.util.*;

import ${Project.PackageName}.domain.entities.*;
import ${Project.PackageName}.infrastructure.data.*;

public class ${Entity.Name}Repository {
	private static ${Entity.Name}DAO dao = new ${Entity.Name}DAO();

	public static void insert(${Entity.Name} entity) throws Exception {
		dao.insert(entity);
	}

	public static void update(${Entity.Name} entity) throws Exception {
		dao.update(entity);
	}

	public static void delete(${Entity.Name} entity) throws Exception {
		dao.delete(entity);
	}

	public static ${Entity.Name} getById(int id) throws Exception {
		if (id==0)
			return null;
		return dao.getById(id);
	}

	public static List getAll() throws Exception {
		return dao.getAll();
	}
}

