Imports NHibernate
Imports NHibernate.Cfg

Public Class SessionFactory
    Private Shared mSession As New SessionFactory
    Private mNhConfiguration As Configuration
    Private mNhSessionFactory As ISessionFactory

    Protected Sub New()
        RegisterClasses()
    End Sub

    Private Sub RegisterClasses()
        mNhConfiguration = New Configuration
        mNhConfiguration.AddAssembly("${Project.Name}.Domain")
        mNhSessionFactory = mNhConfiguration.BuildSessionFactory()
    End Sub

    Public Shared ReadOnly Property Instance() As SessionFactory
        Get
            Return mSession
        End Get
    End Property

    Public Function GetSession() As ISession
        Return mNhSessionFactory.OpenSession
    End Function

    Public Function GetNHibernateFactory() As ISessionFactory
        Return mNhSessionFactory
    End Function
End Class
