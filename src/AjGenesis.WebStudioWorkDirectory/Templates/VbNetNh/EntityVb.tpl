<#

message "Generating Entity ${Entity.Name}"

include	"Templates/VbNetNh/VbFunctions.tpl"

#>

'
'	Project ${Project.Name}
'		${Project.Description}
'	Entity	${Entity.Name}
'		${Entity.Description}
'	
'

Public Class ${Entity.Name}

'	Private Fields

<#
for each Property in Entity.Properties
	message	"Processing Field ${Property.Name}"
#>	
	Private m${Property.Name} as ${VbType(Property)} <#	
end for
#>

'	Default Constructor

	Public Sub New()
	End Sub

'	Public Properties

<#
for each Property in Entity.Properties
	message	"Processing Property ${Property.Name}"
#>	
	Public Property ${Property.Name}() as ${VbType(Property)}
        Get
            Return m${Property.Name}
        End Get
        Set(ByVal Value As ${VbType(Property)})
            m${Property.Name} = Value
        End Set
	End Property
<#
end for
#>

End Class

