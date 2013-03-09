<#

message	"Generating Page for Entities ${Entity.SetName}..."

include "Templates/CSharp2/CSharpFunctions.tpl"
include "Templates/EntityFunctions.tpl"

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
					<asp:HyperLinkField DataNavigateUrlFields="${EntityIdProperty.Name}" DataNavigateUrlFormatString="${Entity.Name}.aspx?${EntityIdProperty.Name}={0}" DataTextField="${EntityIdProperty.Name}" HeaderText="${EntityIdProperty.Description}"></asp:HyperLinkField>
<#					
for each	Property in EntityNoIdSqlProperties
	if Property.Reference then
#>
					<asp:BoundField DataField="${Property.Reference.Name}Description" HeaderText="${Property.Description}"></asp:BoundField>
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
