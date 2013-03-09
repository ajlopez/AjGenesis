<#

rem	CSharp Functions

rem Test if it is the last item on for each
rem It uses the special variables _index and _count

function OnLast(p1,p2)
	if _index +1 = _count then
		return p1
	else
		return p2
	end if
end function

rem Convert from Property Type to C Sharp Type

function CSharpType(prop)
	if prop.Type="Id" then
		return "int"
	end if
	if prop.Type="IdRef" then
		return "int"
	end if
	if prop.Type="Text" then
		return "string"
	end if
	if prop.Type="Date" then
		return "DateTime"
	end if
	if prop.Type="Entity" then
		return prop.Entity
	end if
	if prop.Type.ToLower()="String" then
		return "string"
	end if
	if prop.Type.ToLower()="integer" then
		return "int"
	end if
	return prop.Type
end function

function CSharpFieldName(prop)
	firstletter = prop.Name.Substring(0,1)
	
	return firstletter.ToLower() & prop.Name.Substring(1)
end function

function CSharpNullValue(type)
	if type="Id" then
		return 0
	end if
	if type="IdRef" then
		return 0
	end if
	if type="Integer" then
		return 0
	end if
	if type="Date" then
		return "DateTime.MinValue"
	end if
	return "null"
end function
#>