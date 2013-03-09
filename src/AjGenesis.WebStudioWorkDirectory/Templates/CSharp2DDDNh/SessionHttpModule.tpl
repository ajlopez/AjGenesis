Imports System.Web

Public Class SessionHttpModule
    Implements IHttpModule

    Public Sub New()

    End Sub

    Public Sub Dispose() Implements System.Web.IHttpModule.Dispose

    End Sub

    Public Sub Init(ByVal context As System.Web.HttpApplication) Implements System.Web.IHttpModule.Init
        AddHandler context.BeginRequest, AddressOf BeginRequest
        AddHandler context.EndRequest, AddressOf EndRequest
    End Sub

    Private Sub BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        Repository.Current.OpenSession()
    End Sub

    Private Sub EndRequest(ByVal sender As Object, ByVal e As EventArgs)
        Repository.Current.FlushSession()
        Repository.Current.CloseSession()
    End Sub
End Class
