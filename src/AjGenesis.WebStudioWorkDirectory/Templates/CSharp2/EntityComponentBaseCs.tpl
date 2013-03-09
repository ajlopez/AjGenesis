<#

message	"Generating Base Business Component for Entity ${Entity.Name}..."

include "Templates/CSharp/CSharpFunctions.tpl"
include "Templates/EntityFunctions.tpl"

EntitySqlProperties	= SqlProperties(Entity)
EntityNoIdSqlProperties	= SqlNoIdProperties(Entity)
EntityIdProperty = IdProperty(Entity)

include "Templates/CSharp2/Prologue.tpl"	

#>

/*
 *	Project ${Project.Name}
 *		${Project.Description}
 *	Entity	${Entity.Name}
 *		${Entity.Description}
 *	
 */

using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using ${Project.Name}.Entities;
using ${Project.Name}.Data;

namespace ${Project.Name}.Business {

	public class ${Entity.Name}ComponentBase {
		protected static ${Entity.Name}Data data = new ${Entity.Name}Data();

		public virtual void Validate(${Entity.Name} entity) {
<#
	for each Property in Entity.Properties where Property.Required
		if IsNumeric(Property) then
#>
			if (entity.${Property.Name} == 0)
<#
		else
#>
			if (entity.${Property.Name} == null || entity.${Property.Name}.ToString() == "")
<#
		end if
#>
				throw new Exception("${Property.Description} required");
<#
	end for
#>
		}

		public virtual void ValidateNew(${Entity.Name} entity) {
<#
	for each Property in Entity.Properties where Property.Required
		if IsNumeric(Property) then
#>
			if (entity.${Property.Name} == 0)
<#
		else
#>
			if (entity.${Property.Name} == null || entity.${Property.Name}.ToString() == "")
<#
		end if
#>
				throw new Exception("${Property.Description} required");
<#
	end for
#>
		}

		public virtual void ValidateDelete(${Entity.Name} entity) {
		}

		public void Insert(${Entity.Name} entity) {
			ValidateNew(entity);
			data.Insert(entity);
		}

		public void Update(${Entity.Name} entity) {
			Validate(entity);
			data.Update(entity);
		}

		public void Delete(int id) {
			ValidateDelete(GetById(id));
			data.Delete(id);
		}

		public ${Entity.Name} GetById(int id) {
			return data.GetById(id);
		}

		public List<${Entity.Name}> GetAll() {
			return data.GetAll();
		}
	
		public DataSet GetAllAsDs() {
			return data.GetAllAsDs();
		}
<#
	if Entity.HasReferences then
#>

		public DataSet GetAllEx() {
			return data.GetAllEx();
		}
<#
	end if

	for each Property in Entity.Properties where Property.Reference
#>

		public List<${Entity.Name}> GetBy${Property.Reference.Name}(${CSharpType(Property)} ${Property.Name}) {
			return data.GetBy${Property.Reference.Name}(${Property.Name});
		}

		public DataSet GetBy${Property.Reference.Name}Ex(${CSharpType(Property)} ${Property.Name}) {
			return data.GetBy${Property.Reference.Name}Ex(${Property.Name});
		}
<#
	end for
#>
	}
}


