<%@ Page Language="VB" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="false" CodeFile="PageView.aspx.vb" Inherits="PageView" title="Page" Theme="Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<div align="left">
&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkEdit" runat="server">Edit this page</asp:LinkButton>&nbsp;&nbsp;&nbsp;
</div>  
<br />
<br />
<div align="left" class="Page">
<asp:Literal ID="litContent" runat="server" />
</div>
</asp:Content>

