<%@page contentType="text/html"%>

<%@ page import="java.util.*" %>

<%@ page import="${Project.PackageName}.services.*" %>
<%@ page import="${Project.PackageName}.model.*" %>

<jsp:include page="/includes/Header.jsp">
<jsp:param name="title" value="${Entity.SetDescriptor}"/>
</jsp:include>

<center>

<p>
<a href="${Entity.Name}List.jsp">${Entity.SetDescriptor}</a>
&nbsp;
&nbsp;
<a href="${Entity.Name}Form.jsp?Id=<%=request.getParameter("Id")%>">Update</a>
</p>
<#
	ObjName = Entity.Name.ToLower()
#>
<%
	${Entity.Name} ${ObjName};

	${ObjName} = ${Entity.Name}Services.getById(Integer.parseInt(request.getParameter("Id")));
%>
<p>
<table width='98%' cellspacing=1 cellpadding=2 class='form'>
<#
	for each Property in Entity.Properties
#>
<tr>
<td class='legend' align='left'>${Property.Description}</td>
<td class='datum' align='left'><%= ${ObjName}.get${Property.Name}() %></td>
</tr>
<#
	end for
#>
</table>
</center>

<jsp:include page="/includes/Footer.jsp"/>

