<%@page contentType="text/html"%>

<%@ page import="java.util.*" %>

<%@ page import="${Project.PackageName}.services.*" %>
<%@ page import="${Project.PackageName}.model.*" %>

<jsp:include page="/includes/Header.jsp">
<jsp:param name="title" value="${Entity.SetDescriptor}"/>
</jsp:include>

<center>

<p>
<a href="${Entity.Name}NewForm.jsp">New ${Entity.Descriptor}...</a>
<p>

<table width='98%' cellspacing=1 cellpadding=2 border=0 class='form'>
<tr>
<#
	for each Property in Entity.Properties
#>
<td class='title' align='center'>${Property.Description}</td>
<#
	end for
#>
<%
    List entities;
    Iterator it;

    entities = ${Entity.Name}Services.getAll();
    it = entities.iterator();

    while (it.hasNext()) {
        ${Entity.Name} entity = (${Entity.Name}) it.next();
%>
<tr>
<#
		for each Property in Entity.Properties
			if Property.Name = "Id" then
#>
<td class='datum' align='left'><a href='${Entity.Name}View.jsp?Id=<%= entity.getId() %>'><%= entity.get${Property.Name}() %></a></td>
<#
			else
#>
<td class='datum' align='left'><%= entity.get${Property.Name}() %></td>
<#
			end if
	end for
#>
</tr>
<%
	}
%>

</table>

</center>


<jsp:include page="/includes/Footer.jsp"/>

