# This project has been deprecated in favour of the new templating engine:

https://github.com/dotnet/templating




[![Build status](https://ci.appveyor.com/api/projects/status/ntpiso2gjotxmrmd?svg=true)](https://ci.appveyor.com/project/Grinderofl/tempest)


## Tempest

Ez-mode cross-platform template and scaffolding framework.

### What makes Tempest awesome?
* Super-simple Fluent API
* Generators are DLL's!)
* Generators can be hosted and downloaded from NuGet (using packages.config, automatic discovery coming)
* Generator can do virtually anything you could imagine - may need to make use of [OCP](https://en.wikipedia.org/wiki/Open/closed_principle) though!
* Built on .NET Core - lightning fast, cross-platform
* Create your Generator in Visual Studio!

#### Goals

To have a method of creating template (or otherwise sourced) scaffolders and generators quickly and easily while having a simple and readable syntax.

This project has grown out of the need for a way to easily create and alter .NET Core Solution structure in a manner that allows to preconfigure bunch of boilerplate actions that are usually done by the developer - to allow them to spend more time writing functional code instead of setting up projects, remembering to add specific dependencies, creating build scripts, oh and project.json is also going bye bye so now the best choice to update your project files (or `Project.csproj`) programmatically is via XDT transformation ಠ_ಠ

Part of the idea is to allow the creation of quick microservices that are compatible with Azure, but could potentially be configured to go anywhere.


### Quick start to using a generator

1. Go to the directory you want to install Tempest into and execute the following powershell command

```powershell
Invoke-WebRequest https://raw.githubusercontent.com/Grinderofl/Tempest/develop/install/install.ps1 -OutFile install.ps1
./install
```

2. Start using `tempest` commands!

### Quick start to creating your own Generator

1. Navigate to the root of where you want your template to be on your favorite command processor that has `tempest` in its environment PATH variable
2. Execute `tempest new {YourGeneratorName}`
3. A directory {YourGeneratorName} will be created, and a ready-to-build new generator base will be created for you
4. Update your template files to be included on compilation as resources or copied files
5. Build, run Tempest with generator search directory parameter set to your build directory: `tempest -s|--search "C:/YourProject/bin/Debug" YourProject`

Alternative manual steps:

1. Create a new .NET Core Library
2. Add dependency to `Tempest.Core`
3. Inherit from `GeneratorBase`
4. Add your template files to be included on compilation as resources or copied files
5. Build, run Tempest with generator search directory parameter set to your build directory: `tempest -s|--search "C:/YourProject/bin/Debug" YourProject`

### Generating stuff

When you inherit from GeneratorBase, you will implement ConfigureGenerator which has a parameter of type `IScaffoldBuilder` that can be used to set up scaffolding actions.

```c#
protected override void ConfigureGenerator(IScaffoldBuilder builder)
{
  // Copies a file in your template folder called "YourTemplateFile"
  // And places it into target folder as "YourTargetFile"
  builder.Copy.Template("YourTemplateFile").ToFile("YourTargetFile");
  
  // Copies an embedded resource named "ResourceFile"
  // And places it into target folder as "YourTargetFile"
  builder.Copy.Resource("YourGenerator.Namespace.Path.To.ResourceFile").ToFile("YourTargetFile");
  
  // Creates an empty file with contents "Foo!"
  // And places it into target folder as "YourTargetFile"
  builder.Create.Text("Foo!").ToFile("YourTargetFile");
  
}
```


#### Configuring your generator

Naturally you might want to add some parameters, selectable menu items, get input from user, etc. that can be done quite easily. There's a method that you need to override with the signature `void ConfigureOptions(OptionsFactory options)`. These options will be executed before `ConfigureGenerator(IScaffoldBuilder)`, and everything you set through the actions will be available by that time.

The best way to use this is by utilising the fluent API:

```c#
private string _targetFileName = "YourTargetFile";
private string _action;

protected override void ConfigureOptions(OptionsFactory options)
{
  // First option provides a list with two choices
  //   Generate default file (skip to generation)
  //   Specify a name
  
  options
      .List("What would you like to do?", s => _action = s)
      .Choice("Just generate default file", "default")
      .Choice("Specify target file name", "specific");
      
  // Second option is only displayed when the first option
  // yielded a "specific" action
  
    options
      .Input("Please enter the name for the target file:", s => _targetFileName = s)
      .When(() => _action == "specific");
}

```

These options are automatically available as command line parameters in a list as their ordered indexes after the generator's name. In this case, you could do:

* `tempest YourGenerator default` to skip all options and generate the default file
* `tempest YourGenerator specific YourNewTargetFile` to skip all options and generate 'YourNewTargetFile'
* `tempest YourGenerator specific` to go directly to asking file name


#### Self-hosting generators

Sometimes you might want to have your generator without the extra `tempest` commands. There `(will be)` an option in New generator to create a fresh template with all prerequisites, there's also a sample with a self-hosted generator.

To describe the process of self-hosting:

1) Create Generator Context (use `GeneratorContextFactory` static methods)
2) Create Generator Bootstrapper (use `GeneratorBootstrapperFactory` instanced methods)
3) Execute the strapper


#### Other parameters for tempest:

`tempest -g|--generator GeneratorName` - Specifies generator name differently

`tempest -a|--args "arguments passed to-generator"` - Specifies generator arguments. If more than one argument, surround with quotes.

`tempest -s|--search "C:/path/to/search/for/generator"` - Specifies the path to search for generator


#### Future plans:

Complete filesystem abstraction

`tempest --add-search "C:/Your/Generators/Search/Path"` - adds a path to look for generators

`tempest --remove-search "C:/Your/Generators/Search/Path"` - removes a path from being looked for generators


`tempest --set-install "C:/Your/Generators/Install/Path"` - sets where Tempest should install generators

* Automatic discovery of packages from Nuget when you try to run a generator. 
