<#

rem	Funciones Java

rem Imprime algo si es el ultimo item de un for each
rem Usa las variables especiales _index y _count

function OnLast(p1,p2)
	if _index +1 = _count then
		return p1
	else
		return p2
	end if
end function

rem Pasa de Tipo de Propiedad a Tipo Java

function JavaType(prop)
	if prop.Type="Id" then
		return "int"
	end if
	if prop.Type="IdRef" then
		return "int"
	end if
	if prop.Type="Text" then
		return "String"
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

function JavaTypeCap(prop)
	if prop.Type="Id" then
		return "Int"
	end if
	if prop.Type="IdRef" then
		return "Int"
	end if
	if prop.Type="Text" then
		return "String"
	end if
	if prop.Type="Entity" then
		return prop.Entity
	end if
	if prop.Type.ToLower()="String" then
		return "String"
	end if
	if prop.Type.ToLower()="integer" then
		return "Int"
	end if
	return prop.Type
end function

function JavaNullValue(type)
	if type="Id" then
		return 0
	end if
	if type="IdRef" then
		return 0
	end if
	if type="Integer" then
		return 0
	end if
	return "null"
end function

function JavaFieldName(prop)
	firstletter = prop.Name.Substring(0,1)
	
	return firstletter.ToLower() & prop.Name.Substring(1)
end function

function JavaNullValue(type)
	if type="Id" then
		return 0
	end if
	if type="IdRef" then
		return 0
	end if
	if type="Integer" then
		return 0
	end if
	return "null"
end function

function JavaEjbType(prop)
	if prop.Type="Id" then
		return "Integer"
	end if
	if prop.Type="IdRef" then
		return "Integer"
	end if
	if prop.Type="Text" then
		return "String"
	end if
	if prop.Type="Date" then
		return "Date"
	end if
	if prop.Type="Time" then
		return "Time"
	end if
	if prop.Type="Entity" then
		return prop.Entity
	end if
	return prop.Type
end function

function JavaName(prop)
	initial = prop.Name.Substring(0,1)
	initial = initial.ToLower()
	rest = prop.Name.Substring(1,prop.Name.Length()-1)

	return initial & rest
end function

function JavaSetName(ent)
	initial = ent.SetName.Substring(0,1)
	initial = initial.ToLower()
	rest = ent.SetName.Substring(1,ent.SetName.Length()-1)

	return initial & rest
end function

function JavaSqlType(prop)
	if prop.Type="Id" then
		return "Int"
	end if
	if prop.Type="IdRef" then
		return "Int"
	end if
	if prop.Type="Text" then
		return "String"
	end if
	if prop.Type="Date" then
		return "Date"
	end if
	if prop.Type="Time" then
		return "Time"
	end if
	return prop.Type
end function

#>