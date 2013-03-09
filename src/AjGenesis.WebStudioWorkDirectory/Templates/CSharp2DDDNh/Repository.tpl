<#
include	"Templates/CSharp2DDDNh/Prologue.tpl"
#>

using System;
using System.Web;

using NHibernate;

namespace ${Project.SystemName}.AjNHibernate {
	public class Repository {
		private static Repository repository;
		private SessionFactory sessionfactory;
		private ISession activesession;

		public Repository() {
			sessionfactory = SessionFactory.Instance;
		}

		public static Repository Current() {
			get {
				if (System.Web.HttpContext.Current != null) {
              	if (System.Web.HttpContext.Current.Items("NHRepository") == null)
                 		System.Web.HttpContext.Current.Items("NHRepository") = New Repository();
              	return (Repository) System.Web.HttpContext.Current.Items("NHRepository");
				}

				if (repository == null)
					repository = new Repository();
				
				return repository;
			}
		}

#Region "Session Methods"

		public void OpenSession() {
        If mActiveSession Is Nothing OrElse Not mActiveSession.IsOpen Then
            mActiveSession = mSessionFactory.GetSession
        Else
            Throw New Exception("The repository already has an open session")
        End If
		}

    Public Sub FlushSession()
        If Not mActiveSession Is Nothing AndAlso mActiveSession.IsOpen Then
            mActiveSession.Flush()
        End If
    End Sub

    Public Sub CloseSession()
        If Not mActiveSession Is Nothing Then
            If mActiveSession.IsOpen Then
                mActiveSession.Close()
            End If
            mActiveSession.Dispose()
        End If
    End Sub

    Public ReadOnly Property Session() As ISession
        Get
            If mActiveSession Is Nothing OrElse Not mActiveSession.IsOpen Then
                OpenSession()
            End If

            Return mActiveSession
        End Get
    End Property
#End Region

#Region "Object Methods"

    Public Function GetObjectById(ByVal type As System.Type, ByVal id As Integer) As Object
        Return mActiveSession.Get(type, id)
    End Function

    Public Function GetAll(ByVal type As System.Type) As IList
        Dim crit As ICriteria = mActiveSession.CreateCriteria(type)
        Return crit.List
    End Function

    Public Sub SaveObject(ByVal obj As Object)
        mActiveSession.Save(obj)
    End Sub

    Public Sub UpdateObject(ByVal obj As Object)
        mActiveSession.Update(obj)
    End Sub

    Public Sub DeleteObject(ByVal obj As Object)
        mActiveSession.Delete(obj)
    End Sub

    Public Function GetQuery(ByVal query As String) As IList
        Dim q As IQuery = mActiveSession.CreateQuery(query)
        Return q.List()
    End Function

    Public Function GetQuery(query as IQuery) As IList
        Return query.List()
    End Function

    Public Function CreateQuery(ByVal query As String) As IQuery
        Dim q As IQuery = mActiveSession.CreateQuery(query)
        Return q
    End Function
#End Region

End Class

