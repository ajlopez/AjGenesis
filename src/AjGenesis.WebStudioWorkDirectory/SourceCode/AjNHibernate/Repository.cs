
using System;
using System.Web;
using System.Collections;

using NHibernate;

namespace AjNHibernate {

	public class Repository {
		private static Repository repository;
		private SessionFactory sessionfactory;
		private ISession activesession;

		public Repository() {
			sessionfactory = SessionFactory.Instance;
		}

		public static Repository Current {
			get {
				if (System.Web.HttpContext.Current != null) {
					if (System.Web.HttpContext.Current.Items["NHRepository"]==null)
              		System.Web.HttpContext.Current.Items["NHRepository"] = new Repository();

					return (Repository) System.Web.HttpContext.Current.Items["NHRepository"];
				}

				if (repository==null)
					repository = new Repository();

				return repository;
			}
		}

#region "Session Methods"

		public void OpenSession() {
			if (activesession == null || !activesession.IsOpen)
				activesession = sessionfactory.GetSession();
			else
   				throw new Exception("The repository already has an open session");
		}

		public void FlushSession() {
			if (activesession != null && activesession.IsOpen)
				activesession.Flush();
		}

		public void CloseSession() {
			if (activesession != null) {
				if (activesession.IsOpen)
					activesession.Close();
				activesession.Dispose();
			}
		}

		public ISession Session {
			get {
				if (activesession == null || !activesession.IsOpen)
					OpenSession();

				return activesession;
			}
		}

#endregion

#region "Object Methods"

		public object GetObjectById(System.Type type, int id) {
			return activesession.Get(type, id);
		}

		public IList GetAll(System.Type type) {
			ICriteria crit = activesession.CreateCriteria(type);

			return crit.List();
		}

		public void SaveObject(object obj) {
			activesession.Save(obj);
		}

		public void UpdateObject(object obj) {
			activesession.Update(obj);
		}

		public void DeleteObject(object obj) {
			activesession.Delete(obj);
		}

		public IList GetQuery(string query) {
			IQuery q = activesession.CreateQuery(query);

			return q.List();
		}

		public IList GetQuery(IQuery query) {
			return query.List();
		}

		public IQuery CreateQuery(string query) {
			IQuery q = activesession.CreateQuery(query);

			return q;
		}

#endregion
	}
}

