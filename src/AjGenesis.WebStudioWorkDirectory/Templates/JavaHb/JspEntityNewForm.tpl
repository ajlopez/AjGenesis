<%@page contentType="text/html"%>

<%@ page import="java.util.*" %>

<%@ page import="${Project.PackageName}.services.*" %>
<%@ page import="${Project.PackageName}.model.*" %>

<jsp:include page="/includes/Header.jsp">
<jsp:param name="title" value="New ${Entity.Descriptor}"/>
</jsp:include>

<center>

<p>
<a href="${Entity.Name}List.jsp">${Entity.SetDescriptor}</a>
</p>

<p>
<form action="${Entity.Name}Insert.jsp" method="post">
<table width='98%' cellspacing=1 cellpadding=2 class='form'>
<#
	for each Property in Entity.Properties where Property.Type<>"Id"
#>
<tr>
<td class='legend' align='left'>${Property.Description}</td>
<td class='datum' align='left'><input type="text" size="30" name="${Property.Name}"></td>
</tr>
<#
	end for
#>
</table>
<input type="submit" value="Accept">
</form>
</center>

<jsp:include page="/includes/Footer.jsp"/>

