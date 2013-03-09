
Partial Class PageView
    Inherits System.Web.UI.Page

    Public Property PageCode() As String
        Get
            Return ViewState("PageCode")
        End Get
        Set(ByVal value As String)
            ViewState("PageCode") = value
        End Set
    End Property

    Public Property PageTitle() As String
        Get
            Return ViewState("PageTitle")
        End Get
        Set(ByVal value As String)
            ViewState("PageTitle") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not Request("Page") Is Nothing Then
                PageCode = Request("Page")
            ElseIf Not Context.Items("Page") Is Nothing Then
                PageCode = Context.Items("Page")
            Else
                PageCode = "Index"
            End If

            Dim page As DynamicPage = PageService.GetPage(PageCode)

            PageTitle = page.Title
            litContent.Text = PageService.GetContentAsHtml(page.Content)
        End If

        Title = PageTitle
    End Sub

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkEdit.Click
        Response.Redirect("~/PageEdit.aspx?Page=" & Server.UrlEncode(PageCode))
    End Sub
End Class
