#tool nuget:?package=NUnit.ConsoleRunner&version=3.6.0
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");
var build = Argument("build", "1.0.0");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

var isAppVeyorBuild = AppVeyor.IsRunningOnAppVeyor;

// Define directories.
var buildDir = Directory("./src/Example/bin") + Directory(configuration);

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
	.Does(() =>
{
	CleanDirectory(buildDir);
});

Task("Restore-NuGet-Packages")
	.IsDependentOn("Clean")
	.Does(() =>
{
	NuGetRestore("./src/Example.sln");
});

Task("Build")
	.IsDependentOn("Restore-NuGet-Packages")
	.Does(() =>
{
	if(IsRunningOnWindows())
	{
	  // Use MSBuild
	  MSBuild("./src/Example.sln", new MSBuildSettings().SetConfiguration(configuration));
	}
	else
	{
	  // Use XBuild
	  XBuild("./src/Example.sln", settings =>
		settings.SetConfiguration(configuration));
	}
});

Task("Connect-ReportPortal")
	.IsDependentOn("Build")
	.Does(() =>
{
	System.IO.File.WriteAllText("tools/NUnit.ConsoleRunner/tools/ReportPortal.addins", "../../../src/Example/bin/" + configuration + "/ReportPortal.NUnitExtension.dll\r\n../../../src/Example/bin/" + configuration + "/Example.dll");
});

Task("Run-Unit-Tests")
	.IsDependentOn("Connect-ReportPortal")
	.Does(() =>
{
	try
	{
		NUnit3("./src/**/bin/" + configuration + "/Example.dll", new NUnit3Settings {
			NoResults = true,
			TeamCity = true
			});
	}
	catch(Exception exp) {}
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
	.IsDependentOn("Run-Unit-Tests");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
