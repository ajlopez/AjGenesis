<#

rem		Entity Manager Generartor
rem		for Java


include	"Templates/JavaHb/JavaFunctions.tpl"
message	"Processing Services for ${Entity.Name}"
#>

/*
 *	Project ${Project.Name}
 *		${Project.Description}
 *	Services	${Entity.Name}Services
 *		${Entity.Description}
 *	
 */

package ${Project.PackageName}.application.services;

import java.util.List;

import org.hibernate.*;

import ${Project.PackageName}.*;
import ${Project.PackageName}.domain.entities.*;
import ${Project.PackageName}.domain.repositories.*;

public class ${Entity.Name}Manager {
	public static ${Entity.Name} getById(int id) throws Exception {
		return ${Entity.Name}Repository.getById(id);
	}

	public static List getAll() throws Exception {
		return ${Entity.Name}Repository.getAll();
	}
	
	public static void insert(${Entity.Name} entity) throws Exception {
		${Entity.Name}Repository.insert(entity);
	}
	
	public static void update(${Entity.Name} entity) throws Exception {
		${Entity.Name}Repository.update(entity);
	}
	
	public static void delete(${Entity.Name} entity) throws Exception {
		${Entity.Name}Repository.delete(entity);
	}
}

