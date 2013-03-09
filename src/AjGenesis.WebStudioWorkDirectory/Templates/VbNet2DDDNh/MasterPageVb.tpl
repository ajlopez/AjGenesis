<%@ Master Language="VB" CodeFile="MainMasterPage.master.vb" Inherits="MasterPages_MainMasterPage" %>
<%@ Register Src="../Controls/Title.ascx" TagName="Title" TagPrefix="uc3" %>
<%@ Register Src="../Controls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="../Controls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:Header ID="Header1" runat="server" />

<br />

<table cellpadding="10" width="100%">
	<tr>
		<td>
<div align="center">
        <uc3:Title ID="Title1" runat="server" />
        <asp:contentplaceholder id="MainContentPlaceHolder" runat="server">
        </asp:contentplaceholder>
</div>
</td> </tr> 
</table>
<br />
<br />
        <uc2:Footer ID="Footer1" runat="server" />
    </div>
    </form>
</body>
</html>
