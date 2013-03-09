<%@ Page Language="VB" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="false" CodeFile="DirectoryNew.aspx.vb" Inherits="DirectoryNew" title="New Subdirectory" Theme="Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<div align="left">
&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkDirectory" runat="server">View Parent Directory</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="lnkHelp" runat="server" NavigateUrl="~/PageView.aspx?Page=DirectoryNewPageHelp" Text="Help" Target="_blank"/>&nbsp;&nbsp;&nbsp;
    </div>
    <br />
    <br />
<p align="left">
&nbsp;&nbsp;&nbsp;
    Parent Directory Path: <asp:Label ID="lblParent" runat="server" />
    <br />
    <br />
</p>   
<table cellspacing="0" cellpadding="2" width="400" class="DataTable">
<tr>
<td>
New Directory Name
</td>
<td>
<asp:TextBox ID="txtDirectory" runat="server" />
</td>
</tr>
</table>
<asp:Button ID="btnCreate" runat="server" Text="Create Directory" />
</asp:Content>

