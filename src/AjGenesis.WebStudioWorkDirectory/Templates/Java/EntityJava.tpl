<#

rem		Entity Generator
rem		for Java


include	"Templates/Java/JavaFunctions.tpl"
message	"Processing Entity ${Entity.Name}"
#>

/*
 *	Project ${Project.Name}
 *		${Project.Description}
 *	Entity	${Entity.Name}
 *		${Entity.Description}
 *	
 */

package ${Project.PackageName}.model;

public class ${Entity.Name} {

//	Private Fields

<#
for each Property in Entity.Properties
	message	"Processing Field ${Property.Name}" #>
	private ${Property.JavaType} ${Property.JavaName};
<# end for #>

//	Default Constructor

	public ${Entity.Name}() {
	}

//	Public Properties

<#
for each Property in Entity.Properties
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
#>

}

