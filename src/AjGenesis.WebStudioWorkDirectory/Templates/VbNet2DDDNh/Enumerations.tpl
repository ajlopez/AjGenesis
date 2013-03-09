<#
	for each Enumeration in Project.Model.Enumerations
		if Enumeration.Type.ToLower()="string" then
			IsText=true
			VbType="String"
		else
			IsText=false
			VbType="Integer"
		end if
#>
Public Enum ${Enumeration.Name} as ${VbType}
<#
		for each Item in Enumeration.Items
			if IsText then
#>
	${Item.Value} = "${Item.Key}"
<#
			else
#>
	${Item.Value} = ${Item.Key}
<#
			end if
		end for
#>
End Enum

<#
	end for
#>

Public Class EnumerationValue
	Private mId as Object
	Private mDescription as String

	Public Sub New(id as Object, description as String)
		mId = id
		mDescription = description
	End Sub

	Public Property Id() As Object
        Get
            	Return mId
        End Get
        Set(ByVal Value As Object)
			mId = Value
        End Set
	End Property

	Public Property Description() As String
        Get
            	Return mDescription
        End Get
        Set(ByVal Value As String)
			mDescription = Value
        End Set
    End Property
End Class

Public Class Enumerations
<#
	for each Enumeration in Project.Model.Enumerations
#>
	Public Shared ${Enumeration.Name}List as IList
<#
	end for
#>

	Shared Sub New()
<#
	for each Enumeration in Project.Model.Enumerations
		if Enumeration.Type.ToLower()="string" then
			IsText=true
		else
			IsText=false
		end if
#>

		${Enumeration.Name}List = new ArrayList()
<#
		for each Item in Enumeration.Items
			if IsText then
#>
		${Enumeration.Name}List.Add(new EnumerationValue("${Item.Key}","${Item.Description}"))
<#
			else
#>
		${Enumeration.Name}List.Add(new EnumerationValue(${Item.Key},"${Item.Description}"))
<#
			end if
		end for
	end for
#>
	End Sub

	Public Shared Function Translate(values as IList, key as Object) as String
		Dim ev as EnumerationValue

		For Each ev in values
			if ev.Id=key then
				return ev.Description
			end if
		Next

		return key.ToString()
	End Function
End Class