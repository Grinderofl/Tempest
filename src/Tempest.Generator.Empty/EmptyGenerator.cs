using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tempest.Core;
using Tempest.Core.Options;

namespace Tempest.Generator.Empty
{
    public class EmptyGenerator : GeneratorBase
    {
        private readonly Dictionary<string, Action> _projectFactories;

        public EmptyGenerator()
        {
            _projectFactories = new Dictionary<string, Action>()
            {
                [OptionValues.NewProject] = CreateNewProject
            };
        }
        
        private string _projectName;
        private string _projectType;
        

        protected override IEnumerable<ConfigurationOption> SetupOptions()
        {
            // Split into SimpleList and ComplexList
            // Simple list is for stuff where choices only return one value performed by list
            // Complex list is for stuff where choices all do their own thing
            // Better polymorphism and API.

            // Afternote:
            // Really? Better API? Why can't an option do one thing, and the choice do another thing, at the same time?

            yield return 
                Options
                    .List("Welcome to empty .NET Core generator!", value => _projectType = value)
                    .Choice("New project", OptionValues.NewProject);

            yield return 
                Options
                    .Input("Please choose the name for your generator", value => { _projectName = value; })
                    .When(() => _projectType == OptionValues.NewProject);
        }

        protected override void ExecuteCore()
        {
            Set.TargetSubDirectory(_projectName);
            _projectFactories[_projectType]();
        }

        private void CreateNewProject()
        {
            Globally.TransformToken("ReplaceMe", _projectName);


            // Uncomment/comment either below as needed
            CopyFromTemplates();
            //CopyFromResources();
        }

        /// <summary>
        /// Generate project using embedded resources
        /// </summary>
        private void CopyFromResources()
        {
            Func<string, string> resource = templateFile => $"Tempest.Generator.Empty.Template.{templateFile}";

            Copy.Resource(resource("project.json")).ToFile("project2.json");
            Copy.Resource(resource("Program.cs")).ToFile("Program2.cs");
            Copy.Resource(resource("ReplaceMeGreeter.cs")).ToFile(() => $"{_projectName}_.cs");
        }

        /// <summary>
        /// Generate project using templates
        /// </summary>
        private void CopyFromTemplates()
        {
            Copy.TemplatePattern("./*.cs").ToFiles();
            //Copy.Templates(templatesRelativePath: "relative/Path/With/**/pattern", includeSubDirectories: true)
            //    .ToLocation("");
            Copy.Template("project.json").ToFile("project.json");
            //Copy.Template("Program.cs").ToFile("Program.cs");
            //Copy.Template("ReplaceMeGreeter.cs").ToFile(() => $"{_projectName}.cs");
        }


        /// <summary>
        /// Parameters used for possible input options
        /// </summary>
        private static class OptionValues
        {
            public const string NewProject = "new";
        }
    }
}
