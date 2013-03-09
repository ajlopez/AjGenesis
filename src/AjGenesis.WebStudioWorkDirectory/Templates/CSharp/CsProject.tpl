<VisualStudioProject>
    <CSHARP
<#
	if CsProject.ProjectType="Web" then
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
        ProjectGuid = "{${CsProject.Guid.ToString()}}"
    >
        <Build>
            <Settings
                ApplicationIcon = ""
                AssemblyKeyContainerName = ""
                AssemblyName = "${CsProject.Name}"
                AssemblyOriginatorKeyFile = ""
                DefaultClientScript = "JScript"
                DefaultHTMLPageLayout = "Grid"
                DefaultTargetSchema = "IE50"
                DelaySign = "false"
                OutputType = "Library"
                PreBuildEvent = ""
                PostBuildEvent = ""
                RootNamespace = "${CsProject.Name}"
                RunPostBuildEvent = "OnBuildSuccess"
                StartupObject = ""
            >
                <Config
                    Name = "Debug"
                    AllowUnsafeBlocks = "false"
                    BaseAddress = "285212672"
                    CheckForOverflowUnderflow = "false"
                    ConfigurationOverrideFile = ""
                    DefineConstants = "DEBUG;TRACE"
                    DocumentationFile = ""
                    DebugSymbols = "true"
                    FileAlignment = "4096"
                    IncrementalBuild = "false"
                    NoStdLib = "false"
                    NoWarn = ""
                    Optimize = "false"
<#
	if CsProject.ProjectType="Web" then
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
                    WarningLevel = "4"
                />
                <Config
                    Name = "Release"
                    AllowUnsafeBlocks = "false"
                    BaseAddress = "285212672"
                    CheckForOverflowUnderflow = "false"
                    ConfigurationOverrideFile = ""
                    DefineConstants = "TRACE"
                    DocumentationFile = ""
                    DebugSymbols = "false"
                    FileAlignment = "4096"
                    IncrementalBuild = "false"
                    NoStdLib = "false"
                    NoWarn = ""
                    Optimize = "true"
<#
	if CsProject.ProjectType="Web" then
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
                    WarningLevel = "4"
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
	if CsProject.ProjectType="Web" then
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
#>
                <Reference
                    Name = "System.XML"
                    AssemblyName = "System.Xml"
                />
<#
	for each Library in CsProject.Libraries
#>
                <Reference
                    Name = "${Library.Name}"
                    AssemblyName = "${Library.Name}"
                    HintPath = "${Library.Hint}"
                />
<#
	end for
#>
<#
	for each PrjRef in CsProject.Projects
#>
                <Reference
                    Name = "${PrjRef.Name}"
                    Project = "{${PrjRef.Guid}}"
                    Package = "{${Project.Solution.Guid}}"
                />
<#
	end for
#>

            </References>
            <Imports>
                <Import Namespace = "System" />
                <Import Namespace = "System.Collections" />
<#
	if CsProject.ProjectType="Web" then
#>
                <Import Namespace = "System.Configuration" />
<#
	end if
#>
                <Import Namespace = "System.Data" />
                <Import Namespace = "System.Diagnostics" />
<#
	if CsProject.ProjectType="Web" then
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
	if CsProject.ProjectType="Web" then
#>
                <File
                    RelPath = "Default.aspx"
                    BuildAction = "Content"
                />
                <File
                    RelPath = "Default.aspx.cs"
                    DependentUpon = "Default.aspx"
                    SubType = "ASPXCodeBehind"
                    BuildAction = "Compile"
                />
                <File
                    RelPath = "Default.aspx.resx"
                    DependentUpon = "Default.aspx.cs"
                    BuildAction = "EmbeddedResource"
                />
                <File
                    RelPath = "Global.asax"
                    BuildAction = "Content"
                />
                <File
                    RelPath = "Global.asax.cs"
                    DependentUpon = "Global.asax"
                    SubType = "Code"
                    BuildAction = "Compile"
                />
                <File
                    RelPath = "Global.asax.resx"
                    DependentUpon = "Global.asax.cs"
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

	for each File in CsProject.Includes
		if File.Type="cs" then
#>
                <File
                    RelPath = "${File.Name}.cs"
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

		if File.Type="aspx.cs" then
#>
                <File
                    RelPath = "${File.Name}.aspx.cs"
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
                    DependentUpon = "${File.Name}.aspx.cs"
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
                    RelPath = "Controls\${File.Name}.ascx.cs"
                    DependentUpon = "${File.Name}.ascx"
                    SubType = "ASPXCodeBehind"
                    BuildAction = "Compile"
                />
                <File
                    RelPath = "Controls\${File.Name}.ascx.resx"
                    DependentUpon = "${File.Name}.ascx.cs"
                    BuildAction = "EmbeddedResource"
                />
<#
		end if

	end for
#>
            </Include>
        </Files>
    </CSHARP>
</VisualStudioProject>

