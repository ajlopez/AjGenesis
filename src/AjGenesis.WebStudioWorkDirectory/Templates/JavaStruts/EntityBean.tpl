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

public abstract class ${Entity.Name}Bean implements EntityBean {
    private EntityContext entityContext = null;

<#
for each Property in Entity.Properties
#>
	public abstract ${JavaEjbType(Property)} get${Property.Name}();
	public abstract void set${Property.Name}(${JavaEjbType(Property)} ${Property.JavaName});
<# end for #>

    public Integer ejbCreate(<#
nprop = 0
for each Property in Entity.Properties
	if nprop then
		print ", "
	end if
	print JavaEjbType(Property) & " " & Property.JavaName
	nprop = nprop + 1
end for
#>) throws CreateException {
<#
	for each Property in Entity.Properties
#>
    	set${Property.Name}(${Property.JavaName});
<#
	end for
#>
    	return null;
    }

    public void ejbPostCreate(<#
nprop = 0
for each Property in Entity.Properties
	if nprop then
		print ", "
	end if
	print JavaEjbType(Property) & " " & Property.JavaName
	nprop = nprop + 1
end for
#>) throws CreateException {
    }	

    public void setEntityContext(EntityContext entityContext) throws EJBException {
        this.entityContext = entityContext;
    }

    public void unsetEntityContext() throws EJBException {
        entityContext = null;
    }
    
    public void ejbActivate() throws EJBException { }

    public void ejbPassivate() throws EJBException { }

    public void ejbLoad() throws EJBException { }

    public void ejbStore() throws EJBException { }

    public void ejbRemove() throws RemoveException, EJBException { }

}

