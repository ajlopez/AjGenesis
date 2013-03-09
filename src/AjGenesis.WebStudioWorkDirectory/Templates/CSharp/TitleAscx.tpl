<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Title.ascx.cs" Inherits="${Project.Name}.WebClient.Controls.Title" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table width="98%" cellspacing="0" cellpadding="0">
	<tr>
		<td align="center">
			<asp:label id="PageTitle" cssclass="Head" EnableViewState="False" runat="server" Text="<%# TitleText %>"/>
		</td>
	</tr>
	<tr>
		<td colspan="2">
			<hr noshade size="1">
		</td>
	</tr>
</table>
