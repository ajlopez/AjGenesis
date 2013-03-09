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

package ${Project.PackageName}.data;

import java.util.*;
import org.hibernate.*;

import ${Project.PackageName}.model.*;

public class ${Entity.Name}DAO {
	private Session session;

	public ${Entity.Name}DAO(Session session) {
		this.session = session;
	}

	public ${Entity.Name} getById(int id) throws Exception {
		Query query = session.createQuery("from ${Project.PackageName}.model.${Entity.Name} where Id = :Id");
		query.setInteger("Id",id);
		return (${Entity.Name}) query.uniqueResult();
	}

	public List getAll() throws Exception {
		Query query = session.createQuery("from ${Project.PackageName}.model.${Entity.Name}");
		return query.list();
	}

	public void insert(${Entity.Name} entity) throws Exception {
		session.save(entity);
	}

	public void update(${Entity.Name} entity) throws Exception {
		session.update(entity);
	}

	public void delete(${Entity.Name} entity) throws Exception {
		session.delete(entity);
	}
}

