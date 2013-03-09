<#

message	"Generating Delete Page for Entity ${Entity.Name}..."

include "Templates/CSharp2Nh/CSharpFunctions.tpl"
include "Templates/EntityFunctions.tpl"

#>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="${Entity.Name}Delete.aspx.cs" Inherits="${WebPage.Prefix}${Entity.Name}DeletePage"%>
