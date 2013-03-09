<#

rem		Entity Action Form Generator
rem		for Java


include	"Templates/JavaStruts/JavaFunctions.tpl"
message	"Processing Entity ${Entity.Name}"
#>

/*
 *	Project ${Project.Name}
 *		${Project.Description}
 *	Action Form for Entity ${Entity.Name}
 *		${Entity.Description}
 *	
 */

package ${Project.PackageName}.struts.forms;

import org.apache.struts.action.*;
import javax.servlet.http.HttpServletRequest;

public class ${Entity.Name}Form extends Action {

//	Private Fields

<#
for each Property in Entity.Properties
	message	"Processing Field ${Property.Name}" #>
	private ${Property.JavaType} ${Property.JavaName};
<# end for #>

//	Default Constructor

	public ${Entity.Name}Form() {
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

	public ActionErrors validate(ActionMapping mapping, HttpServletRequest request) {
		return null;
	}
}

