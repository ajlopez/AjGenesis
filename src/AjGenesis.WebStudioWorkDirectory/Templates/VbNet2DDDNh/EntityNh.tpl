<#

message "Generating NHibernate Mapping for Entity ${Entity.Name}"

include	"Templates/VbNet2DDDNh/NhFunctions.tpl"

#>
<?xml version="1.0" encoding="utf-8" ?> 
<hibernate-mapping xmlns="urn:nhibernate-mapping-2.0">
	<class name="${Project.Name}.Domain.${Entity.Name}, ${Project.Name}.Domain" table="${Technology.Database.Prefix}${Entity.SqlTable}">
<#
	for each Property in Entity.Properties where Property.SqlType
		if Property.Type="Id" then
#>
		<id name="${Property.Type}" column="${Property.SqlColumn}" type="Int32" unsaved-value="0">
			<generator class="native"/>
		</id>
<#
		else
			if Property.Reference then
#>
		<many-to-one name="${Property.Reference.Name}" column="${Property.Name}" class="${Project.Name}.Domain.${Property.Reference.Name}, ${Project.Name}.Domain" />
<#
			else
#>
		<property column="${Property.SqlColumn}" type="${NhType(Property)}" name="${Property.Name}" length="255"/>
<#
			end if
		end if
	end for

	for each Relation in Entity.Relations where Relation.RelationType="Referenced"
#>
		<bag name="${Relation.Entity.SetName}" lazy="true" inverse="true" cascade="all">
			<key column="Id${Entity.Name}"/>
			<one-to-many class="${Project.Name}.Domain.${Relation.Entity.Name}, ${Project.Name}.Domain"/>
		</bag>	
<#
	end for
#>
	</class>
</hibernate-mapping>
