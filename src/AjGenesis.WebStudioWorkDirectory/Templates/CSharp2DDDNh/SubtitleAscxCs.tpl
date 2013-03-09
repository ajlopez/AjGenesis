using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Controls_Subtitle : System.Web.UI.UserControl
{
	public string TitleText {
		get {
			return PageSubtitle.Text;
		}
		set {
			PageSubtitle.Text = value;
		}
	}

	private void Page_Load(object sender, System.EventArgs e)
	{
		// Put user code to initialize the page here
		DataBind();
	}
}
