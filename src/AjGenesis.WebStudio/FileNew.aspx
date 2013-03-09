<%@ Page Language="VB" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="false" CodeFile="FileNew.aspx.vb" Inherits="FileNew" title="New File" Theme="Default" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<div align="left">
&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkDirectory" runat="server">View Directory</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="lnkHelp" runat="server" NavigateUrl="~/PageView.aspx?Page=FileNewPageHelp" Text="Help" Target="_blank"/>&nbsp;&nbsp;&nbsp;
</div>  
<br />
<br />  
<p align="left">
&nbsp;&nbsp;&nbsp;
    Directory Path: <asp:Label ID="lblFullPath" runat="server" />
    <br />
    <br />
</p>   
<table class="DataTable" width="400" cellpadding="2" cellspacing="0">
<tr>
<td>File Name</td>
<td>
<asp:TextBox ID="txtFileName" runat="server" />
</td>
</tr>
<tr>
<td colspan="2">
Content
</td>
</tr>
<tr>
<td colspan="2">
<asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Rows="30" Columns="60" Wrap="False" />
</td>
</tr>
</table>
    <asp:Button ID="btnCreate" runat="server" Text="Create File" />
</asp:Content>

