using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Tempest.Boot.Conventions.Defaults;
using Tempest.Core.Generator;
using Tempest.Core.IntegrationTests.EndToEnd.Base;

namespace Tempest.Core.IntegrationTests.EndToEnd.Helpers
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
