<#

rem		Entity Generator
rem		for Java


include	"Templates/JavaHb/JavaFunctions.tpl"
message	"Processing Entity ${Entity.Name}"
#>

/*
 *	Project ${Project.Name}
 *		${Project.Description}
 *	Entity	${Entity.Name}
 *		${Entity.Description}
 *	
 */

package ${Project.PackageName}.domain.entities;

import java.util.*;

public class ${Entity.Name} {

//	Private Fields

<#
for each Property in Entity.Properties where not Property.Reference
	message	"Processing Field ${Property.Name}" 
#>
	private ${Property.JavaType} ${Property.JavaName}; 
<#
end for 

for each Property in Entity.Properties where Property.Reference
	message	"Processing Field ${Property.Name}" 
#>
	private ${Property.Reference.Name} ${Property.Reference.JavaObjectName}; 
<#
end for 

for each Relation in Entity.Relations where Relation.RelationType="Referenced"
	message	"Processing Relation ${Relation.Entity.SetName}"
#>
	private List ${Relation.Entity.JavaSetName}; 
<#	
end for
#>


//	Default Constructor

	public ${Entity.Name}() {
	}

//	Public Properties

<#
for each Property in Entity.Properties where not Property.Reference
	message	"Processing Property ${Property.Name}"
#>	
	public ${Property.JavaType} get${Property.Name}() {
		return ${Property.JavaName};
	}

	public void set${Property.Name}(${Property.JavaType} value) {
		${Property.JavaName} = value;
	}
<#
end for

for each Property in Entity.Properties where Property.Reference
	message	"Processing Property ${Property.Name}"
#>	
	public ${Property.Reference.Name} get${Property.Reference.Name}() {
		return ${Property.Reference.JavaObjectName};
	}

	public void set${Property.Reference.Name}(${Property.Reference.Name} value) {
		${Property.Reference.JavaObjectName} = value;
	}
<#
end for

for each Relation in Entity.Relations where Relation.RelationType="Referenced"
	message	"Processing Relation ${Relation.Entity.SetName}"
#>	
	public List get${Relation.Entity.SetName}() {
		return ${Relation.Entity.JavaSetName};
	}

	public void set${Relation.Entity.SetName}(List value) {
		${Relation.Entity.JavaSetName} = value;
	}
<#
end for

#>

}

