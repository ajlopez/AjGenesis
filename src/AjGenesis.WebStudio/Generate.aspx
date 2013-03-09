<%@ Page Language="VB" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="false" CodeFile="Generate.aspx.vb" Theme="Default" Inherits="Generate" title="Generate Code" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<div align="left">
&nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="lnkHelp" runat="server" NavigateUrl="~/PageView.aspx?Page=GeneratePageHelp" Text="Help" Target="_blank"/>&nbsp;&nbsp;&nbsp;
</div>  
<br />
<br />  
<table cellspacing="0" cellpadding="2" class="DataTable" width="400">
<tr>
<td>Project</td>
<td>
    <asp:DropDownList ID="ddlProjects" runat="server" AutoPostBack="True">
    </asp:DropDownList>
</td>
</tr>
<tr>
<td>Technology</td>
<td>
    <asp:DropDownList ID="ddlTechnologies" runat="server" Enabled="False">
    </asp:DropDownList>
</td>
</tr>
</table>
    <asp:Button ID="btnGenerate" runat="server" Text="Generate" /><br />
    <asp:Panel ID="pnlOutput" runat="server" Visible="False" Width="80%">
    <br />
    <br />
    <div align="left">
    <xmp><asp:Literal ID="litOutput" runat='server'></asp:Literal></xmp>
    </div>
    </asp:Panel>
</asp:Content>

