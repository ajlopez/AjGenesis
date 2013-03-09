<#

message "VbNet Utilities..."

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

Function CreateFileAspxResx(name)
	file = CreateObject()
	file.Name = name
	file.Type = "aspx.resx"
	return file
End Function

Function CreateFileAsmx(name)
	file = CreateObject()
	file.Name = name
	file.Type = "asmx"
	return file
End Function

Function CreateFileAsmxVb(name)
	file = CreateObject()
	file.Name = name
	file.Type = "asmx.vb"
	return file
End Function

Function CreateFileAsmxResx(name)
	file = CreateObject()
	file.Name = name
	file.Type = "asmx.resx"
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