<#

message	"Generating Form for Entity ${Entity.Name}..."

include "Templates/CSharp2DDDNh/CSharpFunctions.tpl"
include "Templates/EntityFunctions.tpl"

Properties = GetAllProperties(Entity)
#>

<%@ Page Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="${Entity.Name}Update.aspx.cs" Inherits="${WebPage.Prefix}${Entity.Name}UpdatePage"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

			<br />
			<div class="Normal">
				<a href="${Entity.SetName}.aspx">${Entity.SetDescriptor}</a>
<%
	if (IdEntity>0) {
%>
				&nbsp;&nbsp;
				<a href="${Entity.Name}.aspx?Id=<%=IdEntity%>">${Entity.Descriptor}</a>		
<%
	}
%>
			</div>
			<asp:Label id="lblMensaje" runat="server" CssClass="Error" Visible="False"></asp:Label>
			<asp:ValidationSummary id="ValidationSummary1" runat="server" CssClass="Error" DisplayMode="List"></asp:ValidationSummary>
			<br />
			<table class="DataTable" id="tblDatos" cellSpacing="1" cellPadding="1" width="80%" border="1">
<#
for each Property in Properties where Property.Type <> "Id"
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
			<asp:Button id="btnAccept" runat="server" Text="Accept" OnClick="btnAccept_Click"></asp:Button>
			<br />			
</asp:Content>
