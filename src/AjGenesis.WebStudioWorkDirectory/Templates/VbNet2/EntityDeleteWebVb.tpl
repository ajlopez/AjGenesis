<#

message	"Generating Delete Page for Entity ${Entity.Name}..."

include "Templates/VbNet2/VbFunctions.tpl"
include "Templates/EntityFunctions.tpl"

#>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="${Entity.Name}Delete.aspx.vb" Inherits="${Project.Name}.WebClient.${Entity.Name}DeletePage"%>
<%@ Page Language="vb" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="false" CodeFile="${Entity.Name}Delete.aspx.vb" Inherits="${WebPage.Prefix}${Entity.Name}DeletePage" Title="${Entity.Descriptor}"%>
