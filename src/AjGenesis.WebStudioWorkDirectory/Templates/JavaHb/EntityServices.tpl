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

package ${Project.PackageName}.services;

import java.util.List;

import org.hibernate.*;

import ${Project.PackageName}.*;
import ${Project.PackageName}.model.*;
import ${Project.PackageName}.data.*;

public class ${Entity.Name}Services {
	public static ${Entity.Name} getById(int id) throws Exception {
		AjHibernate.beginTransaction();		
		Session session = AjHibernate.getSession();

		${Entity.Name}DAO dao;
		${Entity.Name} entity = null;

		try {
			dao = new ${Entity.Name}DAO(session);
			entity = dao.getById(id);
		} catch (Exception ex) {
			AjHibernate.rollbackTransaction();
			throw ex;
		}

		return entity;
	}

	public static List getAll() throws Exception {
		AjHibernate.beginTransaction();		
		Session session = AjHibernate.getSession();

		${Entity.Name}DAO dao;
		List entities = null;

		try {
			dao = new ${Entity.Name}DAO(session);
			entities = dao.getAll();
		} catch (Exception ex) {
			AjHibernate.rollbackTransaction();
			throw ex;
		}

		return entities;
	}
	
	public static void insert(${Entity.Name} entity) throws Exception {
		AjHibernate.beginTransaction();		
		Session session = AjHibernate.getSession();

		${Entity.Name}DAO dao;

		try {
			dao = new ${Entity.Name}DAO(session);
			dao.insert(entity);
		} catch (Exception ex) {
			AjHibernate.rollbackTransaction();
			throw ex;
		}
	}
	
	public static void update(${Entity.Name} entity) throws Exception {
		AjHibernate.beginTransaction();		
		Session session = AjHibernate.getSession();

		${Entity.Name}DAO dao;

		try {
			dao = new ${Entity.Name}DAO(session);
			dao.update(entity);
		} catch (Exception ex) {
			AjHibernate.rollbackTransaction();
			throw ex;
		}
	}
	
	public static void delete(int id) throws Exception {
		delete(getById(id));		
	}
	
	public static void delete(${Entity.Name} entity) throws Exception {
		AjHibernate.beginTransaction();		
		Session session = AjHibernate.getSession();

		${Entity.Name}DAO dao;

		try {
			dao = new ${Entity.Name}DAO(session);
			dao.delete(entity);
		} catch (Exception ex) {
			AjHibernate.rollbackTransaction();
			throw ex;
		}
	}
}

