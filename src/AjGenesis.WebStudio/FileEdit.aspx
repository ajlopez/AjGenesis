<%@ Page Language="VB" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="false" CodeFile="FileEdit.aspx.vb" Inherits="FileEdit" title="Edit File" Theme="Default"  ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<div align="left">
&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkDirectory" runat="server">View Directory</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkView" runat="server">View File</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="lnkHelp" runat="server" NavigateUrl="~/PageView.aspx?Page=FileEditPageHelp" Text="Help" Target="_blank"/>&nbsp;&nbsp;&nbsp;
</div>  
<br />
<br />  
<table class="DataTable" width="400" cellpadding="2" cellspacing="0">
<tr>
<td>
Content
</td>
</tr>
<tr>
<td>
<asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Rows="30" Columns="60" Wrap="False" />
</td>
</tr>
</table>
    <asp:Button ID="btnSave" runat="server" Text="Save" />
</asp:Content>

