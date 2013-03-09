<#

message	"Generating Page for Entities ${Entity.SetName}..."

include "Templates/VbNet/VbFunctions.tpl"
include "Templates/EntityFunctions.tpl"

EntitySqlProperties	= SqlProperties(Entity)
EntityNoIdSqlProperties	= SqlNoIdProperties(Entity)
EntityIdProperty = IdProperty(Entity)
#>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="${Entity.SetName}.aspx.vb" Inherits="${Project.Name}.WebClient.${Entity.SetName}Page"%>
<%@ Register TagPrefix="uc1" TagName="Title" Src="~/Controls/Title.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="~/Controls/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="~/Controls/Footer.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>${Entity.SetDescriptor}</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="<%=Request.ApplicationPath%>/Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" marginwidth="0" marginheight="0">
		<form id="Form1" method="post" runat="server">
			<uc1:Header id="Header1" runat="server"></uc1:Header>
			<uc1:Title id="Title1" Title="${Entity.SetDescriptor}" runat="server"></uc1:Title>
			<br>
			<div class="Normal">
				<a href="${Entity.Name}Update.aspx">New ${Entity.Descriptor}</a>
			</div>
			<br>
			<asp:DataGrid id="dtgData" runat="server" CssClass="DataTable" AutoGenerateColumns="False">
				<Columns>
					<asp:HyperLinkColumn DataNavigateUrlField="${EntityIdProperty.Name}" DataNavigateUrlFormatString="${Entity.Name}.aspx?${EntityIdProperty.Name}={0}" DataTextField="${EntityIdProperty.Name}" HeaderText="${EntityIdProperty.Description}"></asp:HyperLinkColumn>
<#					
for each	Property in EntityNoIdSqlProperties
#>
					<asp:BoundColumn DataField="${Property.Name}" HeaderText="${Property.Description}"></asp:BoundColumn>
<#
end for
#>
				</Columns>
			</asp:DataGrid>
	<uc1:Footer id="Footer1" runat="server"></uc1:Footer>
		</form>
	</body>
</HTML>
