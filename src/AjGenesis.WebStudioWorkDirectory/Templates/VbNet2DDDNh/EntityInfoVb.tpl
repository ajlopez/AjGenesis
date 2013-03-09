<#

message "Generating Entity ${Entity.Name}"

include	"Templates/VbNet2DDDNh/VbFunctions.tpl"

include	"Templates/VbNet2DDDNh/Prologue.tpl"
#>

'
'	Project ${Project.Name}
'		${Project.Description}
'	Entity Message	${Entity.Name}Info
'		${Entity.Description}
'	
'

Public Class ${Entity.Name}Info

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

'	Factory Methods

	Public Shared Function Create( _
<#
	for each Property in Entity.Properties where Property.Type<>"Id"
#>
			${Property.Name} as ${VbType(Property)} ${OnLast("",",")} _
<#
	end for
#>
		) as ${Entity.Name}Info

		Dim ${Entity.VarName} as ${Entity.Name}Info

		${Entity.VarName} = new ${Entity.Name}Info()

<#
	for each Property in Entity.Properties where Property.Type<>"Id"
#>
		${Entity.VarName}.${Property.Name} = ${Property.Name}
<#
	end for
#>

		return ${Entity.VarName}
	End Function

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

