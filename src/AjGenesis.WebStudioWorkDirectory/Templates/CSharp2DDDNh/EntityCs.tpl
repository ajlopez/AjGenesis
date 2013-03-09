<#

rem		Entity Generator
rem		for C Sharp


include	"Templates/CSharp2DDDNh/CSharpFunctions.tpl"
include "Templates/EntityFunctions.tpl"

Properties = GetAllProperties(Entity)

message	"Processing Entity ${Entity.Name}"
#>

/*
 *	Project ${Project.Name}
 *		${Project.Description}
 *	Entity	${Entity.Name}
 *		${Entity.Description}
 *	
 */

using System;
using System.Collections;

namespace ${Project.SystemName}.Domain {

	public class ${Entity.Name} <#
	if Entity.Inherits then
		Print ": " & Entity.Inherits.Name
	end if
#> {

//	Private Fields

<#
for each Property in Entity.Properties where not Property.Reference
	message	"Processing Field ${Property.Name}" 
		if Property.Type="Date" then
#>	
		private ${CSharpType(Property)} ${CSharpFieldName(Property)} = Date.Today;
<#			
		else
#>	
		private ${CSharpType(Property)} ${CSharpFieldName(Property)};
<#
		end if
end for
#>

<#
for each Property in Entity.Properties where Property.Reference
	message	"Processing Field ${Property.Reference.Name}"
#>	
		private ${Property.Reference.Name} ${Property.Reference.VarName};  <#	
end for
#>

<#
for each Relation in Entity.Relations where Relation.RelationType="Referenced"
	message	"Processing Relation ${Relation.Entity.SetName}"
#>	
		private IList ${Relation.Entity.VarSetName}; <#	
end for
#>

//	Default Constructor

		public ${Entity.Name}() {
		}

//	Factory Methods

		public static ${Entity.Name} Create${Entity.Name}(
<#
	for each Property in Properties where Property.Type<>"Id"
		if Property.Reference then
#>
			${Property.Reference.Name} ${Property.Reference.Name} ${OnLast("",",")}
<#
		else
#>
			${CSharpType(Property)} ${Property.VarName} ${OnLast("",",")}
<#
		end if
	end for
#>
			) {
			${Entity.Name} ${Entity.VarName};

			${Entity.VarName} = new ${Entity.Name}();

<#
	for each Property in Properties where Property.Type<>"Id"
		if Property.Reference then
#>
			${Entity.VarName}.${Property.Reference.Name} = ${Property.Reference.Name};
<#
		else
#>
			${Entity.VarName}.${Property.Name} = ${Property.VarName};
<#
		end if
	end for
#>

			return ${Entity.VarName};
		}

//	Public Properties

<#
for each Property in Entity.Properties where not Property.Reference
	message	"Processing Property ${Property.Name}"
#>	
		public virtual ${CSharpType(Property)} ${Property.Name}
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

<#
for each Property in Entity.Properties where Property.Reference
	message	"Processing Property ${Property.Name}"
#>	
		public virtual ${Property.Reference.Name} ${Property.Reference.Name}
		{
			get {
				return ${Property.Reference.VarName};
			}
			set {
				${Property.Reference.VarName} = value;
			}
		}

<#
end for
#>

<#
for each Relation in Entity.Relations where Relation.RelationType="Referenced"
	message	"Processing Relation ${Relation.Entity.SetName}"
#>	
		public virtual IList ${Relation.Entity.SetName} {
			get {
				return ${Relation.Entity.VarSetName};
			}
			set {
				${Relation.Entity.VarSetName} = value;
			}
		}

<#
end for
#>
	}

}