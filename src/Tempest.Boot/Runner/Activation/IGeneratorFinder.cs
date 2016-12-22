using System;
using System.Collections.Generic;
using System.IO;

namespace Tempest.Boot.Runner.Activation
{
    public interface IGeneratorFinder
    {
        Type LocateGenerator(DirectoryInfo[] directoriesToSearch, string generatorName);
        IEnumerable<Type> LocateGenerators(DirectoryInfo[] directoriesToSearch);
    }
}