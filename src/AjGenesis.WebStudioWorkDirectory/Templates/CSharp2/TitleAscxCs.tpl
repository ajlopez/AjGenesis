<#
include "Templates/CSharp2/Prologue.tpl"	
#>

using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
///		Summary description for Header.
/// </summary>
public partial class Controls_Title : System.Web.UI.UserControl
{
	public string TitleText {
		get {
			return PageTitle.Text;
		}
		set {
			PageTitle.Text = value;
		}
	}

	private void Page_Load(object sender, System.EventArgs e)
	{
		// Put user code to initialize the page here
		DataBind();
	}
}

