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
			<TABLE class="DataTable" id="tblDatos" cellSpacing="1" cellPadding="1" width="300" border="1">
<#			
for each Property in Entity.Properties
#>
				<TR>
					<TD>
						${Property.Description}</TD>
					<TD>
						<asp:Label id="lbl${Property.Name}" runat="server" Text="<%# Entity.${Property.Name} %>">
						</asp:Label></TD>
				</TR>
<#				
end for
#>
			</TABLE>
	<uc1:Footer id="Footer1" runat="server"></uc1:Footer>
		</form>
	</body>
</HTML>
