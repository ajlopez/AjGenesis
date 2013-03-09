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

Public Class ${Entity.Name}

'	Private Fields

<#
for each Property in Entity.Properties where not Property.Reference
	message	"Processing Field ${Property.Name}"
		if Property.Type="Date" then
#>	
	Private m${Property.Name} as ${VbType(Property)} = Date.Today <#			
		else
#>	
	Private m${Property.Name} as ${VbType(Property)} <#	
		end if
end for
#>

<#
for each Property in Entity.Properties where Property.Reference
	message	"Processing Field ${Property.Reference.Name}"
#>	
	Private m${Property.Reference.Name} as ${Property.Reference.Name} <#	
end for
#>

<#
for each Relation in Entity.Relations where Relation.RelationType="Referenced"
	message	"Processing Relation ${Relation.Entity.SetName}"
#>	
	Private m${Relation.Entity.SetName} as IList <#	
end for
#>


'	Default Constructor

	Public Sub New()
	End Sub

'	Factory Methods

	Public Shared Function Create( _
<#
	for each Property in Entity.Properties where Property.Type<>"Id"
		if Property.Reference then
#>
			${Property.Reference.Name} as ${Property.Reference.Name} ${OnLast("",",")} _
<#
		else
#>
			${Property.Name} as ${VbType(Property)} ${OnLast("",",")} _
<#
		end if
	end for
#>
		) as ${Entity.Name}
		Dim ${Entity.VarName} as ${Entity.Name}

		${Entity.VarName} = new ${Entity.Name}()

<#
	for each Property in Entity.Properties where Property.Type<>"Id"
		if Property.Reference then
#>
		${Entity.VarName}.${Property.Reference.Name} = ${Property.Reference.Name}
<#
		else
#>
		${Entity.VarName}.${Property.Name} = ${Property.Name}
<#
		end if
	end for
#>

		return ${Entity.VarName}
	End Function

'	Public Properties

<#
for each Property in Entity.Properties where not Property.Reference
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

<#
for each Property in Entity.Properties where Property.Reference
	message	"Processing Property ${Property.Name}"
#>	
	Public Property ${Property.Reference.Name}() as ${Property.Reference.Name}
        Get
            Return m${Property.Reference.Name}
        End Get
        Set(ByVal Value As ${Property.Reference.Name})
            m${Property.Reference.Name} = Value
        End Set
	End Property
<#
end for
#>

<#
for each Relation in Entity.Relations where Relation.RelationType="Referenced"
	message	"Processing Relation ${Relation.Entity.SetName}"
#>	
	Public Property ${Relation.Entity.SetName}() as IList
        Get
            Return m${Relation.Entity.SetName}
        End Get
        Set(ByVal Value As IList)
            m${Relation.Entity.SetName} = Value
        End Set
	End Property

<#
end for
#>

End Class

