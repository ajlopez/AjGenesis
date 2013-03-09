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
 *	Note: This implementation is too much oriented to CRUD operations
 *	
 */

using System;

namespace ${Project.SystemName}.Application {
	public class ${Entity.Name}Info {

		//	Private Fields

<#
for each Property in Entity.Properties
	message	"Processing Field ${Property.Name}"
#>	
		private ${CSharpType(Property)} ${Property.VarName}; <#	
end for
#>

		//	Factory Methods

		public static ${Entity.Name}Info Create(
<#
	for each Property in Entity.Properties where Property.Type<>"Id"
#>
			${CSharpType(Property)} ${Property.VarName} ${OnLast("",",")}
<#
	end for
#>
		) {

			${Entity.Name}Info ${Entity.VarName};

			${Entity.VarName} = new ${Entity.Name}Info();

<#
	for each Property in Entity.Properties where Property.Type<>"Id"
#>
			${Entity.VarName}.${Property.Name} = ${Property.VarName};
<#
	end for
#>

			return ${Entity.VarName};
		}

		//	Public Properties

<#
for each Property in Entity.Properties
	message	"Processing Property ${Property.Name}"
#>	
		public ${CSharpType(Property)} ${Property.Name} {
			get {
				return ${Property.VarName};
			}
			set {
				${Property.VarName} = value;
			}
		}
<#
end for
#>
	}
}

