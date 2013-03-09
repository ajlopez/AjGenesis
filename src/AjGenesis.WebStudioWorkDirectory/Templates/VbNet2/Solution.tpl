Microsoft Visual Studio Solution File, Format Version 9.00
# Visual Studio 2005
<#
	for each proj in Project.Solution.Projects where proj.ProjectType="Web"
#>
Project("{${Project.Solution.WebGuid}}") = "C:\...\${proj.Name}\", "${proj.Name}\", "{${proj.Guid}}"
	ProjectSection(WebsiteProperties) = preProject
<#
if proj.Projects then
#>
		ProjectReferences = "<#
	for each subproj in proj.Projects
		print "{" & subproj.Guid.ToString() & "}|"&subproj.Name&";"
	end for
#>"
<#
end if
#>
		Debug.AspNetCompiler.VirtualPath = "/${proj.Name}"
		Debug.AspNetCompiler.PhysicalPath = "${proj.Name}\"
		Debug.AspNetCompiler.TargetPath = "PrecompiledWeb\${proj.Name}\"
		Debug.AspNetCompiler.Updateable = "true"
		Debug.AspNetCompiler.ForceOverwrite = "true"
		Debug.AspNetCompiler.FixedNames = "false"
		Debug.AspNetCompiler.Debug = "True"
		Release.AspNetCompiler.VirtualPath = "/${proj.Name}"
		Release.AspNetCompiler.PhysicalPath = "${proj.Name}\"
		Release.AspNetCompiler.TargetPath = "PrecompiledWeb\${proj.Name}\"
		Release.AspNetCompiler.Updateable = "true"
		Release.AspNetCompiler.ForceOverwrite = "true"
		Release.AspNetCompiler.FixedNames = "false"
		Release.AspNetCompiler.Debug = "False"
		VWDPort = "11246"
	EndProjectSection
EndProject
<#
	end for

	for each proj in Project.Solution.Projects where proj.ProjectType<>"Web"
#>
Project("{${Project.Solution.Guid}}") = "${proj.Name}", "${proj.Name}\${proj.Name}.vbproj", "{${proj.Guid}}"
EndProject
<#
	end for
#>
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
<#
	for each proj in Project.Solution.Projects
#>
		{${proj.Guid}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{${proj.Guid}}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{${proj.Guid}}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{${proj.Guid}}.Release|Any CPU.Build.0 = Release|Any CPU
<#
	end for
#>
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
EndGlobal
