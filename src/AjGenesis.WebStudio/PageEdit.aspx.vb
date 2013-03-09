
Partial Class PageEdit
    Inherits System.Web.UI.Page

    Public Property PageCode() As String
        Get
            Return ViewState("PageCode")
        End Get
        Set(ByVal value As String)
            ViewState("PageCode") = value
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

            txtCode.Text = page.Code
            txtTitle.Text = page.Title
            txtContent.Text = page.Content
        End If
    End Sub

    Protected Sub lnkCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCancel.Click
        Response.Redirect("~/PageView.aspx?Page=" & Server.UrlEncode(PageCode))
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim page As New DynamicPage

        If txtCode.Text > "" And txtTitle.Text > "" Then
            page.Code = txtCode.Text
            page.Title = txtTitle.Text
            page.Content = txtContent.Text

            PageService.SavePage(page)

            Response.Redirect("~/PageView.aspx?Page=" & Server.UrlEncode(page.Code))
        End If
    End Sub
End Class

