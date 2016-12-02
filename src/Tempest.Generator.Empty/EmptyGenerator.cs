using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tempest.Core;
using Tempest.Core.Options;
using Tempest.Core.Transformation;

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
            yield return 
                Options
                    .List("Welcome to empty .NET Core generator!", value => { _projectType = value; })
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
            Globally.Transform.Token("ReplaceMe", _projectName);

            Copy.Template("project.json").ToFile("project.json");
            Copy.Template("Program.cs").ToFile("Program.cs");
            Copy.Template("ReplaceMe.cs").ToFile(() => $"{_projectName}.cs");
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
