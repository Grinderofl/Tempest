using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tempest.Generator.Empty
{
    public class EmptyGenerator : Core.Generator
    {
        public IEnumerable<string> FilesToHaveAtTheEnd { get; set; } = new[]
        {
            "project.json",
            "EmptyGenerator.cs",
            "Tempest.Generator.Empty.xproj"
        };

        private string _projectName;

        public EmptyGenerator()
        {
            Console.WriteLine("Give me your project's name");
            _projectName = Console.ReadLine();
            Copy.Template("project.json").ToFile("project.json");
            Copy.Template("Program.cs").ToFile("Program.cs");
            Copy.Template("Template.xproj").ToFile(() => $"{_projectName}");
        }


    }
}
