<#

message "Generating Entity ${Entity.Name}"

include	"Templates/VbNet/VbFunctions.tpl"

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
	message	"Procesando Campo ${Property.Name}"
#>	
	Private m${Property.Name} as ${VbType(Property)}
<#	
end for
#>

'	Default Constructor

	Public Sub New()
	End Sub

'	Public Properties

<#
for each Property in Entity.Properties
	message	"Procesando Propiedad ${Property.Name}"
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

