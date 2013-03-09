<#

message	"Generating Web Services for Entity ${Entity.Name}..."

include "Templates/VbNet/VbFunctions.tpl"
include "Templates/EntityFunctions.tpl"

EntitySqlProperties	= SqlProperties(Entity)
EntityNoIdSqlProperties	= SqlNoIdProperties(Entity)
EntityIdProperty = IdProperty(Entity)

#>

'
'	Project ${Project.Name}
'		${Project.Description}
'	Entity	${Entity.Name}
'		${Entity.Description}
'	
'

Imports ${Project.Name}.Entities
Imports ${Project.Name}.Services

Imports System.Web.Services

<WebService(Namespace := "http://tempuri.org/")> _
Public Class ${Entity.Name}WebService
    Inherits System.Web.Services.WebService

#Region " Web Services Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Web Services Designer.
        InitializeComponent()

        'Add your own initialization code after the InitializeComponent() call

    End Sub

    'Required by the Web Services Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Web Services Designer
    'It can be modified using the Web Services Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        'CODEGEN: This procedure is required by the Web Services Designer
        'Do not modify it using the code editor.
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

#End Region

    ' WEB SERVICE EXAMPLE
    ' The HelloWorld() example service returns the string Hello World.
    ' To build, uncomment the following lines then save and build the project.
    ' To test this web service, ensure that the .asmx file is the start page
    ' and press F5.
    '
    '<WebMethod()> Public Function HelloWorld() As String
    '	HelloWorld = "Hello World"
    ' End Function

	<WebMethod()> Public Sub Insert(entity as ${Entity.Name})
		${Entity.Name}Service.Insert(entity)
	End Sub

	<WebMethod()> Public Sub Update(entity as ${Entity.Name})
		${Entity.Name}Service.Update(entity)
	End Sub

	<WebMethod()> Public Sub Delete(id as Integer)
		${Entity.Name}Service.Delete(id)
	End Sub

	<WebMethod()> Public Function GetById(id as Integer) as ${Entity.Name}
		return ${Entity.Name}Service.GetById(id)
	End Function

	<WebMethod()> Public Function GetAll() as ${Entity.Name}()
		Dim arr as ArrayList = DirectCast(${Entity.Name}Service.GetAll(),ArrayList)
		return DirectCast(arr.ToArray(GetType(${Entity.Name})),${Entity.Name}())
	End Function

	<WebMethod()> Public Function GetList() as DataSet
		return DataService.ExecuteDataSet("${Entity.Name}GetList")
	End Function
End Class
