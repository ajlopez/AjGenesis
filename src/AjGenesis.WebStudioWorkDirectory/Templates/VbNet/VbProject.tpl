<VisualStudioProject>
    <VisualBasic
<#
	if VbProject.ProjectType="Web" then
#>
        ProjectType = "Web" 
<#
	else
#>
        ProjectType = "Local" 
<#
	end if
#>
        ProductVersion = "7.10.3077"
        SchemaVersion = "2.0"
        ProjectGuid = "{${VbProject.Guid.ToString()}}"
    >
        <Build>
            <Settings
                ApplicationIcon = ""
                AssemblyKeyContainerName = ""
                AssemblyName = "${VbProject.Name}"
                AssemblyOriginatorKeyFile = ""
                AssemblyOriginatorKeyMode = "None"
                DefaultClientScript = "JScript"
                DefaultHTMLPageLayout = "Grid"
                DefaultTargetSchema = "IE50"
                DelaySign = "false"
                OutputType = "Library"
                OptionCompare = "Binary"
                OptionExplicit = "On"
                OptionStrict = "Off"
                RootNamespace = "${VbProject.Name}"
                StartupObject = ""
            >
                <Config
                    Name = "Debug"
                    BaseAddress = "285212672"
                    ConfigurationOverrideFile = ""
                    DefineConstants = ""
                    DefineDebug = "true"
                    DefineTrace = "true"
                    DebugSymbols = "true"
                    IncrementalBuild = "true"
                    Optimize = "false"
<#
	if VbProject.ProjectType="Web" then
#>
                    OutputPath = "bin\"
<#
	else
#>
                    OutputPath = "bin\Debug\"
<#
	end if
#>
                    RegisterForComInterop = "false"
                    RemoveIntegerChecks = "false"
                    TreatWarningsAsErrors = "false"
                    WarningLevel = "1"
                />
                <Config
                    Name = "Release"
                    BaseAddress = "285212672"
                    ConfigurationOverrideFile = ""
                    DefineConstants = ""
                    DefineDebug = "false"
                    DefineTrace = "true"
                    DebugSymbols = "false"
                    IncrementalBuild = "false"
                    Optimize = "true"
<#
	if VbProject.ProjectType="Web" then
#>
                    OutputPath = "bin\"
<#
	else
#>
                    OutputPath = "bin\Release\"
<#
	end if
#>
                    RegisterForComInterop = "false"
                    RemoveIntegerChecks = "false"
                    TreatWarningsAsErrors = "false"
                    WarningLevel = "1"
                />
            </Settings>
            <References>
                <Reference
                    Name = "System"
                    AssemblyName = "System"
                />
                <Reference
                    Name = "System.Data"
                    AssemblyName = "System.Data"
                />
<#
	if VbProject.ProjectType="Web" then
#>
                <Reference
                    Name = "System.Drawing"
                    AssemblyName = "System.Drawing"
                />
                <Reference
                    Name = "System.Web"
                    AssemblyName = "System.Web"
                />
<#
	end if

	if VbProject.UseWeb then
#>
                <Reference
                    Name = "System.Web"
                    AssemblyName = "System.Web"
                />
<#
	end if
#>
                <Reference
                    Name = "System.XML"
                    AssemblyName = "System.Xml"
                />
<#
	if VbProject.Projects then
		for each PrjRef in VbProject.Projects
#>
                <Reference
                    Name = "${PrjRef.Name}"
                    Project = "{${PrjRef.Guid}}"
                    Package = "{${Project.Solution.Guid}}"
                />
<#
		end for
	end if
#>
<#
	if VbProject.Libraries then
		for each Library in VbProject.Libraries
#>
                <Reference
                    Name = "${Library.Name}"
                    AssemblyName = "${Library.Name}"
                    HintPath = "${Library.Hint}"
                />
<#
		end for
	end if
#>
            </References>
            <Imports>
                <Import Namespace = "Microsoft.VisualBasic" />
                <Import Namespace = "System" />
                <Import Namespace = "System.Collections" />
<#
	if VbProject.ProjectType="Web" then
