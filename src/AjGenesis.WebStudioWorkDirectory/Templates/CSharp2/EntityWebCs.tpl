<#

message	"Generating Page for Entity ${Entity.Name}..."

include "Templates/CSharp2/CSharpFunctions.tpl"
include "Templates/EntityFunctions.tpl"

EntitySqlProperties	= SqlProperties(Entity)
EntityNoIdSqlProperties	= SqlNoIdProperties(Entity)
EntityIdProperty = IdProperty(Entity)
#>

<%@ Page Language="C#"  MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="${Entity.Name}.aspx.cs" Inherits="${WebPage.Prefix}${Entity.Name}Page"%>
<%@ Register Src="~/Controls/Subtitle.ascx" TagName="Subtitle" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
			<br />
			<div class="Normal">
				<a href="${Entity.SetName}.aspx">${Entity.SetDescriptor}</a>
				&nbsp;&nbsp;
				<a href="${Entity.Name}Update.aspx?Id=<%=IdEntity%>">Update</a>
				&nbsp;&nbsp;
				<a href="${Entity.Name}Delete.aspx?Id=<%=IdEntity%>">Delete</a>
			</div>
			<br />
			<table class="DataTable" id="tblDatos" cellSpacing="1" cellPadding="1" width="80%" border="1">
<#			
for each Property in Entity.Properties
	if Property.Reference then
		RefDescriptorProperty = Property.Reference.DescriptorProperty
		RefIdProperty = IdProperty(Property.Reference)
#>
				<tr>
					<td>
						${Property.Description}</td>
					<td>
						<asp:HyperLink id="lnk${Property.Name}" runat="server" Text="<%# Entity${Property.Reference.Name}.${RefDescriptorProperty.Name} %>" NavigateUrl='<%# "${Property.Reference.Name}.aspx?Id=" + Entity${Property.Reference.Name}.Id %>'>
						</asp:HyperLink>
					</td>
				</tr>
<#
	else
		if Property.Enumeration then
#>
				<tr>
					<td>
						${Property.Description}</td>
					<td>
						<asp:Label id="lbl${Property.Name}" runat="server" Text="<%# ${Property.Name}Description %>">
						</asp:Label></td>
				</tr>
<#
		else
#>
				<tr>
					<td>
						${Property.Description}</td>
					<td>
						<asp:Label id="lbl${Property.Name}" runat="server" Text="<%# Entity.${Property.Name} %>">
						</asp:Label></td>
				</tr>
<#
		end if
	end if		
end for
#>
			</table>
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

</asp:Content>
