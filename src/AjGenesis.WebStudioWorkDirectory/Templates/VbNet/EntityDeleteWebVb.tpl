<#

message	"Generating Delete Page for Entity ${Entity.Name}..."

include "Templates/VbNet/VbFunctions.tpl"
include "Templates/EntityFunctions.tpl"

#>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="${Entity.Name}Delete.aspx.vb" Inherits="${Project.Name}.WebClient.${Entity.Name}DeletePage"%>
