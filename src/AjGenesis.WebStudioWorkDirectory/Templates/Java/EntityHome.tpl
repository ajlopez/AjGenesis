<#

'	Entity Generator
'	for Java

message	"Processing Entity ${Entity.Name}"

include "Templates/Java/JavaFunctions.tpl"
#>

/*
 *	Project	${Project.Name}
 *		${Project.Description}
 *	Entity	${Entity.Name}
 *		${Entity.Description}
 *	
 */

package ${Project.PackageName}.ejb;

import javax.ejb.*;
import java.util.Collection;

public interface ${Entity.Name}Home extends EJBHome {
    public ${Entity.Name} create(<#
nprop = 0
for each Property in Entity.Properties
	if nprop then
		print ", "
	end if
	print JavaEjbType(Property) & " " & Property.JavaName
	nprop = nprop + 1
end for
#>) throws javax.ejb.CreateException, java.rmi.RemoteException;
    public Collection findAll() throws javax.ejb.FinderException, java.rmi.RemoteException;
    public Collection findByPrimaryKey(<# print JavaEjbType(Entity.IdProperty) & " " & Entity.IdProperty.JavaName #>) throws javax.ejb.FinderException, java.rmi.RemoteException;
}

