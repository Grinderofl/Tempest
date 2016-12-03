[![Build status](https://ci.appveyor.com/api/projects/status/ntpiso2gjotxmrmd?svg=true)](https://ci.appveyor.com/project/Grinderofl/tempest)


## Tempest

Ez-mode cross-platform template generator.

### What makes Tempest awesome?
* Super-simple Fluent API
* Generators are DLL's!
* Generators are automatically discovered and downloaded from NuGet (TODO)
* Generator can do virtually anything you could imagine - may need to make use of [OCP](https://en.wikipedia.org/wiki/Open/closed_principle) though!
* Built on .NET Core - lightning fast, cross-platform
* Create your Generator in Visual Studio!

#### Goals

This project has grown out of the need for a way to easily create and alter .NET Core Solution structure in a manner that allows to preconfigure bunch of boilerplate actions that are usually done by the developer - to allow them to spend more time writing functional code instead of setting up projects, remembering to add specific dependencies, creating build scripts, oh and project.json is going bye bye so now the best choice to update your project files (or `Project.csproj`) programmatically is via XDT transformation.

Part of the idea is to allow the creation of quick microservices that are compatible with Azure, but could potentially be configured to go anywhere. This sort of stuff is important if you build lots of web services for living that make use of common architectures that loads of other developers can jump in and change - because they all use same conventions and structures!


I'm also a big fan of C# & .NET Core, and the only option previously was to use tools like Yeoman which, while good at what they do, unfortunately do not allow tools to help you as much as .NET does, nor did they have an overly user friendly API, hence Tempest was born.

It should be easy to add in any additional Transformers and Emitters that are needed. For example - you could have a `readme.md` added to each directory - you'll only have to specify that you need `readme.md` once, but emitted into many different locations - making your code more readable.


### Quick start to creating your own Generator

1)  Navigate to the root of where you want your template to be on your favorite command processor that has `tempest` in its path
2) `tempest tempest new {YourGeneratorName}`
3) A directory {YourGeneratorName} will be created, and a ready-to-build new generator base will be created for you

Alternative manual steps:

1) Create a new .NET Core Library
2) Add dependency to `Tempest.Core`
3) Inherit from `GeneratorBase`
4) Build, run Tempest with generator search directory parameter set to your build directory:
`tempest -s|--search "C:/YourProject/bin/Debug" YourProject` (TODO)


#### Installing generators (TODO)

`tempest -i|--install GeneratorName`

Tempest will attempt to first conventionally find a package titled `Tempest.Generator.{GeneratorName}` from the NuGet sources in its `NuGet.config` file, then also `{GeneratorName}`, and install it. If the parameter provided ends with `.zip` and exists, Tempest will try to unpack that instead. If the generator is already installed, it'll say it's already installed. Standard nuget package matching applies.

#### Updating generators (TODO)
`tempest -u|--update` will attempt to update all generators.
`tempest -u|--update GeneratorName` will attempt to update the specified generator.
The convention based matching applies - first for `Tempest.Generator.{GeneratorName}` then `{GeneratorName}`.

#### Getting rid of generators (TODO)

`tempest -r|--remove GeneratorName` removes the specified generator
Convention based matching applies.


#### Various other options

`tempest --add-search "C:/Your/Generators/Search/Path"` - adds a path to look for generators
`tempest --remove-search "C:/Your/Generators/Search/Path"` - removes a path from being looked for generators

`tempest --set-install "C:/Your/Generators/Install/Path"` - sets where Tempest should install generators

