<?xml version="1.0" encoding="ISO-8859-1"?>

<web-app xmlns="http://java.sun.com/xml/ns/j2ee"
    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
    xsi:schemaLocation="http://java.sun.com/xml/ns/j2ee http://java.sun.com/xml/ns/j2ee/web-app_2_4.xsd"
    version="2.4">

  <display-name>${Project.Name}</display-name>
  <description>${Project.Description}</description>

    <filter>
        <filter-name>AjHibernateFilter</filter-name>
        <filter-class>${Project.PackageName}.AjHibernateFilter</filter-class>
    </filter>

    <filter-mapping>
        <filter-name>AjHibernateFilter</filter-name>
        <url-pattern>/*</url-pattern>
    </filter-mapping>
</web-app>

