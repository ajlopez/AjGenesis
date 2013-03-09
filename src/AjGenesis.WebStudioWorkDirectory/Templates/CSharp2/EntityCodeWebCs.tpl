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

public partial class ${WebPage.Prefix}${Entity.Name}Page : System.Web.UI.Page
{
	public ${Entity.Name} Entity;

	public int IdEntity {
		get {
			return (int) ViewState["IdEntity"];
		}
		set {
			ViewState["IdEntity"] = value;
		}
	}

<#
	for each Property in Entity.Properties where Property.Reference
#>
	public ${Property.Reference.Name} Entity${Property.Reference.Name};
<#
	end for
#>

<#
	for each Property in Entity.Properties where Property.Enumeration
#>
	public string ${Property.Name}Description {
		get {
			return Enumerations.Translate(Enumerations.${Property.Enumeration.Name}List, Entity.${Property.Name});
		}
	}

<#
	end for
#>

	protected void Page_Load(object sender, System.EventArgs e)
	{
		// Put user code to initialize the page here
		if (!IsPostBack) {
			IdEntity = Convert.ToInt32(Request["Id"]);
			Entity = ${Entity.Name}Service.GetById(IdEntity);
<#
	for each Property in Entity.Properties where Property.Reference
#>
			Entity${Property.Reference.Name} = ${Property.Reference.Name}Service.GetById(Entity.${Property.Name});
<#
	end for

	for each Relation in Entity.Relations where Relation.RelationType="Referenced"
#>
			dtgData${Relation.Entity.SetName}.DataSource = ${Relation.Entity.Name}Service.GetBy${Entity.Name}Ex(IdEntity);
<#
	end for
#>
           	DataBind();
		}
	}
}

