

// Entity ${Entity.Name}
//		${Entity.Description}

class ${Entity.Name} {
<#
	for each Property in Entity.Properties
#>
	var ${Property.Name};
<#
	end for
#>
}

