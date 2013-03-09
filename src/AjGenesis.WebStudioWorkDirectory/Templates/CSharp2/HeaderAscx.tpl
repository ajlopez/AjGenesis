<%@ Control Language="c#" AutoEventWireup="false" CodeFile="Header.ascx.cs" Inherits="Controls_Header" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table id=TableTop width="100%" cellspacing="0" border="0">
	<tr height="70">
		<td width="10" rowspan="2">
		</td>
		<td height="40" class="SiteTitle">
${Project.Name}
		</td>
		<td align="middle" rowspan="2">
		</td>
	</tr>
</table>
<table id=TableMenuTop cellSpacing="0" cellPadding="0" width="100%" border="0" height=15>
	<tr>
		<td width="140" class="MainMenu"><a href='~/Default.aspx' runat="server" ID="A1">Main</a></td>
		<td width="140" class="MainMenu"><a href='~/Admin/Default.aspx' runat="server" ID="A2">Administration</a></td>
		<td width="140" class="MainMenu"><a href='http://www.ajlopez.com/' runat="server" ID="A3">ajlopez.com</a></td>
		<td class="MainMenu" width="*">&nbsp;</td>
	</tr>
</table>
<br>
<table cellpadding="10" width="100%">
	<tr>
		<td>
<div align="center">