<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Subtitle.ascx.cs" Inherits="${Project.Name}.WebClient.Controls.Subtitle" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table width="98%" cellspacing="0" cellpadding="0">
	<tr>
		<td align="center">
			<asp:label id="PageSubtitle" cssclass="Subhead" EnableViewState="False" runat="server" Text="<%# TitleText %>"/>
		</td>
	</tr>
</table>
