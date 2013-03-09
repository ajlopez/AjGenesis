<#

function HbType(prop)
	if prop.Type="Id" then
		return "int"
	end if
	if prop.Type="IdRef" then
		return "int"
	end if
	if prop.Type="Text" then
		return "string"
	end if
	if prop.Type="Money" then
		return "decimal"
	end if
	if prop.Type="Date" then
		return "date"
	end if
	if prop.Type="Entity" then
		return prop.Entity
	end if
	if prop.Type.ToLower()="integer" then
		return "int"
	end if
	if prop.Type.ToLower()="int" then
		return "int"
	end if
	return prop.Type
end function

#>