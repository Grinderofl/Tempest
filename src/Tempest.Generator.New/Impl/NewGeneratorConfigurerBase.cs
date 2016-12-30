﻿using System;
using Tempest.Core.Scaffolding;

namespace Tempest.Generator.New.Impl
{
    public abstract class NewGeneratorConfigurerBase : AbstractScaffolderConfigurer
    {
        protected NewGeneratorOptions GeneratorOptions;

        protected NewGeneratorConfigurerBase(NewGeneratorOptions generatorOptions)
        {
            GeneratorOptions = generatorOptions;
        }

        public override int Order => 0;

        protected virtual string ResourceFormat => "Tempest.Generator.New.Template.{0}";

        protected virtual string BuildResource(string s) => string.Format(ResourceFormat, s);

        protected virtual string BuildTarget(string target) =>
            $"src/Tempest.Generator.{GeneratorOptions.GeneratorName}/{target}";

        protected virtual void CopyGeneratorFiles(IScaffoldBuilder builder)
        {
            builder.Copy.ResourceOf<NewGenerator>(BuildResource("HelloWorldGenerator.cs"))
                .ToFile(BuildTarget("HelloWorldGenerator.cs"));
        }

        protected virtual void CopyBuildScript(IScaffoldBuilder builder)
        {
            Console.WriteLine("Creating build script");
            // Only supporting appveyor
            if ((GeneratorOptions.BuildScriptTypes & BuildScriptTypes.AppVeyor) != 0)
            {
                Console.WriteLine("Creating appveyor script");
                if (GeneratorOptions.SelfHosted)
                    builder.Copy.ResourceOf<NewGenerator>(BuildResource("SelfHosted.build.cake"))
                        .ToFile("build.cake");
                else
                    builder.Copy.ResourceOf<NewGenerator>(BuildResource("Library.build.cake"))
                        .ToFile("build.cake");


                builder.Copy.ResourceOf<NewGenerator>(BuildResource("build.ps1"))
                    .ToFile("build.ps1");
            }
        }

        protected virtual void CopyGeneratorTemplateFiles(IScaffoldBuilder builder)
        {
            builder.Copy.ResourceOf<NewGenerator>(BuildResource("Template.Program.cs"))
                .ToFile(BuildTarget("Template/Program.cs"));

            builder.Copy.ResourceOf<NewGenerator>(BuildResource("Template.project.json"))
                .ToFile(BuildTarget("Template/project.json"))
                .TransformToken("TEMPLATEMATCHINGPATTERN", $"Template\\\\**\\\\*.*");

            builder.Copy.ResourceOf<NewGenerator>(BuildResource("Template.ReplaceMeGreeter.cs"))
                .ToFile(BuildTarget($"Template/{GeneratorOptions.GeneratorName}Greeter.cs"));
        }

        protected override void ConfigureScaffolder(IScaffoldBuilder builder)
        {
            builder.Globally.TransformToken("HelloWorld", GeneratorOptions.GeneratorName);
            builder.Globally.TransformToken($"SRCDIRECTORY", BuildTarget(""));

            builder.Set.TargetSubDirectory(GeneratorOptions.GeneratorName);

            CopyGeneratorFiles(builder);

            if(GeneratorOptions.IncludeBuildScript)
                CopyBuildScript(builder);

            CopyGeneratorTemplateFiles(builder);
        }
    }
}