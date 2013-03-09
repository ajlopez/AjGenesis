<#

'	Entity Generator
'	for Java

message	"Processing Entity ${Entity.Name}"

include "Templates/JavaStruts/JavaFunctions.tpl"
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

public interface ${Entity.Name} extends EJBObject {
<#
for each Property in Entity.Properties
#>
	public abstract ${JavaEjbType(Property)} get${Property.Name}() throws java.rmi.RemoteException;
	public abstract void set${Property.Name}(${JavaEjbType(Property)} ${Property.JavaName}) throws java.rmi.RemoteException;
<# end for #>
}

