<#

rem		Entity Generator
rem		for C Sharp


include	"Templates/CSharp/CSharpFunctions.tpl"
message	"Processing Entity ${Entity.Name}"
#>

/*
 *	Project ${Project.Name}
 *		${Project.Description}
 *	Entity	${Entity.Name}
 *		${Entity.Description}
 *	
 */

namespace ${Project.SystemName}.Entities {

	public class ${Entity.Name} {

//	Private Fields

<#
for each Property in Entity.Properties
	message	"Processing Field ${Property.Name}" #>
		private ${CSharpType(Property)} ${CSharpFieldName(Property)};
<# end for #>

//	Default Constructor

		public ${Entity.Name}() {
		}

//	Public Properties

<#
for each Property in Entity.Properties
	message	"Processing Property ${Property.Name}"
#>	
		public ${CSharpType(Property)} ${Property.Name}
		{
			get {
				return ${CSharpFieldName(Property)};
			}
			set {
				${CSharpFieldName(Property)} = value;
			}
		}

<#
end for
#>

	}

}