#tool "nuget:?package=GitVersion.CommandLine"
#addin nuget:?package=Cake.Git

var target = Argument("target", Tasks.Test);
var configuration = Argument("configuration", Configurations.Release);

var versionInfo = default(GitVersion);

var publishRoot = "./artifacts/";


private class Tasks
{
    public static string UpdateAppVeyor = "Update-AppVeyor";
    public static string Build = "Build"; 
    public static string Test = "Test";
    public static string Package = "Package";

    public static string UploadArtifacts = "Upload-Artifacts";
    public static string UploadTestResults = "Upload-TestResults";

    public static string Publish = "Publish";
}

private class Configurations
{
    public static string Debug = "Debug";
    public static string Release = "Release";
}

void DotNetCoreTestGlob(string glob)
{
	var directories = System.IO.Directory.EnumerateDirectories(glob);
	
	foreach(var directory in directories.Where(d => IsTestable(d)))
	{
		var directoryName = directory.Substring(searchPath.Length);
		var resultsFile = "./artifacts/testResults-" + directoryName + ".xml";
		var arg = string.Format("-xml \"{0}\"", resultsFile);
		var settings = new DotNetCoreTestSettings(){
			ArgumentCustomization = args => args.Append(arg)
		};
		DotNetCoreTest(directory, settings);
	}
}

bool IsTestable(string path) 
{
	var projectFile = path + "\\project.json";
	return System.IO.File.Exists(projectFile);
}

Setup(context => {
    try {
        var gitVersionSettings = new GitVersionSettings(){
            ArgumentCustomization = args => args.Append("/nofetch")
        };
        versioninfo = GitVersion(gitVersionSettings);
    }
    catch(Exception)
    {
        var path = Directory(".");
        var commit = GitLogTip(path);
        var hash = commit.Sha.Substring(0,8);
        versionInfo = new GitVersion(){
            PreReleaseTag = "Unknown-" + hash,
            FullSemVer = "1.0.0-Unknown-" + hash;
        };
    }

    if(versionInfo != null) {
        Information("Version: {0}", versionInfo.FullSemVer);
    } else {
        throw new Exception("Unable to determine semver version");
    }
});




Task(Tasks.UpdateAppVeyor)
    .WithCriteria(() => AppVeyor.IsRunningOnAppVeyor)
    .Does(() => { 
        AppVeyor.UpdateBuildVersion(versionInfo.FullSemVer +"+" +AppVeyor.Environment.Build.Number);
    });

Task(Tasks.Build)
    .Does(() => {
        DotNetCoreRestore();
        DotNetCoreBuild("./src/**/project.json");
        DotNetCoreBuild("./test/**/project.json");
    });

Task(Tasks.Test)
    .IsDependentOn(Tasks.Build)
    .Does(() => {
        DotNetCoreTestGlob("./test/")
    });

Task(Tasks.Package)
    .IsDependentOn(Tasks.Test)
    .Does(() =>{
        var settings = new DotNetCorePackSettings {
            Configuration = Configurations.Release,
            OutputDirectory = publishRoot,
            VersionSuffix = versionInfo.PreReleaseTag
        };

        DotNetCorePack("./src/Tempest/", settings);
        DotNetCorePack("./src/Tempest.Generator.Empty", settings);
    });

Task(Tasks.UploadArtifacts)
    .IsDependentOn(Tasks.Package) 
    .WithCriteria(() => AppVeyor.IsRunningOnAppVeyor)
    .Does(() =>
            {
                var artifacts = GetFiles("./artifacts/*.nupkg");
				AppVeyor.AddInformationalMessage("Uploading artifacts");
                foreach(var artifact in artifacts.Select(x => x.ToString()))
                {
					var fileInfo = System.IO.Path.GetFileName(artifact);
					Information("Adding " + fileInfo);
                    AppVeyor.UploadArtifact(artifact, settings => settings.SetArtifactType(AppVeyorUploadArtifactType.NuGetPackage));
                }
            }
    );

Task(Tasks.UploadTestResults)
	.IsDependentOn(Tasks.Test)
	.WithCriteria(() => AppVeyor.IsRunningOnAppVeyor)
	.Does(() => {
		var testResults = GetFiles("./artifacts/testResults*.xml");
		foreach(var result in testResults)
		{
			AppVeyor.UploadTestResults(result, AppVeyorTestResultsType.XUnit);
		}
	});

Task(Tasks.Publish)
	.IsDependentOn(Tasks.UploadTestResults)
    .IsDependentOn(Tasks.UploadArtifacts)
    .Does(() => {
        Information("Published");
    });

RunTarget(target);


// Not really glob...
