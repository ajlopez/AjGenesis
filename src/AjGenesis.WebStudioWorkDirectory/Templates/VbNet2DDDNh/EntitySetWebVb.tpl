<#

message	"Generating Page for Entities ${Entity.SetName}..."

include "Templates/VbNet2/VbFunctions.tpl"
include "Templates/EntityFunctions.tpl"

EntitySqlProperties	= SqlProperties(Entity)
EntityNoIdSqlProperties	= SqlNoIdProperties(Entity)
EntityIdProperty = IdProperty(Entity)
#>
<%@ Page Language="VB" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="false" CodeFile="${Entity.SetName}.aspx.vb" Inherits="${WebPage.Prefix}${Entity.SetName}Page" Title="${Entity.SetDescriptor}"%>
<%@ Register Src="~/Controls/Subtitle.ascx" TagName="Subtitle" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

			<br />
			<div class="Normal">
				<a href="${Entity.Name}Update.aspx">New ${Entity.Descriptor}</a>
			</div>
			<br />
			<asp:GridView id="gvwData" runat="server" CssClass="DataTable" AutoGenerateColumns="False" Width="80%">
				<Columns>
					<asp:HyperLinkField DataNavigateUrlFields="${EntityIdProperty.Name}" DataNavigateUrlFormatString="${Entity.Name}.aspx?${EntityIdProperty.Name}={0}" DataTextField="${EntityIdProperty.Name}" HeaderText="${EntityIdProperty.Description}"></asp:HyperLinkField>
<#					
for each	Property in EntityNoIdSqlProperties
	if Property.Reference then
#>
					<asp:TemplateField HeaderText="${Property.Reference.Descriptor}">
						<asp:ItemTemplate>
						<asp:HyperLink id='hln${Property.Reference.Name}' runat='server' NavigateUrl='<%# "${Property.Reference.Name}.aspx?Id=" & DirectCast(DataBinder.Eval(Container,"DataItem.${Property.Reference.Name}"),${Project.Name}.Domain.${Property.Reference.Name}).${Property.Reference.IdProperty.Name} %>'>
							<%# DirectCast(DataBinder.Eval(Container,"DataItem.${Property.Reference.Name}"),${Project.Name}.Domain.${Property.Reference.Name}).${Property.Reference.DescriptorProperty.Name} %>
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
