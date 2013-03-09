<%@page contentType="text/html"%>

<%@ page import="java.util.*" %>

<%@ page import="${Project.PackageName}.services.*" %>
<%@ page import="${Project.PackageName}.model.*" %>

<jsp:include page="/includes/Header.jsp">
<jsp:param name="title" value="Update ${Entity.Descriptor}"/>
</jsp:include>

<center>

<p>
<a href="${Entity.Name}List.jsp">${Entity.SetDescriptor}</a>
&nbsp;&nbsp;
<a href="${Entity.Name}View.jsp">${Entity.Descriptor}</a>
</p>
<#
	ObjName = Entity.Name.ToLower()
#>
<%
	${Entity.Name} ${ObjName};

	${ObjName} = ${Entity.Name}Services.getById(Integer.parseInt(request.getParameter("Id")));
%>
<p>
<form action="${Entity.Name}Update.jsp" method="post">
<table width='98%' cellspacing=1 cellpadding=2 class='form'>
<tr>
<td class='legend' align='left'>Id</td>
<td class='datum' align='left'><%= ${ObjName}.getId() %></td>
</tr>
<#
	for each Property in Entity.Properties where Property.Type<>"Id"
#>
<tr>
<td class='legend' align='left'>${Property.Description}</td>
<td class='datum' align='left'><input type="text" size="30" name="${Property.Name}" value="<%= ${ObjName}.get${Property.Name}() %>"></td>
</tr>
<#
	end for
#>
</table>
<input type="submit" value="Accept">
<input type="hidden" name="Id" value="<%= ${ObjName}.getId() %>">
</form>
</center>

<jsp:include page="/includes/Footer.jsp"/>

