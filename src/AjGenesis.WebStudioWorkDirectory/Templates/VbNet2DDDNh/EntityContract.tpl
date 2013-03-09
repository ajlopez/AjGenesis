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
for each Property in Entity.Properties where not Property.Reference
	message	"Processing Field ${Property.Name}"
#>	
	Private m${Property.Name} as ${VbType(Property)} <#	
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

