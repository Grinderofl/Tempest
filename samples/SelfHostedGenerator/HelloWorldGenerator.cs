using System;
using Tempest.Core.Configuration.Operations.OperationBuilding;
using Tempest.Core.Generator;
using Tempest.Core.Options.Impl;
using Tempest.Core.Options.Rendering;
using Tempest.Core.Scaffolding;

namespace SelfHostedGenerator
{
    public class HelloWorldGenerator : GeneratorBase
    {
        private string _name;

        public HelloWorldGenerator(RenderOptions renderOptions)
        {
            renderOptions.RenderColors[ColorType.SpecialTextHighlightForeground] = ConsoleColor.Red;
        }

        protected override void ConfigureOptions(OptionsFactory options)
        {
            
            options.Check("Just a check that does nothing")
                .Choice("Choice 1", "1", () => {})
                .Choice("Choice 2", "2", () => {})
                .Choice("Choice 3", "3", () => {});

            options.List("Just a list that does nothing")
                .Choice("Choice 1", "1")
                .Choice("Choice 2", "2");

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