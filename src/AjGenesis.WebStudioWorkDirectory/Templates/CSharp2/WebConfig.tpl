<?xml version="1.0"?>
<configuration>
  <configSections>
    <section name="AjFramework" type="AjFramework.Core.ConfigurationHandler,AjFramework.Core" />
  </configSections>
  <appSettings/>
	<connectionStrings>
		<remove name="LocalSqlServer"/>
		<add name="LocalSqlServer" connectionString="Data Source=(local);Initial Catalog=aspnetdb;Integrated Security=True" providerName="System.Data.SqlClient" />
		<add name="Superdome" connectionString="Data Source=(local);Initial Catalog=Superdome;Integrated Security=True" providerName="System.Data.SqlClient" />
	</connectionStrings>
	<system.web>
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
	<AjFramework>
		<Parameters>
			<Parameter Name="ConnectionString" Value="server=${Technology.Database.Host};database=${Technology.Database.Name};uid=${Technology.Database.Username};pwd=${Technology.Database.Password}"/>
		</Parameters>
	</AjFramework>
</configuration>
