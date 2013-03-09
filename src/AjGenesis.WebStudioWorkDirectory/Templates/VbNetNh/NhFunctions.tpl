<#

function NhType(prop)
	if prop.Type="Id" then
		return "Int32"
	end if
	if prop.Type="IdRef" then
		return "Int32"
	end if
	if prop.Type="Text" then
		return "String"
	end if
	if prop.Type="Money" then
		return "Decimal"
	end if
	if prop.Type="Entity" then
		return prop.Entity
	end if
	if prop.Type.ToLower()="integer" then
		return "Int32"
	end if
	if prop.Type.ToLower()="int" then
		return "Int32"
	end if
	return prop.Type
end function

#>