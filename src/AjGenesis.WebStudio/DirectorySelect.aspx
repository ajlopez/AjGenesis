<%@ Page Language="VB" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="false" CodeFile="DirectorySelect.aspx.vb" Inherits="DirectorySelect" title="Select Target Directory" Theme="Default" %>

<%@ Register Src="Controls/DirectoryTree.ascx" TagName="DirectoryTree" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<div align="left">
&nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="lnkHelp" runat="server" NavigateUrl="~/PageView.aspx?Page=DirectorySelectPageHelp" Text="Help" Target="_blank"/>&nbsp;&nbsp;&nbsp;
    </div>
    <br />
    <br />
<p align="left">
&nbsp;&nbsp;&nbsp;
    <asp:Label ID="lblSource" runat="server" />
    <br />
    <br />
</p>
<p id="parEntry" runat="server" visible="false" align="left">
<asp:Label ID="lblEntry" runat="server" />
<asp:TextBox ID="txtName" runat="server" />
</p>   
<p align="left">
<asp:Label ID="lblTarget" runat="server" />:
</p>
    <uc1:DirectoryTree ID="DirectoryTree1" runat="server" />
</asp:Content>

