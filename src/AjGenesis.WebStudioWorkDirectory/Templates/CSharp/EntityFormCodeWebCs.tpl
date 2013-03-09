<#

message	"Generating Form Code Behind for Entity ${Entity.Name}..."

include "Templates/CSharp/CSharpFunctions.tpl"
include "Templates/EntityFunctions.tpl"

EntitySqlProperties	= SqlProperties(Entity)
EntityNoIdSqlProperties	= SqlNoIdProperties(Entity)
EntityIdProperty = IdProperty(Entity)
#>

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using ${Project.Name}.Services;
using ${Project.Name}.Entities;

namespace ${Project.Name}.WebClient
{
	/// <summary>
	/// Summary description for _Default.
	/// </summary>
	public class ${Entity.Name}UpdatePage : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnAccept;

<#
for each Property in EntityNoIdSqlProperties
	if Property.Reference then
#>
		protected System.Web.UI.WebControls.DropDownList ddl${Property.Reference.SetName};
<#
	else
		if Property.Enumeration then
#>
		protected System.Web.UI.WebControls.DropDownList ddl${Property.Name};
<#
		else
#>
		protected System.Web.UI.WebControls.TextBox txt${Property.Name};
<#
		end if
	end if
end for
#>

		protected System.Web.UI.WebControls.Label lblMensaje;
    
		public ${Entity.Name} Entity;

		public int IdEntity {
			get {
				return (int) ViewState["IdEntity"];
			}
			set {
				ViewState["IdEntity"] = value;
			}
		}

<#
	for each Property in Entity.Properties where Property.Reference
#>
		public DataView ${Property.Reference.SetName};
<#
	end for
#>

<#
	for each Property in Entity.Properties where Property.Enumeration
#>
		public IList ${Property.Enumeration.Name}List {
			get {
				return Enumerations.${Property.Enumeration.Name}List;
			}
		}
<#
	end for
#>

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!IsPostBack) {
				if (Request["Id"]==null) {
					IdEntity = 0;
					Entity = new ${Entity.Name}();
				}
				else {
					IdEntity = Convert.ToInt32(Request["Id"]);
					Entity = ${Entity.Name}Service.GetById(IdEntity);
				}
<#
	for each Property in Entity.Properties where Property.Reference
#>
				${Property.Reference.SetName} = Get${Property.Reference.SetName}();
<#
	end for
#>
            	DataBind();

				if (IdEntity>0) {
<#
	for each Property in Entity.Properties where Property.Reference
#>
					ddl${Property.Reference.SetName}.SelectedValue = Entity.${Property.Name};
<#
	end for
#>
<#
	for each Property in Entity.Properties where Property.Enumeration
#>
					ddl${Property.Name}.SelectedValue = Entity.${Property.Name};
<#
	end for
#>
				}
				else {
<#
	for each Property in Entity.Properties where Property.Reference
#>
					if (Request["${Property.Name}"]!=null)
						ddl${Property.Reference.SetName}.SelectedValue = Convert.ToInt32(Request["${Property.Name}"]);
<#
	end for
#>
				}
			}
		}

		private bool FormValidate() {
			return true;
		}

<#
	for each Property in Entity.Properties where Property.Reference
#>
    Private Function Get${Property.Reference.SetName}() As DataView
        Dim ds As DataSet

        ds = ${Property.Reference.Name}Service.GetList

        Dim dr As DataRow

        dr = ds.Tables(0).NewRow
<#
		if not Property.Required then
#>
        dr("Id") = 0
        dr("${Property.Reference.DescriptorProperty.Name}") = ""
        ds.Tables(0).Rows.Add(dr)

<#
		end if
#>
        Dim dw As New DataView(ds.Tables(0))
        dw.Sort = "${Property.Reference.DescriptorProperty.Name}"

        Return dw
    End Function

<#
	end for
#>
    Private Sub Update()
		if IdEntity>0 then
			Entity = ${Entity.Name}Service.GetById(IdEntity)
		else
			Entity = New ${Entity.Name}()
        end if

<#
for each Property in EntityNoIdSqlProperties
	if Property.Reference then
#>
        Entity.${Property.Name} = ddl${Property.Reference.SetName}.SelectedValue
<#
	else
		if Property.Enumeration then
#>
        Entity.${Property.Name} = ddl${Property.Name}.SelectedValue
<#
		else
#>        
		Entity.${Property.Name} = txt${Property.Name}.Text
<#
		end if
	end if
end for
#>        

        If IdEntity = 0 Then
            ${Entity.Name}Service.Insert(Entity)
        Else
            ${Entity.Name}Service.Update(Entity)
        End If
    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        If Not IsValid Then
            Return
        End If

        Try
            If FormValidate() Then
                Update()
		        If IdEntity = 0 Then
		            Server.Transfer("${Entity.SetName}.aspx")
				Else
					Server.Transfer("${Entity.Name}.aspx?Id=" & IdEntity)
		        End If
            End If
        Catch Ex As Exception
            lblMensaje.Visible = True
            lblMensaje.Text = Ex.Message
        End Try
    End Sub


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}


