package ${Project.PackageName};

import org.hibernate.*;
import org.hibernate.cfg.*;

public class AjHibernate {
	private static Configuration configuration;
	private static SessionFactory sessionFactory;
	private static final ThreadLocal threadSession = new ThreadLocal();
	private static final ThreadLocal threadTransaction = new ThreadLocal();
//	private static final ThreadLocal threadInterceptor = new ThreadLocal();
	
	static {
		configuration = new Configuration();
<#
	for each Entity in Project.Model.Entities
#>
		configuration.addClass(${Project.PackageName}.model.${Entity.Name}.class);
<#
	end for
#>
		sessionFactory = configuration.buildSessionFactory();
	}
	
	public static SessionFactory getSessionFactory() {
		return sessionFactory;
	}
	
	public static Configuration getConfiguration() {
		return configuration;
	}
	
	public static Session getSession() {
		Session s = (Session) threadSession.get();
		if (s == null) {
			s = getSessionFactory().openSession();
			threadSession.set(s);
		}
		return s;
	}
	
	public static void closeSession() {
		Session s = (Session) threadSession.get();
		threadSession.set(null);
		if (s != null && s.isOpen()) {
			s.close();
		}
	}
	
	public static void beginTransaction() {
		Transaction tx = (Transaction) threadTransaction.get();
		
		if (tx == null) {
			tx = getSession().beginTransaction();
			threadTransaction.set(tx);
		}
	}

	public static void commitTransaction() {
		Transaction tx = (Transaction) threadTransaction.get();
		if ( tx != null && !tx.wasCommitted()
						&& !tx.wasRolledBack() ) {
			tx.commit();
		}
		threadTransaction.set(null);
	}

	public static void rollbackTransaction() {
		Transaction tx = (Transaction) threadTransaction.get();
		if ( tx != null && !tx.wasCommitted()
						&& !tx.wasRolledBack() ) {
			tx.rollback();
		}
		threadTransaction.set(null);
	}
}
