<#

function OnLast(p1,p2)
	if _index +1 = _count then
		return p1
	else
		return p2
	end if
end function

function VbType(prop)
	if prop.Type="Id" then
		return "Integer"
	end if
	if prop.Type="IdRef" then
		return "Integer"
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
	return prop.Type
end function

function VbNullValue(type)
	if type="Id" then
		return 0
	end if
	if type="IdRef" then
		return 0
	end if
	if type="Integer" then
		return 0
	end if
	if type="Decimal" then
		return 0
	end if
	if type="Date" then
		return "DateTime.MinValue"
	end if
	if type="DateTime" then
		return "DateTime.MinValue"
	end if
	return "Nothing"
end function

#>