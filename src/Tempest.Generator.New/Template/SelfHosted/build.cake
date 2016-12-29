var target = Argument("target", "Default");

Task("Restore")
    .Does(() =>
    {
        DotNetCoreRestore();
    });

Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        DotNetCoreBuild("./SRCDIRECTORY/project.json");
        //DotNetCoreBuild("./test/**/project.json");
    });

/*Task("Test")
    .IsDependentOn("Build")
    .Does(() => 
    {
        var directories = System.IO.Directory.GetDirectories("./test/");
        foreach(var directory in directories)
            DotNetCoreTest(directory);
    });*/

Task("Package")
    //.IsDependentOn("Test")
	.IsDependentOn("Build")
    .Does(() =>
    {
        var publishRoot = "./artifacts/Zing/";

        var settings = new DotNetCorePublishSettings
        {
            Configuration = "Release",
            OutputDirectory = publishRoot
        };
        DotNetCorePublish("SRCDIRECTORY", settings);
        Zip(publishRoot, "./artifacts/Zing.zip");
    });
    
Task("Upload-AppVeyor-Artifacts")
    .IsDependentOn("Package")
    .WithCriteria(() => AppVeyor.IsRunningOnAppVeyor)
    .Does(() =>
{
    var artifact = MakeAbsolute(File(@"./artifacts/Zing.zip"));
    AppVeyor.AddInformationalMessage("Uploading artifacts");
    AppVeyor.UploadArtifact(artifact, settings => settings
        .SetArtifactType(AppVeyorUploadArtifactType.NuGetPackage)
    );
});

Task("Publish")
    .IsDependentOn("Upload-AppVeyor-Artifacts")
    .Does(() => {
        Information("Published");
    });

Task("Default")
    .IsDependentOn("Package")
    .Does(() =>
    {
		Information("Packaged"); 
    });

RunTarget(target);