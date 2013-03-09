
/*
 *	Project ${Project.Name}
 *		${Project.Description}
 *	Action Form for Entity ${Entity.Name}
 *		${Entity.Description}
 *	
 */

package ${Project.PackageName}.struts.actions;

import java.util.List;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.struts.action.*;

import ${Project.PackageName}.services.*;

public class ${Entity.Name}ListAction extends Action {

	public ActionForward execute(ActionMapping mapping, ActionForm form, HttpServletRequest request, HttpServletResponse response) throws Exception {
		DataServices.setDataSource(getDataSource(request));
		List ${Entity.JavaSetName} = ${Entity.Name}Services.getAll();
		request.setAttribute("${Entity.JavaSetName}",${Entity.JavaSetName});
		return mapping.findForward("success");
	}
}
