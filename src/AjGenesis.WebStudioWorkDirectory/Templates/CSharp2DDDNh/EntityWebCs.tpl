<#

message	"Generating Page for Entity ${Entity.Name}..."

include "Templates/CSharp2DDDNh/CSharpFunctions.tpl"
include "Templates/EntityFunctions.tpl"

Properties = GetAllProperties(Entity)
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
for each Property in Properties
	if Property.Reference then
		RefDescriptorProperty = Property.Reference.DescriptorProperty
		RefIdProperty = IdProperty(Property.Reference)
#>
				<tr>
					<td>
						${Property.Description}</td>
					<td>
						<asp:HyperLink id="lnk${Property.Name}" runat="server" Text="<%# Entity.${Property.Reference.Name}.${RefDescriptorProperty.Name} %>" NavigateUrl='<%# "${Property.Reference.Name}.aspx?Id=" + Entity.${Property.Reference.Name}.Id %>'>
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
			<asp:GridView id="gvwData${Relation.Entity.SetName}" datasource="<%# Entity.${Relation.Entity.SetName} %>" runat="server" CssClass="DataTable" AutoGenerateColumns="False" Width="80%">
				<Columns>
					<asp:HyperLinkField DataNavigateUrlFields="${EntityIdProperty.Name}" DataNavigateUrlFormatString="${Relation.Entity.Name}.aspx?${EntityIdProperty.Name}={0}" DataTextField="${EntityIdProperty.Name}" HeaderText="${EntityIdProperty.Description}"></asp:HyperLinkField>
<#					
for each	Property in Relation.Entity.Properties where Property.Type<>"Id" and Property.Name<>Relation.Property.Name
	if Property.Reference then
		RefDescriptorProperty = Property.Reference.DescriptorProperty
		RefIdProperty = IdProperty(Property.Reference)
#>

                    <asp:TemplateField HeaderText="${Property.Description}">
                        <ItemTemplate>
						<asp:HyperLink id='hln${Property.Reference.Name}' runat='server' NavigateUrl='<%# "${Property.Reference.Name}.aspx?Id=" + ((${Project.Name}.Domain.${Relation.Entity.Name})Container.DataItem).${Property.Reference.Name}.${Property.Reference.IdProperty.Name} %>'>
							<asp:Label id='lbl${Property.Reference.Name}' runat='server' text='<%# ((${Project.Name}.Domain.${Relation.Entity.Name})Container.DataItem).${Property.Reference.Name}.${Property.Reference.DescriptorProperty.Name} %>'></asp:Label>
						</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
<#
	else
#>
					<asp:BoundField DataField="${Property.Name}" HeaderText="${Property.Description}"></asp:BoundField>
<#
	end if
end for
#>
				</Columns>
			</asp:GridView>
<#
	end for
#>


</asp:Content>
