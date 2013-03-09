package ${Project.PackageName};

import javax.servlet.*;
import java.io.IOException;

/*
 * Based on Hibernate example written by Christian Bauer <christian@hibernate.org>
 */

public class AjHibernateFilter implements Filter {
	public void init(FilterConfig filterConfig) throws ServletException {
	}

	public void doFilter(ServletRequest request,
						 ServletResponse response,
						 FilterChain chain)
			throws IOException, ServletException {

		// There is actually no explicit "opening" of a Session, the
		// first call to HibernateUtil.beginTransaction() in control
		// logic (e.g. use case controller/event handler) will get
		// a fresh Session.
		try {
			chain.doFilter(request, response);

			// Commit any pending database transaction.
			AjHibernate.commitTransaction();

		} finally {

			// No matter what happens, close the Session.
			AjHibernate.closeSession();

		}
	}

	public void destroy() {}


}
