<#

'	Value Object Generator
'	for Java

message	"Processing Entity ${Entity.Name}"
#>

/*
 *	Project ${Project.Name}
 *		${Project.Description}
 *	Value Object	${Entity.Name}VO
 *		${Entity.Description}
 *	
 */

package ${Project.PackageName}.vo;

public class ${Entity.Name}VO {

//	Private Fields

<#
for each Property in Entity.Properties
#>
	private ${Property.JavaType} ${Property.JavaName};
<# end for #>

//	Default Constructor

	public ${Entity.Name}VO() {
	}

//	Public Properties

<#
for each Property in Entity.Properties
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

