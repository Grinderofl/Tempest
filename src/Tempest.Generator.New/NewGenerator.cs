using System;
using System.Collections.Generic;
using Tempest.Core;
using Tempest.Core.Options;

namespace Tempest.Generator.New
{
    public class NewGenerator : GeneratorBase
    {
        private string _generatorName;

        private bool _includeBuildScript;
        private string _buildScriptType;

        private bool _useConventionalStructure = false;

        public NewGenerator()
        {

        }

        protected override IEnumerable<ConfigurationOption> SetupOptions()
        {

           
            yield return
                Options.Input("Welcome to new Tempest Template generator! \n" +
                              "Please enter the name of your Generator",
                    s => _generatorName = s);

            yield return
                Options
                    .List("Would you like to include a build script?")
                    .Choice("Sure!", "build", () => _includeBuildScript = true)
                    .Choice("I'll pass", "nobuild", () => { });


            yield return
                Options
                    .List("What build script would you like?", s => _buildScriptType = s)
                    .When(() => _includeBuildScript)
                    .Choice("I'll have me some AppVeyor!", "appveyor")
                    .Choice("Oops, pressed wrong button, don't really need.", "nobuild");

            yield return
                Options
                    .List(() => $"Care for your project to be prefixed with 'Tempest.Generator.{_generatorName}'?")
                    .Choice("No problem m8", "conventional", () => { _useConventionalStructure = true; })
                    .Choice("Sod off", "standard");


        }

        private readonly Func<string, string> _locateResource =
            templateFile => $"Tempest.Generator.New.Template.{templateFile}";

        protected override void ExecuteCore()
        {
            var targetDirectory = _generatorName;
            var srcDirectory = "./";

            // Can't embed stuff outside project folder currently, wtf project.json :/
            var projectRelativeTemplateMatch = "Template\\\\**\\\\*.*";
            
            if (_useConventionalStructure)
            {
                targetDirectory = $"Tempest.Generator.{_generatorName}";
                srcDirectory = $"src/Tempest.Generator.{_generatorName}/";
                projectRelativeTemplateMatch = $"Template\\\\**\\\\*.*";
            }

            Set.TargetSubDirectory(targetDirectory);

            Globally.TransformToken("SRCDIRECTORY", srcDirectory);
            Globally.TransformToken("Zing", _generatorName);


           
            Copy.Resource(_locateResource("project.json"))
                .ToFile($"{srcDirectory}project.json")
                .ReplaceToken("TEMPLATEMATCHINGPATTERN", projectRelativeTemplateMatch);

            Copy.Resource(_locateResource("ZingGenerator.cs")).ToFile(() => $"{srcDirectory}{_generatorName}.cs");

            Copy.Resource(_locateResource("replace.me")).ToFile(() => $"{srcDirectory}Template/replace.me");

            if (_includeBuildScript && _buildScriptType == "appveyor")
            {
                Copy.Resource(_locateResource("build.ps1")).ToFile("build.ps1");
                Copy.Resource(_locateResource("build.cake")).ToFile("build.cake");
            }
        }
    }
}
