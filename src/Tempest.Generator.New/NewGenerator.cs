using System;
using System.Collections.Generic;
using Tempest.Core;
using Tempest.Core.Configuration.Operations.OperationBuilding;
using Tempest.Core.Configuration.Options;
using Tempest.Core.Configuration.Options.Base;
using Tempest.Core.Configuration.Options.Impl;
using Tempest.Core.Generator;
using Tempest.Core.Operations;
using Tempest.Core.Options;
using Tempest.Core.Scaffolding;

namespace Tempest.Generator.New
{
    public class NewGenerator : GeneratorBase
    {
        private readonly NewGeneratorOptions _options;
//
//        private bool _includeBuildScript;
//        private string _buildScriptType;
//
//        private bool _useConventionalStructure = false;


//        private readonly Func<string, string> _locateResource =
//            templateFile => $"Tempest.Generator.New.Template.{templateFile}";

        public NewGenerator(NewGeneratorOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            _options = options;
        }

        protected override void ConfigureOptions(OptionsFactory options)
        {
            options.Input("Welcome to new Tempest Template generator! \n" +
                          "Please enter the name of your Generator",
                s => _options.GeneratorName = s);

            options.List("Self-hosted or Library?")
                .Choice("Self-hosted", "selfhosted", () => _options.SelfHosted = true)
                .Choice("Library", "library");

            options
                .List("Would you like to include a build script?")
                .Choice("Sure!", "build", () => _options.IncludeBuildScript = true)
                .Choice("I'll pass", "nobuild", () => { });

            options
                .Check("What build script would you like?")
                .When(() => _options.IncludeBuildScript)
                .Choice("I'll have me some AppVeyor!", "appveyor",
                    () => _options.BuildScriptTypes |= BuildScriptTypes.AppVeyor);
        }

        protected override void ConfigureGenerator(IScaffoldBuilder builder)
        {
//            string generatorName = _options.GeneratorName;
//            var targetDirectory = generatorName;
//            var srcDirectory = "./";
//
//            // Can't embed stuff outside project folder currently, wtf project.json :/
//            var projectRelativeTemplateMatch = "Template\\\\**\\\\*.*";
//
//            if (_useConventionalStructure)
//            {
//                targetDirectory = $"Tempest.Generator.{generatorName}";
//                srcDirectory = $"src/Tempest.Generator.{generatorName}/";
//                projectRelativeTemplateMatch = $"Template\\\\**\\\\*.*";
//            }
//
//            builder.Set.TargetSubDirectory(targetDirectory);
//
//            builder.Globally.TransformToken("SRCDIRECTORY", srcDirectory);
//            builder.Globally.TransformToken("Zing", generatorName);
//
//            builder.Copy.ResourceOf<NewGenerator>(_locateResource("Library/project.json"))
//                .ToFile($"{srcDirectory}project.json")
//                .TransformToken("TEMPLATEMATCHINGPATTERN", projectRelativeTemplateMatch);
//
//            builder.Copy.ResourceOf<NewGenerator>(_locateResource("ZingGenerator.cs")).ToFile(() => $"{srcDirectory}{generatorName}.cs");
//
//            builder.Copy.ResourceOf<NewGenerator>(_locateResource("replace.me")).ToFile(() => $"{srcDirectory}Template/replace.me");
//
//            if (_includeBuildScript && _buildScriptType == "appveyor")
//            {
//                builder.Copy.ResourceOf<NewGenerator>(_locateResource("build.ps1")).ToFile("build.ps1");
//                builder.Copy.ResourceOf<NewGenerator>(_locateResource("Library/build.cake")).ToFile("build.cake");
//            }
        }
    }
}
