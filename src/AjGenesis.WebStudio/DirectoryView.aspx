<%@ Page Language="VB" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="false" CodeFile="DirectoryView.aspx.vb" Inherits="DirectoryView" title="Directory" Theme="Default" %>

<%@ Register Src="Controls/Directory.ascx" TagName="Directory" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<div align="left">
&nbsp;&nbsp;&nbsp;
    <span ID="spanNoWorkingDirectory" runat="server" visible="true">
    <asp:LinkButton ID="lnkParent" runat="server">View Parent Directory</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    </span>
    <span ID="spanWorkingDirectory" runat="server" visible="false">
    <asp:LinkButton ID="lnkChange" runat="server">Change Working Directory</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    </span>
    <asp:LinkButton ID="lnkNewSubdirectory" runat="server">New Subdirectory</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkNewFile" runat="server">New File</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkUploadFile" runat="server">Upload File</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkUploadZip" runat="server">Upload Zip File</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="lnkHelp" runat="server" NavigateUrl="~/PageView.aspx?Page=DirectoryViewPageHelp" Text="Help" Target="_blank"/>&nbsp;&nbsp;&nbsp;
    <br />
    <br />
&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkDownload" runat="server">Download Directory</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkCopy" runat="server">Copy Directory</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkMove" runat="server">Move Directory</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkCopyContent" runat="server">Copy Content</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkMoveContent" runat="server">Move Content</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkDelete" runat="server">Delete Directory</asp:LinkButton>&nbsp;&nbsp;&nbsp;
</div>  
<br />
<br />  
<p align="left">
&nbsp;&nbsp;&nbsp;
    Path <asp:Label ID="lblFullPath" runat="server" />
    <br />
    <br />
</p>   
    <uc1:Directory ID="Directory1" runat="server" />
</asp:Content>

