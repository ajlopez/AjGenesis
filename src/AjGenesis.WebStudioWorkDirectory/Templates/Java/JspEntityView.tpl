<%@page contentType="text/html"%>

<%@ page import="java.util.*" %>

<%@ page import="${Project.PackageName}.services.*" %>
<%@ page import="${Project.PackageName}.model.*" %>

<jsp:include page="/includes/Header.jsp">
<jsp:param name="title" value="${Entity.Descriptor}"/>
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
<#
	for each Property in Entity.Properties where Property.Reference
#>
	${Property.Reference.Name} ${Property.Reference.Name.ToLower()};
<#
	end for
#>
%>
<p>
<table width='98%' cellspacing=1 cellpadding=2 class='form'>
<#
	for each Property in Entity.Properties
#>
<tr>
<td class='legend' align='left'>${Property.Description}</td>
<#		
		if Property.Reference then
#>
<%
		if (${ObjName}.get${Property.Name}()!=0) {
			${Property.Reference.Name.ToLower()} = ${Property.Reference.Name}Services.getById(${ObjName}.get${Property.Name}());
%>
<td class='datum' align='left'>
<a href="${Property.Reference.Name}View.jsp?Id=<%=${ObjName}.get${Property.Name}()%>">
<%= ${Property.Reference.Name.ToLower()}.get${Property.Reference.DescriptorProperty.Name}() %>
</a>
</td>
<%
		} else {
%>
<td class='datum' align='left'>&nbsp;</td>
<%
		}
%>
<#
		else
#>
<td class='datum' align='left'><%= ${ObjName}.get${Property.Name}() %></td>
<#
		end if
#>
</tr>
<#
	end for
#>
</table>
</center>

<#
	nrel = 0
	for each Relation in Entity.Relations where Relation.RelationType="Referenced"
		nrel = nrel+1
#>
<center>
<h2>${Relation.Entity.SetDescriptor}</h2>

<table width='98%' cellspacing=1 cellpadding=2 border=0 class='form'>
<tr>
<#
	for each Property in Relation.Entity.Properties where Property.Reference.Name<>Entity.Name
#>
<td class='title' align='center'>${Property.Description}</td>
<#
	end for
#>
<%
<#
	if nrel=1 then
#>
    List entities;
    Iterator it;

<#
	end if
#>
    entities = ${Relation.Entity.Name}Services.getBy${Entity.Name}(${ObjName}.get${Entity.IdProperty.Name}());
    it = entities.iterator();

    while (it.hasNext()) {
        ${Relation.Entity.Name} entity = (${Relation.Entity.Name}) it.next();
%>
<tr>
<#
		for each Property in Relation.Entity.Properties where Property.Reference.Name<>Entity.Name
			if Property.Name = "Id" then
#>
<td class='datum' align='left'><a href='${Relation.Entity.Name}View.jsp?Id=<%= entity.getId() %>'><%= entity.get${Property.Name}() %></a></td>
<#
			elseif Property.Reference then
#>
<%
				if (entity.get${Property.Name}()!=0) {
					${Property.Reference.Name} ${Property.Reference.Name.ToLower()} = ${Property.Reference.Name}Services.getById(entity.get${Property.Name}());
%>
<td class='datum' align='left'>
<a href="${Property.Reference.Name}View.jsp?Id=<%=entity.get${Property.Name}()%>">
<%= ${Property.Reference.Name.ToLower()}.get${Property.Reference.DescriptorProperty.Name}() %>
</a>
</td>
<%
				} else {
%>
<td class='datum' align='left'>&nbsp;</td>
<%
				}
%>
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


<#
	end for
#>

<jsp:include page="/includes/Footer.jsp"/>

