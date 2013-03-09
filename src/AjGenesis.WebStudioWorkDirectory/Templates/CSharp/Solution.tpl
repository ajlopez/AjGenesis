Microsoft Visual Studio Solution File, Format Version 8.00
<#
	for each proj in Project.Solution.Projects
		if proj.ProjectType="Web" then
#>
Project("{${Project.Solution.Guid}}") = "${proj.Name}", "http://localhost/${proj.Name}/${proj.Name}.csproj", "{${proj.Guid}}"
	ProjectSection(ProjectDependencies) = postProject
	EndProjectSection
EndProject
<#
		else
#>
Project("{${Project.Solution.Guid}}") = "${proj.Name}", "${proj.Name}\${proj.Name}.csproj", "{${proj.Guid}}"
	ProjectSection(ProjectDependencies) = postProject
	EndProjectSection
EndProject
<#
		end if
	end for
#>
Global
	GlobalSection(SolutionConfiguration) = preSolution
		Debug = Debug
		Release = Release
	EndGlobalSection
	GlobalSection(ProjectConfiguration) = postSolution
<#
	for each proj in Project.Solution.Projects
#>
		{${proj.Guid}}.Debug.ActiveCfg = Debug|.NET
		{${proj.Guid}}.Debug.Build.0 = Debug|.NET
		{${proj.Guid}}.Release.ActiveCfg = Release|.NET
		{${proj.Guid}}.Release.Build.0 = Release|.NET
<#
	end for
#>
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
	EndGlobalSection
	GlobalSection(ExtensibilityAddIns) = postSolution
	EndGlobalSection
EndGlobal
