<%@page contentType="text/html"%>

<%@taglib uri="http://jakarta.apache.org/struts/tags-logic" prefix="logic" %>
<%@taglib uri="http://jakarta.apache.org/struts/tags-bean" prefix="bean" %>

<%@ page import="java.util.*" %>

<%@ page import="${Project.PackageName}.services.*" %>
<%@ page import="${Project.PackageName}.model.*" %>

<jsp:include page="/WEB-INF/jsp/includes/Header.jsp">
<jsp:param name="title" value="${Entity.SetDescriptor}"/>
</jsp:include>

<center>

<p>
<a href="${Entity.Name}NewForm.do">New ${Entity.Descriptor}...</a>
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
</tr>
<logic:iterate id="${Entity.JavaObjectName}" name="${Entity.JavaSetName}">
<tr class="datum">
<#
	for each Property in Entity.Properties
#>
<td align="left">
<#
		if Property.Type = "Id" then
#>
<a href="${Entity.Name}View.do?Id=<bean:write name="${Entity.JavaObjectName}" property="${Property.JavaName}"/>">
<bean:write name="${Entity.JavaObjectName}" property="${Property.JavaName}"/>
</a>
<#
		else
#>
<bean:write name="${Entity.JavaObjectName}" property="${Property.JavaName}"/>
<#
		end if
#>
</td>
<#
	end for
#>
</tr>
</logic:iterate>

</table>

</center>


<jsp:include page="/WEB-INF/jsp/includes/Footer.jsp"/>

