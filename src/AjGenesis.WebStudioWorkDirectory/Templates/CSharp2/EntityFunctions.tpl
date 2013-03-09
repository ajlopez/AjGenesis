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

function IdProperty(Entity)
	for each Property in Entity.Properties
		if	Property.Type = "Id" then
			return	Property
		end if
	end for

end function
#>