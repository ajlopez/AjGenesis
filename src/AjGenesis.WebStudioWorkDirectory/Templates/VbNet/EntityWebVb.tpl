<#

message	"Generating Page for Entity ${Entity.Name}..."

include "Templates/VbNet/VbFunctions.tpl"
include "Templates/EntityFunctions.tpl"

EntitySqlProperties	= SqlProperties(Entity)
EntityNoIdSqlProperties	= SqlNoIdProperties(Entity)
EntityIdProperty = IdProperty(Entity)
#>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="${Entity.Name}.aspx.vb" Inherits="${Project.Name}.WebClient.${Entity.Name}Page"%>
<%@ Register TagPrefix="uc1" TagName="Title" Src="~/Controls/Title.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Subtitle" Src="~/Controls/Subtitle.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="~/Controls/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="~/Controls/Footer.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>${Entity.Descriptor}</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="<%=Request.ApplicationPath%>/Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" marginwidth="0" marginheight="0">
		<form id="Form1" method="post" runat="server">
			<uc1:Header id="Header1" runat="server"></uc1:Header>
			<uc1:Title id="Title1" Title="${Entity.Descriptor}" runat="server"></uc1:Title>
			<br>
			<div class="Normal">
				<a href="${Entity.SetName}.aspx">${Entity.SetDescriptor}</a>
				&nbsp;&nbsp;
				<a href="${Entity.Name}Update.aspx?Id=<%=IdEntity%>">Update</a>
				&nbsp;&nbsp;
				<a href="${Entity.Name}Delete.aspx?Id=<%=IdEntity%>">Delete</a>
			</div>
			<br />
			<TABLE class="DataTable" id="tblDatos" cellSpacing="1" cellPadding="1" width="80%" border="1">
<#			
for each Property in Entity.Properties
	if Property.Reference then
		RefDescriptorProperty = Property.Reference.DescriptorProperty
		RefIdProperty = IdProperty(Property.Reference)
#>
				<TR>
					<TD>
						${Property.Description}</TD>
					<TD>
						<asp:HyperLink id="lnk${Property.Name}" runat="server" Text="<%# Entity${Property.Reference.Name}.${RefDescriptorProperty.Name} %>" NavigateUrl='<%# "${Property.Reference.Name}.aspx?Id=" &amp; Entity${Property.Reference.Name}.Id %>'>
						</asp:HyperLink>
					</TD>
				</TR>
<#
	else
		if Property.Enumeration then
#>
				<TR>
					<TD>
						${Property.Description}</TD>
					<TD>
						<asp:Label id="lbl${Property.Name}" runat="server" Text="<%# ${Property.Name}Description %>">
						</asp:Label></TD>
				</TR>
<#
		else
#>
				<TR>
					<TD>
						${Property.Description}</TD>
					<TD>
						<asp:Label id="lbl${Property.Name}" runat="server" Text="<%# Entity.${Property.Name} %>">
						</asp:Label></TD>
				</TR>
<#
		end if
	end if		
end for
#>
			</TABLE>
<#
	nrel = 0
	for each Relation in Entity.Relations where Relation.RelationType="Referenced"
		nrel = nrel+1
		EntityIdProperty = IdProperty(Relation.Entity)
#>
			<br>
			<uc1:Subtitle id="Subtitle<# print nrel #>" Title="${Relation.Entity.SetDescriptor}" runat="server"></uc1:Subtitle>
			<br>
			<div class="Normal">
				<a href="${Relation.Entity.Name}Update.aspx?${Relation.Property.Name}=<%=IdEntity%>">New ${Relation.Entity.Descriptor}</a>
			</div>
			<br>
			<asp:DataGrid id="dtgData${Relation.Entity.SetName}" runat="server" CssClass="DataTable" AutoGenerateColumns="False" Width="80%">
				<Columns>
					<asp:HyperLinkColumn DataNavigateUrlField="${EntityIdProperty.Name}" DataNavigateUrlFormatString="${Relation.Entity.Name}.aspx?${EntityIdProperty.Name}={0}" DataTextField="${EntityIdProperty.Name}" HeaderText="${EntityIdProperty.Description}"></asp:HyperLinkColumn>
<#					
for each	Property in Relation.Entity.Properties where Property.Type<>"Id" and Property.Name<>Relation.Property.Name
	if Property.Reference then
#>
					<asp:BoundColumn DataField="${Property.Reference.Name}Description" HeaderText="${Property.Description}"></asp:BoundColumn>
<#
	else
#>
					<asp:BoundColumn DataField="${Property.Name}" HeaderText="${Property.Description}"></asp:BoundColumn>
<#
	end if
end for
#>
				</Columns>
			</asp:DataGrid>
<#
	end for
#>
	<uc1:Footer id="Footer1" runat="server"></uc1:Footer>
		</form>
	</body>
</HTML>
