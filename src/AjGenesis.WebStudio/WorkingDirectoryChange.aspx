<%@ Page Language="VB" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="false" CodeFile="WorkingDirectoryChange.aspx.vb" Inherits="WorkingDirectoryChange" title="Change Working Directory" Theme="Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<div align="left">
&nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="lnkHelp" runat="server" NavigateUrl="~/PageView.aspx?Page=WorkingDirectoryChangePageHelp" Text="Help" Target="_blank"/>&nbsp;&nbsp;&nbsp;
</div>  
<br />
<br />  
<table class="DataTable" width="500" cellpadding="2" cellspacing="0">
<tr>
<td>
New Working Directory
</td>
<td valign="top" align="left">
<asp:TextBox ID="txtWorkingDirectory" runat="server" Width="300" />
</td>
</tr>
</table>
    <asp:Button ID="btnChange" runat="server" Text="Change"/>
    <asp:Button ID="btnReset" runat="server" Text="Reset"/>
</asp:Content>

