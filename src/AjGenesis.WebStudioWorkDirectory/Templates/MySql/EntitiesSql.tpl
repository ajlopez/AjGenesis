<#

rem		Generador de Scripts SQL
rem		MySql

include "Templates/EntityFunctions.tpl"

#>

--
--		Project:		${Project.Name}
--		Description:	${Project.Description}
--


<#
for each Entity in Project.Model.Entities
#>

--
--		Entity:		${Entity.Name}
--		Description:	${Entity.Description}
--

<#
message	"Generando Scripts de Entidad ${Entity.Name}..."

EntitySqlProperties	= SqlProperties(Entity)
EntityNoIdSqlProperties	= SqlNoIdProperties(Entity)
EntityIdRefSqlProperties = SqlIdRefProperties(Entity)
EntityIdProperty = IdProperty(Entity)


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

<#
end for
#>