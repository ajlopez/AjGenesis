<%@ Page Language="VB" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="false" CodeFile="ProjectNew.aspx.vb" Inherits="ProjectNew" title="New Project" Theme="Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<div align="left">
&nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="lnkProjects" runat="server" NavigateUrl="~/ProjectList.aspx" Text="Projects"/>&nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="lnkHelp" runat="server" NavigateUrl="~/PageView.aspx?Page=ProjectNewPageHelp" Text="Help" Target="_blank"/>&nbsp;&nbsp;&nbsp;
</div>  
<br />
<br />  
<table class="DataTable" cellspacing="0" cellpadding="2">
<tr>
<td>
Project Name
</td>
<td>
<asp:TextBox ID="txtName" runat="server" />
</td>
</tr>
<tr>
<td>
Description
</td>
<td>
<asp:TextBox ID="txtDescription" runat="server" />
</td>
</tr>
</table>
<asp:Button ID="btnCreate" Text="Create Project" runat="server"/>
    <asp:Panel ID="pnlOutput" runat="server" Visible="False" Width="80%">
    <br />
    <br />
    <div align="left">
    <xmp><asp:Literal ID="litOutput" runat='server'></asp:Literal></xmp>
    </div>
    </asp:Panel>

</asp:Content>

