<#

message "Generating Entity ${Entity.Name}"

include	"Templates/VbNet2DDDNh/VbFunctions.tpl"

include	"Templates/VbNet2DDDNh/Prologue.tpl"
#>

'
'	Project ${Project.Name}
'		${Project.Description}
'	Entity	${Entity.Name}
'		${Entity.Description}
'	
'

Public Class ${Entity.Name}Query

'	Criteria

<#
for each Property in Entity.Properties where not Property.Reference
#>	
	Public ${Property.Name} as ${VbType(Property)}
<#
end for
#>

End Class

