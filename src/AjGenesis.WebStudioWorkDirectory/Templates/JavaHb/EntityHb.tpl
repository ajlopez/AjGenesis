<#

message "Generating Hibernate Mapping for Entity ${Entity.Name}"

include	"Templates/JavaHb/HbFunctions.tpl"

#>
<?xml version="1.0" encoding="utf-8" ?> 
<!DOCTYPE hibernate-mapping PUBLIC
    "-//Hibernate/Hibernate Mapping DTD 3.0//EN"
    "http://hibernate.sourceforge.net/hibernate-mapping-3.0.dtd">

<hibernate-mapping>

	<class name="${Project.PackageName}.model.${Entity.Name}" table="${Technology.Database.Prefix}${Entity.SqlTable}">
<#
	for each Property in Entity.Properties where Property.SqlType
		if Property.Type="Id" then
#>
		<id name="${Property.Type}" column="${Property.SqlColumn}" type="int" unsaved-value="0">
			<generator class="native"/>
		</id>
<#
		else
			if Property.Reference then
#>
		<many-to-one name="${Property.Reference.Name}" column="${Property.Name}" class="${Project.PackageName}.model.${Property.Reference.Name}" />
<#
			else
#>
		<property column="${Property.SqlColumn}" type="${HbType(Property)}" name="${Property.Name}"/>
<#
			end if
		end if
	end for

	for each Relation in Entity.Relations where Relation.RelationType="Referenced"
#>
		<bag name="${Relation.Entity.SetName}" lazy="true" inverse="true" cascade="all">
			<key column="Id${Entity.Name}"/>
			<one-to-many class="${Project.PackageName}.model.${Relation.Entity.Name}"/>
		</bag>	
<#
	end for
#>
	</class>
</hibernate-mapping>
