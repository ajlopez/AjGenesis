<#

message "Generating NHibernate Mapping for Entity ${Entity.Name}"

include	"Templates/VbNetNh/NhFunctions.tpl"

#>
<?xml version="1.0" encoding="utf-8" ?> 
<hibernate-mapping xmlns="urn:nhibernate-mapping-2.0">
	<class name="${Project.Name}.Entities.${Entity.Name}, ${Project.Name}.Entities" table="${Technology.Database.Prefix}${Entity.SqlTable}">
<#
	for each Property in Entity.Properties where Property.SqlType
		if Property.Type="Id" then
#>
		<id name="${Property.Type}" column="${Property.SqlColumn}" type="Int32" unsaved-value="0">
			<generator class="native"/>
		</id>
<#
		else
#>
		<property column="${Property.SqlColumn}" type="${NhType(Property)}" name="${Property.Name}" length="255"/>
<#
		end if
	end for
#>
	</class>
</hibernate-mapping>
