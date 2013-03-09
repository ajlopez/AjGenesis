<?xml version="1.0" encoding="ISO-8859-1" ?>

<!DOCTYPE struts-config PUBLIC
          "-//Apache Software Foundation//DTD Struts Configuration 1.2//EN"
          "http://jakarta.apache.org/struts/dtds/struts-config_1_2.dtd">


<struts-config>

<!-- ============================================ Data Source Configuration -->
<data-sources>
<data-source type="org.apache.commons.dbcp.BasicDataSource">
    <set-property
      property="driverClassName"
      value="${Technology.JDBC.Driver}" />
    <set-property
      property="url"
      value="jdbc:${Technology.JDBC.Subprotocol}://${Technology.Database.Host}/${Technology.Database.Name}" />
    <set-property
      property="username"
      value="${Technology.Database.Username}" />
    <set-property
      property="password"
      value="${Technology.Database.Password}" />
</data-source>
</data-sources>

<!-- ================================================ Form Bean Definitions -->

    <form-beans>
<#
	for each Entity in Project.Model.Entities
#>
        <form-bean
            name="${Entity.Name}Form"
            type="${Project.PackageName}.struts.forms.${Entity.Name}Form"/>
<#
	end for
#>
    <!-- sample form bean descriptor for a DynaActionForm
        <form-bean
            name="logonForm"
            type="org.apache.struts.action.DynaActionForm">
            <form-property
                name="username"
                type="java.lang.String"/>
            <form-property
                name="password"
                type="java.lang.String"/>
       </form-bean>
    end sample -->
    </form-beans>


<!-- ========================================= Global Exception Definitions -->

    <global-exceptions>
        <!-- sample exception handler
        <exception
            key="expired.password"
            type="app.ExpiredPasswordException"
            path="/changePassword.jsp"/>
        end sample -->
    </global-exceptions>


<!-- =========================================== Global Forward Definitions -->

    <global-forwards>
        <!-- Default forward to "Welcome" action -->
        <!-- Demonstrates using index.jsp to forward -->
        <forward
            name="welcome"
            path="/Welcome.do"/>
        <forward
            name="failure"
            path="/WEB-INF/jsp/Error.jsp"/>
    </global-forwards>


<!-- =========================================== Action Mapping Definitions -->

    <action-mappings>
<#
	for each Entity in Project.Model.Entities
#>
        <action
            path="/${Entity.Name}List"
            type="${Project.PackageName}.struts.actions.${Entity.Name}ListAction">
            <forward name="success" path="/WEB-INF/jsp/${Entity.Name}List.jsp"/>
        </action>

        <action
            path="/${Entity.Name}View"
            type="${Project.PackageName}.struts.actions.${Entity.Name}ViewAction">
            <forward name="success" path="/WEB-INF/jsp/${Entity.Name}View.jsp"/>
            <forward name="failure" path="/WEB-INF/jsp/Error.jsp"/>
        </action>
        
        <action
            path="/${Entity.Name}Form"
            name="${Entity.Name}Form"
            scope="request"
            validate="false"
            type="${Project.PackageName}.struts.actions.${Entity.Name}FormAction">
            <forward name="success" path="/WEB-INF/jsp/${Entity.Name}Form.jsp"/>
        </action>
        
        <action
            path="/${Entity.Name}Update"
            name="${Entity.Name}Form"
            scope="request"
            input="/WEB-INF/jsp/${Entity.Name}Form.jsp"
				type="${Project.PackageName}.struts.actions.${Entity.Name}UpdateAction">
				<forward name="success" path="/WEB-INF/jsp/${Entity.Name}View.jsp"/>				
				<forward name="canceled" path="/${Entity.Name}View.do"/>				
				</action>
				
		<action path="/${Entity.Name}NewForm"
			forward="/WEB-INF/jsp/${Entity.Name}NewForm.jsp"/>
				
        <action
            path="/${Entity.Name}Insert"
            name="${Entity.Name}Form"
            scope="request"
            input="/WEB-INF/jsp/${Entity.Name}NewForm.jsp"
				type="${Project.PackageName}.struts.actions.${Entity.Name}InsertAction">
				<forward name="success" path="/${Entity.Name}List.do"/>				
				</action>

<#
	end for
#>

        <action
            path="/Home"
				forward="/index.jsp"
/>

    <!-- sample input and input submit actions

        <action
            path="/Input"
            type="org.apache.struts.actions.ForwardAction"
            parameter="/pages/Input.jsp"/>

        <action
            path="/InputSubmit"
            type="app.InputAction"
            name="inputForm"
            scope="request"
            validate="true"
            input="/pages/Input.jsp"/>

            <action
                path="/edit*"
                type="app.Edit{1}Action"
                name="inputForm"
                scope="request"
                validate="true"
                input="/pages/Edit{1}.jsp"/>

    end samples -->
    </action-mappings>



<!-- ======================================== Message Resources Definitions -->

    <message-resources parameter="Resources" />

</struts-config>

