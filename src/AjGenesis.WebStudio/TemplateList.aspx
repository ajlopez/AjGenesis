<%@ Page Language="VB" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="false" CodeFile="TemplateList.aspx.vb" Inherits="TemplateList" title="Templates" Theme="Default" %>
<%@ Register Src="Controls/Directory.ascx" TagName="Directory" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<div align="left">
&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkNewSubdirectory" runat="server">New Subdirectory</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkNewFile" runat="server">New File</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkUploadFile" runat="server">Upload File</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkDownload" runat="server">Download Directory</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkUploadZip" runat="server">Upload Zip File</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="lnkHelp" runat="server" NavigateUrl="~/PageView.aspx?Page=TemplateListPageHelp" Text="Help" Target="_blank"/>&nbsp;&nbsp;&nbsp;
    </div>
    <br />
    <br />
    <uc1:Directory ID="Directory1" runat="server" />
</asp:Content>

