<#
include "Templates/CSharp2/Prologue.tpl"	
#>

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using ${Project.Name}.Services;
using ${Project.Name}.Entities;

public partial class ${WebPage.Prefix}${Entity.SetName}Page : System.Web.UI.Page
{
	protected void Page_Load(object sender, System.EventArgs e)
	{
		// Put user code to initialize the page here
       if (!IsPostBack) {
<#
	if Entity.HasReferences then
#>
           gvwData.DataSource = ${Entity.Name}Service.GetAllEx();
<#
	else
#>
           gvwData.DataSource = ${Entity.Name}Service.GetList();
<#
	end if
#>
           gvwData.DataBind();
		}
	}
}

