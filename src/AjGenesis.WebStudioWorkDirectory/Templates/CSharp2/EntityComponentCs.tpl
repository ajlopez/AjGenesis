<#

message	"Generating Business Component for Entity ${Entity.Name}..."

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

using ${Project.Name}.Entities;
using ${Project.Name}.Data;

namespace ${Project.Name}.Business {
	public class ${Entity.Name}Component : ${Entity.Name}ComponentBase {

		public override void Validate(${Entity.Name} entity) {
			base.Validate(entity);
		}

		public override void ValidateNew(${Entity.Name} entity) {
			base.ValidateNew(entity);
		}

		public override void ValidateDelete(${Entity.Name} entity) {
			base.ValidateDelete(entity);
		}
	}
}

