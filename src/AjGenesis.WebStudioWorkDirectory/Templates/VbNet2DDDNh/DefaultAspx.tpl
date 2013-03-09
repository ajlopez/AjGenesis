<%@ Page Language="VB" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="${WebPage.Prefix}DefaultPage" title="Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

<#
	for each Entity in Project.Model.Entities
#>
<a href="${Entity.SetName}.aspx">${Entity.SetDescriptor}</a>
<br />
<#
	end for
#>

</asp:Content>
