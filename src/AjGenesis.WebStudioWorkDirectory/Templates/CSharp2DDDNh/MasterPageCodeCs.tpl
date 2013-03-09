using System;

public partial class MasterPages_MainMasterPage : System.Web.UI.MasterPage
{
    protected void Page_PreRender(object sender, EventArgs e)
    {
		Title1.TitleText = Page.Title;
    }
}

