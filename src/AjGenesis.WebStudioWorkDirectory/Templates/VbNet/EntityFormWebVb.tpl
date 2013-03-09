<#

message	"Generating Form for Entity ${Entity.Name}..."

include "Templates/VbNet/VbFunctions.tpl"
include "Templates/EntityFunctions.tpl"

EntitySqlProperties	= SqlProperties(Entity)
EntityNoIdSqlProperties	= SqlNoIdProperties(Entity)
EntityIdProperty = IdProperty(Entity)
#>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="${Entity.Name}Update.aspx.vb" Inherits="${Project.Name}.WebClient.${Entity.Name}UpdatePage"%>
<%@ Register TagPrefix="uc1" TagName="Header" Src="~/Controls/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Title" Src="~/Controls/Title.ascx" %>
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
			<uc1:Title id="Title1" Title="Update ${Entity.Descriptor}" runat="server"></uc1:Title>
			<br>
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
			<TABLE class="DataTable" id="tblDatos" cellSpacing="1" cellPadding="1" width="80%" border="1">
<#
for each Property in Entity.Properties where Property.Type <> "Id"
	if Property.Reference then
		RefDescriptorProperty = Property.Reference.DescriptorProperty
		RefIdProperty = IdProperty(Property.Reference)
#>
				<TR>
					<TD>
						${Property.Description}</TD>
					<TD>
						<asp:DropDownList id=ddl${Property.Reference.SetName} runat="server" DataTextField="${RefDescriptorProperty.Name}" DataValueField="${RefIdProperty.Name}" DataSource="<%# ${Property.Reference.SetName} %>">
						</asp:DropDownList>
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
						<asp:DropDownList id=ddl${Property.Name} runat="server" DataTextField="Description" DataValueField="Id" DataSource="<%# ${Property.Enumeration.Name}List %>">
						</asp:DropDownList>
					</TD>
				</TR>
<#
		else
#>			
				<TR>
					<TD>
						${Property.Description}</TD>
					<TD>
						<asp:TextBox id="txt${Property.Name}" runat="server" Text="<%# Entity.${Property.Name} %>">
						</asp:TextBox></TD>
				</TR>
<#				
		end if
	end if
end for
#>
			</TABLE>
			<asp:Button id="btnAccept" runat="server" Text="Accept"></asp:Button>
			<br>			
	<uc1:Footer id="Footer1" runat="server"></uc1:Footer>
	</form>
	</body>
</HTML>
