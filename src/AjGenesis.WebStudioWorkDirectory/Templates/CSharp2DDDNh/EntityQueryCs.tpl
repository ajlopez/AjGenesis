<#

message "Generating Entity ${Entity.Name}"

include	"Templates/CSharp2DDDNh/CSharpFunctions.tpl"

include	"Templates/CSharp2DDDNh/Prologue.tpl"
#>

/*
 *	Project ${Project.Name}
 *		${Project.Description}
 *	Entity	${Entity.Name}
 *		${Entity.Description}
 *	
 */

using System;

namespace ${Project.SystemName}.Entities {

	public class ${Entity.Name}Query {

<#
for each Property in Entity.Properties where not Property.Reference
#>	
		public ${CSharpType(Property)} ${Property.Name};
<#
end for
#>

	}
}

