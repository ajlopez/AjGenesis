<#
include	"Templates/CSharp2DDDNh/Prologue.tpl"
#>

using System;

using NHibernate;
using NHibernate.Cfg;

namespace AjNHibernate {

	public class SessionFactory {
		private static SessionFactory session = new SessionFactory();
		private static Configuration nhconfiguration;
		private static ISessionFactory nhsessionfactory;

		public SessionFactory() {
			RegisterClasses();
		}
	
		private void RegisterClasses() {
			nhconfiguration = new Configuration();
			nhconfiguration.AddAssembly("${Project.Name}.Domain");
			nhsessionfactory = nhconfiguration.BuildSessionFactory();
		}

		public static SessionFactory Instance {
			get {
				return session;
			}
		}

		public ISession GetSession() {
			return nhsessionfactory.OpenSession();
		}

		public ISessionFactory GetNHibernateFactory() {
			return nhsessionfactory;
		}
	}
}
