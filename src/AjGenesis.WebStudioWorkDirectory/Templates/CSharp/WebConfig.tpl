<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    
<configSections>
		<section name="AjFramework" type="AjFramework.Core.ConfigurationHandler,AjFramework.Core" />
</configSections>

  <system.web>

    <!--  DYNAMIC DEBUG COMPILATION
          Set compilation debug="true" to insert debugging symbols (.pdb information)
          into the compiled page. Because this creates a larger file that executes
          more slowly, you should set this value to true only when debugging and to
          false at all other times. For more information, refer to the documentation about
          debugging ASP.NET files.
    -->
    <compilation defaultLanguage="vb" debug="true" />

    <!--  CUSTOM ERROR MESSAGES
          Set customErrors mode="On" or "RemoteOnly" to enable custom error messages, "Off" to disable. 
          Add <error> tags for each of the errors you want to handle.
    -->
    <customErrors mode="RemoteOnly" />

    <!--  AUTHENTICATION 
          This section sets the authentication policies of the application. Possible modes are "Windows", 
          "Forms", "Passport" and "None"
    -->
    <authentication mode="Windows" /> 


    <!--  AUTHORIZATION 
          This section sets the authorization policies of the application. You can allow or deny access
          to application resources by user or role. Wildcards: "*" mean everyone, "?" means anonymous 
          (unauthenticated) users.
    -->
    <authorization>
        <allow users="*" /> <!-- Allow all users -->

            <!--  <allow     users="[comma separated list of users]"
                             roles="[comma separated list of roles]"/>
                  <deny      users="[comma separated list of users]"
                             roles="[comma separated list of roles]"/>
            -->
    </authorization>

    <!--  APPLICATION-LEVEL TRACE LOGGING
          Application-level tracing enables trace log output for every page within an application. 
          Set trace enabled="true" to enable application trace logging.  If pageOutput="true", the
          trace information will be displayed at the bottom of each page.  Otherwise, you can view the 
          application trace log by browsing the "trace.axd" page from your web application
          root. 
    -->
    <trace enabled="false" requestLimit="10" pageOutput="false" traceMode="SortByTime" localOnly="true" />


    <!--  SESSION STATE SETTINGS
          By default ASP.NET uses cookies to identify which requests belong to a particular session. 
          If cookies are not available, a session can be tracked by adding a session identifier to the URL. 
          To disable cookies, set sessionState cookieless="true".
    -->
    <sessionState 
            mode="InProc"
            stateConnectionString="tcpip=127.0.0.1:42424"
            sqlConnectionString="data source=127.0.0.1;user id=sa;password="
            cookieless="false" 
            timeout="20" 
    />

    <!--  GLOBALIZATION
          This section sets the globalization settings of the application. 
    -->
    <globalization requestEncoding="utf-8" responseEncoding="utf-8" culture="es-AR"
       uiCulture="es-AR"
 />
   
  </system.web>

	<AjFramework>
		<Parameters>
			<Parameter Name="ConnectionString" Value="server=${Technology.Database.Host};database=${Technology.Database.Name};uid=${Technology.Database.Username};pwd=${Technology.Database.Password}"/>
		</Parameters>
	</AjFramework>
</configuration>
