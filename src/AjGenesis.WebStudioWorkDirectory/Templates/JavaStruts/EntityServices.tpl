<#

rem		Entity Services Generartor
rem		for Java


include	"Templates/JavaStruts/JavaFunctions.tpl"
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

import ${Project.PackageName}.model.*;
import ${Project.PackageName}.data.*;

public class ${Entity.Name}Services {
    public static ${Entity.Name} getBy${Entity.IdProperty.Name}(${Entity.IdProperty.JavaType} ${Entity.IdProperty.JavaName}) throws Exception {
        Base base = new Base();
        ${Entity.Name}DAO dao = new ${Entity.Name}DAO(base);
        ${Entity.Name} entity = dao.getBy${Entity.IdProperty.Name}(${Entity.IdProperty.JavaName});
        base.dispose();
        
        return entity;
    }
    
    public static List getAll() throws Exception {
        Base base = new Base();
        ${Entity.Name}DAO dao = new ${Entity.Name}DAO(base);
        List entities = dao.getAll();
        base.dispose();
        
        return entities;
    }

    public static void insert(${Entity.Name} entity) throws Exception {
        Base base = new Base();
        ${Entity.Name}DAO dao = new ${Entity.Name}DAO(base);
        dao.insert(entity);
        base.dispose();
    }

    public static void update(${Entity.Name} entity) throws Exception {
        Base base = new Base();
        ${Entity.Name}DAO dao = new ${Entity.Name}DAO(base);
        dao.update(entity);
        base.dispose();
    }

    public static void delete(${Entity.Name} entity) throws Exception {
        Base base = new Base();
        ${Entity.Name}DAO dao = new ${Entity.Name}DAO(base);
        dao.delete(entity);
        base.dispose();
    }
<#
	for each Property in Entity.Properties where Property.Reference
#>

	public static List getBy${Property.Reference.Name}(${Property.JavaType} ${Property.JavaName}) throws Exception {
        Base base = new Base();
        ${Entity.Name}DAO dao = new ${Entity.Name}DAO(base);
        List entities = dao.getBy${Property.Reference.Name}(${Property.JavaName});
        base.dispose();
        
        return entities;
	}
<#
	end for
#>
}

