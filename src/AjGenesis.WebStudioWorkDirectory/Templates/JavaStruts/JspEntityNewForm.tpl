<%@page contentType="text/html"%>

<%@taglib uri="http://jakarta.apache.org/struts/tags-html" prefix="html" %>
<%@taglib uri="http://jakarta.apache.org/struts/tags-bean" prefix="bean" %>

<jsp:include page="/WEB-INF/jsp/includes/Header.jsp">
<jsp:param name="title" value="New ${Entity.Descriptor}"/>
</jsp:include>

<center>

<p>
<a href="${Entity.Name}List.do">${Entity.SetDescriptor}</a>
</p>

<p>
<html:form action="/${Entity.Name}Insert" method="post">
<table width='98%' cellspacing=1 cellpadding=2 class='form'>
<#
	for each Property in Entity.Properties where Property.Type<>"Id"
#>
<tr>
<td class='legend' align='left'>${Property.Description}</td>
<td class='datum' align='left'>
<#
		if Property.Reference then
#>
<select name='${Property.Name}'>
<%
	iterator = ${Property.Reference.SetName.ToLower()}.iterator();

	while (iterator.hasNext()) {
		${Property.Reference.Name} ${Property.Reference.Name.ToLower()} = (${Property.Reference.Name}) iterator.next();
%>
<option value="<%= ${Property.Reference.Name.ToLower()}.get${Property.Reference.IdProperty.Name}() %>">
<%= ${Property.Reference.Name.ToLower()}.get${Property.Reference.DescriptorProperty.Name}() %>
</option>
<%
	}
%>
</select>
<#
		else
#>
<html:text size="30" property="${Property.JavaName}"/>
<#
		end if
#>
</td>
</tr>
<#
	end for
#>
</table>
<html:submit value="Accept"/>
</html:form>
</center>

<jsp:include page="/WEB-INF/jsp/includes/Footer.jsp"/>

