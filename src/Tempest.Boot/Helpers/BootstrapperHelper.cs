using System;
using System.IO;
using Tempest.Core.Generator;

namespace Tempest.Boot.Helpers
{
    public class BootstrapperHelper
    {
        public static GeneratorContext CreateTestContext(Type generatorType, Action<GeneratorContext> configurationAction = null)
        {
            var context = new GeneratorContext()
            {
                GeneratorType = generatorType,
                WorkingDirectory = new DirectoryInfo("C:\\")
            };
            configurationAction?.Invoke(context);
            return context;
        }

        public static GeneratorContext CreateTestContext<TGenerator>(Action<GeneratorContext> configurationAction = null) where TGenerator : GeneratorBase
            => CreateTestContext(typeof(TGenerator), configurationAction);
    }
}