<#

message	"Generating Form for Entity ${Entity.Name}..."

include "Templates/VbNet2/VbFunctions.tpl"
include "Templates/EntityFunctions.tpl"

EntitySqlProperties	= SqlProperties(Entity)
EntityNoIdSqlProperties	= SqlNoIdProperties(Entity)
EntityIdProperty = IdProperty(Entity)

#>

<%@ Page Language="vb" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="false" CodeFile="${Entity.Name}Update.aspx.vb" Inherits="${WebPage.Prefix}${Entity.Name}UpdatePage" Title="${Entity.Descriptor}"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

			<br />
			<div class="Normal">
				<a href="${Entity.SetName}.aspx">${Entity.SetDescriptor}</a>
<%
	if IdEntity>0 then
%>
				&nbsp;&nbsp;
				<a href="${Entity.Name}.aspx?Id=<%=IdEntity%>">${Entity.Descriptor}</a>		
<%
	end if
%>
			</div>
			<asp:Label id="lblMensaje" runat="server" CssClass="Error" Visible="False"></asp:Label>
			<asp:ValidationSummary id="ValidationSummary1" runat="server" CssClass="Error" DisplayMode="List"></asp:ValidationSummary>
			<br />
			<table class="DataTable" id="tblDatos" cellSpacing="1" cellPadding="1" width="80%" border="1">
<#
for each Property in Entity.Properties where Property.Type <> "Id"
	if Property.Reference then
		RefDescriptorProperty = Property.Reference.DescriptorProperty
		RefIdProperty = IdProperty(Property.Reference)
#>
				<tr>
					<td>
						${Property.Description}</td>
					<td>
						<asp:DropDownList id=ddl${Property.Reference.SetName} runat="server" DataTextField="${RefDescriptorProperty.Name}" DataValueField="${RefIdProperty.Name}" DataSource="<%# ${Property.Reference.SetName} %>">
						</asp:DropDownList>
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
						<asp:DropDownList id=ddl${Property.Name} runat="server" DataTextField="Description" DataValueField="Id" DataSource="<%# ${Property.Enumeration.Name}List %>">
						</asp:DropDownList>
					</td>
				</tr>
<#
		else
#>			
				<tr>
					<td>
						${Property.Description}</td>
					<td>
						<asp:TextBox id="txt${Property.Name}" runat="server" Text="<%# Entity.${Property.Name} %>">
						</asp:TextBox></td>
				</tr>
<#				
		end if
	end if
end for
#>
			</table>
			<asp:Button id="btnAccept" runat="server" Text="Accept"></asp:Button>
			<br />			

</asp:Content>

