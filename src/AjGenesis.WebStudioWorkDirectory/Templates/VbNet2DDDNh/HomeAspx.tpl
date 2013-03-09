<%@ Page Language="VB" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="${WebPage.Prefix}DefaultPage" title="${Project.Name}" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

<p>
${Project.Description}
</p>
<p>
Created using <a href='http://www.ajlopez.com/ajgenesis'>AjGenesis</a>, open source code generation engine.
</p>

</asp:Content>
