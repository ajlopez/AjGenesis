<%@ Page Language="VB" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="false" CodeFile="FileUpload.aspx.vb" Inherits="FileUpload" title="Upload File" Theme="Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<div align="left">
&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkDirectory" runat="server">View Directory</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="lnkHelp" runat="server" NavigateUrl="~/PageView.aspx?Page=FileUploadPageHelp" Text="Help" Target="_blank"/>&nbsp;&nbsp;&nbsp;
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
<td>
File to Upload
</td>
</tr>
<tr>
<td>
<asp:FileUpload ID="fupFile" runat="server" />
</td>
</tr>
</table>
    <asp:Button ID="btnUpload" runat="server" Text="Upload" />
</asp:Content>

