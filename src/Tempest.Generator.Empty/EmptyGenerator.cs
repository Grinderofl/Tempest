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

        public EmptyGenerator()
        {
            
        }


    }
}
