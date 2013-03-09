
'
' File generated using AjGenesis
' http://www.ajlopez.com/ajgenesis
' http://www.ajlopez.net/ajgenesis
' Open Source Code Generation Engine
'


Public MustInherit Class Controls_Header
    Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Request.IsAuthenticated = True Then


        End If
    End Sub

End Class
