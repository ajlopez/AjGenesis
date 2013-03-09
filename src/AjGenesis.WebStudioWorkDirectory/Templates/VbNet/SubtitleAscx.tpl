<%@ Control Language="vb" AutoEventWireup="false" Codebehind="Subtitle.ascx.vb" Inherits="${Project.Name}.WebClient.Subtitle" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table width="98%" cellspacing="0" cellpadding="0">
	<tr>
		<td align="center">
			<asp:label id="PageSubtitle" cssclass="Subhead" EnableViewState="False" runat="server" Text="<%# Title %>"/>
		</td>
	</tr>
</table>
