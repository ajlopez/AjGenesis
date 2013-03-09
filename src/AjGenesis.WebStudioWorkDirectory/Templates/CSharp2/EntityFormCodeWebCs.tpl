<#

message	"Generating Form Code Behind for Entity ${Entity.Name}..."

include "Templates/CSharp2/CSharpFunctions.tpl"
include "Templates/EntityFunctions.tpl"

EntitySqlProperties	= SqlProperties(Entity)
EntityNoIdSqlProperties	= SqlNoIdProperties(Entity)
EntityIdProperty = IdProperty(Entity)

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
	for each Property in Entity.Properties where Property.Reference
#>
		public DataView ${Property.Reference.SetName};
<#
	end for
#>

<#
	for each Property in Entity.Properties where Property.Enumeration
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
				Entity = ${Entity.Name}Service.GetById(IdEntity);
			}
<#
	for each Property in Entity.Properties where Property.Reference
#>
			${Property.Reference.SetName} = Get${Property.Reference.SetName}();
<#
	end for
#>
           	DataBind();

			if (IdEntity>0) {
<#
	for each Property in Entity.Properties where Property.Reference
#>
				ddl${Property.Reference.SetName}.SelectedValue = Entity.${Property.Name}.ToString();
<#
	end for
#>
<#
	for each Property in Entity.Properties where Property.Enumeration
#>
				ddl${Property.Name}.SelectedValue = Entity.${Property.Name}.ToString();
<#
	end for
#>
			}
			else {
<#
	for each Property in Entity.Properties where Property.Reference
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
	for each Property in Entity.Properties where Property.Reference
#>
	private DataView Get${Property.Reference.SetName}() {
		DataSet ds;

		ds = ${Property.Reference.Name}Service.GetList();

      DataRow dr;

      dr = ds.Tables[0].NewRow();
<#
		if not Property.Required then
#>
      dr["Id"] = 0;
      dr["${Property.Reference.DescriptorProperty.Name}"] = "";
      ds.Tables[0].Rows.Add(dr);

<#
		end if
#>
		DataView dw = new DataView(ds.Tables[0]);
   	dw.Sort = "${Property.Reference.DescriptorProperty.Name}";

		return dw;
	}

<#
	end for
#>

	private void Update() {
		if (IdEntity>0)
			Entity = ${Entity.Name}Service.GetById(IdEntity);
		else
			Entity = new ${Entity.Name}();

<#
for each Property in EntityNoIdSqlProperties
	if Property.Reference then
#>
      Entity.${Property.Name} = Convert.ToInt32(ddl${Property.Reference.SetName}.SelectedValue);
<#
	else
		if Property.Enumeration then
#>
		Entity.${Property.Name} = Convert.ToInt32(ddl${Property.Name}.SelectedValue);
<#
		else
		if Property.Type="Integer" then
#>
		Entity.${Property.Name} = Convert.ToInt32(txt${Property.Name}.Text);
<#
		else
		if Property.Type="Date" then
#>        
		Entity.${Property.Name} = Convert.ToDateTime(txt${Property.Name}.Text);
<#
		else
#>        
		Entity.${Property.Name} = txt${Property.Name}.Text;
<#
		end if
		end if
		end if
	end if
end for
#>        

     	if (IdEntity == 0)
			${Entity.Name}Service.Insert(Entity);
		else
			${Entity.Name}Service.Update(Entity);
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

