<%@ page import="${Project.PackageName}.services.*" %>
<%@ page import="${Project.PackageName}.model.*" %>
<#
	ObjName = Entity.Name.ToLower()
#>
<%
	${Entity.Name} ${ObjName};

	${ObjName} = ${Entity.Name}Services.getById(Integer.parseInt(request.getParameter("Id")));
<#
	for each Property in Entity.Properties where Property.Type<>"Id"
		if Property.Reference then
#>
	${ObjName}.set${Property.Name}(Integer.parseInt(request.getParameter("${Property.Name}")));
<#
		else
#>
	${ObjName}.set${Property.Name}(request.getParameter("${Property.Name}"));
<#
		end if
	end for
#>
	${Entity.Name}Services.update(${ObjName});

	response.sendRedirect("${Entity.Name}View.jsp?Id="+request.getParameter("Id"));
%>