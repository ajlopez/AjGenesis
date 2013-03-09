<#
include "Templates/CSharp2/Prologue.tpl"
#>

using System;

using ${Project.Name}.Application;
using ${Project.Name}.Domain;

public partial class ${WebPage.Prefix}${Entity.Name}DeletePage : System.Web.UI.Page {

	public ${Entity.Name} Entity;

	public int IdEntity {
		get {
			return (int) ViewState["IdEntity"];
		}
		set {
			ViewState["IdEntity"] = value;
		}
	}

	protected void Page_Load(object sender, System.EventArgs e)
	{
		// Put user code to initialize the page here
		if (!IsPostBack) {
			IdEntity = Convert.ToInt32(Request["Id"]);
			${Entity.Name}Service.Delete${Entity.Name}(IdEntity);
			Response.Redirect("${Entity.SetName}.aspx");
			Response.End();
		}
	}
}
