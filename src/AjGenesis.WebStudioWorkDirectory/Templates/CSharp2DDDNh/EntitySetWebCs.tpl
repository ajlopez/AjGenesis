<#

message	"Generating Page for Entities ${Entity.SetName}..."

include "Templates/CSharp2DDDNh/CSharpFunctions.tpl"
include "Templates/EntityFunctions.tpl"

Properties = GetAllProperties(Entity)

EntitySqlProperties	= SqlProperties(Entity)
EntityNoIdSqlProperties	= SqlNoIdProperties(Entity)
EntityIdProperty = IdProperty(Entity)
#>

<%@ Page Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="${Entity.SetName}.aspx.cs" Inherits="${WebPage.Prefix}${Entity.SetName}Page" Title="${Entity.SetDescriptor}"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

			<br />
			<div class="Normal">
				<a href="${Entity.Name}Update.aspx">New ${Entity.Descriptor}</a>
			</div>
			<br />
			<asp:GridView id="gvwData" runat="server" CssClass="DataTable" AutoGenerateColumns="False" Width="80%">
				<Columns>
					<asp:HyperLinkField DataNavigateUrlFields="${Entity.IdProperty.Name}" DataNavigateUrlFormatString="${Entity.Name}.aspx?${Entity.IdProperty.Name}={0}" DataTextField="${Entity.IdProperty.Name}" HeaderText="${Entity.IdProperty.Description}"></asp:HyperLinkField>
<#					
for each	Property in Properties where Property.Type<>"Id" and Property.SqlType
	if Property.Reference then
#>
					<asp:TemplateField HeaderText="${Property.Reference.Descriptor}">
						<asp:ItemTemplate>
						<asp:HyperLink id='hln${Property.Reference.Name}' runat='server' NavigateUrl='<%# "${Property.Reference.Name}.aspx?Id=" + ((${Project.Name}.Domain.${Property.Reference.Name}) DataBinder.Eval(Container,"DataItem.${Property.Reference.Name}")).${Property.Reference.IdProperty.Name} %>'>
							<%# ((${Project.Name}.Domain.${Property.Reference.Name}) DataBinder.Eval(Container,"DataItem.${Property.Reference.Name}")).${Property.Reference.DescriptorProperty.Name} %>
						</asp:HyperLink>
						</asp:ItemTemplate>
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

</asp:Content>
