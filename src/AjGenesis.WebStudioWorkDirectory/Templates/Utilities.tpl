<#
Function CreateList()
	return new System.Collections.ArrayList()
End Function

Function CreateObject()
	return new AjGenesis.Models.DynamicModel.DynamicObject()
End Function

Function GetItem(Items,Name)
	for each Item in Items where Item.Name=Name
		return Item
	end for

	return Nothing
End Function
#>