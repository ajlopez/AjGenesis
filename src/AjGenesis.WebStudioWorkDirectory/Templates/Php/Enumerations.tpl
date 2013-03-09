<?

<#
	for each Enumeration in Project.Model.Enumerations
#>
$Enum${Enumeration.Name} = array(
<#
		nitem = 0
		for each Item in Enumeration.Items
			if nitem then
				PrintLine ","
			end if
			Print "      "
			if Enumeration.Type="Number" then
				Print Item.Key
			else
				Print "'" + Item.Key + "'"
			end if
			Print " => '" + Item.Value + "'"
			nitem = nitem + 1
		end for

		PrintLine ");"
		PrintLine ""
	end for
#>
?>