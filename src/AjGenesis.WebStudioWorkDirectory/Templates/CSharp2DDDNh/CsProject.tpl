<Project DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProductVersion>8.0.50727</ProductVersion>
    <SchemaVersion>2.0</SchemaVersion>
    <ProjectGuid>{${CsProject.Guid.ToString()}}</ProjectGuid>
    <OutputType>Library</OutputType>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <RootNamespace>${CsProject.Name}</RootNamespace>
    <AssemblyName>${CsProject.Name}</AssemblyName>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\Debug\</OutputPath>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\Release\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="System" />
    <Reference Include="System.Data" />
    <Reference Include="System.Xml" />
    <Reference Include="System.Web" />
<#
	for each CsProjectLibrary in CsProject.Libraries
#>
    <Reference Include="${CsProjectLibrary.Name}">
      <HintPath>${CsProjectLibrary.Hint}</HintPath>
    </Reference>
<#
	end for
#>
<#
	for each CsProjectReference in CsProject.Projects
#>
    <ProjectReference Include="..\${CsProjectReference.Name}\${CsProjectReference.Name}.csproj">
      <Project>{${CsProjectReference.Guid.ToString()}}</Project>
      <Name>${CsProjectReference.Name}</Name>
    </ProjectReference>
<#
	end for
#>
  </ItemGroup>
  <ItemGroup>
<#
	for each File in CsProject.Includes where File.Type="cs"
#>
    <Compile Include="${File.Name}.cs" />
<#
	end for
#>
    <Compile Include="Properties\AssemblyInfo.cs" />
  </ItemGroup>
  <ItemGroup>
<#
	for each File in CsProject.Includes where File.Type="hbm.xml"
#>
    <EmbeddedResource Include="${File.Name}.hbm.xml" />
<#
	end for
#>
  </ItemGroup>
  <Import Project="$(MSBuildBinPath)\Microsoft.CSharp.targets" />
  <!-- To modify your build process, add your task inside one of the targets below and uncomment it. 
       Other similar extension points exist, see Microsoft.Common.targets.
  <Target Name="BeforeBuild">
  </Target>
  <Target Name="AfterBuild">
  </Target>
  -->
</Project>