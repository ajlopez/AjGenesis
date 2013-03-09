<#


rem		Generador de Scripts SQL
rem		MySql

message	"Generando Scripts SQL de Entidad ${Entity.Name}..."

include "Templates\EntityFunctions.tpl"

EntitySqlProperties	= SqlProperties(Entity)
EntityNoIdSqlProperties	= SqlNoIdProperties(Entity)
EntityIdRefSqlProperties = SqlIdRefProperties(Entity)
EntityIdProperty = IdProperty(Entity)

#>

--
--		Project:		${Project.Name}
--		Description:	${Project.Description}
--

--
--		Entity:		${Entity.Name}
--		Description:	${Entity.Description}
--

<#
message	"Generando Create Table..."
#>

drop table if exists ${Technology.Database.Prefix}${Entity.SqlTable};


create table ${Technology.Database.Prefix}${Entity.SqlTable} (
		${EntityIdProperty.SqlColumn} int NOT NULL auto_increment,
<# 
for each Property in EntityNoIdSqlProperties
#>	
		${Property.SqlColumn} ${Property.SqlType},
<#
end for
#>
		primary key (${EntityIdProperty.SqlColumn})
);

