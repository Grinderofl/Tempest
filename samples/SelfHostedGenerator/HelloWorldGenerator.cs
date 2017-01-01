using System;
using Tempest.Core.Configuration.Operations.OperationBuilding;
using Tempest.Core.Generator;
using Tempest.Core.Scaffolding;

namespace SelfHostedGenerator
{
    public class HelloWorldGenerator : GeneratorBase
    {
        private string _name;

        protected override void ConfigureOptions(OptionsFactory options)
        {
            options.Input("Welcome to Hello World generator. Please enter the name: ", s => _name = s);
        }

        protected override void ConfigureGenerator(IScaffoldBuilder builder)
        {
            builder.Globally.TransformToken("ReplaceMe", _name);
            builder.Set.TargetSubDirectory(_name);

            Func<string, string> resource = templateFile => $"SelfHostedGenerator.Template.{templateFile}";
            builder.Copy.ResourceOf<HelloWorldGenerator>(resource("project.json")).ToFile("project.json");
            builder.Copy.ResourceOf<HelloWorldGenerator>(resource("Program.cs")).ToFile("Program.cs");
            builder.Copy.ResourceOf<HelloWorldGenerator>(resource("ReplaceMeGreeter.cs")).ToFile($"{_name}.cs");
        }
    }
}