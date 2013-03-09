<#

'	Data Access Object for ${Entity.Name} Generator
'	for Java

message	"Processing Entity ${Entity.Name}"

ObjName = Entity.JavaObjectName

#>

/*
 *	Project	${Project.Name}
 *		${Project.Description}
 *	${Entity.Name}	${Entity.Name}
 *		${Entity.Description}
 *	
 */

package ${Project.PackageName}.infrastructure.data;

import java.util.*;
import org.hibernate.*;

import ${Project.PackageName}.*;
import ${Project.PackageName}.domain.entities.*;

public class ${Entity.Name}DAO {
	public ${Entity.Name} getById(int id) throws Exception {
		return (${Entity.Name}) AjHibernate.getSession().get(${Entity.Name}.class,new Integer(id));
	}

	public List getAll() throws Exception {
		Query query = (AjHibernate.getSession()).createQuery("from ${Project.PackageName}.domain.entities.${Entity.Name}");
		return query.list();
	}

	public void insert(${Entity.Name} entity) throws Exception {
		AjHibernate.getSession().save(entity);
	}

	public void update(${Entity.Name} entity) throws Exception {
		AjHibernate.getSession().update(entity);
	}

	public void delete(${Entity.Name} entity) throws Exception {
		AjHibernate.getSession().delete(entity);
	}
}

