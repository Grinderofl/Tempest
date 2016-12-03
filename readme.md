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
