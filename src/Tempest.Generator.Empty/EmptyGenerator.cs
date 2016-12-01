using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tempest.Core;
using Tempest.Core.Configuration;

namespace Tempest.Generator.Empty
{
    public class EmptyGenerator : GeneratorBase
    {
        public IEnumerable<string> FilesToHaveAtTheEnd { get; set; } = new[]
        {
            "project.json",
            "EmptyGenerator.cs",
            "Tempest.Generator.Empty.xproj"
        };

        private class OptionValues
        {
            public const string NewProject = "new";
        }

        private string _projectName;
        private string _projectType;
        

        protected override IEnumerable<OptionItem> SetupOptions()
        {
            // Split into SimpleList and ComplexList
            // Simple list is for stuff where choices only return one value performed by list
            // Complex list is for stuff where choices all do their own thing
            yield return 
                Options
                    .List("Welcome to empty Tempest template generator!", value => { _projectType = value; })
                    .Choice("New project", "new");

            yield return 
                Options
                    .Input("Please choose the name for your generator", value => { _projectName = value; })
                    .When(() => _projectType == "new");
        }

        protected override void ExecuteCore()
        {
            Copy.Template("project.json").ToFile("project.json");
            Copy.Template("Program.cs").ToFile("Program.cs");
            Copy.Template("Template.xproj").ToFile(() => $"{_projectName}.xproj");
        }
    }
}
