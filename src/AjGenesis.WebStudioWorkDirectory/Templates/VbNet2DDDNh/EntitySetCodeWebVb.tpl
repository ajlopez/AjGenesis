<#
include "Templates/VbNet2DDDNh/Prologue.tpl"
#>

Imports ${Project.Name}.Application
Imports ${Project.Name}.Domain

Public Partial Class ${WebPage.Prefix}${Entity.SetName}Page
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            gvwData.DataSource = ${Entity.Name}Service.Get${Entity.SetName}()
            gvwData.DataBind()
        End If
    End Sub

End Class