Imports ${Project.Name}.Services
Imports ${Project.Name}.Entities

Public Class ${Entity.Name}UpdatePage
    Inherits System.Web.UI.Page

    Protected WithEvents btnAccept As System.Web.UI.WebControls.Button

<#
for each Property in EntityNoIdSqlProperties
	if Property.Reference then
#>
	Protected WithEvents ddl${Property.Reference.SetName} As System.Web.UI.WebControls.DropDownList
<#
	else
		if Property.Enumeration then
#>
	Protected WithEvents ddl${Property.Name} As System.Web.UI.WebControls.DropDownList	
<#
		else
#>
    Protected WithEvents txt${Property.Name} As System.Web.UI.WebControls.TextBox
<#
		end if
	end if
end for
#>

    Protected WithEvents lblMensaje As System.Web.UI.WebControls.Label
    
    Public Entity As ${Entity.Name}

    Public Property IdEntity() As Integer
        Get
            Return DirectCast(ViewState("IdEntity"), Integer)
        End Get
        Set(ByVal Value As Integer)
            ViewState("IdEntity") = Value
        End Set
    End Property

<#
	for each Property in Entity.Properties where Property.Reference
#>
	Public ${Property.Reference.SetName} as DataView
<#
	end for
#>

<#
	for each Property in Entity.Properties where Property.Enumeration
#>
	Public ReadOnly Property ${Property.Enumeration.Name}List as IList
		Get
			return Enumerations.${Property.Enumeration.Name}List
		End Get
	End Property
<#
	end for
#>

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
        'Put user code to initialize the page here
        If Not IsPostBack Then
			If Request("Id") Is Nothing then
				IdEntity = 0
				Entity = new ${Entity.Name}
			else
				IdEntity = CInt(Request("Id"))
				Entity = ${Entity.Name}Service.GetById(IdEntity)
			End If
<#
	for each Property in Entity.Properties where Property.Reference
#>
			${Property.Reference.SetName} = Get${Property.Reference.SetName}
<#
	end for
#>
            	DataBind()

			if IdEntity>0 then
<#
	for each Property in Entity.Properties where Property.Reference
#>
				ddl${Property.Reference.SetName}.SelectedValue = Entity.${Property.Name}
<#
	end for
#>
<#
	for each Property in Entity.Properties where Property.Enumeration
#>
				ddl${Property.Name}.SelectedValue = Entity.${Property.Name}
<#
	end for
#>
			else
<#
	for each Property in Entity.Properties where Property.Reference
#>
				if not Request("${Property.Name}") is nothing then
					ddl${Property.Reference.SetName}.SelectedValue = CInt(Request("${Property.Name}"))
				end if
<#
	end for
#>
			end if
        End If
    End Sub

    Private Function FormValidate() As Boolean
        Return True
    End Function

<#
	for each Property in Entity.Properties where Property.Reference
#>
    Private Function Get${Property.Reference.SetName}() As DataView
        Dim ds As DataSet

        ds = ${Property.Reference.Name}Service.GetList

        Dim dr As DataRow

        dr = ds.Tables(0).NewRow
<#
		if not Property.Required then
#>
        dr("Id") = 0
        dr("${Property.Reference.DescriptorProperty.Name}") = ""
        ds.Tables(0).Rows.Add(dr)

<#
		end if
#>
        Dim dw As New DataView(ds.Tables(0))
        dw.Sort = "${Property.Reference.DescriptorProperty.Name}"

        Return dw
    End Function

<#
	end for
#>
    Private Sub Update()
		if IdEntity>0 then
			Entity = ${Entity.Name}Service.GetById(IdEntity)
		else
			Entity = New ${Entity.Name}()
        end if

<#
for each Property in EntityNoIdSqlProperties
	if Property.Reference then
#>
        Entity.${Property.Name} = ddl${Property.Reference.SetName}.SelectedValue
<#
	else
		if Property.Enumeration then
#>
        Entity.${Property.Name} = ddl${Property.Name}.SelectedValue
<#
		else
#>        
		Entity.${Property.Name} = txt${Property.Name}.Text
<#
		end if
	end if
end for
#>        

        If IdEntity = 0 Then
            ${Entity.Name}Service.Insert(Entity)
        Else
            ${Entity.Name}Service.Update(Entity)
        End If
    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        If Not IsValid Then
            Return
        End If

        Try
            If FormValidate() Then
                Update()
		        If IdEntity = 0 Then
		            Server.Transfer("${Entity.SetName}.aspx")
				Else
					Server.Transfer("${Entity.Name}.aspx?Id=" & IdEntity)
		        End If
            End If
        Catch Ex As Exception
            lblMensaje.Visible = True
            lblMensaje.Text = Ex.Message
        End Try
    End Sub
End Class
