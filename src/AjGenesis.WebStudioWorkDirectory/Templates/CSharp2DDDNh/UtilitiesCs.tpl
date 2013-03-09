<#
Function CreateList()
	return new System.Collections.ArrayList()
End Function

Function CreateObject()
	return new AjGenesis.Models.DynamicModel.DynamicObject()
End Function

Function CreateGuid()
	guid = new System.Guid()
	guid = guid.NewGuid()
	guid = guid.ToString()
	guid = guid.ToUpper()
	return guid
End Function

Function CreateFileType(name,type)
	file = CreateObject()
	file.Name = name
	file.Type = type
	return file
End Function

Function CreateFileVb(name)
	file = CreateObject()
	file.Name = name
	file.Type = "vb"
	return file
End Function

Function CreateFileCs(name)
	file = CreateObject()
	file.Name = name
	file.Type = "cs"
	return file
End Function

Function CreateFileAspx(name)
	file = CreateObject()
	file.Name = name
	file.Type = "aspx"
	return file
End Function

Function CreateFileAspxVb(name)
	file = CreateObject()
	file.Name = name
	file.Type = "aspx.vb"
	return file
End Function

Function CreateFileAspxCs(name)
	file = CreateObject()
	file.Name = name
	file.Type = "aspx.cs"
	return file
End Function

Function CreateFileAspxResx(name)
	file = CreateObject()
	file.Name = name
	file.Type = "aspx.resx"
	return file
End Function

Function CreateFileAscx(name)
	file = CreateObject()
	file.Name = name
	file.Type = "ascx"
	return file
End Function

Function CreateFileLib(name,hint)
	file = CreateObject()
	file.Name = name
	file.Hint = hint
	return file
End Function

#>