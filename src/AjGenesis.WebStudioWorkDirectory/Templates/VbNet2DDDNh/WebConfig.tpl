<?xml version="1.0"?>
<configuration>
<configSections>
	        <section name="nhibernate" type="System.Configuration.NameValueSectionHandler, System, Version=1.0.3300.0,Culture=neutral, PublicKeyToken=b77a5c561934e089" />
</configSections>

  <appSettings/>
	<connectionStrings>
		<remove name="LocalSqlServer"/>
		<add name="LocalSqlServer" connectionString="Data Source=(local);Initial Catalog=aspnetdb;Integrated Security=True" providerName="System.Data.SqlClient" />
		<add name="Superdome" connectionString="Data Source=(local);Initial Catalog=Superdome;Integrated Security=True" providerName="System.Data.SqlClient" />
	</connectionStrings>
	<system.web>
    <httpModules>
      <add type="AjNHibernate.SessionHttpModule, AjNHibernate" name="NHSessionModule" />
    </httpModules>

		<roleManager enabled="true" />
		<compilation debug="true"/>
		<pages theme="Default">
			<namespaces>
				<clear/>
				<add namespace="System"/>
				<add namespace="System.Collections"/>
				<add namespace="System.Collections.Specialized"/>
				<add namespace="System.Configuration"/>
				<add namespace="System.Text"/>
				<add namespace="System.Text.RegularExpressions"/>
				<add namespace="System.Web"/>
				<add namespace="System.Web.Caching"/>
				<add namespace="System.Web.SessionState"/>
				<add namespace="System.Web.Security"/>
				<add namespace="System.Web.Profile"/>
				<add namespace="System.Web.UI"/>
				<add namespace="System.Web.UI.WebControls"/>
				<add namespace="System.Web.UI.WebControls.WebParts"/>
				<add namespace="System.Web.UI.HtmlControls"/>
			</namespaces>
		</pages>
		<authentication mode="Forms" />
	</system.web>
	<nhibernate>
		<add 
			key="hibernate.connection.provider"          
			value="NHibernate.Connection.DriverConnectionProvider" 
		/>
		<add 
			key="hibernate.dialect"                      
			value="${Technology.NHibernate.Dialect}" 
		/>
		<add 
			key="hibernate.connection.driver_class"          
			value="${Technology.NHibernate.Driver}" 
		/>
		<add 
			key="hibernate.connection.connection_string" 
			value="${Technology.NHibernate.ConnectionString}" 
		/>
	</nhibernate>
</configuration>
