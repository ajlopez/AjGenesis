<%@ page import="${Project.PackageName}.services.*" %>
<%@ page import="${Project.PackageName}.model.*" %>
<#
	ObjName = Entity.Name.ToLower()
#>
<%
	${Entity.Name} ${ObjName} = new ${Entity.Name}();
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
	${Entity.Name}Services.insert(${ObjName});

	response.sendRedirect("${Entity.Name}List.jsp");
%>
