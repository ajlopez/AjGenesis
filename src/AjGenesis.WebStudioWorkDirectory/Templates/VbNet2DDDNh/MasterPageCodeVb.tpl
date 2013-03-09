<#
include "Templates/VbNet2/Prologue.tpl"
#>

Partial Class MasterPages_MainMasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Title1.Title = Page.Title
    End Sub
End Class


