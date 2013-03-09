<%
	String title = request.getParameter("title");
	String prefix = request.getParameter("prefix");

	if (title==null)
		title="";
	if (prefix==null)
		prefix="";
%>
<html>
<head>

<title><%= title %></title>

<META name="title" content="<%= title %>">
<link rel="stylesheet" href="styles/style.css">
</head>

<body bgcolor=#ffffff leftmargin=0 topmargin=0 marginwidth=0 marginheight=0>

<table width="100%" class="top" cellspacing=10 cellpadding=0 border=0>
<tr height=60>
<td class="sitetitle" align="left">
${Project.Name}</td>
<td>
</td>
</tr>
</table>


<table width=100% cellspacing=0 cellpadding=0 border=0>

<tr height=24 bgcolor=black>
<td class="topheader" align=right>
<b>${Project.Description}</b>&nbsp;&nbsp;&nbsp;&nbsp;
</td>
</tr>

</table>

<table width="100%" border="0" cellpadding="0" cellspacing="0">
<tr>
<td width=150 height=500 valign="top" class="left">
<br>

<center>


<p>
<table class="menu" cellspacing=1 cellpadding=2 width="95%">
<tr>
<td align=center class="menutitle">
${Project.Name}</td>
</tr>
<tr>
<td valign="top" class="menuoption">
&nbsp;&nbsp;<strong></strong>&nbsp;&nbsp;<a target='_top' href='<%=prefix%>Home.do' class='menuoption'>Main</a><br>
</td>
</tr>
</table>

<br>
<br>

</p>

<p>
<table class="menu" cellspacing=1 cellpadding=2 width="95%">
<tr>
<td align=center class="menutitle">
Entities</td>
</tr>
<tr>
<td valign="top" class="menuoption">
<#
	for each Entity in Project.Model.Entities
#>
&nbsp;&nbsp;<strong></strong>&nbsp;&nbsp;<a target='_top' href='<%=prefix%>${Entity.Name}List.do' class='menuoption'>${Entity.SetDescriptor}</a><br>
<#
	end for
#>
</td>
</tr>
</table>

<br>
<br>
</p>

</td>			

<td valign="top">

<table width=100% cellspacing=10 border=0 cellpadding=0>
<tr>
<td>

<p>
<h1 align="center"><%= title %></h1>

