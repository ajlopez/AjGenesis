
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Context.Items("Page") = "MainPage"
            Server.Transfer("~/PageView.aspx")
        End If
    End Sub
End Class
