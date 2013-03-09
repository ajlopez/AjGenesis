
using System;
using System.Web;

namespace AjNHibernate {

	public class SessionHttpModule : IHttpModule {
		public void Dispose() {
		}

		public void Init(HttpApplication context) {
			context.BeginRequest += BeginRequest;
			context.EndRequest += EndRequest;
		}

		private void BeginRequest(object sender, EventArgs e) {
			Repository.Current.OpenSession();
		}

		private void EndRequest(object sender, EventArgs e) {
			Repository.Current.FlushSession();
			Repository.Current.CloseSession();
		}
	}
}

