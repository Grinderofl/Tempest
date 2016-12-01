using System;
using System.Linq;
using System.Reflection;
using Tempest.Core;

namespace Tempest
{
    public class GeneratorLoader : IGeneratorLoader
    {
        // Generator loader
        // Finds the DLL with the generator name
        // Loads the DLL
        // Loads the Generator type

        private readonly IGeneratorAssemblyFinder _generatorAssemblyFinder;

        public GeneratorLoader(IGeneratorAssemblyFinder generatorAssemblyFinder)
        {
            _generatorAssemblyFinder = generatorAssemblyFinder;
        }

        public GeneratorEngineBase FindGeneratorByName(string generatorName)
        {
            var generatorAssembly = _generatorAssemblyFinder.FindByName(generatorName);
            var generatorType = FindGeneratorType(generatorAssembly, generatorName);
            if(generatorType == null)
                throw new GeneratorNotFoundException(generatorName, generatorAssembly);

            return Activator.CreateInstance(generatorType) as GeneratorEngineBase;
        }

        private Type FindGeneratorType(Assembly generatorAssembly, string generatorName)
        {
            var implementsGeneratorType =
                generatorAssembly.ExportedTypes.Where(x => IntrospectionExtensions.GetTypeInfo(x).IsSubclassOf(typeof(GeneratorBase)));

            var containsName = implementsGeneratorType.Where(x => x.Name.Contains(generatorName));
            return containsName.FirstOrDefault();
        }
    }
}