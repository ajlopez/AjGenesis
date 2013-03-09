<#

rem		Entity Services Generartor
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
import ${Project.PackageName}.domain.managers.*;
import ${Project.PackageName}.domain.entities.*;
import ${Project.PackageName}.infrastructure.data.*;

public class ${Entity.Name}Services {
	public static ${Entity.Name} getById(int id) throws Exception {
		AjHibernate.beginTransaction();		

		${Entity.Name} entity = null;

		try {
			entity = ${Entity.Name}Manager.getById(id);
		} catch (Exception ex) {
			AjHibernate.rollbackTransaction();
			throw ex;
		}

		return entity;
	}

	public static List getAll() throws Exception {
		AjHibernate.beginTransaction();		

		List entities = null;

		try {
			entities = ${Entity.Name}Manager.getAll();
		} catch (Exception ex) {
			AjHibernate.rollbackTransaction();
			throw ex;
		}

		return entities;
	}
	
	public static void insert(${Entity.Name} entity) throws Exception {
		AjHibernate.beginTransaction();		

		try {
			${Entity.Name}Manager.insert(entity);
		} catch (Exception ex) {
			AjHibernate.rollbackTransaction();
			throw ex;
		}
	}
	
	public static void update(${Entity.Name} entity) throws Exception {
		AjHibernate.beginTransaction();		

		try {
			${Entity.Name}Manager.update(entity);
		} catch (Exception ex) {
			AjHibernate.rollbackTransaction();
			throw ex;
		}
	}
	
	public static void delete(${Entity.Name} entity) throws Exception {
		AjHibernate.beginTransaction();		

		try {
			${Entity.Name}Manager.delete(entity);
		} catch (Exception ex) {
			AjHibernate.rollbackTransaction();
			throw ex;
		}
	}
}

