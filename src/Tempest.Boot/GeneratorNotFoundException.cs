using System;
using System.Reflection;

namespace Tempest.Boot
{
    public class GeneratorNotFoundException : Exception
    {
        public GeneratorNotFoundException(string generatorName, Assembly assembly)
            : base($"The generator {generatorName} could not be found. Searched assembly: '{assembly.FullName}'")
        {
        }

        public GeneratorNotFoundException(string generatorName)
            : base($"The generator {generatorName} could not be loaded.")
        {
        }
    }
}