#>
                <Import Namespace = "System.Configuration" />
<#
	end if
#>
                <Import Namespace = "System.Data" />
                <Import Namespace = "System.Diagnostics" />
<#
	if VbProject.ProjectType="Web" then
#>
                <Import Namespace = "System.Drawing" />
                <Import Namespace = "System.Web" />
                <Import Namespace = "System.Web.UI" />
                <Import Namespace = "System.Web.UI.HtmlControls" />
                <Import Namespace = "System.Web.UI.WebControls" />
<#
	end if
#>
            </Imports>
        </Build>
        <Files>
            <Include>
<#
	if VbProject.ProjectType="Web" then
#>
                <File
                    RelPath = "Default.aspx"
                    BuildAction = "Content"
                />
                <File
                    RelPath = "Default.aspx.vb"
                    DependentUpon = "Default.aspx"
                    SubType = "ASPXCodeBehind"
                    BuildAction = "Compile"
                />
                <File
                    RelPath = "Default.aspx.resx"
                    DependentUpon = "Default.aspx.vb"
                    BuildAction = "EmbeddedResource"
                />
                <File
                    RelPath = "Global.asax"
                    BuildAction = "Content"
                />
                <File
                    RelPath = "Global.asax.vb"
                    DependentUpon = "Global.asax"
                    SubType = "Code"
                    BuildAction = "Compile"
                />
                <File
                    RelPath = "Global.asax.resx"
                    DependentUpon = "Global.asax.vb"
                    BuildAction = "EmbeddedResource"
                />
                <File
                    RelPath = "Styles.css"
                    BuildAction = "Content"
                />
                <File
                    RelPath = "Web.config"
                    BuildAction = "Content"
                />
<#
	end if

	for each File in VbProject.Includes
		if File.Type="vb" then
#>
                <File
                    RelPath = "${File.Name}.vb"
                    SubType = "Code"
                    BuildAction = "Compile"
                />
<#
		end if

		if File.Type="aspx" then
#>
                <File
                    RelPath = "${File.Name}.aspx"
                    BuildAction = "Content"
                />
<#
		end if

		if File.Type="aspx.vb" then
#>
                <File
                    RelPath = "${File.Name}.aspx.vb"
                    DependentUpon = "${File.Name}.aspx"
                    SubType = "ASPXCodeBehind"
                    BuildAction = "Compile"
                />
<#
		end if

		if File.Type="aspx.resx" then
#>
                <File
                    RelPath = "${File.Name}.aspx.resx"
                    DependentUpon = "${File.Name}.aspx.vb"
                    BuildAction = "EmbeddedResource"
                />
<#
		end if


		if File.Type="hbm.xml" then
#>
                <File
                    RelPath = "${File.Name}.hbm.xml"
                    BuildAction = "EmbeddedResource"
                />
<#
		end if

		if File.Type="ascx" then
#>
                <File
                    RelPath = "Controls\${File.Name}.ascx"
                    BuildAction = "Content"
                />
                <File
                    RelPath = "Controls\${File.Name}.ascx.vb"
                    DependentUpon = "${File.Name}.ascx"
                    SubType = "ASPXCodeBehind"
                    BuildAction = "Compile"
                />
                <File
                    RelPath = "Controls\${File.Name}.ascx.resx"
                    DependentUpon = "${File.Name}.ascx.vb"
                    BuildAction = "EmbeddedResource"
                />
<#
		end if

		if File.Type="asmx" then
#>
                <File
                    RelPath = "Controls\${File.Name}.asmx"
                    BuildAction = "Content"
                />
                <File
                    RelPath = "Controls\${File.Name}.asmx.vb"
                    DependentUpon = "${File.Name}.asmx"
                    SubType = "Component"
                    BuildAction = "Compile"
                />
                <File
                    RelPath = "Controls\${File.Name}.asmx.resx"
                    DependentUpon = "${File.Name}.asmx.vb"
                    BuildAction = "EmbeddedResource"
                />
<#
		end if

	end for
#>
            </Include>
        </Files>
    </VisualBasic>
</VisualStudioProject>

