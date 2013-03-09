<?xml version="1.0" encoding="utf-8"?>
<Project DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProductVersion>8.0.50727</ProductVersion>
    <SchemaVersion>2.0</SchemaVersion>
    <ProjectGuid>{${VbProject.Guid.ToString()}}</ProjectGuid>
    <OutputType>Library</OutputType>
    <RootNamespace>${VbProject.Name}</RootNamespace>
    <AssemblyName>${VbProject.Name}</AssemblyName>
    <MyType>Windows</MyType>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <OutputPath>bin\</OutputPath>
    <DocumentationFile>${VbProject.Name}.xml</DocumentationFile>
    <BaseAddress>285212672</BaseAddress>
    <ConfigurationOverrideFile>
    </ConfigurationOverrideFile>
    <DefineConstants>
    </DefineConstants>
    <DefineDebug>true</DefineDebug>
    <DefineTrace>true</DefineTrace>
    <DebugSymbols>true</DebugSymbols>
    <Optimize>false</Optimize>
    <RegisterForComInterop>false</RegisterForComInterop>
    <RemoveIntegerChecks>false</RemoveIntegerChecks>
    <TreatWarningsAsErrors>false</TreatWarningsAsErrors>
    <WarningLevel>1</WarningLevel>
    <NoWarn>42016,42017,42018,42019,42032</NoWarn>
    <DebugType>full</DebugType>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <OutputPath>bin\</OutputPath>
    <DocumentationFile>${Project.Name}.xml</DocumentationFile>
    <BaseAddress>285212672</BaseAddress>
    <ConfigurationOverrideFile>
    </ConfigurationOverrideFile>
    <DefineConstants>
    </DefineConstants>
    <DefineDebug>false</DefineDebug>
    <DefineTrace>true</DefineTrace>
    <DebugSymbols>false</DebugSymbols>
    <Optimize>true</Optimize>
    <RegisterForComInterop>false</RegisterForComInterop>
    <RemoveIntegerChecks>false</RemoveIntegerChecks>
    <TreatWarningsAsErrors>false</TreatWarningsAsErrors>
    <WarningLevel>1</WarningLevel>
    <NoWarn>42016,42017,42018,42019,42032</NoWarn>
    <DebugType>none</DebugType>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="System">
      <Name>System</Name>
    </Reference>
    <Reference Include="System.Data">
      <Name>System.Data</Name>
    </Reference>
    <Reference Include="System.Xml">
      <Name>System.XML</Name>
    </Reference>
    <Reference Include="System.Web">
      <Name>System.Web</Name>
    </Reference>
<#
	for each VbProjectLibrary in VbProject.Libraries
#>
    <Reference Include="${VbProjectLibrary.Name}">
      <HintPath>${VbProjectLibrary.Hint}</HintPath>
    </Reference>
<#
	end for
#>
<#
	for each VbProjectReference in VbProject.Projects
#>
    <ProjectReference Include="${VbProjectReference.Name}">
      <Name>${VbProjectReference.Name}</Name>
      <Project>{${VbProjectReference.Guid.ToString()}}</Project>
      <Package>{F184B08F-C81C-45F6-A57F-5ABD9991F28F}</Package>
    </ProjectReference>
<#
	end for
#>
  </ItemGroup>
  <ItemGroup>
    <Import Include="Microsoft.VisualBasic" />
    <Import Include="System" />
    <Import Include="System.Collections" />
    <Import Include="System.Data" />
    <Import Include="System.Diagnostics" />
  </ItemGroup>
  <ItemGroup>
<#
	for each File in VbProject.Includes where File.Type="vb"
#>
    <Compile Include="${File.Name}.vb">
      <SubType>Code</SubType>
    </Compile>
<#
	end for
#>
  </ItemGroup>
  <ItemGroup>
<#
	for each File in VbProject.Includes where File.Type="hbm.xml"
#>
    <EmbeddedResource Include="${File.Name}.hbm.xml" />
<#
	end for
#>
  </ItemGroup>
  <Import Project="$(MSBuildBinPath)\Microsoft.VisualBasic.targets" />
  <!-- To modify your build process, add your task inside one of the targets below and uncomment it. 
       Other similar extension points exist, see Microsoft.Common.targets.
  <Target Name="BeforeBuild">
  </Target>
  <Target Name="AfterBuild">
  </Target>
  -->
</Project>