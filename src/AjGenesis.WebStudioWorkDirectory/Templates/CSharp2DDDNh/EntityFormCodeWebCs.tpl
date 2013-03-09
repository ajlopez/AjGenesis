<#

message	"Generating Form Code Behind for Entity ${Entity.Name}..."

include "Templates/CSharp2DDDNh/CSharpFunctions.tpl"
include "Templates/EntityFunctions.tpl"

Properties = GetAllProperties(Entity)
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

using ${Project.Name}.Application;
using ${Project.Name}.Domain;

/// <summary>
/// Summary description for _Default.
/// </summary>
public partial class ${WebPage.Prefix}${Entity.Name}UpdatePage : System.Web.UI.Page
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
	for each Property in Properties where Property.Reference
#>
		public IList ${Property.Reference.SetName};
<#
	end for
#>

<#
	for each Property in Properties where Property.Enumeration
#>
	public IList ${Property.Enumeration.Name}List {
		get {
			return Enumerations.${Property.Enumeration.Name}List;
		}
	}
<#
	end for
#>

	protected void Page_Load(object sender, System.EventArgs e)
	{
		// Put user code to initialize the page here
		if (!IsPostBack) {
			if (Request["Id"]==null) {
				IdEntity = 0;
				Entity = new ${Entity.Name}();
			}
			else {
				IdEntity = Convert.ToInt32(Request["Id"]);
				Entity = ${Entity.Name}Service.Get${Entity.Name}ById(IdEntity);
			}
<#
	for each Property in Properties where Property.Reference
#>
			${Property.Reference.SetName} = Get${Property.Reference.SetName}();
<#
	end for
#>
           	DataBind();

			if (IdEntity>0) {
<#
	for each Property in Properties where Property.Reference
#>
				ddl${Property.Reference.SetName}.SelectedValue = Entity.${Property.Reference.Name}.${Property.Reference.IdProperty.Name}.ToString();
<#
	end for
#>
<#
	for each Property in Properties where Property.Enumeration
#>
				ddl${Property.Name}.SelectedValue = Entity.${Property.Name}.ToString();
<#
	end for
#>
			}
			else {
<#
	for each Property in Properties where Property.Reference
#>
				if (Request["${Property.Name}"]!=null)
					ddl${Property.Reference.SetName}.SelectedValue = Request["${Property.Name}"];
<#
	end for
#>
			}
		}
	}

	private bool FormValidate() {
		return true;
	}

<#
	for each Property in Properties where Property.Reference
#>
	private IList Get${Property.Reference.SetName}() {
		return ${Property.Reference.Name}Service.Get${Property.Reference.SetName}();
	}

<#
	end for
#>

	private void Update() {		
		${Entity.Name}Info entity = new ${Entity.Name}Info();

<#
for each Property in Properties where Property.Type<>"Id" and Property.SqlType
	if Property.Reference then
#>
      entity.${Property.Name} = Convert.ToInt32(ddl${Property.Reference.SetName}.SelectedValue);
<#
	else
		if Property.Enumeration then
#>
		entity.${Property.Name} = Convert.ToInt32(ddl${Property.Name}.SelectedValue);
<#
		else
		if Property.Type="Integer" then
#>
		entity.${Property.Name} = Convert.ToInt32(txt${Property.Name}.Text);
<#
		else
		if Property.Type="Date" or Property.Type="DateTime" then
#>        
		entity.${Property.Name} = Convert.ToDateTime(txt${Property.Name}.Text);
<#
		else
		if Property.Type="Boolean" then
#>        
		entity.${Property.Name} = Convert.ToBoolean(txt${Property.Name}.Text);
<#
		else
#>        
		entity.${Property.Name} = txt${Property.Name}.Text;
<#
		end if
		end if
		end if
		end if
	end if
end for
#>        

     	if (IdEntity == 0)
			${Entity.Name}Service.New${Entity.Name}(entity);
		else
			${Entity.Name}Service.Update${Entity.Name}(entity);
	}

   protected void btnAccept_Click(object sender,EventArgs e) {
		if (!IsValid)
			return;

		try {
			if (FormValidate()) {
				Update();
				if (IdEntity==0)
	            Server.Transfer("${Entity.SetName}.aspx");
				else
					Server.Transfer("${Entity.Name}.aspx?Id=" + IdEntity);
			}
		}
      catch (Exception ex) {
         lblMensaje.Visible = true;
         lblMensaje.Text = ex.Message;
		}
	}
}

