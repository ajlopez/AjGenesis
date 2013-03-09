<%@ Control Language="vb" AutoEventWireup="false" CodeFile="Title.ascx.vb" Inherits="Controls_Title" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table width="98%" cellspacing="0" cellpadding="0">
	<tr>
		<td align="left">
			<asp:label id="PageTitle" cssclass="Head" EnableViewState="False" runat="server" Text="<%# Title %>"/>
			<hr noshade size="1">
		</td>
	</tr>
</table>
