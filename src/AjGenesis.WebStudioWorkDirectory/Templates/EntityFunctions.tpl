<#

rem	Rutinas de Entidades

function SqlProperties(Entity)
	Props =	new System.Collections.ArrayList()
	for each Property in Entity.Properties
		if	Property.SqlType then
			Props.Add(Property)
		end if
	end for
	
	return Props
end function

function SqlNoIdProperties(Entity)
	Props = new System.Collections.ArrayList()
	for each Property in Entity.Properties
		if	Property.SqlType then
			if	Property.Type <> "Id" then
				Props.Add(Property)
			end if
		end if
	end for

	return	Props
end function

function SqlIdRefProperties(Entity)
	Props =	new System.Collections.ArrayList()
	
	for each Property in Entity.Properties
		if Property.SqlType then
			if Property.Type = "IdRef" then
				Props.Add(Property)
			end if
		end if
	end for
	return	Props
end function

function GetAllProperties(Entity)
	if not Entity.Inherits then
		Props = new System.Collections.ArrayList()
	else
		Props = GetAllProperties(Entity.Inherits)
	end if

	for each Prop in Entity.Properties
		Props.Add(Prop)
	end for

	return Props
end function

function IdProperty(Entity)
	for each Property in Entity.Properties
		if	Property.Type = "Id" then
			return	Property
		end if
	end for

end function

function GetDescriptorProperty(Entity)
	for each Property in Entity.Properties
		if	Property.Type <> "Id" then
			return	Property
		end if
	end for
end function

function GetEntity(Entities,Name)
	for each Entity in Entities where Entity.Name=Name
		return Entity
	end for
	return Nothing
end function

function GetProperty(Entity,Name)
	for each Property in Entity.Properties where Property.Name=Name
		return Property
	end for
	return Nothing
end function

function GetRelation(Entity,Name)
	for each Relation in Entity.Relations where Relation.Name=Name
		return Relation
	end for
	return Nothing
end function

function IsNumeric(Property)
	type = Property.Type.ToLower()

	if type="id" or type="idref" or type = "int" or type="integer" or type="number" or type="real" or type="decimal" or type="float" or type="double" then
		return true
	end if

	return false
end function

function IsDate(Property)
	type = Property.Type.ToLower()

	if type="date" or type="datetime" then
		return true
	end if

	return false
end function

function IsText(Property)
	if IsNumber(Property) then
		return false
	end if

	if IsDate(Property) then
		return false
	end if

	return true
end function

sub AddRelation(Entity,Relation)
	if not Entity.Relations then
		Entity.Relations = CreateList()
	end if

	Entity.Relations.Add(Relation)
end sub

sub AddReferenceRelation(FromEntity,ToEntity,FromProperty)
	Relation = CreateObject()
	Relation.Entity = ToEntity
	Relation.RelationType = "Reference"
	Relation.Property = FromProperty

	AddRelation(FromEntity,Relation)

	Relation = CreateObject()
	Relation.Entity = FromEntity
	Relation.RelationType = "Referenced"
	Relation.Property = FromProperty

	AddRelation(ToEntity,Relation)
end sub

#>