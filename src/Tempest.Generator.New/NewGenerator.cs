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

        protected override IEnumerable<ConfigurationOption> SetupOptions()
        {
            yield return
                Options.Input("Welcome to new Tempest Template generator! \n " +
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
                    .Choice("Exit", "nobuild");
        }

        protected override void ExecuteCore()
        {
            Set.TargetSubDirectory(_generatorName);
            Globally.TransformToken("Zing", _generatorName);

            Func<string, string> resource = templateFile => $"Tempest.Generator.New.Template.{templateFile}";

            Copy.Resource(resource("project.json")).ToFile("project.json");
            Copy.Resource(resource("ZingGenerator.cs")).ToFile(() => $"{_generatorName}.cs");

            if (_includeBuildScript && _buildScriptType == "appveyor")
                Copy.Resource(resource("build.ps1")).ToFile("build.ps1");
        }
    }
}
