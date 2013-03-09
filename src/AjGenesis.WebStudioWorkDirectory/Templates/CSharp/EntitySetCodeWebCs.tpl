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

namespace ${Project.Name}.WebClient
{
	public class ${Entity.SetName}Page : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid dtgData;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
        if (!IsPostBack) {
<#
	if Entity.HasReferences then
#>
            dtgData.DataSource = ${Entity.Name}Service.GetAllEx();
<#
	else
#>
            dtgData.DataSource = ${Entity.Name}Service.GetList();
<#
	end if
#>
            dtgData.DataBind();
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}

