<?xml version="1.0" encoding = "UTF-8"?>
<project default="build" basedir=".">

	<property file="build.properties" />
	<property name="src.dir" location="Src" />
	<property name="lib.dir" location="Libraries" />
	<property name="build.dir" location="Build"/>
	<property name="classes.dir" location="${"${"}build.dir}/classes"/>
	<property name="war.dir" location="${"${"}build.dir}/war"/>
	<property name="appname" value="${Technology.WebSite.Name}"/>
	<property name="warfile" location="${"${"}war.dir}/${"${"}appname}.war"/>

	<path id="build-classpath">
		<fileset dir="${"${"}struts.root}/lib">
			<include name="*.jar"/>
		</fileset>
		<fileset dir="${"${"}tomcat.home}/common/lib">
			<include name="servlet-api.jar"/>
		</fileset>
	</path>

	<target name="init" description="Create build directories">
		<mkdir dir="${"${"}build.dir}"/>
		<mkdir dir="${"${"}build.dir}/classes"/>
		<mkdir dir="${"${"}build.dir}/war"/>
	</target>
 	
	<target name="compile" depends="init" description="Compiles the Java source code">
		<javac srcdir="${"${"}src.dir}/Java" destdir="${"${"}build.dir}/classes">
			<classpath refid="build-classpath"/>
		</javac>
	</target>
	
	<target name="build" depends="init,compile" description="Makes the war file">
		<war destfile="${"${"}warfile}" webxml="${"${"}src.dir}/Metadata/web.xml">
			<fileset dir="${"${"}src.dir}/Web"/>
			<lib dir="${"${"}lib.dir}"/>
			<lib dir="${"${"}struts.root}/lib"/>
			<classes dir="${"${"}build.dir}/classes"/>
		</war>
	</target>

    <target name="clean" description="Removes the Build directory">
        <delete dir="${"${"}build.dir}"/>
    </target>

<!-- ============================================================== -->
<!-- Tomcat tasks - remove these if you don't have Tomcat installed -->
<!-- ============================================================== -->

	<taskdef name="install" classname="org.apache.catalina.ant.InstallTask">
		<classpath>
			<path location="${"${"}tomcat.home}/server/lib/catalina-ant.jar"/>
		</classpath>
	</taskdef>

	<taskdef name="undeploy" classname="org.apache.catalina.ant.UndeployTask">
		<classpath>
			<path location="${"${"}tomcat.home}/server/lib/catalina-ant.jar"/>
		</classpath>
	</taskdef>

	<taskdef name="reload" classname="org.apache.catalina.ant.ReloadTask">
		<classpath>
			<path location="${"${"}tomcat.home}/server/lib/catalina-ant.jar"/>
		</classpath>
	</taskdef>

	<taskdef name="list" classname="org.apache.catalina.ant.ListTask">
		<classpath>
			<path location="${"${"}tomcat.home}/server/lib/catalina-ant.jar"/>
		</classpath>
	</taskdef>

	<taskdef name="start" classname="org.apache.catalina.ant.StartTask">
		<classpath>
			<path location="${"${"}tomcat.home}/server/lib/catalina-ant.jar"/>
		</classpath>
	</taskdef>

	<taskdef name="stop" classname="org.apache.catalina.ant.StopTask">
		<classpath>
			<path location="${"${"}tomcat.home}/server/lib/catalina-ant.jar"/>
		</classpath>
	</taskdef>

	<target name="install" description="Install application in Tomcat" depends="build">
		<install url="${"${"}tomcat.manager.url}"
			username="${"${"}tomcat.manager.username}"
			password="${"${"}tomcat.manager.password}"
			path="/${"${"}appname}"
			war="file://${"${"}warfile}"/>
	</target>

	<target name="remove" description="Remove application from Tomcat">
		<undeploy url="${"${"}tomcat.manager.url}"
			username="${"${"}tomcat.manager.username}"
			password="${"${"}tomcat.manager.password}"
			path="/${"${"}appname}"/>
	</target>

	<target name="reload" description="Reload application in Tomcat">
		<reload url="${"${"}tomcat.manager.url}"
			username="${"${"}tomcat.manager.username}"
			password="${"${"}tomcat.manager.password}"
			path="/${"${"}appname}"/>
	</target>

	<target name="start" description="Start Tomcat application">
		<start url="${"${"}tomcat.manager.url}"
			username="${"${"}tomcat.manager.username}"
			password="${"${"}tomcat.manager.password}"
			path="/${"${"}appname}"/>
	</target>

	<target name="stop" description="Stop Tomcat application">
		<stop url="${"${"}tomcat.manager.url}"
			username="${"${"}tomcat.manager.username}"
			password="${"${"}tomcat.manager.password}"
			path="/${"${"}appname}"/>
	</target>

	<target name="list" description="List Tomcat applications">
		<list url="${"${"}tomcat.manager.url}"
			username="${"${"}tomcat.manager.username}"
			password="${"${"}tomcat.manager.password}"/>
	</target>

<!-- End Tomcat tasks -->
</project>
