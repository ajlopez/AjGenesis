<#
include "Templates/VbNet2DDDNh/Prologue.tpl"
#>

Imports ${Project.Name}.Application
Imports ${Project.Name}.Domain

Public Partial Class ${WebPage.Prefix}${Entity.Name}DeletePage
    Inherits System.Web.UI.Page

    Public Entity As ${Entity.Name}

    Public Property IdEntity() As Integer
        Get
            Return DirectCast(ViewState("IdEntity"), Integer)
        End Get
        Set(ByVal Value As Integer)
            ViewState("IdEntity") = Value
        End Set
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
		Dim IdEntity as Integer

		IdEntity = CInt(Request("Id"))
		${Entity.Name}Service.Delete(IdEntity)
		Response.Redirect("${Entity.SetName}.aspx")
		Response.End
    End Sub

End Class
