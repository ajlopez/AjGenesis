<%@ Page Language="VB" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="false" CodeFile="PageEdit.aspx.vb" Inherits="PageEdit" title="Edit Page" Theme="Default" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<div align="left">
&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkCancel" runat="server">Cancel Edit</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="lnkHelp" runat="server" NavigateUrl="~/PageView.aspx?Page=PageEditPageHelp" Text="Edit Help" Target="_blank"/>&nbsp;&nbsp;&nbsp;
</div>  
<br />
<br />  
<table class="DataTable" width="400" cellpadding="2" cellspacing="0">
<tr>
<td valign="top">
Code
</td>
<td valign="top">
<asp:TextBox ID="txtCode" runat="server"/>
</td>
</tr>
<tr>
<td valign="top">
Title
</td>
<td valign="top">
<asp:TextBox ID="txtTitle" runat="server"/>
</td>
</tr>
<tr>
<td valign="top">
Content
</td>
<td valign="top">
<asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Rows="30" Columns="60" Wrap="True" />
</td>
</tr>
</table>
    <asp:Button ID="btnSave" runat="server" Text="Save" />
</asp:Content>

