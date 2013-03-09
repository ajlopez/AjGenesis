<%@ Page Language="VB" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="false" CodeFile="FileView.aspx.vb" Inherits="FileView" title="File View" theme="Default" %>

<%@ Register Src="Controls/FileContent.ascx" TagName="FileContent" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<div align="left">
&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkDirectory" runat="server">View Directory</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkEdit" runat="server">Edit File</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkCopy" runat="server">Copy File</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkMove" runat="server">Move File</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkDelete" runat="server">Delete File</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="lnkHelp" runat="server" NavigateUrl="~/PageView.aspx?Page=FileViewPageHelp" Text="Help" Target="_blank"/>&nbsp;&nbsp;&nbsp;
</div>  
<br />
<br />  
<p align="left">
&nbsp;&nbsp;&nbsp;
    File Name <asp:Label ID="lblFileName" runat="server" />
    <br />
    <br />
</p>   
    <asp:Panel ID="Panel2" runat="server" Width="90%">
        <uc1:FileContent ID="FileContent1" runat="server" />
    </asp:Panel>
</asp:Content>

