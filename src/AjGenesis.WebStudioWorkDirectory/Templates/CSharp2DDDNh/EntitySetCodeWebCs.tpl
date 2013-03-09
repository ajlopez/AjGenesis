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

public partial class ${WebPage.Prefix}${Entity.SetName}Page : System.Web.UI.Page
{
	protected void Page_Load(object sender, System.EventArgs e)
	{
		// Put user code to initialize the page here
       if (!IsPostBack) {
            gvwData.DataSource = ${Entity.Name}Service.Get${Entity.SetName}();
            gvwData.DataBind();
		}
	}
}

